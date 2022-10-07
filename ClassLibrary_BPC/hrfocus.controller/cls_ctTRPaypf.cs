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
    public class cls_ctTRPaypf
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTRPaypf() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TRPaypf> getData(string language, string condition)
        {
            List<cls_TRPaypf> list_model = new List<cls_TRPaypf>();
            cls_TRPaypf model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("HRM_TR_PAYPF.COMPANY_CODE");
                obj_str.Append(", HRM_TR_PAYPF.WORKER_CODE");
                obj_str.Append(", HRM_TR_PAYPF.PROVIDENT_CODE");          
                obj_str.Append(", PAYPF_DATE");

                obj_str.Append(", PAYPF_EMP_RATE");
                obj_str.Append(", PAYPF_EMP_AMOUNT");
                obj_str.Append(", PAYPF_COM_RATE");
                obj_str.Append(", PAYPF_COM_AMOUNT");

                
                obj_str.Append(", ISNULL(HRM_TR_PAYPF.MODIFIED_BY, HRM_TR_PAYPF.CREATED_BY) AS MODIFIED_BY");
                obj_str.Append(", ISNULL(HRM_TR_PAYPF.MODIFIED_DATE, HRM_TR_PAYPF.CREATED_DATE) AS MODIFIED_DATE");
                
                if (language.Equals("TH"))
                {
                    obj_str.Append(", INITIAL_NAME_TH + WORKER_FNAME_TH + ' ' + WORKER_LNAME_TH AS WORKER_DETAIL");
                }
                else
                {                    
                    obj_str.Append(", INITIAL_NAME_EN + WORKER_FNAME_EN + ' ' + WORKER_LNAME_EN AS WORKER_DETAIL");
                }
                                
                obj_str.Append(" FROM HRM_TR_PAYPF");
                obj_str.Append(" INNER JOIN HRM_MT_WORKER ON HRM_MT_WORKER.COMPANY_CODE=HRM_TR_PAYPF.COMPANY_CODE AND HRM_MT_WORKER.WORKER_CODE=HRM_TR_PAYPF.WORKER_CODE");
                obj_str.Append(" INNER JOIN HRM_MT_INITIAL ON HRM_MT_INITIAL.INITIAL_CODE=HRM_MT_WORKER.WORKER_INITIAL ");
                
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY HRM_TR_PAYPF.COMPANY_CODE, PAYPF_DATE, HRM_TR_PAYPF.WORKER_CODE");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TRPaypf();

                    model.company_code = Convert.ToString(dr["COMPANY_CODE"]);
                    model.worker_code = Convert.ToString(dr["WORKER_CODE"]);
                    model.provident_code = Convert.ToString(dr["PROVIDENT_CODE"]);
                    model.paypf_date = Convert.ToDateTime(dr["PAYPF_DATE"]);


                    model.paypf_emp_rate = Convert.ToDouble(dr["PAYPF_EMP_RATE"]);
                    model.paypf_emp_amount = Convert.ToDouble(dr["PAYPF_EMP_AMOUNT"]);
                    model.paypf_com_rate = Convert.ToDouble(dr["PAYPF_COM_RATE"]);
                    model.paypf_com_amount = Convert.ToDouble(dr["PAYPF_COM_AMOUNT"]);                    

                    model.worker_detail = Convert.ToString(dr["WORKER_DETAIL"]);

                    model.modified_by = dr["MODIFIED_BY"].ToString();
                    model.modified_date = Convert.ToDateTime(dr["MODIFIED_DATE"]);

                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Paytran.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TRPaypf> getDataByFillter(string language, string com, DateTime datefrom, DateTime dateto, string emp)
        {
            string strCondition = "";

            strCondition += " AND HRM_TR_PAYPF.COMPANY_CODE='" + com + "'";
            strCondition += " AND (HRM_TR_PAYPF.PAYPF_DATE BETWEEN '" + datefrom.ToString("MM/dd/yyyy") + "' AND '" + dateto.ToString("MM/dd/yyyy") + "')";

            if (!emp.Equals(""))
                strCondition += " AND HRM_TR_PAYPF.WORKER_CODE='" + emp + "'";
            
            return this.getData(language, strCondition);
        }

        public List<cls_TRPaypf> getDataByYear(string language, string com, string year, string emp)
        {
            string strCondition = "";

            strCondition += " AND HRM_TR_PAYPF.COMPANY_CODE='" + com + "'";

            strCondition += " AND HRM_TR_PAYPF.PAYPF_DATE IN (SELECT PERIOD_PAYMENT FROM HRM_MT_PERIOD WHERE COMPANY_CODE='" + com + "' AND YEAR_CODE='" + year + "')";

            if (!emp.Equals(""))
                strCondition += " AND HRM_TR_PAYPF.WORKER_CODE='" + emp + "'";

            return this.getData(language, strCondition);
        }

        public List<cls_TRPaypf> getDataMultipleEmp(string language, string com, DateTime datefrom, DateTime dateto, string emp)
        {
            string strCondition = "";

            strCondition += " AND HRM_TR_PAYPF.COMPANY_CODE='" + com + "'";
            strCondition += " AND (HRM_TR_PAYPF.PAYPF_DATE BETWEEN '" + datefrom.ToString("MM/dd/yyyy") + "' AND '" + dateto.ToString("MM/dd/yyyy") + "')";
            strCondition += " AND HRM_TR_PAYPF.WORKER_CODE IN (" + emp + ")";

            return this.getData(language, strCondition);
        }

        public bool checkDataOld(string com, string emp, DateTime date)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT PAYPF_DATE");
                obj_str.Append(" FROM HRM_TR_PAYPF");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "' ");
                obj_str.Append(" AND WORKER_CODE='" + emp + "' ");               
                obj_str.Append(" AND PAYPF_DATE='" + date.ToString("MM/dd/yyyy") + "' ");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Paytran.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }
                
        public bool delete(string com, string emp, DateTime date)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_TR_PAYPF");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "' ");
                obj_str.Append(" AND WORKER_CODE='" + emp + "' ");                
                obj_str.Append(" AND PAYPF_DATE='" + date.ToString("MM/dd/yyyy") + "' ");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());               

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Paytran.delete)" + ex.ToString();
            }

            return blnResult;
        }
                
        public bool insert(cls_TRPaypf model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.company_code, model.worker_code, model.paypf_date))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_PAYPF");
                obj_str.Append(" (");
                obj_str.Append("COMPANY_CODE");
                obj_str.Append(", WORKER_CODE");
                obj_str.Append(", PROVIDENT_CODE");
                obj_str.Append(", PAYPF_DATE");

                obj_str.Append(", PAYPF_EMP_RATE");
                obj_str.Append(", PAYPF_EMP_AMOUNT");
                obj_str.Append(", PAYPF_COM_RATE");
                obj_str.Append(", PAYPF_COM_AMOUNT");


                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@COMPANY_CODE");
                obj_str.Append(", @WORKER_CODE");
                obj_str.Append(", @PROVIDENT_CODE");
                obj_str.Append(", @PAYPF_DATE");

                obj_str.Append(", @PAYPF_EMP_RATE");
                obj_str.Append(", @PAYPF_EMP_AMOUNT");
                obj_str.Append(", @PAYPF_COM_RATE");
                obj_str.Append(", @PAYPF_COM_AMOUNT");


                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                obj_cmd.Parameters.Add("@PROVIDENT_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@PROVIDENT_CODE"].Value = model.provident_code;
                obj_cmd.Parameters.Add("@PAYPF_DATE", SqlDbType.Date); obj_cmd.Parameters["@PAYPF_DATE"].Value = model.paypf_date;

                obj_cmd.Parameters.Add("@PAYPF_EMP_RATE", SqlDbType.Decimal); obj_cmd.Parameters["@PAYPF_EMP_RATE"].Value = model.paypf_emp_rate;
                obj_cmd.Parameters.Add("@PAYPF_EMP_AMOUNT", SqlDbType.Decimal); obj_cmd.Parameters["@PAYPF_EMP_AMOUNT"].Value = model.paypf_emp_amount;
                obj_cmd.Parameters.Add("@PAYPF_COM_RATE", SqlDbType.Decimal); obj_cmd.Parameters["@PAYPF_COM_RATE"].Value = model.paypf_com_rate;
                obj_cmd.Parameters.Add("@PAYPF_COM_AMOUNT", SqlDbType.Decimal); obj_cmd.Parameters["@PAYPF_COM_AMOUNT"].Value = model.paypf_com_amount;

                obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@CREATED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {              
                Message = "ERROR::(Paytran.insert)" + ex.ToString();
            }

            return blnResult;
        }
        
        public bool update(cls_TRPaypf model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_TR_PAYPF SET ");

                obj_str.Append(" PAYPF_EMP_RATE=@PAYPF_EMP_RATE");
                obj_str.Append(", PAYPF_EMP_AMOUNT=@PAYPF_EMP_AMOUNT");
                obj_str.Append(", PAYPF_COM_RATE=@PAYPF_COM_RATE");
                obj_str.Append(", PAYPF_COM_AMOUNT=@PAYPF_COM_AMOUNT");
                
                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");

                obj_str.Append(" WHERE COMPANY_CODE=@COMPANY_CODE ");
                obj_str.Append(" AND WORKER_CODE=@WORKER_CODE ");
                obj_str.Append(" AND PROVIDENT_CODE=@PROVIDENT_CODE ");
                obj_str.Append(" AND PAYPF_DATE=@PAYPF_DATE ");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@PAYPF_EMP_RATE", SqlDbType.Decimal); obj_cmd.Parameters["@PAYPF_EMP_RATE"].Value = model.paypf_emp_rate;
                obj_cmd.Parameters.Add("@PAYPF_EMP_AMOUNT", SqlDbType.Decimal); obj_cmd.Parameters["@PAYPF_EMP_AMOUNT"].Value = model.paypf_emp_amount;
                obj_cmd.Parameters.Add("@PAYPF_COM_RATE", SqlDbType.Decimal); obj_cmd.Parameters["@PAYPF_COM_RATE"].Value = model.paypf_com_rate;
                obj_cmd.Parameters.Add("@PAYPF_COM_AMOUNT", SqlDbType.Decimal); obj_cmd.Parameters["@PAYPF_COM_AMOUNT"].Value = model.paypf_com_amount;

                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = DateTime.Now;

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                obj_cmd.Parameters.Add("@PROVIDENT_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@PROVIDENT_CODE"].Value = model.provident_code;
                obj_cmd.Parameters.Add("@PAYPF_DATE", SqlDbType.Date); obj_cmd.Parameters["@PAYPF_DATE"].Value = model.paypf_date;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Paytran.update)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
