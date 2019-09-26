using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FUTOMedical.Models.Entities
{
    public class Doctor
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }


        [Display(Name = "Doctor ID")]
        public string DoctorId { get; set; }

        [Display(Name = "Surname")]
        [Required]
        public string Surname { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string Firstname { get; set; }

        [Display(Name = "Other Names")]
        public string Othernames { get; set; }

        public string Fullname
        {
            get
            {
                return Surname + " " + Firstname + " " + Othernames;
            }
        }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = " Designation")]
        public string Designation { get; set; }

        [Display(Name = " Department")]
        public int? DeparmentId { get; set; }
        public Department Department { get; set; }

        [Display(Name = "Phone No")]
        public string PhoneNo { get; set; }

        [Display(Name = "MobileNo")]
        [Required]
        public string MobileNo { get; set; }

        [Display(Name = "Biography")]
        [AllowHtml]
        public string Biography { get; set; }

        [Display(Name = "Sex")]
        public string Sex { get; set; }

        [Display(Name = "Blood Group")]
        public string BloodGroup { get; set; }

        [Display(Name = "Education/Degree")]
        public string Education { get; set; }


        [Display(Name = "Picture")]
        public string Picture { get; set; }

        [Display(Name = "Specialist")]
        public string Specialist { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "State of Origin")]
        public string StateOfOrigin { get; set; }

        [Display(Name = "Local Government of Origin")]
        public string LocalGov { get; set; }

        [Display(Name = "Nationality")]
        public string Nationality { get; set; }



    }
}