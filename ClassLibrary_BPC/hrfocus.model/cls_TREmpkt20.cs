using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TREmpkt20
    {
        public cls_TREmpkt20() { }

        public string company_code { get; set; }
        public string worker_code { get; set; }
        public string year_code { get; set; }
        public double empkt20_rate { get; set; }
        public string created_by { get; set; }
        public DateTime created_date { get; set; }        

    }
}
