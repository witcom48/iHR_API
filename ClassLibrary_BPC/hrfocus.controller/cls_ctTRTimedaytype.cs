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
    public class cls_ctTRTimedaytype
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTRTimedaytype() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TRTimedaytype> getData(string language, string condition)
        {
            List<cls_TRTimedaytype> list_model = new List<cls_TRTimedaytype>();
            cls_TRTimedaytype model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append(" HRM_TR_TIMEDAYTYPE.COMPANY_CODE");
                obj_str.Append(", HRM_TR_TIMEDAYTYPE.WORKER_CODE");

                obj_str.Append(", TIMEDAYTYPE_ID");
                obj_str.Append(", ISNULL(TIMEDAYTYPE_DOC, '') AS TIMEDAYTYPE_DOC");

                obj_str.Append(", TIMEDAYTYPE_WORKDATE");

                obj_str.Append(", TIMEDAYTYPE_OLD");
                obj_str.Append(", TIMEDAYTYPE_NEW");
          
                
                obj_str.Append(", ISNULL(TIMEDAYTYPE_NOTE, '') AS TIMEDAYTYPE_NOTE");

                obj_str.Append(", ISNULL(HRM_TR_TIMEDAYTYPE.REASON_CODE, '') AS REASON_CODE");

                if (language.Equals("TH"))
                {                    
                    obj_str.Append(", ISNULL(REASON_NAME_TH, '') AS REASON_DETAIL");                    
                    obj_str.Append(", INITIAL_NAME_TH + WORKER_FNAME_TH + ' ' + WORKER_LNAME_TH AS WORKER_DETAIL");
                }
                else
                {                    
                    obj_str.Append(", ISNULL(REASON_NAME_EN, '') AS REASON_DETAIL");                    
                    obj_str.Append(", INITIAL_NAME_EN + WORKER_FNAME_EN + ' ' + WORKER_LNAME_EN AS WORKER_DETAIL");
                }


                obj_str.Append(", ISNULL(HRM_TR_TIMEDAYTYPE.MODIFIED_BY, HRM_TR_TIMEDAYTYPE.CREATED_BY) AS MODIFIED_BY");
                obj_str.Append(", ISNULL(HRM_TR_TIMEDAYTYPE.MODIFIED_DATE, HRM_TR_TIMEDAYTYPE.CREATED_DATE) AS MODIFIED_DATE");

                obj_str.Append(" FROM HRM_TR_TIMEDAYTYPE");

                obj_str.Append(" INNER JOIN HRM_MT_WORKER ON HRM_MT_WORKER.COMPANY_CODE=HRM_TR_TIMEDAYTYPE.COMPANY_CODE AND HRM_MT_WORKER.WORKER_CODE=HRM_TR_TIMEDAYTYPE.WORKER_CODE");
                obj_str.Append(" INNER JOIN HRM_MT_INITIAL ON HRM_MT_INITIAL.INITIAL_CODE=HRM_MT_WORKER.WORKER_INITIAL ");

                obj_str.Append(" LEFT JOIN HRM_MT_REASON ON HRM_MT_REASON.REASON_CODE=HRM_TR_TIMEDAYTYPE.REASON_CODE AND REASON_GROUP = 'SHIFT'");
                
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY HRM_TR_TIMEDAYTYPE.COMPANY_CODE, HRM_TR_TIMEDAYTYPE.WORKER_CODE, TIMEDAYTYPE_WORKDATE");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TRTimedaytype();

                    model.company_code = dr["COMPANY_CODE"].ToString();
                    model.worker_code = dr["WORKER_CODE"].ToString();

                    model.timedaytype_id = Convert.ToInt32(dr["TIMEDAYTYPE_ID"]);
                    model.timedaytype_doc = dr["TIMEDAYTYPE_DOC"].ToString();

                    model.timedaytype_workdate = Convert.ToDateTime(dr["TIMEDAYTYPE_WORKDATE"]);

                    model.timedaytype_old = dr["TIMEDAYTYPE_OLD"].ToString();
                    model.timedaytype_new = dr["TIMEDAYTYPE_NEW"].ToString();

                    model.timedaytype_note = dr["TIMEDAYTYPE_NOTE"].ToString();                   
                    model.reason_code = dr["REASON_CODE"].ToString();

                    model.worker_detail = dr["WORKER_DETAIL"].ToString();
                    model.reason_detail = dr["REASON_DETAIL"].ToString();
                                        
                    model.modified_by = dr["MODIFIED_BY"].ToString();
                    model.modified_date = Convert.ToDateTime(dr["MODIFIED_DATE"]);
                                                                                                                      
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Timedaytype.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TRTimedaytype> getDataByFillter(string language, string com, string emp, DateTime datefrom, DateTime dateto)
        {
            string strCondition = "";

            strCondition += " AND HRM_TR_TIMEDAYTYPE.COMPANY_CODE='" + com + "'";
            strCondition += " AND HRM_TR_TIMEDAYTYPE.WORKER_CODE='" + emp + "'";
            strCondition += " AND (TIMEDAYTYPE_WORKDATE BETWEEN '" + datefrom.ToString("MM/dd/yyyy") + "' AND '" + dateto.ToString("MM/dd/yyyy") + "')";

            return this.getData(language, strCondition);
        }

        public cls_TRTimedaytype getDataByID(string id)
        {

            string strCondition = " AND HRM_TR_TIMEDAYTYPE.TIMEDAYTYPE_ID='" + id + "'";

            List<cls_TRTimedaytype> list_model = this.getData("EN", strCondition);

            if (list_model.Count > 0)
                return list_model[0];
            else
                return null;

        }

        public bool checkDataOld(string com, string emp, DateTime date)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT COMPANY_CODE");
                obj_str.Append(" FROM HRM_TR_TIMEDAYTYPE");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND WORKER_CODE='" + emp + "'");
                obj_str.Append(" AND TIMEDAYTYPE_WORKDATE='" + date.ToString("MM/dd/yyyy") + "'");
                                                                
                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Timedaytype.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public int getNextID()
        {
            int intResult = 1;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT MAX(TIMEDAYTYPE_ID) ");
                obj_str.Append(" FROM HRM_TR_TIMEDAYTYPE");             

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    intResult = Convert.ToInt32(dt.Rows[0][0]) + 1;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Timedaytype.getNextID)" + ex.ToString();
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

                obj_str.Append(" DELETE FROM HRM_TR_TIMEDAYTYPE");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND TIMEDAYTYPE_ID='" + id + "'");
                                              
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Timedaytype.delete)" + ex.ToString();
            }

            return blnResult;
        }
        
        public bool insert(cls_TRTimedaytype model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.company_code, model.worker_code, model.timedaytype_workdate))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_TIMEDAYTYPE");
                obj_str.Append(" (");
                obj_str.Append("COMPANY_CODE ");
                obj_str.Append(", WORKER_CODE ");

                obj_str.Append(", TIMEDAYTYPE_ID ");
                obj_str.Append(", TIMEDAYTYPE_DOC ");

                obj_str.Append(", TIMEDAYTYPE_WORKDATE ");

                obj_str.Append(", TIMEDAYTYPE_OLD ");
                obj_str.Append(", TIMEDAYTYPE_NEW ");
                
                obj_str.Append(", TIMEDAYTYPE_NOTE ");
                obj_str.Append(", REASON_CODE ");
                
                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@COMPANY_CODE ");
                obj_str.Append(", @WORKER_CODE ");

                obj_str.Append(", @TIMEDAYTYPE_ID ");
                obj_str.Append(", @TIMEDAYTYPE_DOC ");

                obj_str.Append(", @TIMEDAYTYPE_WORKDATE ");

                obj_str.Append(", @TIMEDAYTYPE_OLD ");
                obj_str.Append(", @TIMEDAYTYPE_NEW ");

                obj_str.Append(", @TIMEDAYTYPE_NOTE ");
                obj_str.Append(", @REASON_CODE ");
                
                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;

                obj_cmd.Parameters.Add("@TIMEDAYTYPE_ID", SqlDbType.Int); obj_cmd.Parameters["@TIMEDAYTYPE_ID"].Value = this.getNextID();
                obj_cmd.Parameters.Add("@TIMEDAYTYPE_DOC", SqlDbType.VarChar); obj_cmd.Parameters["@TIMEDAYTYPE_DOC"].Value = model.timedaytype_doc;
                obj_cmd.Parameters.Add("@TIMEDAYTYPE_WORKDATE", SqlDbType.Date); obj_cmd.Parameters["@TIMEDAYTYPE_WORKDATE"].Value = model.timedaytype_workdate;

                obj_cmd.Parameters.Add("@TIMEDAYTYPE_OLD", SqlDbType.Char); obj_cmd.Parameters["@TIMEDAYTYPE_OLD"].Value = model.timedaytype_old;
                obj_cmd.Parameters.Add("@TIMEDAYTYPE_NEW", SqlDbType.Char); obj_cmd.Parameters["@TIMEDAYTYPE_NEW"].Value = model.timedaytype_new;

                obj_cmd.Parameters.Add("@TIMEDAYTYPE_NOTE", SqlDbType.VarChar); obj_cmd.Parameters["@TIMEDAYTYPE_NOTE"].Value = model.timedaytype_note;               
                obj_cmd.Parameters.Add("@REASON_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@REASON_CODE"].Value = model.reason_code;

                obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@CREATED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;
     
                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();
                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Timedaytype.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_TRTimedaytype model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_TR_TIMEDAYTYPE SET ");

                obj_str.Append(" TIMEDAYTYPE_DOC=@TIMEDAYTYPE_DOC ");
                obj_str.Append(", TIMEDAYTYPE_OLD=@TIMEDAYTYPE_OLD ");
                obj_str.Append(", TIMEDAYTYPE_NEW=@TIMEDAYTYPE_NEW ");
                
                obj_str.Append(", TIMEDAYTYPE_NOTE=@TIMEDAYTYPE_NOTE ");
                obj_str.Append(", REASON_CODE=@REASON_CODE ");                    
                
                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");                

                obj_str.Append(" WHERE TIMEDAYTYPE_ID=@TIMEDAYTYPE_ID ");
               
                
                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@TIMEDAYTYPE_DOC", SqlDbType.VarChar); obj_cmd.Parameters["@TIMEDAYTYPE_DOC"].Value = model.timedaytype_doc;

                obj_cmd.Parameters.Add("@TIMEDAYTYPE_OLD", SqlDbType.VarChar); obj_cmd.Parameters["@TIMEDAYTYPE_OLD"].Value = model.timedaytype_old;
                obj_cmd.Parameters.Add("@TIMEDAYTYPE_NEW", SqlDbType.VarChar); obj_cmd.Parameters["@TIMEDAYTYPE_NEW"].Value = model.timedaytype_new;

                obj_cmd.Parameters.Add("@TIMEDAYTYPE_NOTE", SqlDbType.VarChar); obj_cmd.Parameters["@TIMEDAYTYPE_NOTE"].Value = model.timedaytype_note;              
                obj_cmd.Parameters.Add("@REASON_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@REASON_CODE"].Value = model.reason_code;

                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = DateTime.Now;

                obj_cmd.Parameters.Add("@TIMEDAYTYPE_ID", SqlDbType.Int); obj_cmd.Parameters["@TIMEDAYTYPE_ID"].Value = model.timedaytype_id;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Timedaytype.update)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
