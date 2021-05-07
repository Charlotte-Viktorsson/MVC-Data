using MVC_Data.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.ViewModel
{
    public class EditCountryViewModel
    {
        public int Id { get; set; }

        public CreateCountryViewModel CreateVM { get; set; }


        public EditCountryViewModel(int id, Country country)
        {
            Id = id;
            this.CreateVM = new CreateCountryViewModel()
            {
                Name = country.Name,
                CityList = country.Cities
            };
        }

        public EditCountryViewModel()
        {

        }

    }
}
