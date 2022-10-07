using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TREmpawpt
    {
        public cls_TREmpawpt() { }

        public string company_code { get; set; }
        public string worker_code { get; set; }
        
        public int empawpt_no { get; set; }
        public DateTime empawpt_start { get; set; }
        public DateTime empawpt_finish { get; set; }
        public string empawpt_type { get; set; }
        public string empawpt_location { get; set; }
        public string empawpt_reason { get; set; }
        public string empawpt_note { get; set; }        
      
	    public string created_by { get;set; }
        public DateTime created_date { get; set; }
	    public string modified_by { get;set; }
        public DateTime modified_date { get; set; }

        public bool flag { get; set; }
        
    }
}
