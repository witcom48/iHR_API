using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TREmpcard
    {
        public cls_TREmpcard() { this.empcard_code = ""; }

        public string company_code { get; set; }
        public string worker_code { get; set; }

        public int empcard_id { get; set; }
        public string empcard_code { get; set; }

        public string card_type { get; set; }

        public DateTime empcard_issue { get; set; }
        public DateTime empcard_expire { get; set; }

        
	    public string created_by { get;set; }
        public DateTime created_date { get; set; }
	    

	    public string modified_by { get;set; }
        public DateTime modified_date { get; set; }

        public bool flag { get; set; }

    }
}
