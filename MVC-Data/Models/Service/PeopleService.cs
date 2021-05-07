using MVC_Data.Models.Data;
using MVC_Data.Models.ViewModel;
using MVC_Data.Models.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace MVC_Data.Models.Service
{
    public class PeopleService : IPeopleService
    {
        private IPeopleRepo _memory;

        public PeopleService(IPeopleRepo peopleRepo)
        {
            _memory = peopleRepo;
        }

        public Person Add(CreatePersonViewModel createPerson)
        {
            if (createPerson.CityId == -1) //no city
            {
                createPerson.CityId = 0;
            }
            Person createdPerson = _memory.Create(createPerson);
            return createdPerson;
        }

        public PeopleViewModel All()
        {
            PeopleViewModel model = new PeopleViewModel();
            model.Persons = _memory.Read();
            return model;
        }

        /*public Person Edit(int id, Person person)
        {
            //why do we need id, it should be included in Person
            return _memory.Update(person);
        }*/

        public Person Edit(int id, EditPersonViewModel person)
        {
            Person personToUpdate = FindBy(id);

            if (personToUpdate == null)
            {
                return null;
            }

            personToUpdate.Id = id;
            personToUpdate.FirstName = person.CreatePerson.FirstName;
            personToUpdate.LastName = person.CreatePerson.LastName;
            personToUpdate.InCityId = person.CreatePerson.CityId;
            personToUpdate.PhoneNr = person.CreatePerson.PhoneNr;

            return _memory.Update(personToUpdate);
        }

        public PeopleViewModel FindBy(PeopleViewModel search)
        {
            string filter = search.SearchFilter;
            //search = this.All();
            PeopleViewModel filteredModel = new PeopleViewModel();
            filteredModel = this.All();
            List<Person> filteredList = new List<Person>();
            if (String.IsNullOrWhiteSpace(filter))
            {
                return filteredModel; //will show whole list then
            }
            else
            {
                //filter
                foreach (Person person in filteredModel.Persons)
                {
                    if (search.CaseSensitive)
                    {
                        if (person.InCity.Name.Contains(filter, StringComparison.CurrentCulture) ||
                        person.FirstName.Contains(filter, StringComparison.CurrentCulture) ||
                        person.LastName.Contains(filter, StringComparison.CurrentCulture))
                        {
                            filteredList.Add(person);
                        }
                    }
                    else // case insensitive
                    {
                        if (person.InCity.Name.Contains(filter, StringComparison.CurrentCultureIgnoreCase) ||
                            person.FirstName.Contains(filter, StringComparison.CurrentCultureIgnoreCase) ||
                            person.LastName.Contains(filter, StringComparison.CurrentCultureIgnoreCase))
                        {
                            filteredList.Add(person);
                        }
                    }
                }
                search.Persons = filteredList;

                return search;
            }
        }

        //public List<Person> FindByCity(string cityName)
        public List<Person> FindByCity(int cityId)
        {

            PeopleViewModel filteredModel = new PeopleViewModel();
            filteredModel = this.All();
            List<Person> filteredList = new List<Person>();
            foreach (Person person in filteredModel.Persons)
            {

                if (person.InCityId == cityId)
                {
                    filteredList.Add(person);
                }

            }
            return filteredList;
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

        //public PeopleViewModel Sort(string sortOrder, string orderBy)
        //{
        //    PeopleViewModel sortModel = new PeopleViewModel();
        //    sortModel = this.All();
        //    //List<Order> SortedList = objListOrder.OrderBy(o => o.OrderDate).ToList();


        //    List<Person> sortedPersons;
        //    if (sortOrder == "A")
        //    {
        //        sortedPersons = sortModel.Persons.OrderBy(p => orderBy).ToList();
        //    }
        //    else
        //    {
        //        sortedPersons = sortModel.Persons.OrderByDescending(p => "p." + orderBy).ToList();
        //    }

        //    sortModel.Persons = sortedPersons;
        //    return sortModel;

        //}

        /*
        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByProperty,
                                 bool desc) 
       {
            string command = desc ? "OrderByDescending" : "OrderBy";
            var type = typeof(TEntity);
            var property = type.GetProperty(orderByProperty);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },
                                          source.Expression, Expression.Quote(orderByExpression));
            return source.Provider.CreateQuery<TEntity>(resultExpression);
       }
                */
    }
}
