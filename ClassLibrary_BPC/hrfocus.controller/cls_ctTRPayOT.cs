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
    public class cls_ctTRPayOT
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTRPayOT() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TRPayOT> getData(string language, string condition)
        {
            List<cls_TRPayOT> list_model = new List<cls_TRPayOT>();
            cls_TRPayOT model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("COMPANY_CODE");
                obj_str.Append(", WORKER_CODE");
                obj_str.Append(", PAYOT_DATE");
                obj_str.Append(", PAYOT_OT1_MIN");
                obj_str.Append(", PAYOT_OT15_MIN");
                obj_str.Append(", PAYOT_OT2_MIN");
                obj_str.Append(", PAYOT_OT3_MIN");

                obj_str.Append(", PAYOT_OT1_AMOUNT");
                obj_str.Append(", PAYOT_OT15_AMOUNT");
                obj_str.Append(", PAYOT_OT2_AMOUNT");
                obj_str.Append(", PAYOT_OT3_AMOUNT");

                obj_str.Append(", ISNULL(MODIFIED_BY, CREATED_BY) AS MODIFIED_BY");
                obj_str.Append(", ISNULL(MODIFIED_DATE, CREATED_DATE) AS MODIFIED_DATE");

                obj_str.Append(" FROM HRM_TR_PAYOT");

                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY COMPANY_CODE, WORKER_CODE, PAYOT_DATE DESC");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TRPayOT();

                    model.company_code = Convert.ToString(dr["COMPANY_CODE"]);
                    model.worker_code = Convert.ToString(dr["WORKER_CODE"]);
                    model.payot_date = Convert.ToDateTime(dr["PAYOT_DATE"]);
                    model.payot_ot1_min = Convert.ToInt32(dr["PAYOT_OT1_MIN"]);
                    model.payot_ot15_min = Convert.ToInt32(dr["PAYOT_OT15_MIN"]);
                    model.payot_ot2_min = Convert.ToInt32(dr["PAYOT_OT2_MIN"]);
                    model.payot_ot3_min = Convert.ToInt32(dr["PAYOT_OT3_MIN"]);

                    model.payot_ot1_amount = Convert.ToDouble(dr["PAYOT_OT1_AMOUNT"]);
                    model.payot_ot15_amount = Convert.ToDouble(dr["PAYOT_OT15_AMOUNT"]);
                    model.payot_ot2_amount = Convert.ToDouble(dr["PAYOT_OT2_AMOUNT"]);
                    model.payot_ot3_amount = Convert.ToDouble(dr["PAYOT_OT3_AMOUNT"]);


                    model.modified_by = dr["MODIFIED_BY"].ToString();
                    model.modified_date = Convert.ToDateTime(dr["MODIFIED_DATE"]);

                    list_model.Add(model);
                }

            }
            catch (Exception ex)
            {
                Message = "ERROR::(PayOT.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TRPayOT> getDataByFillter(string language, string com, string emp, DateTime date)
        {
            string strCondition = "";

            strCondition += " AND COMPANY_CODE='" + com + "'";
            strCondition += " AND PAYOT_DATE='" + date.ToString("MM/dd/yyyy") + "'";

            if (!emp.Equals(""))
                strCondition += " AND WORKER_CODE='" + emp + "'";

            return this.getData(language, strCondition);
        }

        public List<cls_TRPayOT> getDataitemMultipleEmp(string language, string com, string worker, DateTime date)
        {
            string strCondition = "";

            strCondition += " AND COMPANY_CODE='" + com + "'";
            strCondition += " AND PAYOT_DATE='" + date.ToString("MM/dd/yyyy") + "'";

            strCondition += " AND WORKER_CODE IN (" + worker + ") ";

            return this.getData(language, strCondition);
        }

        public bool checkDataOld(string com, string emp, DateTime date)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT PAYOT_DATE");
                obj_str.Append(" FROM HRM_TR_PAYOT");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "' ");
                obj_str.Append(" AND WORKER_CODE='" + emp + "' ");
                obj_str.Append(" AND PAYOT_DATE='" + date.ToString("MM/dd/yyyy") + "' ");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(PayOT.checkDataOld)" + ex.ToString();
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

                obj_str.Append(" DELETE FROM HRM_TR_PAYOT");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "' ");
                obj_str.Append(" AND WORKER_CODE='" + emp + "' ");
                obj_str.Append(" AND PAYOT_DATE='" + date.ToString("MM/dd/yyyy") + "' ");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());


            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(PayOT.delete)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(cls_TRPayOT model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.company_code, model.worker_code,model.payot_date))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_PAYOT");
                obj_str.Append(" (");
                obj_str.Append("COMPANY_CODE ");
                obj_str.Append(", WORKER_CODE ");
                obj_str.Append(", PAYOT_DATE ");
                obj_str.Append(", PAYOT_OT1_MIN ");
                obj_str.Append(", PAYOT_OT15_MIN ");
                obj_str.Append(", PAYOT_OT2_MIN ");
                obj_str.Append(", PAYOT_OT3_MIN ");
                obj_str.Append(", PAYOT_OT1_AMOUNT ");
                obj_str.Append(", PAYOT_OT15_AMOUNT ");
                obj_str.Append(", PAYOT_OT2_AMOUNT ");
                obj_str.Append(", PAYOT_OT3_AMOUNT ");

                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@COMPANY_CODE ");
                obj_str.Append(", @WORKER_CODE ");
                obj_str.Append(", @PAYOT_DATE ");
                obj_str.Append(", @PAYOT_OT1_MIN ");
                obj_str.Append(", @PAYOT_OT15_MIN ");
                obj_str.Append(", @PAYOT_OT2_MIN ");
                obj_str.Append(", @PAYOT_OT3_MIN ");
                obj_str.Append(", @PAYOT_OT1_AMOUNT ");
                obj_str.Append(", @PAYOT_OT15_AMOUNT ");
                obj_str.Append(", @PAYOT_OT2_AMOUNT ");
                obj_str.Append(", @PAYOT_OT3_AMOUNT ");
                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                obj_cmd.Parameters.Add("@PAYOT_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@PAYOT_DATE"].Value = model.payot_date;
                obj_cmd.Parameters.Add("@PAYOT_OT1_MIN", SqlDbType.Int); obj_cmd.Parameters["@PAYOT_OT1_MIN"].Value = model.payot_ot1_min;
                obj_cmd.Parameters.Add("@PAYOT_OT15_MIN", SqlDbType.Int); obj_cmd.Parameters["@PAYOT_OT15_MIN"].Value = model.payot_ot15_min;
                obj_cmd.Parameters.Add("@PAYOT_OT2_MIN", SqlDbType.Int); obj_cmd.Parameters["@PAYOT_OT2_MIN"].Value = model.payot_ot2_min;
                obj_cmd.Parameters.Add("@PAYOT_OT3_MIN", SqlDbType.Int); obj_cmd.Parameters["@PAYOT_OT3_MIN"].Value = model.payot_ot3_min;

                obj_cmd.Parameters.Add("@PAYOT_OT1_AMOUNT", SqlDbType.Decimal); obj_cmd.Parameters["@PAYOT_OT1_AMOUNT"].Value = model.payot_ot1_amount;
                obj_cmd.Parameters.Add("@PAYOT_OT15_AMOUNT", SqlDbType.Decimal); obj_cmd.Parameters["@PAYOT_OT15_AMOUNT"].Value = model.payot_ot15_amount;
                obj_cmd.Parameters.Add("@PAYOT_OT2_AMOUNT", SqlDbType.Decimal); obj_cmd.Parameters["@PAYOT_OT2_AMOUNT"].Value = model.payot_ot2_amount;
                obj_cmd.Parameters.Add("@PAYOT_OT3_AMOUNT", SqlDbType.Decimal); obj_cmd.Parameters["@PAYOT_OT3_AMOUNT"].Value = model.payot_ot3_amount;

                obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@CREATED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(PayOT.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_TRPayOT model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_TR_PAYOT SET ");

                obj_str.Append("PAYOT_OT1_MIN=@PAYOT_OT1_MIN ");
                obj_str.Append(",PAYOT_OT15_MIN=@PAYOT_OT15_MIN ");
                obj_str.Append(",PAYOT_OT2_MIN=@PAYOT_OT2_MIN ");
                obj_str.Append(",PAYOT_OT3_MIN=@PAYOT_OT3_MIN ");

                obj_str.Append(", PAYOT_OT1_AMOUNT=@PAYOT_OT1_AMOUNT ");
                obj_str.Append(", PAYOT_OT15_AMOUNT=@PAYOT_OT15_AMOUNT ");
                obj_str.Append(", PAYOT_OT2_AMOUNT=@PAYOT_OT2_AMOUNT ");
                obj_str.Append(", PAYOT_OT3_AMOUNT=@PAYOT_OT3_AMOUNT ");


                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");

                obj_str.Append(" WHERE COMPANY_CODE=@COMPANY_CODE ");
                obj_str.Append(" AND WORKER_CODE=@WORKER_CODE ");
                obj_str.Append(" AND PAYOT_DATE=@PAYOT_DATE ");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@PAYOT_OT1_MIN", SqlDbType.Int); obj_cmd.Parameters["@PAYOT_OT1_MIN"].Value = model.payot_ot1_min;
                obj_cmd.Parameters.Add("@PAYOT_OT15_MIN", SqlDbType.Int); obj_cmd.Parameters["@PAYOT_OT15_MIN"].Value = model.payot_ot15_min;
                obj_cmd.Parameters.Add("@PAYOT_OT2_MIN", SqlDbType.Int); obj_cmd.Parameters["@PAYOT_OT2_MIN"].Value = model.payot_ot2_min;
                obj_cmd.Parameters.Add("@PAYOT_OT3_MIN", SqlDbType.Int); obj_cmd.Parameters["@PAYOT_OT3_MIN"].Value = model.payot_ot3_min;

                obj_cmd.Parameters.Add("@PAYOT_OT1_AMOUNT", SqlDbType.Decimal); obj_cmd.Parameters["@PAYOT_OT1_AMOUNT"].Value = model.payot_ot1_amount;
                obj_cmd.Parameters.Add("@PAYOT_OT15_AMOUNT", SqlDbType.Decimal); obj_cmd.Parameters["@PAYOT_OT15_AMOUNT"].Value = model.payot_ot15_amount;
                obj_cmd.Parameters.Add("@PAYOT_OT2_AMOUNT", SqlDbType.Decimal); obj_cmd.Parameters["@PAYOT_OT2_AMOUNT"].Value = model.payot_ot2_amount;
                obj_cmd.Parameters.Add("@PAYOT_OT3_AMOUNT", SqlDbType.Decimal); obj_cmd.Parameters["@PAYOT_OT3_AMOUNT"].Value = model.payot_ot3_amount;

                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = DateTime.Now;

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                obj_cmd.Parameters.Add("@PAYOT_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@PAYOT_DATE"].Value = model.payot_date;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(PayOT.update)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
