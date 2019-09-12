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
    public class NurseService : INurseService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public NurseService()
        {
        }

        public NurseService(ApplicationUserManager userManager,
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

        public async Task<string> NewNurse(NurseDto model)
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
                await UserManager.AddToRoleAsync(user.Id, "Nurse");
                Nurse nurse = new Nurse();
                nurse.UserId = user.Id;
                nurse.EmailAddress = model.Email;
                nurse.Surname = model.Surname;
                nurse.Firstname = model.Firstname;
                nurse.Othernames = model.Othernames;
                nurse.PhoneNo = model.PhoneNo;
                nurse.Sex = model.Sex;
                nurse.BloodGroup = model.BloodGroup;
                nurse.Education = model.Education;
                nurse.Address = model.Address;
                nurse.StateOfOrigin = model.StateOfOrigin;
                nurse.LocalGov = model.LocalGov;



                db.Nurses.Add(nurse);
                await db.SaveChangesAsync();

                var nurReg = await db.Nurses.FirstOrDefaultAsync(x => x.UserId == user.Id);
                string numberid = nurReg.Id.ToString("D3");
                //docReg.StaffRegistrationId = setting.SchoolInitials + "/STAFF/" + numberid;
                nurReg.NurseId = "NUR/" + numberid;
                db.Entry(nurReg).State = EntityState.Modified;
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