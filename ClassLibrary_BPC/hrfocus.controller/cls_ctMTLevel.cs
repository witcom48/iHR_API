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
    public class cls_ctMTLevel
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctMTLevel() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_MTLevel> getData(string condition)
        {
            List<cls_MTLevel> list_model = new List<cls_MTLevel>();
            cls_MTLevel model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("LEVEL_ID");
                obj_str.Append(", LEVEL_CODE");
                obj_str.Append(", ISNULL(LEVEL_NAME_TH, '') AS LEVEL_NAME_TH");
                obj_str.Append(", ISNULL(LEVEL_NAME_EN, '') AS LEVEL_NAME_EN");
                obj_str.Append(", COMPANY_CODE");
                obj_str.Append(", ISNULL(MODIFIED_BY, CREATED_BY) AS MODIFIED_BY");
                obj_str.Append(", ISNULL(MODIFIED_DATE, CREATED_DATE) AS MODIFIED_DATE");

                obj_str.Append(" FROM HRM_MT_LEVEL");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY LEVEL_CODE");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_MTLevel();

                    model.level_id = Convert.ToInt32(dr["LEVEL_ID"]);
                    model.level_code = dr["LEVEL_CODE"].ToString();
                    model.level_name_th = dr["LEVEL_NAME_TH"].ToString();
                    model.level_name_en = dr["LEVEL_NAME_EN"].ToString();

                    model.company_code = dr["COMPANY_CODE"].ToString();

                    model.modified_by = dr["MODIFIED_BY"].ToString();
                    model.modified_date = Convert.ToDateTime(dr["MODIFIED_DATE"]);
                                                                                                                      
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Level.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_MTLevel> getDataByFillter(string com, string id, string code)
        {
            string strCondition = "";

            strCondition += " AND COMPANY_CODE='" + com + "'";

            if (!id.Equals(""))
                strCondition += " AND LEVEL_ID='" + id + "'";

            if (!code.Equals(""))
                strCondition += " AND LEVEL_CODE='" + code + "'";
            
            return this.getData(strCondition);
        }

        public bool checkDataOld(string com, string code)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT LEVEL_ID");
                obj_str.Append(" FROM HRM_MT_LEVEL");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND LEVEL_CODE='" + code + "'");
                                                
                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Level.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public int getNextID()
        {
            int intResult = 1;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT MAX(LEVEL_ID) ");
                obj_str.Append(" FROM HRM_MT_LEVEL");                

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    intResult = Convert.ToInt32(dt.Rows[0][0]) + 1;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Level.getNextID)" + ex.ToString();
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

                obj_str.Append(" DELETE FROM HRM_MT_LEVEL");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND LEVEL_ID='" + id + "'");
                                              
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Level.delete)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(cls_MTLevel model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.company_code, model.level_code))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_MT_LEVEL");
                obj_str.Append(" (");
                obj_str.Append("LEVEL_ID ");
                obj_str.Append(", LEVEL_CODE ");
                obj_str.Append(", LEVEL_NAME_TH ");
                obj_str.Append(", LEVEL_NAME_EN ");
                obj_str.Append(", COMPANY_CODE ");
                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append(" @LEVEL_ID ");
                obj_str.Append(", @LEVEL_CODE ");
                obj_str.Append(", @LEVEL_NAME_TH ");
                obj_str.Append(", @LEVEL_NAME_EN ");
                obj_str.Append(", @COMPANY_CODE ");
                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");              
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@LEVEL_ID", SqlDbType.Int); obj_cmd.Parameters["@LEVEL_ID"].Value = this.getNextID();
                obj_cmd.Parameters.Add("@LEVEL_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@LEVEL_CODE"].Value = model.level_code;
                obj_cmd.Parameters.Add("@LEVEL_NAME_TH", SqlDbType.VarChar); obj_cmd.Parameters["@LEVEL_NAME_TH"].Value = model.level_name_th;
                obj_cmd.Parameters.Add("@LEVEL_NAME_EN", SqlDbType.VarChar); obj_cmd.Parameters["@LEVEL_NAME_EN"].Value = model.level_name_en;
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
                Message = "ERROR::(Level.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_MTLevel model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_MT_LEVEL SET ");

                obj_str.Append(" LEVEL_CODE=@LEVEL_CODE ");
                obj_str.Append(", LEVEL_NAME_TH=@LEVEL_NAME_TH ");
                obj_str.Append(", LEVEL_NAME_EN=@LEVEL_NAME_EN ");
                obj_str.Append(", COMPANY_CODE=@COMPANY_CODE ");
                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");
                obj_str.Append(", FLAG=@FLAG ");


                obj_str.Append(" WHERE LEVEL_ID=@LEVEL_ID ");

                
                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
                                
                obj_cmd.Parameters.Add("@LEVEL_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@LEVEL_CODE"].Value = model.level_code;
                obj_cmd.Parameters.Add("@LEVEL_NAME_TH", SqlDbType.VarChar); obj_cmd.Parameters["@LEVEL_NAME_TH"].Value = model.level_name_th;
                obj_cmd.Parameters.Add("@LEVEL_NAME_EN", SqlDbType.VarChar); obj_cmd.Parameters["@LEVEL_NAME_EN"].Value = model.level_name_en;
                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;

                obj_cmd.Parameters.Add("@LEVEL_ID", SqlDbType.Int); obj_cmd.Parameters["@LEVEL_ID"].Value = model.level_id;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Level.update)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
