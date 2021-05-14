using MVC_Data.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.ViewModel
{
    public class CountryViewModel
    {
        public List<Country> Countries { get; set; }

        public CountryViewModel()
        {
            Countries = new List<Country>();
        }
    }
}
