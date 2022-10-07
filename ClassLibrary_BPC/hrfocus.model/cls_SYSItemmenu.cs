using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_SYSItemmenu
    {
        public cls_SYSItemmenu() { }

        public string submenu_code { get; set; }
        public string itemmenu_code { get; set; }
        public string itemmenu_detail_th { get; set; }
        public string itemmenu_detail_en { get; set; }
        public int itemmenu_order { get; set; }

    }
}
