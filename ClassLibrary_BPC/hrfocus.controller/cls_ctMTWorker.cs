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
    public class cls_ctMTWorker
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctMTWorker() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_MTWorker> getData(string condition)
        {
            List<cls_MTWorker> list_model = new List<cls_MTWorker>();
            cls_MTWorker model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("COMPANY_CODE");
                obj_str.Append(", WORKER_ID");
                obj_str.Append(", WORKER_CODE");
                obj_str.Append(", WORKER_CARD");
                obj_str.Append(", WORKER_INITIAL");
                obj_str.Append(", ISNULL(WORKER_FNAME_TH, '') AS WORKER_FNAME_TH");
                obj_str.Append(", ISNULL(WORKER_LNAME_TH, '') AS WORKER_LNAME_TH");
                obj_str.Append(", ISNULL(WORKER_FNAME_EN, '') AS WORKER_FNAME_EN");
                obj_str.Append(", ISNULL(WORKER_LNAME_EN, '') AS WORKER_LNAME_EN");

                obj_str.Append(", WORKER_EMPTYPE");
                obj_str.Append(", WORKER_GENDER");
                obj_str.Append(", WORKER_BIRTHDATE");
                obj_str.Append(", WORKER_HIREDATE");

                obj_str.Append(", ISNULL(WORKER_RESIGNDATE, '01/01/2999') AS WORKER_RESIGNDATE");
                obj_str.Append(", ISNULL(WORKER_RESIGNSTATUS, '0') AS WORKER_RESIGNSTATUS");
                obj_str.Append(", ISNULL(WORKER_RESIGNREASON, '') AS WORKER_RESIGNREASON");

                obj_str.Append(", ISNULL(WORKER_PROBATIONDATE, '01/01/2999') AS WORKER_PROBATIONDATE");
                obj_str.Append(", ISNULL(WORKER_PROBATIONENDDATE, '01/01/2999') AS WORKER_PROBATIONENDDATE");

                obj_str.Append(", ISNULL(HRS_PERDAY, 0) AS HRS_PERDAY");

                obj_str.Append(", ISNULL(WORKER_TAXMETHOD, '1') AS WORKER_TAXMETHOD");

                obj_str.Append(", ISNULL(HRM_MT_WORKER.MODIFIED_BY, HRM_MT_WORKER.CREATED_BY) AS MODIFIED_BY");
                obj_str.Append(", ISNULL(HRM_MT_WORKER.MODIFIED_DATE, HRM_MT_WORKER.CREATED_DATE) AS MODIFIED_DATE");


                obj_str.Append(", ISNULL(INITIAL_NAME_TH, '') AS INITIAL_NAME_TH");
                obj_str.Append(", ISNULL(INITIAL_NAME_EN, '') AS INITIAL_NAME_EN");

                obj_str.Append(", ISNULL(SELF_ADMIN, 0) AS SELF_ADMIN");

                obj_str.Append(" FROM HRM_MT_WORKER");
                obj_str.Append(" INNER JOIN HRM_MT_INITIAL ON HRM_MT_WORKER.WORKER_INITIAL=HRM_MT_INITIAL.INITIAL_CODE");

                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY COMPANY_CODE, WORKER_CODE");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_MTWorker();

                    model.company_code = dr["COMPANY_CODE"].ToString();

                    model.worker_id = Convert.ToInt32(dr["WORKER_ID"]);
                    model.worker_code = dr["WORKER_CODE"].ToString();
                    model.worker_card = dr["WORKER_CARD"].ToString();
                    model.worker_initial = dr["WORKER_INITIAL"].ToString();
                    model.worker_fname_th = dr["WORKER_FNAME_TH"].ToString();
                    model.worker_lname_th = dr["WORKER_LNAME_TH"].ToString();
                    model.worker_fname_en = dr["WORKER_FNAME_EN"].ToString();
                    model.worker_lname_en = dr["WORKER_LNAME_EN"].ToString();

                    model.worker_emptype = dr["WORKER_EMPTYPE"].ToString();
                    model.worker_gender = dr["WORKER_GENDER"].ToString();
                    model.worker_birthdate = Convert.ToDateTime(dr["WORKER_BIRTHDATE"]);
                    model.worker_hiredate = Convert.ToDateTime(dr["WORKER_HIREDATE"]);

                    model.worker_resigndate = Convert.ToDateTime(dr["WORKER_RESIGNDATE"]);
                    model.worker_resignstatus = Convert.ToBoolean(dr["WORKER_RESIGNSTATUS"]);
                    model.worker_resignreason = dr["WORKER_RESIGNREASON"].ToString();

                    model.worker_probationdate = Convert.ToDateTime(dr["WORKER_PROBATIONDATE"]);
                    model.worker_probationenddate = Convert.ToDateTime(dr["WORKER_PROBATIONENDDATE"]);
                    
                    model.hrs_perday = Convert.ToDouble(dr["HRS_PERDAY"]);

                    model.worker_taxmethod = dr["WORKER_TAXMETHOD"].ToString();
                    
                    model.modified_by = dr["MODIFIED_BY"].ToString();
                    model.modified_date = Convert.ToDateTime(dr["MODIFIED_DATE"]);

                    model.initial_name_th = dr["INITIAL_NAME_TH"].ToString();
                    model.initial_name_en = dr["INITIAL_NAME_EN"].ToString();

                    model.self_admin = Convert.ToBoolean(dr["SELF_ADMIN"]);
                                                                                                                      
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Worker.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_MTWorker> getDataByFillter(string com, string id, string worker_code, string worker_fname_th, string worker_lname_th, string worker_fname_en, string worker_lname_en, string emptype, string level_code, string depcod_code, string position_code, string group_code, bool include_resign, string location_code, DateTime date_fill)
        {
            string strCondition = "";

            if (!com.Equals(""))
                strCondition += " AND COMPANY_CODE='" + com + "'";

            if (!emptype.Equals(""))
                strCondition += " AND WORKER_EMPTYPE='" + emptype + "'";

            if (!id.Equals(""))
                strCondition += " AND WORKER_ID='" + id + "'";

            if (!worker_code.Equals(""))
                strCondition += " AND WORKER_CODE='" + worker_code + "'";


            if (!level_code.Equals("") && !depcod_code.Equals(""))
            {
                strCondition += " AND WORKER_CODE IN (SELECT WORKER_CODE FROM HRM_TR_EMPDEP WHERE COMPANY_CODE='" + com + "' AND EMPDEP_LEVEL" + level_code + "='" + depcod_code + "')";
            }

            if (!position_code.Equals(""))
            {
                strCondition += " AND WORKER_CODE IN (SELECT DISTINCT WORKER_CODE FROM HRM_TR_EMPPOSITION WHERE COMPANY_CODE='" + com + "' AND EMPPOSITION_POSITION='" + position_code + "' )";
            }

            if (!location_code.Equals(""))
            {
                strCondition += " AND WORKER_CODE IN (SELECT DISTINCT WORKER_CODE FROM HRM_TR_EMPLOCATION WHERE COMPANY_CODE='" + com + "' AND LOCATION_CODE='" + location_code + "' )";
            }
                

            if (!include_resign)
            {
                strCondition += " AND (WORKER_RESIGNSTATUS='0' OR WORKER_RESIGNSTATUS IS NULL) ";
            }
                

            return this.getData(strCondition);
        }

        public List<cls_MTWorker> getDataByWorkerCode(string com, string worker_code)
        {
            string strCondition = "";

            
            strCondition += " AND COMPANY_CODE='" + com + "'";
            strCondition += " AND WORKER_CODE='" + worker_code + "'";            

            return this.getData(strCondition);
        }

        public List<cls_MTWorker> getDataByCompanyCode(string com)
        {          
            return this.getData(" AND COMPANY_CODE='" + com + "'");
        }

        public List<cls_MTWorker> getDataMultipleEmp(string com, string worker)
        {
            string strCondition = " AND COMPANY_CODE='" + com + "'";
            strCondition += " AND WORKER_CODE IN (" + worker + ") ";

            return this.getData(strCondition);
        }

        public cls_MTWorker doLogin(string worker_code, string worker_pwd)
        {
            string strCondition = "";


            
            strCondition += " AND WORKER_CODE='" + worker_code + "'";
            strCondition += " AND WORKER_PWD='" + worker_pwd + "'";

            List<cls_MTWorker> list_model = this.getData(strCondition);

            if (list_model.Count > 0)
                return list_model[0];
            else
                return null;

        }


        public bool checkDataOld(string com, string code)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT WORKER_ID");
                obj_str.Append(" FROM HRM_MT_WORKER");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND WORKER_CODE='" + code + "'");
                                                
                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Worker.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public bool checkDataOld(string id)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT WORKER_ID");
                obj_str.Append(" FROM HRM_MT_WORKER");
                obj_str.Append(" WHERE 1=1 ");                
                obj_str.Append(" AND WORKER_ID='" + id + "'");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Worker.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public int doGetNextRunningID(string com, string prefix)
        {
            int intResult = 1;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT TOP 1 WORKER_CODE");
                obj_str.Append(" FROM HRM_MT_WORKER");                
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND WORKER_CODE LIKE '" + prefix + "%'");
                obj_str.Append(" ORDER BY WORKER_CODE DESC");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    string strID = dt.Rows[0][0].ToString();

                    strID = strID.Replace(prefix, "");

                    intResult = Convert.ToInt32(strID) + 1;
                    
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Worker.doGetNextRunningID)" + ex.ToString();
            }

            return intResult;
        }

        public int getNextID()
        {
            int intResult = 1;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT MAX(WORKER_ID) ");
                obj_str.Append(" FROM HRM_MT_WORKER");             

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    intResult = Convert.ToInt32(dt.Rows[0][0]) + 1;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Worker.getNextID)" + ex.ToString();
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

                obj_str.Append(" DELETE FROM HRM_MT_WORKER");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND WORKER_ID='" + id + "'");
                                              
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Company.delete)" + ex.ToString();
            }

            return blnResult;
        }

        public string insert(cls_MTWorker model)
        {
            string strResult = "";
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.worker_id.ToString()))
                {
                    bool blnResult = this.update(model);

                    if (blnResult)
                        return model.worker_code;
                }
                cls_ctSYSPackage Package = new cls_ctSYSPackage();
                List<cls_SYSPackage> listPackage =   Package.getData();
                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_MT_WORKER");
                obj_str.Append(" (");
                obj_str.Append("COMPANY_CODE ");
                obj_str.Append(", WORKER_ID ");
                obj_str.Append(", WORKER_CODE ");
                obj_str.Append(", WORKER_CARD ");

                obj_str.Append(", WORKER_INITIAL ");

                obj_str.Append(", WORKER_FNAME_TH ");
                obj_str.Append(", WORKER_LNAME_TH ");

                obj_str.Append(", WORKER_FNAME_EN ");
                obj_str.Append(", WORKER_LNAME_EN ");

                obj_str.Append(", WORKER_EMPTYPE ");
                obj_str.Append(", WORKER_GENDER ");
                obj_str.Append(", WORKER_BIRTHDATE ");
                obj_str.Append(", WORKER_HIREDATE ");

                obj_str.Append(", WORKER_RESIGNSTATUS ");

                if (model.worker_resignstatus)
                {
                    obj_str.Append(", WORKER_RESIGNDATE ");
                    obj_str.Append(", WORKER_RESIGNREASON ");
                }

                
                obj_str.Append(", WORKER_PROBATIONDATE ");
                obj_str.Append(", WORKER_PROBATIONENDDATE ");

                obj_str.Append(", HRS_PERDAY ");

                obj_str.Append(", WORKER_TAXMETHOD ");

                obj_str.Append(", WORKER_PWD ");
                obj_str.Append(", SELF_ADMIN ");
                
                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@COMPANY_CODE ");
                obj_str.Append(", @WORKER_ID ");
                obj_str.Append(", @WORKER_CODE ");
                obj_str.Append(", @WORKER_CARD ");

                obj_str.Append(", @WORKER_INITIAL ");

                obj_str.Append(", @WORKER_FNAME_TH ");
                obj_str.Append(", @WORKER_LNAME_TH ");

                obj_str.Append(", @WORKER_FNAME_EN ");
                obj_str.Append(", @WORKER_LNAME_EN ");

                obj_str.Append(", @WORKER_EMPTYPE ");
                obj_str.Append(", @WORKER_GENDER ");
                obj_str.Append(", @WORKER_BIRTHDATE ");
                obj_str.Append(", @WORKER_HIREDATE ");

                obj_str.Append(", @WORKER_RESIGNSTATUS ");

                if (model.worker_resignstatus)
                {
                    obj_str.Append(", @WORKER_RESIGNDATE ");
                    obj_str.Append(", @WORKER_RESIGNREASON ");
                }
                               

                obj_str.Append(", @WORKER_PROBATIONDATE ");
                obj_str.Append(", @WORKER_PROBATIONENDDATE ");

                obj_str.Append(", @HRS_PERDAY ");

                obj_str.Append(", @WORKER_TAXMETHOD ");
                obj_str.Append(", @WORKER_PWD ");
                obj_str.Append(", @SELF_ADMIN ");

                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                strResult = this.getNextID().ToString();

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;

                obj_cmd.Parameters.Add("@WORKER_ID", SqlDbType.Int); obj_cmd.Parameters["@WORKER_ID"].Value = strResult;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                obj_cmd.Parameters.Add("@WORKER_CARD", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CARD"].Value = model.worker_card;

                obj_cmd.Parameters.Add("@WORKER_INITIAL", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_INITIAL"].Value = model.worker_initial;
                
                obj_cmd.Parameters.Add("@WORKER_FNAME_TH", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_FNAME_TH"].Value = model.worker_fname_th;
                obj_cmd.Parameters.Add("@WORKER_LNAME_TH", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_LNAME_TH"].Value = model.worker_lname_th;
                obj_cmd.Parameters.Add("@WORKER_FNAME_EN", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_FNAME_EN"].Value = model.worker_fname_en;
                obj_cmd.Parameters.Add("@WORKER_LNAME_EN", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_LNAME_EN"].Value = model.worker_lname_en;

                obj_cmd.Parameters.Add("@WORKER_EMPTYPE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_EMPTYPE"].Value = model.worker_emptype;
                obj_cmd.Parameters.Add("@WORKER_GENDER", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_GENDER"].Value = model.worker_gender;

                obj_cmd.Parameters.Add("@WORKER_BIRTHDATE", SqlDbType.DateTime); obj_cmd.Parameters["@WORKER_BIRTHDATE"].Value = model.worker_birthdate;
                obj_cmd.Parameters.Add("@WORKER_HIREDATE", SqlDbType.DateTime); obj_cmd.Parameters["@WORKER_HIREDATE"].Value = model.worker_hiredate;

                obj_cmd.Parameters.Add("@WORKER_RESIGNSTATUS", SqlDbType.Bit); obj_cmd.Parameters["@WORKER_RESIGNSTATUS"].Value = model.worker_resignstatus;

                if (model.worker_resignstatus)
                {
                    obj_cmd.Parameters.Add("@WORKER_RESIGNDATE", SqlDbType.DateTime); obj_cmd.Parameters["@WORKER_RESIGNDATE"].Value = model.worker_resigndate;
                    obj_cmd.Parameters.Add("@WORKER_RESIGNREASON", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_RESIGNREASON"].Value = model.worker_resignreason;
                }

                
                obj_cmd.Parameters.Add("@WORKER_PROBATIONDATE", SqlDbType.DateTime); obj_cmd.Parameters["@WORKER_PROBATIONDATE"].Value = model.worker_probationdate;
                obj_cmd.Parameters.Add("@WORKER_PROBATIONENDDATE", SqlDbType.DateTime); obj_cmd.Parameters["@WORKER_PROBATIONENDDATE"].Value = model.worker_probationenddate;

                obj_cmd.Parameters.Add("@HRS_PERDAY", SqlDbType.Decimal); obj_cmd.Parameters["@HRS_PERDAY"].Value = model.hrs_perday;

                obj_cmd.Parameters.Add("@WORKER_TAXMETHOD", SqlDbType.Char); obj_cmd.Parameters["@WORKER_TAXMETHOD"].Value = model.worker_taxmethod;
                obj_cmd.Parameters.Add("@WORKER_PWD", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_PWD"].Value = model.worker_pwd;

                obj_cmd.Parameters.Add("@SELF_ADMIN", SqlDbType.Bit); obj_cmd.Parameters["@SELF_ADMIN"].Value = model.self_admin;


                obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@CREATED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

            }
            catch (Exception ex)
            {
                strResult = "";
                Message = "ERROR::(Worker.insert)" + ex.ToString();
            }

            return strResult;
        }

        public bool update(cls_MTWorker model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_MT_WORKER SET ");

                obj_str.Append(" WORKER_CODE=@WORKER_CODE ");
                obj_str.Append(", WORKER_CARD=@WORKER_CARD ");
                obj_str.Append(", WORKER_INITIAL=@WORKER_INITIAL ");
                obj_str.Append(", WORKER_FNAME_TH=@WORKER_FNAME_TH ");
                obj_str.Append(", WORKER_LNAME_TH=@WORKER_LNAME_TH ");

                obj_str.Append(", WORKER_FNAME_EN=@WORKER_FNAME_EN ");
                obj_str.Append(", WORKER_LNAME_EN=@WORKER_LNAME_EN ");

                obj_str.Append(", WORKER_EMPTYPE=@WORKER_EMPTYPE ");
                obj_str.Append(", WORKER_GENDER=@WORKER_GENDER ");

                obj_str.Append(", WORKER_BIRTHDATE=@WORKER_BIRTHDATE ");
                obj_str.Append(", WORKER_HIREDATE=@WORKER_HIREDATE ");

                obj_str.Append(", WORKER_RESIGNSTATUS=@WORKER_RESIGNSTATUS ");

                if (model.worker_resignstatus)
                {
                    obj_str.Append(", WORKER_RESIGNDATE=@WORKER_RESIGNDATE ");
                    obj_str.Append(", WORKER_RESIGNREASON=@WORKER_RESIGNREASON ");
                }


                obj_str.Append(", WORKER_PROBATIONDATE=@WORKER_PROBATIONDATE ");
                obj_str.Append(", WORKER_PROBATIONENDDATE=@WORKER_PROBATIONENDDATE ");

                obj_str.Append(", HRS_PERDAY=@HRS_PERDAY ");

                obj_str.Append(", WORKER_TAXMETHOD=@WORKER_TAXMETHOD ");

                obj_str.Append(", SELF_ADMIN=@SELF_ADMIN ");

                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");
                obj_str.Append(", FLAG=@FLAG ");

                obj_str.Append(" WHERE WORKER_ID=@WORKER_ID ");
                
                
                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());                              
                
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                obj_cmd.Parameters.Add("@WORKER_CARD", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CARD"].Value = model.worker_card;

                obj_cmd.Parameters.Add("@WORKER_INITIAL", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_INITIAL"].Value = model.worker_initial;

                obj_cmd.Parameters.Add("@WORKER_FNAME_TH", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_FNAME_TH"].Value = model.worker_fname_th;
                obj_cmd.Parameters.Add("@WORKER_LNAME_TH", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_LNAME_TH"].Value = model.worker_lname_th;
                obj_cmd.Parameters.Add("@WORKER_FNAME_EN", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_FNAME_EN"].Value = model.worker_fname_en;
                obj_cmd.Parameters.Add("@WORKER_LNAME_EN", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_LNAME_EN"].Value = model.worker_lname_en;

                obj_cmd.Parameters.Add("@WORKER_EMPTYPE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_EMPTYPE"].Value = model.worker_emptype;
                obj_cmd.Parameters.Add("@WORKER_GENDER", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_GENDER"].Value = model.worker_gender;

                obj_cmd.Parameters.Add("@WORKER_BIRTHDATE", SqlDbType.DateTime); obj_cmd.Parameters["@WORKER_BIRTHDATE"].Value = model.worker_birthdate;
                obj_cmd.Parameters.Add("@WORKER_HIREDATE", SqlDbType.DateTime); obj_cmd.Parameters["@WORKER_HIREDATE"].Value = model.worker_hiredate;

                obj_cmd.Parameters.Add("@WORKER_RESIGNSTATUS", SqlDbType.Bit); obj_cmd.Parameters["@WORKER_RESIGNSTATUS"].Value = model.worker_resignstatus;
                
                if (model.worker_resignstatus)
                {
                    obj_cmd.Parameters.Add("@WORKER_RESIGNDATE", SqlDbType.DateTime); obj_cmd.Parameters["@WORKER_RESIGNDATE"].Value = model.worker_resigndate;
                    obj_cmd.Parameters.Add("@WORKER_RESIGNREASON", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_RESIGNREASON"].Value = model.worker_resignreason;
                }
                
                obj_cmd.Parameters.Add("@WORKER_PROBATIONDATE", SqlDbType.DateTime); obj_cmd.Parameters["@WORKER_PROBATIONDATE"].Value = model.worker_probationdate;
                obj_cmd.Parameters.Add("@WORKER_PROBATIONENDDATE", SqlDbType.DateTime); obj_cmd.Parameters["@WORKER_PROBATIONENDDATE"].Value = model.worker_probationenddate;

                obj_cmd.Parameters.Add("@HRS_PERDAY", SqlDbType.Decimal); obj_cmd.Parameters["@HRS_PERDAY"].Value = model.hrs_perday;

                obj_cmd.Parameters.Add("@WORKER_TAXMETHOD", SqlDbType.Char); obj_cmd.Parameters["@WORKER_TAXMETHOD"].Value = model.worker_taxmethod;

                obj_cmd.Parameters.Add("@SELF_ADMIN", SqlDbType.Bit); obj_cmd.Parameters["@SELF_ADMIN"].Value = model.self_admin;
                

                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;

                obj_cmd.Parameters.Add("@WORKER_ID", SqlDbType.Int); obj_cmd.Parameters["@WORKER_ID"].Value = model.worker_id;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Worker.update)" + ex.ToString();
            }

            return blnResult;
        }

        public bool change_pwd(cls_MTWorker model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_MT_WORKER SET ");

                obj_str.Append(" WORKER_PWD=@WORKER_PWD ");

                obj_str.Append(" WHERE WORKER_CODE=@WORKER_CODE ");
                obj_str.Append(" AND COMPANY_CODE=@COMPANY_CODE ");


                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@WORKER_PWD", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_PWD"].Value = model.worker_pwd;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Worker.change_pwd)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
