using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TREmpdep
    {
        public cls_TREmpdep() {

            this.company_code = "";
            this.worker_code = "";

            this.empdep_level01 = "";
            this.empdep_level02 = "";
            this.empdep_level03 = "";
            this.empdep_level04 = "";
            this.empdep_level05 = "";
        
        }
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

        public string company_code { get; set; }
        public string worker_code { get; set; }
        public int empdep_id { get; set; }
        public DateTime empdep_date { get; set; }
        public string empdep_level01 { get; set; }
        public string empdep_level02 { get; set; }
        public string empdep_level03 { get; set; }
        public string empdep_level04 { get; set; }
        public string empdep_level05 { get; set; }
        public string empdep_level06 { get; set; }
        public string empdep_level07 { get; set; }
        public string empdep_level08 { get; set; }
        public string empdep_level09 { get; set; }
        public string empdep_level10 { get; set; }
        public string empdep_reason { get; set; }

        public string created_by { get;set; }
        public DateTime created_date { get; set; }
	    

	    public string modified_by { get;set; }
        public DateTime modified_date { get; set; }

        public bool flag { get; set; }

    }
}
