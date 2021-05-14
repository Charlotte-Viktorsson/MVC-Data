using MVC_Data.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.Service
{
    public interface IPersonLanguageService
    {
        public PersonLanguage Create(PersonLanguage personLanguage);

        public List<PersonLanguage> All();

        public PersonLanguage FindBy(int pId, int langId);

        public bool Remove(int pId, int langId);

    }
}
