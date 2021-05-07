using Microsoft.EntityFrameworkCore;
using MVC_Data.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.DataAccess
{
    public class DatabaseCountryRepo : ICountryRepo
    {
        readonly PeopleDbContext _dbContext;
        public DatabaseCountryRepo(PeopleDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Country Create(Country country)
        {
            _dbContext.Countries.Add(country);

            int saveResult = _dbContext.SaveChanges();

            if (saveResult == 0)
            {
                return null;
            }

            return country;
        }

        public List<Country> Read()
        {
            return _dbContext.Countries.ToList();
        }

        public Country Read(int id)
        {
            return _dbContext.Countries.SingleOrDefault(c => c.Id == id);
        }

        public Country Update(Country country)
        {
            if (country != null)
            {
                Country originalCountry = Read(country.Id);

                if (originalCountry == null)
                {
                    return null;
                }

                _dbContext.Countries.Update(country);

                int saveResult = _dbContext.SaveChanges();

                if (saveResult == 0)//no changes in the database
                {
                    return null;
                }

            }
            return country;
        }

        public bool Delete(Country country)
        {
            if (country == null)
            {
                return false;
            }
            else
            {
                Country originalCountry = Read(country.Id);
                if (originalCountry == null)
                {
                    return false;
                }

                _dbContext.Remove(originalCountry);
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
