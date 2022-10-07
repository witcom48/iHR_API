using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_SYSAccount
    {
        public cls_SYSAccount() { }

        public string company_code { get; set; }  
        public int account_id { get; set; }
        public string account_usr { get; set; }
        public string account_pwd { get; set; }
        public string account_detail { get; set; }
        public string account_email { get; set; }
        public bool account_emailalert { get; set; }
        public string account_line { get; set; }
        public bool account_linealert { get; set; }
        public bool account_lock { get; set; }
        public int account_faillogin { get; set; }
        public DateTime account_lastlogin { get; set; }        
        public bool account_monthly { get; set; }
        public bool account_daily { get; set; }

        public string polmenu_code { get; set; }

        public string worker_code { get; set; }

        public string modified_by { get; set; }
        public DateTime modified_date { get; set; }
        public bool flag { get; set; }

    }
}
