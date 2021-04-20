using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.ViewModel
{
    public class CreatePersonViewModel
    {
        public CreatePersonViewModel()
        {
            //peopleViewModel = new PeopleViewModel();
        }

        [Required(ErrorMessage = "Firstname is required")]
        [StringLength(100, MinimumLength = 1)]
        [Display(Name = "Firstname")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lastname is required")]
        [StringLength(100, MinimumLength = 1)]
        [Display(Name = "Lastname")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(100, MinimumLength = 1)]
        public string City { get; set; }

        [Phone(ErrorMessage = "Only +- and 0-9 allowed")] //0-9, +-
        [Display(Name = "Phone number")]
        public string PhoneNr { get; set; }


        //public PeopleViewModel peopleViewModel { get; set; }

    }
}
