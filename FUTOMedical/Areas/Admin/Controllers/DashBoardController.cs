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

namespace FUTOMedical.Areas.Admin.Controllers
{
    public class DashBoardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/DashBoard
        // GET: Reports
        public async Task<ActionResult> Index()
        {
            return View(await db.Reports.Include(x => x.OPD).Include(x => x.Doctor).Include(x => x.Nurse).Include(x=>x.Patient).OrderByDescending(x=>x.Id).ToListAsync());
        }
    }
}