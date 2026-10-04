using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternalPracticeApp.Models
{
    public class City
    {
        public int city_id { get; set; }
        public int state_id { get; set; }
        public IEnumerable<SelectListItem> states { get; set; } = new List<SelectListItem>();
        public string city_name { get; set; }
    }
}