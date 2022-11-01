using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TRComcard
    {
        public cls_TRComcard() {

            this.comcard_code = "";
            this.card_type = "";
        }

        public int comcard_id { get; set; }
        public string comcard_code { get; set; }
        
        public string combank_bankaccount { get; set; }
        public string card_type { get; set; }

        public DateTime comcard_issue { get; set; }
        public DateTime comcard_expire { get; set; }

        public string company_code { get; set; }
        public string combranch_code { get; set; }

	    public string created_by { get;set; }
        public DateTime created_date { get; set; }
	    

	    public string modified_by { get;set; }
        public DateTime modified_date { get; set; }

        public bool flag { get; set; }

    }
}
