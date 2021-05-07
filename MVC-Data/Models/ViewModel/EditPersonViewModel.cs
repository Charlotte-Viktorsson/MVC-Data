using MVC_Data.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.ViewModel
{
    public class EditPersonViewModel
    {
        public int Id { get; set; }

        public CreatePersonViewModel CreatePerson { get; set; }

        public EditPersonViewModel(int id, Person person)
        {
            Id = id;
            this.CreatePerson = new CreatePersonViewModel()
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                CityId = person.InCityId,
                PhoneNr = person.PhoneNr
            };

        }

        public EditPersonViewModel(int id, CreatePersonViewModel createPerson)
        {
            Id = id;
            this.CreatePerson = createPerson;
        }

        public EditPersonViewModel()
        {

        }
    }
}
