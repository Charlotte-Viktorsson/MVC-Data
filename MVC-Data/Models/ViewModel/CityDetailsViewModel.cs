using MVC_Data.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.ViewModel
{
    public class CityDetailsViewModel
    {
        public int Id { get; set; }

        public List<Person> Citizens { get; set; }

        public string NationName { get; set; }

        public string CityName { get; set; }
        //public CreateCityViewModel CreateVM { get; set; }
    }
}
