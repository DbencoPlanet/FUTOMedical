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
    public class PaymentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Accountant/Payments
        public async Task<ActionResult> Index()
        {
            var payments = db.Payments.Include(p => p.Invoice)
                .Include(p => p.Patient).OrderByDescending(X=>X.PaymentDate);

            var set = await db.Settings.FirstOrDefaultAsync();
            ViewBag.set = set;
            return View(await payments.ToListAsync());
        }

        // GET: Accountant/Payments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = await db.Payments.Include(x=>x.Invoice).Include(x=>x.Patient).FirstOrDefaultAsync(x=>x.Id == id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            var set = await db.Settings.FirstOrDefaultAsync();
            ViewBag.set = set;
            return View(payment);
        }

        // GET: Accountant/Payments/Create
        public ActionResult Create(int? pId,int?invId)
        {
            //ViewBag.InvoiceId = new SelectList(db.Invoices, "Id", "InvoiceNumber");
            //ViewBag.PatientId = new SelectList(db.Patients, "Id", "UserId");
            ViewBag.PatientId = pId;
            ViewBag.InvoiceId = invId;

            return View();
        }

        // POST: Accountant/Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Payment payment,int? pId, int? invId)
        {
            if (ModelState.IsValid)
            {
                //Update Invoice Payment Status
                var invoice = await db.Invoices.Include(x => x.InvoiceLine).Include(c => c.Patient).FirstOrDefaultAsync(c=>c.Id == invId);
                invoice.Status = PaymentStatus.Paid;
                db.Entry(invoice).State = EntityState.Modified;
                await db.SaveChangesAsync();

                //Invoice Payment
                payment.InvoiceId = invId;
                payment.PatientId = pId;
                payment.PaymentDate = DateTime.UtcNow.AddHours(1);
                db.Payments.Add(payment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            //ViewBag.InvoiceId = new SelectList(db.Invoices, "Id", "InvoiceNumber", payment.InvoiceId);
            //ViewBag.PatientId = new SelectList(db.Patients, "Id", "UserId", payment.PatientId);
            return View(payment);
        }

        // GET: Accountant/Payments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = await db.Payments.FindAsync(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvoiceId = new SelectList(db.Invoices, "Id", "InvoiceNumber", payment.InvoiceId);
            ViewBag.PatientId = new SelectList(db.Patients, "Id", "UserId", payment.PatientId);
            return View(payment);
        }

        // POST: Accountant/Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,PatientId,InvoiceId,PaymentDate")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.InvoiceId = new SelectList(db.Invoices, "Id", "InvoiceNumber", payment.InvoiceId);
            ViewBag.PatientId = new SelectList(db.Patients, "Id", "UserId", payment.PatientId);
            return View(payment);
        }

        // GET: Accountant/Payments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = await db.Payments.FindAsync(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Accountant/Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Payment payment = await db.Payments.FindAsync(id);
            db.Payments.Remove(payment);
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
