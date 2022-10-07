﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TREmpimages
    {
        public cls_TREmpimages() { }

        public string company_code { get; set; }
        public string worker_code { get; set; }

        public int empimages_no { get; set; }
        public byte[] empimages_images { get; set; }        
        public string created_by { get;set; }
        public DateTime created_date { get; set; }  
	    public string modified_by { get;set; }
        public DateTime modified_date { get; set; }

        public bool flag { get; set; }

    }
}
