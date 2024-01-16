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

                obj_str.Append("COMPANY_CODE");
                obj_str.Append(", YEAR_CODE");
                obj_str.Append(", MONTH_NO");

                obj_str.Append(", KT20_RATE");

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


                obj_str.Append(", CREATED_BY");
                obj_str.Append(", CREATED_DATE");

                obj_str.Append(" FROM HRM_TR_KT20_SUMMARY");

                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY COMPANY_CODE, YEAR_CODE,MONTH_NO");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TRKT20();

                    model.company_code = Convert.ToString(dr["COMPANY_CODE"]);
                    model.year_code = Convert.ToString(dr["YEAR_CODE"]);
                    model.month_no = Convert.ToString(dr["MONTH_NO"]);

                    model.kt20_rate = Convert.ToDouble(dr["KT20_RATE"]);

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

                    model.created_by = dr["CREATED_BY"].ToString();
                    model.created_date = Convert.ToDateTime(dr["CREATED_DATE"]);

                    list_model.Add(model);
                }

            }
            catch (Exception ex)
            {
                Message = "ERROR::(KT20.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TRKT20> getDataByFillter(string language, string com ,string year,string month)
        {
            string strCondition = "";

            strCondition += " AND COMPANY_CODE='" + com + "'";

            if (!year.Equals(""))
                strCondition += " AND YEAR_CODE='" + year + "'";

            if (!month.Equals(""))
                strCondition += " AND MONTH_NO='" + month + "'";

            return this.getData(language, strCondition);
        }
    }
}
