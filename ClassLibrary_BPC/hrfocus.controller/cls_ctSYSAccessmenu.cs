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
    public class cls_ctSYSAccessmenu
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctSYSAccessmenu() { }

        public string getMessage() { return this.Message; }
        
        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_SYSAccessmenu> getData(string condition)
        {
            List<cls_SYSAccessmenu> list_model = new List<cls_SYSAccessmenu>();
            cls_SYSAccessmenu model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("COMPANY_CODE");
                obj_str.Append(", POLMENU_CODE");
                obj_str.Append(", ACCESSMENU_MODULE");
                obj_str.Append(", ACCESSMENU_TYPE");
                obj_str.Append(", ACCESSMENU_CODE");
                
                obj_str.Append(" FROM HRM_SYS_ACCESSMENU");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY COMPANY_CODE, POLMENU_CODE, ACCESSMENU_MODULE");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_SYSAccessmenu();

                    model.company_code = dr["COMPANY_CODE"].ToString();
                    model.polmenu_code = dr["POLMENU_CODE"].ToString();
                    model.accessmenu_module = dr["ACCESSMENU_MODULE"].ToString();
                    model.accessmenu_type = dr["ACCESSMENU_TYPE"].ToString();
                    model.accessmenu_code = dr["ACCESSMENU_CODE"].ToString();
                                                                                                                                          
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Accessmenu.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_SYSAccessmenu> getData(string com, string pol)
        {
            string strCondition = "";

            strCondition += " AND COMPANY_CODE='" + com + "'";
            strCondition += " AND POLMENU_CODE='" + pol + "'";
            
            return this.getData(strCondition);
        }

        public bool clear(string com, string pol)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_SYS_ACCESSMENU");                
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND POLMENU_CODE='" + pol + "'");
                
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Accessmenu.clear)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(string com, string pol, List<cls_SYSAccessmenu> list_model)
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

                obj_str.Append(" DELETE FROM HRM_SYS_ACCESSMENU");                
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND POLMENU_CODE='" + pol + "'");
                
                blnResult = obj_conn.doExecuteSQL_transaction(obj_str.ToString());

                //-- Step 2 insert
                if (blnResult)
                {
                    obj_str = new System.Text.StringBuilder();
                    obj_str.Append("INSERT INTO HRM_SYS_ACCESSMENU");
                    obj_str.Append(" (");
                    obj_str.Append("COMPANY_CODE ");
                    obj_str.Append(", POLMENU_CODE ");
                    obj_str.Append(", ACCESSMENU_MODULE ");
                    obj_str.Append(", ACCESSMENU_TYPE ");
                    obj_str.Append(", ACCESSMENU_CODE ");                    
                    obj_str.Append(" )");

                    obj_str.Append(" VALUES(");
                    obj_str.Append("@COMPANY_CODE ");
                    obj_str.Append(", @POLMENU_CODE ");
                    obj_str.Append(", @ACCESSMENU_MODULE ");
                    obj_str.Append(", @ACCESSMENU_TYPE ");
                    obj_str.Append(", @ACCESSMENU_CODE ");      
                    obj_str.Append(" )");

                    SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
                    obj_cmd.Transaction = obj_conn.getTransaction();

                    obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@POLMENU_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@ACCESSMENU_MODULE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@ACCESSMENU_TYPE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@ACCESSMENU_CODE", SqlDbType.VarChar);
                   
                    foreach (cls_SYSAccessmenu model in list_model)
                    {

                        obj_cmd.Parameters["@COMPANY_CODE"].Value = com;
                        obj_cmd.Parameters["@POLMENU_CODE"].Value = pol;
                        obj_cmd.Parameters["@ACCESSMENU_MODULE"].Value = model.accessmenu_module;
                        obj_cmd.Parameters["@ACCESSMENU_TYPE"].Value = model.accessmenu_type;
                        obj_cmd.Parameters["@ACCESSMENU_CODE"].Value = model.accessmenu_code;
                        
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
                Message = "ERROR::(Accessmenu.insert)" + ex.ToString();
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
