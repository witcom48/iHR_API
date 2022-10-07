using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_SYSAccessdata
    {
        public cls_SYSAccessdata() { }
        public string company_code { get; set; }          
        public string polmenu_code { get; set; }
        public string accessdata_module { get; set; }
        public bool accessdata_new { get; set; }
        public bool accessdata_edit { get; set; }
        public bool accessdata_delete { get; set; }
        public bool accessdata_salary { get; set; }
    }
}
