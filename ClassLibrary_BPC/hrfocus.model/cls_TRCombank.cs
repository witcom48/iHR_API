﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TRCombank
    {
        public cls_TRCombank() { }
        //public string panytran_ssocom { get; }

        public string company_code { get; set; }
      
        public int combank_id { get; set; }
        public string combank_bankcode { get; set; }
        public string combank_bankaccount { get; set; }
        public string name_detail { get; set; }      

        
        public string created_by { get;set; }
        public DateTime created_date { get; set; }  
	    public string modified_by { get;set; }
        public DateTime modified_date { get; set; }

        public bool flag { get; set; }

    }
}
