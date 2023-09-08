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
    public class cls_ctTRAttTimecardDash
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTRAttTimecardDash() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }



        private List<cls_TRAttTimecardDash> getData(string condition)
        {
            List<cls_TRAttTimecardDash> list_model = new List<cls_TRAttTimecardDash>();
            cls_TRAttTimecardDash model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("COUNT(HRM_TR_TIMECARD.TIMECARD_LATE_MIN) as LATE");
                obj_str.Append(", HRM_MT_DEP.DEP_NAME_EN");
                obj_str.Append(", HRM_MT_DEP.DEP_NAME_TH");
                obj_str.Append(" from HRM_TR_TIMECARD");
                obj_str.Append("inner join HRM_TR_EMPDEP on HRM_TR_TIMECARD.WORKER_CODE = HRM_TR_EMPDEP.WORKER_CODE");
                obj_str.Append("inner join HRM_MT_DEP on HRM_TR_EMPDEP.EMPDEP_LEVEL01 = HRM_MT_DEP.DEP_CODE");
                obj_str.Append("where HRM_TR_TIMECARD.COMPANY_CODE = 'APT' ");
                obj_str.Append(" AND (HRM_TR_TIMECARD.TIMECARD_LATE_MIN !='0' AND HRM_TR_TIMECARD.TIMECARD_LATE_MIN_APP !='0' )");


                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append("GROUP BY HRM_MT_DEP.DEP_NAME_EN,HRM_MT_DEP.DEP_NAME_TH");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TRAttTimecardDash();

                    model.TIMECARD_LATE_MIN = Convert.ToInt32(dr["TIMECARD_LATE_MIN"]);
                    model.DEP_NAME_EN = dr["DEP_NAME_EN"].ToString();
                    model.DEP_NAME_TH = dr["DEP_NAME_TH"].ToString();


                   

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
