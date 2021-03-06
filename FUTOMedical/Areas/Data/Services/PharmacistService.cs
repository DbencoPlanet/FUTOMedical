﻿using FUTOMedical.Areas.Data.IServices;
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
    public class PharmacistService : IPharmacistService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public PharmacistService()
        {
        }

        public PharmacistService(ApplicationUserManager userManager,
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

        public async Task<string> NewPharmacist(PharmacistDto model)
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
                await UserManager.AddToRoleAsync(user.Id, "Pharmacist");
                Pharmacist pharmacist = new Pharmacist();
                pharmacist.UserId = user.Id;
                pharmacist.EmailAddress = model.Email;
                pharmacist.Surname = model.Surname;
                pharmacist.Firstname = model.Firstname;
                pharmacist.Othernames = model.Othernames;
                pharmacist.PhoneNo = model.PhoneNo;
                pharmacist.Sex = model.Sex;
                pharmacist.BloodGroup = model.BloodGroup;
                pharmacist.Education = model.Education;
                pharmacist.Address = model.Address;
                pharmacist.StateOfOrigin = model.StateOfOrigin;
                pharmacist.LocalGov = model.LocalGov;
                pharmacist.Picture = model.Picture;
                pharmacist.Nationality = model.Nationality;



                db.Pharmacist.Add(pharmacist);
                await db.SaveChangesAsync();

                var nurReg = await db.Pharmacist.FirstOrDefaultAsync(x => x.UserId == user.Id);
                string numberid = nurReg.Id.ToString("D3");
                //docReg.StaffRegistrationId = setting.SchoolInitials + "/STAFF/" + numberid;
                nurReg.PharmacistId = "PHAR/" + numberid;
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