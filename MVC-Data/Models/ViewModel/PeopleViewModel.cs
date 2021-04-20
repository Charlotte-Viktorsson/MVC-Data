using MVC_Data.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.ViewModel
{
    public class PeopleViewModel
    {
        public PeopleViewModel()
        {
            createPersonViewModel = new CreatePersonViewModel();
            Persons = new List<Person>();
        }

        public List<Person> Persons { get; set; }

        [Display(Name = "Search filter")]
        public string SearchFilter { get; set; }

        public CreatePersonViewModel createPersonViewModel { get; set; }
    }
}
