using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TRTimeshift
    {
        public cls_TRTimeshift() { }

        public string company_code { get; set; }
        public string worker_code { get; set; }

        public int timeshift_id { get; set; }
        public string timeshift_doc { get; set; }

        public DateTime timeshift_workdate { get; set; }

        public string timeshift_old { get; set; }
        public string timeshift_new { get; set; }
       
        public string timeshift_note { get; set; }       
        public string reason_code { get; set; }
      
	    public string created_by { get;set; }
        public DateTime created_date { get; set; }
	    public string modified_by { get;set; }
        public DateTime modified_date { get; set; }

        public bool flag { get; set; }

        //-- Show only
        public string worker_detail { get; set; }        
        public string reason_detail { get; set; }
        public string shift_detail { get; set; }

    }
}
