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
   public class cls_ctSYSAllow
    {
          string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctSYSAllow() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        public List<cls_SYSAllow> getData()
        {
            List<cls_SYSAllow> list_model = new List<cls_SYSAllow>();
            cls_SYSAllow model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT *");
                obj_str.Append(" FROM HRM_SYS_ALLOW");
                obj_str.Append(" WHERE 1=1");
                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_SYSAllow();

                    model.allow_ip = dr["ALLOW_IP"].ToString();
                    model.allow_port = dr["ALLOW_PORT"].ToString();                                                                
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(HRM_SYS_ALLOW.getData)" + ex.ToString();
            }

            return list_model;
        }
        public bool checkDataOld(string ip,string port)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT * FROM HRM_SYS_ALLOW");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND ALLOW_IP='" + ip + "'");
                obj_str.Append(" AND ALLOW_PORT='" + port + "'");                       
                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(HRM_SYS_ALLOW.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public bool delete(string ip,string port)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_SYS_ALLOW");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND ALLOW_IP='" + ip + "'");
                obj_str.Append(" AND ALLOW_PORT='" + port + "'");
                                              
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(HRM_SYS_ALLOW.delete)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(cls_SYSAllow model)
        {
            bool blnResult = false;
 
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.allow_ip,model.allow_port))
                {
                    return true;
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_SYS_ALLOW");
                obj_str.Append(" (");
                obj_str.Append("ALLOW_IP ");
                obj_str.Append(", ALLOW_PORT ");
                obj_str.Append(" )");
                obj_str.Append(" VALUES(");
                obj_str.Append("@ALLOW_IP ");
                obj_str.Append(", @ALLOW_PORT ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@ALLOW_IP", SqlDbType.VarChar); obj_cmd.Parameters["@ALLOW_IP"].Value = model.allow_ip;
                obj_cmd.Parameters.Add("@ALLOW_PORT", SqlDbType.VarChar); obj_cmd.Parameters["@ALLOW_PORT"].Value = model.allow_port;
     
                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();
                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(HRM_SYS_ALLOW.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_SYSAllow model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_SYS_ALLOW SET ");
                obj_str.Append(" ALLOW_IP=@ALLOW_IP ");
                obj_str.Append(", ALLOW_PORT=@ALLOW_PORT ");
               
                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@ALLOW_IP", SqlDbType.VarChar); obj_cmd.Parameters["@ALLOW_IP"].Value = model.allow_ip;
                obj_cmd.Parameters.Add("@ALLOW_PORT", SqlDbType.VarChar); obj_cmd.Parameters["@ALLOW_PORT"].Value = model.allow_port;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(HRM_SYS_PACKAGE.update)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
