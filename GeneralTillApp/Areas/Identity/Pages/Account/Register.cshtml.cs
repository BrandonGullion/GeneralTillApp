using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using GeneralTillApp.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace GeneralTillApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        public IEnumerable<string> RoleList;
        private readonly SignInManager<CustomUser> _signInManager;
        private readonly UserManager<CustomUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;


        public RegisterModel(
            UserManager<CustomUser> userManager,
            SignInManager<CustomUser> signInManager,
            ILogger<RegisterModel> logger,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _roleManager = roleManager;

            RoleList = new List<string>()
            {
                StaticMembers.Role_Admin,
                StaticMembers.Role_Management,
                StaticMembers.Role_Default
            };
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Username")]
            public string UserName { get; set; }
            [Display(Name = "User/Employee Id")]
            public string UserId { get; set; }
            [Required]
            [Display(Name = "User Role")]
            public string UserRole { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                // This will be used to update the info for the custom user 
                var user = new CustomUser
                {
                    UserName = Input.UserName,
                    UserRole = Input.UserRole,
                    UserId = Input.UserId,
                    DateCreated = DateTime.Now,
                };

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    // Generate the required roles if they do not already exist 
                    if (!await _roleManager.RoleExistsAsync(StaticMembers.Role_Admin))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(StaticMembers.Role_Admin));
                    }
                    if (!await _roleManager.RoleExistsAsync(StaticMembers.Role_Management))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(StaticMembers.Role_Management));
                    }
                    if (!await _roleManager.RoleExistsAsync(StaticMembers.Role_Default))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(StaticMembers.Role_Default));
                    }

                    // If nothing was selected the user if added to default view 
                    if (user.UserRole == null)
                        await _userManager.AddToRoleAsync(user, StaticMembers.Role_Default);

                    // Else assign the user to the desired role
                    else
                        await _userManager.AddToRoleAsync(user, user.UserRole);


                    // Removed email verification, can be added later if required 

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
