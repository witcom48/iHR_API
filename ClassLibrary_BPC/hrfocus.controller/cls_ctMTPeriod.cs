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
    public class cls_ctMTPeriod
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctMTPeriod() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_MTPeriod> getData(string condition)
        {
            List<cls_MTPeriod> list_model = new List<cls_MTPeriod>();
            cls_MTPeriod model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("COMPANY_CODE");

                obj_str.Append(", PERIOD_ID");
                obj_str.Append(", PERIOD_TYPE");
                obj_str.Append(", EMPTYPE_CODE");
                obj_str.Append(", YEAR_CODE");
                obj_str.Append(", PERIOD_NO");


                obj_str.Append(", ISNULL(PERIOD_NAME_TH, '') AS PERIOD_NAME_TH");
                obj_str.Append(", ISNULL(PERIOD_NAME_EN, '') AS PERIOD_NAME_EN");

                obj_str.Append(", PERIOD_FROM");
                obj_str.Append(", PERIOD_TO");
                obj_str.Append(", PERIOD_PAYMENT");

                obj_str.Append(", ISNULL(PERIOD_DAYONPERIOD, '0') AS PERIOD_DAYONPERIOD");
                obj_str.Append(", ISNULL(PERIOD_CLOSETA, '0') AS PERIOD_CLOSETA");
                obj_str.Append(", ISNULL(PERIOD_CLOSEPR, '0') AS PERIOD_CLOSEPR");

                obj_str.Append(", ISNULL(CHANGESTATUS_BY, '') AS CHANGESTATUS_BY");
                obj_str.Append(", ISNULL(CHANGESTATUS_DATE, '' ) AS CHANGESTATUS_DATE");

                obj_str.Append(", ISNULL(MODIFIED_BY, CREATED_BY) AS MODIFIED_BY");
                obj_str.Append(", ISNULL(MODIFIED_DATE, CREATED_DATE) AS MODIFIED_DATE");

                
                obj_str.Append(" FROM HRM_MT_PERIOD");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY COMPANY_CODE, YEAR_CODE, EMPTYPE_CODE, PERIOD_NO");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_MTPeriod();
                                       

                    model.company_code = dr["COMPANY_CODE"].ToString();
                    model.period_id = Convert.ToInt32(dr["PERIOD_ID"]);
                    model.period_type = dr["PERIOD_TYPE"].ToString();
                    model.emptype_code = dr["EMPTYPE_CODE"].ToString();
                    model.year_code = dr["YEAR_CODE"].ToString();
                    model.period_no = dr["PERIOD_NO"].ToString();

                    model.period_name_th = dr["PERIOD_NAME_TH"].ToString();
                    model.period_name_en = dr["PERIOD_NAME_EN"].ToString();

                    model.period_from = Convert.ToDateTime(dr["PERIOD_FROM"]);
                    model.period_to = Convert.ToDateTime(dr["PERIOD_TO"]);
                    model.period_payment = Convert.ToDateTime(dr["PERIOD_PAYMENT"]);

                    model.period_dayonperiod = Convert.ToBoolean(dr["PERIOD_DAYONPERIOD"]);
                    model.period_closeta = Convert.ToBoolean(dr["PERIOD_CLOSETA"]);
                    model.period_closepr = Convert.ToBoolean(dr["PERIOD_CLOSEPR"]);
                    model.changestatus_by = dr["CHANGESTATUS_BY"].ToString();
                    model.changestatus_date = Convert.ToDateTime(dr["CHANGESTATUS_DATE"]);

                    model.modified_by = dr["MODIFIED_BY"].ToString();
                    model.modified_date = Convert.ToDateTime(dr["MODIFIED_DATE"]);
                                                                                                                      
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Period.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_MTPeriod> getDataByFillter(string id, string com, string type, string year, string emptype)
        {
            string strCondition = "";

            if (!id.Equals(""))
                strCondition += " AND PERIOD_ID='" + id + "'";

            if (!com.Equals(""))
                strCondition += " AND COMPANY_CODE='" + com + "'";

            if (!type.Equals(""))
                strCondition += " AND PERIOD_TYPE='" + type + "'";

            if (!year.Equals(""))
                strCondition += " AND YEAR_CODE='" + year + "'";

            if (!emptype.Equals(""))
                strCondition += " AND EMPTYPE_CODE='" + emptype + "'";
            
            return this.getData(strCondition);
        }
        //กรองวันที่
        public List<cls_MTPeriod> getDataByFillter2(string id, string com, string type, string year, string emptype, DateTime datefrom, DateTime dateto)
        {
            string strCondition = "";

            if (!id.Equals(""))
                strCondition += " AND PERIOD_ID='" + id + "'";

            if (!com.Equals(""))
                strCondition += " AND COMPANY_CODE='" + com + "'";

            if (!type.Equals(""))
                strCondition += " AND PERIOD_TYPE='" + type + "'";

            if (!year.Equals(""))
                strCondition += " AND YEAR_CODE='" + year + "'";

            if (!emptype.Equals(""))
                strCondition += " AND EMPTYPE_CODE='" + emptype + "'";

            strCondition += " AND (HRM_MT_PERIOD.PERIOD_FROM BETWEEN '" + datefrom.ToString("MM/dd/yyyy") + "' AND '" + dateto.ToString("MM/dd/yyyy") + "')";

            return this.getData(strCondition);
        }
        public bool checkDataOld(string com, string type, string year, string emptype, string no)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT PERIOD_ID");
                obj_str.Append(" FROM HRM_MT_PERIOD");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND PERIOD_TYPE='" + type + "'");
                obj_str.Append(" AND YEAR_CODE='" + year + "'");
                obj_str.Append(" AND EMPTYPE_CODE='" + emptype + "'");
                obj_str.Append(" AND PERIOD_NO='" + no + "'");
                                                                
                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Period.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public int getNextID()
        {
            int intResult = 1;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT MAX(PERIOD_ID) ");
                obj_str.Append(" FROM HRM_MT_PERIOD");             

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    intResult = Convert.ToInt32(dt.Rows[0][0]) + 1;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Period.getNextID)" + ex.ToString();
            }

            return intResult;
        }

        public bool delete(string id)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_MT_PERIOD");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND PERIOD_ID='" + id + "'");
                                              
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Period.delete)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(cls_MTPeriod model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.company_code, model.period_type, model.year_code, model.emptype_code, model.period_no))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_MT_PERIOD");
                obj_str.Append(" (");
                obj_str.Append("COMPANY_CODE ");
                obj_str.Append(", PERIOD_ID ");

                obj_str.Append(", PERIOD_TYPE ");
                obj_str.Append(", EMPTYPE_CODE ");
                obj_str.Append(", YEAR_CODE ");
                obj_str.Append(", PERIOD_NO ");
                
                obj_str.Append(", PERIOD_NAME_TH ");
                obj_str.Append(", PERIOD_NAME_EN ");

                obj_str.Append(", PERIOD_FROM ");
                obj_str.Append(", PERIOD_TO ");
                obj_str.Append(", PERIOD_PAYMENT ");
                obj_str.Append(", PERIOD_DAYONPERIOD ");
                obj_str.Append(", PERIOD_CLOSETA ");
                obj_str.Append(", PERIOD_CLOSEPR ");

                obj_str.Append(", CHANGESTATUS_BY ");
                obj_str.Append(", CHANGESTATUS_DATE ");

                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@COMPANY_CODE ");
                obj_str.Append(", @PERIOD_ID ");

                obj_str.Append(", @PERIOD_TYPE ");
                obj_str.Append(", @EMPTYPE_CODE ");
                obj_str.Append(", @YEAR_CODE ");
                obj_str.Append(", @PERIOD_NO ");

                obj_str.Append(", @PERIOD_NAME_TH ");
                obj_str.Append(", @PERIOD_NAME_EN ");

                obj_str.Append(", @PERIOD_FROM ");
                obj_str.Append(", @PERIOD_TO ");
                obj_str.Append(", @PERIOD_PAYMENT ");
                obj_str.Append(", @PERIOD_DAYONPERIOD ");
                obj_str.Append(", @PERIOD_CLOSETA ");
                obj_str.Append(", @PERIOD_CLOSEPR ");

                obj_str.Append(", @CHANGESTATUS_BY ");
                obj_str.Append(", @CHANGESTATUS_DATE ");

                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@PERIOD_ID", SqlDbType.Int); obj_cmd.Parameters["@PERIOD_ID"].Value = this.getNextID();

                obj_cmd.Parameters.Add("@PERIOD_TYPE", SqlDbType.VarChar); obj_cmd.Parameters["@PERIOD_TYPE"].Value = model.period_type;
                obj_cmd.Parameters.Add("@EMPTYPE_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@EMPTYPE_CODE"].Value = model.emptype_code;
                obj_cmd.Parameters.Add("@YEAR_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@YEAR_CODE"].Value = model.year_code;
                obj_cmd.Parameters.Add("@PERIOD_NO", SqlDbType.VarChar); obj_cmd.Parameters["@PERIOD_NO"].Value = model.period_no;

                obj_cmd.Parameters.Add("@PERIOD_NAME_TH", SqlDbType.VarChar); obj_cmd.Parameters["@PERIOD_NAME_TH"].Value = model.period_name_th;
                obj_cmd.Parameters.Add("@PERIOD_NAME_EN", SqlDbType.VarChar); obj_cmd.Parameters["@PERIOD_NAME_EN"].Value = model.period_name_en;

                obj_cmd.Parameters.Add("@PERIOD_FROM", SqlDbType.Date); obj_cmd.Parameters["@PERIOD_FROM"].Value = model.period_from;
                obj_cmd.Parameters.Add("@PERIOD_TO", SqlDbType.Date); obj_cmd.Parameters["@PERIOD_TO"].Value = model.period_to;
                obj_cmd.Parameters.Add("@PERIOD_PAYMENT", SqlDbType.Date); obj_cmd.Parameters["@PERIOD_PAYMENT"].Value = model.period_payment;

                obj_cmd.Parameters.Add("@PERIOD_DAYONPERIOD", SqlDbType.Bit); obj_cmd.Parameters["@PERIOD_DAYONPERIOD"].Value = model.period_dayonperiod;
                obj_cmd.Parameters.Add("@PERIOD_CLOSETA", SqlDbType.Bit); obj_cmd.Parameters["@PERIOD_CLOSETA"].Value = model.period_closeta;
                obj_cmd.Parameters.Add("@PERIOD_CLOSEPR", SqlDbType.Bit); obj_cmd.Parameters["@PERIOD_CLOSEPR"].Value = model.period_closepr;



                obj_cmd.Parameters.Add("@CHANGESTATUS_BY", SqlDbType.VarChar); obj_cmd.Parameters["@CHANGESTATUS_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@CHANGESTATUS_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@CHANGESTATUS_DATE"].Value = DateTime.Now;

                obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@CREATED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;
     
                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();
                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Period.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_MTPeriod model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_MT_PERIOD SET ");

                obj_str.Append(" PERIOD_NAME_TH=@PERIOD_NAME_TH ");
                obj_str.Append(", PERIOD_NAME_EN=@PERIOD_NAME_EN ");

                obj_str.Append(", PERIOD_FROM=@PERIOD_FROM ");
                obj_str.Append(", PERIOD_TO=@PERIOD_TO ");
                obj_str.Append(", PERIOD_PAYMENT=@PERIOD_PAYMENT ");
                obj_str.Append(", PERIOD_DAYONPERIOD=@PERIOD_DAYONPERIOD ");

                obj_str.Append(", PERIOD_CLOSETA=@PERIOD_CLOSETA ");
                obj_str.Append(", PERIOD_CLOSEPR=@PERIOD_CLOSEPR ");

                obj_str.Append(", CHANGESTATUS_BY=@CHANGESTATUS_BY ");
                obj_str.Append(", CHANGESTATUS_DATE=@CHANGESTATUS_DATE ");

               

                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");
                obj_str.Append(", FLAG=@FLAG ");

                obj_str.Append(" WHERE PERIOD_ID=@PERIOD_ID ");
                                
                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@PERIOD_NAME_TH", SqlDbType.VarChar); obj_cmd.Parameters["@PERIOD_NAME_TH"].Value = model.period_name_th;
                obj_cmd.Parameters.Add("@PERIOD_NAME_EN", SqlDbType.VarChar); obj_cmd.Parameters["@PERIOD_NAME_EN"].Value = model.period_name_en;

                obj_cmd.Parameters.Add("@PERIOD_FROM", SqlDbType.Date); obj_cmd.Parameters["@PERIOD_FROM"].Value = model.period_from;
                obj_cmd.Parameters.Add("@PERIOD_TO", SqlDbType.Date); obj_cmd.Parameters["@PERIOD_TO"].Value = model.period_to;
                obj_cmd.Parameters.Add("@PERIOD_PAYMENT", SqlDbType.Date); obj_cmd.Parameters["@PERIOD_PAYMENT"].Value = model.period_payment;

                obj_cmd.Parameters.Add("@PERIOD_DAYONPERIOD", SqlDbType.Bit); obj_cmd.Parameters["@PERIOD_DAYONPERIOD"].Value = model.period_dayonperiod;
                obj_cmd.Parameters.Add("@PERIOD_CLOSETA", SqlDbType.Bit); obj_cmd.Parameters["@PERIOD_CLOSETA"].Value = model.period_closeta;
                obj_cmd.Parameters.Add("@PERIOD_CLOSEPR", SqlDbType.Bit); obj_cmd.Parameters["@PERIOD_CLOSEPR"].Value = model.period_closepr;


                obj_cmd.Parameters.Add("@CHANGESTATUS_BY", SqlDbType.VarChar); obj_cmd.Parameters["@CHANGESTATUS_BY"].Value = model.changestatus_by;
                obj_cmd.Parameters.Add("@CHANGESTATUS_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@CHANGESTATUS_DATE"].Value = DateTime.Now;

                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;

                obj_cmd.Parameters.Add("@PERIOD_ID", SqlDbType.Int); obj_cmd.Parameters["@PERIOD_ID"].Value = model.period_id;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Period.update)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
