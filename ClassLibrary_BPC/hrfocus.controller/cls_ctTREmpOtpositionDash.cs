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
   public class cls_ctTREmpOtpositionDash
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTREmpOtpositionDash() { }

        public string getMessage() { return this.Message; }

        private string FormatDateDB = "MM/dd/yyyy";

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TREmpOtpositionDash> getData(string condition)
        {
            List<cls_TREmpOtpositionDash> list_model = new List<cls_TREmpOtpositionDash>();
            cls_TREmpOtpositionDash model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("SUM(HRM_TR_TIMECARD.TIMECARD_BEFORE_MIN) as BEFORE_MIN");
                obj_str.Append(", SUM(CASE WHEN (HRM_TR_TIMECARD.TIMECARD_DAYTYPE) = 'O' THEN HRM_TR_TIMECARD.TIMECARD_WORK1_MIN else null END) AS NORMAL_MIN");
                obj_str.Append(", SUM(HRM_TR_TIMECARD.TIMECARD_AFTER_MIN) as AFTER_MIN");
                obj_str.Append(", HRM_TR_EMPPOSITION.EMPPOSITION_POSITION");
                obj_str.Append(", HRM_MT_POSITION.POSITION_NAME_TH");
                obj_str.Append(", HRM_MT_POSITION.POSITION_NAME_EN");
                obj_str.Append(" FROM HRM_TR_TIMECARD");
                obj_str.Append(" INNER JOIN HRM_TR_EMPPOSITION ON HRM_TR_TIMECARD.WORKER_CODE = HRM_TR_EMPPOSITION.WORKER_CODE");
                obj_str.Append(" INNER JOIN HRM_MT_POSITION ON HRM_TR_EMPPOSITION.EMPPOSITION_POSITION=HRM_MT_POSITION.POSITION_CODE");
                obj_str.Append(" WHERE HRM_TR_TIMECARD.COMPANY_CODE = 'APT'");
                
          

                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" GROUP BY EMPPOSITION_POSITION, HRM_MT_POSITION.POSITION_NAME_TH, HRM_MT_POSITION.POSITION_NAME_EN");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TREmpOtpositionDash();


                    model.worker_code = Convert.ToInt32(dr["WORKER_CODE"]);
                    model.EMPPOSITION_POSITION = dr["EMPPOSITION_POSITION"].ToString();
                    model.POSITION_NAME_TH = dr["POSITION_NAME_TH"].ToString();
                    model.POSITION_NAME_EN = dr["POSITION_NAME_EN"].ToString();
                    

                    list_model.Add(model);
                }

            }
            catch (Exception ex)
            {
                Message = "ERROR::(Worker.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TREmpOtpositionDash> getDataByFillter(DateTime fromdate, DateTime todate)
        {
            string strCondition = "";

            strCondition += " AND (TIMECARD_WORKDATE BETWEEN '" + fromdate.ToString(this.FormatDateDB) + "' AND '" + todate.ToString(this.FormatDateDB) + "' )";

            return this.getData(strCondition);
        }
    }
}