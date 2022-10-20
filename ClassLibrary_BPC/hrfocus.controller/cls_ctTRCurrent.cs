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
    public class cls_ctTRCurrent
    {
         string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTRCurrent() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }
        private List<cls_TRCurrent> getData(string condition)
        {
            List<cls_TRCurrent> list_model = new List<cls_TRCurrent>();
            cls_TRCurrent model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");
                obj_str.Append("HRM_TR_TIMECARD.TIMECARD_WORKDATE");
                obj_str.Append(", HRM_TR_TIMECARD.TIMECARD_DAYTYPE");
                obj_str.Append(", HRM_TR_TIMECARD.SHIFT_CODE");
                obj_str.Append(", HRM_MT_SHIFT.SHIFT_NAME_TH");
                obj_str.Append(", HRM_MT_SHIFT.SHIFT_NAME_EN");

                obj_str.Append(" FROM HRM_TR_TIMECARD");
                obj_str.Append(" JOIN HRM_MT_SHIFT ON HRM_TR_TIMECARD.SHIFT_CODE = HRM_MT_SHIFT.SHIFT_CODE");
                obj_str.Append(" AND HRM_TR_TIMECARD.COMPANY_CODE = HRM_MT_SHIFT.COMPANY_CODE");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                //obj_str.Append(" ORDER BY COMPANY_CODE, DILIGENCE_CODE, STEPPAY_STEP");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TRCurrent();
                    model.date = Convert.ToDateTime(dr["TIMECARD_WORKDATE"]);
                    model.daytype = dr["TIMECARD_DAYTYPE"].ToString();
                    model.shift_code = dr["SHIFT_CODE"].ToString();
                    model.shift_name_th = dr["SHIFT_NAME_TH"].ToString();
                    model.shift_name_en = dr["SHIFT_NAME_EN"].ToString();

                    list_model.Add(model);
                }

            }
            catch (Exception ex)
            {
                Message = "ERROR::(TRCurrent.getData)" + ex.ToString();
            }

            return list_model;
        }
        public List<cls_TRCurrent> getDataByFillter(string worker_id, string startdate,string todate)
        {
            string strCondition = " AND  HRM_TR_TIMECARD.WORKER_CODE='" + worker_id + "'";

            if (!startdate.Equals("") && !todate.Equals(""))
                strCondition += "AND HRM_TR_TIMECARD.TIMECARD_WORKDATE BETWEEN '" + startdate + "' AND '" + todate + "'";


            return this.getData(strCondition);
        }
    }
}
