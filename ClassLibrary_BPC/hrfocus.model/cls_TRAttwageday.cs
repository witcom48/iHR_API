using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TRAttwageday
    {
        public cls_TRAttwageday() { }

        public string company_code { get; set; }
        public string worker_code { get; set; }

        public DateTime wageday_date { get; set; }
        public string wageday_daytype { get; set; }

        public double wageday_wagemoney { get; set; }
        public string wageday_wagehhmm { get; set; }

        public double wageday_latemoney { get; set; }
        public string wageday_latehhmm { get; set; }

        public double wageday_leavemoney { get; set; }
        public string wageday_leavehhmm { get; set; }

        public double wageday_absentmoney { get; set; }
        public string wageday_absenthhmm { get; set; }

        public double wageday_ot1money { get; set; }
        public double wageday_ot15money { get; set; }
        public double wageday_ot2money { get; set; }
        public double wageday_ot3money { get; set; }

        public string wageday_ot1hhmm { get; set; }
        public string wageday_ot15hhmm { get; set; }
        public string wageday_ot2hhmm { get; set; }
        public string wageday_ot3hhmm { get; set; }

        public double wageday_allowance { get; set; }

        public string created_by { get;set; }
        public DateTime created_date { get; set; }
	    
	    public string modified_by { get;set; }
        public DateTime modified_date { get; set; }

        public bool flag { get; set; }

        //-- Show only
        public string worker_detail { get; set; }

    }
}
