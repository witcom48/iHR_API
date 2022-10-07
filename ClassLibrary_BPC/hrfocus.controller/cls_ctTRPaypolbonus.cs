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
    public class cls_ctTRPaypolbonus
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTRPaypolbonus() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TRPaypolbonus> getData(string language, string condition)
        {
            List<cls_TRPaypolbonus> list_model = new List<cls_TRPaypolbonus>();
            cls_TRPaypolbonus model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("HRM_TR_PAYPOLBONUS.COMPANY_CODE");
                obj_str.Append(", HRM_TR_PAYPOLBONUS.WORKER_CODE");

                obj_str.Append(", HRM_TR_PAYPOLBONUS.PAYPOLBONUS_CODE");

                obj_str.Append(", HRM_TR_PAYPOLBONUS.CREATED_BY");
                obj_str.Append(", HRM_TR_PAYPOLBONUS.CREATED_DATE");

                if (language.Equals("TH"))
                {
                    obj_str.Append(", INITIAL_NAME_TH + WORKER_FNAME_TH + ' ' + WORKER_LNAME_TH AS WORKER_DETAIL");
                }
                else
                {
                    obj_str.Append(", INITIAL_NAME_EN + WORKER_FNAME_EN + ' ' + WORKER_LNAME_EN AS WORKER_DETAIL");
                }

                obj_str.Append(" FROM HRM_TR_PAYPOLBONUS");
                obj_str.Append(" INNER JOIN HRM_MT_WORKER ON HRM_MT_WORKER.COMPANY_CODE=HRM_TR_PAYPOLBONUS.COMPANY_CODE AND HRM_MT_WORKER.WORKER_CODE=HRM_TR_PAYPOLBONUS.WORKER_CODE");
                obj_str.Append(" INNER JOIN HRM_MT_INITIAL ON HRM_MT_INITIAL.INITIAL_CODE=HRM_MT_WORKER.WORKER_INITIAL ");
                


                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY HRM_TR_PAYPOLBONUS.COMPANY_CODE, HRM_TR_PAYPOLBONUS.WORKER_CODE, HRM_TR_PAYPOLBONUS.CREATED_DATE DESC");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TRPaypolbonus();

                    model.company_code = dr["COMPANY_CODE"].ToString();
                    model.worker_code = dr["WORKER_CODE"].ToString();
                    model.paypolbonus_code = dr["PAYPOLBONUS_CODE"].ToString();
                    
                    model.created_by = dr["CREATED_BY"].ToString();
                    model.created_date = Convert.ToDateTime(dr["CREATED_DATE"]);

                    model.worker_detail = dr["WORKER_DETAIL"].ToString();
                                                                                                                      
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Paypolbonus.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TRPaypolbonus> getDataByFillter(string language, string access_emp, string com, string bonuscode)
        {
            string strCondition = " AND HRM_TR_PAYPOLBONUS.COMPANY_CODE='" + com + "'";
            
            if (!bonuscode.Equals(""))
                strCondition += " AND HRM_TR_PAYPOLBONUS.PAYPOLBONUS_CODE='" + bonuscode + "'";


            return this.getData(language, strCondition);
        }

        public bool delete(string com, string emp, string bonuscode)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_TR_PAYPOLBONUS");          
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND WORKER_CODE='" + emp + "'");
                obj_str.Append(" AND PAYPOLBONUS_CODE='" + bonuscode + "'");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());
            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Paypolbonus.delete)" + ex.ToString();
            }

            return blnResult;
        }
                
        public bool insert(string com, string bonuscode, List<cls_TRPaypolbonus> list_model)
        {
            bool blnResult = false;
            try
            {

                
                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_PAYPOLBONUS");
                obj_str.Append(" (");
                obj_str.Append("COMPANY_CODE ");
                obj_str.Append(", WORKER_CODE ");
                obj_str.Append(", PAYPOLBONUS_CODE ");                              
                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@COMPANY_CODE ");
                obj_str.Append(", @WORKER_CODE ");
                obj_str.Append(", @PAYPOLBONUS_CODE ");                
                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");                

                obj_conn.doConnect();

                obj_conn.doOpenTransaction();

                //-- Step 1 delete data old
                string strWorkerID = "";
                foreach (cls_TRPaypolbonus model in list_model)
                {
                    strWorkerID += "'" + model.worker_code + "',";
                }
                if (strWorkerID.Length > 0)
                    strWorkerID = strWorkerID.Substring(0, strWorkerID.Length - 1);

                System.Text.StringBuilder obj_str2 = new System.Text.StringBuilder();

                obj_str2.Append(" DELETE FROM HRM_TR_PAYPOLBONUS");
                obj_str2.Append(" WHERE 1=1 ");
                obj_str2.Append(" AND COMPANY_CODE='" + com + "'");
                obj_str2.Append(" AND WORKER_CODE IN (" + strWorkerID + ")");
                //obj_str2.Append(" AND PAYPOLBONUS_CODE='" + bonuscode + "'");

                blnResult = obj_conn.doExecuteSQL_transaction(obj_str2.ToString());

                if (blnResult) {

                    SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
                    obj_cmd.Transaction = obj_conn.getTransaction();

                    obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@PAYPOLBONUS_CODE", SqlDbType.VarChar);                    
                    obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime);
                    obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit);
                    
                    foreach (cls_TRPaypolbonus model in list_model)
                    {

                        obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                        obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                        obj_cmd.Parameters["@PAYPOLBONUS_CODE"].Value = model.paypolbonus_code;                        
                        obj_cmd.Parameters["@CREATED_BY"].Value = model.created_by;
                        obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                        obj_cmd.Parameters["@FLAG"].Value = false;

                        obj_cmd.ExecuteNonQuery();
                    }

                    blnResult = obj_conn.doCommit();

                    if(!blnResult)
                        obj_conn.doRollback();
                    obj_conn.doClose();                    

                }
                else
                {
                    obj_conn.doRollback();
                    obj_conn.doClose();
                }
                
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Paypolbonus.insert)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
