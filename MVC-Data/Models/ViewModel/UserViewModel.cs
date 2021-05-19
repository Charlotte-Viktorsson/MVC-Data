using MVC_Data.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.ViewModel
{
    public class UserViewModel
    {
        public List<ApplicationUser> Users { get; set; }

        public UserViewModel()
        {
            Users = new List<ApplicationUser>();
        }
    }
}
