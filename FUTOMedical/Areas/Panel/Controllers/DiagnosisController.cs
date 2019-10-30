using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FUTOMedical.Models;
using FUTOMedical.Models.Entities;

namespace FUTOMedical.Areas.Panel.Controllers
{
    public class DiagnosisController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Diagnosis
        public async Task<ActionResult> Index()
        {
            var ind = await db.TestReports.Include(x => x.Doctor).Include(x => x.Laboratorists).Include(x => x.Nurse).Include(x => x.OPD).Include(x => x.Patient).Where(x => x.Status == ReportStatus.Test).ToListAsync();
            return View(ind);
        }

        // GET: Diagnosis
        public async Task<ActionResult> PatientDiagnosis()
        {
            return View(await db.Reports.Include(x => x.OPD).Include(x => x.Doctor).Include(x => x.Patient).Include(x => x.Nurse).Where(x => x.Status == ReportStatus.Test).ToListAsync());
        }

        // GET: Diagnosis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var diagnosis = db.TestReports.Include(x => x.Doctor).Include(x => x.Laboratorists).Include(x => x.Nurse).Include(x => x.OPD).Include(x => x.Patient).FirstOrDefault(x => x.Id == id);

            if (diagnosis == null)
            {
                return HttpNotFound();
            }
            return View(diagnosis);
        }

        // GET: Diagnosis/Create
        public ActionResult Create(int? id)
        {
            ViewBag.id = id;
            return View();
        }

        // POST: Diagnosis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TestReport diagnosis, int? id)
        {
            if (ModelState.IsValid)
            {
                diagnosis.PatientId = id;
                var opd = db.OPD.OrderByDescending(x => x.Id).FirstOrDefault(x => x.PatientId == id);
                var pat = db.Reports.OrderByDescending(x => x.Id).FirstOrDefault(x => x.PatientId == id);
                diagnosis.OPDId = opd.Id;
                diagnosis.NurseId = opd.NurseId;
                diagnosis.DoctorId = pat.DoctorId;
                diagnosis.Status = ReportStatus.AttendedTo;

                //Update Report status after diagnosis
                var report = db.Reports.Include(x => x.Patient).FirstOrDefault(x => x.PatientId == id);
                var report2 = db.Reports.Include(x => x.Patient).FirstOrDefault(x => x.Id == report.Id);
                report2.Status = ReportStatus.AttendedTo;
                db.Entry(report2).State = EntityState.Modified;
                db.SaveChanges();


                db.TestReports.Add(diagnosis);
                db.SaveChanges();
                return RedirectToAction("Details", "Diagnosis", new { id = diagnosis.Id, area = "Panel" });
            }

            return View(diagnosis);
        }

        // GET: Diagnosis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestReport diagnosis = db.TestReports.Find(id);
            if (diagnosis == null)
            {
                return HttpNotFound();
            }
            return View(diagnosis);
        }

        // POST: Diagnosis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TestReport diagnosis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diagnosis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(diagnosis);
        }

        // GET: Diagnosis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //TestReport diagnosis = db.TestReports.Find(id);
            var diagnosis = db.TestReports.Include(x => x.Doctor).Include(x => x.Laboratorists).Include(x => x.Nurse).Include(x => x.OPD).Include(x => x.Patient).FirstOrDefault(x => x.Id == id);
            if (diagnosis == null)
            {
                return HttpNotFound();
            }
            return View(diagnosis);
        }

        // POST: Diagnosis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestReport diagnosis = db.TestReports.Find(id);
            db.TestReports.Remove(diagnosis);
            db.SaveChanges();
            return RedirectToAction("Index", "Diagnosis", new { area = "Panel" });
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
