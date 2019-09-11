using FUTOMedical.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FUTOMedical.Models.Entities;
using System.Data;
using System.Data.Entity;

namespace FUTOMedical.Areas.Service
{
    public class GeneralService
    {
        public static bool IsUserInRole(string userId, string role)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            if (manager.IsInRole(userId, role))
            {
                return true;
            }

            return false;
        }

        public static Doctor DoctorInfo()
        {
            Doctor user;
            using (var db = new ApplicationDbContext())
            {
                var userid = HttpContext.Current.User.Identity.GetUserId();
                user = db.Doctors.Include(x => x.User).FirstOrDefault(x => x.UserId == userid);

            }
            return user;

        }

        public static Doctor DoctorInfo2()
        {
            Doctor user;
            using (var db = new ApplicationDbContext())
            {
                user = db.Doctors.Include(x => x.User).FirstOrDefault();

            }
            return user;

        }

        public static Nurse NurseInfo()
        {
            Nurse user;
            using (var db = new ApplicationDbContext())
            {
                var userid = HttpContext.Current.User.Identity.GetUserId();
                user = db.Nurses.Include(x => x.User).FirstOrDefault(x => x.UserId == userid);

            }
            return user;

        }

        public static Nurse NurseInfo2()
        {
            Nurse user;
            using (var db = new ApplicationDbContext())
            {
                user = db.Nurses.Include(x => x.User).FirstOrDefault();

            }
            return user;

        }

        public static Pharmacist PharmacistInfo()
        {
            Pharmacist user;
            using (var db = new ApplicationDbContext())
            {
                var userid = HttpContext.Current.User.Identity.GetUserId();
                user = db.Pharmacist.Include(x => x.User).FirstOrDefault(x => x.UserId == userid);

            }
            return user;

        }

        public static Pharmacist PharmacistInfo2()
        {
            Pharmacist user;
            using (var db = new ApplicationDbContext())
            {
                user = db.Pharmacist.Include(x => x.User).FirstOrDefault();

            }
            return user;

        }


        public static Patient PatientInfo()
        {
            Patient user;
            using (var db = new ApplicationDbContext())
            {
                var userid = HttpContext.Current.User.Identity.GetUserId();
                user = db.Patients.Include(x => x.User).FirstOrDefault(x => x.UserId == userid);

            }
            return user;

        }

        public static Patient PatientInfo2()
        {
            Patient user;
            using (var db = new ApplicationDbContext())
            {
                user = db.Patients.Include(x => x.User).FirstOrDefault();

            }
            return user;

        }


    }
}