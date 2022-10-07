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
    public class cls_ctMTYear
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctMTYear() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_MTYear> getData(string condition)
        {
            List<cls_MTYear> list_model = new List<cls_MTYear>();
            cls_MTYear model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("YEAR_ID");
                obj_str.Append(", YEAR_CODE");
                obj_str.Append(", ISNULL(YEAR_NAME_TH, '') AS YEAR_NAME_TH");
                obj_str.Append(", ISNULL(YEAR_NAME_EN, '') AS YEAR_NAME_EN");

                obj_str.Append(", YEAR_FROMDATE");
                obj_str.Append(", YEAR_TODATE");
                obj_str.Append(", YEAR_GROUP");

                obj_str.Append(", COMPANY_CODE");

                obj_str.Append(", ISNULL(MODIFIED_BY, CREATED_BY) AS MODIFIED_BY");
                obj_str.Append(", ISNULL(MODIFIED_DATE, CREATED_DATE) AS MODIFIED_DATE");

                obj_str.Append(" FROM HRM_MT_YEAR");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY YEAR_CODE");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_MTYear();

                    model.year_id = Convert.ToInt32(dr["YEAR_ID"]);
                    model.year_code = dr["YEAR_CODE"].ToString();

                    model.year_fromdate = Convert.ToDateTime(dr["YEAR_FROMDATE"]);
                    model.year_todate = Convert.ToDateTime(dr["YEAR_TODATE"]);

                    model.year_name_th = dr["YEAR_NAME_TH"].ToString();
                    model.year_name_en = dr["YEAR_NAME_EN"].ToString();

                    model.year_group = dr["YEAR_GROUP"].ToString();

                    model.company_code = dr["COMPANY_CODE"].ToString();

                    model.modified_by = dr["MODIFIED_BY"].ToString();
                    model.modified_date = Convert.ToDateTime(dr["MODIFIED_DATE"]);
                                                                                                                      
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Year.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_MTYear> getDataByFillter(string com, string group, string id, string code)
        {
            string strCondition = "";

            strCondition += " AND COMPANY_CODE='" + com + "'";

            if (!group.Equals(""))
                strCondition += " AND YEAR_GROUP='" + group + "'";

            if (!id.Equals(""))
                strCondition += " AND YEAR_ID='" + id + "'";

            if (!code.Equals(""))
                strCondition += " AND YEAR_CODE='" + code + "'";
            
            return this.getData(strCondition);
        }

        public bool checkDataOld(string com, string group, string code)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT YEAR_ID");
                obj_str.Append(" FROM HRM_MT_YEAR");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND YEAR_GROUP='" + group + "'");
                obj_str.Append(" AND YEAR_CODE='" + code + "'");
                                                
                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Year.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public int getNextID()
        {
            int intResult = 1;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT MAX(YEAR_ID) ");
                obj_str.Append(" FROM HRM_MT_YEAR");                

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    intResult = Convert.ToInt32(dt.Rows[0][0]) + 1;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Year.getNextID)" + ex.ToString();
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

                obj_str.Append(" DELETE FROM HRM_MT_YEAR");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND YEAR_ID='" + id + "'");
                                              
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Year.delete)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(cls_MTYear model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.company_code, model.year_group, model.year_code))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_MT_YEAR");
                obj_str.Append(" (");
                obj_str.Append("COMPANY_CODE ");
                obj_str.Append(", YEAR_ID ");
                obj_str.Append(", YEAR_CODE ");
                obj_str.Append(", YEAR_NAME_TH ");
                obj_str.Append(", YEAR_NAME_EN ");
                obj_str.Append(", YEAR_FROMDATE ");
                obj_str.Append(", YEAR_TODATE ");
                obj_str.Append(", YEAR_GROUP ");
                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@COMPANY_CODE ");
                obj_str.Append(", @YEAR_ID ");
                obj_str.Append(", @YEAR_CODE ");
                obj_str.Append(", @YEAR_NAME_TH ");
                obj_str.Append(", @YEAR_NAME_EN ");
                obj_str.Append(", @YEAR_FROMDATE ");
                obj_str.Append(", @YEAR_TODATE ");
                obj_str.Append(", @YEAR_GROUP ");
                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");        
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@YEAR_ID", SqlDbType.Int); obj_cmd.Parameters["@YEAR_ID"].Value = this.getNextID();
                obj_cmd.Parameters.Add("@YEAR_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@YEAR_CODE"].Value = model.year_code;
                obj_cmd.Parameters.Add("@YEAR_NAME_TH", SqlDbType.VarChar); obj_cmd.Parameters["@YEAR_NAME_TH"].Value = model.year_name_th;
                obj_cmd.Parameters.Add("@YEAR_NAME_EN", SqlDbType.VarChar); obj_cmd.Parameters["@YEAR_NAME_EN"].Value = model.year_name_en;
                obj_cmd.Parameters.Add("@YEAR_FROMDATE", SqlDbType.DateTime); obj_cmd.Parameters["@YEAR_FROMDATE"].Value = model.year_fromdate;
                obj_cmd.Parameters.Add("@YEAR_TODATE", SqlDbType.DateTime); obj_cmd.Parameters["@YEAR_TODATE"].Value = model.year_todate;
                obj_cmd.Parameters.Add("@YEAR_GROUP", SqlDbType.VarChar); obj_cmd.Parameters["@YEAR_GROUP"].Value = model.year_group;
                obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@CREATED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;
     
                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();
                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Year.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_MTYear model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_MT_YEAR SET ");

                obj_str.Append(" YEAR_CODE=@YEAR_CODE ");
                obj_str.Append(", YEAR_NAME_TH=@YEAR_NAME_TH ");
                obj_str.Append(", YEAR_NAME_EN=@YEAR_NAME_EN ");

                obj_str.Append(", YEAR_FROMDATE=@YEAR_FROMDATE ");
                obj_str.Append(", YEAR_TODATE=@YEAR_TODATE ");

                obj_str.Append(", YEAR_GROUP=@YEAR_GROUP ");
                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");
               
                obj_str.Append(" WHERE YEAR_ID=@YEAR_ID ");
                
                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@YEAR_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@YEAR_CODE"].Value = model.year_code;
                obj_cmd.Parameters.Add("@YEAR_NAME_TH", SqlDbType.VarChar); obj_cmd.Parameters["@YEAR_NAME_TH"].Value = model.year_name_th;
                obj_cmd.Parameters.Add("@YEAR_NAME_EN", SqlDbType.VarChar); obj_cmd.Parameters["@YEAR_NAME_EN"].Value = model.year_name_en;
                obj_cmd.Parameters.Add("@YEAR_FROMDATE", SqlDbType.DateTime); obj_cmd.Parameters["@YEAR_FROMDATE"].Value = model.year_fromdate;
                obj_cmd.Parameters.Add("@YEAR_TODATE", SqlDbType.DateTime); obj_cmd.Parameters["@YEAR_TODATE"].Value = model.year_todate;
                obj_cmd.Parameters.Add("@YEAR_GROUP", SqlDbType.VarChar); obj_cmd.Parameters["@YEAR_GROUP"].Value = model.year_group;

                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = DateTime.Now;

                obj_cmd.Parameters.Add("@YEAR_ID", SqlDbType.Int); obj_cmd.Parameters["@YEAR_ID"].Value = model.year_id;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Year.update)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
