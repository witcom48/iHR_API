using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TRPayitem
    {
        public cls_TRPayitem() { }

        public string company_code { get; set; }
        public string worker_code { get; set; }
        public string item_code { get; set; }
        public DateTime payitem_date { get; set; }
        public double payitem_amount { get; set; }
        public double payitem_quantity { get; set; }
        public string payitem_paytype { get; set; }
        public string payitem_note { get; set; }
        public string created_by { get;set; }
        public DateTime created_date { get; set; }	    
	    public string modified_by { get;set; }
        public DateTime modified_date { get; set; }
        public bool flag { get; set; }


        //-- Show
        public string item_detail { get; set; }
        public string item_type { get; set; }
        public string worker_detail { get; set; }

        public double amount1 { get; set; }
        public double amount2 { get; set; }
        public double amount { get; set; }

        public double pay_previous { get; set; }
        public double pay_current { get; set; }
        public DateTime worker_hiredate { get; set; }
        public DateTime worker_resigndate { get; set; }
        public bool worker_resignstatus { get; set; }
        public string verify_status { get; set; }

        public string status { get; set; }
        public string create_by { get; set; }
        public DateTime create_date { get; set; }
    }
}
