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
    public class AdmissionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admissions
        public ActionResult Index()
        {
            var ind = db.Admissions.Include(x=>x.OPD).Include(x=>x.Patient).Where(x => x.Status == AdmissionStatus.Active);
            return View(ind.ToList());
        }

        // GET: Reports
        public async Task<ActionResult> Admit()
        {
            return View(await db.Reports.Include(x => x.OPD).Include(x => x.Doctor).Include(x=>x.Patient).Include(x => x.Nurse).Where(x=>x.Status == ReportStatus.Admit).ToListAsync());
        }

        // GET: Sections
        public ActionResult Discharged()
        {
            var discharge = db.Admissions.Include(x => x.OPD).Include(x => x.Patient).Where(x => x.Status == AdmissionStatus.Discharged);
            return View(discharge);
        }

        // GET: Admissions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Admission admission = db.Admissions.Find(id);
            var admission = db.Admissions.Include(x => x.OPD).Include(x => x.Patient).FirstOrDefault(x=>x.Id == id);
            if (admission == null)
            {
                return HttpNotFound();
            }
            return View(admission);
        }

        // GET: Admissions/Create
        public ActionResult Create(int? id)
        {
            ViewBag.id = id;
            return View();
        }

        // POST: Admissions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Admission admission, int? id)
        {
            if (ModelState.IsValid)
            {
                admission.PatientId = id;
                var opd = db.OPD.OrderByDescending(x => x.Id).FirstOrDefault(x => x.PatientId == id).Id;
                admission.OPDId = opd;
                admission.DateAdmitted = DateTime.UtcNow.AddHours(1);
                admission.Status = AdmissionStatus.Active;

                //Update Report status after admission
                var report = db.Reports.Include(x=>x.Patient).FirstOrDefault(x=>x.PatientId == id);
                var report2 = db.Reports.Include(x => x.Patient).FirstOrDefault(x=>x.Id == report.Id);
                report2.Status = ReportStatus.DontAdmit;
                db.Entry(report2).State = EntityState.Modified;
                db.SaveChanges();

                db.Admissions.Add(admission);
                db.SaveChanges();
                return RedirectToAction("Details","Admissions", new { id = admission.Id, area ="Panel" });
            }

            return View(admission);
        }

        // GET: Admissions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admission admission = db.Admissions.Find(id);
            if (admission == null)
            {
                return HttpNotFound();
            }
            return View(admission);
        }

        // POST: Admissions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Admission admission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admission).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admission);
        }

        // GET: Admissions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admission admission = db.Admissions.Find(id);
            if (admission == null)
            {
                return HttpNotFound();
            }
            return View(admission);
        }

        // POST: Admissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admission admission = db.Admissions.Find(id);
            db.Admissions.Remove(admission);
            db.SaveChanges();
            return RedirectToAction("Index","Admissions",new { area = "Panel"});
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
