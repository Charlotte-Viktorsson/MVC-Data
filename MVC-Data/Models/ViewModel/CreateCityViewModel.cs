using MVC_Data.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.ViewModel
{
    public class CreateCityViewModel
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "City name")]

        public string Name { get; set; }


        [Required]
        [Display(Name = "Country")]
        public int NationId { get; set; }

        public List<Country> Countries { get; set; }

    }
}
