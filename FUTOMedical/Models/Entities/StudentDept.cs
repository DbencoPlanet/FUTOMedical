using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FUTOMedical.Models.Entities
{
    public class StudentDept
    {
        public int Id { get; set; }

        [Display(Name = "Dept Name")]
        public string Name { get; set; }

        [Display(Name = "Dept Code")]
        public string ShortCode { get; set; }



        [Display(Name = "Image")]
        public string Image { get; set; }


        public int? SchoolId { get; set; }
        public School School { get; set; }

     

        public static List<StudentDept> GetDept()
        {
            var db = new ApplicationDbContext();
            return db.StudentDepts.ToList();

        }
    }
}