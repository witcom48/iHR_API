using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TREmpprovident
    {
        public cls_TREmpprovident() { }

        /// worker
        public int worker_id { get; set; }

        public string worker_card { get; set; }

        public string worker_initial { get; set; }

        public string worker_fname_th { get; set; }
        public string worker_lname_th { get; set; }

        public string worker_fname_en { get; set; }
        public string worker_lname_en { get; set; }

        /// <worker>

        public string company_code { get; set; }
        public string worker_code { get; set; }

        public string provident_code { get; set; }

        public string empprovident_card { get; set; }

        public DateTime empprovident_entry { get; set; }
        public DateTime empprovident_start { get; set; }
        public DateTime empprovident_end { get; set; }

        public string created_by { get;set; }
        public DateTime created_date { get; set; }
	    public string modified_by { get;set; }
        public DateTime modified_date { get; set; }

        public bool flag { get; set; }

    }
}
