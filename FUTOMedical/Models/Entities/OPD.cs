using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FUTOMedical.Models.Entities
{
    public class OPD
    {
        public int Id { get; set; }
        public string BP { get; set; }
        public string Temperature { get; set; }
        public string Weight { get; set; }

        public int? PatientId { get; set; }
        public Patient Patient { get; set; }

        public int? NurseId { get; set; }
        public Nurse Nurse { get; set; }
    }
}