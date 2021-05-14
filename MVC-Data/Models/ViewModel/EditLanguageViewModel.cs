using MVC_Data.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.ViewModel
{
    public class EditLanguageViewModel
    {
        public int Id { get; set; }

        public CreateLanguageViewModel CreateVM { get; set; }

        //? list of Person or PersonLanguage?
        public List<Person> Speakers { get; set; }


        public EditLanguageViewModel(int id, Language language)
        {
            Id = id;
            this.CreateVM = new CreateLanguageViewModel()
            {
                Name = language.Name,
            };
        }

        public EditLanguageViewModel()
        {

        }

    }
}
