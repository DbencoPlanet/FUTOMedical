using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FUTOMedical.Models.Entities
{
    public class Medicine
    {
        public int Id { get; set; }

        [Display(Name="Medicine Name")]
        public string Name { get; set; }

        public int? MedicineCategoryId { get; set; }
        public MedicineCategory MedicineCategory { get; set; }

        [Display(Name = "Description")]
        [AllowHtml]
        public string Description { get; set; }

        public decimal Price { get; set; }

        [Display(Name = "Manufactured By")]
        public string ManufacturedBy { get; set; }

        public MedicineStatus Status { get; set; }

        
    }
}
