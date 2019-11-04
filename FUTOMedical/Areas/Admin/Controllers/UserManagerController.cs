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



        public ActionResult AddDoctor(int? userid)
        {
            ViewBag.id = userid;
            ViewBag.StateName = new SelectList(db.States.OrderBy(x => x.StateName), "StateName", "StateName");
            return View();
        }

        // POST: Staff/Portal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddDoctor(Doctor profile, HttpPostedFileBase picture, int? userid)
        {

            ViewBag.id = userid;
            profile.UserId = ViewBag.id;


            if (ModelState.IsValid)
            {
                //    //picture
                if (picture != null && picture.ContentLength > 0)

                {
                    System.Random randomInteger = new System.Random();
                    int genNumber = randomInteger.Next(1000);

                    if (picture.ContentLength > 0 && picture.ContentType.ToUpper().Contains("JPEG") || picture.ContentType.ToUpper().Contains("PNG") || picture.ContentType.ToUpper().Contains("JPG"))
                    {

                        string fileName = Path.GetFileName(profile.Surname + "_" + profile.Firstname + "_" + genNumber + "_" + picture.FileName);
                        profile.Picture = "~/Uploads/Profile/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Uploads/Profile/"), fileName);
                        picture.SaveAs(fileName);


                    }
                }




                db.Doctors.Add(profile);
                await db.SaveChangesAsync();
                return RedirectToAction("welcome", new { userid = profile.Id });
            }
            ViewBag.StateOfOrigin = new SelectList(db.States.OrderBy(x => x.StateName), "StateName", "StateName", profile.StateOfOrigin);
            return View(profile);
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


        public async Task<ActionResult> Nurses()
        {
            ViewBag.Roles = RoleManager.Roles.Where(x => x.Name != "SuperAdmin").ToList();

            var items = await _userService.Nurses();
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


        public async Task<ActionResult> Accountants()
        {
            ViewBag.Roles = RoleManager.Roles.Where(x => x.Name != "SuperAdmin").ToList();

            var items = await _userService.Accountants();
            var item2 = await _userService.AllUsers();
            ViewBag.male = items.Count();
            ViewBag.countall = item2.Count();

            return View(items);

        }


        public async Task<ActionResult> Laboratorists()
        {
            ViewBag.Roles = RoleManager.Roles.Where(x => x.Name != "SuperAdmin").ToList();

            var items = await _userService.Laboratorists();
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
                profile.MobileNo = "000000";

                db.Doctors.Add(profile);
                await db.SaveChangesAsync();



                // //Create User to roles
                var role = new IdentityRole("SuperAdmin");
                var role1 = new IdentityRole("Doctor");
                var role2 = new IdentityRole("Admin");
                var role3 = new IdentityRole("Patient");
                var role4 = new IdentityRole("Pharmacist");
                var role5 = new IdentityRole("Accountant");
                var role6 = new IdentityRole("Laboratorist");
              

                await RoleManager.CreateAsync(role);
                await RoleManager.CreateAsync(role1);
                await RoleManager.CreateAsync(role2);
                await RoleManager.CreateAsync(role3);
                await RoleManager.CreateAsync(role4);
                await RoleManager.CreateAsync(role5);
                await RoleManager.CreateAsync(role6);

                //Add User to SuperAdmin role
                await UserManager.AddToRoleAsync(user.Id, "SuperAdmin");

                //Add User to Admin role
                await UserManager.AddToRoleAsync(user.Id, "Admin");

                //Add User to Patient role
                await UserManager.AddToRoleAsync(user.Id, "Patient");
                //Add User to Doctor role
                await UserManager.AddToRoleAsync(user.Id, "Doctor");

                //Add User to Pharmacist role
                await UserManager.AddToRoleAsync(user.Id, "Pharmacist");

                //Add User to Laboratorist role
                await UserManager.AddToRoleAsync(user.Id, "Laboratorist");

                //Add User to Accountant role
                await UserManager.AddToRoleAsync(user.Id, "Accountant");


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
                profile.MobileNo = "000000";

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
            var stateId = db.States.FirstOrDefault(x => x.StateName == Id).Id;
            var local = from s in db.LocalGovs
                        where s.StatesId == stateId
                        select s;

            return Json(new SelectList(local.ToArray(), "StateName", "StateName"), JsonRequestBehavior.AllowGet);
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