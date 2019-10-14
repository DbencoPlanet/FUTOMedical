using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FUTOMedical.Models.Entities
{
    public class Section
    {
        public int Id { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public bool OpdStatus { get; set; }
        public bool SectionOpen { get; set; }
        public bool SectionClose { get; set; }

        public int? PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}