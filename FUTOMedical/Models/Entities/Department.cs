using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FUTOMedical.Models.Entities
{
    public class Department
    {
        public int Id { get; set; }

        [Display(Name="Department Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

      
    }
}
