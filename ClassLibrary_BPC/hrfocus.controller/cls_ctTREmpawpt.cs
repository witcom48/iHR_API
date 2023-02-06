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
    public class cls_ctTREmpawpt
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTREmpawpt() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TREmpawpt> getData(string condition)
        {
            List<cls_TREmpawpt> list_model = new List<cls_TREmpawpt>();
            cls_TREmpawpt model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("COMPANY_CODE ");
                obj_str.Append(", WORKER_CODE ");

                obj_str.Append(", EMPAWPT_NO");
                obj_str.Append(", EMPAWPT_START");
                obj_str.Append(", EMPAWPT_FINISH");
                obj_str.Append(", EMPAWPT_TYPE");
                obj_str.Append(", ISNULL(EMPAWPT_LOCATION, '') AS EMPAWPT_LOCATION");
                obj_str.Append(", ISNULL(EMPAWPT_REASON, '') AS EMPAWPT_REASON");
                obj_str.Append(", ISNULL(EMPAWPT_NOTE, '') AS EMPAWPT_NOTE");

                obj_str.Append(", ISNULL(MODIFIED_BY, CREATED_BY) AS MODIFIED_BY");
                obj_str.Append(", ISNULL(MODIFIED_DATE, CREATED_DATE) AS MODIFIED_DATE");
                

                obj_str.Append(" FROM HRM_TR_EMPAWPT");

                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY COMPANY_CODE, WORKER_CODE, EMPAWPT_FINISH DESC");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TREmpawpt();

                    model.company_code = dr["COMPANY_CODE"].ToString();
                    model.worker_code = dr["WORKER_CODE"].ToString();

                    model.empawpt_no = Convert.ToInt32(dr["EMPAWPT_NO"]);
                    model.empawpt_start = Convert.ToDateTime(dr["EMPAWPT_START"]);
                    model.empawpt_finish = Convert.ToDateTime(dr["EMPAWPT_FINISH"]);
                    model.empawpt_type = dr["EMPAWPT_TYPE"].ToString();
                    model.empawpt_location = dr["EMPAWPT_LOCATION"].ToString();
                    model.empawpt_reason = dr["EMPAWPT_REASON"].ToString();
                    model.empawpt_note = dr["EMPAWPT_NOTE"].ToString();
                                        
                    model.modified_by = dr["MODIFIED_BY"].ToString();
                    model.modified_date = Convert.ToDateTime(dr["MODIFIED_DATE"]);
                                                                                                                      
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Empawpt.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TREmpawpt> getDataByFillter(string com, string worker, string type)
        {
            string strCondition = " AND HRM_TR_EMPAWPT.COMPANY_CODE='" + com + "'";

            if (!worker.Equals(""))
                strCondition += " AND HRM_TR_EMPAWPT.WORKER_CODE='" + worker + "'";

            if (!type.Equals(""))
                strCondition += " AND HRM_TR_EMPAWPT.EMPAWPT_TYPE='" + type + "'";

                        
            return this.getData(strCondition);
        }

        public List<cls_TREmpawpt> getDataByCreateDate(string com, DateTime create)
        {
            string strCondition = " AND HRM_TR_EMPAWPT.COMPANY_CODE='" + com + "'";

            strCondition = " AND (HRM_TR_EMPAWPT.CREATED_DATE BETWEEN CONVERT(datetime,'" + create.ToString(Config.FormatDateSQL) + "') AND CONVERT(datetime, '" + create.ToString(Config.FormatDateSQL) + " 23:59:59:998') )";

            return this.getData(strCondition);
        }

        public bool checkDataOld(string com, string worker, string no)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT EMPAWPT_NO");
                obj_str.Append(" FROM HRM_TR_EMPAWPT");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND WORKER_CODE='" + worker + "'");
                obj_str.Append(" AND EMPAWPT_NO='" + no + "'");
                                                                
                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empawpt.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public int getID(string com, string empid)
        {
            int intResult = 0;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT EMPAWPT_NO ");
                obj_str.Append(" FROM HRM_TR_EMPAWPT");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND WORKER_CODE='" + empid + "'");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    intResult = Convert.ToInt32(dt.Rows[0][0]);
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empawpt.getID)" + ex.ToString();
            }

            return intResult;
        }

        public int getNextID(string com, string worker)
        {
            int intResult = 1;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT MAX(EMPAWPT_NO) ");
                obj_str.Append(" FROM HRM_TR_EMPAWPT");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND WORKER_CODE='" + worker + "'");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    intResult = Convert.ToInt32(dt.Rows[0][0]) + 1;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empawpt.getNextID)" + ex.ToString();
            }

            return intResult;
        }

        public int getNextID(cls_ctConnection obj_conn, string com, string worker)
        {
            int intResult = 1;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT MAX(EMPBENEFIT_ID) ");
                obj_str.Append(" FROM HRM_TR_EMPAWPT");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND WORKER_CODE='" + worker + "'");

                DataTable dt = obj_conn.doGetTableTransaction(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    intResult = Convert.ToInt32(dt.Rows[0][0]) + 1;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empawpt.getNextID)" + ex.ToString();
            }

            return intResult;
        }

        public bool delete(string com, string worker, string no)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_TR_EMPAWPT");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND WORKER_CODE='" + worker + "'");
                obj_str.Append(" AND EMPAWPT_NO='" + no + "'");
                                              
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Empawpt.delete)" + ex.ToString();
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

                obj_str.Append(" DELETE FROM HRM_TR_EMPAWPT");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND WORKER_CODE='" + emp + "'");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Empawpt.clear)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(cls_TREmpawpt model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.company_code, model.worker_code, model.empawpt_no.ToString()))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_EMPAWPT");
                obj_str.Append(" (");             
                obj_str.Append("COMPANY_CODE ");
                obj_str.Append(", WORKER_CODE ");

                obj_str.Append(", EMPAWPT_NO ");
                obj_str.Append(", EMPAWPT_START ");
                obj_str.Append(", EMPAWPT_FINISH ");
                obj_str.Append(", EMPAWPT_TYPE ");
                obj_str.Append(", EMPAWPT_LOCATION ");
                obj_str.Append(", EMPAWPT_REASON ");
                obj_str.Append(", EMPAWPT_NOTE ");
               
                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@COMPANY_CODE ");
                obj_str.Append(", @WORKER_CODE ");

                obj_str.Append(", @EMPAWPT_NO ");
                obj_str.Append(", @EMPAWPT_START ");
                obj_str.Append(", @EMPAWPT_FINISH ");
                obj_str.Append(", @EMPAWPT_TYPE ");
                obj_str.Append(", @EMPAWPT_LOCATION ");
                obj_str.Append(", @EMPAWPT_REASON ");
                obj_str.Append(", @EMPAWPT_NOTE ");

                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
                                
                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;

                obj_cmd.Parameters.Add("@EMPAWPT_NO", SqlDbType.Int); obj_cmd.Parameters["@EMPAWPT_NO"].Value = this.getNextID(model.company_code, model.worker_code);
                obj_cmd.Parameters.Add("@EMPAWPT_START", SqlDbType.DateTime); obj_cmd.Parameters["@EMPAWPT_START"].Value = model.empawpt_start;
                obj_cmd.Parameters.Add("@EMPAWPT_FINISH", SqlDbType.DateTime); obj_cmd.Parameters["@EMPAWPT_FINISH"].Value = model.empawpt_finish;
                obj_cmd.Parameters.Add("@EMPAWPT_TYPE", SqlDbType.VarChar); obj_cmd.Parameters["@EMPAWPT_TYPE"].Value = model.empawpt_type;
                obj_cmd.Parameters.Add("@EMPAWPT_LOCATION", SqlDbType.VarChar); obj_cmd.Parameters["@EMPAWPT_LOCATION"].Value = model.empawpt_location;
                obj_cmd.Parameters.Add("@EMPAWPT_REASON", SqlDbType.VarChar); obj_cmd.Parameters["@EMPAWPT_REASON"].Value = model.empawpt_reason;
                obj_cmd.Parameters.Add("@EMPAWPT_NOTE", SqlDbType.VarChar); obj_cmd.Parameters["@EMPAWPT_NOTE"].Value = model.empawpt_note;


                obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@CREATED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;
     
                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();
                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empawpt.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_TREmpawpt model)
        {
            string strResult = model.empawpt_no.ToString();
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

              
                obj_str.Append("UPDATE HRM_TR_EMPAWPT SET ");

                obj_str.Append(" EMPAWPT_START=@EMPAWPT_START ");
                obj_str.Append(", EMPAWPT_FINISH=@EMPAWPT_FINISH ");
                obj_str.Append(", EMPAWPT_TYPE=@EMPAWPT_TYPE ");
                obj_str.Append(", EMPAWPT_LOCATION=@EMPAWPT_LOCATION ");
                obj_str.Append(", EMPAWPT_REASON=@EMPAWPT_REASON ");
                obj_str.Append(", EMPAWPT_NOTE=@EMPAWPT_NOTE ");

                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");
                obj_str.Append(", FLAG=@FLAG ");

                obj_str.Append(" WHERE COMPANY_CODE=@COMPANY_CODE ");
                obj_str.Append(" AND WORKER_CODE=@WORKER_CODE ");
                obj_str.Append(" AND EMPAWPT_NO=@EMPAWPT_NO ");
               
                
                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
                if (model.empawpt_no == 0)
                {
                    strResult = this.getID(model.company_code, model.worker_code).ToString();
                }
                obj_cmd.Parameters.Add("@EMPAWPT_START", SqlDbType.DateTime); obj_cmd.Parameters["@EMPAWPT_START"].Value = model.empawpt_start;
                obj_cmd.Parameters.Add("@EMPAWPT_FINISH", SqlDbType.DateTime); obj_cmd.Parameters["@EMPAWPT_FINISH"].Value = model.empawpt_finish;
                obj_cmd.Parameters.Add("@EMPAWPT_TYPE", SqlDbType.VarChar); obj_cmd.Parameters["@EMPAWPT_TYPE"].Value = model.empawpt_type;
                obj_cmd.Parameters.Add("@EMPAWPT_LOCATION", SqlDbType.VarChar); obj_cmd.Parameters["@EMPAWPT_LOCATION"].Value = model.empawpt_location;
                obj_cmd.Parameters.Add("@EMPAWPT_REASON", SqlDbType.VarChar); obj_cmd.Parameters["@EMPAWPT_REASON"].Value = model.empawpt_reason;
                obj_cmd.Parameters.Add("@EMPAWPT_NOTE", SqlDbType.VarChar); obj_cmd.Parameters["@EMPAWPT_NOTE"].Value = model.empawpt_note;
                
                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                obj_cmd.Parameters.Add("@EMPAWPT_NO", SqlDbType.Int); obj_cmd.Parameters["@EMPAWPT_NO"].Value = strResult;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empawpt.update)" + ex.ToString();
            }

            return blnResult;
        }

    }
}
