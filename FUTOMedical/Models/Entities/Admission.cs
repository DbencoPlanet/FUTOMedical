using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FUTOMedical.Models.Entities
{
    public class Admission
    {
        public int Id { get; set; }
        public string RoomNo { get; set; }
        public string BedNo { get; set; }
        public DateTime DateAdmitted { get; set; }
        public DateTime? DateDischarged { get; set; }

        public int? PatientId { get; set; }
        public Patient Patient { get; set; }

        public int? OPDId { get; set; }
        public OPD OPD { get; set; }

        public AdmissionStatus Status { get; set; }
    }
}