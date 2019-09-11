using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FUTOMedical.Models.Entities
{
    public class School
    {
        public int? Id { get; set; }

        [Display(Name = "School Name")]
        public string Name { get; set; }

        [Display(Name = "School Code")]
        public string ShortCode { get; set; }


        public virtual ICollection<StudentDept> StudentDept { get; set; }


        public static List<School> GetSchools()
        {
            var db = new ApplicationDbContext();
            return db.Schools.ToList();

        }
    }
}