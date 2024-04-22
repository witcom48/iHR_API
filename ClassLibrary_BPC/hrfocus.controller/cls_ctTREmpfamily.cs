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
    public class cls_ctTREmpfamily
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTREmpfamily() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TREmpfamily> getData(string condition)
        {
            List<cls_TREmpfamily> list_model = new List<cls_TREmpfamily>();
            cls_TREmpfamily model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("EMPFAMILY_ID");
                obj_str.Append(", EMPFAMILY_CODE");               
                obj_str.Append(", FAMILY_TYPE");
                obj_str.Append(", EMPFAMILY_FNAME_TH");
                obj_str.Append(", EMPFAMILY_LNAME_TH");
                obj_str.Append(", EMPFAMILY_FNAME_EN");
                obj_str.Append(", EMPFAMILY_LNAME_EN");

                obj_str.Append(", EMPFAMILY_BIRTHDATE");

                obj_str.Append(", COMPANY_CODE");
                obj_str.Append(", WORKER_CODE");

                                 
                obj_str.Append(", ISNULL(MODIFIED_BY, CREATED_BY) AS MODIFIED_BY");
                obj_str.Append(", ISNULL(MODIFIED_DATE, CREATED_DATE) AS MODIFIED_DATE");

                obj_str.Append(" FROM HRM_TR_EMPFAMILY");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY COMPANY_CODE, WORKER_CODE, FAMILY_TYPE");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TREmpfamily();

                    model.empfamily_id = Convert.ToInt32(dr["EMPFAMILY_ID"]);

                    model.empfamily_code = dr["EMPFAMILY_CODE"].ToString();
                    model.family_type = dr["FAMILY_TYPE"].ToString();
                    model.empfamily_fname_th = dr["EMPFAMILY_FNAME_TH"].ToString();
                    model.empfamily_lname_th = dr["EMPFAMILY_LNAME_TH"].ToString();
                    model.empfamily_fname_en = dr["EMPFAMILY_FNAME_EN"].ToString();
                    model.empfamily_lname_en = dr["EMPFAMILY_LNAME_EN"].ToString();
                    model.empfamily_birthdate = Convert.ToDateTime(dr["EMPFAMILY_BIRTHDATE"]);

                    model.company_code = dr["COMPANY_CODE"].ToString();
                    model.worker_code = dr["WORKER_CODE"].ToString();
                                       
                    model.modified_by = dr["MODIFIED_BY"].ToString();
                    model.modified_date = Convert.ToDateTime(dr["MODIFIED_DATE"]);
                                                                                                                      
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(EMPFAMILY.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TREmpfamily> getDataByFillter(string com, string worker)
        {
            string strCondition = " AND COMPANY_CODE='" + com + "'";

            if (!worker.Equals(""))
                strCondition += " AND WORKER_CODE='" + worker + "'";

                        
            return this.getData(strCondition);
        }

        public List<cls_TREmpfamily> getDataByFillter2(string com, string worker)
        {
            string strCondition = " AND COMPANY_CODE='" + com + "'";

            if (!worker.Equals(""))
                strCondition += " AND WORKER_CODE=" + worker + "";


            return this.getData(strCondition);
        }
        public bool checkDataOld(string com, string worker, string type, string card)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT EMPFAMILY_ID");
                obj_str.Append(" FROM HRM_TR_EMPFAMILY");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND WORKER_CODE='" + worker + "'");
                obj_str.Append(" AND FAMILY_TYPE='" + type + "'");
                obj_str.Append(" AND EMPFAMILY_CODE='" + card + "'");
                                                
                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(EMPFAMILY.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public int getNextID()
        {
            int intResult = 1;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT MAX(EMPFAMILY_ID) ");
                obj_str.Append(" FROM HRM_TR_EMPFAMILY");             

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    intResult = Convert.ToInt32(dt.Rows[0][0])+1;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(EMPFAMILY.getNextID)" + ex.ToString();
            }

            return intResult;
        }

        public int getID(string com, string empid)
        {
            int intResult = 0;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT EMPFAMILY_ID ");
                obj_str.Append(" FROM HRM_TR_EMPFAMILY");
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
                Message = "ERROR::(EMPFAMILY.getID)" + ex.ToString();
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

                obj_str.Append(" DELETE FROM HRM_TR_EMPFAMILY");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND EMPFAMILY_ID='" + id + "'");
                                              
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Empreduce.delete)" + ex.ToString();
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

                obj_str.Append(" DELETE FROM HRM_TR_EMPFAMILY");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND WORKER_CODE='" + emp + "'");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Empreduce.clear)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(cls_TREmpfamily model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.company_code, model.worker_code, model.family_type, model.empfamily_code))
                {

                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_EMPFAMILY");
                obj_str.Append(" (");
                obj_str.Append("EMPFAMILY_ID ");
                obj_str.Append(", EMPFAMILY_CODE ");
                obj_str.Append(", FAMILY_TYPE ");
                obj_str.Append(", EMPFAMILY_FNAME_TH ");
                obj_str.Append(", EMPFAMILY_LNAME_TH ");
                obj_str.Append(", EMPFAMILY_FNAME_EN ");
                obj_str.Append(", EMPFAMILY_LNAME_EN ");
                obj_str.Append(", EMPFAMILY_BIRTHDATE ");
                obj_str.Append(", COMPANY_CODE ");
                obj_str.Append(", WORKER_CODE ");                                
                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@EMPFAMILY_ID ");
                obj_str.Append(", @EMPFAMILY_CODE ");
                obj_str.Append(", @FAMILY_TYPE ");
                obj_str.Append(", @EMPFAMILY_FNAME_TH ");
                obj_str.Append(", @EMPFAMILY_LNAME_TH ");
                obj_str.Append(", @EMPFAMILY_FNAME_EN ");
                obj_str.Append(", @EMPFAMILY_LNAME_EN ");
                obj_str.Append(", @EMPFAMILY_BIRTHDATE ");
                obj_str.Append(", @COMPANY_CODE ");
                obj_str.Append(", @WORKER_CODE ");
                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@EMPFAMILY_ID", SqlDbType.Int); obj_cmd.Parameters["@EMPFAMILY_ID"].Value = this.getNextID();
                obj_cmd.Parameters.Add("@EMPFAMILY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@EMPFAMILY_CODE"].Value = model.empfamily_code;
                obj_cmd.Parameters.Add("@FAMILY_TYPE", SqlDbType.VarChar); obj_cmd.Parameters["@FAMILY_TYPE"].Value = model.family_type;
                obj_cmd.Parameters.Add("@EMPFAMILY_FNAME_TH", SqlDbType.VarChar); obj_cmd.Parameters["@EMPFAMILY_FNAME_TH"].Value = model.empfamily_fname_th;
                obj_cmd.Parameters.Add("@EMPFAMILY_LNAME_TH", SqlDbType.VarChar); obj_cmd.Parameters["@EMPFAMILY_LNAME_TH"].Value = model.empfamily_lname_th;
                obj_cmd.Parameters.Add("@EMPFAMILY_FNAME_EN", SqlDbType.VarChar); obj_cmd.Parameters["@EMPFAMILY_FNAME_EN"].Value = model.empfamily_fname_en;
                obj_cmd.Parameters.Add("@EMPFAMILY_LNAME_EN", SqlDbType.VarChar); obj_cmd.Parameters["@EMPFAMILY_LNAME_EN"].Value = model.empfamily_lname_en;
                //obj_cmd.Parameters.Add("@EMPFAMILY_BIRTHDATE", SqlDbType.DateTime); obj_cmd.Parameters["@EMPFAMILY_BIRTHDATE"].Value = (object)model.empfamily_birthdate ?? DBNull.Value;
                obj_cmd.Parameters.Add("@EMPFAMILY_BIRTHDATE", SqlDbType.DateTime); obj_cmd.Parameters["@EMPFAMILY_BIRTHDATE"].Value = model.empfamily_birthdate;

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
                Message = "ERROR::(EMPFAMILY.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_TREmpfamily model)
        {
            string strResult = model.empfamily_id.ToString();
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_TR_EMPFAMILY SET ");

                obj_str.Append(" EMPFAMILY_CODE=@EMPFAMILY_CODE ");
                obj_str.Append(", FAMILY_TYPE=@FAMILY_TYPE ");
                obj_str.Append(", EMPFAMILY_FNAME_TH=@EMPFAMILY_FNAME_TH ");
                obj_str.Append(", EMPFAMILY_LNAME_TH=@EMPFAMILY_LNAME_TH ");
                obj_str.Append(", EMPFAMILY_FNAME_EN=@EMPFAMILY_FNAME_EN ");
                obj_str.Append(", EMPFAMILY_LNAME_EN=@EMPFAMILY_LNAME_EN ");
                obj_str.Append(", EMPFAMILY_BIRTHDATE=@EMPFAMILY_BIRTHDATE ");
               
              
                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");
                obj_str.Append(", FLAG=@FLAG ");

                obj_str.Append(" WHERE EMPFAMILY_ID=@EMPFAMILY_ID ");
                obj_str.Append(" AND WORKER_CODE=@WORKER_CODE ");

                
                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                if (model.empfamily_id == 0)
                {
                    strResult = this.getID(model.company_code, model.worker_code).ToString();
                }

                obj_cmd.Parameters.Add("@EMPFAMILY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@EMPFAMILY_CODE"].Value = model.empfamily_code;
                obj_cmd.Parameters.Add("@FAMILY_TYPE", SqlDbType.VarChar); obj_cmd.Parameters["@FAMILY_TYPE"].Value = model.family_type;
                obj_cmd.Parameters.Add("@EMPFAMILY_FNAME_TH", SqlDbType.VarChar); obj_cmd.Parameters["@EMPFAMILY_FNAME_TH"].Value = model.empfamily_fname_th;
                obj_cmd.Parameters.Add("@EMPFAMILY_LNAME_TH", SqlDbType.VarChar); obj_cmd.Parameters["@EMPFAMILY_LNAME_TH"].Value = model.empfamily_lname_th;
                obj_cmd.Parameters.Add("@EMPFAMILY_FNAME_EN", SqlDbType.VarChar); obj_cmd.Parameters["@EMPFAMILY_FNAME_EN"].Value = model.empfamily_fname_en;
                obj_cmd.Parameters.Add("@EMPFAMILY_LNAME_EN", SqlDbType.VarChar); obj_cmd.Parameters["@EMPFAMILY_LNAME_EN"].Value = model.empfamily_lname_en;
                obj_cmd.Parameters.Add("@EMPFAMILY_BIRTHDATE", SqlDbType.DateTime); obj_cmd.Parameters["@EMPFAMILY_BIRTHDATE"].Value = model.empfamily_birthdate;
                //obj_cmd.Parameters.Add("@EMPFAMILY_BIRTHDATE", SqlDbType.DateTime); obj_cmd.Parameters["@EMPFAMILY_BIRTHDATE"].Value = (object)model.empfamily_birthdate ?? DBNull.Value;

                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;

                obj_cmd.Parameters.Add("@EMPFAMILY_ID", SqlDbType.Int); obj_cmd.Parameters["@EMPFAMILY_ID"].Value = strResult;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(EMPFAMILY.update)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
