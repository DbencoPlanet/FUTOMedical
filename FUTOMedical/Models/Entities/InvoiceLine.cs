using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FUTOMedical.Models.Entities
{
    public class InvoiceLine 
    {
        public InvoiceLine()
        {
            this.Quantity = 0;
            this.Price = 0;
            this.SubTotal = 0;
            this.Amount = 0;
            this.Vat = 0;
            this.VatRate = 0;
            this.Discount = 0;

        }


        public int Id { get; set; }

        public int? InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        public string Name { get; set; }

        [Display(Name = "Vat")]
        public decimal? Vat { get; set; }

        [Display(Name = "Vat Rate")]
        public float? VatRate { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Quantity")]
        public int? Quantity { get; set; }

        [Display(Name = "Price")]
        public decimal? Price { get; set; }

        [Display(Name = "Discount")]
        public decimal? Discount { get; set; }

        [Display(Name = "Amount")]
        public decimal? Amount { get; set; }

        [Display(Name = "Sub Total")]
        public decimal? SubTotal { get; set; }

      
    }
}
