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
    public class PatientService : IPatientService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public PatientService()
        {
        }

        public PatientService(ApplicationUserManager userManager,
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

        public async Task<string> NewPatient(PatientDto model)
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
                await UserManager.AddToRoleAsync(user.Id, "Patient");
                Patient patient = new Patient();
                patient.UserId = user.Id;
                patient.EmailAddress = model.Email;
                patient.Surname = model.Surname;
                patient.Firstname = model.Firstname;
                patient.Othernames = model.Othernames;
                patient.PhoneNo = model.PhoneNo;
                patient.Sex = model.Sex;
                patient.BloodGroup = model.BloodGroup;
                patient.Address = model.Address;
                patient.StateOfOrigin = model.StateOfOrigin;
                patient.LocalGov = model.LocalGov;
                //patient.ModeOfEntry = model.ModeOfEntry;
                //patient.ModeOfStudy = model.ModeOfStudy;
                patient.Nationality = model.Nationality;
                patient.DOB = model.DOB;
                //patient.JambReg = model.JambReg;
                //patient.MatricNo = model.MatricNo;
                //patient.SchoolId = model.SchoolId;
                //patient.StudentDeptId = model.StudentDeptId;
                patient.ParentGuardianName = model.ParentGuardianName;
                patient.ParentGuardianPhone = model.ParentGuardianPhone;
                patient.PermanentAddress = model.PermanentAddress;
                patient.NextOfKin = model.NextOfKin;
                patient.NextOfKinAddress = model.NextOfKinAddress;
                patient.NextOfKinPhone = model.NextOfKinPhone;
                //patient.DateOfEntry = model.DateOfEntry;
                patient.PlaceOfBirth = model.PlaceOfBirth;
                patient.Religion = model.Religion;
                patient.ResidentAddress = model.ResidentAddress;
                patient.Address = model.Address;
                patient.Department = model.Department;
                patient.Photo = model.Photo;



                db.Patients.Add(patient);
                await db.SaveChangesAsync();

                var patReg = await db.Patients.FirstOrDefaultAsync(x => x.UserId == user.Id);
                string numberid = patReg.Id.ToString("D3");
                patReg.CardNumber = DateTime.UtcNow.Year +"/"+numberid;
                patReg.FolderNumber = DateTime.UtcNow.Year +"/"+ numberid;

                db.Entry(patReg).State = EntityState.Modified;
                await db.SaveChangesAsync();

                Section section = new Section();
                section.OpenDate = DateTime.UtcNow.AddHours(1);
                section.PatientId = patReg.Id;
                section.SectionOpen = true;
                db.Sections.Add(section);
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