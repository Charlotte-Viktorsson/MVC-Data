using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.ViewModel
{
    public class ManageUserRolesViewModel
    {
        public ManageUserRolesViewModel()
        {
            NotAssignedRoles = new List<string>();
            Roles = new List<string>();
        }
        public List<string> NotAssignedRoles { get; set; }


        public IList<string> Roles { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }
    }
}
