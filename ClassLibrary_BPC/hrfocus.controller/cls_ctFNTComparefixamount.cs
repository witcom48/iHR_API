using ClassLibrary_BPC.hrfocus.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.controller
{
    public class cls_ctFNTComparefixamount
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctFNTComparefixamount() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_FNTComparefixamount> getData(string condition, DateTime datefrom, DateTime dateto, string com, string item)
        {
            List<cls_FNTComparefixamount> list_model = new List<cls_FNTComparefixamount>();
            cls_FNTComparefixamount model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append(" EmpID");
                obj_str.Append(", EmpName");
                obj_str.Append(", Amount");
                obj_str.Append(", AmountOld");
                obj_str.Append(", Filldate");
                obj_str.Append(", Resigndate");
                obj_str.Append(" FROM HRM_FNT_COMPAREFIX_AMOUNT('" + datefrom.ToString("MM/dd/yyyy") + "', '" + dateto.ToString("MM/dd/yyyy") + "', '" + com + "', '" + item + "')");


                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_FNTComparefixamount();

                    model.EmpID = Convert.ToString(dr["EmpID"]);
                    model.EmpName = Convert.ToString(dr["EmpName"]);

                    model.Amount = Convert.ToDouble(dr["Amount"]);
                    model.AmountOld = Convert.ToDouble(dr["AmountOld"]);


                    model.Filldate = Convert.ToDateTime(dr["Filldate"]);
                    model.Resigndate = Convert.ToDateTime(dr["Resigndate"]);
                    //
                   
                    list_model.Add(model);
                }

            }
            catch (Exception ex)
            {
                Message = "ERROR::(Compareamount.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_FNTComparefixamount> getDataByFillter(string id, string user, DateTime datefrom, DateTime dateto, string com, string item)
        {
            string strCondition = "";
            if (!id.Equals(""))
                strCondition += " AND EmpID='" + id + "'";
            if (!user.Equals(""))
                strCondition += " AND EmpName='" + user + "'";


            strCondition += " AND (HRM_FNT_COMPAREFIX_AMOUNT BETWEEN '" + datefrom.ToString("MM/dd/yyyy") + "' AND '" + dateto.ToString("MM/dd/yyyy") + " AND EmpID='" + com + "'" + " AND EmpID='" + item + "'" + "')";

            return this.getData(strCondition, datefrom, dateto, com, item);
        }
    }
}

