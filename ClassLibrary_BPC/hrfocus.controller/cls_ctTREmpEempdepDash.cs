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
    class cls_ctTREmpEempdepDash
    {
    string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTREmpEempdepDash() { }

        public string getMessage() { return this.Message; }

        private string FormatDateDB = "MM/dd/yyyy";

        public void dispose()
        {
            Obj_conn.doClose();
        }


        private List<cls_TREmpEempdepDash> getData(string condition)
        {
            List<cls_TREmpEempdepDash> list_model = new List<cls_TREmpEempdepDash>();
            cls_TREmpEempdepDash model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("SUM(HRM_TR_TIMECARD.TIMECARD_BEFORE_MIN) as BEFORE_MIN");
                obj_str.Append(", SUM(CASE WHEN (HRM_TR_TIMECARD.TIMECARD_DAYTYPE) = 'O' THEN HRM_TR_TIMECARD.TIMECARD_WORK1_MIN else null END) AS NORMAL_MIN");
                obj_str.Append(", SUM(HRM_TR_TIMECARD.TIMECARD_AFTER_MIN) as AFTER_MIN");
                obj_str.Append(", HRM_MT_DEP.DEP_NAME_EN");
                obj_str.Append(", HRM_MT_DEP.DEP_NAME_TH");
                obj_str.Append(" from HRM_TR_TIMECARD");
                obj_str.Append(" inner join HRM_TR_EMPDEP on HRM_TR_TIMECARD.WORKER_CODE = HRM_TR_EMPDEP.WORKER_CODE");
                obj_str.Append(" inner join HRM_MT_DEP on HRM_TR_EMPDEP.EMPDEP_LEVEL01 = HRM_MT_DEP.DEP_CODE");
                //obj_str.Append(" where HRM_TR_TIMECARD.TIMECARD_WORKDATE between '03/01/2022' AND '03/30/2022'");


                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append("GROUP BY HRM_MT_DEP.DEP_NAME_EN,HRM_MT_DEP.DEP_NAME_TH ");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TREmpEempdepDash();


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

        public List<cls_TREmpEempdepDash> getDataByFillter(DateTime fromdate, DateTime todate)
        {
            string strCondition = "";

            strCondition += " AND (TIMECARD_WORKDATE BETWEEN '" + fromdate.ToString(this.FormatDateDB) + "' AND '" + todate.ToString(this.FormatDateDB) + "' )";

            return this.getData(strCondition);
        }
    }

}