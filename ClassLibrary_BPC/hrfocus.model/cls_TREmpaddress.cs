﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TREmpaddress
    {
        public cls_TREmpaddress() { }

        /// <summary>
        /// worker
        /// </summary>



        public int worker_id { get; set; }

        public string worker_card { get; set; }

        public string worker_initial { get; set; }

        public string worker_fname_th { get; set; }
        public string worker_lname_th { get; set; }

        public string worker_fname_en { get; set; }
        public string worker_lname_en { get; set; }

        /// <summary>
        /// 
        /// </summary>

        public string company_code { get; set; }
        public string worker_code { get; set; }

        public string empaddress_type { get; set; }
        public string empaddress_no { get; set; }

        public string empaddress_moo { get; set; }
        public string empaddress_soi { get; set; }
        public string empaddress_road { get; set; }
        public string empaddress_tambon { get; set; }
        public string empaddress_amphur { get; set; }
        public string empaddress_zipcode { get; set; }
        public string empaddress_tel { get; set; }
        public string empaddress_email { get; set; }
        public string empaddress_line { get; set; }
        public string empaddress_facebook { get; set; }
        public string province_code { get; set; }
                
	    public string created_by { get;set; }
        public DateTime created_date { get; set; }
	    public string modified_by { get;set; }
        public DateTime modified_date { get; set; }

        public bool flag { get; set; }

    }
}
