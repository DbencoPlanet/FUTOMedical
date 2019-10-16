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
    public class MedicinesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Panel/Medicines
        public async Task<ActionResult> Index()
        {
            var medicines = db.Medicines.Include(m => m.MedicineCategory);
            return View(await medicines.ToListAsync());
        }

        // GET: Panel/Medicines/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicine medicine = await db.Medicines.FindAsync(id);
            if (medicine == null)
            {
                return HttpNotFound();
            }
            return View(medicine);
        }

        // GET: Panel/Medicines/Create
        public ActionResult Create()
        {
            ViewBag.MedicineCategoryId = new SelectList(db.MedicineCategories, "Id", "Name");
            return View();
        }

        // POST: Panel/Medicines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                db.Medicines.Add(medicine);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MedicineCategoryId = new SelectList(db.MedicineCategories, "Id", "Name", medicine.MedicineCategoryId);
            return View(medicine);
        }

        // GET: Panel/Medicines/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicine medicine = await db.Medicines.FindAsync(id);
            if (medicine == null)
            {
                return HttpNotFound();
            }
            ViewBag.MedicineCategoryId = new SelectList(db.MedicineCategories, "Id", "Name", medicine.MedicineCategoryId);
            return View(medicine);
        }

        // POST: Panel/Medicines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicine).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MedicineCategoryId = new SelectList(db.MedicineCategories, "Id", "Name", medicine.MedicineCategoryId);
            return View(medicine);
        }

        // GET: Panel/Medicines/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicine medicine = await db.Medicines.FindAsync(id);
            if (medicine == null)
            {
                return HttpNotFound();
            }
            return View(medicine);
        }

        // POST: Panel/Medicines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Medicine medicine = await db.Medicines.FindAsync(id);
            db.Medicines.Remove(medicine);
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
