using MVC_Data.Models.Data;
using MVC_Data.Models.ViewModel;
using MVC_Data.Models.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.Service
{
    public class PeopleService : IPeopleService
    {
        private IPeopleRepo _memory;

        public PeopleService()
        {
            _memory = new InMemoryPeopleRepo();
        }

        public Person Add(CreatePersonViewModel createPerson)
        {
            Person createdPerson = _memory.Create(createPerson);
            return createdPerson;
        }

        public PeopleViewModel All()
        {
            PeopleViewModel model = new PeopleViewModel();
            model.Persons = _memory.Read();
            return model;
        }

        public Person Edit(int id, Person person)
        {
            //why do we need id, it should be included in Person
            return _memory.Update(person);
        }

        public PeopleViewModel FindBy(PeopleViewModel search)
        {
            string filter = search.SearchFilter;
            search = this.All();
            List<Person> filteredList = new List<Person>();
            if (String.IsNullOrWhiteSpace(filter))
            {
                return search; //will show whole list then
            }
            else
            {
                //filter
                foreach (Person person in search.Persons)
                {
                    if (person.City.Contains(filter) ||
                        person.FirstName.Contains(filter) ||
                        person.LastName.Contains(filter)
                    )
                    {
                        filteredList.Add(person);
                    }

                }
                search.Persons = filteredList;

                return search;
            }
        }

        public Person FindBy(int id)
        {
            return _memory.Read(id);
        }

        public bool Remove(int id)
        {
            bool removeResult;
            Person deletePerson = this.FindBy(id);
            if (deletePerson != null)
            {
                removeResult = _memory.Delete(deletePerson);
            }
            else //couldn't find person
            {
                removeResult = false;
            }
            return removeResult;
        }
    }
}
