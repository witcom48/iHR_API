using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TRVerify
    {
        public cls_TRVerify() { }

        public string company_code { get; set; }
        public string item_code { get; set; }
        public string emptype_id { get; set; }
        public DateTime payitem_date { get; set; }
        public string verify_status { get; set; }
        public string worker_code { get; set; }

        

        public string created_by { get; set; }
        public DateTime created_date { get; set; }

        public string modified_by { get; set; }
        public DateTime modified_date { get; set; }
        public bool flag { get; set; }
    }
}
