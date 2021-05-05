using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.Data
{
    public class Person
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(50)]
        public string PhoneNr { get; set; }
    }
}
