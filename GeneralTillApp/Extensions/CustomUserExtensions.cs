using GeneralTillApp.Data;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralTillApp.Extensions
{
    public class CustomUserExtensions 
    {
        private ApplicationDbContext _context { get; set; }

        public CustomUserExtensions(ApplicationDbContext context)
        {
            _context = context;
        }

        public CustomUser GetCustomUser (AuthenticationState authState)
        {
            var user = _context.CustomUsers.FirstOrDefault(u => u.UserName == authState.User.Identity.Name);
            if (user != null)
                return user;
            else
                return new CustomUser();
        }
    }
}
