﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TRWageday
    {
        public cls_TRWageday() { }

        public string company_code { get; set; }
        public string worker_code { get; set; }

        public DateTime wageday_date { get; set; }
        public double wageday_wage { get; set; }

        public double wageday_before_rate { get; set; }
        public double wageday_normal_rate { get; set; }
        public double wageday_break_rate { get; set; }
        public double wageday_after_rate { get; set; }
     
        public int wageday_before_min { get; set; }
        public int wageday_normal_min { get; set; }
        public int wageday_break_min { get; set; }
        public int wageday_after_min { get; set; }

        public double wageday_before_amount { get; set; }
        public double wageday_normal_amount { get; set; }
        public double wageday_break_amount { get; set; }
        public double wageday_after_amount { get; set; }

        public int ot1_min { get; set; }
        public int ot15_min { get; set; }
        public int ot2_min { get; set; }
        public int ot3_min { get; set; }

        public double ot1_amount { get; set; }
        public double ot15_amount { get; set; }
        public double ot2_amount { get; set; }
        public double ot3_amount { get; set; }

        public int late_min { get; set; }
        public double late_amount { get; set; }

        public int leave_min { get; set; }
        public double leave_amount { get; set; }

        public int absent_min { get; set; }
        public double absent_amount { get; set; }

        public double allowance_amount { get; set; }


	    
	    public string modified_by { get;set; }
        public DateTime modified_date { get; set; }

        public bool flag { get; set; }

        //-- Show only
        public string worker_detail { get; set; }

    }
}
