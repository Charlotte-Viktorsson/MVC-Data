using MVC_Data.Models.Data;
using MVC_Data.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.DataAccess
{
    public interface IPeopleRepo
    {

        public Person Create(CreatePersonViewModel createPerson);

        public List<Person> Read();

        public Person Read(int id);

        public Person Update(Person person);

        public bool Delete(Person person);
    }
}
