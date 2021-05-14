using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.Data
{
    public class Language
    {
        [Key]
        public int Id { get; set; }


        [Required]
        public string Name { get; set; }

        public List<PersonLanguage> PersonLanguages { get; set; }
    }
}
