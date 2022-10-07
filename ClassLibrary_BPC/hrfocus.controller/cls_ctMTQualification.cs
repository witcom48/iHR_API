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
    public class cls_ctMTQualification
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctMTQualification() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_MTQualification> getData(string condition)
        {
            List<cls_MTQualification> list_model = new List<cls_MTQualification>();
            cls_MTQualification model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("COMPANY_CODE");
                obj_str.Append(", QUALIFICATION_ID");
                obj_str.Append(", QUALIFICATION_CODE");
                obj_str.Append(", ISNULL(QUALIFICATION_NAME_TH, '') AS QUALIFICATION_NAME_TH");
                obj_str.Append(", ISNULL(QUALIFICATION_NAME_EN, '') AS QUALIFICATION_NAME_EN");
                
                obj_str.Append(", ISNULL(MODIFIED_BY, CREATED_BY) AS MODIFIED_BY");
                obj_str.Append(", ISNULL(MODIFIED_DATE, CREATED_DATE) AS MODIFIED_DATE");

                obj_str.Append(" FROM HRM_MT_QUALIFICATION");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY COMPANY_CODE, QUALIFICATION_CODE");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_MTQualification();

                    model.company_code = Convert.ToString(dr["COMPANY_CODE"]);
                    model.qualification_id = Convert.ToInt32(dr["QUALIFICATION_ID"]);
                    model.qualification_code = Convert.ToString(dr["QUALIFICATION_CODE"]);
                    model.qualification_name_th = Convert.ToString(dr["QUALIFICATION_NAME_TH"]);
                    model.qualification_name_en = Convert.ToString(dr["QUALIFICATION_NAME_EN"]);
                   
                    model.modified_by = dr["MODIFIED_BY"].ToString();
                    model.modified_date = Convert.ToDateTime(dr["MODIFIED_DATE"]);
                                                                                                                      
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Qualification.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_MTQualification> getDataByFillter(string com, string id, string code)
        {
            string strCondition = "";

            strCondition += " AND COMPANY_CODE='" + com + "'";

            if (!id.Equals(""))
                strCondition += " AND QUALIFICATION_ID='" + id + "'";

            if (!code.Equals(""))
                strCondition += " AND QUALIFICATION_CODE='" + code + "'";
            
            return this.getData(strCondition);
        }

        public bool checkDataOld(string com, string code)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT QUALIFICATION_ID");
                obj_str.Append(" FROM HRM_MT_QUALIFICATION");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND QUALIFICATION_CODE='" + code + "'");
                                                
                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Qualification.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public int getNextID()
        {
            int intResult = 1;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT MAX(QUALIFICATION_ID) ");
                obj_str.Append(" FROM HRM_MT_QUALIFICATION");                

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    intResult = Convert.ToInt32(dt.Rows[0][0]) + 1;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Qualification.getNextID)" + ex.ToString();
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

                obj_str.Append(" DELETE FROM HRM_MT_QUALIFICATION");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND QUALIFICATION_ID='" + id + "'");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Qualification.delete)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(cls_MTQualification model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.company_code, model.qualification_code))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_MT_QUALIFICATION");
                obj_str.Append(" (");
                obj_str.Append("QUALIFICATION_ID ");
                obj_str.Append(", QUALIFICATION_CODE ");
                obj_str.Append(", QUALIFICATION_NAME_TH ");
                obj_str.Append(", QUALIFICATION_NAME_EN ");
                                
                obj_str.Append(", COMPANY_CODE ");

                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@QUALIFICATION_ID ");
                obj_str.Append(", @QUALIFICATION_CODE ");
                obj_str.Append(", @QUALIFICATION_NAME_TH ");
                obj_str.Append(", @QUALIFICATION_NAME_EN ");                
                obj_str.Append(", @COMPANY_CODE ");
                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");              
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@QUALIFICATION_ID", SqlDbType.Int); obj_cmd.Parameters["@QUALIFICATION_ID"].Value = this.getNextID();
                obj_cmd.Parameters.Add("@QUALIFICATION_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@QUALIFICATION_CODE"].Value = model.qualification_code;
                obj_cmd.Parameters.Add("@QUALIFICATION_NAME_TH", SqlDbType.VarChar); obj_cmd.Parameters["@QUALIFICATION_NAME_TH"].Value = model.qualification_name_th;
                obj_cmd.Parameters.Add("@QUALIFICATION_NAME_EN", SqlDbType.VarChar); obj_cmd.Parameters["@QUALIFICATION_NAME_EN"].Value = model.qualification_name_en;
                              
                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;

                obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@CREATED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;
     
                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {              
                Message = "ERROR::(Qualification.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_MTQualification model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_MT_QUALIFICATION SET ");

                obj_str.Append(" QUALIFICATION_CODE=@QUALIFICATION_CODE ");
                obj_str.Append(", QUALIFICATION_NAME_TH=@QUALIFICATION_NAME_TH ");
                obj_str.Append(", QUALIFICATION_NAME_EN=@QUALIFICATION_NAME_EN ");
                                
                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");
                obj_str.Append(", FLAG=@FLAG ");


                obj_str.Append(" WHERE QUALIFICATION_ID=@QUALIFICATION_ID ");

                
                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@QUALIFICATION_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@QUALIFICATION_CODE"].Value = model.qualification_code;
                obj_cmd.Parameters.Add("@QUALIFICATION_NAME_TH", SqlDbType.VarChar); obj_cmd.Parameters["@QUALIFICATION_NAME_TH"].Value = model.qualification_name_th;
                obj_cmd.Parameters.Add("@QUALIFICATION_NAME_EN", SqlDbType.VarChar); obj_cmd.Parameters["@QUALIFICATION_NAME_EN"].Value = model.qualification_name_en;
                
                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;

                obj_cmd.Parameters.Add("@QUALIFICATION_ID", SqlDbType.Int); obj_cmd.Parameters["@QUALIFICATION_ID"].Value = model.qualification_id;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Qualification.update)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
