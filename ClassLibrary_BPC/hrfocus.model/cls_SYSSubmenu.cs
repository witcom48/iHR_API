using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_SYSSubmenu
    {
        public cls_SYSSubmenu() { }

        public string mainmenu_code { get; set; }
        public string submenu_code { get; set; }
        public string submenu_detail_th { get; set; }
        public string submenu_detail_en { get; set; }
        public int submenu_order { get; set; }

    }
}
