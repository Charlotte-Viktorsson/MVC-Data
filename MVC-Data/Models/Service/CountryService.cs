using MVC_Data.Models.Data;
using MVC_Data.Models.DataAccess;
using MVC_Data.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.Service
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepo _countryRepo;

        public CountryService(ICountryRepo countryRepo)
        {
            _countryRepo = countryRepo;
        }

        public Country Add(CreateCountryViewModel createCountry)
        {
            Country country = new Country();
            country.Name = createCountry.Name;
            country.Cities = createCountry.CityList;
            return _countryRepo.Create(country);
        }

        public CountryViewModel All()
        {
            CountryViewModel countryViewModel = new CountryViewModel();
            countryViewModel.Countries = _countryRepo.Read();
            return countryViewModel;
        }

        public Country FindBy(int id)
        {
            return _countryRepo.Read(id);
        }

        public Country Edit(int id, EditCountryViewModel countryVM)
        {
            Country originalCountry = FindBy(id);

            if (originalCountry == null)
            {
                return null;
            }

            originalCountry.Name = countryVM.CreateVM.Name;
            originalCountry.Cities = countryVM.CreateVM.CityList;
            originalCountry = _countryRepo.Update(originalCountry);

            return originalCountry;
        }

        public bool Remove(int id)
        {
            Country removeCountry = _countryRepo.Read(id);
            if (removeCountry != null)
            {
                return _countryRepo.Delete(removeCountry);
            }
            return false;
        }
    }
}
