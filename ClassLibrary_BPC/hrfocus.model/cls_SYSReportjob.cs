using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_SYSReportjob
    {
        public cls_SYSReportjob() { }
        public string company_code { get; set; }        
        public int reportjob_id { get; set; }
        public string reportjob_ref { get; set; }
        public string reportjob_type { get; set; }
        public string reportjob_status { get; set; }
        public DateTime reportjob_fromdate { get; set; }
        public DateTime reportjob_todate { get; set; }
        public DateTime reportjob_paydate { get; set; }       
        public string created_by { get;set; }
        public DateTime created_date { get; set; }  
    }
}
