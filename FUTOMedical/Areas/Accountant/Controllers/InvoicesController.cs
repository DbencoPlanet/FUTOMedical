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

namespace FUTOMedical.Areas.Accountant.Controllers
{
    public class InvoicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Accountant/Invoices
        public async Task<ActionResult> Index()
        {
            var invoices = db.Invoices.Include(i => i.Patient).Include(x=>x.InvoiceLine);
            return View(await invoices.ToListAsync());
        }

        public async Task<ActionResult> PrintInvoice(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var invoice = await db.Invoices.Include(x => x.InvoiceLine).Include(x => x.Patient).FirstOrDefaultAsync(x => x.Id == id);
            var setting = await db.Settings.FirstOrDefaultAsync();
            ViewBag.setting = setting;
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: Accountant/Invoices/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var invoice = await db.Invoices.Include(x=>x.InvoiceLine).Include(x=>x.Patient).FirstOrDefaultAsync(x=>x.Id == id);
            var setting = await db.Settings.FirstOrDefaultAsync();
            ViewBag.setting = setting;
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: Accountant/Invoices/Create
        public ActionResult Create()
        {
            ViewBag.PatientId = new SelectList(db.Patients, "Id", "Fullname");
            return View();
        }

        // POST: Accountant/Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Invoices.Add(invoice);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PatientId = new SelectList(db.Patients, "Id", "Fullname", invoice.PatientId);
            return View(invoice);
        }

        // GET: Accountant/Invoices/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = await db.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.PatientId = new SelectList(db.Patients, "Id", "Fullname", invoice.PatientId);
            return View(invoice);
        }

        // POST: Accountant/Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PatientId = new SelectList(db.Patients, "Id", "Fullname", invoice.PatientId);
            return View(invoice);
        }

        // GET: Accountant/Invoices/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = await db.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Accountant/Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Invoice invoice = await db.Invoices.FindAsync(id);
            db.Invoices.Remove(invoice);
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
