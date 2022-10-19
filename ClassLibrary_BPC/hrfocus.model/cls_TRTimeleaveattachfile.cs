using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TRTimeleaveattachfile
    {
        public cls_TRTimeleaveattachfile() { }

        public string COMPANY_CODE { get; set; }
        public string TIMELEAVE_DOC { get; set; }

        public string FILE_NO { get; set; }
        public string FILE_NAME { get; set; }

        public string FILE_PATH { get; set; }

        public DateTime CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }

        public DateTime? MODIFIED_DATE { get; set; }
        public string MODIFIED_BY { get; set; }
    }
}
