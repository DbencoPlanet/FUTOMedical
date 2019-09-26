using FUTOMedical.Areas.Data.IServices;
using FUTOMedical.Models;
using FUTOMedical.Models.Dtos;
using FUTOMedical.Models.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FUTOMedical.Areas.Data.Services
{
    public class DoctorService : IDoctorService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public DoctorService()
        {
        }

        public DoctorService(ApplicationUserManager userManager,
            ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public async Task<string> NewDoctor(DoctorDto model)
        {
          
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Surname = model.Surname,
                Email = model.Email,
                Firstname = model.Firstname,
                Othernames = model.Othernames
            };
            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await UserManager.AddToRoleAsync(user.Id, "Doctor");
                Doctor doctor = new Doctor();
                doctor.UserId = user.Id;
                doctor.EmailAddress = model.Email;
                doctor.Surname = model.Surname;
                doctor.Firstname = model.Firstname;
                doctor.Othernames = model.Othernames;
                doctor.DeparmentId = model.DeptId;
                doctor.PhoneNo = model.PhoneNo;
                doctor.MobileNo = model.MobileNo;
                doctor.Biography = model.Biography;
                doctor.BloodGroup = model.BloodGroup;
                doctor.Education = model.Education;
                doctor.Specialist = model.Specialist;
                doctor.Address = model.Address;
                doctor.StateOfOrigin = model.StateOfOrigin;
                doctor.LocalGov = model.LocalGov;
                doctor.Nationality = model.Nationality;
                doctor.Sex = model.Sex;
                doctor.Designation = model.Designation;
                doctor.Picture = model.Picture;

                
                db.Doctors.Add(doctor);
                await db.SaveChangesAsync();

                var docReg = await db.Doctors.FirstOrDefaultAsync(x => x.UserId == user.Id);
                string numberid = docReg.Id.ToString("D3");
                //docReg.StaffRegistrationId = setting.SchoolInitials + "/STAFF/" + numberid;
                docReg.DoctorId = "DOC/" + numberid;
                db.Entry(docReg).State = EntityState.Modified;
                await db.SaveChangesAsync();

                //email verifiation



                return "true";
            }
            var errors = result.Errors;
            var message = string.Join(", ", errors);

            return message;
        }
    }
}