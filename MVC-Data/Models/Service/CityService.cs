using MVC_Data.Models.Data;
using MVC_Data.Models.DataAccess;
using MVC_Data.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.Service
{
    public class CityService : ICityService
    {
        private readonly ICityRepo _cityRepo;
        private readonly ICountryRepo _countryRepo;

        public CityService(ICityRepo cityRepo, ICountryRepo countryRepo)
        {
            _cityRepo = cityRepo;
            _countryRepo = countryRepo;
        }
        public City Add(CreateCityViewModel createCity)
        {
            //create city
            City city = new City();
            city.Name = createCity.Name;
            city.Nation = _countryRepo.Read(createCity.NationId);
            city = _cityRepo.Create(city);

            //update Country with city  --should not be needed
            //Country countryToUpdate = _countryRepo.Read(city.Nation.Id);
            //if (countryToUpdate != null)
            //{
            //    _countryRepo.Read(createCity.NationId).Cities.Add(city);
            //}
            return city;
        }

        public CityViewModel All()
        {
            CityViewModel cityViewModel = new CityViewModel();
            cityViewModel.Cities = _cityRepo.Read();
            return cityViewModel;
        }

        public City FindBy(int id)
        {
            City foundCity = _cityRepo.Read(id);
            return foundCity;
        }

        public List<City> FindCitiesByNation(int nationId)
        {
            List<City> citiesList = new List<City>();

            foreach (City city in _cityRepo.Read())
            {
                if (city.Nation.Id.Equals(nationId))
                {
                    citiesList.Add(city);
                }
            }

            return citiesList;
        }

        public City Edit(int id, EditCityViewModel editCity)
        {
            City originalCity = FindBy(id);

            if (originalCity == null)
            {
                return null;
            }

            originalCity.Name = editCity.CreateVM.Name;
            originalCity.Nation = _countryRepo.Read(editCity.CreateVM.NationId);

            originalCity = _cityRepo.Update(originalCity);

            return originalCity;
        }

        public bool Remove(int id)
        {
            City removeCity = _cityRepo.Read(id);
            if (removeCity != null)
            {
                return _cityRepo.Delete(removeCity);
            }
            return false;
        }

    }
}
