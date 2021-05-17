using MVC_Data.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.ViewModel
{
    public class ManagePersonLanguageViewModel
    {
        public Person Person { get; set; }

        public List<Language> NotSpokenLanguages { get; set; }
        public List<Language> AllLanguages { get; set; }

        public ManagePersonLanguageViewModel(Person speaker)
        {
            Person = speaker;
            NotSpokenLanguages = new List<Language>();
            AllLanguages = new List<Language>();
        }

        public ManagePersonLanguageViewModel()
        {
            NotSpokenLanguages = new List<Language>();
            AllLanguages = new List<Language>();
        }
    }
}
