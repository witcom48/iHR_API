﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TRPaytran
    {
        public cls_TRPaytran() { }

        public string company_code { get; set; }
        public string worker_code { get; set; }

        public DateTime paytran_date { get; set; }

        public double paytran_ssoemp { get; set; }
        public double paytran_ssocom { get; set; }
        public double paytran_ssorateemp { get; set; }
        public double paytran_ssoratecom { get; set; }

        public double paytran_pfemp { get; set; }
        public double paytran_pfcom { get; set; }

        public double paytran_income_401 { get; set; }
        public double paytran_deduct_401 { get; set; }
        public double paytran_tax_401 { get; set; }

        public double paytran_income_4012 { get; set; }
        public double paytran_deduct_4012 { get; set; }
        public double paytran_tax_4012 { get; set; }

        public double paytran_income_4013 { get; set; }
        public double paytran_deduct_4013 { get; set; }
        public double paytran_tax_4013 { get; set; }

        public double paytran_income_402I { get; set; }
        public double paytran_deduct_402I { get; set; }
        public double paytran_tax_402I { get; set; }

        public double paytran_income_402O { get; set; }
        public double paytran_deduct_402O { get; set; }
        public double paytran_tax_402O { get; set; }

        public double paytran_income_notax { get; set; }
        public double paytran_deduct_notax { get; set; }

        public double paytran_income_total { get; set; }
        public double paytran_deduct_total { get; set; }

        public double paytran_netpay_b { get; set; }
        public double paytran_netpay_c { get; set; }

        public string created_by { get;set; }
        public DateTime created_date { get; set; }
	    
	    public string modified_by { get;set; }
        public DateTime modified_date { get; set; }

        public bool flag { get; set; }

        //-- Show only
        public string worker_detail { get; set; }
        public double paytran_salary { get; set; }
        public string position { get; set; }
        public string level01 { get; set; }
        public string level02 { get; set; }
        public double A01 { get; set; }
        public double A02 { get; set; }
        public double AL03 { get; set; }
        public double BO01 { get; set; }
        public double DG01 { get; set; }
        public double GA01 { get; set; }
        public double OT01 { get; set; }
        public double SA01 { get; set; }
        public double SA02 { get; set; }
        public double LV01 { get; set; }
        public double SLF1 { get; set; }
        public DateTime employment_date { get; set; }
        public string bankaccount { get; set; }
        public string type { get; set; }





    }
}
