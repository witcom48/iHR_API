﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary_BPC.hrfocus.model;
using System.Data.SqlClient;
using System.Data;

namespace ClassLibrary_BPC.hrfocus.controller
{
    public class cls_ctTRTimeinput
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTRTimeinput() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TRTimeinput> getData(string condition)
        {
            List<cls_TRTimeinput> list_model = new List<cls_TRTimeinput>();
            cls_TRTimeinput model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("TIMEINPUT_CARD");
                obj_str.Append(", TIMEINPUT_DATE");
                obj_str.Append(", TIMEINPUT_HHMM");
                obj_str.Append(", ISNULL(TIMEINPUT_TERMINAL, '') AS TIMEINPUT_TERMINAL");
                obj_str.Append(", ISNULL(TIMEINPUT_FUNCTION, '') AS TIMEINPUT_FUNCTION");
                obj_str.Append(", ISNULL(TIMEINPUT_COMPARE, 'N') AS TIMEINPUT_COMPARE");

                obj_str.Append(" FROM HRM_TR_TIMEINPUT");
                obj_str.Append(" INNER JOIN HRM_MT_WORKER ON HRM_TR_TIMEINPUT.TIMEINPUT_CARD=HRM_MT_WORKER.WORKER_CARD");

                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY TIMEINPUT_DATE, HRM_MT_WORKER.WORKER_CODE");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TRTimeinput();

                    model.timeinput_card = dr["TIMEINPUT_CARD"].ToString();
                    model.timeinput_date = Convert.ToDateTime(dr["TIMEINPUT_DATE"]);
                    model.timeinput_hhmm = dr["TIMEINPUT_HHMM"].ToString();
                    model.timeinput_terminal = dr["TIMEINPUT_TERMINAL"].ToString();
                    model.timeinput_function = dr["TIMEINPUT_FUNCTION"].ToString();
                    model.timeinput_compare = dr["TIMEINPUT_COMPARE"].ToString();

                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Timeinput.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TRTimeinput> getDataByFillter(string com, string emp, DateTime datefrom, DateTime dateto, bool compare)
        {
            string strCondition = "";

            strCondition += " AND HRM_MT_WORKER.COMPANY_CODE='" + com + "'";
            strCondition += " AND HRM_MT_WORKER.WORKER_CODE='" + emp + "'";
            strCondition += " AND (TIMEINPUT_DATE BETWEEN '" + datefrom.ToString("MM/dd/yyyy") + "' AND '" + dateto.ToString("MM/dd/yyyy") + "')";

            if (compare)
                strCondition += " AND TIMEINPUT_COMPARE='N' ";
            
            return this.getData(strCondition);
        }

        public bool checkDataOld(DateTime date, string card, string time)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT TIMEINPUT_CARD");
                obj_str.Append(" FROM HRM_TR_TIMEINPUT");                
                obj_str.Append(" WHERE TIMEINPUT_DATE='" + date.ToString("MM/dd/yyyy") + "'");
                obj_str.Append(" AND TIMEINPUT_HHMM='" + time + "'");
                obj_str.Append(" AND TIMEINPUT_CARD='" + card + "'");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Timeinput.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public bool delete(DateTime date, string card, string time)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_TR_TIMEINPUT");
                obj_str.Append(" WHERE TIMEINPUT_DATE='" + date.ToString("MM/dd/yyyy") + "'");
                obj_str.Append(" AND TIMEINPUT_HHMM='" + time + "'");
                obj_str.Append(" AND TIMEINPUT_CARD='" + card + "'");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Timeinput.delete)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(cls_TRTimeinput model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.timeinput_date, model.timeinput_card, model.timeinput_hhmm))
                {
                    return true;
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_TIMEINPUT");
                obj_str.Append(" (");
                obj_str.Append("TIMEINPUT_CARD ");
                obj_str.Append(", TIMEINPUT_DATE ");
                obj_str.Append(", TIMEINPUT_HHMM ");
                obj_str.Append(", TIMEINPUT_TERMINAL ");
                obj_str.Append(", TIMEINPUT_FUNCTION ");
                obj_str.Append(", TIMEINPUT_COMPARE ");                
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@TIMEINPUT_CARD ");
                obj_str.Append(", @TIMEINPUT_DATE ");
                obj_str.Append(", @TIMEINPUT_HHMM ");
                obj_str.Append(", @TIMEINPUT_TERMINAL ");
                obj_str.Append(", @TIMEINPUT_FUNCTION ");
                obj_str.Append(", @TIMEINPUT_COMPARE ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@TIMEINPUT_CARD", SqlDbType.VarChar); obj_cmd.Parameters["@TIMEINPUT_CARD"].Value = model.timeinput_card;
                obj_cmd.Parameters.Add("@TIMEINPUT_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@TIMEINPUT_DATE"].Value = model.timeinput_date;
                obj_cmd.Parameters.Add("@TIMEINPUT_HHMM", SqlDbType.VarChar); obj_cmd.Parameters["@TIMEINPUT_HHMM"].Value = model.timeinput_hhmm;
                obj_cmd.Parameters.Add("@TIMEINPUT_TERMINAL", SqlDbType.VarChar); obj_cmd.Parameters["@TIMEINPUT_TERMINAL"].Value = model.timeinput_terminal;
                obj_cmd.Parameters.Add("@TIMEINPUT_FUNCTION", SqlDbType.VarChar); obj_cmd.Parameters["@TIMEINPUT_FUNCTION"].Value = model.timeinput_function;
                obj_cmd.Parameters.Add("@TIMEINPUT_COMPARE", SqlDbType.VarChar); obj_cmd.Parameters["@TIMEINPUT_COMPARE"].Value = model.timeinput_compare;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;
     
                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();
                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Timeinput.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(List<cls_TRTimeinput> list_model)
        {
            bool blnResult = false;
            cls_ctConnection obj_conn = new cls_ctConnection();
            try
            {
                
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_conn.doConnect();
                obj_conn.doOpenTransaction();

                obj_str.Append("INSERT INTO HRM_TR_TIMEINPUT");
                obj_str.Append(" (");
                obj_str.Append("TIMEINPUT_CARD ");
                obj_str.Append(", TIMEINPUT_DATE ");
                obj_str.Append(", TIMEINPUT_HHMM ");
                obj_str.Append(", TIMEINPUT_TERMINAL ");
                obj_str.Append(", TIMEINPUT_FUNCTION ");
                obj_str.Append(", TIMEINPUT_COMPARE ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@TIMEINPUT_CARD ");
                obj_str.Append(", @TIMEINPUT_DATE ");
                obj_str.Append(", @TIMEINPUT_HHMM ");
                obj_str.Append(", @TIMEINPUT_TERMINAL ");
                obj_str.Append(", @TIMEINPUT_FUNCTION ");
                obj_str.Append(", @TIMEINPUT_COMPARE ");
                obj_str.Append(" )");

                
                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
                obj_cmd.Transaction = obj_conn.getTransaction();

                obj_cmd.Parameters.Add("@TIMEINPUT_CARD", SqlDbType.VarChar);
                obj_cmd.Parameters.Add("@TIMEINPUT_DATE", SqlDbType.Date);
                obj_cmd.Parameters.Add("@TIMEINPUT_HHMM", SqlDbType.VarChar);
                obj_cmd.Parameters.Add("@TIMEINPUT_TERMINAL", SqlDbType.VarChar);
                obj_cmd.Parameters.Add("@TIMEINPUT_FUNCTION", SqlDbType.VarChar);
                obj_cmd.Parameters.Add("@TIMEINPUT_COMPARE", SqlDbType.VarChar);
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit);

                foreach (cls_TRTimeinput model in list_model)
                {
                    obj_str = new System.Text.StringBuilder();
                    obj_str.Append(" DELETE FROM HRM_TR_TIMEINPUT");
                    obj_str.Append(" WHERE TIMEINPUT_CARD='" + model.timeinput_card + "'");
                    obj_str.Append(" AND TIMEINPUT_DATE='" + model.timeinput_date.ToString("MM/dd/yyyy") + "'");
                    obj_str.Append(" AND TIMEINPUT_HHMM='" + model.timeinput_hhmm + "'");

                    blnResult = obj_conn.doExecuteSQL_transaction(obj_str.ToString());

                    if (blnResult)
                    {

                        obj_cmd.Parameters["@TIMEINPUT_CARD"].Value = model.timeinput_card;
                        obj_cmd.Parameters["@TIMEINPUT_DATE"].Value = model.timeinput_date;
                        obj_cmd.Parameters["@TIMEINPUT_HHMM"].Value = model.timeinput_hhmm;
                        obj_cmd.Parameters["@TIMEINPUT_TERMINAL"].Value = model.timeinput_terminal;
                        obj_cmd.Parameters["@TIMEINPUT_FUNCTION"].Value = model.timeinput_function;
                        obj_cmd.Parameters["@TIMEINPUT_COMPARE"].Value = model.timeinput_compare;
                        obj_cmd.Parameters["@FLAG"].Value = false;

                        obj_cmd.ExecuteNonQuery();
                    }
                }

                blnResult = obj_conn.doCommit();

                if (!blnResult)
                    obj_conn.doRollback();
                                

                

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Timeinput.insert)" + ex.ToString();
                obj_conn.doRollback();
            }
            finally
            {
                obj_conn.doClose();
            }

            return blnResult;
        }


        public bool update_compare(List<cls_TRTimeinput> list_model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();
                
                obj_conn.doConnect();
                obj_conn.doOpenTransaction();


                obj_str.Append("UPDATE HRM_TR_TIMEINPUT SET TIMEINPUT_COMPARE=@TIMEINPUT_COMPARE");
                obj_str.Append(" WHERE TIMEINPUT_CARD=@TIMEINPUT_CARD ");
                obj_str.Append(" AND TIMEINPUT_DATE=@TIMEINPUT_DATE ");
                obj_str.Append(" AND TIMEINPUT_HHMM=@TIMEINPUT_HHMM ");
                

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
                obj_cmd.Transaction = obj_conn.getTransaction();

                obj_cmd.Parameters.Add("@TIMEINPUT_COMPARE", SqlDbType.VarChar);
                obj_cmd.Parameters.Add("@TIMEINPUT_CARD", SqlDbType.VarChar);
                obj_cmd.Parameters.Add("@TIMEINPUT_DATE", SqlDbType.Date);
                obj_cmd.Parameters.Add("@TIMEINPUT_HHMM", SqlDbType.VarChar);
                
                foreach (cls_TRTimeinput model in list_model)
                {

                    obj_cmd.Parameters["@TIMEINPUT_COMPARE"].Value = model.timeinput_compare;
                    obj_cmd.Parameters["@TIMEINPUT_CARD"].Value = model.timeinput_card;
                    obj_cmd.Parameters["@TIMEINPUT_DATE"].Value = model.timeinput_date;
                    obj_cmd.Parameters["@TIMEINPUT_HHMM"].Value = model.timeinput_hhmm;

                    obj_cmd.ExecuteNonQuery();
                }

                blnResult = obj_conn.doCommit();

                if (!blnResult)
                    obj_conn.doRollback();


                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Timeinput.update_compare)" + ex.ToString();
            }

            return blnResult;
        }

        public bool clear_compare(string card, DateTime datefrom, DateTime dateto)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" UPDATE HRM_TR_TIMEINPUT SET TIMEINPUT_COMPARE='N'");
                obj_str.Append(" WHERE TIMEINPUT_CARD ='" + card + "'");
                obj_str.Append(" AND (TIMEINPUT_DATE BETWEEN '" + datefrom.ToString(Config.FormatDateSQL) + "' AND '" + dateto.ToString(Config.FormatDateSQL) + "')");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Timeinput.delete)" + ex.ToString();
            }

            return blnResult;
        }

    }
}
