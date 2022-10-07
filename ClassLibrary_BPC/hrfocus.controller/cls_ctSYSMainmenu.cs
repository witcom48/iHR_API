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
    public class cls_ctSYSMainmenu
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctSYSMainmenu() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_SYSMainmenu> getData(string condition)
        {
            List<cls_SYSMainmenu> list_model = new List<cls_SYSMainmenu>();
            cls_SYSMainmenu model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("MAINMENU_CODE");
                obj_str.Append(", MAINMENU_DETAIL_TH");
                obj_str.Append(", MAINMENU_DETAIL_EN");
                obj_str.Append(", MAINMENU_ORDER");
                
                obj_str.Append(" FROM HRM_SYS_MAINMENU");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY MAINMENU_ORDER");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_SYSMainmenu();

                    model.mainmenu_code = Convert.ToString(dr["MAINMENU_CODE"]);
                    model.mainmenu_detail_th = Convert.ToString(dr["MAINMENU_DETAIL_TH"]);
                    model.mainmenu_detail_en = Convert.ToString(dr["MAINMENU_DETAIL_EN"]);
                    model.mainmenu_order = Convert.ToInt32(dr["MAINMENU_ORDER"]);
                                                                                                                                         
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Mainmenu.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_SYSMainmenu> getData()
        {
            string strCondition = "";            
            return this.getData(strCondition);
        }
    }
}
