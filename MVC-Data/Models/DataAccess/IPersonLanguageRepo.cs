using MVC_Data.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.DataAccess
{
    public interface IPersonLanguageRepo
    {
        PersonLanguage Create(PersonLanguage personLanguage);

        PersonLanguage Read(int pId, int langId);
        List<PersonLanguage> Read();

        bool Delete(int pId, int langId);

    }
}

