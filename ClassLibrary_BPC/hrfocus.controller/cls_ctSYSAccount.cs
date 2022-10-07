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
    public class cls_ctSYSAccount
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctSYSAccount() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_SYSAccount> getData(string condition)
        {
            List<cls_SYSAccount> list_model = new List<cls_SYSAccount>();
            cls_SYSAccount model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("COMPANY_CODE");
                obj_str.Append(", ACCOUNT_ID");
                obj_str.Append(", ACCOUNT_USR");
                obj_str.Append(", ACCOUNT_PWD");
                obj_str.Append(", ACCOUNT_DETAIL");

                obj_str.Append(", ISNULL(ACCOUNT_EMAIL, '') AS ACCOUNT_EMAIL");
                obj_str.Append(", ISNULL(ACCOUNT_EMAILALERT, '0') AS ACCOUNT_EMAILALERT");

                obj_str.Append(", ISNULL(ACCOUNT_LINE, '') AS ACCOUNT_LINE");
                obj_str.Append(", ISNULL(ACCOUNT_LINEALERT, '0') AS ACCOUNT_LINEALERT");

                obj_str.Append(", ISNULL(ACCOUNT_LOCK, '0') AS ACCOUNT_LOCK");
                obj_str.Append(", ISNULL(ACCOUNT_FAILLOGIN, '0') AS ACCOUNT_FAILLOGIN");
                obj_str.Append(", ISNULL(ACCOUNT_LASTLOGIN, '01/01/1900') AS ACCOUNT_LASTLOGIN");
                                                
                obj_str.Append(", ISNULL(ACCOUNT_MONTHLY, '0') AS ACCOUNT_MONTHLY");
                obj_str.Append(", ISNULL(ACCOUNT_DAILY, '0') AS ACCOUNT_DAILY");

                obj_str.Append(", POLMENU_CODE");

                obj_str.Append(", ISNULL(WORKER_CODE, '') AS WORKER_CODE");
                                
                obj_str.Append(", ISNULL(MODIFIED_BY, CREATED_BY) AS MODIFIED_BY");
                obj_str.Append(", ISNULL(MODIFIED_DATE, CREATED_DATE) AS MODIFIED_DATE");

                obj_str.Append(" FROM HRM_SYS_ACCOUNT");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY ACCOUNT_USR");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_SYSAccount();

                    model.company_code = Convert.ToString(dr["COMPANY_CODE"]);

                    model.account_id = Convert.ToInt32(dr["ACCOUNT_ID"]);

                    model.account_usr = Convert.ToString(dr["ACCOUNT_USR"]);
                    model.account_pwd = Convert.ToString(dr["ACCOUNT_PWD"]);
                    model.account_detail = Convert.ToString(dr["ACCOUNT_DETAIL"]);

                    model.account_email = Convert.ToString(dr["ACCOUNT_EMAIL"]);
                    model.account_emailalert = Convert.ToBoolean(dr["ACCOUNT_EMAILALERT"]);
                    model.account_line = Convert.ToString(dr["ACCOUNT_LINE"]);
                    model.account_linealert = Convert.ToBoolean(dr["ACCOUNT_LINEALERT"]);

                    model.account_lock = Convert.ToBoolean(dr["ACCOUNT_LOCK"]);
                    model.account_faillogin = Convert.ToInt32(dr["ACCOUNT_FAILLOGIN"]);
                    model.account_lastlogin = Convert.ToDateTime(dr["ACCOUNT_LASTLOGIN"]);


                    model.account_monthly = Convert.ToBoolean(dr["ACCOUNT_MONTHLY"]);
                    model.account_daily = Convert.ToBoolean(dr["ACCOUNT_DAILY"]);

                    model.polmenu_code = dr["POLMENU_CODE"].ToString();

                    model.worker_code = dr["WORKER_CODE"].ToString();
                   
                    model.modified_by = dr["MODIFIED_BY"].ToString();
                    model.modified_date = Convert.ToDateTime(dr["MODIFIED_DATE"]);
                                                                                                                      
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Account.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_SYSAccount> getData()
        {
            string strCondition = "";                        
            return this.getData(strCondition);
        }

        public List<cls_SYSAccount> getData(string username, string password)
        {
            string strCondition = " AND ACCOUNT_USR='" + username.Replace("'", "").Replace(",", "").Replace("=", "") + "'";
            strCondition += " AND ACCOUNT_PWD='" + password.Replace("'", "").Replace(",", "").Replace("=", "") + "'";
            return this.getData(strCondition);
        }

        public List<cls_SYSAccount> getDataByUsername(string username)
        {
            string strCondition = " AND ACCOUNT_USR='" + username.Replace("'", "").Replace(",", "").Replace("=", "") + "'";            
            return this.getData(strCondition);
        }

        public bool checkDataOld(string username)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ACCOUNT_ID");
                obj_str.Append(" FROM HRM_SYS_ACCOUNT");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND ACCOUNT_USR='" + username + "'");
                                                                
                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Account.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public int getNextID()
        {
            int intResult = 1;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT MAX(ACCOUNT_ID) ");
                obj_str.Append(" FROM HRM_SYS_ACCOUNT");                

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    intResult = Convert.ToInt32(dt.Rows[0][0]) + 1;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Account.getNextID)" + ex.ToString();
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

                obj_str.Append(" DELETE FROM HRM_SYS_ACCOUNT");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND ACCOUNT_ID='" + id + "'");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Account.delete)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(cls_SYSAccount model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.account_usr))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_SYS_ACCOUNT");
                obj_str.Append(" (");
                obj_str.Append("COMPANY_CODE ");
                obj_str.Append(", ACCOUNT_ID ");
                obj_str.Append(", ACCOUNT_USR ");
                obj_str.Append(", ACCOUNT_PWD ");
                obj_str.Append(", ACCOUNT_DETAIL ");

                obj_str.Append(", ACCOUNT_EMAIL ");
                obj_str.Append(", ACCOUNT_EMAILALERT ");
                obj_str.Append(", ACCOUNT_LINE ");
                obj_str.Append(", ACCOUNT_LINEALERT ");

                obj_str.Append(", ACCOUNT_LOCK ");
          
                obj_str.Append(", ACCOUNT_MONTHLY ");
                obj_str.Append(", ACCOUNT_DAILY ");

                obj_str.Append(", POLMENU_CODE ");

                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@COMPANY_CODE ");
                obj_str.Append(", @ACCOUNT_ID ");
                obj_str.Append(", @ACCOUNT_USR ");
                obj_str.Append(", @ACCOUNT_PWD ");
                obj_str.Append(", @ACCOUNT_DETAIL ");

                obj_str.Append(", @ACCOUNT_EMAIL ");
                obj_str.Append(", @ACCOUNT_EMAILALERT ");
                obj_str.Append(", @ACCOUNT_LINE ");
                obj_str.Append(", @ACCOUNT_LINEALERT ");

                obj_str.Append(", @ACCOUNT_LOCK ");
                
                obj_str.Append(", @ACCOUNT_MONTHLY ");
                obj_str.Append(", @ACCOUNT_DAILY ");

                obj_str.Append(", @POLMENU_CODE ");

                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");            
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;

                obj_cmd.Parameters.Add("@ACCOUNT_ID", SqlDbType.Int); obj_cmd.Parameters["@ACCOUNT_ID"].Value = this.getNextID();

                obj_cmd.Parameters.Add("@ACCOUNT_USR", SqlDbType.VarChar); obj_cmd.Parameters["@ACCOUNT_USR"].Value = model.account_usr;
                obj_cmd.Parameters.Add("@ACCOUNT_PWD", SqlDbType.VarChar); obj_cmd.Parameters["@ACCOUNT_PWD"].Value = model.account_pwd;
                obj_cmd.Parameters.Add("@ACCOUNT_DETAIL", SqlDbType.VarChar); obj_cmd.Parameters["@ACCOUNT_DETAIL"].Value = model.account_detail;

                obj_cmd.Parameters.Add("@ACCOUNT_EMAIL", SqlDbType.VarChar); obj_cmd.Parameters["@ACCOUNT_EMAIL"].Value = model.account_email;
                obj_cmd.Parameters.Add("@ACCOUNT_EMAILALERT", SqlDbType.Bit); obj_cmd.Parameters["@ACCOUNT_EMAILALERT"].Value = model.account_emailalert;
                obj_cmd.Parameters.Add("@ACCOUNT_LINE", SqlDbType.VarChar); obj_cmd.Parameters["@ACCOUNT_LINE"].Value = model.account_line;
                obj_cmd.Parameters.Add("@ACCOUNT_LINEALERT", SqlDbType.Bit); obj_cmd.Parameters["@ACCOUNT_LINEALERT"].Value = model.account_linealert;

                obj_cmd.Parameters.Add("@ACCOUNT_LOCK", SqlDbType.Bit); obj_cmd.Parameters["@ACCOUNT_LOCK"].Value = model.account_lock;                


                obj_cmd.Parameters.Add("@ACCOUNT_MONTHLY", SqlDbType.Bit); obj_cmd.Parameters["@ACCOUNT_MONTHLY"].Value = model.account_monthly;
                obj_cmd.Parameters.Add("@ACCOUNT_DAILY", SqlDbType.Bit); obj_cmd.Parameters["@ACCOUNT_DAILY"].Value = model.account_daily;

                obj_cmd.Parameters.Add("@POLMENU_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@POLMENU_CODE"].Value = model.polmenu_code;
                
                obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@CREATED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;
     
                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Account.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_SYSAccount model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_SYS_ACCOUNT SET ");

                obj_str.Append(" ACCOUNT_PWD=@ACCOUNT_PWD ");
                obj_str.Append(", ACCOUNT_DETAIL=@ACCOUNT_DETAIL ");

                obj_str.Append(", ACCOUNT_EMAIL=@ACCOUNT_EMAIL ");
                obj_str.Append(", ACCOUNT_EMAILALERT=@ACCOUNT_EMAILALERT ");
                obj_str.Append(", ACCOUNT_LINE=@ACCOUNT_LINE ");
                obj_str.Append(", ACCOUNT_LINEALERT=@ACCOUNT_LINEALERT ");

                obj_str.Append(", ACCOUNT_LOCK=@ACCOUNT_LOCK ");

                obj_str.Append(", ACCOUNT_NEWDATA=@ACCOUNT_NEWDATA ");
                obj_str.Append(", ACCOUNT_EDITDATA=@ACCOUNT_EDITDATA ");
                obj_str.Append(", ACCOUNT_DELETEDATA=@ACCOUNT_DELETEDATA ");
                obj_str.Append(", ACCOUNT_VIEWSALARY=@ACCOUNT_VIEWSALARY ");

                obj_str.Append(", ACCOUNT_MONTHLY=@ACCOUNT_MONTHLY ");
                obj_str.Append(", ACCOUNT_DAILY=@ACCOUNT_DAILY ");

                obj_str.Append(", POLMENU_CODE=@POLMENU_CODE ");
                                
                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");
                obj_str.Append(", FLAG=@FLAG ");


                obj_str.Append(" WHERE ACCOUNT_ID=@ACCOUNT_ID ");

                
                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@ACCOUNT_PWD", SqlDbType.VarChar); obj_cmd.Parameters["@ACCOUNT_PWD"].Value = model.account_pwd;
                obj_cmd.Parameters.Add("@ACCOUNT_DETAIL", SqlDbType.VarChar); obj_cmd.Parameters["@ACCOUNT_DETAIL"].Value = model.account_detail;

                obj_cmd.Parameters.Add("@ACCOUNT_EMAIL", SqlDbType.VarChar); obj_cmd.Parameters["@ACCOUNT_EMAIL"].Value = model.account_email;
                obj_cmd.Parameters.Add("@ACCOUNT_EMAILALERT", SqlDbType.Bit); obj_cmd.Parameters["@ACCOUNT_EMAILALERT"].Value = model.account_emailalert;
                obj_cmd.Parameters.Add("@ACCOUNT_LINE", SqlDbType.VarChar); obj_cmd.Parameters["@ACCOUNT_LINE"].Value = model.account_line;
                obj_cmd.Parameters.Add("@ACCOUNT_LINEALERT", SqlDbType.Bit); obj_cmd.Parameters["@ACCOUNT_LINEALERT"].Value = model.account_linealert;

                obj_cmd.Parameters.Add("@ACCOUNT_LOCK", SqlDbType.Bit); obj_cmd.Parameters["@ACCOUNT_LOCK"].Value = model.account_lock;

                obj_cmd.Parameters.Add("@ACCOUNT_MONTHLY", SqlDbType.Bit); obj_cmd.Parameters["@ACCOUNT_MONTHLY"].Value = model.account_monthly;
                obj_cmd.Parameters.Add("@ACCOUNT_DAILY", SqlDbType.Bit); obj_cmd.Parameters["@ACCOUNT_DAILY"].Value = model.account_daily;

                obj_cmd.Parameters.Add("@POLMENU_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@POLMENU_CODE"].Value = model.polmenu_code;
                
                
                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;

                obj_cmd.Parameters.Add("@ACCOUNT_ID", SqlDbType.Int); obj_cmd.Parameters["@ACCOUNT_ID"].Value = model.account_id;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Account.update)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
