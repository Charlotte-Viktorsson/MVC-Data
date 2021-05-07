using MVC_Data.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.ViewModel
{
    public class CreateCountryViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Country name")]
        public string Name { get; set; }


        public List<City> CityList { get; set; }

        public CreateCountryViewModel()
        {
            CityList = new List<City>();
        }
    }
}
