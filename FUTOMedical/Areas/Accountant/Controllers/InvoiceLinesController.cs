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
    public class InvoiceLinesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Accountant/InvoiceLines
        public async Task<ActionResult> Index()
        {
            var invoiceLines = db.InvoiceLines.Include(i => i.Invoice);
            return View(await invoiceLines.ToListAsync());
        }

        // GET: Accountant/InvoiceLines/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceLine invoiceLine = await db.InvoiceLines.FindAsync(id);
            if (invoiceLine == null)
            {
                return HttpNotFound();
            }
            return View(invoiceLine);
        }

        // GET: Accountant/InvoiceLines/Create
        public ActionResult Create(int? id)
        {
            ViewBag.id = id;
            //ViewBag.InvoiceId = new SelectList(db.Invoices, "Id", "InvoiceNumber");
            return View();
        }

        // POST: Accountant/InvoiceLines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(InvoiceLine invoiceLine, int? id)
        {
            if (ModelState.IsValid)
            {
                invoiceLine.InvoiceId = id;
                invoiceLine.Amount = (decimal)invoiceLine.Quantity * invoiceLine.Price;
                invoiceLine.Vat = (decimal)(invoiceLine.VatRate / 100.0) * invoiceLine.Amount;
                invoiceLine.SubTotal = invoiceLine.Amount + invoiceLine.Vat - invoiceLine.Discount;


                Invoice inv = await db.Invoices.Include(x => x.InvoiceLine).SingleOrDefaultAsync(x => x.Id == id);
                inv.Vat = inv.InvoiceLine.Sum(x => x.Vat);
                inv.Total = inv.InvoiceLine.Sum(x => x.Amount);
                inv.Due = inv.Total;
                inv.Discount = inv.InvoiceLine.Sum(x => x.Discount);
                inv.GrandTotal = inv.Total + inv.Vat - inv.Discount;
                db.Entry(inv).State = EntityState.Modified;
                await db.SaveChangesAsync();

                db.InvoiceLines.Add(invoiceLine);
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "Invoices", new { id = invoiceLine.InvoiceId});
            }

            //ViewBag.InvoiceId = new SelectList(db.Invoices, "Id", "InvoiceNumber", invoiceLine.InvoiceId);
            return View(invoiceLine);
        }

        // GET: Accountant/InvoiceLines/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceLine invoiceLine = await db.InvoiceLines.FindAsync(id);
            if (invoiceLine == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvoiceId = new SelectList(db.Invoices, "Id", "InvoiceNumber", invoiceLine.InvoiceId);
            return View(invoiceLine);
        }

        // POST: Accountant/InvoiceLines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,InvoiceId,Name,Description,Quantity,Price,SubTotal")] InvoiceLine invoiceLine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoiceLine).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.InvoiceId = new SelectList(db.Invoices, "Id", "InvoiceNumber", invoiceLine.InvoiceId);
            return View(invoiceLine);
        }

        // GET: Accountant/InvoiceLines/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceLine invoiceLine = await db.InvoiceLines.FindAsync(id);
            if (invoiceLine == null)
            {
                return HttpNotFound();
            }
            return View(invoiceLine);
        }

        // POST: Accountant/InvoiceLines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            InvoiceLine invoiceLine = await db.InvoiceLines.FindAsync(id);
            db.InvoiceLines.Remove(invoiceLine);
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
