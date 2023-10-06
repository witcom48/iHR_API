using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.service
{
    public class cls_srvProcessEmployee
    {

        public string doChangeWorkerCode(string company_code, string worker_code, string new_code)
        {
            string strResult = "";

            cls_ctConnection obj_conn = new cls_ctConnection();

            try
            {
               
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_conn.doConnect();
                //obj_conn.doOpenTransaction();

                obj_str.Append(" EXEC [dbo].[HRM_PRO_CHANGEWORKERCODE] '" + company_code + "', '" + worker_code + "', '" + new_code + "' ");

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
               
                obj_cmd.CommandType = CommandType.Text;

                int intCountSuccess = obj_cmd.ExecuteNonQuery();

                if (intCountSuccess > 0)
                {
                    //obj_conn.doCommit();
                    strResult = "Success::" + intCountSuccess.ToString();
                }

            }
            catch (Exception ex)
            {

            }

            return strResult;
        }
    }
}
