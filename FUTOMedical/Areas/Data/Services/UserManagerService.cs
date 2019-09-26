using FUTOMedical.Areas.Data.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using FUTOMedical.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using FUTOMedical.Models.Entities;

namespace FUTOMedical.Areas.Data.Services
{
    public class UserManagerService : IUserManagerService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public UserManagerService()
        {
        }

        public UserManagerService(ApplicationUserManager userManager,
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



        

        public async Task<List<ApplicationUser>> AllUsers()
        {

            return (await UserManager.Users.Where(x => x.UserName != "super@admin.com").ToListAsync());

           
        }


        public async Task<List<Doctor>> Doctors()
        {

            var users = db.Doctors.Include(x => x.User).Where(x => x.User.UserName != "super@admin.com");

            return await users.OrderBy(x => x.Surname).ToListAsync();
        }


        public async Task<List<Nurse>> Nurses()
        {
            var users = db.Nurses.Include(x => x.User).Where(x => x.User.UserName != "super@admin.com");

            return await users.OrderBy(x => x.Surname).ToListAsync();
        }

        public async Task<List<Pharmacist>> Pharmacists()
        {
            var users = db.Pharmacist.Include(x => x.User).Where(x => x.User.UserName != "super@admin.com");

            return await users.OrderBy(x => x.Surname).ToListAsync();
        }

        public async Task<List<Patient>> Patients()
        {
            var users = db.Patients.Include(x => x.User).Where(x => x.User.UserName != "super@admin.com");

            return await users.OrderBy(x => x.Surname).ToListAsync();
        }


        public async Task<List<Accountants>> Accountants()
        {
            var users = db.Accountants.Include(x => x.User).Where(x => x.User.UserName != "super@admin.com");

            return await users.OrderBy(x => x.Surname).ToListAsync();
        }

        public async Task<List<Laboratorists>> Laboratorists()
        {
            var users = db.Laboratorists.Include(x => x.User).Where(x => x.User.UserName != "super@admin.com");

            return await users.OrderBy(x => x.Surname).ToListAsync();
        }




        public async Task<bool> IsUsersinRole(string userid, string role)
        {
            var users = await _userManager.IsInRoleAsync(userid, role);
            return users;
        }
        ///
        //public int CountString(string searchString)
        //{
        //    int result = 0;

        //    searchString = searchString.Trim();

        //    if (searchString == "")
        //        return 0;

        //    while (searchString.Contains("  "))
        //        searchString = searchString.Replace("  ", " ");

        //    foreach (string y in searchString.Split(' '))

        //        result++;


        //    return result;
        //}


        public async Task<List<ApplicationUser>> Users()
        {
            return (await UserManager.Users.ToListAsync());
        }

        public async Task AddUserToRole(string userId, string rolename)
        {
            await UserManager.AddToRoleAsync(userId, rolename);
        }
        public async Task RemoveUserToRole(string userId, string rolename)
        {
            await UserManager.RemoveFromRoleAsync(userId, rolename);
        }

      
    }
}