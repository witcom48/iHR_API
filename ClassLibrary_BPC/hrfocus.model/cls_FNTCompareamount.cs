using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
   public class cls_FNTCompareamount
  {
       public cls_FNTCompareamount() { }
       public int EmpID { get; set; }
       public string EmpName { get; set; }
       public string Amount { get; set; }
       public string AmountOld { get; set; }
       public DateTime Filldate { get; set; }
       public DateTime Resigndate { get; set; }

 
    }
}
