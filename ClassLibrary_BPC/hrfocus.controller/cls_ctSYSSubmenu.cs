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
    public class cls_ctSYSSubmenu
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctSYSSubmenu() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_SYSSubmenu> getData(string condition)
        {
            List<cls_SYSSubmenu> list_model = new List<cls_SYSSubmenu>();
            cls_SYSSubmenu model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("MAINMENU_CODE");
                obj_str.Append(", SUBMENU_CODE");
                obj_str.Append(", SUBMENU_DETAIL_TH");
                obj_str.Append(", SUBMENU_DETAIL_EN");
                obj_str.Append(", SUBMENU_ORDER");

                obj_str.Append(" FROM HRM_SYS_SUBMENU");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY MAINMENU_CODE, SUBMENU_ORDER");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_SYSSubmenu();

                    model.mainmenu_code = Convert.ToString(dr["MAINMENU_CODE"]);
                    model.submenu_code = Convert.ToString(dr["SUBMENU_CODE"]);
                    model.submenu_detail_th = Convert.ToString(dr["SUBMENU_DETAIL_TH"]);
                    model.submenu_detail_en = Convert.ToString(dr["SUBMENU_DETAIL_EN"]);
                    model.submenu_order = Convert.ToInt32(dr["SUBMENU_ORDER"]);
                                                                                                                                         
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Submenu.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_SYSSubmenu> getData()
        {
            string strCondition = "";            
            return this.getData(strCondition);
        }
    }
}
