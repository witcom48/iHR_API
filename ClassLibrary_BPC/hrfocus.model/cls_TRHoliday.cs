﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TRHoliday
    {
        public cls_TRHoliday() { }

        public string company_code { get; set; }
        public DateTime holiday_date { get; set; }
        public string holiday_name_th { get; set; }
        public string holiday_name_en { get; set; }

        public string planholiday_code { get; set; }

        public string holiday_daytype { get; set; }
        public double holiday_payper { get; set; }
       

    }
}
