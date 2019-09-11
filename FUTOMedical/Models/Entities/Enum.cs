﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FUTOMedical.Models.Entities
{
    public enum ReportStatus
    {

        [Description("Admit")]
        Admit = 1,
        [Description("Dont Admit")]
        DontAdmit = 2,
        [Description("None")]
        None = 3,
    }

    public enum AdmissionStatus
    {

        [Description("Active")]
        Active = 1,
        [Description("NotActive")]
        NotActive = 2,

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