using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Travel.Models
{
    [Table("People")]
    public class Person
    {
        [key]
        public int PersonId { get; set; }
        public int ExperienceId { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
    }
}
