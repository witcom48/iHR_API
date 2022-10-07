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
    public class cls_ctTREmpcard
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTREmpcard() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TREmpcard> getData(string condition)
        {
            List<cls_TREmpcard> list_model = new List<cls_TREmpcard>();
            cls_TREmpcard model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("EMPCARD_ID");
                obj_str.Append(", EMPCARD_CODE");
                obj_str.Append(", CARD_TYPE");
                obj_str.Append(", ISNULL(EMPCARD_ISSUE, '') AS EMPCARD_ISSUE");
                obj_str.Append(", ISNULL(EMPCARD_EXPIRE, '') AS EMPCARD_EXPIRE");

                obj_str.Append(", COMPANY_CODE");
                obj_str.Append(", WORKER_CODE");
                                 
                obj_str.Append(", ISNULL(MODIFIED_BY, CREATED_BY) AS MODIFIED_BY");
                obj_str.Append(", ISNULL(MODIFIED_DATE, CREATED_DATE) AS MODIFIED_DATE");

                obj_str.Append(" FROM HRM_TR_EMPCARD");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY COMPANY_CODE");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TREmpcard();

                    model.empcard_id = Convert.ToInt32(dr["EMPCARD_ID"]);
                    model.empcard_code = dr["EMPCARD_CODE"].ToString();
                    model.card_type = dr["CARD_TYPE"].ToString();
                    model.empcard_issue = Convert.ToDateTime(dr["EMPCARD_ISSUE"]);
                    model.empcard_expire = Convert.ToDateTime(dr["EMPCARD_EXPIRE"]);

                    model.company_code = dr["COMPANY_CODE"].ToString();
                    model.worker_code = dr["WORKER_CODE"].ToString();
                    
                    model.modified_by = dr["MODIFIED_BY"].ToString();
                    model.modified_date = Convert.ToDateTime(dr["MODIFIED_DATE"]);
                                                                                                                      
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Empcard.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TREmpcard> getDataByFillter(string com, string type, string id, string emp)
        {
            string strCondition = " AND COMPANY_CODE='" + com + "'";

            if (!type.Equals(""))
                strCondition += " AND CARD_TYPE='" + type + "'";

            if (!id.Equals(""))
                strCondition += " AND EMPCARD_ID='" + id + "'";

            if (!emp.Equals(""))
                strCondition += " AND WORKER_CODE='" + emp + "'";
            
            return this.getData(strCondition);
        }

        public List<cls_TREmpcard> getDataTaxMultipleEmp(string com, string worker)
        {
            string strCondition = " AND COMPANY_CODE='" + com + "'";
            strCondition += " AND CARD_TYPE = 'NTID'  ";
            strCondition += " AND WORKER_CODE IN (" + worker + ") ";

            return this.getData(strCondition);
        }

        public bool checkDataOld(string com, string emp, string type)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT COMPANY_CODE");
                obj_str.Append(" FROM HRM_TR_EMPCARD");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND WORKER_CODE='" + emp + "'");               
                obj_str.Append(" AND CARD_TYPE='" + type + "'");
                                                
                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empcard.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public int getNextID()
        {
            int intResult = 1;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT MAX(EMPCARD_ID) ");
                obj_str.Append(" FROM HRM_TR_EMPCARDD");             

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    intResult = Convert.ToInt32(dt.Rows[0][0]) + 1;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empcard.getNextID)" + ex.ToString();
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

                obj_str.Append(" DELETE FROM HRM_TR_EMPCARD");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND EMPCARD_ID='" + id + "'");
                                              
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Empcard.delete)" + ex.ToString();
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

                obj_str.Append(" DELETE FROM HRM_TR_EMPCARD");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND WORKER_CODE='" + emp + "'");   

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Empcard.clear)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(cls_TREmpcard model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.company_code, model.worker_code, model.card_type))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_EMPCARD");
                obj_str.Append(" (");
                obj_str.Append("EMPCARD_ID ");
                obj_str.Append(", EMPCARD_CODE ");
                obj_str.Append(", CARD_TYPE ");
                obj_str.Append(", EMPCARD_ISSUE ");
                obj_str.Append(", EMPCARD_EXPIRE ");

                obj_str.Append(", COMPANY_CODE ");
                obj_str.Append(", WORKER_CODE ");

                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@empcard_ID ");
                obj_str.Append(", @EMPCARD_CODE ");
                obj_str.Append(", @CARD_TYPE ");
                obj_str.Append(", @EMPCARD_ISSUE ");
                obj_str.Append(", @EMPCARD_EXPIRE ");

                obj_str.Append(", @COMPANY_CODE ");
                obj_str.Append(", @WORKER_CODE ");

                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");   
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@EMPCARD_ID", SqlDbType.Int); obj_cmd.Parameters["@EMPCARD_ID"].Value = this.getNextID();
                obj_cmd.Parameters.Add("@EMPCARD_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@EMPCARD_CODE"].Value = model.empcard_code;
                obj_cmd.Parameters.Add("@CARD_TYPE", SqlDbType.VarChar); obj_cmd.Parameters["@CARD_TYPE"].Value = model.card_type;
                obj_cmd.Parameters.Add("@EMPCARD_ISSUE", SqlDbType.DateTime); obj_cmd.Parameters["@EMPCARD_ISSUE"].Value = model.empcard_issue;
                obj_cmd.Parameters.Add("@EMPCARD_EXPIRE", SqlDbType.DateTime); obj_cmd.Parameters["@EMPCARD_EXPIRE"].Value = model.empcard_expire;

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
                Message = "ERROR::(Empcard.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_TREmpcard model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_TR_EMPCARD SET ");

                obj_str.Append(" EMPCARD_CODE=@EMPCARD_CODE ");
                obj_str.Append(", EMPCARD_ISSUE=@EMPCARD_ISSUE ");
                obj_str.Append(", EMPCARD_EXPIRE=@EMPCARD_EXPIRE ");
                
                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");
                obj_str.Append(", FLAG=@FLAG ");

                obj_str.Append(" WHERE EMPCARD_ID=@EMPCARD_ID ");
               
                
                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@EMPCARD_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@EMPCARD_CODE"].Value = model.empcard_code;

                obj_cmd.Parameters.Add("@EMPCARD_ISSUE", SqlDbType.DateTime); obj_cmd.Parameters["@EMPCARD_ISSUE"].Value = model.empcard_issue;
                obj_cmd.Parameters.Add("@EMPCARD_EXPIRE", SqlDbType.DateTime); obj_cmd.Parameters["@EMPCARD_EXPIRE"].Value = model.empcard_expire;

                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;

                obj_cmd.Parameters.Add("@EMPCARD_ID", SqlDbType.Int); obj_cmd.Parameters["@EMPCARD_ID"].Value = model.empcard_id;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empcard.update)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
