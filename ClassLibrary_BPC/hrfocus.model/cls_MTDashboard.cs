using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_MTDashboard
    {
        public cls_MTDashboard() { }

        public int worker_code { get; set; }
        public string worker_gender_en { get; set; }
        public string worker_gender_th { get; set; }
        public string dep_name_th { get; set; }
        public string dep_name_en { get; set; }
        public string age { get; set; }

        
    }
}
