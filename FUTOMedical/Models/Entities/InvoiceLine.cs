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

        }


        public int Id { get; set; }

        public int? InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Sub Total")]
        public decimal SubTotal { get; set; }

      
    }
}
