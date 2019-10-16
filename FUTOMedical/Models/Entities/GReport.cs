using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FUTOMedical.Models.Entities
{
    public class GReport
    {
        public int Id { get; set; }

        [Display(Name = "Patient" )]
        public int? PatientId { get; set; }
        public Patient Patient { get; set; }

        [AllowHtml]
        public string Report { get; set; }

        [Display(Name = "Report Type")]
        public GReportType Type { get; set; }


    }
}