using FUTOMedical.Models;
using FUTOMedical.Models.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FUTOMedical.Areas.Patients.Controllers
{
    public class PanelController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;


        public PanelController()
        {

        }
        public PanelController(ApplicationSignInManager signInManager, ApplicationUserManager userManager, ApplicationRoleManager roleManager)
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

        // GET: Patients/Panel
        // GET: OPDs
        public async Task<ActionResult> Index()
        {
            var user = User.Identity.GetUserId();
            var pat = await db.Patients.FirstOrDefaultAsync(x=>x.UserId == user);
            var opd = await db.OPD.Include(x => x.Patient).Include(x => x.Nurse).Where(x=>x.PatientId == pat.Id).ToListAsync();
            return View(opd);
        }

        // GET: Admissions
        public async Task<ActionResult> Admission()
        {
            var user = User.Identity.GetUserId();
            var pat = await db.Patients.FirstOrDefaultAsync(x => x.UserId == user);
            var ind = await db.Admissions.Include(x => x.OPD).Include(x => x.Patient).Where(x => x.Status == AdmissionStatus.Active && x.PatientId == pat.Id).ToListAsync();
            return View(ind);
        }

        // GET: Birth Report
        public async Task<ActionResult> Birth()
        {
            var user = User.Identity.GetUserId();
            var pat = await db.Patients.FirstOrDefaultAsync(x => x.UserId == user);
            var birth = await db.GReports.Include(x => x.Patient).Where(x => x.Type == GReportType.Birth && x.PatientId == pat.Id).ToListAsync();
            return View(birth);
        }

        // GET: Birth Report
        public async Task<ActionResult> Death()
        {
            var user = User.Identity.GetUserId();
            var pat = await db.Patients.FirstOrDefaultAsync(x => x.UserId == user);
            var birth = db.GReports.Include(x => x.Patient).Where(x => x.Type == GReportType.Death && x.PatientId == pat.Id).ToList();
            return View(birth);
        }

        // GET: Operation Report
        public async Task<ActionResult> Operation()
        {
            var user = User.Identity.GetUserId();
            var pat = await db.Patients.FirstOrDefaultAsync(x => x.UserId == user);
            var birth = db.GReports.Include(x => x.Patient).Where(x => x.Type == GReportType.Operation && x.PatientId == pat.Id).ToList();
            return View(birth);
        }

        // GET: Reports
        public async Task<ActionResult> Prescription()
        {
            var user = User.Identity.GetUserId();
            var pat = await db.Patients.FirstOrDefaultAsync(x => x.UserId == user);
            return View(await db.Reports.Include(x => x.OPD).Include(x => x.Doctor).Include(x => x.Nurse).Include(x => x.Patient).Where(x=>x.PatientId == pat.Id).OrderByDescending(x => x.Id).ToListAsync());
        }

        // GET: Diagnosis
        public async Task<ActionResult> Diagnosis()
        {
            var user = User.Identity.GetUserId();
            var pat = await db.Patients.FirstOrDefaultAsync(x => x.UserId == user);
            var ind = await db.TestReports.Include(x => x.Doctor).Include(x => x.Laboratorists).Include(x => x.Nurse).Include(x => x.OPD).Include(x => x.Patient).Where(x => x.Status == ReportStatus.Test && x.PatientId == pat.Id).ToListAsync();
            return View(ind);
        }

        // GET: Panel/BloodBanks
        public async Task<ActionResult> BloodBank()
        {
            return View(await db.BloodBank.ToListAsync());
        }

        public async Task<ActionResult> Doctors()
        {
            var doc = await db.Doctors.Include(x => x.User).Where(x => x.User.UserName != "super@admin.com").ToListAsync();
            return View(doc);
        }
    }
}