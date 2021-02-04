using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GeneralTillApp
{
    public static class StaticMembers
    {
        #region User Roles
        public static string Role_Admin { get; set; } = "Admin";
        public static string Role_Management { get; set; } = "Management";
        public static string Role_Default { get; set; } = "Default";
        #endregion

    }
}
