using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.model
{
   public class cls_FNTComparefixamount
    {
       public cls_FNTComparefixamount() { }
       public string EmpID { get; set; }
       public string EmpName { get; set; }
       public double Amount { get; set; }
       public double AmountOld { get; set; }
       public DateTime Filldate { get; set; }
       public DateTime Resigndate { get; set; }
 
    }
}
