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
    public class cls_ctTRKT20
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTRKT20() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TRKT20> getData(string language, string condition)
        {
            List<cls_TRKT20> list_model = new List<cls_TRKT20>();
            cls_TRKT20 model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("HRM_TR_KT20.COMPANY_CODE");
                obj_str.Append(", HRM_TR_KT20.WORKER_CODE");
                obj_str.Append(", YEAR_CODE");
                obj_str.Append(", MONTH_NO");
                obj_str.Append(", EMP");

                obj_str.Append(", ISNULL(SALARY_MONTH_MIN, 0) AS SALARY_MONTH_MIN");
                obj_str.Append(", ISNULL(SALARY_DAILY_MIN, 0) AS SALARY_DAILY_MIN");
                obj_str.Append(", ISNULL(SALARY_MONTH, 0) AS SALARY_MONTH");
                obj_str.Append(", ISNULL(SALARY_DAILY, 0) AS SALARY_DAILY");
                obj_str.Append(", ISNULL(BONUS, 0) AS BONUS");
                obj_str.Append(", ISNULL(OVERTIME, 0) AS OVERTIME");
                obj_str.Append(", ISNULL(OTHER, 0) AS OTHER");
                obj_str.Append(", ISNULL(SUMMARY, 0) AS SUMMARY");
                obj_str.Append(", ISNULL(MORE20000, 0) AS MORE20000");
                obj_str.Append(", ISNULL(NET, 0) AS NET");
                

                obj_str.Append(", ISNULL(HRM_TR_PAYTRAN.MODIFIED_BY, HRM_TR_PAYTRAN.CREATED_BY) AS MODIFIED_BY");
                obj_str.Append(", ISNULL(HRM_TR_PAYTRAN.MODIFIED_DATE, HRM_TR_PAYTRAN.CREATED_DATE) AS MODIFIED_DATE");

                if (language.Equals("TH"))
                {
                    obj_str.Append(", INITIAL_NAME_TH + WORKER_FNAME_TH + ' ' + WORKER_LNAME_TH AS WORKER_DETAIL");
                }
                else
                {
                    obj_str.Append(", INITIAL_NAME_EN + WORKER_FNAME_EN + ' ' + WORKER_LNAME_EN AS WORKER_DETAIL");
                }


                obj_str.Append(" FROM HRM_TR_KT20");
                obj_str.Append(" INNER JOIN HRM_MT_WORKER ON HRM_MT_WORKER.COMPANY_CODE=HRM_TR_KT20.COMPANY_CODE AND HRM_MT_WORKER.WORKER_CODE=HRM_TR_KT20.WORKER_CODE");
                obj_str.Append(" INNER JOIN HRM_MT_INITIAL ON HRM_MT_INITIAL.INITIAL_CODE=HRM_MT_WORKER.WORKER_INITIAL ");

                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY HRM_TR_KT20.COMPANY_CODE, HRM_TR_KT20.WORKER_CODE,YEAR_CODE,MONTH_NO");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TRKT20();

                    model.company_code = Convert.ToString(dr["COMPANY_CODE"]);
                    model.worker_code = Convert.ToString(dr["WORKER_CODE"]);
                    model.year_code = Convert.ToString(dr["YEAR_CODE"]);
                    model.month_no = Convert.ToString(dr["MONTH_NO"]);

                    model.emp = Convert.ToInt32(dr["EMP"]);

                    model.salary_month_min = Convert.ToDouble(dr["SALARY_MONTH_MIN"]);
                    model.salary_daily_min = Convert.ToDouble(dr["SALARY_DAILY_MIN"]);
                    model.salary_month = Convert.ToDouble(dr["SALARY_MONTH"]);
                    model.salary_daily = Convert.ToDouble(dr["SALARY_DAILY"]);

                    model.bonus = Convert.ToDouble(dr["BONUS"]);
                    model.overtime = Convert.ToDouble(dr["OVERTIME"]);

                    model.other = Convert.ToDouble(dr["OTHER"]);
                    model.summary = Convert.ToDouble(dr["SUMMARY"]);
                    model.more20000 = Convert.ToDouble(dr["MORE20000"]);

                    model.net = Convert.ToDouble(dr["NET"]);


                    model.worker_detail = Convert.ToString(dr["WORKER_DETAIL"]);


                    model.modified_by = dr["MODIFIED_BY"].ToString();
                    model.modified_date = Convert.ToDateTime(dr["MODIFIED_DATE"]);

                    list_model.Add(model);
                }

            }
            catch (Exception ex)
            {
                Message = "ERROR::(KT20.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TRKT20> getDataByFillter(string language, string com, string emp ,string year,string month)
        {
            string strCondition = "";

            strCondition += " AND HRM_TR_PAYTRAN.COMPANY_CODE='" + com + "'";

            if (!emp.Equals(""))
                strCondition += " AND HRM_TR_PAYTRAN.WORKER_CODE='" + emp + "'";

            if (!year.Equals(""))
                strCondition += " AND YEAR_CODE='" + year + "'";

            if (!month.Equals(""))
                strCondition += " AND MONTH_NO='" + month + "'";

            return this.getData(language, strCondition);
        }
    }
}
