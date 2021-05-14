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

        public List<Person> Speakers { get; set; }


        public EditLanguageViewModel(int id, Language language)
        {
            Id = id;
            this.CreateVM = new CreateLanguageViewModel()
            {
                Name = language.Name,
            };
            Speakers = new List<Person>();
        }

        public EditLanguageViewModel()
        {
            Speakers = new List<Person>();
        }

    }
}
