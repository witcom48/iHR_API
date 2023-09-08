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
   public class cls_ctMTEmpGenderDash
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctMTEmpGenderDash() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }



        private List<cls_MTEmpGenderDash> getData(string condition)
        {
            List<cls_MTEmpGenderDash> list_model = new List<cls_MTEmpGenderDash>();
            cls_MTEmpGenderDash model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("COUNT(WORKER_CODE)as WORKER_CODE");
                obj_str.Append(", (CASE ISNULL(WORKER_GENDER, '') WHEN 'M' THEN 'Male' WHEN 'F' THEN 'Female' END) AS WORKER_GENDER_EN");
                obj_str.Append(", (CASE ISNULL(WORKER_GENDER, '') WHEN 'M' THEN 'เพศชาย' WHEN 'F' THEN 'เพศหญิง' END) AS WORKER_GENDER_TH");
                obj_str.Append(" FROM HRM_MT_WORKER");
                obj_str.Append(" where COMPANY_CODE = 'APT'");

                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" GROUP BY HRM_MT_WORKER.WORKER_GENDER");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_MTEmpGenderDash();

                    model.WORKER_CODE = Convert.ToInt32(dr["WORKER_CODE"]);
                    model.WORKER_GENDER = dr["WORKER_GENDER"].ToString();


                   

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

