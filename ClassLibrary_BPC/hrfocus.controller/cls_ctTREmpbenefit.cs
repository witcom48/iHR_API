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
    public class cls_ctTREmpbenefit
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTREmpbenefit() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TREmpbenefit> getData(string condition)
        {
            List<cls_TREmpbenefit> list_model = new List<cls_TREmpbenefit>();
            cls_TREmpbenefit model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("EMPBENEFIT_ID");
                obj_str.Append(", HRM_TR_EMPBENEFIT.COMPANY_CODE AS COMPANY_CODE");
                obj_str.Append(", HRM_TR_EMPBENEFIT.WORKER_CODE AS WORKER_CODE");

                obj_str.Append(", HRM_TR_EMPBENEFIT.ITEM_CODE AS ITEM_CODE");

                obj_str.Append(", EMPBENEFIT_AMOUNT");
                obj_str.Append(", EMPBENEFIT_STARTDATE");
                obj_str.Append(", EMPBENEFIT_ENDDATE");

                obj_str.Append(", EMPBENEFIT_REASON");
                obj_str.Append(", ISNULL(EMPBENEFIT_NOTE, '') AS EMPBENEFIT_NOTE");

                obj_str.Append(", ISNULL(EMPBENEFIT_PAYTYPE, 'A') AS EMPBENEFIT_PAYTYPE");
                obj_str.Append(", ISNULL(EMPBENEFIT_BREAK, 0) AS EMPBENEFIT_BREAK");
                obj_str.Append(", ISNULL(EMPBENEFIT_BREAKREASON, '') AS EMPBENEFIT_BREAKREASON");

                obj_str.Append(", ISNULL(EMPBENEFIT_CONDITIONPAY, 'F') AS EMPBENEFIT_CONDITIONPAY");
                obj_str.Append(", ISNULL(EMPBENEFIT_PAYFIRST, 'Y') AS EMPBENEFIT_PAYFIRST");


                obj_str.Append(", ISNULL(HRM_TR_EMPBENEFIT.MODIFIED_BY, HRM_TR_EMPBENEFIT.CREATED_BY) AS MODIFIED_BY");
                obj_str.Append(", ISNULL(HRM_TR_EMPBENEFIT.MODIFIED_DATE, HRM_TR_EMPBENEFIT.CREATED_DATE) AS MODIFIED_DATE");

                obj_str.Append(", WORKER_INITIAL");
                obj_str.Append(", ISNULL(WORKER_FNAME_TH, '') AS WORKER_FNAME_TH");
                obj_str.Append(", ISNULL(WORKER_LNAME_TH, '') AS WORKER_LNAME_TH");
                obj_str.Append(", ISNULL(WORKER_FNAME_EN, '') AS WORKER_FNAME_EN");
                obj_str.Append(", ISNULL(WORKER_LNAME_EN, '') AS WORKER_LNAME_EN");
                obj_str.Append(", ISNULL(ITEM_NAME_TH, '') AS ITEM_NAME_TH");
                obj_str.Append(", ISNULL(ITEM_NAME_EN, '') AS ITEM_NAME_EN");

                obj_str.Append(" FROM HRM_TR_EMPBENEFIT");

                obj_str.Append(" INNER JOIN HRM_MT_WORKER ON HRM_TR_EMPBENEFIT.COMPANY_CODE=HRM_MT_WORKER.COMPANY_CODE AND HRM_TR_EMPBENEFIT.WORKER_CODE=HRM_MT_WORKER.WORKER_CODE");

                obj_str.Append(" INNER JOIN HRM_MT_ITEM ON HRM_TR_EMPBENEFIT.COMPANY_CODE=HRM_MT_ITEM.COMPANY_CODE AND HRM_TR_EMPBENEFIT.ITEM_CODE=HRM_MT_ITEM.ITEM_CODE");
                
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY HRM_TR_EMPBENEFIT.COMPANY_CODE, HRM_TR_EMPBENEFIT.WORKER_CODE, EMPBENEFIT_STARTDATE DESC");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TREmpbenefit();

                    model.company_code = dr["COMPANY_CODE"].ToString();
                    model.worker_code = dr["WORKER_CODE"].ToString();

                    model.item_code = dr["ITEM_CODE"].ToString();

                    model.empbenefit_id = Convert.ToInt32(dr["EMPBENEFIT_ID"]);

                    model.empbenefit_amount = Convert.ToDouble(dr["EMPBENEFIT_AMOUNT"]);
                    model.empbenefit_startdate = Convert.ToDateTime(dr["EMPBENEFIT_STARTDATE"]);
                    model.empbenefit_enddate = Convert.ToDateTime(dr["EMPBENEFIT_ENDDATE"]);
                    model.empbenefit_reason = dr["EMPBENEFIT_REASON"].ToString();
                    model.empbenefit_note = dr["EMPBENEFIT_NOTE"].ToString();

                    model.empbenefit_paytype = dr["EMPBENEFIT_PAYTYPE"].ToString();
                    model.empbenefit_break = Convert.ToBoolean(dr["EMPBENEFIT_BREAK"]);
                    model.empbenefit_breakreason = dr["EMPBENEFIT_BREAKREASON"].ToString();

                    model.empbenefit_conditionpay = dr["EMPBENEFIT_CONDITIONPAY"].ToString();
                    model.empbenefit_payfirst = dr["EMPBENEFIT_PAYFIRST"].ToString();
                                        
                    model.modified_by = dr["MODIFIED_BY"].ToString();
                    model.modified_date = Convert.ToDateTime(dr["MODIFIED_DATE"]);

                    model.worker_initial = dr["WORKER_INITIAL"].ToString();
                    model.worker_fname_th = dr["WORKER_FNAME_TH"].ToString();
                    model.worker_lname_th = dr["WORKER_LNAME_TH"].ToString();
                    model.worker_fname_en = dr["WORKER_FNAME_EN"].ToString();
                    model.worker_lname_en = dr["WORKER_LNAME_EN"].ToString();
                    model.item_name_th = dr["ITEM_NAME_TH"].ToString();
                    model.item_name_en = dr["ITEM_NAME_EN"].ToString();
                                                                                                                      
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Empbenefit.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TREmpbenefit> getDataByFillter(string com, string worker)
        {
            string strCondition = " AND HRM_TR_EMPBENEFIT.COMPANY_CODE='" + com + "'";

            if (!worker.Equals(""))
                strCondition += " AND HRM_TR_EMPBENEFIT.WORKER_CODE='" + worker + "'";

                        
            return this.getData(strCondition);
        }

        public List<cls_TREmpbenefit> getDataByCreateDate(string com, DateTime create)
        {
            string strCondition = " AND HRM_TR_EMPBENEFIT.COMPANY_CODE='" + com + "'";

            strCondition = " AND (HRM_TR_EMPBENEFIT.CREATED_DATE BETWEEN CONVERT(datetime,'" + create.ToString(Config.FormatDateSQL) + "') AND CONVERT(datetime, '" + create.ToString(Config.FormatDateSQL) + " 23:59:59:998') )";

            return this.getData(strCondition);
        }

        public bool checkDataOld(string com, string worker, string item, DateTime effdate)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT WORKER_CODE");
                obj_str.Append(" FROM HRM_TR_EMPBENEFIT");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND WORKER_CODE='" + worker + "'");
                obj_str.Append(" AND ITEM_CODE='" + item + "'");
                obj_str.Append(" AND EMPBENEFIT_STARTDATE='" + effdate.ToString("MM/dd/yyyy") + "'");
                                                
                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empbenefit.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public int getNextID()
        {
            int intResult = 1;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT MAX(EMPBENEFIT_ID) ");
                obj_str.Append(" FROM HRM_TR_EMPBENEFIT");             

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    intResult = Convert.ToInt32(dt.Rows[0][0]) + 1;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empbenefit.getNextID)" + ex.ToString();
            }

            return intResult;
        }

        public int getNextID(cls_ctConnection obj_conn)
        {
            int intResult = 1;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT MAX(EMPBENEFIT_ID) ");
                obj_str.Append(" FROM HRM_TR_EMPBENEFIT");

                DataTable dt = obj_conn.doGetTableTransaction(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    intResult = Convert.ToInt32(dt.Rows[0][0]) + 1;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empbenefit.getNextID)" + ex.ToString();
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

                obj_str.Append(" DELETE FROM HRM_TR_EMPBENEFIT");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND EMPBENEFIT_ID='" + id + "'");
                                              
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Empbenefit.delete)" + ex.ToString();
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

                obj_str.Append(" DELETE FROM HRM_TR_EMPBENEFIT");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND WORKER_CODE='" + emp + "'");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Empbenefit.clear)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(cls_TREmpbenefit model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.company_code, model.worker_code, model.item_code, model.empbenefit_startdate))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_EMPBENEFIT");
                obj_str.Append(" (");
                obj_str.Append("EMPBENEFIT_ID ");
                obj_str.Append(", COMPANY_CODE ");
                obj_str.Append(", WORKER_CODE ");
                obj_str.Append(", ITEM_CODE ");
                obj_str.Append(", EMPBENEFIT_AMOUNT ");
                obj_str.Append(", EMPBENEFIT_STARTDATE ");
                obj_str.Append(", EMPBENEFIT_ENDDATE ");
                obj_str.Append(", EMPBENEFIT_REASON ");
                obj_str.Append(", EMPBENEFIT_NOTE ");

                obj_str.Append(", EMPBENEFIT_PAYTYPE ");
                obj_str.Append(", EMPBENEFIT_BREAK ");
                obj_str.Append(", EMPBENEFIT_BREAKREASON ");

                obj_str.Append(", EMPBENEFIT_CONDITIONPAY ");
                obj_str.Append(", EMPBENEFIT_PAYFIRST ");     

                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@EMPBENEFIT_ID ");
                obj_str.Append(", @COMPANY_CODE ");
                obj_str.Append(", @WORKER_CODE ");
                obj_str.Append(", @ITEM_CODE ");
                obj_str.Append(", @EMPBENEFIT_AMOUNT ");
                obj_str.Append(", @EMPBENEFIT_STARTDATE ");
                obj_str.Append(", @EMPBENEFIT_ENDDATE ");
                obj_str.Append(", @EMPBENEFIT_REASON ");
                obj_str.Append(", @EMPBENEFIT_NOTE ");

                obj_str.Append(", @EMPBENEFIT_PAYTYPE ");
                obj_str.Append(", @EMPBENEFIT_BREAK ");
                obj_str.Append(", @EMPBENEFIT_BREAKREASON ");

                obj_str.Append(", @EMPBENEFIT_CONDITIONPAY ");
                obj_str.Append(", @EMPBENEFIT_PAYFIRST ");     

                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@EMPBENEFIT_ID", SqlDbType.Int); obj_cmd.Parameters["@EMPBENEFIT_ID"].Value = this.getNextID();
                
                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;

                obj_cmd.Parameters.Add("@ITEM_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@ITEM_CODE"].Value = model.item_code;
                obj_cmd.Parameters.Add("@EMPBENEFIT_AMOUNT", SqlDbType.Decimal); obj_cmd.Parameters["@EMPBENEFIT_AMOUNT"].Value = model.empbenefit_amount;
                obj_cmd.Parameters.Add("@EMPBENEFIT_STARTDATE", SqlDbType.DateTime); obj_cmd.Parameters["@EMPBENEFIT_STARTDATE"].Value = model.empbenefit_startdate;
                obj_cmd.Parameters.Add("@EMPBENEFIT_ENDDATE", SqlDbType.DateTime); obj_cmd.Parameters["@EMPBENEFIT_ENDDATE"].Value = model.empbenefit_enddate;
                obj_cmd.Parameters.Add("@EMPBENEFIT_REASON", SqlDbType.VarChar); obj_cmd.Parameters["@EMPBENEFIT_REASON"].Value = model.empbenefit_reason;
                obj_cmd.Parameters.Add("@EMPBENEFIT_NOTE", SqlDbType.VarChar); obj_cmd.Parameters["@EMPBENEFIT_NOTE"].Value = model.empbenefit_note;

                obj_cmd.Parameters.Add("@EMPBENEFIT_PAYTYPE", SqlDbType.VarChar); obj_cmd.Parameters["@EMPBENEFIT_PAYTYPE"].Value = model.empbenefit_paytype;
                obj_cmd.Parameters.Add("@EMPBENEFIT_BREAK", SqlDbType.Bit); obj_cmd.Parameters["@EMPBENEFIT_BREAK"].Value = model.empbenefit_break;
                obj_cmd.Parameters.Add("@EMPBENEFIT_BREAKREASON", SqlDbType.VarChar); obj_cmd.Parameters["@EMPBENEFIT_BREAKREASON"].Value = model.empbenefit_breakreason;

                obj_cmd.Parameters.Add("@EMPBENEFIT_CONDITIONPAY", SqlDbType.VarChar); obj_cmd.Parameters["@EMPBENEFIT_CONDITIONPAY"].Value = model.empbenefit_conditionpay;
                obj_cmd.Parameters.Add("@EMPBENEFIT_PAYFIRST", SqlDbType.VarChar); obj_cmd.Parameters["@EMPBENEFIT_PAYFIRST"].Value = model.empbenefit_payfirst;
                

                obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@CREATED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;
     
                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();
                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empbenefit.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_TREmpbenefit model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

              
                obj_str.Append("UPDATE HRM_TR_EMPBENEFIT SET ");

                obj_str.Append(" ITEM_CODE=@ITEM_CODE ");
                obj_str.Append(", EMPBENEFIT_AMOUNT=@EMPBENEFIT_AMOUNT ");
                obj_str.Append(", EMPBENEFIT_STARTDATE=@EMPBENEFIT_STARTDATE ");
                obj_str.Append(", EMPBENEFIT_ENDDATE=@EMPBENEFIT_ENDDATE ");
                obj_str.Append(", EMPBENEFIT_REASON=@EMPBENEFIT_REASON ");
                obj_str.Append(", EMPBENEFIT_NOTE=@EMPBENEFIT_NOTE ");

                obj_str.Append(", EMPBENEFIT_PAYTYPE=@EMPBENEFIT_PAYTYPE ");
                obj_str.Append(", EMPBENEFIT_BREAK=@EMPBENEFIT_BREAK ");
                obj_str.Append(", EMPBENEFIT_BREAKREASON=@EMPBENEFIT_BREAKREASON ");

                obj_str.Append(", EMPBENEFIT_CONDITIONPAY=@EMPBENEFIT_CONDITIONPAY ");
                obj_str.Append(", EMPBENEFIT_PAYFIRST=@EMPBENEFIT_PAYFIRST ");
                              
                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");
                obj_str.Append(", FLAG=@FLAG ");

                obj_str.Append(" WHERE EMPBENEFIT_ID=@EMPBENEFIT_ID ");
               
                
                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@ITEM_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@ITEM_CODE"].Value = model.item_code;
                obj_cmd.Parameters.Add("@EMPBENEFIT_AMOUNT", SqlDbType.Decimal); obj_cmd.Parameters["@EMPBENEFIT_AMOUNT"].Value = model.empbenefit_amount;
                obj_cmd.Parameters.Add("@EMPBENEFIT_STARTDATE", SqlDbType.DateTime); obj_cmd.Parameters["@EMPBENEFIT_STARTDATE"].Value = model.empbenefit_startdate;
                obj_cmd.Parameters.Add("@EMPBENEFIT_ENDDATE", SqlDbType.DateTime); obj_cmd.Parameters["@EMPBENEFIT_ENDDATE"].Value = model.empbenefit_enddate;
                obj_cmd.Parameters.Add("@EMPBENEFIT_REASON", SqlDbType.VarChar); obj_cmd.Parameters["@EMPBENEFIT_REASON"].Value = model.empbenefit_reason;
                obj_cmd.Parameters.Add("@EMPBENEFIT_NOTE", SqlDbType.VarChar); obj_cmd.Parameters["@EMPBENEFIT_NOTE"].Value = model.empbenefit_note;

                obj_cmd.Parameters.Add("@EMPBENEFIT_PAYTYPE", SqlDbType.VarChar); obj_cmd.Parameters["@EMPBENEFIT_PAYTYPE"].Value = model.empbenefit_paytype;
                obj_cmd.Parameters.Add("@EMPBENEFIT_BREAK", SqlDbType.Bit); obj_cmd.Parameters["@EMPBENEFIT_BREAK"].Value = model.empbenefit_break;
                obj_cmd.Parameters.Add("@EMPBENEFIT_BREAKREASON", SqlDbType.VarChar); obj_cmd.Parameters["@EMPBENEFIT_BREAKREASON"].Value = model.empbenefit_breakreason;

                obj_cmd.Parameters.Add("@EMPBENEFIT_CONDITIONPAY", SqlDbType.VarChar); obj_cmd.Parameters["@EMPBENEFIT_CONDITIONPAY"].Value = model.empbenefit_conditionpay;
                obj_cmd.Parameters.Add("@EMPBENEFIT_PAYFIRST", SqlDbType.VarChar); obj_cmd.Parameters["@EMPBENEFIT_PAYFIRST"].Value = model.empbenefit_payfirst;
                

                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;

                obj_cmd.Parameters.Add("@EMPBENEFIT_ID", SqlDbType.Int); obj_cmd.Parameters["@EMPBENEFIT_ID"].Value = model.empbenefit_id;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empbenefit.update)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(List<cls_TREmpbenefit> list_model)
        {
            bool blnResult = false;
            cls_ctConnection obj_conn = new cls_ctConnection();

            try
            {
                
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_EMPBENEFIT");
                obj_str.Append(" (");
                obj_str.Append("EMPBENEFIT_ID ");
                obj_str.Append(", COMPANY_CODE ");
                obj_str.Append(", WORKER_CODE ");
                obj_str.Append(", ITEM_CODE ");
                obj_str.Append(", EMPBENEFIT_AMOUNT ");
                obj_str.Append(", EMPBENEFIT_STARTDATE ");
                obj_str.Append(", EMPBENEFIT_ENDDATE ");
                obj_str.Append(", EMPBENEFIT_REASON ");
                obj_str.Append(", EMPBENEFIT_NOTE ");

                obj_str.Append(", EMPBENEFIT_PAYTYPE ");
                obj_str.Append(", EMPBENEFIT_BREAK ");
                obj_str.Append(", EMPBENEFIT_BREAKREASON ");  

                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@EMPBENEFIT_ID ");
                obj_str.Append(", @COMPANY_CODE ");
                obj_str.Append(", @WORKER_CODE ");
                obj_str.Append(", @ITEM_CODE ");
                obj_str.Append(", @EMPBENEFIT_AMOUNT ");
                obj_str.Append(", @EMPBENEFIT_STARTDATE ");
                obj_str.Append(", @EMPBENEFIT_ENDDATE ");
                obj_str.Append(", @EMPBENEFIT_REASON ");
                obj_str.Append(", @EMPBENEFIT_NOTE ");

                obj_str.Append(", @EMPBENEFIT_PAYTYPE ");
                obj_str.Append(", @EMPBENEFIT_BREAK ");
                obj_str.Append(", @EMPBENEFIT_BREAKREASON ");  

                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");


                obj_conn.doConnect();

                obj_conn.doOpenTransaction();

                //-- Step 1 delete data old
                string strWorkerID = "";
                foreach (cls_TREmpbenefit model in list_model)
                {
                    strWorkerID += "'" + model.worker_code + "',";
                }
                if (strWorkerID.Length > 0)
                    strWorkerID = strWorkerID.Substring(0, strWorkerID.Length - 1);

                System.Text.StringBuilder obj_str2 = new System.Text.StringBuilder();

                obj_str2.Append(" DELETE FROM HRM_TR_EMPBENEFIT");
                obj_str2.Append(" WHERE 1=1 ");
                obj_str2.Append(" AND COMPANY_CODE='" + list_model[0].company_code + "'");
                obj_str2.Append(" AND WORKER_CODE IN (" + strWorkerID + ")");
                obj_str2.Append(" AND ITEM_CODE='" + list_model[0].item_code + "'");

                blnResult = obj_conn.doExecuteSQL_transaction(obj_str2.ToString());

                if (blnResult)
                {

                    SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
                    obj_cmd.Transaction = obj_conn.getTransaction();

                    obj_cmd.Parameters.Add("@EMPBENEFIT_ID", SqlDbType.Int);
                    obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); 
                    obj_cmd.Parameters.Add("@ITEM_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@EMPBENEFIT_AMOUNT", SqlDbType.Decimal);
                    obj_cmd.Parameters.Add("@EMPBENEFIT_STARTDATE", SqlDbType.DateTime);
                    obj_cmd.Parameters.Add("@EMPBENEFIT_ENDDATE", SqlDbType.DateTime);
                    obj_cmd.Parameters.Add("@EMPBENEFIT_REASON", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@EMPBENEFIT_NOTE", SqlDbType.VarChar);

                    obj_cmd.Parameters.Add("@EMPBENEFIT_PAYTYPE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@EMPBENEFIT_BREAK", SqlDbType.Bit);
                    obj_cmd.Parameters.Add("@EMPBENEFIT_BREAKREASON", SqlDbType.VarChar);                 

                    obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime);
                    obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit);


                    foreach (cls_TREmpbenefit model in list_model)
                    {

                        obj_cmd.Parameters["@EMPBENEFIT_ID"].Value = this.getNextID(obj_conn);

                        obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                        obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;

                        obj_cmd.Parameters["@ITEM_CODE"].Value = model.item_code;
                        obj_cmd.Parameters["@EMPBENEFIT_AMOUNT"].Value = model.empbenefit_amount;
                        obj_cmd.Parameters["@EMPBENEFIT_STARTDATE"].Value = model.empbenefit_startdate;
                        obj_cmd.Parameters["@EMPBENEFIT_ENDDATE"].Value = model.empbenefit_enddate;
                        obj_cmd.Parameters["@EMPBENEFIT_REASON"].Value = model.empbenefit_reason;
                        obj_cmd.Parameters["@EMPBENEFIT_NOTE"].Value = model.empbenefit_note;

                        obj_cmd.Parameters["@EMPBENEFIT_PAYTYPE"].Value = model.empbenefit_paytype;
                        obj_cmd.Parameters["@EMPBENEFIT_BREAK"].Value = model.empbenefit_break;
                        obj_cmd.Parameters["@EMPBENEFIT_BREAKREASON"].Value = model.empbenefit_breakreason;
                
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
