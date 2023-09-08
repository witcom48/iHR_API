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
    public class cls_ctTREmpeducation
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTREmpeducation() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TREmpeducation> getData(string condition)
        {
            List<cls_TREmpeducation> list_model = new List<cls_TREmpeducation>();
            cls_TREmpeducation model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("COMPANY_CODE");
                obj_str.Append(", WORKER_CODE");
                obj_str.Append(", EMPEDUCATION_NO");
                obj_str.Append(", ISNULL(INSTITUTE_CODE, '') AS INSTITUTE_CODE");
                obj_str.Append(", ISNULL(FACULTY_CODE, '') AS FACULTY_CODE");
                obj_str.Append(", ISNULL(MAJOR_CODE, '') AS MAJOR_CODE");
                obj_str.Append(", ISNULL(QUALIFICATION_CODE, '') AS QUALIFICATION_CODE");
                obj_str.Append(", ISNULL(EMPEDUCATION_GPA, '') AS EMPEDUCATION_GPA");
                obj_str.Append(", ISNULL(EMPEDUCATION_START, '01/01/1900') AS EMPEDUCATION_START");
                obj_str.Append(", ISNULL(EMPEDUCATION_FINISH, '01/01/1900') AS EMPEDUCATION_FINISH");
                
                obj_str.Append(" FROM HRM_TR_EMPEDUCATION");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY COMPANY_CODE, WORKER_CODE, EMPEDUCATION_NO");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TREmpeducation();

                    model.company_code = Convert.ToString(dr["COMPANY_CODE"]);

                    model.worker_code = Convert.ToString(dr["WORKER_CODE"]);
                    model.empeducation_no = Convert.ToInt32(dr["EMPEDUCATION_NO"]);
                    model.institute_code = Convert.ToString(dr["INSTITUTE_CODE"]);
                    model.faculty_code = Convert.ToString(dr["FACULTY_CODE"]);
                    model.major_code = Convert.ToString(dr["MAJOR_CODE"]);
                    model.qualification_code = Convert.ToString(dr["QUALIFICATION_CODE"]);
                    model.empeducation_gpa = Convert.ToString(dr["EMPEDUCATION_GPA"]);
                    model.empeducation_start = Convert.ToDateTime(dr["EMPEDUCATION_START"]);
                    model.empeducation_finish = Convert.ToDateTime(dr["EMPEDUCATION_FINISH"]);
                    
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Empeducation.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TREmpeducation> getDataByFillter(string com, string emp)
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

                obj_str.Append("SELECT WORKER_CODE");
                obj_str.Append(" FROM HRM_TR_EMPEDUCATION");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "' ");
                obj_str.Append(" AND WORKER_CODE='" + emp + "' ");
                obj_str.Append(" AND EMPEDUCATION_NO='" + no + "' ");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empeducation.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }
        public int getID(string com, string empid)
        {
            int intResult = 0;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT EMPEDUCATION_NO ");
                obj_str.Append(" FROM HRM_TR_EMPEDUCATION");
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
                Message = "ERROR::(Empeducation.getID)" + ex.ToString();
            }

            return intResult;
        }


        public int getNextID(string com, string worker)
        {
            int intResult = 1;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT MAX(EMPEDUCATION_NO) ");
                obj_str.Append(" FROM HRM_TR_EMPEDUCATION");
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
                Message = "ERROR::(Empeducation.getNextID)" + ex.ToString();
            }

            return intResult;
        }

        public int getNextID(cls_ctConnection obj_conn, string com, string worker)
        {
            int intResult = 1;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT MAX(EMPEDUCATION_NO) ");
                obj_str.Append(" FROM HRM_TR_EMPEDUCATION");
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
                Message = "ERROR::(Empeducation.getNextID)" + ex.ToString();
            }

            return intResult;
        }
                
        public bool delete(string com, string emp, string no)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_TR_EMPEDUCATION");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "' ");
                obj_str.Append(" AND WORKER_CODE='" + emp + "' ");
                obj_str.Append(" AND EMPEDUCATION_NO='" + no + "' ");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());
                

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Empeducation.delete)" + ex.ToString();
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

                obj_str.Append(" DELETE FROM HRM_TR_EMPEDUCATION");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "' ");
                obj_str.Append(" AND WORKER_CODE='" + emp + "' ");
                
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());
            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Empeducation.delete)" + ex.ToString();
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

                obj_str.Append(" DELETE FROM HRM_TR_EMPEDUCATION");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND WORKER_CODE='" + emp + "'");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Empeducation.clear)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(cls_TREmpeducation model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.company_code, model.worker_code, model.empeducation_no.ToString()))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_EMPEDUCATION");
                obj_str.Append(" (");
                obj_str.Append("COMPANY_CODE ");
                obj_str.Append(", WORKER_CODE ");
                obj_str.Append(", EMPEDUCATION_NO ");
                obj_str.Append(", INSTITUTE_CODE ");
                obj_str.Append(", FACULTY_CODE ");
                obj_str.Append(", MAJOR_CODE ");
                obj_str.Append(", QUALIFICATION_CODE ");
                obj_str.Append(", EMPEDUCATION_GPA ");
                obj_str.Append(", EMPEDUCATION_START ");
                obj_str.Append(", EMPEDUCATION_FINISH ");
                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@COMPANY_CODE ");
                obj_str.Append(", @WORKER_CODE ");
                obj_str.Append(", @EMPEDUCATION_NO ");
                obj_str.Append(", @INSTITUTE_CODE ");
                obj_str.Append(", @FACULTY_CODE ");
                obj_str.Append(", @MAJOR_CODE ");
                obj_str.Append(", @QUALIFICATION_CODE ");
                obj_str.Append(", @EMPEDUCATION_GPA ");
                obj_str.Append(", @EMPEDUCATION_START ");
                obj_str.Append(", @EMPEDUCATION_FINISH ");
                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                obj_cmd.Parameters.Add("@EMPEDUCATION_NO", SqlDbType.Int); obj_cmd.Parameters["@EMPEDUCATION_NO"].Value = this.getNextID(model.company_code, model.worker_code);
                obj_cmd.Parameters.Add("@INSTITUTE_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@INSTITUTE_CODE"].Value = model.institute_code;
                obj_cmd.Parameters.Add("@FACULTY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@FACULTY_CODE"].Value = model.faculty_code;
                obj_cmd.Parameters.Add("@MAJOR_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@MAJOR_CODE"].Value = model.major_code;
                obj_cmd.Parameters.Add("@QUALIFICATION_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@QUALIFICATION_CODE"].Value = model.qualification_code;
                obj_cmd.Parameters.Add("@EMPEDUCATION_GPA", SqlDbType.VarChar); obj_cmd.Parameters["@EMPEDUCATION_GPA"].Value = model.empeducation_gpa;
                obj_cmd.Parameters.Add("@EMPEDUCATION_START", SqlDbType.DateTime); obj_cmd.Parameters["@EMPEDUCATION_START"].Value = model.empeducation_start;
                obj_cmd.Parameters.Add("@EMPEDUCATION_FINISH", SqlDbType.DateTime); obj_cmd.Parameters["@EMPEDUCATION_FINISH"].Value = model.empeducation_finish;
                
                obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@CREATED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {              
                Message = "ERROR::(Empeducation.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(string com, string emp, List<cls_TREmpeducation> list_model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_EMPEDUCATION");
                obj_str.Append(" (");
                obj_str.Append("COMPANY_CODE ");
                obj_str.Append(", WORKER_CODE ");
                obj_str.Append(", EMPEDUCATION_NO ");
                obj_str.Append(", INSTITUTE_CODE ");
                obj_str.Append(", FACULTY_CODE ");
                obj_str.Append(", MAJOR_CODE ");
                obj_str.Append(", QUALIFICATION_CODE ");
                obj_str.Append(", EMPEDUCATION_GPA ");
                obj_str.Append(", EMPEDUCATION_START ");
                obj_str.Append(", EMPEDUCATION_FINISH ");
                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@COMPANY_CODE ");
                obj_str.Append(", @WORKER_CODE ");
                obj_str.Append(", @EMPEDUCATION_NO ");
                obj_str.Append(", @INSTITUTE_CODE ");
                obj_str.Append(", @FACULTY_CODE ");
                obj_str.Append(", @MAJOR_CODE ");
                obj_str.Append(", @QUALIFICATION_CODE ");
                obj_str.Append(", @EMPEDUCATION_GPA ");
                obj_str.Append(", @EMPEDUCATION_START ");
                obj_str.Append(", @EMPEDUCATION_FINISH ");
                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                obj_conn.doOpenTransaction();

                System.Text.StringBuilder obj_str2 = new System.Text.StringBuilder();

                obj_str2.Append(" DELETE FROM HRM_TR_EMPEDUCATION");                
                obj_str2.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str2.Append(" AND WORKER_CODE='" + emp + "'");
                
                blnResult = obj_conn.doExecuteSQL_transaction(obj_str2.ToString());

                if (blnResult)
                {
                    SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                    obj_cmd.Transaction = obj_conn.getTransaction();

                    obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@EMPEDUCATION_NO", SqlDbType.Int);
                    obj_cmd.Parameters.Add("@INSTITUTE_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@FACULTY_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@MAJOR_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@QUALIFICATION_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@EMPEDUCATION_GPA", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@EMPEDUCATION_START", SqlDbType.DateTime);
                    obj_cmd.Parameters.Add("@EMPEDUCATION_FINISH", SqlDbType.DateTime);

                    obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime);
                    obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit);

                    foreach (cls_TREmpeducation model in list_model)
                    {

                        obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                        obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                        obj_cmd.Parameters["@EMPEDUCATION_NO"].Value = model.empeducation_no;
                        obj_cmd.Parameters["@INSTITUTE_CODE"].Value = model.institute_code;
                        obj_cmd.Parameters["@FACULTY_CODE"].Value = model.faculty_code;
                        obj_cmd.Parameters["@MAJOR_CODE"].Value = model.major_code;
                        obj_cmd.Parameters["@QUALIFICATION_CODE"].Value = model.qualification_code;
                        obj_cmd.Parameters["@EMPEDUCATION_GPA"].Value = model.empeducation_gpa;
                        obj_cmd.Parameters["@EMPEDUCATION_START"].Value = model.empeducation_start;
                        obj_cmd.Parameters["@EMPEDUCATION_FINISH"].Value = model.empeducation_finish;

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
                Message = "ERROR::(Empeducation.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_TREmpeducation model)
        {
            string strResult = model.empeducation_no.ToString();
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_TR_EMPEDUCATION SET ");
                
                obj_str.Append("INSTITUTE_CODE=@INSTITUTE_CODE ");
                obj_str.Append(", FACULTY_CODE=@FACULTY_CODE ");
                obj_str.Append(", MAJOR_CODE=@MAJOR_CODE ");
                obj_str.Append(", QUALIFICATION_CODE=@QUALIFICATION_CODE ");
                obj_str.Append(", EMPEDUCATION_GPA=@EMPEDUCATION_GPA ");
                obj_str.Append(", EMPEDUCATION_START=@EMPEDUCATION_START ");
                obj_str.Append(", EMPEDUCATION_FINISH=@EMPEDUCATION_FINISH ");
                
                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");

                obj_str.Append(" WHERE COMPANY_CODE=@COMPANY_CODE ");
                obj_str.Append(" AND WORKER_CODE=@WORKER_CODE ");
                obj_str.Append(" AND EMPEDUCATION_NO=@EMPEDUCATION_NO ");


                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
                if (model.empeducation_no == 0)
                {
                    strResult = this.getID(model.company_code, model.worker_code).ToString();
                }              
                obj_cmd.Parameters.Add("@INSTITUTE_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@INSTITUTE_CODE"].Value = model.institute_code;
                obj_cmd.Parameters.Add("@FACULTY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@FACULTY_CODE"].Value = model.faculty_code;
                obj_cmd.Parameters.Add("@MAJOR_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@MAJOR_CODE"].Value = model.major_code;
                obj_cmd.Parameters.Add("@QUALIFICATION_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@QUALIFICATION_CODE"].Value = model.qualification_code;
                obj_cmd.Parameters.Add("@EMPEDUCATION_GPA", SqlDbType.VarChar); obj_cmd.Parameters["@EMPEDUCATION_GPA"].Value = model.empeducation_gpa;
                obj_cmd.Parameters.Add("@EMPEDUCATION_START", SqlDbType.DateTime); obj_cmd.Parameters["@EMPEDUCATION_START"].Value = model.empeducation_start;
                obj_cmd.Parameters.Add("@EMPEDUCATION_FINISH", SqlDbType.DateTime); obj_cmd.Parameters["@EMPEDUCATION_FINISH"].Value = model.empeducation_finish;
                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                obj_cmd.Parameters.Add("@EMPEDUCATION_NO", SqlDbType.Int); obj_cmd.Parameters["@EMPEDUCATION_NO"].Value = strResult;
                
                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empeducation.update)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
