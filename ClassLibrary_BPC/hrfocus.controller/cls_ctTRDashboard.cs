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
    public class cls_ctTRDashboard
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTRDashboard() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TRDashboard> getDataItemIN(string condition)
        {
            List<cls_TRDashboard> list_model = new List<cls_TRDashboard>();
            cls_TRDashboard model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT");

                obj_str.Append(" SUM(HRM_TR_PAYITEM.PAYITEM_AMOUNT) as AMOUNT");
                obj_str.Append(", HRM_MT_ITEM.ITEM_NAME_TH");
                obj_str.Append(", HRM_MT_ITEM.ITEM_NAME_EN");
                obj_str.Append(", HRM_TR_PAYITEM.ITEM_CODE");
                obj_str.Append(" FROM HRM_TR_PAYITEM");
                obj_str.Append(" INNER JOIN HRM_MT_ITEM on HRM_TR_PAYITEM.ITEM_CODE = HRM_MT_ITEM.ITEM_CODE");

                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" AND HRM_MT_ITEM.ITEM_TYPE = 'IN'");
                obj_str.Append(" GROUP BY HRM_MT_ITEM.ITEM_NAME_TH,HRM_MT_ITEM.ITEM_NAME_EN,HRM_TR_PAYITEM.ITEM_CODE ");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TRDashboard();

                    model.amount = Convert.ToInt32(dr["AMOUNT"]);
                    model.item_name_en = dr["ITEM_NAME_EN"].ToString();
                    model.item_name_th = dr["ITEM_NAME_TH"].ToString();
                    model.item_code = dr["ITEM_CODE"].ToString();

                    list_model.Add(model);
                }

            }
            catch (Exception ex)
            {
                Message = "ERROR::(TRDashboard.getData)" + ex.ToString();
            }

            return list_model;
        }
        public List<cls_TRDashboard> getDataItemINByFillter(string com, DateTime datefrom, DateTime dateto)
        {
            string strCondition = "";

                strCondition += " AND HRM_TR_PAYITEM.COMPANY_CODE ='" + com + "'";

                if (!datefrom.Equals("") || !dateto.Equals(""))
                    strCondition += " AND (HRM_TR_PAYITEM.PAYITEM_DATE BETWEEN '" + datefrom.ToString("MM/dd/yyyy") + "' AND '" + dateto.ToString("MM/dd/yyyy") + "')";

            return this.getDataItemIN(strCondition);
        }

        private List<cls_TRDashboard> getDataItemDE(string condition)
        {
            List<cls_TRDashboard> list_model = new List<cls_TRDashboard>();
            cls_TRDashboard model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT");

                obj_str.Append(" SUM(HRM_TR_PAYITEM.PAYITEM_AMOUNT) as AMOUNT");
                obj_str.Append(", HRM_MT_ITEM.ITEM_NAME_TH");
                obj_str.Append(", HRM_MT_ITEM.ITEM_NAME_EN");
                obj_str.Append(", HRM_TR_PAYITEM.ITEM_CODE");
                obj_str.Append(" FROM HRM_TR_PAYITEM");
                obj_str.Append(" INNER JOIN HRM_MT_ITEM on HRM_TR_PAYITEM.ITEM_CODE = HRM_MT_ITEM.ITEM_CODE");

                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" AND HRM_MT_ITEM.ITEM_TYPE = 'DE'");
                obj_str.Append(" GROUP BY HRM_MT_ITEM.ITEM_NAME_TH,HRM_MT_ITEM.ITEM_NAME_EN,HRM_TR_PAYITEM.ITEM_CODE ");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TRDashboard();

                    model.amount = Convert.ToInt32(dr["AMOUNT"]);
                    model.item_name_en = dr["ITEM_NAME_EN"].ToString();
                    model.item_name_th = dr["ITEM_NAME_TH"].ToString();
                    model.item_code = dr["ITEM_CODE"].ToString();

                    list_model.Add(model);
                }

            }
            catch (Exception ex)
            {
                Message = "ERROR::(TRDashboard.getData)" + ex.ToString();
            }

            return list_model;
        }
        public List<cls_TRDashboard> getDataItemDEByFillter(string com, DateTime datefrom, DateTime dateto)
        {
            string strCondition = "";

                strCondition += " AND HRM_TR_PAYITEM.COMPANY_CODE ='" + com + "'";

                if (!datefrom.Equals("") || !dateto.Equals(""))
                    strCondition += " AND (HRM_TR_PAYITEM.PAYITEM_DATE BETWEEN '" + datefrom.ToString("MM/dd/yyyy") + "' AND '" + dateto.ToString("MM/dd/yyyy") + "')";

            return this.getDataItemDE(strCondition);
        }

        private List<cls_TRDashboard> getDataOTDep(string condition)
        {
            List<cls_TRDashboard> list_model = new List<cls_TRDashboard>();
            cls_TRDashboard model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT");

                obj_str.Append(" ISNULL(SUM(HRM_TR_TIMECARD.TIMECARD_BEFORE_MIN),'' )as BEFORE_MIN");
                obj_str.Append(", ISNULL(SUM(CASE WHEN (HRM_TR_TIMECARD.TIMECARD_DAYTYPE) = 'O' THEN HRM_TR_TIMECARD.TIMECARD_WORK1_MIN else null END),'') AS NORMAL_MIN");
                obj_str.Append(", ISNULL(SUM(HRM_TR_TIMECARD.TIMECARD_AFTER_MIN),'') as AFTER_MIN");
                obj_str.Append(", HRM_MT_DEP.DEP_NAME_EN");
                obj_str.Append(", HRM_MT_DEP.DEP_NAME_TH");
                obj_str.Append(" from HRM_TR_TIMECARD");
                obj_str.Append(" inner join HRM_TR_EMPDEP on HRM_TR_TIMECARD.WORKER_CODE = HRM_TR_EMPDEP.WORKER_CODE");
                obj_str.Append(" inner join HRM_MT_DEP on HRM_TR_EMPDEP.EMPDEP_LEVEL01 = HRM_MT_DEP.DEP_CODE");

                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" GROUP BY HRM_MT_DEP.DEP_NAME_EN,HRM_MT_DEP.DEP_NAME_TH ");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TRDashboard();

                    model.before_min = Convert.ToInt32(dr["BEFORE_MIN"]);
                    model.normal_min = Convert.ToInt32(dr["NORMAL_MIN"]);
                    model.after_min = Convert.ToInt32(dr["AFTER_MIN"]);
                    model.dep_name_en = dr["DEP_NAME_EN"].ToString();
                    model.dep_name_th = dr["DEP_NAME_TH"].ToString();

                    list_model.Add(model);
                }

            }
            catch (Exception ex)
            {
                Message = "ERROR::(TRDashboard.getData)" + ex.ToString();
            }

            return list_model;
        }
        public List<cls_TRDashboard> getDataOTDepByFillter(DateTime datefrom, DateTime dateto)
        {
            string strCondition = "";


            if (!datefrom.Equals("") || !dateto.Equals(""))
                strCondition += " AND (HRM_TR_TIMECARD.TIMECARD_WORKDATE BETWEEN '" + datefrom.ToString("MM/dd/yyyy") + "' AND '" + dateto.ToString("MM/dd/yyyy") + "')";

            return this.getDataOTDep(strCondition);
        }

        private List<cls_TRDashboard> getDataOTPo(string condition)
        {
            List<cls_TRDashboard> list_model = new List<cls_TRDashboard>();
            cls_TRDashboard model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT");

                obj_str.Append(" ISNULL(SUM(HRM_TR_TIMECARD.TIMECARD_BEFORE_MIN),'' )as BEFORE_MIN");
                obj_str.Append(", ISNULL(SUM(CASE WHEN (HRM_TR_TIMECARD.TIMECARD_DAYTYPE) = 'O' THEN HRM_TR_TIMECARD.TIMECARD_WORK1_MIN else null END),'') AS NORMAL_MIN");
                obj_str.Append(", ISNULL(SUM(HRM_TR_TIMECARD.TIMECARD_AFTER_MIN),'') as AFTER_MIN");
                obj_str.Append(", HRM_TR_EMPPOSITION.EMPPOSITION_POSITION");
                obj_str.Append(", HRM_MT_POSITION.POSITION_NAME_EN");
                obj_str.Append(", HRM_MT_POSITION.POSITION_NAME_TH");
                obj_str.Append(" from HRM_TR_TIMECARD");
                obj_str.Append(" INNER JOIN HRM_TR_EMPPOSITION ON HRM_TR_TIMECARD.WORKER_CODE = HRM_TR_EMPPOSITION.WORKER_CODE");
                obj_str.Append(" INNER JOIN HRM_MT_POSITION ON HRM_TR_EMPPOSITION.EMPPOSITION_POSITION=HRM_MT_POSITION.POSITION_CODE");

                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" GROUP BY EMPPOSITION_POSITION, HRM_MT_POSITION.POSITION_NAME_TH, HRM_MT_POSITION.POSITION_NAME_EN ");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TRDashboard();

                    model.before_min = Convert.ToInt32(dr["BEFORE_MIN"]);
                    model.normal_min = Convert.ToInt32(dr["NORMAL_MIN"]);
                    model.after_min = Convert.ToInt32(dr["AFTER_MIN"]);
                    model.empposition_position = dr["EMPPOSITION_POSITION"].ToString(); 
                    model.position_name_en = dr["POSITION_NAME_EN"].ToString();
                    model.position_name_th = dr["POSITION_NAME_TH"].ToString();

                    list_model.Add(model);
                }

            }
            catch (Exception ex)
            {
                Message = "ERROR::(TRDashboard.getData)" + ex.ToString();
            }

            return list_model;
        }
        public List<cls_TRDashboard> getDataOTPoByFillter(String com,DateTime datefrom, DateTime dateto)
        {
            string strCondition = "";

            strCondition += " AND HRM_TR_TIMECARD.COMPANY_CODE='" + com + "'";

            if (!datefrom.Equals("") || !dateto.Equals(""))
                strCondition += " AND (HRM_TR_TIMECARD.TIMECARD_WORKDATE BETWEEN '" + datefrom.ToString("MM/dd/yyyy") + "' AND '" + dateto.ToString("MM/dd/yyyy") + "')";

            return this.getDataOTPo(strCondition);
        }
    }
}
