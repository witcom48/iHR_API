using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TRKT20
    {
        public cls_TRKT20() { }

        public string company_code { get; set; }
        public string worker_code { get; set; }
        public string year_code { get; set; }
        public string month_no { get; set; }

        public double kt20_rate { get; set; }

        public int emp { get; set; }

        public double salary_month_min { get; set; }
        public double salary_daily_min { get; set; }
        public double salary_month { get; set; }
        public double salary_daily { get; set; }
        public double bonus { get; set; }
        public double overtime { get; set; }
        public double other { get; set; }
        public double summary { get; set; }
        public double more20000 { get; set; }
        public double net { get; set; }

        public string created_by { get; set; }
        public DateTime created_date { get; set; }

        public string modified_by { get; set; }
        public DateTime modified_date { get; set; }

        public bool flag { get; set; }

        //-- Show only
        public string worker_detail { get; set; }
    }
}
