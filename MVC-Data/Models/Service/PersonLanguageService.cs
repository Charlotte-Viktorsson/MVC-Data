using MVC_Data.Models.Data;
using MVC_Data.Models.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.Service
{
    public class PersonLanguageService : IPersonLanguageService
    {
        private readonly IPersonLanguageRepo _personLanguageRepo;

        public PersonLanguageService(IPersonLanguageRepo personLanguageRepo)
        {
            _personLanguageRepo = personLanguageRepo;
        }

        public List<PersonLanguage> All()
        {
            return _personLanguageRepo.Read();
        }

        public PersonLanguage Create(PersonLanguage personLanguage)
        {
            return _personLanguageRepo.Create(personLanguage);
        }

        public PersonLanguage FindBy(int pId, int langId)
        {
            return _personLanguageRepo.Read(pId, langId);
        }

        public bool Remove(int pId, int langId)
        {
            return _personLanguageRepo.Delete(pId, langId);
        }
    }
}
