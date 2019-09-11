using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FUTOMedical.Models.Entities;

namespace FUTOMedical.Models.Dtos
{
    public class NurseDto
    {
      
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Display(Name = "Nurse ID")]
        public string NurseId { get; set; }

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

        [Display(Name = " Email Address")]
        [Required]
        public string EmailAddress { get; set; }


        [Display(Name = "Phone No")]
        public string PhoneNo { get; set; }



        [Display(Name = "Sex")]
        public string Sex { get; set; }

        [Display(Name = "Picture")]
        public string ProfilePicture { get; set; }

        [Display(Name = "Address")]
        [Required]
        public string Address { get; set; }

    }
}