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
    public class cls_ctTREmpdep
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTREmpdep() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TREmpdep> getData(string condition)
        {
            List<cls_TREmpdep> list_model = new List<cls_TREmpdep>();
            cls_TREmpdep model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("EMPDEP_ID");
                obj_str.Append(", EMPDEP_DATE");
                obj_str.Append(", ISNULL(EMPDEP_LEVEL01, '') AS EMPDEP_LEVEL01");
                obj_str.Append(", ISNULL(EMPDEP_LEVEL02, '') AS EMPDEP_LEVEL02");
                obj_str.Append(", ISNULL(EMPDEP_LEVEL03, '') AS EMPDEP_LEVEL03");
                obj_str.Append(", ISNULL(EMPDEP_LEVEL04, '') AS EMPDEP_LEVEL04");
                obj_str.Append(", ISNULL(EMPDEP_LEVEL05, '') AS EMPDEP_LEVEL05");
                obj_str.Append(", ISNULL(EMPDEP_LEVEL06, '') AS EMPDEP_LEVEL06");
                obj_str.Append(", ISNULL(EMPDEP_LEVEL07, '') AS EMPDEP_LEVEL07");
                obj_str.Append(", ISNULL(EMPDEP_LEVEL08, '') AS EMPDEP_LEVEL08");
                obj_str.Append(", ISNULL(EMPDEP_LEVEL09, '') AS EMPDEP_LEVEL09");
                obj_str.Append(", ISNULL(EMPDEP_LEVEL10, '') AS EMPDEP_LEVEL10");
                obj_str.Append(", ISNULL(EMPDEP_REASON, '') AS EMPDEP_REASON");

                obj_str.Append(", COMPANY_CODE");
                obj_str.Append(", WORKER_CODE");
                                 
                obj_str.Append(", ISNULL(MODIFIED_BY, CREATED_BY) AS MODIFIED_BY");
                obj_str.Append(", ISNULL(MODIFIED_DATE, CREATED_DATE) AS MODIFIED_DATE");

                obj_str.Append(" FROM HRM_TR_EMPDEP");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY COMPANY_CODE, WORKER_CODE, EMPDEP_DATE DESC");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TREmpdep();

                    model.empdep_id = Convert.ToInt32(dr["EMPDEP_ID"]);

                    model.empdep_date = Convert.ToDateTime(dr["EMPDEP_DATE"]);

                    model.empdep_level01 = dr["EMPDEP_LEVEL01"].ToString();
                    model.empdep_level02 = dr["EMPDEP_LEVEL02"].ToString();
                    model.empdep_level03 = dr["EMPDEP_LEVEL03"].ToString();
                    model.empdep_level04 = dr["EMPDEP_LEVEL04"].ToString();
                    model.empdep_level05 = dr["EMPDEP_LEVEL05"].ToString();
                    model.empdep_level06 = dr["EMPDEP_LEVEL06"].ToString();
                    model.empdep_level07 = dr["EMPDEP_LEVEL07"].ToString();
                    model.empdep_level08 = dr["EMPDEP_LEVEL08"].ToString();
                    model.empdep_level09 = dr["EMPDEP_LEVEL09"].ToString();
                    model.empdep_level10 = dr["EMPDEP_LEVEL10"].ToString();

                    model.empdep_reason = dr["EMPDEP_REASON"].ToString();

                    model.company_code = dr["COMPANY_CODE"].ToString();
                    model.worker_code = dr["WORKER_CODE"].ToString();
                                       
                    model.modified_by = dr["MODIFIED_BY"].ToString();
                    model.modified_date = Convert.ToDateTime(dr["MODIFIED_DATE"]);
                                                                                                                      
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(EMPDEP.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TREmpdep> getDataByFillter(string com, string worker)
        {
            string strCondition = " AND COMPANY_CODE='" + com + "'";

            if (!worker.Equals(""))
                strCondition += " AND WORKER_CODE='" + worker + "'";

                        
            return this.getData(strCondition);
        }

        public List<cls_TREmpdep> getDataTaxMultipleEmp(string com, string worker, DateTime paydate)
        {
            string strCondition = " AND COMPANY_CODE='" + com + "'";          
            strCondition += " AND WORKER_CODE IN (" + worker + ") ";
            strCondition += " AND EMPDEP_DATE <= " + paydate.ToString("MM/dd/yyyy") + " ";
            return this.getData(strCondition);
        }

        public bool checkDataOld(string com, string worker, DateTime date)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT EMPDEP_ID");
                obj_str.Append(" FROM HRM_TR_EMPDEP");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND WORKER_CODE='" + worker + "'");
                obj_str.Append(" AND EMPDEP_DATE='" + date.ToString("MM/dd/yyyy") + "'");
                                                
                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empdep.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public int getNextID()
        {
            int intResult = 1;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT MAX(EMPDEP_ID) ");
                obj_str.Append(" FROM HRM_TR_EMPDEP");             

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    intResult = Convert.ToInt32(dt.Rows[0][0]) + 1;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empdep.getNextID)" + ex.ToString();
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

                obj_str.Append(" DELETE FROM HRM_TR_EMPDEP");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND EMPDEP_ID='" + id + "'");
                                              
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Empdep.delete)" + ex.ToString();
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

                obj_str.Append(" DELETE FROM HRM_TR_EMPDEP");
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

        public bool insert(cls_TREmpdep model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.company_code, model.worker_code, model.empdep_date))
                {

                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_EMPDEP");
                obj_str.Append(" (");
                obj_str.Append("EMPDEP_ID ");
                obj_str.Append(", EMPDEP_DATE ");
                obj_str.Append(", EMPDEP_LEVEL01 ");
                obj_str.Append(", EMPDEP_LEVEL02 ");
                obj_str.Append(", EMPDEP_LEVEL03 ");
                obj_str.Append(", EMPDEP_LEVEL04 ");
                obj_str.Append(", EMPDEP_LEVEL05 ");
                obj_str.Append(", EMPDEP_LEVEL06 ");
                obj_str.Append(", EMPDEP_LEVEL07 ");
                obj_str.Append(", EMPDEP_LEVEL08 ");
                obj_str.Append(", EMPDEP_LEVEL09 ");
                obj_str.Append(", EMPDEP_LEVEL10 ");
                obj_str.Append(", EMPDEP_REASON ");
                obj_str.Append(", COMPANY_CODE ");
                obj_str.Append(", WORKER_CODE ");                                
                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@EMPDEP_ID ");
                obj_str.Append(", @EMPDEP_DATE ");
                obj_str.Append(", @EMPDEP_LEVEL01 ");
                obj_str.Append(", @EMPDEP_LEVEL02 ");
                obj_str.Append(", @EMPDEP_LEVEL03 ");
                obj_str.Append(", @EMPDEP_LEVEL04 ");
                obj_str.Append(", @EMPDEP_LEVEL05 ");
                obj_str.Append(", @EMPDEP_LEVEL06 ");
                obj_str.Append(", @EMPDEP_LEVEL07 ");
                obj_str.Append(", @EMPDEP_LEVEL08 ");
                obj_str.Append(", @EMPDEP_LEVEL09 ");
                obj_str.Append(", @EMPDEP_LEVEL10 ");
                obj_str.Append(", @EMPDEP_REASON ");
                obj_str.Append(", @COMPANY_CODE ");
                obj_str.Append(", @WORKER_CODE ");
                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@EMPDEP_ID", SqlDbType.Int); obj_cmd.Parameters["@EMPDEP_ID"].Value = this.getNextID();
                obj_cmd.Parameters.Add("@EMPDEP_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@EMPDEP_DATE"].Value = model.empdep_date;

                obj_cmd.Parameters.Add("@EMPDEP_LEVEL01", SqlDbType.VarChar); obj_cmd.Parameters["@EMPDEP_LEVEL01"].Value = model.empdep_level01;
                obj_cmd.Parameters.Add("@EMPDEP_LEVEL02", SqlDbType.VarChar); obj_cmd.Parameters["@EMPDEP_LEVEL02"].Value = model.empdep_level02;
                obj_cmd.Parameters.Add("@EMPDEP_LEVEL03", SqlDbType.VarChar); obj_cmd.Parameters["@EMPDEP_LEVEL03"].Value = model.empdep_level03;
                obj_cmd.Parameters.Add("@EMPDEP_LEVEL04", SqlDbType.VarChar); obj_cmd.Parameters["@EMPDEP_LEVEL04"].Value = model.empdep_level04;
                obj_cmd.Parameters.Add("@EMPDEP_LEVEL05", SqlDbType.VarChar); obj_cmd.Parameters["@EMPDEP_LEVEL05"].Value = model.empdep_level05;
                obj_cmd.Parameters.Add("@EMPDEP_LEVEL06", SqlDbType.VarChar); obj_cmd.Parameters["@EMPDEP_LEVEL06"].Value = model.empdep_level06;
                obj_cmd.Parameters.Add("@EMPDEP_LEVEL07", SqlDbType.VarChar); obj_cmd.Parameters["@EMPDEP_LEVEL07"].Value = model.empdep_level07;
                obj_cmd.Parameters.Add("@EMPDEP_LEVEL08", SqlDbType.VarChar); obj_cmd.Parameters["@EMPDEP_LEVEL08"].Value = model.empdep_level08;
                obj_cmd.Parameters.Add("@EMPDEP_LEVEL09", SqlDbType.VarChar); obj_cmd.Parameters["@EMPDEP_LEVEL09"].Value = model.empdep_level09;
                obj_cmd.Parameters.Add("@EMPDEP_LEVEL10", SqlDbType.VarChar); obj_cmd.Parameters["@EMPDEP_LEVEL10"].Value = model.empdep_level10;

                obj_cmd.Parameters.Add("@EMPDEP_REASON", SqlDbType.VarChar); obj_cmd.Parameters["@EMPDEP_REASON"].Value = model.empdep_reason;
                
                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;

                obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@CREATED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;
     
                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();
                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empdep.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_TREmpdep model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_TR_EMPDEP SET ");

                obj_str.Append(" EMPDEP_DATE=@EMPDEP_DATE ");
                obj_str.Append(", EMPDEP_LEVEL01=@EMPDEP_LEVEL01 ");
                obj_str.Append(", EMPDEP_LEVEL02=@EMPDEP_LEVEL02 ");
                obj_str.Append(", EMPDEP_LEVEL03=@EMPDEP_LEVEL03 ");
                obj_str.Append(", EMPDEP_LEVEL04=@EMPDEP_LEVEL04 ");
                obj_str.Append(", EMPDEP_LEVEL05=@EMPDEP_LEVEL05 ");
                obj_str.Append(", EMPDEP_LEVEL06=@EMPDEP_LEVEL06 ");
                obj_str.Append(", EMPDEP_LEVEL07=@EMPDEP_LEVEL07 ");
                obj_str.Append(", EMPDEP_LEVEL08=@EMPDEP_LEVEL08 ");
                obj_str.Append(", EMPDEP_LEVEL09=@EMPDEP_LEVEL09 ");
                obj_str.Append(", EMPDEP_LEVEL10=@EMPDEP_LEVEL10 ");

                obj_str.Append(", EMPDEP_REASON=@EMPDEP_REASON ");

                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");
                obj_str.Append(", FLAG=@FLAG ");

                obj_str.Append(" WHERE EMPDEP_ID=@EMPDEP_ID ");
                               
                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@EMPDEP_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@EMPDEP_DATE"].Value = model.empdep_date;

                obj_cmd.Parameters.Add("@EMPDEP_LEVEL01", SqlDbType.VarChar); obj_cmd.Parameters["@EMPDEP_LEVEL01"].Value = model.empdep_level01;
                obj_cmd.Parameters.Add("@EMPDEP_LEVEL02", SqlDbType.VarChar); obj_cmd.Parameters["@EMPDEP_LEVEL02"].Value = model.empdep_level02;
                obj_cmd.Parameters.Add("@EMPDEP_LEVEL03", SqlDbType.VarChar); obj_cmd.Parameters["@EMPDEP_LEVEL03"].Value = model.empdep_level03;
                obj_cmd.Parameters.Add("@EMPDEP_LEVEL04", SqlDbType.VarChar); obj_cmd.Parameters["@EMPDEP_LEVEL04"].Value = model.empdep_level04;
                obj_cmd.Parameters.Add("@EMPDEP_LEVEL05", SqlDbType.VarChar); obj_cmd.Parameters["@EMPDEP_LEVEL05"].Value = model.empdep_level05;
                obj_cmd.Parameters.Add("@EMPDEP_LEVEL06", SqlDbType.VarChar); obj_cmd.Parameters["@EMPDEP_LEVEL06"].Value = model.empdep_level06;
                obj_cmd.Parameters.Add("@EMPDEP_LEVEL07", SqlDbType.VarChar); obj_cmd.Parameters["@EMPDEP_LEVEL07"].Value = model.empdep_level07;
                obj_cmd.Parameters.Add("@EMPDEP_LEVEL08", SqlDbType.VarChar); obj_cmd.Parameters["@EMPDEP_LEVEL08"].Value = model.empdep_level08;
                obj_cmd.Parameters.Add("@EMPDEP_LEVEL09", SqlDbType.VarChar); obj_cmd.Parameters["@EMPDEP_LEVEL09"].Value = model.empdep_level09;
                obj_cmd.Parameters.Add("@EMPDEP_LEVEL10", SqlDbType.VarChar); obj_cmd.Parameters["@EMPDEP_LEVEL10"].Value = model.empdep_level10;

                obj_cmd.Parameters.Add("@EMPDEP_REASON", SqlDbType.VarChar); obj_cmd.Parameters["@EMPDEP_REASON"].Value = model.empdep_reason;
                
                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;

                obj_cmd.Parameters.Add("@EMPDEP_ID", SqlDbType.Int); obj_cmd.Parameters["@EMPDEP_ID"].Value = model.empdep_id;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empdep.update)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
