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
    public class ReportsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reports
        public async Task<ActionResult> Index()
        {
            return View(await db.Reports.Include(x=>x.OPD).Include(x=>x.Doctor).Include(x=>x.Nurse).Include(x=>x.Patient).OrderByDescending(x=>x.Id).ToListAsync());
        }

        // GET: Reports
        public async Task<ActionResult> PatientReport(int? id)
        {
            return View(await db.Reports.Include(x => x.OPD).Include(x => x.Doctor).Include(x => x.Nurse).Include(x=>x.Patient).OrderByDescending(x=>x.Id).Where(x => x.PatientId == id).ToListAsync());
        }


        // GET: Reports/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = await db.Reports.Include(x => x.OPD).Include(x => x.Doctor).Include(x => x.Nurse).FirstOrDefaultAsync(x => x.Id == id);
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
        public async Task<ActionResult> Create(Report report)
        {
            if (ModelState.IsValid)
            {
                db.Reports.Add(report);
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
            //else if (!String.IsNullOrEmpty(searchString))
            //{
            //    //  patient = patient.FirstOrDefault(x => x.FolderNumber.ToUpper().Contains(searchString.ToUpper()));
            //}
            else if (report == null)
            {
                TempData["error"] = "Folder not found.";
            }
            return View(report);
        }


        public ActionResult Discharge(int? id)
        {
            var sec = db.Sections.OrderByDescending(x=>x.Id).FirstOrDefault(x => x.PatientId == id);
            var sec2 = db.Sections.OrderByDescending(x => x.Id).FirstOrDefault(x => x.Id == sec.Id);
            sec2.CloseDate = DateTime.UtcNow.AddHours(1);
            sec2.SectionClose = true;
            db.Entry(sec2).State = EntityState.Modified;
            db.SaveChanges();
            var adm = db.Admissions.FirstOrDefault(x => x.PatientId == id);
            var adm2 = db.Admissions.FirstOrDefault(x => x.Id == adm.Id);
            if (adm2 != null)
            {
                adm2.Status = AdmissionStatus.NotActive;
                adm2.DateDischarged = DateTime.UtcNow.AddHours(1);
                db.Entry(adm2).State = EntityState.Modified;
                db.SaveChanges();
            }


            return RedirectToAction("Success");
        }

        //
        public ActionResult Success()
        {
            return View();
        }
        // GET: Reports/Edit/5
        //  [Authorize(Roles="Doctor")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = await db.Reports.FirstOrDefaultAsync(x => x.Id == id);
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
        public async Task<ActionResult> Edit(Report report, int? id)
        {
            if (ModelState.IsValid)
            {
                Report report1 = await db.Reports.FirstOrDefaultAsync(x => x.Id == id);
                var user = User.Identity.GetUserId();
                var doc = await db.Doctors.FirstOrDefaultAsync(x=>x.UserId == user);
                report.DoctorId = doc.Id;
                if (report1.Status == ReportStatus.Operation)
                {
                    GReport greport = new GReport();
                    greport.PatientId = report1.PatientId;
                    greport.Report = report.DoctorReport;
                    greport.Type = GReportType.Operation;
                    db.GReports.Add(greport);
                    await db.SaveChangesAsync();

                    report1.Status = ReportStatus.None;
                    db.Entry(report1).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Operation","EventReport","Panel");
                }
                else
                {
                    db.Entry(report).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                
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
