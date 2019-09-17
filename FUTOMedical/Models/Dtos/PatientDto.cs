using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FUTOMedical.Models.Entities;

namespace FUTOMedical.Models.Dtos
{
    public class PatientDto
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


        public string Photo { get; set; }

        public string Nationality { get; set; }

        [Display(Name = "State of Origin")]
        public string StateOfOrigin { get; set; }


        [Display(Name = "Local Government of Origin")]
        public string LocalGov { get; set; }

        [Display(Name = "Surname")]
        [Required(ErrorMessage = "Surname field is required")]
        public string Surname { get; set; }


        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name field is required")]
        public string Firstname { get; set; }

        [Display(Name = "Other Name")]
        public string Othernames { get; set; }


        public string Fullname
        {
            get
            {
                return  Surname + " " + Firstname + " " + Othernames;
            }
        }

        [Display(Name = "Phone No")]
        public string PhoneNo { get; set; }

        [Display(Name = "Date Of Birth")]
        public DateTime? DOB { get; set; }

        //[Display(Name = "JAMB Reg No")]
        //public string JambReg { get; set; }

        //[Display(Name = "Matirc No")]
        //public string MatricNo { get; set; }

        //[Display(Name = "School")]
        //public int? SchoolId { get; set; }

        //[Display(Name = "School")]
        //public School School { get; set; }

        //[Display(Name = "Department")]
        //public int? StudentDeptId { get; set; }

        //[Display(Name = "Department")]
        //public StudentDept StudentDept { get; set; }

        [Display(Name = "Department")]
        public string Department { get; set; }

        [Display(Name = "Permanent Address")]
        public string PermanentAddress { get; set; }

        [Display(Name = "Parent/Guardian Name")]
        public string ParentGuardianName { get; set; }

        [Display(Name = "Parent/Guardian Phone")]
        public string ParentGuardianPhone { get; set; }

        [Display(Name = "Next Of Kin")]
        public string NextOfKin { get; set; }

        [Display(Name = "Next Of Kin Address")]
        public string NextOfKinAddress { get; set; }

        [Display(Name = "Next Of Kin Phone")]
        public string NextOfKinPhone { get; set; }

        //[Display(Name = "Date Of Entry")]
        //public DateTime DateOfEntry { get; set; }

        //[UIHint("Enum")]
        //[Display(Name = "Mode Of Entry")]
        //public ModeOfEntry ModeOfEntry { get; set; }

        //[UIHint("Enum")]
        //[Display(Name = "Mode Of Study")]
        //public ModeOfStudy ModeOfStudy { get; set; }

        [Display(Name = "Resident Address")]
        public string Address { get; set; }

        [Display(Name = "Place Of Birth")]
        public string PlaceOfBirth { get; set; }


        [Display(Name = "Resident Address")]
        public string ResidentAddress { get; set; }

        public string Religion { get; set; }

        public string CardNumber { get; set; }

        public string FolderNumber { get; set; }

        public string BloodGroup { get; set; }


        [Display(Name = "Sex")]
        [Required(ErrorMessage = "Sex field is required")]
        public string Sex { get; set; }



    }
}