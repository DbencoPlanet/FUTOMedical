using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FUTOMedical.Models.Entities
{
    public class Payment
    {
        public int Id { get; set; }

        [Display(Name= "Patient Name")]
        public int? PatientId {get;set;}
        public Patient Patient { get; set; }

        [Display(Name = "Patient Invoice")]
        public int? InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

    }
}