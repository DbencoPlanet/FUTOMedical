using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FUTOMedical.Models.Entities
{
    public class LGA
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StatesId { get; set; }
    }
}