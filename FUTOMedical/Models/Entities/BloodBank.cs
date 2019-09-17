using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FUTOMedical.Models.Entities
{
    public class BloodBank
    {
        public int Id { get; set; }
        public string Age { get; set; }

        public string DonorName { get; set; }

        public string Sex { get; set; }

        public string BloodGroup {get;set;}

        [Display(Name="Last Donation Date")]
        public DateTime LastDonationDate { get; set; }
    }
}
