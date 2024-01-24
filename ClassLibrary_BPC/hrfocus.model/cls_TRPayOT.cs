using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TRPayOT
    {
        public cls_TRPayOT() { }

        public string company_code { get; set; }
        public string worker_code { get; set; }

        public DateTime payot_date { get; set; }

        public int payot_ot1_min { get; set; }
        public int payot_ot15_min { get; set; }
        public int payot_ot2_min { get; set; }
        public int payot_ot3_min { get; set; }

        public double payot_ot1_amount { get; set; }
        public double payot_ot15_amount { get; set; }
        public double payot_ot2_amount { get; set; }
        public double payot_ot3_amount { get; set; }

        public string created_by { get; set; }
        public DateTime created_date { get; set; }

        public string modified_by { get; set; }
        public DateTime modified_date { get; set; }

        public bool flag { get; set; }

        //-- Show only
        public string worker_detail { get; set; }
    }
}
