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
    public class  cls_ctTREmpEmpdeplevel01
    {
    string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTREmpEmpdeplevel01() { }

        public string getMessage() { return this.Message; }

        private string FormatDateDB = "MM/dd/yyyy";

        public void dispose()
        {
            Obj_conn.doClose();
        }


        private List<cls_TREmpEmpdeplevel01Dash> getData(string condition)
        {
            List<cls_TREmpEmpdeplevel01Dash> list_model = new List<cls_TREmpEmpdeplevel01Dash>();
            cls_TREmpEmpdeplevel01Dash model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("COUNT(WORKER_CODE)as WORKER_CODE");
                obj_str.Append(", RM_MT_DEP.DEP_NAME_EN");
                obj_str.Append(", HRM_MT_DEP.DEP_NAME_TH");
                obj_str.Append("from HRM_TR_EMPDEP");
                obj_str.Append(" inner join HRM_MT_DEP on HRM_TR_EMPDEP.EMPDEP_LEVEL01 = HRM_MT_DEP.DEP_CODE");
                obj_str.Append(" where HRM_TR_EMPDEP.COMPANY_CODE = 'APT'");

                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append("GROUP BY HRM_MT_DEP.DEP_NAME_EN,HRM_MT_DEP.DEP_NAME_TH");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TREmpEmpdeplevel01Dash();


                    model.WORKER_CODE = Convert.ToInt32(dr["WORKER_CODE"]);
                    model.DEP_NAME_EN = dr["DEP_NAME_EN"].ToString();
                    model.DEP_NAME_EN = dr["DEP_NAME_TH"].ToString();
                    

                    list_model.Add(model);
                }

            }
            catch (Exception ex)
            {
                Message = "ERROR::(Worker.getData)" + ex.ToString();
            }

            return list_model;
        
        }
    }
}
