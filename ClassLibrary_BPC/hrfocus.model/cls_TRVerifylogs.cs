using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
  public  class cls_TRVerifylogs
   {
      public cls_TRVerifylogs() { }

        public string company_code { get; set; }
        public string item_code { get; set; }
        public string emptype_id { get; set; }
        public DateTime payitem_date { get; set; }
        public string verify_quantity { get; set; }
        public string verify_amount { get; set; }
        public string verify_note { get; set; }



        public string create_by { get; set; }
        public DateTime create_date { get; set; }

        public string modified_by { get; set; }
        public DateTime modified_date { get; set; }
        public bool flag { get; set; }
    }
}
