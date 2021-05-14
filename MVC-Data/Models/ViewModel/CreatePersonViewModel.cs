using MVC_Data.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.ViewModel
{
    public class CreatePersonViewModel
    {
        [Required(ErrorMessage = "Firstname is required")]
        [StringLength(100, MinimumLength = 1)]
        [Display(Name = "Firstname")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lastname is required")]
        [StringLength(100, MinimumLength = 1)]
        [Display(Name = "Lastname")]
        public string LastName { get; set; }

        [Phone(ErrorMessage = "Only +- and 0-9 allowed")] //0-9, +-
        [Display(Name = "Phone number (optional)")]
        public string PhoneNr { get; set; }

        //public string City { get; set; }

        [Display(Name = "City")]
        public int? CityId { get; set; }

        public List<City> Cities { get; set; }

        public CreatePersonViewModel()
        {
            Cities = new List<City>();
        }

    }
}
