using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TRBonusrate
    {
        public cls_TRBonusrate() { }

        public string company_code { get; set; }                
        public string bonus_code { get; set; }
        public double bonusrate_from { get; set; }
        public double bonusrate_to { get; set; }
        public double bonusrate_rate { get; set; }        

    }
}
