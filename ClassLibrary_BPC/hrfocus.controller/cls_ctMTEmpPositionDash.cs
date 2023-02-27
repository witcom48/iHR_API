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
    public class cls_ctMTEmpPositionDash
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctMTEmpPositionDash() { }

        public string getMessage() { return this.Message; }

        private string FormatDateDB = "MM/dd/yyyy";

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TREmpPositionDash> getData(string condition)
        {
            List<cls_TREmpPositionDash> list_model = new List<cls_TREmpPositionDash>();
            cls_TREmpPositionDash model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("COUNT(WORKER_CODE)as WORKER_CODE");
                obj_str.Append(", EMPPOSITION_POSITION");
                obj_str.Append(", HRM_MT_POSITION.POSITION_NAME_TH");
                obj_str.Append(", HRM_MT_POSITION.POSITION_NAME_EN");
                obj_str.Append(" FROM HRM_TR_EMPPOSITION");
                obj_str.Append(" INNER JOIN HRM_MT_POSITION ON HRM_TR_EMPPOSITION.COMPANY_CODE=HRM_MT_POSITION.COMPANY_CODE");
                obj_str.Append(" AND HRM_TR_EMPPOSITION.EMPPOSITION_POSITION=HRM_MT_POSITION.POSITION_CODE");

                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" GROUP BY EMPPOSITION_POSITION, HRM_MT_POSITION.POSITION_NAME_TH, HRM_MT_POSITION.POSITION_NAME_EN");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TREmpPositionDash();


                    model.worker_code = Convert.ToInt32(dr["WORKER_CODE"]);
                    model.empposition_position = dr["EMPPOSITION_POSITION"].ToString();
                    model.position_name_en = dr["POSITION_NAME_EN"].ToString();
                    model.position_name_th = dr["POSITION_NAME_TH"].ToString();
                    

                    list_model.Add(model);
                }

            }
            catch (Exception ex)
            {
                Message = "ERROR::(Worker.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TREmpPositionDash> getDataByFillter(string fromdate, string todate)
        {
            string strCondition = "";

            strCondition += " AND (EMPPOSITION_DATE BETWEEN '" + fromdate + "' AND '" + todate+ "' )";

            return this.getData(strCondition);
        }
    }
}
