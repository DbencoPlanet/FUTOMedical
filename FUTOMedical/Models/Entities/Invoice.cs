using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FUTOMedical.Models.Entities
{
    public class Invoice
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public Invoice()
        {

            var set = db.Settings.FirstOrDefault();
            var setname = set.Initial;

            this.InvoiceNumber = DateTime.UtcNow.Date.Year.ToString() +
                DateTime.UtcNow.Date.Month.ToString() +
                DateTime.UtcNow.Date.Day.ToString() + Guid.NewGuid().ToString().Substring(0, 4).ToUpper() +"INV"+"-"+setname;
            this.StartDate = DateTime.UtcNow;
            this.EndDate = DateTime.UtcNow.Date.AddMonths(1);
            this.Vat = 0;
            this.Discount = 0;
            this.Due = 0;
            this.Total = 0;
            this.GrandTotal = 0;
        }

        public int Id { get; set; }

        public string InvoiceNumber { get; set; }

        [Display(Name = "Patient Name")]
        public int? PatientId { get; set; }
        public Patient Patient { get; set; }


        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<InvoiceLine> InvoiceLine { get; set; } = new List<InvoiceLine>();

        [Display(Name = "Vat")]
        public decimal? Vat { get; set; }

        [Display(Name = "Discount")]
        public decimal? Discount { get; set; }

        [Display(Name = "Payment Status")]
        public PaymentStatus Status { get; set; }

        [Display(Name = "Due")]
        public decimal? Due { get; set; }

        [Display(Name = "Total")]
        public decimal? Total { get; set; }

        [Display(Name = "Grand Total")]
        public decimal? GrandTotal { get; set; }

    }


}
