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
    public class AccountantService : IAccountantService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public AccountantService()
        {
        }

        public AccountantService(ApplicationUserManager userManager,
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

        public async Task<string> NewAccountant(AccountantDto model)
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
                await UserManager.AddToRoleAsync(user.Id, "Accountant");
                Accountants accountant = new Accountants();
                accountant.UserId = user.Id;
                accountant.EmailAddress = model.Email;
                accountant.Surname = model.Surname;
                accountant.Firstname = model.Firstname;
                accountant.Othernames = model.Othernames;
                accountant.PhoneNo = model.PhoneNo;
                accountant.Sex = model.Sex;
                accountant.BloodGroup = model.BloodGroup;
                accountant.Education = model.Education;
                accountant.Address = model.Address;
                accountant.StateOfOrigin = model.StateOfOrigin;
                accountant.LocalGov = model.LocalGov;
                accountant.Picture = model.Picture;
                accountant.Nationality = model.Nationality;



                db.Accountants.Add(accountant);
                await db.SaveChangesAsync();

                var nurReg = await db.Accountants.FirstOrDefaultAsync(x => x.UserId == user.Id);
                string numberid = nurReg.Id.ToString("D3");
                nurReg.AccountantId = "ACCT/" + numberid;
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