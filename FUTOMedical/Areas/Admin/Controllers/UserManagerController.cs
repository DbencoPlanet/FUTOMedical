using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using FUTOMedical.Areas.Data.IServices;
using FUTOMedical.Areas.Data.Services;
using FUTOMedical.Models;
using FUTOMedical.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using System.IO;

namespace FUTOMedical.Areas.Admin.Controllers
{

    [Authorize(Roles = "Admin,SuperAdmin")]
    public class UserManagerController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private IUserManagerService _userService = new UserManagerService();



        public UserManagerController()
        {

        }
        public UserManagerController(UserManagerService userService, ApplicationUserManager userManager, ApplicationRoleManager roleManager, ApplicationSignInManager signInManager)
        {
            _userService = userService;
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;

        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
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
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }


        // GET: Admin/UserManagers
     
        public async Task<ActionResult> Index()
        {
            ViewBag.Roles = RoleManager.Roles.Where(x => x.Name != "SuperAdmin").ToList();

            var items = await _userService.AllUsers();
            ViewBag.countall = items.Count();


            return View(items);

        }


        // GET: Admin/UserManagers
     
        public async Task<ActionResult> Doctors()
        {
            ViewBag.Roles = RoleManager.Roles.Where(x => x.Name != "SuperAdmin").ToList();

            var items = await _userService.Doctors();
            var item2 = await _userService.AllUsers();
            ViewBag.male = items.Count();
            ViewBag.countall = item2.Count();

            return View(items);

        }


        public async Task<ActionResult> Pharmacists()
        {
            ViewBag.Roles = RoleManager.Roles.Where(x => x.Name != "SuperAdmin").ToList();

            var items = await _userService.Pharmacists();
            var item2 = await _userService.AllUsers();
            ViewBag.male = items.Count();
            ViewBag.countall = item2.Count();

            return View(items);

        }


        public async Task<ActionResult> Patients()
        {
            ViewBag.Roles = RoleManager.Roles.Where(x => x.Name != "SuperAdmin").ToList();

            var items = await _userService.Patients();
            var item2 = await _userService.AllUsers();
            ViewBag.male = items.Count();
            ViewBag.countall = item2.Count();

            return View(items);

        }


      
        public async Task<ActionResult> MyProfile()
        {
            var userinfo = User.Identity.GetUserId();
            var profile = await db.Doctors.Include(x => x.User).Include(x => x.Department).FirstOrDefaultAsync(x => x.UserId == userinfo);
            ViewBag.profile = profile;
            return View();
        }



        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult CreateAccount()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAccount(RegisterViewModel model)
        {
            model.Email = "super@admin.com";
            model.Surname = "Super";
            model.Firstname = "Admin";
            model.Othernames = "Admin";
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email,Surname = model.Surname,Firstname = model.Firstname,Othernames = model.Othernames};
            model.Password = "super@123";




            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                Doctor profile = new Doctor();
                profile.UserId = user.Id;
                profile.EmailAddress = user.Email;
                profile.Surname = model.Surname;
                profile.Firstname = model.Firstname;
                profile.Othernames = model.Othernames;

                db.Doctors.Add(profile);
                await db.SaveChangesAsync();



                var role = new IdentityRole("SuperAdmin");
                var role1 = new IdentityRole("Doctor");
                var role2 = new IdentityRole("Admin");
                var role3 = new IdentityRole("Patient");
                var role4 = new IdentityRole("Pharmacist");

                await RoleManager.CreateAsync(role);
                await RoleManager.CreateAsync(role1);
                await RoleManager.CreateAsync(role2);
                await RoleManager.CreateAsync(role3);
                await RoleManager.CreateAsync(role4);







                await UserManager.AddToRoleAsync(user.Id, "SuperAdmin");

                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                return RedirectToAction("CreateAdmin");
            }
            TempData["error"] = "Contact Administrator";
            return View();

        }


        public async Task<ActionResult> CreateAdmin(RegisterViewModel model)
        {
            model.Email = "admin@admin.com";
            model.Surname = "Admin";
            model.Firstname = "Admin";
            model.Othernames = "Admin";
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Surname = model.Surname, Firstname = model.Firstname, Othernames = model.Othernames };
            model.Password = "Admin@123";



            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {

                Doctor profile = new Doctor();
                profile.UserId = user.Id;
                profile.EmailAddress = user.Email;
                profile.Surname = model.Surname;
                profile.Firstname = model.Firstname;
                profile.Othernames = model.Othernames;

                db.Doctors.Add(profile);
                await db.SaveChangesAsync();

                await UserManager.AddToRoleAsync(user.Id, "Admin");
             

                return RedirectToAction("Index", "DashBoard", new { area = "Admin" });
            }
            return View();

        }



        [HttpPost]
        public async Task<ActionResult> UserToRole(string rolename, string userId, bool? ischecked)
        {
            if (ischecked.HasValue && ischecked.Value)
            {
                await _userService.AddUserToRole(userId, rolename);
            }
            else
            {
                await _userService.RemoveUserToRole(userId, rolename);
            }

            return RedirectToAction("Index");
        }


        public JsonResult LgaList(string Id)
        {
            var stateId = db.State.FirstOrDefault(x => x.Name == Id).Id;
            var local = from s in db.LGA
                        where s.StatesId == stateId
                        select s;

            return Json(new SelectList(local.ToArray(), "Name", "Name"), JsonRequestBehavior.AllowGet);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}