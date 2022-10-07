using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TRTimedoc
    {
        public cls_TRTimedoc() { }

        public string company_code { get; set; }
        public string worker_code { get; set; }
        public DateTime timedoc_workdate { get; set; }
        public string timedoc_doctype { get; set; }

        public string timedoc_docno { get; set; }

        public string timedoc_value1 { get; set; }
        public string timedoc_value2 { get; set; }
        public string timedoc_value3 { get; set; }
        public string timedoc_value4 { get; set; }

        public string timedoc_reasoncode { get; set; }
        public string timedoc_note { get; set; }
      
	    public string created_by { get;set; }
        public DateTime created_date { get; set; }
	    public string modified_by { get;set; }
        public DateTime modified_date { get; set; }

        public bool flag { get; set; }

    }
}
