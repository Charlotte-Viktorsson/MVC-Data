using MVC_Data.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.DataAccess
{
    public class DatabasePersonLanguageRepo : IPersonLanguageRepo
    {
        private readonly PeopleDbContext _context;

        public DatabasePersonLanguageRepo(PeopleDbContext context)
        {
            _context = context;
        }

        public PersonLanguage Create(PersonLanguage personLanguage)
        {
            _context.PersonLanguages.Add(personLanguage);

            if (_context.SaveChanges() > 0)
            {
                return personLanguage;
            }

            return null;
        }

        public PersonLanguage Read(int pId, int langId)
        {
            return _context.PersonLanguages.SingleOrDefault(pl => pl.PersonId == pId && pl.LanguageId == langId);
        }

        public List<PersonLanguage> Read()
        {
            return _context.PersonLanguages.ToList();
        }


        public bool Delete(int pId, int langId)
        {

            PersonLanguage personLanguage = Read(pId, langId);

            if (personLanguage == null)
            {
                return false;
            }

            _context.PersonLanguages.Remove(personLanguage);

            if (_context.SaveChanges() > 0)
            {
                return true;
            }

            return false;
        }
    }
}
