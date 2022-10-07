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
    public class cls_ctSYSAccessdep
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctSYSAccessdep() { }

        public string getMessage() { return this.Message; }
        
        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_SYSAccessdep> getData(string condition)
        {
            List<cls_SYSAccessdep> list_model = new List<cls_SYSAccessdep>();
            cls_SYSAccessdep model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("COMPANY_CODE");
                obj_str.Append(", ACCOUNT_USR");
                obj_str.Append(", ACCESSDEP_LEVEL");
                obj_str.Append(", ACCESSDEP_DEP");
                
                obj_str.Append(" FROM HRM_SYS_ACCESSDEP");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY COMPANY_CODE, ACCOUNT_USR");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_SYSAccessdep();

                    model.company_code = dr["COMPANY_CODE"].ToString();
                    model.account_usr = dr["ACCOUNT_USR"].ToString();
                    model.accessdep_level = dr["ACCESSDEP_LEVEL"].ToString();
                    model.accessdep_dep = dr["ACCESSDEP_DEP"].ToString();
                                                                                                                                          
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Accessdep.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_SYSAccessdep> getData(string com, string username)
        {
            string strCondition = "";

            strCondition += " AND COMPANY_CODE='" + com + "'";
            strCondition += " AND ACCOUNT_USR='" + username + "'";
            
            return this.getData(strCondition);
        }
        
        public bool clear(string com, string username)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_SYS_ACCESSDEP");                
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND ACCOUNT_USR='" + username + "'");
                
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Accessdep.clear)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(string com, string username, List<cls_SYSAccessdep> list_model)
        {
            bool blnResult = false;
            cls_ctConnection obj_conn = new cls_ctConnection();
            try
            {               
                
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();
                                
                obj_conn.doConnect();

                obj_conn.doOpenTransaction();

                //-- Step 1 delete data old
                obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_SYS_ACCESSDEP");                
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND ACCOUNT_USR='" + username + "'");
                
                blnResult = obj_conn.doExecuteSQL_transaction(obj_str.ToString());

                //-- Step 2 insert
                if (blnResult)
                {
                    obj_str = new System.Text.StringBuilder();
                    obj_str.Append("INSERT INTO HRM_SYS_ACCESSDEP");
                    obj_str.Append(" (");
                    obj_str.Append("COMPANY_CODE ");
                    obj_str.Append(", ACCOUNT_USR ");
                    obj_str.Append(", ACCESSDEP_LEVEL ");
                    obj_str.Append(", ACCESSDEP_DEP ");                    
                    obj_str.Append(" )");

                    obj_str.Append(" VALUES(");
                    obj_str.Append("@COMPANY_CODE ");
                    obj_str.Append(", @ACCOUNT_USR ");
                    obj_str.Append(", @ACCESSDEP_LEVEL ");
                    obj_str.Append(", @ACCESSDEP_DEP ");      
                    obj_str.Append(" )");

                    SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
                    obj_cmd.Transaction = obj_conn.getTransaction();

                    obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@ACCOUNT_USR", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@ACCESSDEP_LEVEL", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@ACCESSDEP_DEP", SqlDbType.VarChar);
                   
                    foreach (cls_SYSAccessdep model in list_model)
                    {

                        obj_cmd.Parameters["@COMPANY_CODE"].Value = com;
                        obj_cmd.Parameters["@ACCOUNT_USR"].Value = username;
                        obj_cmd.Parameters["@ACCESSDEP_LEVEL"].Value = model.accessdep_level;
                        obj_cmd.Parameters["@ACCESSDEP_DEP"].Value = model.accessdep_dep;
                        
                        obj_cmd.ExecuteNonQuery();
                    }

                    blnResult = obj_conn.doCommit();

                }
                else
                {
                    obj_conn.doRollback();
                }


            }


            catch (Exception ex)
            {
                Message = "ERROR::(Accessdep.insert)" + ex.ToString();
                obj_conn.doRollback();
            }
            finally
            {
                obj_conn.doClose();
            }

            return blnResult;
        }
          
    }
}
