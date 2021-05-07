using MVC_Data.Models.Data;
using MVC_Data.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.Service
{
    public interface ICountryService
    {
        public Country Add(CreateCountryViewModel createCountry);

        public CountryViewModel All();

        public Country FindBy(int id);

        //public Country Edit(int id, Country country);

        public Country Edit(int id, EditCountryViewModel country);

        public bool Remove(int id);
    }
}
