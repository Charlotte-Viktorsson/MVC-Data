using MVC_Data.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.ViewModel
{
    public class CityViewModel
    {
        public List<City> Cities { get; set; }

        public CityViewModel()
        {
            Cities = new List<City>();
        }

    }
}
