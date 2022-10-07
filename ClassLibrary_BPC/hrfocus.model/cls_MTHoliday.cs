﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_MTHoliday
    {
        public cls_MTHoliday() { }

        public string company_code { get; set; }

        public int holiday_id { get; set; }
        public DateTime holiday_date { get; set; }
        public string holiday_name_th { get; set; }
        public string holiday_name_en { get; set; }

        public string year_code { get; set; }

        public string holiday_daytype { get; set; }
        public double holiday_payper { get; set; }
        
	    public string created_by { get;set; }
        public DateTime created_date { get; set; }
	    

	    public string modified_by { get;set; }
        public DateTime modified_date { get; set; }

        public bool flag { get; set; }

    }
}
