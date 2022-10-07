﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary_BPC.hrfocus.model;
using System.Data.SqlClient;
using System.Data;

namespace ClassLibrary_BPC.hrfocus.controller
{
    public class cls_ctTRComaddress
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTRComaddress() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TRComaddress> getData(string condition)
        {
            List<cls_TRComaddress> list_model = new List<cls_TRComaddress>();
            cls_TRComaddress model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("COMPANY_CODE");
                obj_str.Append(", COMBRANCH_CODE");
                obj_str.Append(", COMADDRESS_TYPE");

                obj_str.Append(", ISNULL(COMADDRESS_NO, '') AS COMADDRESS_NO");
                obj_str.Append(", ISNULL(COMADDRESS_MOO, '') AS COMADDRESS_MOO");
                obj_str.Append(", ISNULL(COMADDRESS_SOI, '') AS COMADDRESS_SOI");
                obj_str.Append(", ISNULL(COMADDRESS_ROAD, '') AS COMADDRESS_ROAD");
                obj_str.Append(", ISNULL(COMADDRESS_TAMBON, '') AS COMADDRESS_TAMBON");
                obj_str.Append(", ISNULL(COMADDRESS_AMPHUR, '') AS COMADDRESS_AMPHUR");
                obj_str.Append(", ISNULL(COMADDRESS_ZIPCODE, '') AS COMADDRESS_ZIPCODE");
                obj_str.Append(", ISNULL(COMADDRESS_TEL, '') AS COMADDRESS_TEL");
                obj_str.Append(", ISNULL(COMADDRESS_EMAIL, '') AS COMADDRESS_EMAIL");
                obj_str.Append(", ISNULL(COMADDRESS_LINE, '') AS COMADDRESS_LINE");
                obj_str.Append(", ISNULL(COMADDRESS_FACEBOOK, '') AS COMADDRESS_FACEBOOK");
                obj_str.Append(", ISNULL(PROVINCE_CODE, '') AS PROVINCE_CODE");

                obj_str.Append(", ISNULL(MODIFIED_BY, CREATED_BY) AS MODIFIED_BY");
                obj_str.Append(", ISNULL(MODIFIED_DATE, CREATED_DATE) AS MODIFIED_DATE");

                obj_str.Append(" FROM HRM_TR_COMADDRESS");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY COMPANY_CODE, COMBRANCH_CODE, COMADDRESS_TYPE");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TRComaddress();

                    model.company_code = Convert.ToString(dr["COMPANY_CODE"]);
                    model.combranch_code = Convert.ToString(dr["COMBRANCH_CODE"]);
                    model.comaddress_type = Convert.ToString(dr["COMADDRESS_TYPE"]);
                    model.comaddress_no = Convert.ToString(dr["COMADDRESS_NO"]);
                    model.comaddress_moo = Convert.ToString(dr["COMADDRESS_MOO"]);
                    model.comaddress_soi = Convert.ToString(dr["COMADDRESS_SOI"]);
                    model.comaddress_road = Convert.ToString(dr["COMADDRESS_ROAD"]);
                    model.comaddress_tambon = Convert.ToString(dr["COMADDRESS_TAMBON"]);
                    model.comaddress_amphur = Convert.ToString(dr["COMADDRESS_AMPHUR"]);
                    model.comaddress_zipcode = Convert.ToString(dr["COMADDRESS_ZIPCODE"]);
                    model.comaddress_tel = Convert.ToString(dr["COMADDRESS_TEL"]);
                    model.comaddress_email = Convert.ToString(dr["COMADDRESS_EMAIL"]);
                    model.comaddress_line = Convert.ToString(dr["COMADDRESS_LINE"]);
                    model.comaddress_facebook = Convert.ToString(dr["COMADDRESS_FACEBOOK"]);
                    model.province_code = Convert.ToString(dr["PROVINCE_CODE"]);

                    model.modified_by = dr["MODIFIED_BY"].ToString();
                    model.modified_date = Convert.ToDateTime(dr["MODIFIED_DATE"]);

                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Comaddress.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TRComaddress> getDataByFillter(string com, string branch, string type)
        {
            string strCondition = "";

            strCondition += " AND COMPANY_CODE='" + com + "'";

            if (!branch.Equals(""))
                strCondition += " AND COMBRANCH_CODE='" + branch + "'";

            if (!type.Equals(""))
                strCondition += " AND COMADDRESS_TYPE='" + type + "'";
                        
            return this.getData(strCondition);
        }

        public bool checkDataOld(string com, string branch, string type)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT COMPANY_CODE");
                obj_str.Append(" FROM HRM_TR_COMADDRESS");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "' ");
                obj_str.Append(" AND COMBRANCH_CODE='" + branch + "' ");
                obj_str.Append(" AND COMADDRESS_TYPE='" + type + "' ");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Comaddress.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public bool delete(string com, string branch, string type)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_TR_COMADDRESS");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "' ");
                obj_str.Append(" AND COMBRANCH_CODE='" + branch + "' ");
                obj_str.Append(" AND COMADDRESS_TYPE='" + type + "' ");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());                

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Comaddress.delete)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(cls_TRComaddress model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.company_code, model.combranch_code, model.comaddress_type))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_COMADDRESS");
                obj_str.Append(" (");
                obj_str.Append("COMPANY_CODE ");
                obj_str.Append(", COMBRANCH_CODE ");
                obj_str.Append(", COMADDRESS_TYPE ");
                obj_str.Append(", COMADDRESS_NO ");
                obj_str.Append(", COMADDRESS_MOO ");
                obj_str.Append(", COMADDRESS_SOI ");
                obj_str.Append(", COMADDRESS_ROAD ");
                obj_str.Append(", COMADDRESS_TAMBON ");
                obj_str.Append(", COMADDRESS_AMPHUR ");
                obj_str.Append(", COMADDRESS_ZIPCODE ");
                obj_str.Append(", COMADDRESS_TEL ");
                obj_str.Append(", COMADDRESS_EMAIL ");
                obj_str.Append(", COMADDRESS_LINE ");
                obj_str.Append(", COMADDRESS_FACEBOOK ");
                obj_str.Append(", PROVINCE_CODE ");
                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@COMPANY_CODE ");
                obj_str.Append(", @COMBRANCH_CODE ");
                obj_str.Append(", @COMADDRESS_TYPE ");
                obj_str.Append(", @COMADDRESS_NO ");
                obj_str.Append(", @COMADDRESS_MOO ");
                obj_str.Append(", @COMADDRESS_SOI ");
                obj_str.Append(", @COMADDRESS_ROAD ");
                obj_str.Append(", @COMADDRESS_TAMBON ");
                obj_str.Append(", @COMADDRESS_AMPHUR ");
                obj_str.Append(", @COMADDRESS_ZIPCODE ");
                obj_str.Append(", @COMADDRESS_TEL ");
                obj_str.Append(", @COMADDRESS_EMAIL ");
                obj_str.Append(", @COMADDRESS_LINE ");
                obj_str.Append(", @COMADDRESS_FACEBOOK ");
                obj_str.Append(", @PROVINCE_CODE ");
                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@COMBRANCH_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMBRANCH_CODE"].Value = model.combranch_code;
                obj_cmd.Parameters.Add("@COMADDRESS_TYPE", SqlDbType.VarChar); obj_cmd.Parameters["@COMADDRESS_TYPE"].Value = model.comaddress_type;
                obj_cmd.Parameters.Add("@COMADDRESS_NO", SqlDbType.VarChar); obj_cmd.Parameters["@COMADDRESS_NO"].Value = model.comaddress_no;
                obj_cmd.Parameters.Add("@COMADDRESS_MOO", SqlDbType.VarChar); obj_cmd.Parameters["@COMADDRESS_MOO"].Value = model.comaddress_moo;
                obj_cmd.Parameters.Add("@COMADDRESS_SOI", SqlDbType.VarChar); obj_cmd.Parameters["@COMADDRESS_SOI"].Value = model.comaddress_soi;
                obj_cmd.Parameters.Add("@COMADDRESS_ROAD", SqlDbType.VarChar); obj_cmd.Parameters["@COMADDRESS_ROAD"].Value = model.comaddress_road;
                obj_cmd.Parameters.Add("@COMADDRESS_TAMBON", SqlDbType.VarChar); obj_cmd.Parameters["@COMADDRESS_TAMBON"].Value = model.comaddress_tambon;
                obj_cmd.Parameters.Add("@COMADDRESS_AMPHUR", SqlDbType.VarChar); obj_cmd.Parameters["@COMADDRESS_AMPHUR"].Value = model.comaddress_amphur;
                obj_cmd.Parameters.Add("@COMADDRESS_ZIPCODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMADDRESS_ZIPCODE"].Value = model.comaddress_zipcode;
                obj_cmd.Parameters.Add("@COMADDRESS_TEL", SqlDbType.VarChar); obj_cmd.Parameters["@COMADDRESS_TEL"].Value = model.comaddress_tel;
                obj_cmd.Parameters.Add("@COMADDRESS_EMAIL", SqlDbType.VarChar); obj_cmd.Parameters["@COMADDRESS_EMAIL"].Value = model.comaddress_email;
                obj_cmd.Parameters.Add("@COMADDRESS_LINE", SqlDbType.VarChar); obj_cmd.Parameters["@COMADDRESS_LINE"].Value = model.comaddress_line;
                obj_cmd.Parameters.Add("@COMADDRESS_FACEBOOK", SqlDbType.VarChar); obj_cmd.Parameters["@COMADDRESS_FACEBOOK"].Value = model.comaddress_facebook;
                obj_cmd.Parameters.Add("@PROVINCE_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@PROVINCE_CODE"].Value = model.province_code;

                obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@CREATED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {              
                Message = "ERROR::(Comaddress.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_TRComaddress model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_TR_COMADDRESS SET ");

                obj_str.Append(" COMADDRESS_NO=@COMADDRESS_NO ");
                obj_str.Append(", COMADDRESS_MOO=@COMADDRESS_MOO ");
                obj_str.Append(", COMADDRESS_SOI=@COMADDRESS_SOI ");
                obj_str.Append(", COMADDRESS_ROAD=@COMADDRESS_ROAD ");
                obj_str.Append(", COMADDRESS_TAMBON=@COMADDRESS_TAMBON ");
                obj_str.Append(", COMADDRESS_AMPHUR=@COMADDRESS_AMPHUR ");
                obj_str.Append(", COMADDRESS_ZIPCODE=@COMADDRESS_ZIPCODE ");
                obj_str.Append(", COMADDRESS_TEL=@COMADDRESS_TEL ");
                obj_str.Append(", COMADDRESS_EMAIL=@COMADDRESS_EMAIL ");
                obj_str.Append(", COMADDRESS_LINE=@COMADDRESS_LINE ");
                obj_str.Append(", COMADDRESS_FACEBOOK=@COMADDRESS_FACEBOOK ");
                obj_str.Append(", PROVINCE_CODE=@PROVINCE_CODE ");

                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");
                
                obj_str.Append(" WHERE COMPANY_CODE=@COMPANY_CODE ");
                obj_str.Append(" AND COMBRANCH_CODE=@COMBRANCH_CODE ");
                obj_str.Append(" AND COMADDRESS_TYPE=@COMADDRESS_TYPE ");


                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@COMADDRESS_NO", SqlDbType.VarChar); obj_cmd.Parameters["@COMADDRESS_NO"].Value = model.comaddress_no;
                obj_cmd.Parameters.Add("@COMADDRESS_MOO", SqlDbType.VarChar); obj_cmd.Parameters["@COMADDRESS_MOO"].Value = model.comaddress_moo;
                obj_cmd.Parameters.Add("@COMADDRESS_SOI", SqlDbType.VarChar); obj_cmd.Parameters["@COMADDRESS_SOI"].Value = model.comaddress_soi;
                obj_cmd.Parameters.Add("@COMADDRESS_ROAD", SqlDbType.VarChar); obj_cmd.Parameters["@COMADDRESS_ROAD"].Value = model.comaddress_road;
                obj_cmd.Parameters.Add("@COMADDRESS_TAMBON", SqlDbType.VarChar); obj_cmd.Parameters["@COMADDRESS_TAMBON"].Value = model.comaddress_tambon;
                obj_cmd.Parameters.Add("@COMADDRESS_AMPHUR", SqlDbType.VarChar); obj_cmd.Parameters["@COMADDRESS_AMPHUR"].Value = model.comaddress_amphur;
                obj_cmd.Parameters.Add("@COMADDRESS_ZIPCODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMADDRESS_ZIPCODE"].Value = model.comaddress_zipcode;
                obj_cmd.Parameters.Add("@COMADDRESS_TEL", SqlDbType.VarChar); obj_cmd.Parameters["@COMADDRESS_TEL"].Value = model.comaddress_tel;
                obj_cmd.Parameters.Add("@COMADDRESS_EMAIL", SqlDbType.VarChar); obj_cmd.Parameters["@COMADDRESS_EMAIL"].Value = model.comaddress_email;
                obj_cmd.Parameters.Add("@COMADDRESS_LINE", SqlDbType.VarChar); obj_cmd.Parameters["@COMADDRESS_LINE"].Value = model.comaddress_line;
                obj_cmd.Parameters.Add("@COMADDRESS_FACEBOOK", SqlDbType.VarChar); obj_cmd.Parameters["@COMADDRESS_FACEBOOK"].Value = model.comaddress_facebook;
                obj_cmd.Parameters.Add("@PROVINCE_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@PROVINCE_CODE"].Value = model.province_code;

                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = DateTime.Now;

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@COMBRANCH_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMBRANCH_CODE"].Value = model.combranch_code;
                obj_cmd.Parameters.Add("@COMADDRESS_TYPE", SqlDbType.VarChar); obj_cmd.Parameters["@COMADDRESS_TYPE"].Value = model.comaddress_type;


                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Comaddress.update)" + ex.ToString();
            }

            return blnResult;
        }

    }
}
