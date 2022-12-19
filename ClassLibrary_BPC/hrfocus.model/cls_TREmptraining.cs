using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TREmptraining
    {
        public cls_TREmptraining() { }

        /// <summary>
        /// worker
        /// </summary>
        public int worker_id { get; set; }

        public string worker_card { get; set; }

        public string worker_initial { get; set; }

        public string worker_fname_th { get; set; }
        public string worker_lname_th { get; set; }

        public string worker_fname_en { get; set; }
        public string worker_lname_en { get; set; }

        /// <summary>
        /// 
        /// </summary>

        public string company_code { get; set; }
        public string worker_code { get; set; }

        public int emptraining_no { get; set; }        
        public DateTime emptraining_start { get; set; }
        public DateTime emptraining_finish { get; set; }
        public string emptraining_status { get; set; }
        public double emptraining_hours { get; set; }
        public double emptraining_cost { get; set; }
        public string emptraining_note { get; set; }

        public string institute_code { get; set; }
        public string institute_other { get; set; }
        public string course_code { get; set; }
        public string course_other { get; set; }

        public string created_by { get;set; }
        public DateTime created_date { get; set; }
	    public string modified_by { get;set; }
        public DateTime modified_date { get; set; }

        public bool flag { get; set; }

    }
}
