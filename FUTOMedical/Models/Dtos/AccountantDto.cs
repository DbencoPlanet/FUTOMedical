using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FUTOMedical.Models.Dtos
{
    public class AccountantDto
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

        [Display(Name = "Accountant ID")]
        public string AccountantId { get; set; }

        [Display(Name = "Surname")]
        [Required]
        public string Surname { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string Firstname { get; set; }

        [Display(Name = "Other Name")]
        public string Othernames { get; set; }

        public string Fullname
        {
            get
            {
                return Surname + " " + Firstname + " " + Othernames;
            }
        }


        [Display(Name = "Phone No")]
        public string PhoneNo { get; set; }



        [Display(Name = "Sex")]
        public string Sex { get; set; }

        [Display(Name = "Picture")]
        public string Picture { get; set; }

        [Display(Name = "Address")]
        [Required]
        public string Address { get; set; }

        [Display(Name = "State of Origin")]
        public string StateOfOrigin { get; set; }

        [Display(Name = "Local Government of Origin")]
        public string LocalGov { get; set; }

        public string BloodGroup { get; set; }

        public string Education { get; set; }

        public string Nationality { get; set; }
    }
}