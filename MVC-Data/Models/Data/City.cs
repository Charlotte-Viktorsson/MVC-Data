using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.Data
{
    public class City
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [MaxLength(100)]
        public string Name { get; set; }


        public List<Person> Citizens { get; set; }


        [Required]
        public Country Nation { get; set; }

    }
}
