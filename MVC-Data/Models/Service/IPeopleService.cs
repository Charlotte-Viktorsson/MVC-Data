using Microsoft.AspNetCore.DataProtection.KeyManagement;
using MVC_Data.Models.Data;
using MVC_Data.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.Service
{
    public interface IPeopleService
    {
        public Person Add(CreatePersonViewModel createPerson);

        public PeopleViewModel All();

        public PeopleViewModel FindBy(PeopleViewModel search);

        public List<Person> FindByCity(int cityId);

        public Person FindBy(int id);

        //public Person Edit(int id, Person person);

        public Person Edit(int id, EditPersonViewModel person);

        public bool Remove(int id);

        //public PeopleViewModel Sort(string sortOrder, string orderBy);
    }
}
