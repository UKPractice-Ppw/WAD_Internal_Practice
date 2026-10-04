using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternalPracticeApp.Models
{
    public class User
    {
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string user_pass { get; set; }
        public string user_email { get; set; }
        public int state_id { get; set; }
        public int city_id { get; set; }
        public IEnumerable<SelectListItem> States { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Cities { get; set; } = new List<SelectListItem>();
    }
}