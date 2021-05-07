using MVC_Data.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.DataAccess
{
    public interface ICityRepo
    {
        public City Create(City city);

        public List<City> Read();

        public City Read(int id);

        public City Update(City city);

        public bool Delete(City city);
    }
}
