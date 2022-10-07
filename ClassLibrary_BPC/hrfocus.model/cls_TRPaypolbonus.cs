using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TRPaypolbonus
    {
        public cls_TRPaypolbonus() { }

        public string company_code { get; set; }
        public string worker_code { get; set; }
        public string paypolbonus_code { get; set; }                   
	    public string created_by { get;set; }
        public DateTime created_date { get; set; }	    
        public bool flag { get; set; }

        //-- Show only
        public string worker_detail { get; set; }

    }
}
