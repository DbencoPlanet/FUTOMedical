using FUTOMedical.Models;
using FUTOMedical.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace FUTOMedical.Areas.Doctors.Controllers
{
    public class DashBoardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;


        public DashBoardController()
        {

        }
        public DashBoardController(ApplicationSignInManager signInManager, ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
         
            UserManager = userManager;
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
        // GET: Doctor/DashBoard
        // GET: Reports
        public async Task<ActionResult> Index()
        {
            return View(await db.Reports.Include(x => x.OPD).Include(x => x.Doctor).Include(x => x.Nurse).Include(x=>x.Patient).ToListAsync());
        }


        // GET: Reports
        public async Task<ActionResult> Report()
        {
            var user = User.Identity.GetUserId();
            var doc = await db.Doctors.Include(x => x.User).FirstOrDefaultAsync(x=>x.UserId == user);
            return View(await db.Reports.Include(x => x.OPD).Include(x => x.Doctor).Include(x => x.Nurse).Include(x => x.Patient).OrderByDescending(x => x.Id).Where(x => x.DoctorId == doc.Id).ToListAsync());
        }
    }
}