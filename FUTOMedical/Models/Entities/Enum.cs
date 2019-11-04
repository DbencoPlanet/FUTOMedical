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


    public enum PaymentStatus
    {

        [Description("Paid")]
        [Display(Name = "Paid")]
        Paid = 1,
        [Description("UnPaid")]
        [Display(Name = "UnPaid")]
        UnPaid = 2


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
        Operation = 4,
        [Description("AttendedTo")]
        AttendedTo = 5,


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

   
}