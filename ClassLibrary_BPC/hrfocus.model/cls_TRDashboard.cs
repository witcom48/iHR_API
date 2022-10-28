using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TRDashboard
    {
        public cls_TRDashboard() { }
        public int worker_code { get; set; }
       
        public string item_name_th { get; set; }
        public string item_name_en { get; set; }
        public string item_code { get; set; }
        public int before_min { get; set; }
        public int normal_min { get; set; }
        public int after_min { get; set; }
        public string dep_name_th { get; set; }
        public string dep_name_en { get; set; }
        public string empposition_position { get; set; }
        public string position_name_th { get; set; }
        public string position_name_en { get; set; }
        public int amount { get; set; }

       // public Newtonsoft.Json.Linq.JToken worker_code { get; set; }
    }
}
