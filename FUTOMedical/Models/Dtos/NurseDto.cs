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


        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

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