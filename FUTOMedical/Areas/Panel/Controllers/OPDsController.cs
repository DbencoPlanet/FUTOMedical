using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using FUTOMedical.Models;
using FUTOMedical.Models.Entities;

namespace FUTOMedical.Areas.Panel.Controllers
{
    public class OPDsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OPDs
        public async Task<ActionResult> Index()
        {
            return View(await db.OPD.ToListAsync());
        }

        // GET: OPDs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OPD oPD = await db.OPD.FindAsync(id);
            if (oPD == null)
            {
                return HttpNotFound();
            }
            return View(oPD);
        }

        // GET: OPDs/Create
        public ActionResult Create(int id)
        {
            ViewBag.id = id;
            return View();
        }

        // POST: OPDs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OPD oPD, int id)
        {
            if (ModelState.IsValid)
            {
                var folderNo = db.Patients.FirstOrDefault(x => x.Id == id).FolderNumber;
                var user = User.Identity.GetUserId();
                var nurse = await db.Nurses.FirstOrDefaultAsync(x=>x.UserId == user);
                oPD.NurseId = nurse.Id;
                oPD.PatientId = id;
                db.OPD.Add(oPD);
                await db.SaveChangesAsync();

                Report report = new Report();
                report.Status = ReportStatus.None;
                report.FolderNumber = folderNo;
                report.OPDId = oPD.Id;
                report.PatientId = id;
                db.Reports.Add(report);
                await db.SaveChangesAsync();

                TestReport report2 = new TestReport();
                report.Status = ReportStatus.None;
                report.FolderNumber = folderNo;
                report.OPDId = oPD.Id;
                report.PatientId = id;
                db.TestReports.Add(report2);
                await db.SaveChangesAsync();

                var sec = await db.Sections.FirstOrDefaultAsync(x => x.PatientId == id);
                sec.OpdStatus = true;
                db.Entry(sec).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Success");
            }

            return View(oPD);
        }
        public ActionResult Success()
        {
            return View();
        }

        // GET: OPDs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OPD oPD = await db.OPD.FindAsync(id);
            if (oPD == null)
            {
                return HttpNotFound();
            }
            return View(oPD);
        }

        // POST: OPDs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(OPD oPD)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oPD).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(oPD);
        }

        // GET: OPDs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OPD oPD = await db.OPD.FindAsync(id);
            if (oPD == null)
            {
                return HttpNotFound();
            }
            return View(oPD);
        }

        // POST: OPDs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            OPD oPD = await db.OPD.FindAsync(id);
            db.OPD.Remove(oPD);
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
