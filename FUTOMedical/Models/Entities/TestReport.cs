using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FUTOMedical.Models.Entities
{
    public class TestReport
    {
        public int Id { get; set; }

        public ReportStatus Status { get; set; }

        public int? DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public string FolderNumber { get; set; }

        public int? OPDId { get; set; }
        public OPD OPD { get; set; }

        public int? NurseId { get; set; }
        public Nurse Nurse { get; set; }

        public int? PatientId { get; set; }
        public Patient Patient { get; set; }

        public int? LaboratoristsId { get; set; }
        public Laboratorists Laboratorists { get; set; }
    }
}