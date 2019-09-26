using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNet.Identity;
using System.Web;
using System.Web.Mvc;
using FUTOMedical.Models;
using FUTOMedical.Models.Entities;

namespace FUTOMedical.Areas.Panel.Controllers
{
    public class DiagnosisController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reports
        public async Task<ActionResult> Index()
        {
            return View(await db.TestReports.Include(x => x.OPD).Include(x => x.Doctor).Include(x => x.Nurse).Include(x=>x.Laboratorists).ToListAsync());
        }

        // GET: Reports/Details/5
        public async Task<ActionResult> Details(string folderNumber)
        {
            if (folderNumber == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestReport report = await db.TestReports.Include(x => x.OPD).Include(x => x.Doctor).Include(x => x.Nurse).FirstOrDefaultAsync(x => x.FolderNumber == folderNumber);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // GET: Reports/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TestReport report)
        {
            if (ModelState.IsValid)
            {
                db.TestReports.Add(report);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(report);
        }
        public ActionResult SearchReport()
        {
            return View();
        }
        public ActionResult SearchPatientReport(string currentFilter, string searchString)
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
            var report = db.Patients.FirstOrDefault(x => x.FolderNumber.ToUpper().Contains(searchString.ToUpper()));

            if (String.IsNullOrEmpty(searchString))
            {
                ViewBag.SearchString = "Empty";
            }
         
            else if (report == null)
            {
                TempData["error"] = "Folder not found.";
            }
            return View(report);
        }


        // GET: Reports/Edit/5
        //  [Authorize(Roles="Doctor")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestReport report = await db.TestReports.FirstOrDefaultAsync(x => x.PatientId == id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TestReport report)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.GetUserId();
                var doc = await db.Laboratorists.FirstOrDefaultAsync(x => x.UserId == user);
                report.LaboratoristsId = doc.Id;
                db.Entry(report).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(report);
        }

        // GET: Reports/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = await db.Reports.FindAsync(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Report report = await db.Reports.FindAsync(id);
            db.Reports.Remove(report);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
