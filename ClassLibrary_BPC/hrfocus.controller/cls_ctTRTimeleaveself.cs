﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using ClassLibrary_BPC.hrfocus.model;
namespace ClassLibrary_BPC.hrfocus.controller
{
    public class cls_ctTRTimeleaveself
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTRTimeleaveself() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TRTimeleaveself> getData(string condition)
        {
            List<cls_TRTimeleaveself> list_model = new List<cls_TRTimeleaveself>();
            cls_TRTimeleaveself model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append(" SELF_TR_TIMELEAVE.COMPANY_CODE");
                obj_str.Append(", SELF_TR_TIMELEAVE.WORKER_CODE");

                obj_str.Append(", LEAVE_NAME_TH AS LEAVE_DETAIL_TH");
                obj_str.Append(", INITIAL_NAME_TH + WORKER_FNAME_TH + ' ' + WORKER_LNAME_TH AS WORKER_DETAIL_TH");
                obj_str.Append(", LEAVE_NAME_EN AS LEAVE_DETAIL_EN");
                obj_str.Append(", INITIAL_NAME_EN + WORKER_FNAME_EN + ' ' + WORKER_LNAME_EN AS WORKER_DETAIL_EN");

                obj_str.Append(", TIMELEAVE_ID");
                obj_str.Append(", ISNULL(TIMELEAVE_DOC, '') AS TIMELEAVE_DOC");

                obj_str.Append(", TIMELEAVE_FROMDATE");
                obj_str.Append(", TIMELEAVE_TODATE");

                obj_str.Append(", TIMELEAVE_TYPE");
                obj_str.Append(", ISNULL(TIMELEAVE_MIN, 0) AS TIMELEAVE_MIN");

                obj_str.Append(", ISNULL(TIMELEAVE_ACTUALDAY, 0) AS TIMELEAVE_ACTUALDAY");
                obj_str.Append(", ISNULL(TIMELEAVE_INCHOLIDAY, '0') AS TIMELEAVE_INCHOLIDAY");
                obj_str.Append(", ISNULL(TIMELEAVE_DEDUCT, '0') AS TIMELEAVE_DEDUCT");

                obj_str.Append(", ISNULL(TIMELEAVE_NOTE, '') AS TIMELEAVE_NOTE");

                obj_str.Append(", SELF_TR_TIMELEAVE.LEAVE_CODE");
                obj_str.Append(", ISNULL(HRM_MT_REASON.REASON_CODE, '') AS REASON_CODE");
                obj_str.Append(", ISNULL(HRM_MT_REASON.REASON_NAME_TH, '') AS REASON_NAME_TH");
                obj_str.Append(", ISNULL(HRM_MT_REASON.REASON_NAME_EN, '') AS REASON_NAME_EN");
                obj_str.Append(", ISNULL(STATUS, 0) AS STATUS");

                obj_str.Append(", ISNULL(SELF_TR_TIMELEAVE.MODIFIED_BY, SELF_TR_TIMELEAVE.CREATED_BY) AS MODIFIED_BY");
                obj_str.Append(", ISNULL(SELF_TR_TIMELEAVE.MODIFIED_DATE, SELF_TR_TIMELEAVE.CREATED_DATE) AS MODIFIED_DATE");
                obj_str.Append(", SELF_MT_JOBTABLE.STATUS_JOB");
                obj_str.Append(", ISNULL(SELF_TR_TIMELEAVE.REJECT_NOTE, '') AS REJECT_NOTE");
                obj_str.Append(" FROM SELF_TR_TIMELEAVE");

                obj_str.Append(" INNER JOIN HRM_MT_WORKER ON HRM_MT_WORKER.COMPANY_CODE=SELF_TR_TIMELEAVE.COMPANY_CODE AND HRM_MT_WORKER.WORKER_CODE=SELF_TR_TIMELEAVE.WORKER_CODE");
                obj_str.Append(" INNER JOIN HRM_MT_INITIAL ON HRM_MT_INITIAL.INITIAL_CODE=HRM_MT_WORKER.WORKER_INITIAL ");
                obj_str.Append(" INNER JOIN HRM_MT_LEAVE ON HRM_MT_LEAVE.COMPANY_CODE=SELF_TR_TIMELEAVE.COMPANY_CODE AND HRM_MT_LEAVE.LEAVE_CODE=SELF_TR_TIMELEAVE.LEAVE_CODE");
                obj_str.Append(" INNER JOIN HRM_MT_REASON ON HRM_MT_REASON.REASON_CODE=SELF_TR_TIMELEAVE.REASON_CODE AND HRM_MT_REASON.REASON_GROUP = 'LEAVE'");
                obj_str.Append(" INNER JOIN SELF_MT_JOBTABLE ON SELF_TR_TIMELEAVE.COMPANY_CODE=SELF_MT_JOBTABLE.COMPANY_CODE AND SELF_MT_JOBTABLE.JOB_ID = SELF_TR_TIMELEAVE.TIMELEAVE_ID AND SELF_MT_JOBTABLE.JOB_TYPE = 'LEA'");

                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY SELF_TR_TIMELEAVE.COMPANY_CODE, SELF_TR_TIMELEAVE.WORKER_CODE, TIMELEAVE_FROMDATE");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TRTimeleaveself();

                    model.company_code = dr["COMPANY_CODE"].ToString();
                    model.worker_code = dr["WORKER_CODE"].ToString();

                    model.worker_detail_th = dr["WORKER_DETAIL_TH"].ToString();
                    model.leave_detail_th = dr["LEAVE_DETAIL_TH"].ToString();
                    model.worker_detail_en = dr["WORKER_DETAIL_EN"].ToString();
                    model.leave_detail_en = dr["LEAVE_DETAIL_EN"].ToString();

                    model.timeleave_id = Convert.ToInt32(dr["TIMELEAVE_ID"]);
                    model.timeleave_doc = dr["TIMELEAVE_DOC"].ToString();

                    model.timeleave_fromdate = Convert.ToDateTime(dr["TIMELEAVE_FROMDATE"]);
                    model.timeleave_todate = Convert.ToDateTime(dr["TIMELEAVE_TODATE"]);

                    model.timeleave_type = dr["TIMELEAVE_TYPE"].ToString();
                    model.timeleave_min = Convert.ToInt32(dr["TIMELEAVE_MIN"]);

                    model.timeleave_actualday = Convert.ToInt32(dr["TIMELEAVE_ACTUALDAY"]);
                    model.timeleave_incholiday = Convert.ToBoolean(dr["TIMELEAVE_INCHOLIDAY"]);
                    model.timeleave_deduct = Convert.ToBoolean(dr["TIMELEAVE_DEDUCT"]);

                    model.timeleave_note = dr["TIMELEAVE_NOTE"].ToString();
                    model.leave_code = dr["LEAVE_CODE"].ToString();
                    model.reason_code = dr["REASON_CODE"].ToString();
                    model.reason_en = dr["REASON_NAME_EN"].ToString();
                    model.reason_th = dr["REASON_NAME_TH"].ToString();
                    model.status = Convert.ToInt32(dr["STATUS"]);
                    model.status_job = dr["STATUS_JOB"].ToString();

                    model.modified_by = dr["MODIFIED_BY"].ToString();
                    model.modified_date = Convert.ToDateTime(dr["MODIFIED_DATE"]);
                    model.reject_note = dr["REJECT_NOTE"].ToString();
                    list_model.Add(model);
                }

            }
            catch (Exception ex)
            {
                Message = "ERROR::(Timeleave.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TRTimeleaveself> getDataByFillter(int id, int status, string com, string emp, string datefrom, string dateto)
        {
            string strCondition = "";

            strCondition += " AND SELF_TR_TIMELEAVE.COMPANY_CODE='" + com + "'";
            if (!status.Equals(1))
            {
                strCondition += " AND SELF_TR_TIMELEAVE.STATUS='" + status + "'";
            }
            if (!emp.Equals(""))
            {
                strCondition += " AND SELF_TR_TIMELEAVE.WORKER_CODE='" + emp + "'";
            }
            if (!id.Equals(0))
            {
                strCondition += " AND SELF_TR_TIMELEAVE.TIMELEAVE_ID='" + id + "'";
            }
            if (!datefrom.Equals("") && !dateto.Equals(""))
            {
                strCondition += " AND (TIMELEAVE_FROMDATE BETWEEN '" + datefrom + "' AND '" + dateto + "'";
                strCondition += "  OR TIMELEAVE_TODATE BETWEEN '" + datefrom + "' AND '" + dateto + "')";
            }


            return this.getData(strCondition);
        }

        public List<cls_TRTimeleaveself> getDataByFillteracc(string id, string status, string com, string emp, DateTime datefrom, DateTime dateto)
        {
            string strCondition = "";

            strCondition += " AND SELF_TR_TIMELEAVE.COMPANY_CODE='" + com + "'";

            if (!emp.Equals(""))
            {
                strCondition += " AND SELF_TR_TIMELEAVE.WORKER_CODE='" + emp + "'";
            }
            if (!status.Equals(""))
            {
                strCondition += " AND SELF_TR_TIMELEAVE.STATUS NOT IN ('" + status + "')";
            }
            if (!id.Equals(""))
            {
                strCondition += " AND SELF_TR_TIMELEAVE.TIMELEAVE_ID='" + id + "'";
            }
            strCondition += " AND (TIMELEAVE_FROMDATE BETWEEN '" + datefrom.ToString("MM/dd/yyyy") + "' AND '" + dateto.ToString("MM/dd/yyyy") + "'";
            strCondition += "  OR TIMELEAVE_TODATE BETWEEN '" + datefrom.ToString("MM/dd/yyyy") + "' AND '" + dateto.ToString("MM/dd/yyyy") + "')";


            return this.getData(strCondition);
        }


        public cls_TRTimeleaveself getDataByID(int id)
        {

            string strCondition = " AND SELF_TR_TIMELEAVE.TIMELEAVE_ID='" + id + "'";

            List<cls_TRTimeleaveself> list_model = this.getData(strCondition);

            if (list_model.Count > 0)
                return list_model[0];
            else
                return null;

        }

        public cls_TRTimeleaveself getDataByDOC(string doc)
        {

            string strCondition = " AND SELF_TR_TIMELEAVE.TIMELEAVE_DOC='" + doc + "'";

            List<cls_TRTimeleaveself> list_model = this.getData(strCondition);

            if (list_model.Count > 0)
                return list_model[0];
            else
                return null;

        }


        public bool checkDataOld(string com, string emp, DateTime datefrom, DateTime dateto, string leave_type, string leave_code)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT COMPANY_CODE");
                obj_str.Append(" FROM SELF_TR_TIMELEAVE");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND WORKER_CODE='" + emp + "'");
                obj_str.Append(" AND TIMELEAVE_FROMDATE='" + datefrom.ToString("MM/dd/yyyy") + "'");
                obj_str.Append(" AND TIMELEAVE_TODATE='" + dateto.ToString("MM/dd/yyyy") + "'");
                obj_str.Append(" AND TIMELEAVE_TYPE='" + leave_type + "'");
                obj_str.Append(" AND LEAVE_CODE='" + leave_code + "'");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Timeleave.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public bool checkDataOld(cls_TRTimeleaveself model,string status)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT COMPANY_CODE");
                obj_str.Append(" FROM SELF_TR_TIMELEAVE");
                obj_str.Append(" WHERE COMPANY_CODE='" + model.company_code + "'");
                obj_str.Append(" AND WORKER_CODE='" + model.worker_code + "'");
                obj_str.Append(" AND ('" + model.timeleave_fromdate.ToString("MM/dd/yyyy") + "' BETWEEN TIMELEAVE_FROMDATE AND TIMELEAVE_TODATE OR '" + model.timeleave_todate.ToString("MM/dd/yyyy") + "' BETWEEN TIMELEAVE_FROMDATE AND TIMELEAVE_TODATE)");
                //obj_str.Append(" AND TIMELEAVE_TODATE='" + model.timeleave_todate.ToString("MM/dd/yyyy") + "'");
                if (!status.Equals(""))
                {
                    obj_str.Append(" AND STATUS IN ("+status+")");
                }
                //obj_str.Append(" AND LEAVE_CODE='" + model.leave_code + "'");
                //obj_str.Append(" AND TIMELEAVE_TYPE='" + model.timeleave_type + "'");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Timeleave.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public int getNextID()
        {
            int intResult = 1;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT MAX(TIMELEAVE_ID) ");
                obj_str.Append(" FROM SELF_TR_TIMELEAVE");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    intResult = Convert.ToInt32(dt.Rows[0][0]) + 1;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Timeleave.getNextID)" + ex.ToString();
            }

            return intResult;
        }

        public bool delete(int id)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM SELF_TR_TIMELEAVE");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND TIMELEAVE_ID='" + id + "'");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Timeleave.delete)" + ex.ToString();
            }

            return blnResult;
        }
        public bool delete(string doc)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM SELF_TR_TIMELEAVE");
                obj_str.Append(" WHERE TIMELEAVE_DOC='" + doc + "'");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Timeleave.delete)" + ex.ToString();
            }

            return blnResult;
        }

        public string insert(cls_TRTimeleaveself model)
        {
            string blnResult = "";
            try
            {
                //-- Check data old
                if (this.checkDataOld(model,""))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();
                int id = this.getNextID();
                obj_str.Append("INSERT INTO SELF_TR_TIMELEAVE");
                obj_str.Append(" (");
                obj_str.Append("COMPANY_CODE ");
                obj_str.Append(", WORKER_CODE ");

                obj_str.Append(", TIMELEAVE_ID ");
                obj_str.Append(", TIMELEAVE_DOC ");

                obj_str.Append(", TIMELEAVE_FROMDATE ");
                obj_str.Append(", TIMELEAVE_TODATE ");

                obj_str.Append(", TIMELEAVE_TYPE ");
                obj_str.Append(", TIMELEAVE_MIN ");

                obj_str.Append(", TIMELEAVE_ACTUALDAY ");
                obj_str.Append(", TIMELEAVE_INCHOLIDAY ");
                obj_str.Append(", TIMELEAVE_DEDUCT ");

                obj_str.Append(", TIMELEAVE_NOTE ");
                obj_str.Append(", LEAVE_CODE ");
                obj_str.Append(", REASON_CODE ");
                obj_str.Append(", STATUS ");

                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@COMPANY_CODE ");
                obj_str.Append(", @WORKER_CODE ");

                obj_str.Append(", @TIMELEAVE_ID ");
                obj_str.Append(", @TIMELEAVE_DOC ");

                obj_str.Append(", @TIMELEAVE_FROMDATE ");
                obj_str.Append(", @TIMELEAVE_TODATE ");

                obj_str.Append(", @TIMELEAVE_TYPE ");
                obj_str.Append(", @TIMELEAVE_MIN ");

                obj_str.Append(", @TIMELEAVE_ACTUALDAY ");
                obj_str.Append(", @TIMELEAVE_INCHOLIDAY ");
                obj_str.Append(", @TIMELEAVE_DEDUCT ");

                obj_str.Append(", @TIMELEAVE_NOTE ");
                obj_str.Append(", @LEAVE_CODE ");
                obj_str.Append(", @REASON_CODE ");
                obj_str.Append(", @STATUS ");

                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;


                obj_cmd.Parameters.Add("@TIMELEAVE_ID", SqlDbType.Int); obj_cmd.Parameters["@TIMELEAVE_ID"].Value = id;
                obj_cmd.Parameters.Add("@TIMELEAVE_DOC", SqlDbType.VarChar); obj_cmd.Parameters["@TIMELEAVE_DOC"].Value = model.timeleave_doc;
                obj_cmd.Parameters.Add("@TIMELEAVE_FROMDATE", SqlDbType.Date); obj_cmd.Parameters["@TIMELEAVE_FROMDATE"].Value = model.timeleave_fromdate;
                obj_cmd.Parameters.Add("@TIMELEAVE_TODATE", SqlDbType.Date); obj_cmd.Parameters["@TIMELEAVE_TODATE"].Value = model.timeleave_todate;
                obj_cmd.Parameters.Add("@TIMELEAVE_TYPE", SqlDbType.VarChar); obj_cmd.Parameters["@TIMELEAVE_TYPE"].Value = model.timeleave_type;

                obj_cmd.Parameters.Add("@TIMELEAVE_MIN", SqlDbType.Int); obj_cmd.Parameters["@TIMELEAVE_MIN"].Value = model.timeleave_min;
                obj_cmd.Parameters.Add("@TIMELEAVE_ACTUALDAY", SqlDbType.Int); obj_cmd.Parameters["@TIMELEAVE_ACTUALDAY"].Value = model.timeleave_actualday;

                obj_cmd.Parameters.Add("@TIMELEAVE_INCHOLIDAY", SqlDbType.Bit); obj_cmd.Parameters["@TIMELEAVE_INCHOLIDAY"].Value = model.timeleave_incholiday;
                obj_cmd.Parameters.Add("@TIMELEAVE_DEDUCT", SqlDbType.Bit); obj_cmd.Parameters["@TIMELEAVE_DEDUCT"].Value = model.timeleave_deduct;

                obj_cmd.Parameters.Add("@TIMELEAVE_NOTE", SqlDbType.VarChar); obj_cmd.Parameters["@TIMELEAVE_NOTE"].Value = model.timeleave_note;
                obj_cmd.Parameters.Add("@LEAVE_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@LEAVE_CODE"].Value = model.leave_code;
                obj_cmd.Parameters.Add("@REASON_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@REASON_CODE"].Value = model.reason_code;
                obj_cmd.Parameters.Add("@STATUS", SqlDbType.Int); obj_cmd.Parameters["@STATUS"].Value = model.status;

                obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@CREATED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = model.flag;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();
                blnResult = id.ToString(); ;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Timeleave.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public string update(cls_TRTimeleaveself model)
        {
            string blnResult = "";
            if (model.timeleave_id.Equals(0))
            {
                return "D";
            }
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE SELF_TR_TIMELEAVE SET ");

                obj_str.Append(" TIMELEAVE_DOC=@TIMELEAVE_DOC ");
                obj_str.Append(", TIMELEAVE_FROMDATE=@TIMELEAVE_FROMDATE ");
                obj_str.Append(", TIMELEAVE_TODATE=@TIMELEAVE_TODATE ");

                obj_str.Append(", TIMELEAVE_TYPE=@TIMELEAVE_TYPE ");
                obj_str.Append(", TIMELEAVE_MIN=@TIMELEAVE_MIN ");
                obj_str.Append(", TIMELEAVE_ACTUALDAY=@TIMELEAVE_ACTUALDAY ");
                obj_str.Append(", TIMELEAVE_INCHOLIDAY=@TIMELEAVE_INCHOLIDAY ");
                obj_str.Append(", TIMELEAVE_DEDUCT=@TIMELEAVE_DEDUCT ");
                obj_str.Append(", TIMELEAVE_NOTE=@TIMELEAVE_NOTE ");

                obj_str.Append(", LEAVE_CODE=@LEAVE_CODE ");
                obj_str.Append(", REASON_CODE=@REASON_CODE ");
                obj_str.Append(", STATUS=@STATUS ");
                obj_str.Append(", REJECT_NOTE=@REJECT_NOTE ");

                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");

                obj_str.Append(" WHERE TIMELEAVE_ID=@TIMELEAVE_ID ");


                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@TIMELEAVE_DOC", SqlDbType.VarChar); obj_cmd.Parameters["@TIMELEAVE_DOC"].Value = model.timeleave_doc;
                obj_cmd.Parameters.Add("@TIMELEAVE_FROMDATE", SqlDbType.Date); obj_cmd.Parameters["@TIMELEAVE_FROMDATE"].Value = model.timeleave_fromdate;
                obj_cmd.Parameters.Add("@TIMELEAVE_TODATE", SqlDbType.Date); obj_cmd.Parameters["@TIMELEAVE_TODATE"].Value = model.timeleave_todate;
                obj_cmd.Parameters.Add("@TIMELEAVE_TYPE", SqlDbType.VarChar); obj_cmd.Parameters["@TIMELEAVE_TYPE"].Value = model.timeleave_type;

                obj_cmd.Parameters.Add("@TIMELEAVE_MIN", SqlDbType.Int); obj_cmd.Parameters["@TIMELEAVE_MIN"].Value = model.timeleave_min;
                obj_cmd.Parameters.Add("@TIMELEAVE_ACTUALDAY", SqlDbType.Int); obj_cmd.Parameters["@TIMELEAVE_ACTUALDAY"].Value = model.timeleave_actualday;

                obj_cmd.Parameters.Add("@TIMELEAVE_INCHOLIDAY", SqlDbType.Bit); obj_cmd.Parameters["@TIMELEAVE_INCHOLIDAY"].Value = model.timeleave_incholiday;
                obj_cmd.Parameters.Add("@TIMELEAVE_DEDUCT", SqlDbType.Bit); obj_cmd.Parameters["@TIMELEAVE_DEDUCT"].Value = model.timeleave_deduct;

                obj_cmd.Parameters.Add("@TIMELEAVE_NOTE", SqlDbType.VarChar); obj_cmd.Parameters["@TIMELEAVE_NOTE"].Value = model.timeleave_note;
                obj_cmd.Parameters.Add("@LEAVE_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@LEAVE_CODE"].Value = model.leave_code;
                obj_cmd.Parameters.Add("@REASON_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@REASON_CODE"].Value = model.reason_code;
                obj_cmd.Parameters.Add("@STATUS", SqlDbType.Int); obj_cmd.Parameters["@STATUS"].Value = model.status;
                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = DateTime.Now;

                obj_cmd.Parameters.Add("@REJECT_NOTE", SqlDbType.VarChar); obj_cmd.Parameters["@REJECT_NOTE"].Value = model.reject_note;

                obj_cmd.Parameters.Add("@TIMELEAVE_ID", SqlDbType.Int); obj_cmd.Parameters["@TIMELEAVE_ID"].Value = model.timeleave_id;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = model.timeleave_id.ToString();
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Timeleave.update)" + ex.ToString();
            }

            return blnResult;
        }

        internal List<cls_TRTimeleaveself> getDataByFillter(int p1, string p2, string com, string p3, DateTime dateFrom, DateTime dateTo)
        {
            throw new NotImplementedException();
        }
    }
}
