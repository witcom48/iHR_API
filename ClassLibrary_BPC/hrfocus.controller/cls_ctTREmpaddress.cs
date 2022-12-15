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
    public class cls_ctTREmpaddress
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTREmpaddress() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TREmpaddress> getData(string condition)
        {
            List<cls_TREmpaddress> list_model = new List<cls_TREmpaddress>();
            cls_TREmpaddress model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("COMPANY_CODE");
                obj_str.Append(", WORKER_CODE");
                obj_str.Append(", EMPADDRESS_TYPE");

                obj_str.Append(", ISNULL(EMPADDRESS_NO, '') AS EMPADDRESS_NO");
                obj_str.Append(", ISNULL(EMPADDRESS_MOO, '') AS EMPADDRESS_MOO");
                obj_str.Append(", ISNULL(EMPADDRESS_SOI, '') AS EMPADDRESS_SOI");
                obj_str.Append(", ISNULL(EMPADDRESS_ROAD, '') AS EMPADDRESS_ROAD");
                obj_str.Append(", ISNULL(EMPADDRESS_TAMBON, '') AS EMPADDRESS_TAMBON");
                obj_str.Append(", ISNULL(EMPADDRESS_AMPHUR, '') AS EMPADDRESS_AMPHUR");
                obj_str.Append(", ISNULL(EMPADDRESS_ZIPCODE, '') AS EMPADDRESS_ZIPCODE");
                obj_str.Append(", ISNULL(EMPADDRESS_TEL, '') AS EMPADDRESS_TEL");
                obj_str.Append(", ISNULL(EMPADDRESS_EMAIL, '') AS EMPADDRESS_EMAIL");
                obj_str.Append(", ISNULL(EMPADDRESS_LINE, '') AS EMPADDRESS_LINE");
                obj_str.Append(", ISNULL(EMPADDRESS_FACEBOOK, '') AS EMPADDRESS_FACEBOOK");
                obj_str.Append(", ISNULL(PROVINCE_CODE, '') AS PROVINCE_CODE");

                obj_str.Append(", ISNULL(MODIFIED_BY, CREATED_BY) AS MODIFIED_BY");
                obj_str.Append(", ISNULL(MODIFIED_DATE, CREATED_DATE) AS MODIFIED_DATE");

                obj_str.Append(" FROM HRM_TR_EMPADDRESS");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY COMPANY_CODE, WORKER_CODE, EMPADDRESS_TYPE");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TREmpaddress();

                    model.company_code = Convert.ToString(dr["COMPANY_CODE"]);
                    model.worker_code = Convert.ToString(dr["WORKER_CODE"]);
                    model.empaddress_type = Convert.ToString(dr["EMPADDRESS_TYPE"]);
                    model.empaddress_no = Convert.ToString(dr["EMPADDRESS_NO"]);
                    model.empaddress_moo = Convert.ToString(dr["EMPADDRESS_MOO"]);
                    model.empaddress_soi = Convert.ToString(dr["EMPADDRESS_SOI"]);
                    model.empaddress_road = Convert.ToString(dr["EMPADDRESS_ROAD"]);
                    model.empaddress_tambon = Convert.ToString(dr["EMPADDRESS_TAMBON"]);
                    model.empaddress_amphur = Convert.ToString(dr["EMPADDRESS_AMPHUR"]);
                    model.empaddress_zipcode = Convert.ToString(dr["EMPADDRESS_ZIPCODE"]);
                    model.empaddress_tel = Convert.ToString(dr["EMPADDRESS_TEL"]);
                    model.empaddress_email = Convert.ToString(dr["EMPADDRESS_EMAIL"]);
                    model.empaddress_line = Convert.ToString(dr["EMPADDRESS_LINE"]);
                    model.empaddress_facebook = Convert.ToString(dr["EMPADDRESS_FACEBOOK"]);
                    model.province_code = Convert.ToString(dr["PROVINCE_CODE"]);

                    model.modified_by = dr["MODIFIED_BY"].ToString();
                    model.modified_date = Convert.ToDateTime(dr["MODIFIED_DATE"]);

                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Empaddress.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TREmpaddress> getDataByFillter(string com, string emp)
        {
            string strCondition = "";

            strCondition += " AND COMPANY_CODE='" + com + "'";

            if (!emp.Equals(""))
                strCondition += " AND WORKER_CODE='" + emp + "'";
                        
            return this.getData(strCondition);
        }

        public List<cls_TREmpaddress> getDataMultipleEmp(string com, string worker)
        {
            string strCondition = " AND COMPANY_CODE='" + com + "'";
            strCondition += " AND WORKER_CODE IN (" + worker + ") ";

            return this.getData(strCondition);
        }

        public bool checkDataOld(string com, string emp, string type)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT EMPADDRESS_TYPE");
                obj_str.Append(" FROM HRM_TR_EMPADDRESS");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "' ");
                obj_str.Append(" AND WORKER_CODE='" + emp + "' ");
                obj_str.Append(" AND EMPADDRESS_TYPE='" + type + "' ");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empaddress.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }
                
        public bool delete(string com, string emp, string type)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_TR_EMPADDRESS");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "' ");
                obj_str.Append(" AND WORKER_CODE='" + emp + "' ");
                obj_str.Append(" AND EMPADDRESS_TYPE='" + type + "' ");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());
                

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Empaddress.delete)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(cls_TREmpaddress model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.company_code, model.worker_code, model.empaddress_type))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_EMPADDRESS");
                obj_str.Append(" (");
                obj_str.Append("COMPANY_CODE ");
                obj_str.Append(", WORKER_CODE ");
                obj_str.Append(", EMPADDRESS_TYPE ");
                obj_str.Append(", EMPADDRESS_NO ");
                obj_str.Append(", EMPADDRESS_MOO ");
                obj_str.Append(", EMPADDRESS_SOI ");
                obj_str.Append(", EMPADDRESS_ROAD ");
                obj_str.Append(", EMPADDRESS_TAMBON ");
                obj_str.Append(", EMPADDRESS_AMPHUR ");
                obj_str.Append(", EMPADDRESS_ZIPCODE ");
                obj_str.Append(", EMPADDRESS_TEL ");
                obj_str.Append(", EMPADDRESS_EMAIL ");
                obj_str.Append(", EMPADDRESS_LINE ");
                obj_str.Append(", EMPADDRESS_FACEBOOK ");
                obj_str.Append(", PROVINCE_CODE ");
                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@COMPANY_CODE ");
                obj_str.Append(", @WORKER_CODE ");
                obj_str.Append(", @EMPADDRESS_TYPE ");
                obj_str.Append(", @EMPADDRESS_NO ");
                obj_str.Append(", @EMPADDRESS_MOO ");
                obj_str.Append(", @EMPADDRESS_SOI ");
                obj_str.Append(", @EMPADDRESS_ROAD ");
                obj_str.Append(", @EMPADDRESS_TAMBON ");
                obj_str.Append(", @EMPADDRESS_AMPHUR ");
                obj_str.Append(", @EMPADDRESS_ZIPCODE ");
                obj_str.Append(", @EMPADDRESS_TEL ");
                obj_str.Append(", @EMPADDRESS_EMAIL ");
                obj_str.Append(", @EMPADDRESS_LINE ");
                obj_str.Append(", @EMPADDRESS_FACEBOOK ");
                obj_str.Append(", @PROVINCE_CODE ");
                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                obj_cmd.Parameters.Add("@EMPADDRESS_TYPE", SqlDbType.VarChar); obj_cmd.Parameters["@EMPADDRESS_TYPE"].Value = model.empaddress_type;
                obj_cmd.Parameters.Add("@EMPADDRESS_NO", SqlDbType.VarChar); obj_cmd.Parameters["@EMPADDRESS_NO"].Value = model.empaddress_no;
                obj_cmd.Parameters.Add("@EMPADDRESS_MOO", SqlDbType.VarChar); obj_cmd.Parameters["@EMPADDRESS_MOO"].Value = model.empaddress_moo;
                obj_cmd.Parameters.Add("@EMPADDRESS_SOI", SqlDbType.VarChar); obj_cmd.Parameters["@EMPADDRESS_SOI"].Value = model.empaddress_soi;
                obj_cmd.Parameters.Add("@EMPADDRESS_ROAD", SqlDbType.VarChar); obj_cmd.Parameters["@EMPADDRESS_ROAD"].Value = model.empaddress_road;
                obj_cmd.Parameters.Add("@EMPADDRESS_TAMBON", SqlDbType.VarChar); obj_cmd.Parameters["@EMPADDRESS_TAMBON"].Value = model.empaddress_tambon;
                obj_cmd.Parameters.Add("@EMPADDRESS_AMPHUR", SqlDbType.VarChar); obj_cmd.Parameters["@EMPADDRESS_AMPHUR"].Value = model.empaddress_amphur;
                obj_cmd.Parameters.Add("@EMPADDRESS_ZIPCODE", SqlDbType.VarChar); obj_cmd.Parameters["@EMPADDRESS_ZIPCODE"].Value = model.empaddress_zipcode;
                obj_cmd.Parameters.Add("@EMPADDRESS_TEL", SqlDbType.VarChar); obj_cmd.Parameters["@EMPADDRESS_TEL"].Value = model.empaddress_tel;
                obj_cmd.Parameters.Add("@EMPADDRESS_EMAIL", SqlDbType.VarChar); obj_cmd.Parameters["@EMPADDRESS_EMAIL"].Value = model.empaddress_email;
                obj_cmd.Parameters.Add("@EMPADDRESS_LINE", SqlDbType.VarChar); obj_cmd.Parameters["@EMPADDRESS_LINE"].Value = model.empaddress_line;
                obj_cmd.Parameters.Add("@EMPADDRESS_FACEBOOK", SqlDbType.VarChar); obj_cmd.Parameters["@EMPADDRESS_FACEBOOK"].Value = model.empaddress_facebook;
                obj_cmd.Parameters.Add("@PROVINCE_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@PROVINCE_CODE"].Value = model.province_code;

                obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@CREATED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                
            }
            catch (Exception ex)
            {              
                Message = "ERROR::(Empaddress.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_TREmpaddress model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_TR_EMPADDRESS SET ");

                obj_str.Append(" EMPADDRESS_NO=@EMPADDRESS_NO ");
                obj_str.Append(", EMPADDRESS_MOO=@EMPADDRESS_MOO ");
                obj_str.Append(", EMPADDRESS_SOI=@EMPADDRESS_SOI ");
                obj_str.Append(", EMPADDRESS_ROAD=@EMPADDRESS_ROAD ");
                obj_str.Append(", EMPADDRESS_TAMBON=@EMPADDRESS_TAMBON ");
                obj_str.Append(", EMPADDRESS_AMPHUR=@EMPADDRESS_AMPHUR ");
                obj_str.Append(", EMPADDRESS_ZIPCODE=@EMPADDRESS_ZIPCODE ");
                obj_str.Append(", EMPADDRESS_TEL=@EMPADDRESS_TEL ");
                obj_str.Append(", EMPADDRESS_EMAIL=@EMPADDRESS_EMAIL ");
                obj_str.Append(", EMPADDRESS_LINE=@EMPADDRESS_LINE ");
                obj_str.Append(", EMPADDRESS_FACEBOOK=@EMPADDRESS_FACEBOOK ");
                obj_str.Append(", PROVINCE_CODE=@PROVINCE_CODE ");

                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");
                
                obj_str.Append(" WHERE COMPANY_CODE=@COMPANY_CODE ");
                obj_str.Append(" AND WORKER_CODE=@WORKER_CODE ");
                obj_str.Append(" AND EMPADDRESS_TYPE=@EMPADDRESS_TYPE ");


                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@EMPADDRESS_NO", SqlDbType.VarChar); obj_cmd.Parameters["@EMPADDRESS_NO"].Value = model.empaddress_no;
                obj_cmd.Parameters.Add("@EMPADDRESS_MOO", SqlDbType.VarChar); obj_cmd.Parameters["@EMPADDRESS_MOO"].Value = model.empaddress_moo;
                obj_cmd.Parameters.Add("@EMPADDRESS_SOI", SqlDbType.VarChar); obj_cmd.Parameters["@EMPADDRESS_SOI"].Value = model.empaddress_soi;
                obj_cmd.Parameters.Add("@EMPADDRESS_ROAD", SqlDbType.VarChar); obj_cmd.Parameters["@EMPADDRESS_ROAD"].Value = model.empaddress_road;
                obj_cmd.Parameters.Add("@EMPADDRESS_TAMBON", SqlDbType.VarChar); obj_cmd.Parameters["@EMPADDRESS_TAMBON"].Value = model.empaddress_tambon;
                obj_cmd.Parameters.Add("@EMPADDRESS_AMPHUR", SqlDbType.VarChar); obj_cmd.Parameters["@EMPADDRESS_AMPHUR"].Value = model.empaddress_amphur;
                obj_cmd.Parameters.Add("@EMPADDRESS_ZIPCODE", SqlDbType.VarChar); obj_cmd.Parameters["@EMPADDRESS_ZIPCODE"].Value = model.empaddress_zipcode;
                obj_cmd.Parameters.Add("@EMPADDRESS_TEL", SqlDbType.VarChar); obj_cmd.Parameters["@EMPADDRESS_TEL"].Value = model.empaddress_tel;
                obj_cmd.Parameters.Add("@EMPADDRESS_EMAIL", SqlDbType.VarChar); obj_cmd.Parameters["@EMPADDRESS_EMAIL"].Value = model.empaddress_email;
                obj_cmd.Parameters.Add("@EMPADDRESS_LINE", SqlDbType.VarChar); obj_cmd.Parameters["@EMPADDRESS_LINE"].Value = model.empaddress_line;
                obj_cmd.Parameters.Add("@EMPADDRESS_FACEBOOK", SqlDbType.VarChar); obj_cmd.Parameters["@EMPADDRESS_FACEBOOK"].Value = model.empaddress_facebook;
                obj_cmd.Parameters.Add("@PROVINCE_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@PROVINCE_CODE"].Value = model.province_code;

                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = DateTime.Now;

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                obj_cmd.Parameters.Add("@EMPADDRESS_TYPE", SqlDbType.VarChar); obj_cmd.Parameters["@EMPADDRESS_TYPE"].Value = model.empaddress_type;


                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Empaddress.update)" + ex.ToString();
            }

            return blnResult;
        }

    }
}
