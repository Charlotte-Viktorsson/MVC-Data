using MVC_Data.Models.Data;
using MVC_Data.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.Service
{
    public interface ILanguageService
    {
        Language Add(CreateLanguageViewModel language);

        List<Language> All();

        Language FindById(int id);//Find Language by its Id

        List<Person> FindSpeakersByLanguageId(int id);//Find speakers by language Id

        Language Edit(int id, EditLanguageViewModel language);

        bool Remove(int id);
    }
}
