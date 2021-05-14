using MVC_Data.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.ViewModel
{
    public class DeleteLanguageViewModel
    {
        public Language Language { get; set; }

        public List<Person> Speakers { get; set; }

    }
}
