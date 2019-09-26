using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FUTOMedical.Models.Entities
{
    public class Accountants
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Display(Name = "Accountant ID")]
        public string AccountantId { get; set; }

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

        [Display(Name = " Email Address")]
        [Required]
        public string EmailAddress { get; set; }


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