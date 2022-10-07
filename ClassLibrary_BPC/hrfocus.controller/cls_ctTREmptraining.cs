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
    public class cls_ctTREmptraining
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTREmptraining() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TREmptraining> getData(string condition)
        {
            List<cls_TREmptraining> list_model = new List<cls_TREmptraining>();
            cls_TREmptraining model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("COMPANY_CODE");
                obj_str.Append(", WORKER_CODE");
                obj_str.Append(", EMPTRAINING_NO");
                obj_str.Append(", ISNULL(INSTITUTE_CODE, '') AS INSTITUTE_CODE");
                obj_str.Append(", ISNULL(COURSE_CODE, '') AS COURSE_CODE");
                obj_str.Append(", ISNULL(INSTITUTE_OTHER, '') AS INSTITUTE_OTHER");
                obj_str.Append(", ISNULL(COURSE_OTHER, '') AS COURSE_OTHER");                
                obj_str.Append(", ISNULL(EMPTRAINING_START, '01/01/1900') AS EMPTRAINING_START");
                obj_str.Append(", ISNULL(EMPTRAINING_FINISH, '01/01/1900') AS EMPTRAINING_FINISH");
                obj_str.Append(", ISNULL(EMPTRAINING_STATUS, '') AS EMPTRAINING_STATUS");
                obj_str.Append(", ISNULL(EMPTRAINING_HOURS, 0) AS EMPTRAINING_HOURS");
                obj_str.Append(", ISNULL(EMPTRAINING_COST, 0) AS EMPTRAINING_COST");
                obj_str.Append(", ISNULL(EMPTRAINING_NOTE, '') AS EMPTRAINING_NOTE");

                obj_str.Append(" FROM HRM_TR_EMPTRAINING");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY COMPANY_CODE, WORKER_CODE, EMPTRAINING_NO");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TREmptraining();

                    model.company_code = Convert.ToString(dr["COMPANY_CODE"]);

                    model.worker_code = Convert.ToString(dr["WORKER_CODE"]);
                    model.emptraining_no = Convert.ToInt32(dr["EMPTRAINING_NO"]);
                    model.institute_code = Convert.ToString(dr["INSTITUTE_CODE"]);
                    model.course_code = Convert.ToString(dr["COURSE_CODE"]);
                    model.institute_other = Convert.ToString(dr["INSTITUTE_OTHER"]);
                    model.course_other = Convert.ToString(dr["COURSE_OTHER"]);
                    
                    model.emptraining_start = Convert.ToDateTime(dr["EMPTRAINING_START"]);
                    model.emptraining_finish = Convert.ToDateTime(dr["EMPTRAINING_FINISH"]);

                    model.emptraining_status = Convert.ToString(dr["EMPTRAINING_STATUS"]);
                    model.emptraining_hours = Convert.ToDouble(dr["EMPTRAINING_HOURS"]);
                    model.emptraining_cost = Convert.ToDouble(dr["EMPTRAINING_COST"]);
                    model.emptraining_note = Convert.ToString(dr["EMPTRAINING_NOTE"]);

                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Emptraining.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TREmptraining> getDataByFillter(string com, string emp)
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

                obj_str.Append("SELECT COMPANY_CODE");
                obj_str.Append(" FROM HRM_TR_EMPTRAINING");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "' ");
                obj_str.Append(" AND WORKER_CODE='" + emp + "' ");
                obj_str.Append(" AND EMPTRAINING_NO='" + no + "' ");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Emptraining.checkDataOld)" + ex.ToString();
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

                obj_str.Append(" DELETE FROM HRM_TR_EMPTRAINING");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "' ");
                obj_str.Append(" AND WORKER_CODE='" + emp + "' ");
                obj_str.Append(" AND EMPTRAINING_NO='" + no + "' ");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());
                

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Emptraining.delete)" + ex.ToString();
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

                obj_str.Append(" DELETE FROM HRM_TR_EMPTRAINING");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "' ");
                obj_str.Append(" AND WORKER_CODE='" + emp + "' ");
                
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());
            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Emptraining.delete)" + ex.ToString();
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

                obj_str.Append(" DELETE FROM HRM_TR_EMPTRAINING");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND WORKER_CODE='" + emp + "'");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Emptraining.clear)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(cls_TREmptraining model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.company_code, model.worker_code, model.emptraining_no.ToString()))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_EMPTRAINING");
                obj_str.Append(" (");
                obj_str.Append("COMPANY_CODE ");
                obj_str.Append(", WORKER_CODE ");
                obj_str.Append(", EMPTRAINING_NO ");
                obj_str.Append(", INSTITUTE_CODE ");
                obj_str.Append(", COURSE_CODE ");
                obj_str.Append(", INSTITUTE_OTHER ");
                obj_str.Append(", COURSE_OTHER ");                
                obj_str.Append(", EMPTRAINING_START ");
                obj_str.Append(", EMPTRAINING_FINISH ");
                obj_str.Append(", EMPTRAINING_STATUS ");
                obj_str.Append(", EMPTRAINING_HOURS ");
                obj_str.Append(", EMPTRAINING_COST ");
                obj_str.Append(", EMPTRAINING_NOTE ");
                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@COMPANY_CODE ");
                obj_str.Append(", @WORKER_CODE ");
                obj_str.Append(", @EMPTRAINING_NO ");
                obj_str.Append(", @INSTITUTE_CODE ");
                obj_str.Append(", @COURSE_CODE ");
                obj_str.Append(", @INSTITUTE_OTHER ");
                obj_str.Append(", @COURSE_OTHER ");
                obj_str.Append(", @EMPTRAINING_START ");
                obj_str.Append(", @EMPTRAINING_FINISH ");
                obj_str.Append(", @EMPTRAINING_STATUS ");
                obj_str.Append(", @EMPTRAINING_HOURS ");
                obj_str.Append(", @EMPTRAINING_COST ");
                obj_str.Append(", @EMPTRAINING_NOTE ");
                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                obj_cmd.Parameters.Add("@EMPTRAINING_NO", SqlDbType.Int); obj_cmd.Parameters["@EMPTRAINING_NO"].Value = model.emptraining_no;
                obj_cmd.Parameters.Add("@INSTITUTE_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@INSTITUTE_CODE"].Value = model.institute_code;
                obj_cmd.Parameters.Add("@COURSE_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COURSE_CODE"].Value = model.course_code;
                obj_cmd.Parameters.Add("@INSTITUTE_OTHER", SqlDbType.VarChar); obj_cmd.Parameters["@INSTITUTE_OTHER"].Value = model.institute_other;
                obj_cmd.Parameters.Add("@COURSE_OTHER", SqlDbType.VarChar); obj_cmd.Parameters["@COURSE_OTHER"].Value = model.course_other;                
                obj_cmd.Parameters.Add("@EMPTRAINING_START", SqlDbType.DateTime); obj_cmd.Parameters["@EMPTRAINING_START"].Value = model.emptraining_start;
                obj_cmd.Parameters.Add("@EMPTRAINING_FINISH", SqlDbType.DateTime); obj_cmd.Parameters["@EMPTRAINING_FINISH"].Value = model.emptraining_finish;
                obj_cmd.Parameters.Add("@EMPTRAINING_STATUS", SqlDbType.VarChar); obj_cmd.Parameters["@EMPTRAINING_STATUS"].Value = model.emptraining_status;
                obj_cmd.Parameters.Add("@EMPTRAINING_HOURS", SqlDbType.Decimal); obj_cmd.Parameters["@EMPTRAINING_HOURS"].Value = model.emptraining_hours;
                obj_cmd.Parameters.Add("@EMPTRAINING_COST", SqlDbType.Decimal); obj_cmd.Parameters["@EMPTRAINING_COST"].Value = model.emptraining_cost;
                obj_cmd.Parameters.Add("@EMPTRAINING_NOTE", SqlDbType.VarChar); obj_cmd.Parameters["@EMPTRAINING_NOTE"].Value = model.emptraining_note;
                obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@CREATED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {              
                Message = "ERROR::(Emptraining.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(string com, string emp, List<cls_TREmptraining> list_model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_EMPTRAINING");
                obj_str.Append(" (");
                obj_str.Append("COMPANY_CODE ");
                obj_str.Append(", WORKER_CODE ");
                obj_str.Append(", EMPTRAINING_NO ");
                obj_str.Append(", INSTITUTE_CODE ");
                obj_str.Append(", COURSE_CODE ");
                obj_str.Append(", INSTITUTE_OTHER ");
                obj_str.Append(", COURSE_OTHER ");
                obj_str.Append(", EMPTRAINING_START ");
                obj_str.Append(", EMPTRAINING_FINISH ");
                obj_str.Append(", EMPTRAINING_STATUS ");
                obj_str.Append(", EMPTRAINING_HOURS ");
                obj_str.Append(", EMPTRAINING_COST ");
                obj_str.Append(", EMPTRAINING_NOTE ");
                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@COMPANY_CODE ");
                obj_str.Append(", @WORKER_CODE ");
                obj_str.Append(", @EMPTRAINING_NO ");
                obj_str.Append(", @INSTITUTE_CODE ");
                obj_str.Append(", @COURSE_CODE ");
                obj_str.Append(", @INSTITUTE_OTHER ");
                obj_str.Append(", @COURSE_OTHER ");
                obj_str.Append(", @EMPTRAINING_START ");
                obj_str.Append(", @EMPTRAINING_FINISH ");
                obj_str.Append(", @EMPTRAINING_STATUS ");
                obj_str.Append(", @EMPTRAINING_HOURS ");
                obj_str.Append(", @EMPTRAINING_COST ");
                obj_str.Append(", @EMPTRAINING_NOTE ");
                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                obj_conn.doOpenTransaction();

                System.Text.StringBuilder obj_str2 = new System.Text.StringBuilder();

                obj_str2.Append(" DELETE FROM HRM_TR_EMPTRAINING");                
                obj_str2.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str2.Append(" AND WORKER_CODE='" + emp + "'");
                
                blnResult = obj_conn.doExecuteSQL_transaction(obj_str2.ToString());

                if (blnResult)
                {
                    SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
                    obj_cmd.Transaction = obj_conn.getTransaction();

                    obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@EMPTRAINING_NO", SqlDbType.Int);
                    obj_cmd.Parameters.Add("@INSTITUTE_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@COURSE_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@INSTITUTE_OTHER", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@COURSE_OTHER", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@EMPTRAINING_START", SqlDbType.DateTime);
                    obj_cmd.Parameters.Add("@EMPTRAINING_FINISH", SqlDbType.DateTime);
                    obj_cmd.Parameters.Add("@EMPTRAINING_STATUS", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@EMPTRAINING_HOURS", SqlDbType.Decimal);
                    obj_cmd.Parameters.Add("@EMPTRAINING_COST", SqlDbType.Decimal);
                    obj_cmd.Parameters.Add("@EMPTRAINING_NOTE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime);
                    obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit);

                    foreach (cls_TREmptraining model in list_model)
                    {

                        obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                        obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                        obj_cmd.Parameters["@EMPTRAINING_NO"].Value = model.emptraining_no;
                        obj_cmd.Parameters["@INSTITUTE_CODE"].Value = model.institute_code;
                        obj_cmd.Parameters["@COURSE_CODE"].Value = model.course_code;
                        obj_cmd.Parameters["@INSTITUTE_OTHER"].Value = model.institute_other;
                        obj_cmd.Parameters["@COURSE_OTHER"].Value = model.course_other;
                        obj_cmd.Parameters["@EMPTRAINING_START"].Value = model.emptraining_start;
                        obj_cmd.Parameters["@EMPTRAINING_FINISH"].Value = model.emptraining_finish;
                        obj_cmd.Parameters["@EMPTRAINING_STATUS"].Value = model.emptraining_status;
                        obj_cmd.Parameters["@EMPTRAINING_HOURS"].Value = model.emptraining_hours;
                        obj_cmd.Parameters["@EMPTRAINING_COST"].Value = model.emptraining_cost;
                        obj_cmd.Parameters["@EMPTRAINING_NOTE"].Value = model.emptraining_note;
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
                Message = "ERROR::(Emptraining.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_TREmptraining model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_TR_EMPTRAINING SET ");
                
                obj_str.Append("INSTITUTE_CODE=@INSTITUTE_CODE ");
                obj_str.Append(", COURSE_CODE=@COURSE_CODE ");
                obj_str.Append(", INSTITUTE_OTHER=@INSTITUTE_OTHER ");
                obj_str.Append(", COURSE_OTHER=@COURSE_OTHER ");               
                obj_str.Append(", EMPTRAINING_START=@EMPTRAINING_START ");
                obj_str.Append(", EMPTRAINING_FINISH=@EMPTRAINING_FINISH ");
                obj_str.Append(", EMPTRAINING_STATUS=@EMPTRAINING_STATUS ");
                obj_str.Append(", EMPTRAINING_HOURS=@EMPTRAINING_HOURS ");
                obj_str.Append(", EMPTRAINING_COST=@EMPTRAINING_COST ");
                obj_str.Append(", EMPTRAINING_NOTE=@EMPTRAINING_NOTE ");                

                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");

                obj_str.Append(" WHERE COMPANY_CODE=@COMPANY_CODE ");
                obj_str.Append(" AND WORKER_CODE=@WORKER_CODE ");
                obj_str.Append(" AND EMPTRAINING_NO=@EMPTRAINING_NO ");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
                                
                obj_cmd.Parameters.Add("@INSTITUTE_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@INSTITUTE_CODE"].Value = model.institute_code;
                obj_cmd.Parameters.Add("@COURSE_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COURSE_CODE"].Value = model.course_code;
                obj_cmd.Parameters.Add("@INSTITUTE_OTHER", SqlDbType.VarChar); obj_cmd.Parameters["@INSTITUTE_OTHER"].Value = model.institute_other;
                obj_cmd.Parameters.Add("@COURSE_OTHER", SqlDbType.VarChar); obj_cmd.Parameters["@COURSE_OTHER"].Value = model.course_other;
                obj_cmd.Parameters.Add("@EMPTRAINING_START", SqlDbType.DateTime); obj_cmd.Parameters["@EMPTRAINING_START"].Value = model.emptraining_start;
                obj_cmd.Parameters.Add("@EMPTRAINING_FINISH", SqlDbType.DateTime); obj_cmd.Parameters["@EMPTRAINING_FINISH"].Value = model.emptraining_finish;
                obj_cmd.Parameters.Add("@EMPTRAINING_STATUS", SqlDbType.VarChar); obj_cmd.Parameters["@EMPTRAINING_STATUS"].Value = model.emptraining_status;
                obj_cmd.Parameters.Add("@EMPTRAINING_HOURS", SqlDbType.Decimal); obj_cmd.Parameters["@EMPTRAINING_HOURS"].Value = model.emptraining_hours;
                obj_cmd.Parameters.Add("@EMPTRAINING_COST", SqlDbType.Decimal); obj_cmd.Parameters["@EMPTRAINING_COST"].Value = model.emptraining_cost;
                obj_cmd.Parameters.Add("@EMPTRAINING_NOTE", SqlDbType.VarChar); obj_cmd.Parameters["@EMPTRAINING_NOTE"].Value = model.emptraining_note;

                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = DateTime.Now;

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                obj_cmd.Parameters.Add("@EMPTRAINING_NO", SqlDbType.Int); obj_cmd.Parameters["@EMPTRAINING_NO"].Value = model.emptraining_no;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Emptraining.update)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
