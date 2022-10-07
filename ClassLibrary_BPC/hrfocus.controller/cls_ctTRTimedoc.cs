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
    public class cls_ctTRTimedoc
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTRTimedoc() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TRTimedoc> getData(string condition)
        {
            List<cls_TRTimedoc> list_model = new List<cls_TRTimedoc>();
            cls_TRTimedoc model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("COMPANY_CODE");
                obj_str.Append(", WORKER_CODE");
                obj_str.Append(", TIMEDOC_WORKDATE");
                obj_str.Append(", TIMEDOC_DOCTYPE");

                obj_str.Append(", ISNULL(TIMEDOC_DOCNO, '') AS TIMEDOC_DOCNO");
                obj_str.Append(", ISNULL(TIMEDOC_VALUE1, '') AS TIMEDOC_VALUE1");
                obj_str.Append(", ISNULL(TIMEDOC_VALUE2, '') AS TIMEDOC_VALUE2");
                obj_str.Append(", ISNULL(TIMEDOC_VALUE3, '') AS TIMEDOC_VALUE3");
                obj_str.Append(", ISNULL(TIMEDOC_VALUE4, '') AS TIMEDOC_VALUE4");

                obj_str.Append(", ISNULL(TIMEDOC_REASONCODE, '') AS TIMEDOC_REASONCODE");
                obj_str.Append(", ISNULL(TIMEDOC_NOTE, '') AS TIMEDOC_NOTE");

                obj_str.Append(", ISNULL(MODIFIED_BY, CREATED_BY) AS MODIFIED_BY");
                obj_str.Append(", ISNULL(MODIFIED_DATE, CREATED_DATE) AS MODIFIED_DATE");

                obj_str.Append(" FROM HRM_TR_TIMEDOC");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY COMPANY_CODE, WORKER_CODE, TIMEDOC_WORKDATE, TIMEDOC_DOCTYPE");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TRTimedoc();

                    model.company_code = dr["COMPANY_CODE"].ToString();
                    model.worker_code = dr["WORKER_CODE"].ToString();

                    model.timedoc_workdate = Convert.ToDateTime(dr["TIMEDOC_WORKDATE"]);
                    model.timedoc_doctype = dr["TIMEDOC_DOCTYPE"].ToString();
                    model.timedoc_docno = dr["TIMEDOC_DOCNO"].ToString();

                    model.timedoc_value1 = dr["TIMEDOC_VALUE1"].ToString();
                    model.timedoc_value2 = dr["TIMEDOC_VALUE2"].ToString();
                    model.timedoc_value3 = dr["TIMEDOC_VALUE3"].ToString();
                    model.timedoc_value4 = dr["TIMEDOC_VALUE4"].ToString();

                    model.timedoc_reasoncode = dr["TIMEDOC_REASONCODE"].ToString();
                    model.timedoc_note = dr["TIMEDOC_NOTE"].ToString();


                    model.modified_by = dr["MODIFIED_BY"].ToString();
                    model.modified_date = Convert.ToDateTime(dr["MODIFIED_DATE"]);
                                                                                                                      
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Timedoc.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TRTimedoc> getDataByFillter(string com, string worker, DateTime workdate, string type)
        {
            string strCondition = " AND COMPANY_CODE='" + com + "'";
            strCondition += " AND TIMEDOC_WORKDATE='" + workdate.ToString("MM/dd/yyyy") + "'";

            if (!worker.Equals(""))
                strCondition += " AND WORKER_CODE='" + worker + "'";

            if (!type.Equals(""))
                strCondition += " AND TIMEDOC_DOCTYPE='" + type + "'";


            return this.getData(strCondition);
        }

        public bool delete(string com, string emp, DateTime workdate, string type)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_TR_TIMEDOC");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND WORKER_CODE='" + emp + "'");
                obj_str.Append(" AND TIMEDOC_WORKDATE='" + workdate.ToString("MM/dd/yyyy") + "'");
                obj_str.Append(" AND TIMEDOC_DOCTYPE='" + type + "'");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Timedoc.delete)" + ex.ToString();
            }

            return blnResult;
        }

        public bool checkDataOld(string com, string emp, DateTime workdate, string type)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT HRM_TR_TIMEDOC");            
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND WORKER_CODE='" + emp + "'");
                obj_str.Append(" AND TIMEDOC_WORKDATE='" + workdate.ToString("MM/dd/yyyy") + "'");
                obj_str.Append(" AND TIMEDOC_DOCTYPE='" + type + "'");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Timedoc.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(cls_TRTimedoc model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.company_code, model.worker_code, model.timedoc_workdate, model.timedoc_doctype))
                {
                    return this.update(model);
                }
                
                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_TIMEDOC");
                obj_str.Append(" (");
                obj_str.Append("COMPANY_CODE ");
                obj_str.Append(", WORKER_CODE ");
                obj_str.Append(", TIMEDOC_WORKDATE ");
                obj_str.Append(", TIMEDOC_DOCTYPE ");

                obj_str.Append(", TIMEDOC_DOCNO ");
                obj_str.Append(", TIMEDOC_VALUE1 ");
                obj_str.Append(", TIMEDOC_VALUE2 ");
                obj_str.Append(", TIMEDOC_VALUE3 ");
                obj_str.Append(", TIMEDOC_VALUE4 ");

                obj_str.Append(", TIMEDOC_REASONCODE ");
                obj_str.Append(", TIMEDOC_NOTE ");    

                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@COMPANY_CODE ");
                obj_str.Append(", @WORKER_CODE ");
                obj_str.Append(", @TIMEDOC_WORKDATE ");
                obj_str.Append(", @TIMEDOC_DOCTYPE ");

                obj_str.Append(", @TIMEDOC_DOCNO ");
                obj_str.Append(", @TIMEDOC_VALUE1 ");
                obj_str.Append(", @TIMEDOC_VALUE2 ");
                obj_str.Append(", @TIMEDOC_VALUE3 ");
                obj_str.Append(", @TIMEDOC_VALUE4 ");

                obj_str.Append(", @TIMEDOC_REASONCODE ");
                obj_str.Append(", @TIMEDOC_NOTE ");

                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");


                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.Int); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.DateTime); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                obj_cmd.Parameters.Add("@TIMEDOC_WORKDATE", SqlDbType.DateTime); obj_cmd.Parameters["@TIMEDOC_WORKDATE"].Value = model.timedoc_workdate;
                obj_cmd.Parameters.Add("@TIMEDOC_DOCTYPE", SqlDbType.DateTime); obj_cmd.Parameters["@TIMEDOC_DOCTYPE"].Value = model.timedoc_doctype;

                obj_cmd.Parameters.Add("@TIMEDOC_DOCNO", SqlDbType.DateTime); obj_cmd.Parameters["@TIMEDOC_DOCNO"].Value = model.timedoc_docno;
                obj_cmd.Parameters.Add("@TIMEDOC_VALUE1", SqlDbType.DateTime); obj_cmd.Parameters["@TIMEDOC_VALUE1"].Value = model.timedoc_value1;
                obj_cmd.Parameters.Add("@TIMEDOC_VALUE2", SqlDbType.DateTime); obj_cmd.Parameters["@TIMEDOC_VALUE2"].Value = model.timedoc_value2;
                obj_cmd.Parameters.Add("@TIMEDOC_VALUE3", SqlDbType.DateTime); obj_cmd.Parameters["@TIMEDOC_VALUE3"].Value = model.timedoc_value3;
                obj_cmd.Parameters.Add("@TIMEDOC_VALUE4", SqlDbType.DateTime); obj_cmd.Parameters["@TIMEDOC_VALUE4"].Value = model.timedoc_value4;

                obj_cmd.Parameters.Add("@TIMEDOC_REASONCODE", SqlDbType.DateTime); obj_cmd.Parameters["@TIMEDOC_REASONCODE"].Value = model.timedoc_reasoncode;
                obj_cmd.Parameters.Add("@TIMEDOC_NOTE", SqlDbType.DateTime); obj_cmd.Parameters["@TIMEDOC_NOTE"].Value = model.timedoc_note;
                

                obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@CREATED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();
                blnResult = true;   
                
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Timedoc.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_TRTimedoc model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_TR_TIMEDOC SET ");

                obj_str.Append(" TIMEDOC_DOCNO=@TIMEDOC_DOCNO ");
                obj_str.Append(", TIMEDOC_VALUE1=@TIMEDOC_VALUE1 ");
                obj_str.Append(", TIMEDOC_VALUE2=@TIMEDOC_VALUE2 ");
                obj_str.Append(", TIMEDOC_VALUE3=@TIMEDOC_VALUE3 ");
                obj_str.Append(", TIMEDOC_VALUE4=@TIMEDOC_VALUE4 ");

                obj_str.Append(", TIMEDOC_REASONCODE=@TIMEDOC_REASONCODE ");
                obj_str.Append(", TIMEDOC_NOTE=@TIMEDOC_NOTE ");    

                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");
                obj_str.Append(", FLAG=@FLAG ");

                obj_str.Append(" WHERE COMPANY_CODE=@COMPANY_CODE ");
                obj_str.Append(" AND WORKER_CODE=@WORKER_CODE ");
                obj_str.Append(" AND TIMEDOC_WORKDATE=@TIMEDOC_WORKDATE ");
                obj_str.Append(" AND TIMEDOC_DOCTYPE=@TIMEDOC_DOCTYPE ");
                               
                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@TIMEDOC_DOCNO", SqlDbType.DateTime); obj_cmd.Parameters["@TIMEDOC_DOCNO"].Value = model.timedoc_docno;
                obj_cmd.Parameters.Add("@TIMEDOC_VALUE1", SqlDbType.DateTime); obj_cmd.Parameters["@TIMEDOC_VALUE1"].Value = model.timedoc_value1;
                obj_cmd.Parameters.Add("@TIMEDOC_VALUE2", SqlDbType.DateTime); obj_cmd.Parameters["@TIMEDOC_VALUE2"].Value = model.timedoc_value2;
                obj_cmd.Parameters.Add("@TIMEDOC_VALUE3", SqlDbType.DateTime); obj_cmd.Parameters["@TIMEDOC_VALUE3"].Value = model.timedoc_value3;
                obj_cmd.Parameters.Add("@TIMEDOC_VALUE4", SqlDbType.DateTime); obj_cmd.Parameters["@TIMEDOC_VALUE4"].Value = model.timedoc_value4;

                obj_cmd.Parameters.Add("@TIMEDOC_REASONCODE", SqlDbType.DateTime); obj_cmd.Parameters["@TIMEDOC_REASONCODE"].Value = model.timedoc_reasoncode;
                obj_cmd.Parameters.Add("@TIMEDOC_NOTE", SqlDbType.DateTime); obj_cmd.Parameters["@TIMEDOC_NOTE"].Value = model.timedoc_note;
                
                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.Int); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.DateTime); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                obj_cmd.Parameters.Add("@TIMEDOC_WORKDATE", SqlDbType.DateTime); obj_cmd.Parameters["@TIMEDOC_WORKDATE"].Value = model.timedoc_workdate;
                obj_cmd.Parameters.Add("@TIMEDOC_DOCTYPE", SqlDbType.DateTime); obj_cmd.Parameters["@TIMEDOC_DOCTYPE"].Value = model.timedoc_doctype;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Timedoc.update)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
