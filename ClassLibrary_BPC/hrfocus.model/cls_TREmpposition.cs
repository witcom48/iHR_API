using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TREmpposition
    {
        public cls_TREmpposition() { }
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
        public int empposition_id { get; set; }
        public DateTime empposition_date { get; set; }
        public string empposition_position { get; set; }        
        public string empposition_reason { get; set; }
        public string created_by { get;set; }
        public DateTime created_date { get; set; }  
	    public string modified_by { get;set; }
        public DateTime modified_date { get; set; }

        public bool flag { get; set; }

    }
}
