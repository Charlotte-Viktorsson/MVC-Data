using MVC_Data.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.ViewModel
{
    public class EditCityViewModel
    {
        public int Id { get; set; }

        public CreateCityViewModel CreateVM { get; set; }

    }
}
