using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralTillApp.Data
{
    public class CustomUser : IdentityUser
    {
        public string UserId { get; set; }
        [Required]
        public string UserRole { get; set; }
        public DateTime DateCreated { get; set; }

        public static CustomUser GetUser(AuthenticationState state, ApplicationDbContext context)
        {
            var user = new CustomUser();

            if(state != null && context != null)
            {
                user = context.Users.FirstOrDefault(u => u.UserName == state.User.Identity.Name);
                if(user != null)
                {
                    // User Logger in this location 
                    Console.WriteLine($"Successfully found {user.UserName} in the database");
                }
                else
                {
                    Console.WriteLine("There was an error finding the user in the database, are you " +
                        "logged in?");
                    return user;
                }
            }

            // Return user because state was null 
            return user;
        }
    }
}
