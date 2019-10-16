using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FUTOMedical.Models;
using FUTOMedical.Models.Entities;

namespace FUTOMedical.Areas.Panel.Controllers
{
    public class EventReportController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: All Report
        public ActionResult Index()
        {
            var report = db.GReports.Include(x => x.Patient).ToList();
            return View(report);
        }

        //Patients to be operated on
        // GET: Operate
        public async Task<ActionResult> Operate()
        {
            return View(await db.Reports.Include(x => x.OPD).Include(x => x.Doctor).Include(x => x.Patient).Include(x => x.Nurse).Where(x => x.Status == ReportStatus.Operation).ToListAsync());
        }

        // GET: Birth Report
        public ActionResult Birth()
        {
            var birth = db.GReports.Include(x => x.Patient).Where(x => x.Type == GReportType.Birth).ToList();
            return View(birth);
        }

        // GET: Birth Report
        public ActionResult Death()
        {
            var birth = db.GReports.Include(x => x.Patient).Where(x => x.Type == GReportType.Death).ToList();
            return View(birth);
        }

        // GET: Operation Report
        public ActionResult Operation()
        {
            var birth = db.GReports.Include(x => x.Patient).Where(x => x.Type == GReportType.Operation).ToList();
            return View(birth);
        }

        // GET: BirthReport/Create
        public ActionResult BirthReport()
        {
            var patient = db.Patients.ToList();
            ViewBag.PatientId = new SelectList(patient,"Id","Fullname");
            return View();
        }

        // POST: Sections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BirthReport(GReport greport)
        {
            if (ModelState.IsValid)
            {
                greport.Type = GReportType.Birth;
                db.GReports.Add(greport);
                await db.SaveChangesAsync();
                return RedirectToAction("Birth");
            }
            var patient = db.Patients.ToList();
            ViewBag.PatientId = new SelectList(patient, "Id", "Fullname",greport.PatientId);
            return View(greport);
        }


        // GET: DeathReport/Create
        public ActionResult DeathReport()
        {
            var patient = db.Patients.ToList();
            ViewBag.PatientId = new SelectList(patient, "Id", "Fullname");
            return View();
        }

        // POST: Sections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeathReport(GReport greport)
        {
            if (ModelState.IsValid)
            {
                greport.Type = GReportType.Death;
                db.GReports.Add(greport);
                await db.SaveChangesAsync();
                return RedirectToAction("Death");
            }
            var patient = db.Patients.ToList();
            ViewBag.PatientId = new SelectList(patient, "Id", "Fullname", greport.PatientId);
            return View(greport);
        }



        // GET: EventReport/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var report = await db.GReports.Include(x => x.Patient).FirstOrDefaultAsync(x=>x.Id == id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // GET: EventReport/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(GReport evt)
        {
            if (ModelState.IsValid)
            {
                db.GReports.Add(evt);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(evt);
        }

        // GET: EventReport/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var evt = db.GReports.Include(x => x.Patient).FirstOrDefaultAsync(x=>x.Id == id);
            if (evt == null)
            {
                return HttpNotFound();
            }
            return View(evt);
        }

        // POST: Sections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(GReport evt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evt).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(evt);
        }

        // GET: EventReport/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var evt = db.GReports.Include(x => x.Patient).FirstOrDefaultAsync(x => x.Id == id);
            if (evt == null)
            {
                return HttpNotFound();
            }
            return View(evt);
        }

        // POST: EventReport/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            GReport evt = await db.GReports.FindAsync(id);
            db.GReports.Remove(evt);
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
