using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TREmpfamily
    {
        public cls_TREmpfamily() { }

        public string company_code { get; set; }
        public string worker_code { get; set; }

        public int empfamily_id { get; set; }
        public string empfamily_code { get; set; }

        public string empfamily_fname_th { get; set; }
        public string empfamily_lname_th { get; set; }
        public string empfamily_fname_en { get; set; }
        public string empfamily_lname_en { get; set; }

        public DateTime empfamily_birthdate { get; set; }
        
        public string family_type { get; set; }
        
	    public string created_by { get;set; }
        public DateTime created_date { get; set; }
	    

	    public string modified_by { get;set; }
        public DateTime modified_date { get; set; }

        public bool flag { get; set; }

    }
}
