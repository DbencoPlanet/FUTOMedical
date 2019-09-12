using FUTOMedical.Areas.Data.IServices;
using FUTOMedical.Areas.Data.Services;
using FUTOMedical.Models;
using FUTOMedical.Models.Dtos;
using FUTOMedical.Models.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FUTOMedical.Areas.Nurses.Controllers
{
    public class UserManagerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private INurseService _nurseService = new NurseService();
        private IUserManagerService _userService = new UserManagerService();
        private ApplicationSignInManager _signInManager;


        public UserManagerController()
        {

        }
        public UserManagerController(UserManagerService userService, ApplicationSignInManager signInManager, ApplicationUserManager userManager, ApplicationRoleManager roleManager, NurseService nurseService)
        {
            _userService = userService;
            UserManager = userManager;
            _nurseService = nurseService;
            RoleManager = roleManager;
            SignInManager = signInManager;
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
        // GET: Doctors/UserManager
        public async Task<ActionResult> NewNurse()
        {
            ViewBag.StateOfOrigin = new SelectList(db.States.OrderBy(x => x.StateName), "StateName", "StateName");
            return View();
        }

        // POST: Admin/Sessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> NewNurse(NurseDto model, HttpPostedFileBase picture)
        {
            var ee = "";
            if (ModelState.IsValid)
            {



                try
                {
                    string succed;

                    succed = await _nurseService.NewNurse(model);
                    if (succed == "true")
                    {
                        var user = await UserManager.FindByNameAsync(model.Email);

                        //picture
                        if (picture != null && picture.ContentLength > 0)

                        {
                            System.Random randomInteger = new System.Random();
                            int genNumber = randomInteger.Next(1000);

                            if (picture.ContentLength > 0 && picture.ContentType.ToUpper().Contains("JPEG") || picture.ContentType.ToUpper().Contains("PNG") || picture.ContentType.ToUpper().Contains("JPG"))
                            {

                                string fileName = Path.GetFileName(model.Surname + "_" + model.Firstname + "_" + genNumber + "_" + picture.FileName);
                                model.Picture = "~/Uploads/Profile/" + fileName;
                                fileName = Path.Combine(Server.MapPath("~/Uploads/Profile/"), fileName);
                                picture.SaveAs(fileName);


                            }
                        }

                        //profile pic upload
                        var img = await db.Nurses.FirstOrDefaultAsync(x => x.UserId == user.Id);

                        db.Entry(img).State = EntityState.Modified;
                        await db.SaveChangesAsync();

                        TempData["success"] = "Nurse with username <i> " + model.Fullname + "</i> Added Successfully";
                        return RedirectToAction("NewNurse");
                    }
                    else
                    {
                        TempData["error1"] = succed;
                    }


                }
                catch (Exception e)
                {
                    ee = e.ToString();
                }

            }
            var allErrors = ModelState.Values.SelectMany(v => v.Errors);
            TempData["error"] = "Creation of new Nurse not successful" + ee;
            ViewBag.StateOfOrigin = new SelectList(db.States.OrderBy(x => x.StateName), "StateName", "StateName", model.StateOfOrigin);
            return View(model);
        }



        public async Task<ActionResult> EditNurse(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.StateName = new SelectList(db.States.OrderBy(x => x.StateName), "StateName", "StateName");
         
            var profile = await db.Nurses.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);

            Nurse data = await db.Nurses.FindAsync(id);

            if (data == null)
            {
                return HttpNotFound();
            }


            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditNurse(Nurse profile, HttpPostedFileBase picture, int? id)
        {

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


                ApplicationUser user = await UserManager.FindByIdAsync(profile.UserId);
                if (user != null)
                {
                    user.Firstname = profile.Firstname;
                    user.Surname = profile.Surname;
                    user.Othernames = profile.Othernames;
                    await UserManager.UpdateAsync(user);
                }


                db.Entry(profile).State = EntityState.Modified;
                await db.SaveChangesAsync();
                TempData["success"] = "Profile Edited Successfully.";
                return RedirectToAction("EditNurse", "UserManager");
            }
            TempData["error"] = "Unable to Edit Profile.";
            ViewBag.StateOfOrigin = new SelectList(db.States.OrderBy(x => x.StateName), "StateName", "StateName", profile.StateOfOrigin);
            return View(profile);
        }


        public async Task<ActionResult> MyProfile()
        {
            var userinfo = User.Identity.GetUserId();
            var profile = await db.Nurses.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == userinfo);
            ViewBag.profile = profile;
            return View();
        }


        public async Task<ActionResult> Profile(int? id)
        {
            var profile = await db.Nurses.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            ViewBag.profile = profile;
            return View();
        }

    }
}