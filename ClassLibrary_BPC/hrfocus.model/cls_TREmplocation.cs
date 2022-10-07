﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TREmplocation
    {
        public cls_TREmplocation() { }

        public string company_code { get; set; }
        public string worker_code { get; set; }
        public string location_code { get; set; }
        public DateTime emplocation_startdate { get; set; }
        public DateTime emplocation_enddate { get; set; }
        public string emplocation_note { get; set; }      
	    public string created_by { get;set; }
        public DateTime created_date { get; set; }
	    public string modified_by { get;set; }
        public DateTime modified_date { get; set; }

        public bool flag { get; set; }

        //-- Show only
        public string worker_initial { get; set; }
        public string worker_fname_th { get; set; }
        public string worker_lname_th { get; set; }
        public string worker_fname_en { get; set; }
        public string worker_lname_en { get; set; }

        public string location_name_th { get; set; }
        public string location_name_en { get; set; }

    }
}
