using MVC_Data.Models.Data;
using MVC_Data.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.Service
{
    public interface ICityService
    {
        public City Add(CreateCityViewModel createCity);

        public CityViewModel All();

        public City FindBy(int id);
        public List<City> FindCitiesByNation(int id);

        public City Edit(int id, EditCityViewModel city);

        public bool Remove(int id);
    }
}
