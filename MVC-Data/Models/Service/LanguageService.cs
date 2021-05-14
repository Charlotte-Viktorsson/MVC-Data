using MVC_Data.Models.Data;
using MVC_Data.Models.DataAccess;
using MVC_Data.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.Service
{
    public class LanguageService : ILanguageService
    {
        private readonly ILanguageRepo _languageRepo;
        private readonly IPersonLanguageRepo _personLanguageRepo;

        public LanguageService(ILanguageRepo languageRepo, IPersonLanguageRepo personLanguageRepo)
        {
            _languageRepo = languageRepo;
            _personLanguageRepo = personLanguageRepo;
        }

        public Language Add(CreateLanguageViewModel createLanguage)
        {
            Language language = new Language();
            language.Name = createLanguage.Name;
            return _languageRepo.Create(language);
        }

        public List<Language> All()
        {
            return _languageRepo.Read();
        }

        public Language FindById(int id)
        {
            return _languageRepo.Read(id);
        }

        public List<Person> FindSpeakersByLanguageId(int id)
        {
            List<PersonLanguage> personLanguages = _personLanguageRepo.Read();
            List<Person> speakers = new List<Person>();
            foreach (var language in personLanguages)
            {
                if (language.LanguageId == id)
                {
                    speakers.Add(language.Person);
                }
            }
            return speakers;
        }

        public Language Edit(int id, EditLanguageViewModel languageVM)
        {

            Language originalLanguage = FindById(id);

            if (originalLanguage == null)
            {
                return null;
            }

            originalLanguage.Name = languageVM.CreateVM.Name;
            originalLanguage = _languageRepo.Update(originalLanguage);

            return originalLanguage;
        }

        public bool Remove(int id)
        {
            Language removeLanguage = _languageRepo.Read(id);
            if (removeLanguage != null)
            {
                return _languageRepo.Delete(removeLanguage);
            }
            return false;
        }

    }
}
