using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary_BPC.hrfocus.model;
using System.Data.SqlClient;
using System.Data;

namespace ClassLibrary_BPC.hrfocus.controller
{
    public class cls_ctSYSItemmenu
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctSYSItemmenu() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_SYSItemmenu> getData(string condition)
        {
            List<cls_SYSItemmenu> list_model = new List<cls_SYSItemmenu>();
            cls_SYSItemmenu model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("SUBMENU_CODE");
                obj_str.Append(", ITEMMENU_CODE");
                obj_str.Append(", ITEMMENU_DETAIL_TH");
                obj_str.Append(", ITEMMENU_DETAIL_EN");
                obj_str.Append(", ITEMMENU_ORDER");

                obj_str.Append(" FROM HRM_SYS_ITEMMENU");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY SUBMENU_CODE, ITEMMENU_ORDER");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_SYSItemmenu();

                    model.submenu_code = Convert.ToString(dr["SUBMENU_CODE"]);
                    model.itemmenu_code = Convert.ToString(dr["ITEMMENU_CODE"]);
                    model.itemmenu_detail_th = Convert.ToString(dr["ITEMMENU_DETAIL_TH"]);
                    model.itemmenu_detail_en = Convert.ToString(dr["ITEMMENU_DETAIL_EN"]);
                    model.itemmenu_order = Convert.ToInt32(dr["ITEMMENU_ORDER"]);
                                                                                                                                         
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Itemmenu.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_SYSItemmenu> getData()
        {
            string strCondition = "";            
            return this.getData(strCondition);
        }
    }
}
