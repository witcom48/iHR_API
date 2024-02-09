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
    public class cls_ctTRAccountapprove
    {
         string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTRAccountapprove() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TRAccountapprove> getData(string condition)
        {
            List<cls_TRAccountapprove> list_model = new List<cls_TRAccountapprove>();
            cls_TRAccountapprove model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("ACCOUNT_USER");
                obj_str.Append(", WORKER_CODE");
                obj_str.Append(", STEP");
                obj_str.Append(", JOB_TYPE");
                obj_str.Append(", COMPANY_CODE");
                obj_str.Append(" FROM SELF_TR_ACCOUNTAPPROVE");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TRAccountapprove();

                    model.company_code = dr["COMPANY_CODE"].ToString();
                    model.account_user = dr["ACCOUNT_USER"].ToString();
                    model.worker_code = dr["WORKER_CODE"].ToString();
                    model.step = dr["STEP"].ToString();

                    model.job_type = dr["JOB_TYPE"].ToString();                                                                                                                      
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(accountapprovegetData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TRAccountapprove> getDataByFillter(string com, string account, string worker, string step, string job_type)
        {
            string strCondition = "";

            strCondition += " AND COMPANY_CODE='" + com + "'";

            if (!account.Equals(""))
                strCondition += " AND ACCOUNT_USER='" + account + "'";

            if (!worker.Equals(""))
                strCondition += " AND WORKER_CODE='" + worker + "'";

            if (!step.Equals(""))
                strCondition += " AND STEP='" + step + "'";

            if (!job_type.Equals(""))
                strCondition += " AND JOB_TYPE='" + job_type + "'";
            
            return this.getData(strCondition);
        }

        public bool checkDataOld(string com, string acount,string worker,string step,string jobtype)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT COMPANY_CODE");
                obj_str.Append(" FROM SELF_TR_ACCOUNTAPPROVE");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                if(!acount.Equals(""))
                    obj_str.Append(" AND ACCOUNT_USER='" + acount + "'");

                if (!worker.Equals(""))
                    obj_str.Append(" AND WORKER_CODE='" + worker + "'");

                if (!step.Equals(""))
                    obj_str.Append(" AND STEP='" + step + "'");

                if (!jobtype.Equals(""))
                    obj_str.Append(" AND JOB_TYPE='" + jobtype + "'");
                                                
                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(accountapprove.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public bool delete(string com, string acount,string worker,string step,string jobtype)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM SELF_TR_ACCOUNTAPPROVE");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                if (!acount.Equals(""))
                    obj_str.Append(" AND ACCOUNT_USER='" + acount + "'");

                if (!worker.Equals(""))
                    obj_str.Append(" AND WORKER_CODE='" + worker + "'");

                if (!step.Equals(""))
                    obj_str.Append(" AND STEP='" + step + "'");

                if (!jobtype.Equals(""))
                    obj_str.Append(" AND JOB_TYPE='" + jobtype + "'");
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(accountapprove.delete)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(cls_TRAccountapprove model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.company_code, model.account_user,model.worker_code,model.step,model.job_type))
                {
                    return true;
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO SELF_TR_ACCOUNTAPPROVE");
                obj_str.Append(" (");
                obj_str.Append("COMPANY_CODE ");
                obj_str.Append(", ACCOUNT_USER ");
                obj_str.Append(", WORKER_CODE ");
                obj_str.Append(", STEP ");
                obj_str.Append(", JOB_TYPE ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append(" @COMPANY_CODE ");
                obj_str.Append(", @ACCOUNT_USER ");
                obj_str.Append(", @WORKER_CODE ");
                obj_str.Append(", @STEP ");
                obj_str.Append(", @JOB_TYPE ");       
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@ACCOUNT_USER", SqlDbType.VarChar); obj_cmd.Parameters["@ACCOUNT_USER"].Value = model.account_user;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                obj_cmd.Parameters.Add("@STEP", SqlDbType.VarChar); obj_cmd.Parameters["@STEP"].Value = model.step;
                obj_cmd.Parameters.Add("@JOB_TYPE", SqlDbType.VarChar); obj_cmd.Parameters["@JOB_TYPE"].Value = model.job_type;
     
                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {              
                Message = "ERROR::(accountapprove.insert)" + ex.ToString();
            }

            return blnResult;
        }
        public bool insert(List<cls_TRAccountapprove> list_model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO SELF_TR_ACCOUNTAPPROVE");
                obj_str.Append(" (");
                obj_str.Append("COMPANY_CODE ");
                obj_str.Append(", ACCOUNT_USER ");
                obj_str.Append(", WORKER_CODE ");
                obj_str.Append(", STEP ");
                obj_str.Append(", JOB_TYPE ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append(" @COMPANY_CODE ");
                obj_str.Append(", @ACCOUNT_USER ");
                obj_str.Append(", @WORKER_CODE ");
                obj_str.Append(", @STEP ");
                obj_str.Append(", @JOB_TYPE ");
                obj_str.Append(" )");


                obj_conn.doConnect();

                obj_conn.doOpenTransaction();

                //-- Step 1 delete data old
                System.Text.StringBuilder obj_str2 = new System.Text.StringBuilder();

                obj_str2.Append(" DELETE FROM SELF_TR_ACCOUNTAPPROVE");
                obj_str2.Append(" WHERE 1=1 ");
                obj_str2.Append(" AND COMPANY_CODE='" + list_model[0].company_code + "'");
                obj_str2.Append(" AND ACCOUNT_USER='" + list_model[0].account_user + "' ");

                blnResult = obj_conn.doExecuteSQL_transaction(obj_str2.ToString());
                if (blnResult)
                {
                    SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
                    obj_cmd.Transaction = obj_conn.getTransaction();

                    obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@ACCOUNT_USER", SqlDbType.VarChar);
                    foreach (cls_TRAccountapprove model in list_model)
                    {

                        obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                        obj_cmd.Parameters["@ACCOUNT_USER"].Value = model.account_user;
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
                Message = "ERROR::(accountapprove.insert)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
