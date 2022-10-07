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
    public class cls_ctTREmplocation
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTREmplocation() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TREmplocation> getData(string condition)
        {
            List<cls_TREmplocation> list_model = new List<cls_TREmplocation>();
            cls_TREmplocation model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append(" HRM_TR_EMPLOCATION.COMPANY_CODE AS COMPANY_CODE");
                obj_str.Append(", HRM_TR_EMPLOCATION.WORKER_CODE AS WORKER_CODE");
                obj_str.Append(", HRM_TR_EMPLOCATION.LOCATION_CODE AS LOCATION_CODE");

                obj_str.Append(", EMPLOCATION_STARTDATE");
                obj_str.Append(", EMPLOCATION_ENDDATE");
                
                obj_str.Append(", ISNULL(EMPLOCATION_NOTE, '') AS EMPLOCATION_NOTE");

                obj_str.Append(", ISNULL(HRM_TR_EMPLOCATION.MODIFIED_BY, HRM_TR_EMPLOCATION.CREATED_BY) AS MODIFIED_BY");
                obj_str.Append(", ISNULL(HRM_TR_EMPLOCATION.MODIFIED_DATE, HRM_TR_EMPLOCATION.CREATED_DATE) AS MODIFIED_DATE");

                obj_str.Append(", ISNULL(LOCATION_NAME_TH, '') AS LOCATION_NAME_TH");
                obj_str.Append(", ISNULL(LOCATION_NAME_EN, '') AS LOCATION_NAME_EN");
                                
                obj_str.Append(", WORKER_INITIAL");
                obj_str.Append(", ISNULL(WORKER_FNAME_TH, '') AS WORKER_FNAME_TH");
                obj_str.Append(", ISNULL(WORKER_LNAME_TH, '') AS WORKER_LNAME_TH");
                obj_str.Append(", ISNULL(WORKER_FNAME_EN, '') AS WORKER_FNAME_EN");
                obj_str.Append(", ISNULL(WORKER_LNAME_EN, '') AS WORKER_LNAME_EN");

                obj_str.Append(" FROM HRM_TR_EMPLOCATION");

                obj_str.Append(" INNER JOIN HRM_MT_WORKER ON HRM_TR_EMPLOCATION.COMPANY_CODE=HRM_MT_WORKER.COMPANY_CODE AND HRM_TR_EMPLOCATION.WORKER_CODE=HRM_MT_WORKER.WORKER_CODE");
                obj_str.Append(" INNER JOIN HRM_MT_LOCATION ON HRM_TR_EMPLOCATION.LOCATION_CODE=HRM_MT_LOCATION.LOCATION_CODE");

                
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY HRM_TR_EMPLOCATION.COMPANY_CODE, HRM_TR_EMPLOCATION.WORKER_CODE, EMPLOCATION_STARTDATE DESC");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TREmplocation();

                    model.company_code = dr["COMPANY_CODE"].ToString();
                    model.worker_code = dr["WORKER_CODE"].ToString();

                    model.location_code = dr["LOCATION_CODE"].ToString();
                    model.emplocation_startdate = Convert.ToDateTime(dr["EMPLOCATION_STARTDATE"]);
                    model.emplocation_enddate = Convert.ToDateTime(dr["EMPLOCATION_ENDDATE"]);

                    model.emplocation_note = dr["EMPLOCATION_NOTE"].ToString();

                    model.modified_by = dr["MODIFIED_BY"].ToString();
                    model.modified_date = Convert.ToDateTime(dr["MODIFIED_DATE"]);

                    model.location_name_th = dr["LOCATION_NAME_TH"].ToString();
                    model.location_name_en = dr["LOCATION_NAME_EN"].ToString();

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
                Message = "ERROR::(Emplocation.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TREmplocation> getDataByFillter(string com, string worker, string location)
        {
            string strCondition = " AND HRM_TR_EMPLOCATION.COMPANY_CODE='" + com + "'";

            if (!worker.Equals(""))
                strCondition += " AND HRM_TR_EMPLOCATION.WORKER_CODE='" + worker + "'";

            if (!location.Equals(""))
                strCondition += " AND HRM_TR_EMPLOCATION.LOCATION_CODE='" + location + "'";
                                    
            return this.getData(strCondition);
        }

        public List<cls_TREmplocation> getDataByCreateDate(string com, DateTime create)
        {
            string strCondition = " AND HRM_TR_EMPLOCATION.COMPANY_CODE='" + com + "'";

            strCondition = " AND (HRM_TR_EMPLOCATION.CREATED_DATE BETWEEN CONVERT(datetime,'" + create.ToString(Config.FormatDateSQL) + "') AND CONVERT(datetime, '" + create.ToString(Config.FormatDateSQL) + " 23:59:59:998') )";

            return this.getData(strCondition);
        }

        public bool checkDataOld(string com, string worker, DateTime startdate)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT COMPANY_CODE");
                obj_str.Append(" FROM HRM_TR_EMPLOCATION");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND WORKER_CODE='" + worker + "'");
                obj_str.Append(" AND EMPLOCATION_STARTDATE='" + startdate.ToString(Config.FormatDateSQL) + "'");
                                                
                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Emplocation.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public bool delete(string com, string worker, DateTime startdate)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_TR_EMPLOCATION");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND WORKER_CODE='" + worker + "'");
                obj_str.Append(" AND EMPLOCATION_STARTDATE='" + startdate.ToString(Config.FormatDateSQL) + "'");
                                              
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Emplocation.delete)" + ex.ToString();
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

                obj_str.Append(" DELETE FROM HRM_TR_EMPLOCATION");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND WORKER_CODE='" + emp + "'");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Emplocation.clear)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(cls_TREmplocation model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.company_code, model.worker_code, model.emplocation_startdate))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_EMPLOCATION");
                obj_str.Append(" (");                
                obj_str.Append(" COMPANY_CODE ");
                obj_str.Append(", WORKER_CODE ");
                obj_str.Append(", LOCATION_CODE ");
                obj_str.Append(", EMPLOCATION_STARTDATE ");
                obj_str.Append(", EMPLOCATION_ENDDATE ");
                obj_str.Append(", EMPLOCATION_NOTE ");
                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                
                obj_str.Append(" @COMPANY_CODE ");
                obj_str.Append(", @WORKER_CODE ");
                obj_str.Append(", @LOCATION_CODE ");
                obj_str.Append(", @EMPLOCATION_STARTDATE ");
                obj_str.Append(", @EMPLOCATION_ENDDATE ");
                obj_str.Append(", @EMPLOCATION_NOTE ");
                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                obj_cmd.Parameters.Add("@LOCATION_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@LOCATION_CODE"].Value = model.location_code;
                obj_cmd.Parameters.Add("@EMPLOCATION_STARTDATE", SqlDbType.Date); obj_cmd.Parameters["@EMPLOCATION_STARTDATE"].Value = model.emplocation_startdate;
                obj_cmd.Parameters.Add("@EMPLOCATION_ENDDATE", SqlDbType.Date); obj_cmd.Parameters["@EMPLOCATION_ENDDATE"].Value = model.emplocation_enddate;
                obj_cmd.Parameters.Add("@EMPLOCATION_NOTE", SqlDbType.VarChar); obj_cmd.Parameters["@EMPLOCATION_NOTE"].Value = model.emplocation_note;
                
                obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@CREATED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;
     
                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();
                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Emplocation.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_TREmplocation model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_TR_EMPLOCATION SET ");

                obj_str.Append(" EMPLOCATION_ENDDATE=@EMPLOCATION_ENDDATE ");
                obj_str.Append(", EMPLOCATION_NOTE=@EMPLOCATION_NOTE ");

                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");
                obj_str.Append(", FLAG=@FLAG ");

                obj_str.Append(" WHERE COMPANY_CODE=@COMPANY_CODE ");
                obj_str.Append(" AND WORKER_CODE=@WORKER_CODE ");
                obj_str.Append(" AND LOCATION_CODE=@LOCATION_CODE ");
                obj_str.Append(" AND EMPLOCATION_STARTDATE=@EMPLOCATION_STARTDATE ");
               
                
                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@EMPLOCATION_ENDDATE", SqlDbType.Date); obj_cmd.Parameters["@EMPLOCATION_ENDDATE"].Value = model.emplocation_enddate;
                obj_cmd.Parameters.Add("@EMPLOCATION_NOTE", SqlDbType.VarChar); obj_cmd.Parameters["@EMPLOCATION_NOTE"].Value = model.emplocation_note;
                

                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                obj_cmd.Parameters.Add("@LOCATION_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@LOCATION_CODE"].Value = model.location_code;
                obj_cmd.Parameters.Add("@EMPLOCATION_STARTDATE", SqlDbType.Date); obj_cmd.Parameters["@EMPLOCATION_STARTDATE"].Value = model.emplocation_startdate;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Emplocation.update)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(DateTime dateStart, List<cls_TREmplocation> list_model)
        {
            bool blnResult = false;
            cls_ctConnection obj_conn = new cls_ctConnection();

            try
            {

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_EMPLOCATION");
                obj_str.Append(" (");                
                obj_str.Append("COMPANY_CODE ");
                obj_str.Append(", WORKER_CODE ");
                obj_str.Append(", LOCATION_CODE ");
                obj_str.Append(", EMPLOCATION_STARTDATE ");
                obj_str.Append(", EMPLOCATION_ENDDATE ");
                obj_str.Append(", EMPLOCATION_NOTE ");
                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");                
                obj_str.Append("@COMPANY_CODE ");
                obj_str.Append(", @WORKER_CODE ");
                obj_str.Append(", @LOCATION_CODE ");
                obj_str.Append(", @EMPLOCATION_STARTDATE ");
                obj_str.Append(", @EMPLOCATION_ENDDATE ");
                obj_str.Append(", @EMPLOCATION_NOTE ");
                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                obj_conn.doOpenTransaction();

                //-- Step 1 delete data old
                string strWorkerID = "";
                foreach (cls_TREmplocation model in list_model)
                {
                    strWorkerID += "'" + model.worker_code + "',";
                }
                if (strWorkerID.Length > 0)
                    strWorkerID = strWorkerID.Substring(0, strWorkerID.Length - 1);

                System.Text.StringBuilder obj_str2 = new System.Text.StringBuilder();

                obj_str2.Append(" DELETE FROM HRM_TR_EMPLOCATION");               
                obj_str2.Append(" WHERE COMPANY_CODE='" + list_model[0].company_code + "'");
                obj_str2.Append(" AND WORKER_CODE IN (" + strWorkerID + ")");
                obj_str2.Append(" AND EMPLOCATION_STARTDATE='" + dateStart.ToString(Config.FormatDateSQL) + "'");

                blnResult = obj_conn.doExecuteSQL_transaction(obj_str2.ToString());

                if (blnResult)
                {

                    SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
                    obj_cmd.Transaction = obj_conn.getTransaction();

                    obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@LOCATION_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@EMPLOCATION_STARTDATE", SqlDbType.Date);
                    obj_cmd.Parameters.Add("@EMPLOCATION_ENDDATE", SqlDbType.Date);
                    obj_cmd.Parameters.Add("@EMPLOCATION_NOTE", SqlDbType.VarChar);

                    obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime);
                    obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit);

                    foreach (cls_TREmplocation model in list_model)
                    {

                        obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                        obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                        obj_cmd.Parameters["@LOCATION_CODE"].Value = model.location_code;
                        obj_cmd.Parameters["@EMPLOCATION_STARTDATE"].Value = model.emplocation_startdate;
                        obj_cmd.Parameters["@EMPLOCATION_ENDDATE"].Value = model.emplocation_enddate;
                        obj_cmd.Parameters["@EMPLOCATION_NOTE"].Value = model.emplocation_note;

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
