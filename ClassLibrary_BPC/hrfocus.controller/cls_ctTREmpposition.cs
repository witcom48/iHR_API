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
    public class cls_ctTREmpposition
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTREmpposition() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TREmpposition> getData(string condition)
        {
            List<cls_TREmpposition> list_model = new List<cls_TREmpposition>();
            cls_TREmpposition model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("EMPPOSITION_ID");
                obj_str.Append(", EMPPOSITION_DATE");
                obj_str.Append(", ISNULL(EMPPOSITION_POSITION, '') AS EMPPOSITION_POSITION");                
                obj_str.Append(", ISNULL(EMPPOSITION_REASON, '') AS EMPPOSITION_REASON");
                obj_str.Append(", COMPANY_CODE");
                obj_str.Append(", WORKER_CODE");                                 
                obj_str.Append(", ISNULL(MODIFIED_BY, CREATED_BY) AS MODIFIED_BY");
                obj_str.Append(", ISNULL(MODIFIED_DATE, CREATED_DATE) AS MODIFIED_DATE");

                obj_str.Append(" FROM HRM_TR_EMPPOSITION");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY COMPANY_CODE, WORKER_CODE, EMPPOSITION_DATE");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TREmpposition();

                    model.empposition_id = Convert.ToInt32(dr["EMPPOSITION_ID"]);
                    model.empposition_date = Convert.ToDateTime(dr["EMPPOSITION_DATE"]);
                    model.empposition_position = dr["EMPPOSITION_POSITION"].ToString();                    

                    model.empposition_reason = dr["EMPPOSITION_REASON"].ToString();

                    model.company_code = dr["COMPANY_CODE"].ToString();
                    model.worker_code = dr["WORKER_CODE"].ToString();
                                       
                    model.modified_by = dr["MODIFIED_BY"].ToString();
                    model.modified_date = Convert.ToDateTime(dr["MODIFIED_DATE"]);
                                                                                                                      
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Empposition.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TREmpposition> getDataByFillter(string com, string worker)
        {
            string strCondition = " AND COMPANY_CODE='" + com + "'";

            if (!worker.Equals(""))
                strCondition += " AND WORKER_CODE='" + worker + "'";

                        
            return this.getData(strCondition);
        }

        public bool checkDataOld(string com, string worker, DateTime date)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT COMPANY_CODE");
                obj_str.Append(" FROM HRM_TR_EMPPOSITION");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND WORKER_CODE='" + worker + "'");
                obj_str.Append(" AND EMPPOSITION_DATE='" + date.ToString("MM/dd/yyyy") + "'");
                                                
                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empdep.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public int getNextID()
        {
            int intResult = 1;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT MAX(EMPPOSITION_ID) ");
                obj_str.Append(" FROM HRM_TR_EMPPOSITION");             

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    intResult = Convert.ToInt32(dt.Rows[0][0]) + 1;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empposition.getNextID)" + ex.ToString();
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

                obj_str.Append(" DELETE FROM HRM_TR_EMPPOSITION");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND EMPPOSITION_ID='" + id + "'");
                                              
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Empposition.delete)" + ex.ToString();
            }

            return blnResult;
        }

        public bool clear(string com, string emp)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_TR_EMPPOSITION");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND WORKER_CODE='" + emp + "'");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Empdep.clear)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(cls_TREmpposition model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.company_code, model.worker_code, model.empposition_date))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_EMPPOSITION");
                obj_str.Append(" (");
                obj_str.Append("EMPPOSITION_ID ");
                obj_str.Append(", EMPPOSITION_DATE ");
                obj_str.Append(", EMPPOSITION_POSITION ");               
                obj_str.Append(", EMPPOSITION_REASON ");
                obj_str.Append(", COMPANY_CODE ");
                obj_str.Append(", WORKER_CODE ");                                
                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@EMPPOSITION_ID ");
                obj_str.Append(", @EMPPOSITION_DATE ");
                obj_str.Append(", @EMPPOSITION_POSITION ");              
                obj_str.Append(", @EMPPOSITION_REASON ");
                obj_str.Append(", @COMPANY_CODE ");
                obj_str.Append(", @WORKER_CODE ");
                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@EMPPOSITION_ID", SqlDbType.Int); obj_cmd.Parameters["@EMPPOSITION_ID"].Value = this.getNextID();
                obj_cmd.Parameters.Add("@EMPPOSITION_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@EMPPOSITION_DATE"].Value = model.empposition_date;
                obj_cmd.Parameters.Add("@EMPPOSITION_POSITION", SqlDbType.VarChar); obj_cmd.Parameters["@EMPPOSITION_POSITION"].Value = model.empposition_position;
                obj_cmd.Parameters.Add("@EMPPOSITION_REASON", SqlDbType.VarChar); obj_cmd.Parameters["@EMPPOSITION_REASON"].Value = model.empposition_reason;
                
                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;

                obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@CREATED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;
     
                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();
                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empposition.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_TREmpposition model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_TR_EMPPOSITION SET ");

                obj_str.Append(" EMPPOSITION_DATE=@EMPPOSITION_DATE ");
                obj_str.Append(", EMPPOSITION_POSITION=@EMPPOSITION_POSITION ");    
                obj_str.Append(", EMPPOSITION_REASON=@EMPPOSITION_REASON ");

                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");
                obj_str.Append(", FLAG=@FLAG ");

                obj_str.Append(" WHERE EMPPOSITION_ID=@EMPPOSITION_ID ");
                               
                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@EMPPOSITION_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@EMPPOSITION_DATE"].Value = model.empposition_date;
                obj_cmd.Parameters.Add("@EMPPOSITION_POSITION", SqlDbType.VarChar); obj_cmd.Parameters["@EMPPOSITION_POSITION"].Value = model.empposition_position;
                obj_cmd.Parameters.Add("@EMPPOSITION_REASON", SqlDbType.VarChar); obj_cmd.Parameters["@EMPPOSITION_REASON"].Value = model.empposition_reason;
                
                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;

                obj_cmd.Parameters.Add("@EMPPOSITION_ID", SqlDbType.Int); obj_cmd.Parameters["@EMPPOSITION_ID"].Value = model.empposition_id;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empposition.update)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
