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
    public class cls_ctTRAttwageday
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTRAttwageday() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TRAttwageday> getData(string language, string condition)
        {
            List<cls_TRAttwageday> list_model = new List<cls_TRAttwageday>();
            cls_TRAttwageday model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("HRM_TR_ATTWAGEDAY.COMPANY_CODE");
                obj_str.Append(", HRM_TR_ATTWAGEDAY.WORKER_CODE");
                obj_str.Append(", WAGEDAY_DATE");
                obj_str.Append(", WAGEDAY_DAYTYPE");

                obj_str.Append(", WAGEDAY_WAGEMONEY");
                obj_str.Append(", WAGEDAY_WAGEHHMM");

                obj_str.Append(", WAGEDAY_LATEMONEY");
                obj_str.Append(", WAGEDAY_LATEHHMM");

                obj_str.Append(", WAGEDAY_LEAVEMONEY");
                obj_str.Append(", WAGEDAY_LEAVEHHMM");

                obj_str.Append(", WAGEDAY_ABSENTMONEY");
                obj_str.Append(", WAGEDAY_ABSENTHHMM");

                obj_str.Append(", ISNULL(WAGEDAY_OT1MONEY, 0) AS WAGEDAY_OT1MONEY");
                obj_str.Append(", ISNULL(WAGEDAY_OT1HHMM, '00:00') AS WAGEDAY_OT1HHMM");

                obj_str.Append(", ISNULL(WAGEDAY_OT15MONEY, 0) AS WAGEDAY_OT15MONEY");
                obj_str.Append(", ISNULL(WAGEDAY_OT15HHMM, '00:00') AS WAGEDAY_OT15HHMM");

                obj_str.Append(", ISNULL(WAGEDAY_OT2MONEY, 0) AS WAGEDAY_OT2MONEY");
                obj_str.Append(", ISNULL(WAGEDAY_OT2HHMM, '00:00') AS WAGEDAY_OT2HHMM");

                obj_str.Append(", ISNULL(WAGEDAY_OT3MONEY, 0) AS WAGEDAY_OT3MONEY");
                obj_str.Append(", ISNULL(WAGEDAY_OT3HHMM, '00:00') AS WAGEDAY_OT3HHMM");

                obj_str.Append(", ISNULL(WAGEDAY_ALLOWANCE, 0) AS WAGEDAY_ALLOWANCE");

                obj_str.Append(", ISNULL(HRM_TR_ATTWAGEDAY.MODIFIED_BY, HRM_TR_ATTWAGEDAY.CREATED_BY) AS MODIFIED_BY");
                obj_str.Append(", ISNULL(HRM_TR_ATTWAGEDAY.MODIFIED_DATE, HRM_TR_ATTWAGEDAY.CREATED_DATE) AS MODIFIED_DATE");
                
                if (language.Equals("TH"))
                {
                    obj_str.Append(", INITIAL_NAME_TH + WORKER_FNAME_TH + ' ' + WORKER_LNAME_TH AS WORKER_DETAIL");
                }
                else
                {                    
                    obj_str.Append(", INITIAL_NAME_EN + WORKER_FNAME_EN + ' ' + WORKER_LNAME_EN AS WORKER_DETAIL");
                }


                obj_str.Append(" FROM HRM_TR_ATTWAGEDAY");

                obj_str.Append(" INNER JOIN HRM_MT_WORKER ON HRM_MT_WORKER.COMPANY_CODE=HRM_TR_ATTWAGEDAY.COMPANY_CODE AND HRM_MT_WORKER.WORKER_CODE=HRM_TR_ATTWAGEDAY.WORKER_CODE");
                obj_str.Append(" INNER JOIN HRM_MT_INITIAL ON HRM_MT_INITIAL.INITIAL_CODE=HRM_MT_WORKER.WORKER_INITIAL ");
                
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY HRM_TR_ATTWAGEDAY.COMPANY_CODE, WAGEDAY_DATE, HRM_TR_ATTWAGEDAY.WORKER_CODE");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TRAttwageday();

                    model.company_code = Convert.ToString(dr["COMPANY_CODE"]);
                    model.worker_code = Convert.ToString(dr["WORKER_CODE"]);
                    model.wageday_date = Convert.ToDateTime(dr["WAGEDAY_DATE"]);
                    model.wageday_daytype = Convert.ToString(dr["WAGEDAY_DAYTYPE"]);

                    model.wageday_wagemoney = Convert.ToDouble(dr["WAGEDAY_WAGEMONEY"]);
                    model.wageday_wagehhmm = Convert.ToString(dr["WAGEDAY_WAGEHHMM"]);

                    model.wageday_latemoney = Convert.ToDouble(dr["WAGEDAY_LATEMONEY"]);
                    model.wageday_latehhmm = Convert.ToString(dr["WAGEDAY_LATEHHMM"]);

                    model.wageday_leavemoney = Convert.ToDouble(dr["WAGEDAY_LEAVEMONEY"]);
                    model.wageday_leavehhmm = Convert.ToString(dr["WAGEDAY_LEAVEHHMM"]);

                    model.wageday_absentmoney = Convert.ToDouble(dr["WAGEDAY_ABSENTMONEY"]);
                    model.wageday_absenthhmm = Convert.ToString(dr["WAGEDAY_ABSENTHHMM"]);

                    model.wageday_ot1money = Convert.ToDouble(dr["WAGEDAY_OT1MONEY"]);
                    model.wageday_ot1hhmm = Convert.ToString(dr["WAGEDAY_OT1HHMM"]);

                    model.wageday_ot15money = Convert.ToDouble(dr["WAGEDAY_OT15MONEY"]);
                    model.wageday_ot15hhmm = Convert.ToString(dr["WAGEDAY_OT15HHMM"]);

                    model.wageday_ot2money = Convert.ToDouble(dr["WAGEDAY_OT2MONEY"]);
                    model.wageday_ot2hhmm = Convert.ToString(dr["WAGEDAY_OT2HHMM"]);

                    model.wageday_ot3money = Convert.ToDouble(dr["WAGEDAY_OT3MONEY"]);
                    model.wageday_ot3hhmm = Convert.ToString(dr["WAGEDAY_OT3HHMM"]);

                    model.wageday_allowance = Convert.ToDouble(dr["WAGEDAY_ALLOWANCE"]);

                    model.worker_detail = Convert.ToString(dr["WORKER_DETAIL"]);

                    model.modified_by = dr["MODIFIED_BY"].ToString();
                    model.modified_date = Convert.ToDateTime(dr["MODIFIED_DATE"]);

                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Attwageday.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TRAttwageday> getDataByFillter(string language, string com, DateTime datefrom, DateTime dateto, string emp)
        {
            string strCondition = "";

            strCondition += " AND HRM_TR_ATTWAGEDAY.COMPANY_CODE='" + com + "'";
            strCondition += " AND (HRM_TR_ATTWAGEDAY.WAGEDAY_DATE BETWEEN '" + datefrom.ToString("MM/dd/yyyy") + "' AND '" + dateto.ToString("MM/dd/yyyy") + "')";

            if (!emp.Equals(""))
                strCondition += " AND HRM_TR_ATTWAGEDAY.WORKER_CODE='" + emp + "'";
            
            return this.getData(language, strCondition);
        }

        public bool delete(string com, string emp, DateTime date)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_TR_ATTWAGEDAY");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "' ");
                obj_str.Append(" AND WORKER_CODE='" + emp + "' ");                
                obj_str.Append(" AND WAGEDAY_DATE='" + date.ToString("MM/dd/yyyy") + "' ");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());               

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Wageday.delete)" + ex.ToString();
            }

            return blnResult;
        }

        public bool checkDataOld(string com, string emp, DateTime date)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT COMPANY_CODE");
                obj_str.Append(" FROM HRM_TR_ATTWAGEDAY");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "' ");
                obj_str.Append(" AND WORKER_CODE='" + emp + "' ");               
                obj_str.Append(" AND PAYITEM_DATE='" + date.ToString("MM/dd/yyyy") + "' ");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Payitem.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(cls_TRAttwageday model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.company_code, model.worker_code, model.wageday_date))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_ATTWAGEDAY");
                obj_str.Append(" (");
                obj_str.Append("COMPANY_CODE ");
                obj_str.Append(", WORKER_CODE ");
                obj_str.Append(", WAGEDAY_DATE ");
                obj_str.Append(", WAGEDAY_DAYTYPE ");

                obj_str.Append(", WAGEDAY_WAGEMONEY ");
                obj_str.Append(", WAGEDAY_WAGEHHMM ");

                obj_str.Append(", WAGEDAY_LATEMONEY ");
                obj_str.Append(", WAGEDAY_LATEHHMM ");

                obj_str.Append(", WAGEDAY_LEAVEMONEY ");
                obj_str.Append(", WAGEDAY_LEAVEHHMM ");

                obj_str.Append(", WAGEDAY_ABSENTMONEY ");
                obj_str.Append(", WAGEDAY_ABSENTHHMM ");

                obj_str.Append(", WAGEDAY_OT1MONEY ");
                obj_str.Append(", WAGEDAY_OT1HHMM ");

                obj_str.Append(", WAGEDAY_OT15MONEY ");
                obj_str.Append(", WAGEDAY_OT15HHMM ");

                obj_str.Append(", WAGEDAY_OT2MONEY ");
                obj_str.Append(", WAGEDAY_OT2HHMM ");

                obj_str.Append(", WAGEDAY_OT3MONEY ");
                obj_str.Append(", WAGEDAY_OT3HHMM ");

                obj_str.Append(", WAGEDAY_ALLOWANCE ");

                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@COMPANY_CODE ");
                obj_str.Append(", @WORKER_CODE ");
                obj_str.Append(", @WAGEDAY_DATE ");
                obj_str.Append(", @WAGEDAY_DAYTYPE ");

                obj_str.Append(", @WAGEDAY_WAGEMONEY ");
                obj_str.Append(", @WAGEDAY_WAGEHHMM ");

                obj_str.Append(", @WAGEDAY_LATEMONEY ");
                obj_str.Append(", @WAGEDAY_LATEHHMM ");

                obj_str.Append(", @WAGEDAY_LEAVEMONEY ");
                obj_str.Append(", @WAGEDAY_LEAVEHHMM ");

                obj_str.Append(", @WAGEDAY_ABSENTMONEY ");
                obj_str.Append(", @WAGEDAY_ABSENTHHMM ");

                obj_str.Append(", @WAGEDAY_OT1MONEY ");
                obj_str.Append(", @WAGEDAY_OT1HHMM ");

                obj_str.Append(", @WAGEDAY_OT15MONEY ");
                obj_str.Append(", @WAGEDAY_OT15HHMM ");

                obj_str.Append(", @WAGEDAY_OT2MONEY ");
                obj_str.Append(", @WAGEDAY_OT2HHMM ");

                obj_str.Append(", @WAGEDAY_OT3MONEY ");
                obj_str.Append(", @WAGEDAY_OT3HHMM ");

                obj_str.Append(", @WAGEDAY_ALLOWANCE ");

                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                obj_cmd.Parameters.Add("@WAGEDAY_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@WAGEDAY_DATE"].Value = model.wageday_date;
                obj_cmd.Parameters.Add("@WAGEDAY_DAYTYPE", SqlDbType.VarChar); obj_cmd.Parameters["@WAGEDAY_DAYTYPE"].Value = model.wageday_daytype;

                obj_cmd.Parameters.Add("@WAGEDAY_WAGEMONEY", SqlDbType.Decimal); obj_cmd.Parameters["@PAYITEM_AMOUNT"].Value = model.wageday_wagemoney;
                obj_cmd.Parameters.Add("@WAGEDAY_WAGEHHMM", SqlDbType.VarChar); obj_cmd.Parameters["@PAYITEM_AMOUNT"].Value = model.wageday_wagehhmm;

                obj_cmd.Parameters.Add("@WAGEDAY_LATEMONEY", SqlDbType.Decimal); obj_cmd.Parameters["@WAGEDAY_LATEMONEY"].Value = model.wageday_latemoney;
                obj_cmd.Parameters.Add("@WAGEDAY_LATEHHMM", SqlDbType.VarChar); obj_cmd.Parameters["@WAGEDAY_LATEHHMM"].Value = model.wageday_latehhmm;

                obj_cmd.Parameters.Add("@WAGEDAY_LEAVEMONEY", SqlDbType.Decimal); obj_cmd.Parameters["@WAGEDAY_LEAVEMONEY"].Value = model.wageday_leavemoney;
                obj_cmd.Parameters.Add("@WAGEDAY_LEAVEHHMM", SqlDbType.VarChar); obj_cmd.Parameters["@WAGEDAY_LEAVEHHMM"].Value = model.wageday_leavehhmm;

                obj_cmd.Parameters.Add("@WAGEDAY_ABSENTMONEY", SqlDbType.Decimal); obj_cmd.Parameters["@WAGEDAY_ABSENTMONEY"].Value = model.wageday_absentmoney;
                obj_cmd.Parameters.Add("@WAGEDAY_ABSENTHHMM", SqlDbType.VarChar); obj_cmd.Parameters["@WAGEDAY_ABSENTHHMM"].Value = model.wageday_absenthhmm;

                obj_cmd.Parameters.Add("@WAGEDAY_OT1MONEY", SqlDbType.Decimal); obj_cmd.Parameters["@WAGEDAY_OT1MONEY"].Value = model.wageday_ot1money;
                obj_cmd.Parameters.Add("@WAGEDAY_OT1HHMM", SqlDbType.VarChar); obj_cmd.Parameters["@WAGEDAY_OT1HHMM"].Value = model.wageday_ot1hhmm;

                obj_cmd.Parameters.Add("@WAGEDAY_OT15MONEY", SqlDbType.Decimal); obj_cmd.Parameters["@WAGEDAY_OT15MONEY"].Value = model.wageday_ot15money;
                obj_cmd.Parameters.Add("@WAGEDAY_OT15HHMM", SqlDbType.VarChar); obj_cmd.Parameters["@WAGEDAY_OT15HHMM"].Value = model.wageday_ot15hhmm;

                obj_cmd.Parameters.Add("@WAGEDAY_OT2MONEY", SqlDbType.Decimal); obj_cmd.Parameters["@WAGEDAY_OT2MONEY"].Value = model.wageday_ot2money;
                obj_cmd.Parameters.Add("@WAGEDAY_OT2HHMM", SqlDbType.VarChar); obj_cmd.Parameters["@WAGEDAY_OT2HHMM"].Value = model.wageday_ot2hhmm;

                obj_cmd.Parameters.Add("@WAGEDAY_OT3MONEY", SqlDbType.Decimal); obj_cmd.Parameters["@WAGEDAY_OT3MONEY"].Value = model.wageday_ot3money;
                obj_cmd.Parameters.Add("@WAGEDAY_OT3HHMM", SqlDbType.VarChar); obj_cmd.Parameters["@WAGEDAY_OT3HHMM"].Value = model.wageday_ot3hhmm;

                obj_cmd.Parameters.Add("@WAGEDAY_ALLOWANCE", SqlDbType.Decimal); obj_cmd.Parameters["@WAGEDAY_ALLOWANCE"].Value = model.wageday_allowance;

                obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@CREATED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Attwageday.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(string com, string emp, DateTime date, List<cls_TRAttwageday> list_model)
        {
            bool blnResult = false;
            cls_ctConnection obj_conn = new cls_ctConnection();

            try
            {

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_ATTWAGEDAY");
                obj_str.Append(" (");
                obj_str.Append("COMPANY_CODE ");
                obj_str.Append(", WORKER_CODE ");
                obj_str.Append(", WAGEDAY_DATE ");
                obj_str.Append(", WAGEDAY_DAYTYPE ");

                obj_str.Append(", WAGEDAY_WAGEMONEY ");
                obj_str.Append(", WAGEDAY_WAGEHHMM ");

                obj_str.Append(", WAGEDAY_LATEMONEY ");
                obj_str.Append(", WAGEDAY_LATEHHMM ");

                obj_str.Append(", WAGEDAY_LEAVEMONEY ");
                obj_str.Append(", WAGEDAY_LEAVEHHMM ");

                obj_str.Append(", WAGEDAY_ABSENTMONEY ");
                obj_str.Append(", WAGEDAY_ABSENTHHMM ");

                obj_str.Append(", WAGEDAY_OT1MONEY ");
                obj_str.Append(", WAGEDAY_OT1HHMM ");

                obj_str.Append(", WAGEDAY_OT15MONEY ");
                obj_str.Append(", WAGEDAY_OT15HHMM ");

                obj_str.Append(", WAGEDAY_OT2MONEY ");
                obj_str.Append(", WAGEDAY_OT2HHMM ");

                obj_str.Append(", WAGEDAY_OT3MONEY ");
                obj_str.Append(", WAGEDAY_OT3HHMM ");

                obj_str.Append(", WAGEDAY_ALLOWANCE ");

                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@COMPANY_CODE ");
                obj_str.Append(", @WORKER_CODE ");
                obj_str.Append(", @WAGEDAY_DATE ");
                obj_str.Append(", @WAGEDAY_DAYTYPE ");

                obj_str.Append(", @WAGEDAY_WAGEMONEY ");
                obj_str.Append(", @WAGEDAY_WAGEHHMM ");

                obj_str.Append(", @WAGEDAY_LATEMONEY ");
                obj_str.Append(", @WAGEDAY_LATEHHMM ");

                obj_str.Append(", @WAGEDAY_LEAVEMONEY ");
                obj_str.Append(", @WAGEDAY_LEAVEHHMM ");

                obj_str.Append(", @WAGEDAY_ABSENTMONEY ");
                obj_str.Append(", @WAGEDAY_ABSENTHHMM ");

                obj_str.Append(", @WAGEDAY_OT1MONEY ");
                obj_str.Append(", @WAGEDAY_OT1HHMM ");

                obj_str.Append(", @WAGEDAY_OT15MONEY ");
                obj_str.Append(", @WAGEDAY_OT15HHMM ");

                obj_str.Append(", @WAGEDAY_OT2MONEY ");
                obj_str.Append(", @WAGEDAY_OT2HHMM ");

                obj_str.Append(", @WAGEDAY_OT3MONEY ");
                obj_str.Append(", @WAGEDAY_OT3HHMM ");

                obj_str.Append(", @WAGEDAY_ALLOWANCE ");

                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                obj_conn.doOpenTransaction();

                System.Text.StringBuilder obj_str2 = new System.Text.StringBuilder();

                obj_str2.Append(" DELETE FROM HRM_TR_ATTWAGEDAY");
                obj_str2.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str2.Append(" AND WORKER_CODE='" + emp + "'");
                obj_str2.Append(" AND PAYITEM_DATE='" + date.ToString("MM/dd/yyyy") + "'");

                blnResult = obj_conn.doExecuteSQL_transaction(obj_str2.ToString());

                if (blnResult)
                {
                    SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
                    obj_cmd.Transaction = obj_conn.getTransaction();

                    obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@WAGEDAY_DATE", SqlDbType.DateTime);
                    obj_cmd.Parameters.Add("@WAGEDAY_DAYTYPE", SqlDbType.VarChar);

                    obj_cmd.Parameters.Add("@WAGEDAY_WAGEMONEY", SqlDbType.Decimal);
                    obj_cmd.Parameters.Add("@WAGEDAY_WAGEHHMM", SqlDbType.VarChar);

                    obj_cmd.Parameters.Add("@WAGEDAY_LATEMONEY", SqlDbType.Decimal);
                    obj_cmd.Parameters.Add("@WAGEDAY_LATEHHMM", SqlDbType.VarChar);

                    obj_cmd.Parameters.Add("@WAGEDAY_LEAVEMONEY", SqlDbType.Decimal);
                    obj_cmd.Parameters.Add("@WAGEDAY_LEAVEHHMM", SqlDbType.VarChar);

                    obj_cmd.Parameters.Add("@WAGEDAY_ABSENTMONEY", SqlDbType.Decimal);
                    obj_cmd.Parameters.Add("@WAGEDAY_ABSENTHHMM", SqlDbType.VarChar);

                    obj_cmd.Parameters.Add("@WAGEDAY_OT1MONEY", SqlDbType.Decimal);
                    obj_cmd.Parameters.Add("@WAGEDAY_OT1HHMM", SqlDbType.VarChar);

                    obj_cmd.Parameters.Add("@WAGEDAY_OT15MONEY", SqlDbType.Decimal);
                    obj_cmd.Parameters.Add("@WAGEDAY_OT15HHMM", SqlDbType.VarChar);

                    obj_cmd.Parameters.Add("@WAGEDAY_OT2MONEY", SqlDbType.Decimal);
                    obj_cmd.Parameters.Add("@WAGEDAY_OT2HHMM", SqlDbType.VarChar);

                    obj_cmd.Parameters.Add("@WAGEDAY_OT3MONEY", SqlDbType.Decimal);
                    obj_cmd.Parameters.Add("@WAGEDAY_OT3HHMM", SqlDbType.VarChar);

                    obj_cmd.Parameters.Add("@WAGEDAY_ALLOWANCE", SqlDbType.Decimal);

                    obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime);
                    obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit);

                    foreach (cls_TRAttwageday model in list_model)
                    {

                        obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                        obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                        obj_cmd.Parameters["@WAGEDAY_DATE"].Value = model.wageday_date;
                        obj_cmd.Parameters["@WAGEDAY_DAYTYPE"].Value = model.wageday_daytype;

                        obj_cmd.Parameters["@PAYITEM_AMOUNT"].Value = model.wageday_wagemoney;
                        obj_cmd.Parameters["@PAYITEM_AMOUNT"].Value = model.wageday_wagehhmm;

                        obj_cmd.Parameters["@WAGEDAY_LATEMONEY"].Value = model.wageday_latemoney;
                        obj_cmd.Parameters["@WAGEDAY_LATEHHMM"].Value = model.wageday_latehhmm;

                        obj_cmd.Parameters["@WAGEDAY_LEAVEMONEY"].Value = model.wageday_leavemoney;
                        obj_cmd.Parameters["@WAGEDAY_LEAVEHHMM"].Value = model.wageday_leavehhmm;

                        obj_cmd.Parameters["@WAGEDAY_ABSENTMONEY"].Value = model.wageday_absentmoney;
                        obj_cmd.Parameters["@WAGEDAY_ABSENTHHMM"].Value = model.wageday_absenthhmm;

                        obj_cmd.Parameters["@WAGEDAY_OT1MONEY"].Value = model.wageday_ot1money;
                        obj_cmd.Parameters["@WAGEDAY_OT1HHMM"].Value = model.wageday_ot1hhmm;

                        obj_cmd.Parameters["@WAGEDAY_OT15MONEY"].Value = model.wageday_ot15money;
                        obj_cmd.Parameters["@WAGEDAY_OT15HHMM"].Value = model.wageday_ot15hhmm;

                        obj_cmd.Parameters["@WAGEDAY_OT2MONEY"].Value = model.wageday_ot2money;
                        obj_cmd.Parameters["@WAGEDAY_OT2HHMM"].Value = model.wageday_ot2hhmm;

                        obj_cmd.Parameters["@WAGEDAY_OT3MONEY"].Value = model.wageday_ot3money;
                        obj_cmd.Parameters["@WAGEDAY_OT3HHMM"].Value = model.wageday_ot3hhmm;

                        obj_cmd.Parameters["@WAGEDAY_ALLOWANCE"].Value = model.wageday_allowance;

                        obj_cmd.Parameters["@CREATED_BY"].Value = model.modified_by;
                        obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                        obj_cmd.Parameters["@FLAG"].Value = false;

                        obj_cmd.ExecuteNonQuery();

                    }

                    blnResult = obj_conn.doCommit();

                    if (!blnResult)
                    {
                        obj_conn.doRollback();
                    }
                }
                else
                {
                    obj_conn.doRollback();
                }

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Attwageday.insert)" + ex.ToString();
                obj_conn.doRollback();
            }
            finally
            {
                obj_conn.doClose();
            }

            return blnResult;
        }

        public bool update(cls_TRAttwageday model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_TR_ATTWAGEDAY SET ");

                obj_str.Append(" WAGEDAY_DAYTYPE=@WAGEDAY_DAYTYPE ");

                obj_str.Append(", WAGEDAY_WAGEMONEY=@WAGEDAY_WAGEMONEY ");
                obj_str.Append(", WAGEDAY_WAGEHHMM=@WAGEDAY_WAGEHHMM ");

                obj_str.Append(", WAGEDAY_LATEMONEY=@WAGEDAY_LATEMONEY ");
                obj_str.Append(", WAGEDAY_LATEHHMM=@WAGEDAY_LATEHHMM ");

                obj_str.Append(", WAGEDAY_LEAVEMONEY=@WAGEDAY_LEAVEMONEY ");
                obj_str.Append(", WAGEDAY_LEAVEHHMM=@WAGEDAY_LEAVEHHMM ");

                obj_str.Append(", WAGEDAY_ABSENTMONEY=@WAGEDAY_ABSENTMONEY ");
                obj_str.Append(", WAGEDAY_ABSENTHHMM=@WAGEDAY_ABSENTHHMM ");

                obj_str.Append(", WAGEDAY_OT1MONEY=@WAGEDAY_OT1MONEY ");
                obj_str.Append(", WAGEDAY_OT1HHMM=@WAGEDAY_OT1HHMM ");

                obj_str.Append(", WAGEDAY_OT15MONEY=@WAGEDAY_OT15MONEY ");
                obj_str.Append(", WAGEDAY_OT15HHMM=@WAGEDAY_OT15HHMM ");

                obj_str.Append(", WAGEDAY_OT2MONEY=@WAGEDAY_OT2MONEY ");
                obj_str.Append(", WAGEDAY_OT2HHMM=@WAGEDAY_OT2HHMM ");

                obj_str.Append(", WAGEDAY_OT3MONEY=@WAGEDAY_OT3MONEY ");
                obj_str.Append(", WAGEDAY_OT3HHMM=@WAGEDAY_OT3HHMM ");

                obj_str.Append(", WAGEDAY_ALLOWANCE=@WAGEDAY_ALLOWANCE ");

                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");

                obj_str.Append(" WHERE COMPANY_CODE=@COMPANY_CODE ");
                obj_str.Append(" AND WORKER_CODE=@WORKER_CODE ");
                obj_str.Append(" AND WAGEDAY_DATE=@WAGEDAY_DATE ");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@WAGEDAY_DAYTYPE", SqlDbType.VarChar); obj_cmd.Parameters["@WAGEDAY_DAYTYPE"].Value = model.wageday_daytype;

                obj_cmd.Parameters.Add("@WAGEDAY_WAGEMONEY", SqlDbType.Decimal); obj_cmd.Parameters["@PAYITEM_AMOUNT"].Value = model.wageday_wagemoney;
                obj_cmd.Parameters.Add("@WAGEDAY_WAGEHHMM", SqlDbType.VarChar); obj_cmd.Parameters["@PAYITEM_AMOUNT"].Value = model.wageday_wagehhmm;

                obj_cmd.Parameters.Add("@WAGEDAY_LATEMONEY", SqlDbType.Decimal); obj_cmd.Parameters["@WAGEDAY_LATEMONEY"].Value = model.wageday_latemoney;
                obj_cmd.Parameters.Add("@WAGEDAY_LATEHHMM", SqlDbType.VarChar); obj_cmd.Parameters["@WAGEDAY_LATEHHMM"].Value = model.wageday_latehhmm;

                obj_cmd.Parameters.Add("@WAGEDAY_LEAVEMONEY", SqlDbType.Decimal); obj_cmd.Parameters["@WAGEDAY_LEAVEMONEY"].Value = model.wageday_leavemoney;
                obj_cmd.Parameters.Add("@WAGEDAY_LEAVEHHMM", SqlDbType.VarChar); obj_cmd.Parameters["@WAGEDAY_LEAVEHHMM"].Value = model.wageday_leavehhmm;

                obj_cmd.Parameters.Add("@WAGEDAY_ABSENTMONEY", SqlDbType.Decimal); obj_cmd.Parameters["@WAGEDAY_ABSENTMONEY"].Value = model.wageday_absentmoney;
                obj_cmd.Parameters.Add("@WAGEDAY_ABSENTHHMM", SqlDbType.VarChar); obj_cmd.Parameters["@WAGEDAY_ABSENTHHMM"].Value = model.wageday_absenthhmm;

                obj_cmd.Parameters.Add("@WAGEDAY_OT1MONEY", SqlDbType.Decimal); obj_cmd.Parameters["@WAGEDAY_OT1MONEY"].Value = model.wageday_ot1money;
                obj_cmd.Parameters.Add("@WAGEDAY_OT1HHMM", SqlDbType.VarChar); obj_cmd.Parameters["@WAGEDAY_OT1HHMM"].Value = model.wageday_ot1hhmm;

                obj_cmd.Parameters.Add("@WAGEDAY_OT15MONEY", SqlDbType.Decimal); obj_cmd.Parameters["@WAGEDAY_OT15MONEY"].Value = model.wageday_ot15money;
                obj_cmd.Parameters.Add("@WAGEDAY_OT15HHMM", SqlDbType.VarChar); obj_cmd.Parameters["@WAGEDAY_OT15HHMM"].Value = model.wageday_ot15hhmm;

                obj_cmd.Parameters.Add("@WAGEDAY_OT2MONEY", SqlDbType.Decimal); obj_cmd.Parameters["@WAGEDAY_OT2MONEY"].Value = model.wageday_ot2money;
                obj_cmd.Parameters.Add("@WAGEDAY_OT2HHMM", SqlDbType.VarChar); obj_cmd.Parameters["@WAGEDAY_OT2HHMM"].Value = model.wageday_ot2hhmm;

                obj_cmd.Parameters.Add("@WAGEDAY_OT3MONEY", SqlDbType.Decimal); obj_cmd.Parameters["@WAGEDAY_OT3MONEY"].Value = model.wageday_ot3money;
                obj_cmd.Parameters.Add("@WAGEDAY_OT3HHMM", SqlDbType.VarChar); obj_cmd.Parameters["@WAGEDAY_OT3HHMM"].Value = model.wageday_ot3hhmm;

                obj_cmd.Parameters.Add("@WAGEDAY_ALLOWANCE", SqlDbType.Decimal); obj_cmd.Parameters["@WAGEDAY_ALLOWANCE"].Value = model.wageday_allowance;

                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = DateTime.Now;

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                obj_cmd.Parameters.Add("@WAGEDAY_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@WAGEDAY_DATE"].Value = model.wageday_date;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Attwageday.update)" + ex.ToString();
            }

            return blnResult;
        }
          
    }
}
