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
    public class cls_ctTREmpkt20
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTREmpkt20() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TREmpkt20> getData(string condition)
        {
            List<cls_TREmpkt20> list_model = new List<cls_TREmpkt20>();
            cls_TREmpkt20 model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("COMPANY_CODE");
                obj_str.Append(", WORKER_CODE");
                obj_str.Append(", YEAR_CODE");
                obj_str.Append(", EMPKT20_RATE");

                obj_str.Append(", CREATED_BY");
                obj_str.Append(", CREATED_DATE");
                    
                obj_str.Append(" FROM HRM_TR_EMPKT20");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY COMPANY_CODE, WORKER_CODE, YEAR_CODE");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TREmpkt20();

                    model.company_code = dr["COMPANY_CODE"].ToString();
                    model.worker_code = dr["WORKER_CODE"].ToString();
                    model.year_code = dr["YEAR_CODE"].ToString();
                    model.empkt20_rate = Convert.ToDouble(dr["EMPKT20_RATE"]);
                    model.company_code = dr["COMPANY_CODE"].ToString();
                    model.worker_code = dr["WORKER_CODE"].ToString();

                    model.created_by = dr["CREATED_BY"].ToString();
                    model.created_date = Convert.ToDateTime(dr["CREATED_DATE"]);
                                                                                                                      
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(EMPFAMILY.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TREmpkt20> getDataByFillter(string com, string year, double rate)
        {
            string strCondition = " AND COMPANY_CODE='" + com + "'";

            if (!year.Equals(""))
                strCondition += " AND YEAR_CODE='" + year + "'";

            if (rate > 0)
                strCondition += " AND EMPKT20_RATE='" + rate + "'";
                        
            return this.getData(strCondition);
        }

        public bool delete(string com, string emp, string year)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_TR_EMPKT20");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND WORKER_CODE='" + emp + "'");
                obj_str.Append(" AND YEAR_CODE='" + year + "'");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Accessdata.clear)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(string com, string year, List<cls_TREmpkt20> list_model)
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

                obj_str.Append(" DELETE FROM HRM_TR_EMPKT20");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND YEAR_CODE='" + year + "'");

                blnResult = obj_conn.doExecuteSQL_transaction(obj_str.ToString());

                //-- Step 2 insert
                if (blnResult)
                {
                    obj_str = new System.Text.StringBuilder();
                    obj_str.Append("INSERT INTO HRM_TR_EMPKT20");
                    obj_str.Append(" (");
                    obj_str.Append("COMPANY_CODE ");
                    obj_str.Append(", WORKER_CODE ");
                    obj_str.Append(", YEAR_CODE ");
                    obj_str.Append(", EMPKT20_RATE ");
                    obj_str.Append(", CREATED_BY ");
                    obj_str.Append(", CREATED_DATE ");                    
                    obj_str.Append(" )");

                    obj_str.Append(" VALUES(");
                    obj_str.Append("@COMPANY_CODE ");
                    obj_str.Append(", @WORKER_CODE ");
                    obj_str.Append(", @YEAR_CODE ");
                    obj_str.Append(", @EMPKT20_RATE ");
                    obj_str.Append(", @CREATED_BY ");
                    obj_str.Append(", @CREATED_DATE ");                   
                    obj_str.Append(" )");

                    SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
                    obj_cmd.Transaction = obj_conn.getTransaction();

                    obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@YEAR_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@EMPKT20_RATE", SqlDbType.Decimal);
                    obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime);

                    foreach (cls_TREmpkt20 model in list_model)
                    {
                        obj_cmd.Parameters["@COMPANY_CODE"].Value = com;
                        obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                        obj_cmd.Parameters["@YEAR_CODE"].Value = model.year_code;
                        obj_cmd.Parameters["@EMPKT20_RATE"].Value = model.empkt20_rate;
                        obj_cmd.Parameters["@CREATED_BY"].Value = model.created_by;
                        obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                        
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
                Message = "ERROR::(Accessdata.insert)" + ex.ToString();
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
