using FUTOMedical.Models;
using FUTOMedical.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;

namespace FUTOMedical.Areas.Accountant.Controllers
{
    public class DashBoardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Accountant/Invoices
        public async Task<ActionResult> Index()
        {
            var invoices = db.Invoices.Include(i => i.Patient).Include(x => x.InvoiceLine).OrderByDescending(x=>x.StartDate);
            return View(await invoices.ToListAsync());
        }

        // GET: Accountant/Invoices
        public async Task<ActionResult> PatientInvoice(int? id)
        {
            var invoices = db.Invoices.Include(i => i.Patient).Include(x => x.InvoiceLine).OrderByDescending(x => x.StartDate).Where(x=>x.PatientId == id);
            return View(await invoices.ToListAsync());
        }


        // GET: Accountant/Invoices
        public async Task<ActionResult> PatientPayment(int? id)
        {
            var payment = db.Payments.Include(i => i.Patient).Include(x => x.Invoice).OrderByDescending(x => x.PaymentDate).Where(x => x.PatientId == id);
            return View(await payment.ToListAsync());
        }

        public async Task<ActionResult> Patients()
        {
            var pat = db.Patients.Include(i => i.User).OrderBy(x => x.Surname);
            return View(await pat.ToListAsync());
        }
    }
}