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
    public class LaboratoristService : ILaboratoristService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public LaboratoristService()
        {
        }

        public LaboratoristService(ApplicationUserManager userManager,
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

        public async Task<string> NewLaboratorist(LaboratoristDto model)
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
                await UserManager.AddToRoleAsync(user.Id, "Laboratorist");
                Laboratorists laboratorist = new Laboratorists();
                laboratorist.UserId = user.Id;
                laboratorist.EmailAddress = model.Email;
                laboratorist.Surname = model.Surname;
                laboratorist.Firstname = model.Firstname;
                laboratorist.Othernames = model.Othernames;
                laboratorist.PhoneNo = model.PhoneNo;
                laboratorist.Sex = model.Sex;
                laboratorist.BloodGroup = model.BloodGroup;
                laboratorist.Education = model.Education;
                laboratorist.Address = model.Address;
                laboratorist.StateOfOrigin = model.StateOfOrigin;
                laboratorist.LocalGov = model.LocalGov;
                laboratorist.Picture = model.Picture;
                laboratorist.Nationality = model.Nationality;



                db.Laboratorists.Add(laboratorist);
                await db.SaveChangesAsync();

                var nurReg = await db.Laboratorists.FirstOrDefaultAsync(x => x.UserId == user.Id);
                string numberid = nurReg.Id.ToString("D3");
                nurReg.LaboratoristId = "LAB/" + numberid;
                db.Entry(nurReg).State = EntityState.Modified;
                await db.SaveChangesAsync();

              
                return "true";
            }
            var errors = result.Errors;
            var message = string.Join(", ", errors);

            return message;
        }
    }
}