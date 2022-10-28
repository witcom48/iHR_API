using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TADashboard
    {
        public cls_TADashboard() { }
        public int worker_code { get; set; }
        public int timeleave_actualday { get; set; }
        public string dep_name_th { get; set; }
        public string dep_name_en { get; set; }
        public int late { get; set; }
    }
}
