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
    public class cls_ctMTDashboard
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctMTDashboard() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_MTDashboard> getDataGender(string condition)
        {
            List<cls_MTDashboard> list_model = new List<cls_MTDashboard>();
            cls_MTDashboard model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT");

                obj_str.Append(" COUNT(WORKER_CODE)as WORKER_CODE");
                obj_str.Append(", (CASE ISNULL(WORKER_GENDER, '') WHEN 'M' THEN 'Male' WHEN 'F' THEN 'Female' END) AS WORKER_GENDER_EN");
                obj_str.Append(", (CASE ISNULL(WORKER_GENDER, '') WHEN 'M' THEN 'เพศชาย' WHEN 'F' THEN 'เพศหญิง' END) AS WORKER_GENDER_TH");
                obj_str.Append(" FROM HRM_MT_WORKER");

                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" GROUP BY HRM_MT_WORKER.WORKER_GENDER ");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_MTDashboard();

                    model.worker_code = Convert.ToInt32(dr["WORKER_CODE"]);
                    model.worker_gender_en = dr["WORKER_GENDER_EN"].ToString();
                    model.worker_gender_th = dr["WORKER_GENDER_TH"].ToString();

                    list_model.Add(model);
                }

            }
            catch (Exception ex)
            {
                Message = "ERROR::(MTDashboard.getData)" + ex.ToString();
            }

            return list_model;
        }
        public List<cls_MTDashboard> getDataGenderByFillter(string com)
        {
            string strCondition = "";


            if (!com.Equals(""))
                strCondition += " AND COMPANY_CODE ='" + com + "'";

            return this.getDataGender(strCondition);
        }

        private List<cls_MTDashboard> getDataEmpDep(string condition)
        {
            List<cls_MTDashboard> list_model = new List<cls_MTDashboard>();
            cls_MTDashboard model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT");

                obj_str.Append(" COUNT(WORKER_CODE)as WORKER_CODE");
                obj_str.Append(", HRM_MT_DEP.DEP_NAME_EN");
                obj_str.Append(", HRM_MT_DEP.DEP_NAME_TH");
                obj_str.Append(" from HRM_TR_EMPDEP");
                obj_str.Append(" INNER JOIN HRM_MT_DEP on HRM_TR_EMPDEP.EMPDEP_LEVEL01 = HRM_MT_DEP.DEP_CODE");

                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" GROUP BY HRM_MT_DEP.DEP_NAME_EN,HRM_MT_DEP.DEP_NAME_TH ");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_MTDashboard();

                    model.worker_code = Convert.ToInt32(dr["WORKER_CODE"]);
                    model.dep_name_en = dr["DEP_NAME_EN"].ToString();
                    model.dep_name_th = dr["DEP_NAME_TH"].ToString();

                    list_model.Add(model);
                }

            }
            catch (Exception ex)
            {
                Message = "ERROR::(DashEmpDep.getData)" + ex.ToString();
            }

            return list_model;
        }
        public List<cls_MTDashboard> getDataEmpDepByFillter(string com)
        {
            string strCondition = "";


            if (!com.Equals(""))
                strCondition += " AND HRM_TR_EMPDEP.COMPANY_CODE ='" + com + "'";

            return this.getDataEmpDep(strCondition);
        }
    }
}
