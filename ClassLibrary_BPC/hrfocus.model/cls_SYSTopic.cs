using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_SYSTopic
    {
        public cls_SYSTopic() { }

        public string topic_id { get; set; }
        public string detail { get; set; }
        public string status  { get; set; }
        public string create_by { get; set; }
        public DateTime create_date { get; set; }
        public string modified_by { get; set; }
        public DateTime modified_date { get; set; }
    }
}
