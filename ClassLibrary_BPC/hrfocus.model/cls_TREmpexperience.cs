using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TREmpexperience
    {
        public cls_TREmpexperience() { }

        public string company_code { get; set; }
        public string worker_code { get; set; }
        public int empexperience_no { get; set; }
        public string empexperience_at { get; set; }
        public string empexperience_position { get; set; }
        public string empexperience_reasonchange { get; set; }        
        public DateTime empexperience_start { get; set; }
        public DateTime empexperience_finish { get; set; }
        public string created_by { get;set; }
        public DateTime created_date { get; set; }  
	    public string modified_by { get;set; }
        public DateTime modified_date { get; set; }

        public bool flag { get; set; }

    }
}
