using Microsoft.EntityFrameworkCore;
using MVC_Data.Models.Data;
using MVC_Data.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.DataAccess
{
    public class DatabasePeopleRepo : IPeopleRepo
    {
        readonly PeopleDbContext _peopleDbContext;
        public DatabasePeopleRepo(PeopleDbContext dbContext)
        {
            _peopleDbContext = dbContext;
        }

        public Person Create(CreatePersonViewModel createPerson)
        {

            Person newPerson = new Person
            {
                FirstName = createPerson.FirstName,
                LastName = createPerson.LastName,
                InCityId = createPerson.CityId,
                PhoneNr = createPerson.PhoneNr
            };

            _peopleDbContext.People.Add(newPerson);

            int saveResult = _peopleDbContext.SaveChanges();

            if (saveResult == 0)//no changes in the database
            {
                //throw new Exception("unable to create Person in database.");
                return null;
            }

            return newPerson;
        }


        public List<Person> Read()
        {
            return _peopleDbContext.People.Include("InCity").ToList();
        }

        public Person Read(int id)
        {
            return _peopleDbContext.People.Include("InCity").SingleOrDefault(p => p.Id == id);
        }

        public Person Update(Person person)
        {
            if (person != null)
            {
                Person originalPerson = Read(person.Id);

                if (originalPerson == null)
                {
                    return null;
                }

                _peopleDbContext.People.Update(person);

                int saveResult = _peopleDbContext.SaveChanges();

                if (saveResult == 0)//no changes in the database
                {
                    return null;
                }

            }
            return person;
        }

        public bool Delete(Person person)
        {
            if (person == null)
            {
                return false;
            }
            else
            {
                Person originalPerson = Read(person.Id);
                if (originalPerson == null)
                {
                    return false;
                }

                _peopleDbContext.Remove(originalPerson);
                int saveResult = _peopleDbContext.SaveChanges();

                if (saveResult == 0)//no changes in the database
                {
                    return false;
                }
                return true;
            }
        }

    }
}
