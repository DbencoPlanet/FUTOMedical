using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FUTOMedical.Models.Entities;

namespace FUTOMedical.Models.Dtos
{
    public class DoctorDto
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }


        [Display(Name = "Doctor ID")]
        public string DoctorId { get; set; }

        [Display(Name = "Surname")]
        [Required]
        public string SurName { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Other Name")]
        public string OtherName { get; set; }

        public string Fullname
        {
            get
            {
                return SurName + " " + FirstName + " " + OtherName;
            }
        }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = " Designation")]
        public string Designation { get; set; }

        [Display(Name = " Department")]
        public int? DeptId { get; set; }
        public Department Department { get; set; }

        [Display(Name = "Phone No")]
        public string PhoneNo { get; set; }

        [Display(Name = "MobileNo")]
        [Required]
        public string MobileNo { get; set; }

        [Display(Name = "Biography")]
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
        [Required]
        public string Address { get; set; }

      
    }
}