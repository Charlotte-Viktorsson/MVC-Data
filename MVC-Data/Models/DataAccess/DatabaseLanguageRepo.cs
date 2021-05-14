using Microsoft.EntityFrameworkCore;
using MVC_Data.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.DataAccess
{
    public class DatabaseLanguageRepo : ILanguageRepo
    {
        private readonly PeopleDbContext _peopleDbContext;

        public DatabaseLanguageRepo(PeopleDbContext peopleDbContext)
        {
            _peopleDbContext = peopleDbContext;
        }

        public Language Create(Language language)
        {
            _peopleDbContext.Add(language);

            if (_peopleDbContext.SaveChanges() > 0)
            {
                return language;
            }

            return null;
        }

        public List<Language> Read()
        {
            return _peopleDbContext.Languages.ToList();
        }

        public Language Read(int id)
        {
            return _peopleDbContext.Languages
                .Include(l => l.PersonLanguages)
                    .ThenInclude(pl => pl.Person)
                .SingleOrDefault(l => l.Id == id);
        }

        public Language Update(Language language)
        {
            if (language != null)
            {
                Language originalLanguage = Read(language.Id);

                if (originalLanguage == null)
                {
                    return null;
                }

                _peopleDbContext.Languages.Update(language);

                int saveResult = _peopleDbContext.SaveChanges();

                if (saveResult == 0)//no changes in the database
                {
                    return null;
                }

            }
            return language;
        }

        public bool Delete(Language language)
        {
            if (language == null)
            {
                return false;
            }
            else
            {
                Language originalLanguage = Read(language.Id);
                if (originalLanguage == null)
                {
                    return false;
                }

                _peopleDbContext.Remove(originalLanguage);
                int saveResult = _peopleDbContext.SaveChanges();

                if (saveResult == 0)//no changes in the database
                {
                    return false;
                }
                return true;
            }
        }
    }
}
