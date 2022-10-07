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
    public class cls_ctTREmpexperience
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTREmpexperience() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TREmpexperience> getData(string condition)
        {
            List<cls_TREmpexperience> list_model = new List<cls_TREmpexperience>();
            cls_TREmpexperience model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("COMPANY_CODE");
                obj_str.Append(", WORKER_CODE");
                obj_str.Append(", EMPEXPERIENCE_NO");
                obj_str.Append(", ISNULL(EMPEXPERIENCE_AT, '') AS EMPEXPERIENCE_AT");
                obj_str.Append(", ISNULL(EMPEXPERIENCE_POSITION, '') AS EMPEXPERIENCE_POSITION");
                obj_str.Append(", ISNULL(EMPEXPERIENCE_REASONCHANGE, '') AS EMPEXPERIENCE_REASONCHANGE");                
                obj_str.Append(", ISNULL(EMPEXPERIENCE_START, '01/01/1900') AS EMPEXPERIENCE_START");
                obj_str.Append(", ISNULL(EMPEXPERIENCE_FINISH, '01/01/1900') AS EMPEXPERIENCE_FINISH");
                
                obj_str.Append(" FROM HRM_TR_EMPEXPERIENCE");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY COMPANY_CODE, WORKER_CODE, EMPEXPERIENCE_NO");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TREmpexperience();

                    model.company_code = Convert.ToString(dr["COMPANY_CODE"]);                    
                    model.worker_code = Convert.ToString(dr["WORKER_CODE"]);
                    model.empexperience_no = Convert.ToInt32(dr["EMPEXPERIENCE_NO"]);
                    model.empexperience_at = Convert.ToString(dr["EMPEXPERIENCE_AT"]);
                    model.empexperience_position = Convert.ToString(dr["EMPEXPERIENCE_POSITION"]);
                    model.empexperience_reasonchange = Convert.ToString(dr["EMPEXPERIENCE_REASONCHANGE"]);
            
                    model.empexperience_start = Convert.ToDateTime(dr["EMPEXPERIENCE_START"]);
                    model.empexperience_finish = Convert.ToDateTime(dr["EMPEXPERIENCE_FINISH"]);
                    
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Empexperience.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TREmpexperience> getDataByFillter(string com, string emp)
        {
            string strCondition = "";

            strCondition += " AND COMPANY_CODE='" + com + "'";

            if (!emp.Equals(""))
                strCondition += " AND WORKER_CODE='" + emp + "'";
                        
            return this.getData(strCondition);
        }

        public bool checkDataOld(string com, string emp, string no)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT EMPEXPERIENCE_NO");
                obj_str.Append(" FROM HRM_TR_EMPEXPERIENCE");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "' ");
                obj_str.Append(" AND WORKER_CODE='" + emp + "' ");
                obj_str.Append(" AND EMPEXPERIENCE_NO='" + no + "' ");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empexperience.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }
                
        public bool delete(string com, string emp, string no)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_TR_EMPEXPERIENCE");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "' ");
                obj_str.Append(" AND WORKER_CODE='" + emp + "' ");
                obj_str.Append(" AND EMPEXPERIENCE_NO='" + no + "' ");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());
                

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Empexperience.delete)" + ex.ToString();
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

                obj_str.Append(" DELETE FROM HRM_TR_EMPEXPERIENCE");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND WORKER_CODE='" + emp + "'");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Empexperience.clear)" + ex.ToString();
            }

            return blnResult;
        }

        public bool delete(string com, string emp)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_TR_EMPEXPERIENCE");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "' ");
                obj_str.Append(" AND WORKER_CODE='" + emp + "' ");
                
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());
            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Empexperience.delete)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(cls_TREmpexperience model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.company_code, model.worker_code, model.empexperience_no.ToString()))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_EMPEXPERIENCE");
                obj_str.Append(" (");
                obj_str.Append("COMPANY_CODE ");
                obj_str.Append(", WORKER_CODE ");
                obj_str.Append(", EMPEXPERIENCE_NO ");
                obj_str.Append(", EMPEXPERIENCE_AT ");
                obj_str.Append(", EMPEXPERIENCE_POSITION ");
                obj_str.Append(", EMPEXPERIENCE_REASONCHANGE ");
                obj_str.Append(", EMPEXPERIENCE_START ");
                obj_str.Append(", EMPEXPERIENCE_FINISH ");                
                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@COMPANY_CODE ");
                obj_str.Append(", @WORKER_CODE ");
                obj_str.Append(", @EMPEXPERIENCE_NO ");
                obj_str.Append(", @EMPEXPERIENCE_AT ");
                obj_str.Append(", @EMPEXPERIENCE_POSITION ");
                obj_str.Append(", @EMPEXPERIENCE_REASONCHANGE ");
                obj_str.Append(", @EMPEXPERIENCE_START ");
                obj_str.Append(", @EMPEXPERIENCE_FINISH ");
                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                obj_cmd.Parameters.Add("@EMPEXPERIENCE_NO", SqlDbType.Int); obj_cmd.Parameters["@EMPEXPERIENCE_NO"].Value = model.empexperience_no;
                obj_cmd.Parameters.Add("@EMPEXPERIENCE_AT", SqlDbType.VarChar); obj_cmd.Parameters["@EMPEXPERIENCE_AT"].Value = model.empexperience_at;
                obj_cmd.Parameters.Add("@EMPEXPERIENCE_POSITION", SqlDbType.VarChar); obj_cmd.Parameters["@EMPEXPERIENCE_POSITION"].Value = model.empexperience_position;
                obj_cmd.Parameters.Add("@EMPEXPERIENCE_REASONCHANGE", SqlDbType.VarChar); obj_cmd.Parameters["@EMPEXPERIENCE_REASONCHANGE"].Value = model.empexperience_reasonchange;
                obj_cmd.Parameters.Add("@EMPEXPERIENCE_START", SqlDbType.Date); obj_cmd.Parameters["@EMPEXPERIENCE_START"].Value = model.empexperience_start;
                obj_cmd.Parameters.Add("@EMPEXPERIENCE_FINISH", SqlDbType.Date); obj_cmd.Parameters["@EMPEXPERIENCE_FINISH"].Value = model.empexperience_finish;
                
                obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@CREATED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {              
                Message = "ERROR::(Empexperience.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(string com, string emp, List<cls_TREmpexperience> list_model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_EMPEXPERIENCE");
                obj_str.Append(" (");
                obj_str.Append("COMPANY_CODE ");
                obj_str.Append(", WORKER_CODE ");
                obj_str.Append(", EMPEXPERIENCE_NO ");
                obj_str.Append(", EMPEXPERIENCE_AT ");
                obj_str.Append(", EMPEXPERIENCE_POSITION ");
                obj_str.Append(", EMPEXPERIENCE_REASONCHANGE ");
                obj_str.Append(", EMPEXPERIENCE_START ");
                obj_str.Append(", EMPEXPERIENCE_FINISH ");
                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@COMPANY_CODE ");
                obj_str.Append(", @WORKER_CODE ");
                obj_str.Append(", @EMPEXPERIENCE_NO ");
                obj_str.Append(", @EMPEXPERIENCE_AT ");
                obj_str.Append(", @EMPEXPERIENCE_POSITION ");
                obj_str.Append(", @EMPEXPERIENCE_REASONCHANGE ");
                obj_str.Append(", @EMPEXPERIENCE_START ");
                obj_str.Append(", @EMPEXPERIENCE_FINISH ");
                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                obj_conn.doOpenTransaction();

                System.Text.StringBuilder obj_str2 = new System.Text.StringBuilder();

                obj_str2.Append(" DELETE FROM HRM_TR_EMPEXPERIENCE");                
                obj_str2.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str2.Append(" AND WORKER_CODE='" + emp + "'");
                
                blnResult = obj_conn.doExecuteSQL_transaction(obj_str2.ToString());

                if (blnResult)
                {
                    SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
                    obj_cmd.Transaction = obj_conn.getTransaction();

                    obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@EMPEXPERIENCE_NO", SqlDbType.Int);
                    obj_cmd.Parameters.Add("@EMPEXPERIENCE_AT", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@EMPEXPERIENCE_POSITION", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@EMPEXPERIENCE_REASONCHANGE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@EMPEXPERIENCE_START", SqlDbType.Date);
                    obj_cmd.Parameters.Add("@EMPEXPERIENCE_FINISH", SqlDbType.Date);
                    
                    obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime);
                    obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit);

                    foreach (cls_TREmpexperience model in list_model)
                    {

                        obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                        obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                        obj_cmd.Parameters["@EMPEXPERIENCE_NO"].Value = model.empexperience_no;
                        obj_cmd.Parameters["@EMPEXPERIENCE_AT"].Value = model.empexperience_at;
                        obj_cmd.Parameters["@EMPEXPERIENCE_POSITION"].Value = model.empexperience_position;
                        obj_cmd.Parameters["@EMPEXPERIENCE_REASONCHANGE"].Value = model.empexperience_reasonchange;
                        obj_cmd.Parameters["@EMPEXPERIENCE_START"].Value = model.empexperience_start;
                        obj_cmd.Parameters["@EMPEXPERIENCE_FINISH"].Value = model.empexperience_finish;
                        
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
                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empexperience.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_TREmpexperience model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_TR_EMPEXPERIENCE SET ");
                
                obj_str.Append("EMPEXPERIENCE_AT=@EMPEXPERIENCE_AT ");
                obj_str.Append(", EMPEXPERIENCE_POSITION=@EMPEXPERIENCE_POSITION ");
                obj_str.Append(", EMPEXPERIENCE_REASONCHANGE=@EMPEXPERIENCE_REASONCHANGE ");
                obj_str.Append(", EMPEXPERIENCE_START=@EMPEXPERIENCE_START ");
                obj_str.Append(", EMPEXPERIENCE_FINISH=@EMPEXPERIENCE_FINISH ");
                                
                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");

                obj_str.Append(" WHERE COMPANY_CODE=@COMPANY_CODE ");
                obj_str.Append(" AND WORKER_CODE=@WORKER_CODE ");
                obj_str.Append(" AND EMPEXPERIENCE_NO=@EMPEXPERIENCE_NO ");


                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@EMPEXPERIENCE_AT", SqlDbType.VarChar); obj_cmd.Parameters["@EMPEXPERIENCE_AT"].Value = model.empexperience_at;
                obj_cmd.Parameters.Add("@EMPEXPERIENCE_POSITION", SqlDbType.VarChar); obj_cmd.Parameters["@EMPEXPERIENCE_POSITION"].Value = model.empexperience_position;
                obj_cmd.Parameters.Add("@EMPEXPERIENCE_REASONCHANGE", SqlDbType.VarChar); obj_cmd.Parameters["@EMPEXPERIENCE_REASONCHANGE"].Value = model.empexperience_reasonchange;
                obj_cmd.Parameters.Add("@EMPEXPERIENCE_START", SqlDbType.Date); obj_cmd.Parameters["@EMPEXPERIENCE_START"].Value = model.empexperience_start;
                obj_cmd.Parameters.Add("@EMPEXPERIENCE_FINISH", SqlDbType.Date); obj_cmd.Parameters["@EMPEXPERIENCE_FINISH"].Value = model.empexperience_finish;

                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = DateTime.Now;

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                obj_cmd.Parameters.Add("@EMPEXPERIENCE_NO", SqlDbType.Int); obj_cmd.Parameters["@EMPEXPERIENCE_NO"].Value = model.empexperience_no;
                
                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empexperience.update)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
