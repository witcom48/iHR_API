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
    public class cls_ctTREmpbank
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTREmpbank() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TREmpbank> getData(string condition)
        {
            List<cls_TREmpbank> list_model = new List<cls_TREmpbank>();
            cls_TREmpbank model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("COMPANY_CODE");
                obj_str.Append(", WORKER_CODE");
                obj_str.Append(", EMPBANK_ID");      
                obj_str.Append(", ISNULL(EMPBANK_BANKCODE, '') AS EMPBANK_BANKCODE");
                obj_str.Append(", ISNULL(EMPBANK_BANKACCOUNT, '') AS EMPBANK_BANKACCOUNT");
                obj_str.Append(", EMPBANK_BANKPERCENT");
                obj_str.Append(", EMPBANK_CASHPERCENT");                                 
                obj_str.Append(", ISNULL(MODIFIED_BY, CREATED_BY) AS MODIFIED_BY");
                obj_str.Append(", ISNULL(MODIFIED_DATE, CREATED_DATE) AS MODIFIED_DATE");

                obj_str.Append(", ISNULL(EMPBANK_BANKNAME, '') AS EMPBANK_BANKNAME");

                obj_str.Append(" FROM HRM_TR_EMPBANK");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY COMPANY_CODE, WORKER_CODE");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TREmpbank();

                    model.company_code = dr["COMPANY_CODE"].ToString();
                    model.worker_code = dr["WORKER_CODE"].ToString();
                    model.empbank_id = Convert.ToInt32(dr["EMPBANK_ID"]);
                    model.empbank_bankcode = dr["EMPBANK_BANKCODE"].ToString();
                    model.empbank_bankaccount = dr["EMPBANK_BANKACCOUNT"].ToString();

                    model.empbank_bankpercent = Convert.ToDouble(dr["EMPBANK_BANKPERCENT"]);
                    model.empbank_cashpercent = Convert.ToDouble(dr["EMPBANK_CASHPERCENT"]);
                    
                    model.modified_by = dr["MODIFIED_BY"].ToString();
                    model.modified_date = Convert.ToDateTime(dr["MODIFIED_DATE"]);

                    model.empbank_bankname = dr["EMPBANK_BANKNAME"].ToString();
                                                                                                                      
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Empbank.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TREmpbank> getDataByFillter(string com, string worker)
        {
            string strCondition = " AND COMPANY_CODE='" + com + "'";

            if (!worker.Equals(""))
                strCondition += " AND WORKER_CODE='" + worker + "'";

                        
            return this.getData(strCondition);
        }

        public List<cls_TREmpbank> getDataMultipleEmp(string com, string worker)
        {
            string strCondition = " AND COMPANY_CODE='" + com + "'";
            strCondition += " AND WORKER_CODE IN (" + worker + ") ";

            return this.getData(strCondition);
        }

        public bool checkDataOld(string com, string worker, string account)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT EMPBANK_BANKCODE");
                obj_str.Append(" FROM HRM_TR_EMPBANK");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND WORKER_CODE='" + worker + "'");
                obj_str.Append(" AND EMPBANK_BANKACCOUNT='" + account + "'");
                                                
                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empbank.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }


        public bool delete(string id)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_TR_EMPBANK");
                obj_str.Append(" WHERE EMPBANK_ID='" + id + "'");
                                          
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Empbank.delete)" + ex.ToString();
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

                obj_str.Append(" DELETE FROM HRM_TR_EMPBANK");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND WORKER_CODE='" + emp + "'");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Empdep.clear)" + ex.ToString();
            }

            return blnResult;
        }

        public int getNextID()
        {
            int intResult = 1;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT MAX(EMPBANK_ID) ");
                obj_str.Append(" FROM HRM_TR_EMPBANK");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    intResult = Convert.ToInt32(dt.Rows[0][0]) + 1;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empbank.getNextID)" + ex.ToString();
            }

            return intResult;
        }

        public int getID(string com, string empid)
        {
            int intResult = 0;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT EMPBANK_ID ");
                obj_str.Append(" FROM HRM_TR_EMPBANK");
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
                Message = "ERROR::(Empbank.getID)" + ex.ToString();
            }

            return intResult;
        }

        public bool insert(cls_TREmpbank model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.company_code, model.worker_code, model.empbank_bankaccount))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_EMPBANK");
                obj_str.Append(" (");
                obj_str.Append("COMPANY_CODE ");
                obj_str.Append(", WORKER_CODE ");
                obj_str.Append(", EMPBANK_ID ");
                obj_str.Append(", EMPBANK_BANKCODE ");
                obj_str.Append(", EMPBANK_BANKACCOUNT ");
                obj_str.Append(", EMPBANK_BANKPERCENT ");
                obj_str.Append(", EMPBANK_CASHPERCENT ");
                obj_str.Append(", EMPBANK_BANKNAME ");        
                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@COMPANY_CODE ");
                obj_str.Append(", @WORKER_CODE ");
                obj_str.Append(", @EMPBANK_ID ");
                obj_str.Append(", @EMPBANK_BANKCODE ");
                obj_str.Append(", @EMPBANK_BANKACCOUNT ");
                obj_str.Append(", @EMPBANK_BANKPERCENT ");
                obj_str.Append(", @EMPBANK_CASHPERCENT ");
                obj_str.Append(", @EMPBANK_BANKNAME ");        
                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;

                obj_cmd.Parameters.Add("@EMPBANK_ID", SqlDbType.Int); obj_cmd.Parameters["@EMPBANK_ID"].Value = this.getNextID();
                obj_cmd.Parameters.Add("@EMPBANK_BANKCODE", SqlDbType.VarChar); obj_cmd.Parameters["@EMPBANK_BANKCODE"].Value = model.empbank_bankcode;
                obj_cmd.Parameters.Add("@EMPBANK_BANKACCOUNT", SqlDbType.VarChar); obj_cmd.Parameters["@EMPBANK_BANKACCOUNT"].Value = model.empbank_bankaccount;
                obj_cmd.Parameters.Add("@EMPBANK_BANKPERCENT", SqlDbType.Decimal); obj_cmd.Parameters["@EMPBANK_BANKPERCENT"].Value = model.empbank_bankpercent;
                obj_cmd.Parameters.Add("@EMPBANK_CASHPERCENT", SqlDbType.Decimal); obj_cmd.Parameters["@EMPBANK_CASHPERCENT"].Value = model.empbank_cashpercent;

                obj_cmd.Parameters.Add("@EMPBANK_BANKNAME", SqlDbType.VarChar); obj_cmd.Parameters["@EMPBANK_BANKNAME"].Value = model.empbank_bankname;

                obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@CREATED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;
     
                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();
                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empbank.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_TREmpbank model)
        {
            string strResult = model.empbank_id.ToString();
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_TR_EMPBANK SET ");

                obj_str.Append(" EMPBANK_BANKCODE=@EMPBANK_BANKCODE ");
                obj_str.Append(", EMPBANK_BANKACCOUNT=@EMPBANK_BANKACCOUNT ");
                obj_str.Append(", EMPBANK_BANKPERCENT=@EMPBANK_BANKPERCENT ");
                obj_str.Append(", EMPBANK_CASHPERCENT=@EMPBANK_CASHPERCENT ");

                obj_str.Append(", EMPBANK_BANKNAME=@EMPBANK_BANKNAME ");

                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");
                obj_str.Append(", FLAG=@FLAG ");

                obj_str.Append(" WHERE EMPBANK_ID=@EMPBANK_ID ");
                obj_str.Append(" AND WORKER_CODE=@WORKER_CODE ");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
                if (model.empbank_id == 0)
                {
                    strResult = this.getID(model.company_code, model.worker_code).ToString();
                }
                obj_cmd.Parameters.Add("@EMPBANK_BANKCODE", SqlDbType.VarChar); obj_cmd.Parameters["@EMPBANK_BANKCODE"].Value = model.empbank_bankcode;
                obj_cmd.Parameters.Add("@EMPBANK_BANKACCOUNT", SqlDbType.VarChar); obj_cmd.Parameters["@EMPBANK_BANKACCOUNT"].Value = model.empbank_bankaccount;
                obj_cmd.Parameters.Add("@EMPBANK_BANKPERCENT", SqlDbType.Decimal); obj_cmd.Parameters["@EMPBANK_BANKPERCENT"].Value = model.empbank_bankpercent;
                obj_cmd.Parameters.Add("@EMPBANK_CASHPERCENT", SqlDbType.Decimal); obj_cmd.Parameters["@EMPBANK_CASHPERCENT"].Value = model.empbank_cashpercent;

                obj_cmd.Parameters.Add("@EMPBANK_BANKNAME", SqlDbType.VarChar); obj_cmd.Parameters["@EMPBANK_BANKNAME"].Value = model.empbank_bankname;
                                 
                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;

                obj_cmd.Parameters.Add("@EMPBANK_ID", SqlDbType.Int); obj_cmd.Parameters["@EMPBANK_ID"].Value = strResult;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empbank.update)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
