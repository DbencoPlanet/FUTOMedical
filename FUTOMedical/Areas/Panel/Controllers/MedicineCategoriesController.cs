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
    public class MedicineCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Panel/MedicineCategories
        public async Task<ActionResult> Index()
        {
            return View(await db.MedicineCategories.ToListAsync());
        }

        // GET: Panel/MedicineCategories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicineCategory medicineCategory = await db.MedicineCategories.FindAsync(id);
            if (medicineCategory == null)
            {
                return HttpNotFound();
            }
            return View(medicineCategory);
        }

        // GET: Panel/MedicineCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Panel/MedicineCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MedicineCategory medicineCategory)
        {
            if (ModelState.IsValid)
            {
                db.MedicineCategories.Add(medicineCategory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(medicineCategory);
        }

        // GET: Panel/MedicineCategories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicineCategory medicineCategory = await db.MedicineCategories.FindAsync(id);
            if (medicineCategory == null)
            {
                return HttpNotFound();
            }
            return View(medicineCategory);
        }

        // POST: Panel/MedicineCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(MedicineCategory medicineCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicineCategory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(medicineCategory);
        }

        // GET: Panel/MedicineCategories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicineCategory medicineCategory = await db.MedicineCategories.FindAsync(id);
            if (medicineCategory == null)
            {
                return HttpNotFound();
            }
            return View(medicineCategory);
        }

        // POST: Panel/MedicineCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MedicineCategory medicineCategory = await db.MedicineCategories.FindAsync(id);
            db.MedicineCategories.Remove(medicineCategory);
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
