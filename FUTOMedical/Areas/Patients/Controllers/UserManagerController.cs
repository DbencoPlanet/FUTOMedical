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

namespace FUTOMedical.Areas.Patients.Controllers
{
    public class UserManagerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private IPatientService _patientService = new PatientService();
        private IUserManagerService _userService = new UserManagerService();
        private ApplicationSignInManager _signInManager;


        public UserManagerController()
        {

        }
        public UserManagerController(UserManagerService userService, ApplicationSignInManager signInManager, ApplicationUserManager userManager, ApplicationRoleManager roleManager, PatientService patientService)
        {
            _userService = userService;
            UserManager = userManager;
            _patientService = patientService;
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

        // GET: Patients
        public async Task<ActionResult> Index()
        {
            return View(await db.Patients.ToListAsync());
        }
        public ActionResult Patient()
        {
            return View();
        }
        public ActionResult SearchPatient()
        {
            return View();
        }
        public ActionResult SearchPatientResult(string currentFilter, string searchString)
        {
            ViewBag.search = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {

            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = currentFilter;
            var patient = db.Patients.FirstOrDefault(x => x.FolderNumber.ToUpper().Contains(searchString.ToUpper()));

            if (String.IsNullOrEmpty(searchString))
            {
                ViewBag.SearchString = "Empty";
            }
            else if (!String.IsNullOrEmpty(searchString))
            {
                //  patient = patient.FirstOrDefault(x => x.FolderNumber.ToUpper().Contains(searchString.ToUpper()));
            }
            else if (patient == null)
            {
                TempData["error"] = "Folder not found.";
            }
            return View(patient);
        }

      

        // GET: Patients/UserManager
        public async Task<ActionResult> NewPatient()
        {
            ViewBag.StateOfOrigin = new SelectList(db.States.OrderBy(x => x.StateName), "StateName", "StateName");
            //ViewBag.SchoolId = new SelectList(db.Schools.OrderBy(x => x.Name), "Id", "Name");
            return View();
        }

        // POST: Patients/UserManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> NewPatient(PatientDto model, HttpPostedFileBase picture)
        {
            var ee = "";
            if (ModelState.IsValid)
            {



                try
                {
                    string succed;

                    succed = await _patientService.NewPatient(model);
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
                                model.Photo = "~/Uploads/Profile/" + fileName;
                                fileName = Path.Combine(Server.MapPath("~/Uploads/Profile/"), fileName);
                                picture.SaveAs(fileName);


                            }
                        }

                        //profile pic upload
                        var img = await db.Patients.FirstOrDefaultAsync(x => x.UserId == user.Id);
                        img.Photo = model.Photo;

                        db.Entry(img).State = EntityState.Modified;
                        await db.SaveChangesAsync();

                        TempData["success"] = "Patient with username <i> " + model.Fullname + "</i> Added Successfully";
                        return RedirectToAction("NewPatient");
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
            TempData["error"] = "Creation of new Patient not successful" + ee;
            ViewBag.StateOfOrigin = new SelectList(db.States.OrderBy(x => x.StateName), "StateName", "StateName", model.StateOfOrigin);
            //ViewBag.SchoolId = new SelectList(db.Schools.OrderBy(x => x.Name), "Id", "Name",model.SchoolId);
            return View(model);
        }


        public async Task<ActionResult> EditPatient(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.StateName = new SelectList(db.States.OrderBy(x => x.StateName), "StateName", "StateName");
            //ViewBag.SchoolId = new SelectList(db.Schools.OrderBy(x => x.Name), "Id", "Name");

            var profile = await db.Patients.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
          
            Patient data = await db.Patients.FindAsync(id);

            if (data == null)
            {
                return HttpNotFound();
            }


            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPatient(Patient profile, HttpPostedFileBase picture, int? id)
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
                        profile.Photo = "~/Uploads/Profile/" + fileName;
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
                return RedirectToAction("EditPatient", "UserManager");
            }
            TempData["error"] = "Unable to Edit Profile.";
            ViewBag.StateOfOrigin = new SelectList(db.States.OrderBy(x => x.StateName), "StateName", "StateName", profile.StateOfOrigin);
            //ViewBag.SchoolId = new SelectList(db.Schools.OrderBy(x => x.Name), "Id", "Name", profile.SchoolId);
            return View(profile);
        }


        public async Task<ActionResult> MyProfile()
        {
            var userinfo = User.Identity.GetUserId();
            var profile = await db.Patients.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == userinfo);
            ViewBag.profile = profile;
            return View(profile);
        }


        public async Task<ActionResult> profile(int? id)
        {
            var profile = await db.Patients.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            ViewBag.profile = profile;
            return View(profile);
        }


        // GET: Patients/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = await db.Patients.FindAsync(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Patient patient = await db.Patients.FindAsync(id);
            db.Patients.Remove(patient);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public JsonResult LgaList(string Id)
        {
            var stateId = db.States.FirstOrDefault(x => x.StateName == Id).Id;
            var local = from s in db.LocalGovs
                        where s.StatesId == stateId
                        select s;

            return Json(new SelectList(local.ToArray(), "LGAName", "LGAName"), JsonRequestBehavior.AllowGet);
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