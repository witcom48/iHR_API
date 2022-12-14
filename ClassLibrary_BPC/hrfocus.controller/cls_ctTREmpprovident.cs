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
    public class cls_ctTREmpprovident
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTREmpprovident() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TREmpprovident> getData(string condition)
        {
            List<cls_TREmpprovident> list_model = new List<cls_TREmpprovident>();
            cls_TREmpprovident model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("COMPANY_CODE");
                obj_str.Append(", WORKER_CODE");
                obj_str.Append(", PROVIDENT_CODE");
                obj_str.Append(", EMPPROVIDENT_CARD");
                obj_str.Append(", EMPPROVIDENT_ENTRY");
                obj_str.Append(", EMPPROVIDENT_START");
                obj_str.Append(", ISNULL(EMPPROVIDENT_END, '01/01/2999') AS EMPPROVIDENT_END");

                obj_str.Append(", ISNULL(MODIFIED_BY, CREATED_BY) AS MODIFIED_BY");
                obj_str.Append(", ISNULL(MODIFIED_DATE, CREATED_DATE) AS MODIFIED_DATE");

                obj_str.Append(" FROM HRM_TR_EMPPROVIDENT");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY COMPANY_CODE, WORKER_CODE, PROVIDENT_CODE");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TREmpprovident();

                    model.company_code = Convert.ToString(dr["COMPANY_CODE"]);

                    model.worker_code = Convert.ToString(dr["WORKER_CODE"]);
                    model.provident_code = Convert.ToString(dr["PROVIDENT_CODE"]);
                    model.empprovident_card = Convert.ToString(dr["EMPPROVIDENT_CARD"]);
                    model.empprovident_entry = Convert.ToDateTime(dr["EMPPROVIDENT_ENTRY"]);
                    model.empprovident_start = Convert.ToDateTime(dr["EMPPROVIDENT_START"]);
                    model.empprovident_end = Convert.ToDateTime(dr["EMPPROVIDENT_END"]);

                    model.modified_by = dr["MODIFIED_BY"].ToString();
                    model.modified_date = Convert.ToDateTime(dr["MODIFIED_DATE"]);

                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Empprovident.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TREmpprovident> getDataByFillter(string com, string emp, string provident)
        {
            string strCondition = "";

            strCondition += " AND COMPANY_CODE='" + com + "'";

            if (!emp.Equals(""))
                strCondition += " AND WORKER_CODE='" + emp + "'";

            if (!provident.Equals(""))
                strCondition += " AND PROVIDENT_CODE='" + provident + "'";

            return this.getData(strCondition);
        }

        public bool checkDataOld(string com, string emp, string provident)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT WORKER_CODE");
                obj_str.Append(" FROM HRM_TR_EMPPROVIDENT");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "' ");
                obj_str.Append(" AND WORKER_CODE='" + emp + "' ");
                obj_str.Append(" AND PROVIDENT_CODE='" + provident + "' ");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empprovident.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }
                
        public bool delete(string com, string emp, string provident)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_TR_EMPPROVIDENT");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "' ");
                obj_str.Append(" AND WORKER_CODE='" + emp + "' ");
                obj_str.Append(" AND PROVIDENT_CODE='" + provident + "' ");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());
                

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Empprovident.delete)" + ex.ToString();
            }

            return blnResult;
        }

        public bool delete(string com, string emp)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_TR_EMPPROVIDENT");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "' ");
                obj_str.Append(" AND WORKER_CODE='" + emp + "' ");
                
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());
            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Empprovident.delete)" + ex.ToString();
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

                obj_str.Append(" DELETE FROM HRM_TR_EMPPROVIDENT");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND WORKER_CODE='" + emp + "'");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Empprovident.clear)" + ex.ToString();
            }

            return blnResult;
        }
        public bool insert(cls_TREmpprovident model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.company_code, model.worker_code, model.provident_code))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_EMPPROVIDENT");
                obj_str.Append(" (");
                obj_str.Append("COMPANY_CODE ");
                obj_str.Append(", WORKER_CODE ");
                obj_str.Append(", PROVIDENT_CODE ");
                obj_str.Append(", EMPPROVIDENT_CARD ");
                obj_str.Append(", EMPPROVIDENT_ENTRY ");
                obj_str.Append(", EMPPROVIDENT_START ");                
                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@COMPANY_CODE ");
                obj_str.Append(", @WORKER_CODE ");
                obj_str.Append(", @PROVIDENT_CODE ");
                obj_str.Append(", @EMPPROVIDENT_CARD ");
                obj_str.Append(", @EMPPROVIDENT_ENTRY ");
                obj_str.Append(", @EMPPROVIDENT_START ");
                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                obj_cmd.Parameters.Add("@EMPPROVIDENT_CARD", SqlDbType.VarChar); obj_cmd.Parameters["@EMPPROVIDENT_CARD"].Value = model.empprovident_card;
                obj_cmd.Parameters.Add("@PROVIDENT_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@PROVIDENT_CODE"].Value = model.provident_code;
                obj_cmd.Parameters.Add("@EMPPROVIDENT_ENTRY", SqlDbType.Date); obj_cmd.Parameters["@EMPPROVIDENT_ENTRY"].Value = model.empprovident_entry;
                obj_cmd.Parameters.Add("@EMPPROVIDENT_START", SqlDbType.Date); obj_cmd.Parameters["@EMPPROVIDENT_START"].Value = model.empprovident_start;
               
                obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@CREATED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {              
                Message = "ERROR::(Empprovident.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_TREmpprovident model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_TR_EMPPROVIDENT SET ");
                obj_str.Append("EMPPROVIDENT_CARD=@EMPPROVIDENT_CARD ");
                obj_str.Append(", EMPPROVIDENT_ENTRY=@EMPPROVIDENT_ENTRY ");
                obj_str.Append(", EMPPROVIDENT_START=@EMPPROVIDENT_START ");
                obj_str.Append(", EMPPROVIDENT_END=@EMPPROVIDENT_END ");


                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");

                obj_str.Append(" WHERE COMPANY_CODE=@COMPANY_CODE ");
                obj_str.Append(" AND WORKER_CODE=@WORKER_CODE ");
                obj_str.Append(" AND PROVIDENT_CODE=@PROVIDENT_CODE ");


                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@EMPPROVIDENT_CARD", SqlDbType.VarChar); obj_cmd.Parameters["@EMPPROVIDENT_CARD"].Value = model.empprovident_card;
                obj_cmd.Parameters.Add("@EMPPROVIDENT_ENTRY", SqlDbType.Date); obj_cmd.Parameters["@EMPPROVIDENT_ENTRY"].Value = model.empprovident_entry;
                obj_cmd.Parameters.Add("@EMPPROVIDENT_START", SqlDbType.Date); obj_cmd.Parameters["@EMPPROVIDENT_START"].Value = model.empprovident_start;

                obj_cmd.Parameters.Add("@EMPPROVIDENT_END", SqlDbType.Date); obj_cmd.Parameters["@EMPPROVIDENT_END"].Value = model.empprovident_end;


                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = DateTime.Now;

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                obj_cmd.Parameters.Add("@PROVIDENT_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@PROVIDENT_CODE"].Value = model.provident_code;
                
                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empprovident.update)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
