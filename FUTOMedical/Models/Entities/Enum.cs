using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FUTOMedical.Models.Entities
{


    public enum GReportType
    {

        [Description("Birth Report")]
        [Display(Name = "Birth Report")]
        Birth = 1,
        [Description("Death Report")]
        [Display(Name = "Death Report")]
        Death = 2,
        [Description("Operation Report")]
        [Display(Name = "Operation Report")]
        Operation = 3


    }


    public enum BloodStatus
    {

        [Description("In Stock")]
        [Display(Name = "In Stock")]
        InStock = 1,
        [Description("In Stock")]
        [Display(Name = "Out Of Stock")]
        OutOfStock = 2


    }


    public enum MedicineStatus
    {
        [Description("In Stock")]
        [Display(Name = "In Stock")]
        InStock = 1,
        [Description("In Stock")]
        [Display(Name = "Out Of Stock")]
        OutOfStock = 2
    }

    public enum ReportStatus
    {

        [Description("None")]
        None = 0,
        [Description("Admit")]
        Admit = 1,
        [Description("Dont Admit")]
        DontAdmit = 2,
        [Description("Test")]
        Test = 3,
        [Description("Operation")]
        Operation = 4


    }

    public enum AdmissionStatus
    {

        [Description("Active")]
        Active = 1,
        [Description("NotActive")]
        NotActive = 2,
        [Description("Discharged")]
        Discharged = 3

    }

    public enum ModeOfStudy
    {
        [Description("None")]
        [Display(Name = "None")]
        NONE = 0,

        [Description("Regular")]
        [Display(Name = "Regular")]
        Regular = 1,

        [Description("Weekend")]
        [Display(Name = "Weekend")]
        Weekend = 2,

        [Description("PGD")]
        [Display(Name = "PGD")]
        PGD = 3

    }

    public enum ModeOfEntry
    {
        [Description("None")]
        [Display(Name = "None")]
        NONE = 0,

        [Description("UTME")]
        [Display(Name = "UTME")]
        UTME = 1,

        [Description("Direct Entry")]
        [Display(Name = "Direct Entry")]
        DirectEntry = 2,

        [Description("Pre-degree")]
        [Display(Name = "Pre-degree")]
        Predegree = 3

    }
}