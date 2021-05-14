using Microsoft.EntityFrameworkCore;
using MVC_Data.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.DataAccess
{
    public class DatabaseCityRepo : ICityRepo
    {
        readonly PeopleDbContext _dbContext;
        public DatabaseCityRepo(PeopleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public City Create(City city)
        {
            _dbContext.Cities.Add(city);

            int saveResult = _dbContext.SaveChanges();

            if (saveResult == 0)
            {
                return null;
            }

            return city;
        }

        public List<City> Read()
        {
            return _dbContext.Cities.Include(c => c.Nation).ToList();
        }

        public City Read(int id)
        {
            return _dbContext.Cities.Include(c => c.Nation).SingleOrDefault(c => c.Id == id);
        }

        public City Update(City city)
        {
            if (city != null)
            {
                City originalCity = Read(city.Id);

                if (originalCity == null)
                {
                    return null;
                }

                _dbContext.Cities.Update(city);

                int saveResult = _dbContext.SaveChanges();

                if (saveResult == 0)//no changes in the database
                {
                    return null;
                }

            }
            return city;
        }

        public bool Delete(City city)
        {
            if (city == null)
            {
                return false;
            }
            else
            {
                City originalCity = Read(city.Id);
                if (originalCity == null)
                {
                    return false;
                }

                _dbContext.Remove(originalCity);
                int saveResult = _dbContext.SaveChanges();

                if (saveResult == 0)//no changes in the database
                {
                    return false;
                }
                return true;
            }
        }
    }
}
