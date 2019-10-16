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
using System.IO;

namespace FUTOMedical.Areas.Admin.Controllers
{
    public class SettingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Settings
        public async Task<ActionResult> Index()
        {
            return View(await db.Settings.FirstOrDefaultAsync());
        }

        // GET: Admin/Settings/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Setting setting = await db.Settings.FindAsync(id);
            if (setting == null)
            {
                return HttpNotFound();
            }
            return View(setting);
        }

        // GET: Admin/Settings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Settings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Setting setting, HttpPostedFileBase logo, HttpPostedFileBase favicon)
        {
            if (ModelState.IsValid)
            {
                //logo
                if (logo != null && logo.ContentLength > 0)

                {
                    System.Random randomInteger = new System.Random();
                    int genNumber = randomInteger.Next(1000);

                    if (logo.ContentLength > 0 && logo.ContentType.ToUpper().Contains("JPEG") || logo.ContentType.ToUpper().Contains("PNG") || logo.ContentType.ToUpper().Contains("JPG"))
                    {

                        string fileName = Path.GetFileName(genNumber + "_" + logo.FileName);
                        setting.Logo = "~/Uploads/Settings/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Uploads/Settings/"), fileName);
                        logo.SaveAs(fileName);


                    }
                }

                //favicon
                if (favicon != null && favicon.ContentLength > 0)

                {
                    System.Random randomInteger = new System.Random();
                    int genNumber = randomInteger.Next(1000);

                    if (favicon.ContentLength > 0 && favicon.ContentType.ToUpper().Contains("JPEG") || favicon.ContentType.ToUpper().Contains("PNG") || favicon.ContentType.ToUpper().Contains("JPG"))
                    {

                        string fileName = Path.GetFileName(genNumber + "_" + favicon.FileName);
                        setting.Favicon = "~/Uploads/Settings/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Uploads/Settings/"), fileName);
                        favicon.SaveAs(fileName);


                    }
                }
                db.Settings.Add(setting);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(setting);
        }

        // GET: Admin/Settings/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Setting setting = await db.Settings.FindAsync(id);
            if (setting == null)
            {
                return HttpNotFound();
            }
            return View(setting);
        }

        // POST: Admin/Settings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Setting setting, HttpPostedFileBase logo, HttpPostedFileBase favicon)
        {
            if (ModelState.IsValid)
            {
                //logo
                if (logo != null && logo.ContentLength > 0)

                {
                    System.Random randomInteger = new System.Random();
                    int genNumber = randomInteger.Next(1000);

                    if (logo.ContentLength > 0 && logo.ContentType.ToUpper().Contains("JPEG") || logo.ContentType.ToUpper().Contains("PNG") || logo.ContentType.ToUpper().Contains("JPG"))
                    {

                        string fileName = Path.GetFileName(genNumber + "_" + logo.FileName);
                        setting.Logo = "~/Uploads/Settings/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Uploads/Settings/"), fileName);
                        logo.SaveAs(fileName);


                    }
                }

                //favicon
                if (favicon != null && favicon.ContentLength > 0)

                {
                    System.Random randomInteger = new System.Random();
                    int genNumber = randomInteger.Next(1000);

                    if (favicon.ContentLength > 0 && favicon.ContentType.ToUpper().Contains("JPEG") || favicon.ContentType.ToUpper().Contains("PNG") || favicon.ContentType.ToUpper().Contains("JPG"))
                    {

                        string fileName = Path.GetFileName(genNumber + "_" + favicon.FileName);
                        setting.Favicon = "~/Uploads/Settings/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Uploads/Settings/"), fileName);
                        favicon.SaveAs(fileName);


                    }
                }
                db.Entry(setting).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(setting);
        }

        // GET: Admin/Settings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Setting setting = await db.Settings.FindAsync(id);
            if (setting == null)
            {
                return HttpNotFound();
            }
            return View(setting);
        }

        // POST: Admin/Settings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Setting setting = await db.Settings.FindAsync(id);
            db.Settings.Remove(setting);
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
