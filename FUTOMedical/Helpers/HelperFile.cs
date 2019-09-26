using FUTOMedical.Models;
using FUTOMedical.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FUTOMedical.Helpers
{
    public class HelperFile
    {
        public static string GetFullName(string name)
        {
            var Name = "User";
            using (var db = new ApplicationDbContext())
            {
                var user = db.Patients.FirstOrDefault(x => x.FolderNumber == name);
                if (user != null)
                {
                    Name = user.Fullname;
                }
            }

            return Name;
        }

        public static string GetName(int id)
        {
            var Name = "User";
            using (var db = new ApplicationDbContext())
            {
                var user = db.Patients.FirstOrDefault(x => x.Id == id);
                if (user != null)
                {
                    Name = user.Fullname;
                }
            }

            return Name;
        }


        public static bool GetSectionStatus(int? Id)
        {
            bool start = false;
            using (var db = new ApplicationDbContext())
            {
                var st = db.Sections.OrderByDescending(x => x.PatientId).FirstOrDefault(x => x.PatientId == Id).SectionClose;
                start = st;
                return start;
            }
        }
    }
       


   
}