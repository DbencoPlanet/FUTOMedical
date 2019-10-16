using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FUTOMedical.Models.Entities
{
    public class MedicineCategory
    {
        public int Id { get; set; }

        [Display(Name="Category Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [AllowHtml]
        public string Description { get; set; }

    }
}
