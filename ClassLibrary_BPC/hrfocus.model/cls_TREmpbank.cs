using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TREmpbank
    {
        public cls_TREmpbank() { }

        public string company_code { get; set; }
        public string worker_code { get; set; }

        public int empbank_id { get; set; }
        public string empbank_bankcode { get; set; }
        public string empbank_bankaccount { get; set; }
        public double empbank_bankpercent { get; set; }
        public double empbank_cashpercent { get; set; }

        public string empbank_bankname { get; set; }

        public string created_by { get;set; }
        public DateTime created_date { get; set; }  
	    public string modified_by { get;set; }
        public DateTime modified_date { get; set; }

        public bool flag { get; set; }

    }
}
