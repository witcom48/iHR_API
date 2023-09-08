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
    public class cls_ctTREmpsalary
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTREmpsalary() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TREmpsalary> getData(string condition)
        {
            List<cls_TREmpsalary> list_model = new List<cls_TREmpsalary>();
            cls_TREmpsalary model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("EMPSALARY_ID");
                obj_str.Append(", HRM_TR_EMPSALARY.COMPANY_CODE AS COMPANY_CODE");
                obj_str.Append(", HRM_TR_EMPSALARY.WORKER_CODE AS WORKER_CODE");
                
                obj_str.Append(", EMPSALARY_AMOUNT");
                obj_str.Append(", EMPSALARY_DATE");
                obj_str.Append(", EMPSALARY_REASON");

                obj_str.Append(", ISNULL(EMPSALARY_INCAMOUNT, 0) AS EMPSALARY_INCAMOUNT");
                obj_str.Append(", ISNULL(EMPSALARY_INCPERCENT, 0) AS EMPSALARY_INCPERCENT");

                obj_str.Append(", ISNULL(HRM_TR_EMPSALARY.MODIFIED_BY, HRM_TR_EMPSALARY.CREATED_BY) AS MODIFIED_BY");
                obj_str.Append(", ISNULL(HRM_TR_EMPSALARY.MODIFIED_DATE, HRM_TR_EMPSALARY.CREATED_DATE) AS MODIFIED_DATE");

                obj_str.Append(", WORKER_INITIAL");
                obj_str.Append(", ISNULL(WORKER_FNAME_TH, '') AS WORKER_FNAME_TH");
                obj_str.Append(", ISNULL(WORKER_LNAME_TH, '') AS WORKER_LNAME_TH");
                obj_str.Append(", ISNULL(WORKER_FNAME_EN, '') AS WORKER_FNAME_EN");
                obj_str.Append(", ISNULL(WORKER_LNAME_EN, '') AS WORKER_LNAME_EN");

                obj_str.Append(" FROM HRM_TR_EMPSALARY");

                obj_str.Append(" INNER JOIN HRM_MT_WORKER ON HRM_TR_EMPSALARY.COMPANY_CODE=HRM_MT_WORKER.COMPANY_CODE AND HRM_TR_EMPSALARY.WORKER_CODE=HRM_MT_WORKER.WORKER_CODE");

                
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY HRM_TR_EMPSALARY.COMPANY_CODE, HRM_TR_EMPSALARY.WORKER_CODE, EMPSALARY_DATE DESC");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TREmpsalary();

                    model.company_code = dr["COMPANY_CODE"].ToString();
                    model.worker_code = dr["WORKER_CODE"].ToString();

                    model.empsalary_id = Convert.ToInt32(dr["EMPSALARY_ID"]);

                    model.empsalary_amount = Convert.ToDouble(dr["EMPSALARY_AMOUNT"]);
                    model.empsalary_date = Convert.ToDateTime(dr["EMPSALARY_DATE"]);
                    model.empsalary_reason = dr["EMPSALARY_REASON"].ToString();

                    model.empsalary_incamount = Convert.ToDouble(dr["EMPSALARY_INCAMOUNT"]);
                    model.empsalary_incpercent = Convert.ToDouble(dr["EMPSALARY_INCPERCENT"]);

                    
                    model.modified_by = dr["MODIFIED_BY"].ToString();
                    model.modified_date = Convert.ToDateTime(dr["MODIFIED_DATE"]);

                    model.worker_initial = dr["WORKER_INITIAL"].ToString();
                    model.worker_fname_th = dr["WORKER_FNAME_TH"].ToString();
                    model.worker_lname_th = dr["WORKER_LNAME_TH"].ToString();
                    model.worker_fname_en = dr["WORKER_FNAME_EN"].ToString();
                    model.worker_lname_en = dr["WORKER_LNAME_EN"].ToString();
                                                                                                                      
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Empsalary.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TREmpsalary> getDataByFillter(string com, string worker)
        {
            string strCondition = " AND HRM_TR_EMPSALARY.COMPANY_CODE='" + com + "'";

            if (!worker.Equals(""))
                strCondition += " AND HRM_TR_EMPSALARY.WORKER_CODE='" + worker + "'";

                        
            return this.getData(strCondition);
        }

        public List<cls_TREmpsalary> getDataByCreateDate(string com, DateTime create)
        {
            string strCondition = " AND HRM_TR_EMPSALARY.COMPANY_CODE='" + com + "'";

            strCondition = " AND (HRM_TR_EMPSALARY.CREATED_DATE BETWEEN CONVERT(datetime,'" + create.ToString(Config.FormatDateSQL) + "') AND CONVERT(datetime, '" + create.ToString(Config.FormatDateSQL) + " 23:59:59:998') )";

            return this.getData(strCondition);
        }

        public bool checkDataOld(string com, string worker, DateTime effdate)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT COMPANY_CODE");
                obj_str.Append(" FROM HRM_TR_EMPSALARY");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND WORKER_CODE='" + worker + "'");
                obj_str.Append(" AND EMPSALARY_DATE='" + effdate.ToString("MM/dd/yyyy") + "'");
                                                
                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empsalary.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public int getNextID()
        {
            int intResult = 1;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT MAX(EMPSALARY_ID) ");
                obj_str.Append(" FROM HRM_TR_EMPSALARY");             

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    intResult = Convert.ToInt32(dt.Rows[0][0]) + 1;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empsalary.getNextID)" + ex.ToString();
            }

            return intResult;
        }

        public int getNextID(cls_ctConnection obj_conn)
        {
            int intResult = 1;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT MAX(EMPSALARY_ID) ");
                obj_str.Append(" FROM HRM_TR_EMPSALARY");

                DataTable dt = obj_conn.doGetTableTransaction(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    intResult = Convert.ToInt32(dt.Rows[0][0]) + 1;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empsalary.getNextID)" + ex.ToString();
            }

            return intResult;
        }

        public int getID(string com, string empid)
        {
            int intResult = 0;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT EMPSALARY_ID ");
                obj_str.Append(" FROM HRM_TR_EMPSALARY");
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
                Message = "ERROR::(Empsalary.getID)" + ex.ToString();
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

                obj_str.Append(" DELETE FROM HRM_TR_EMPSALARY");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND EMPSALARY_ID='" + id + "'");
                                              
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Empsalary.delete)" + ex.ToString();
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

                obj_str.Append(" DELETE FROM HRM_TR_EMPSALARY");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND WORKER_CODE='" + emp + "'");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Empsalary.clear)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(cls_TREmpsalary model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.company_code, model.worker_code, model.empsalary_date))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_EMPSALARY");
                obj_str.Append(" (");
                obj_str.Append("EMPSALARY_ID ");
                obj_str.Append(", COMPANY_CODE ");
                obj_str.Append(", WORKER_CODE ");
                obj_str.Append(", EMPSALARY_AMOUNT ");
                obj_str.Append(", EMPSALARY_DATE ");
                obj_str.Append(", EMPSALARY_REASON ");

                obj_str.Append(", EMPSALARY_INCAMOUNT ");
                obj_str.Append(", EMPSALARY_INCPERCENT "); 

                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@EMPSALARY_ID ");
                obj_str.Append(", @COMPANY_CODE ");
                obj_str.Append(", @WORKER_CODE ");
                obj_str.Append(", @EMPSALARY_AMOUNT ");
                obj_str.Append(", @EMPSALARY_DATE ");
                obj_str.Append(", @EMPSALARY_REASON ");

                obj_str.Append(", @EMPSALARY_INCAMOUNT ");
                obj_str.Append(", @EMPSALARY_INCPERCENT "); 

                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@EMPSALARY_ID", SqlDbType.Int); obj_cmd.Parameters["@EMPSALARY_ID"].Value = this.getNextID();
                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                obj_cmd.Parameters.Add("@EMPSALARY_AMOUNT", SqlDbType.Decimal); obj_cmd.Parameters["@EMPSALARY_AMOUNT"].Value = model.empsalary_amount;
                obj_cmd.Parameters.Add("@EMPSALARY_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@EMPSALARY_DATE"].Value = model.empsalary_date;
                obj_cmd.Parameters.Add("@EMPSALARY_REASON", SqlDbType.VarChar); obj_cmd.Parameters["@EMPSALARY_REASON"].Value = model.empsalary_reason;

                obj_cmd.Parameters.Add("@EMPSALARY_INCAMOUNT", SqlDbType.Decimal); obj_cmd.Parameters["@EMPSALARY_INCAMOUNT"].Value = model.empsalary_incamount;
                obj_cmd.Parameters.Add("@EMPSALARY_INCPERCENT", SqlDbType.Decimal); obj_cmd.Parameters["@EMPSALARY_INCPERCENT"].Value = model.empsalary_incpercent;

                obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@CREATED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;
     
                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();
                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empsalary.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_TREmpsalary model)
        {
            string strResult = model.empsalary_id.ToString();
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_TR_EMPSALARY SET ");

                obj_str.Append(" EMPSALARY_AMOUNT=@EMPSALARY_AMOUNT ");
                obj_str.Append(", EMPSALARY_REASON=@EMPSALARY_REASON ");

                obj_str.Append(", EMPSALARY_INCAMOUNT=@EMPSALARY_INCAMOUNT ");
                obj_str.Append(", EMPSALARY_INCPERCENT=@EMPSALARY_INCPERCENT "); 
              
                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");
                obj_str.Append(", FLAG=@FLAG ");

                obj_str.Append(" WHERE EMPSALARY_ID=@EMPSALARY_ID ");
                obj_str.Append(" AND WORKER_CODE=@WORKER_CODE ");
                
                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
                if (model.empsalary_id == 0)
                {
                    strResult = this.getID(model.company_code, model.worker_code).ToString();
                }
                obj_cmd.Parameters.Add("@EMPSALARY_AMOUNT", SqlDbType.Decimal); obj_cmd.Parameters["@EMPSALARY_AMOUNT"].Value = model.empsalary_amount;
                obj_cmd.Parameters.Add("@EMPSALARY_REASON", SqlDbType.VarChar); obj_cmd.Parameters["@EMPSALARY_REASON"].Value = model.empsalary_reason;

                obj_cmd.Parameters.Add("@EMPSALARY_INCAMOUNT", SqlDbType.Decimal); obj_cmd.Parameters["@EMPSALARY_INCAMOUNT"].Value = model.empsalary_incamount;
                obj_cmd.Parameters.Add("@EMPSALARY_INCPERCENT", SqlDbType.Decimal); obj_cmd.Parameters["@EMPSALARY_INCPERCENT"].Value = model.empsalary_incpercent;

                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;

                obj_cmd.Parameters.Add("@EMPSALARY_ID", SqlDbType.Int); obj_cmd.Parameters["@EMPSALARY_ID"].Value = strResult;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empsalary.update)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(DateTime dateStart, List<cls_TREmpsalary> list_model)
        {
            bool blnResult = false;
            cls_ctConnection obj_conn = new cls_ctConnection();

            try
            {

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_EMPSALARY");
                obj_str.Append(" (");
                obj_str.Append("EMPSALARY_ID ");
                obj_str.Append(", COMPANY_CODE ");
                obj_str.Append(", WORKER_CODE ");
                obj_str.Append(", EMPSALARY_AMOUNT ");
                obj_str.Append(", EMPSALARY_DATE ");
                obj_str.Append(", EMPSALARY_REASON ");

                obj_str.Append(", EMPSALARY_INCAMOUNT ");
                obj_str.Append(", EMPSALARY_INCPERCENT ");

                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@EMPSALARY_ID ");
                obj_str.Append(", @COMPANY_CODE ");
                obj_str.Append(", @WORKER_CODE ");
                obj_str.Append(", @EMPSALARY_AMOUNT ");
                obj_str.Append(", @EMPSALARY_DATE ");
                obj_str.Append(", @EMPSALARY_REASON ");

                obj_str.Append(", @EMPSALARY_INCAMOUNT ");
                obj_str.Append(", @EMPSALARY_INCPERCENT ");

                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");


                obj_conn.doConnect();

                obj_conn.doOpenTransaction();

                //-- Step 1 delete data old
                string strWorkerID = "";
                foreach (cls_TREmpsalary model in list_model)
                {
                    strWorkerID += "'" + model.worker_code + "',";
                }
                if (strWorkerID.Length > 0)
                    strWorkerID = strWorkerID.Substring(0, strWorkerID.Length - 1);

                System.Text.StringBuilder obj_str2 = new System.Text.StringBuilder();

                obj_str2.Append(" DELETE FROM HRM_TR_EMPSALARY");               
                obj_str2.Append(" WHERE COMPANY_CODE='" + list_model[0].company_code + "'");
                obj_str2.Append(" AND WORKER_CODE IN (" + strWorkerID + ")");
                obj_str2.Append(" AND EMPSALARY_DATE='" + dateStart.ToString(Config.FormatDateSQL) + "'");

                blnResult = obj_conn.doExecuteSQL_transaction(obj_str2.ToString());

                if (blnResult)
                {

                    SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
                    obj_cmd.Transaction = obj_conn.getTransaction();

                    obj_cmd.Parameters.Add("@EMPSALARY_ID", SqlDbType.Int); 
                    obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@EMPSALARY_AMOUNT", SqlDbType.Decimal);
                    obj_cmd.Parameters.Add("@EMPSALARY_DATE", SqlDbType.DateTime);
                    obj_cmd.Parameters.Add("@EMPSALARY_REASON", SqlDbType.VarChar);

                    obj_cmd.Parameters.Add("@EMPSALARY_INCAMOUNT", SqlDbType.Decimal);
                    obj_cmd.Parameters.Add("@EMPSALARY_INCPERCENT", SqlDbType.Decimal);

                    obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime); 
                    obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit);

                    foreach (cls_TREmpsalary model in list_model)
                    {

                        obj_cmd.Parameters["@EMPSALARY_ID"].Value = this.getNextID(obj_conn);

                        obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                        obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                        obj_cmd.Parameters["@EMPSALARY_AMOUNT"].Value = model.empsalary_amount;
                        obj_cmd.Parameters["@EMPSALARY_DATE"].Value = model.empsalary_date;
                        obj_cmd.Parameters["@EMPSALARY_REASON"].Value = model.empsalary_reason;

                        obj_cmd.Parameters["@EMPSALARY_INCAMOUNT"].Value = model.empsalary_incamount;
                        obj_cmd.Parameters["@EMPSALARY_INCPERCENT"].Value = model.empsalary_incpercent;

                        obj_cmd.Parameters["@CREATED_BY"].Value = model.modified_by;
                        obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                        obj_cmd.Parameters["@FLAG"].Value = false;

                        obj_cmd.ExecuteNonQuery();

                    }

                    blnResult = obj_conn.doCommit();

                    if (!blnResult)
                        obj_conn.doRollback();


                }
                else
                {
                    obj_conn.doRollback();

                }

            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empbenefit.insert)" + ex.ToString();
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
