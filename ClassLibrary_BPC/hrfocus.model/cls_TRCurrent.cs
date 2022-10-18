using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TRCurrent
    {
        public cls_TRCurrent() { }

        public string worker_id { get; set; }
        public DateTime date { get; set; }
        public string daytype { get; set; }
        public string shift_code { get; set; }
        public string shift_name_th { get; set; }
        public string shift_name_en { get; set; }
    }
}
