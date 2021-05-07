using MVC_Data.Models.Data;
using MVC_Data.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.DataAccess
{
    public class InMemoryPeopleRepo : IPeopleRepo
    {
        private List<Person> persons = new List<Person>();
        private int idCounter = 0;

        public Person Create(CreatePersonViewModel createPerson)
        {
            Person newPerson = new Person
            {
                Id = ++idCounter,
                FirstName = createPerson.FirstName,
                LastName = createPerson.LastName,
                InCityId = createPerson.CityId,
                PhoneNr = createPerson.PhoneNr
            };
            persons.Add(newPerson);

            return newPerson;
        }

        public bool Delete(Person person)
        {
            try
            {
                persons.Remove(person);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Person> Read()
        {
            return persons;
        }

        public Person Read(int id)
        {
            Person foundPerson = null;
            try
            {
                foundPerson = persons.Find(x => x.Id == id);
            }
            catch (Exception)
            {
                foundPerson = null;
            }

            return foundPerson;
        }

        //only tested with an "existing" person
        public Person Update(Person person)
        {
            Person personToUpdate = persons.FirstOrDefault(x => x.Id == person.Id);
            if (personToUpdate != null)
            {
                personToUpdate.FirstName = person.FirstName;
                personToUpdate.LastName = person.LastName;
                personToUpdate.InCityId = person.InCityId;
                personToUpdate.PhoneNr = person.PhoneNr;
            }
            return personToUpdate;
        }
    }
}
