using MVC_Data.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.ViewModel
{
    public class CreateLanguageViewModel
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public CreateLanguageViewModel() //Model binding in controller needs a Zero constructor to be present
        {

        }

    }


}
