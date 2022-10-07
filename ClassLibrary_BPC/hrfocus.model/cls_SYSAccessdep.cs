using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_SYSAccessdep
    {
        public cls_SYSAccessdep() { }
        public string company_code { get; set; }          
        public string account_usr { get; set; }
        public string accessdep_level { get; set; }
        public string accessdep_dep { get; set; }   
    }
}
