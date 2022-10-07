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
    public class cls_ctMTFamily
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctMTFamily() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_MTFamily> getData(string condition)
        {
            List<cls_MTFamily> list_model = new List<cls_MTFamily>();
            cls_MTFamily model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("FAMILY_ID");
                obj_str.Append(", FAMILY_CODE");
                obj_str.Append(", ISNULL(FAMILY_NAME_TH, '') AS FAMILY_NAME_TH");
                obj_str.Append(", ISNULL(FAMILY_NAME_EN, '') AS FAMILY_NAME_EN");        
                obj_str.Append(", ISNULL(MODIFIED_BY, CREATED_BY) AS MODIFIED_BY");
                obj_str.Append(", ISNULL(MODIFIED_DATE, CREATED_DATE) AS MODIFIED_DATE");
                
                obj_str.Append(" FROM HRM_MT_FAMILY");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY FAMILY_CODE");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_MTFamily();

                    model.family_id = Convert.ToInt32(dr["FAMILY_ID"]);
                    model.family_code = dr["FAMILY_CODE"].ToString();
                    model.family_name_th = dr["FAMILY_NAME_TH"].ToString();
                    model.family_name_en = dr["FAMILY_NAME_EN"].ToString();
                    model.modified_by = dr["MODIFIED_BY"].ToString();
                    model.modified_date = Convert.ToDateTime(dr["MODIFIED_DATE"]);
                                                                                                                      
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(FAMILY.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_MTFamily> getDataByFillter(string id, string code)
        {
            string strCondition = "";

            if (!id.Equals(""))
                strCondition += " AND FAMILY_ID='" + id + "'";

            if (!code.Equals(""))
                strCondition += " AND FAMILY_CODE='" + code + "'";
            
            return this.getData(strCondition);
        }

        public bool checkDataOld(string code)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT FAMILY_ID");
                obj_str.Append(" FROM HRM_MT_FAMILY");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND FAMILY_CODE='" + code + "'");
                                                
                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(FAMILY.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public int getNextID()
        {
            int intResult = 1;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT MAX(FAMILY_ID) ");
                obj_str.Append(" FROM HRM_MT_FAMILY");             

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    intResult = Convert.ToInt32(dt.Rows[0][0]) + 1;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(FAMILY.getNextID)" + ex.ToString();
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

                obj_str.Append(" DELETE FROM HRM_MT_FAMILY");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND FAMILY_ID='" + id + "'");
                                              
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(FAMILY.delete)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(cls_MTFamily model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.family_code))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_MT_FAMILY");
                obj_str.Append(" (");
                obj_str.Append("FAMILY_ID ");
                obj_str.Append(", FAMILY_CODE ");
                obj_str.Append(", FAMILY_NAME_TH ");
                obj_str.Append(", FAMILY_NAME_EN ");
                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append(" @FAMILY_ID ");
                obj_str.Append(", @FAMILY_CODE ");
                obj_str.Append(", @FAMILY_NAME_TH ");
                obj_str.Append(", @FAMILY_NAME_EN ");
                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");              
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@FAMILY_ID", SqlDbType.Int); obj_cmd.Parameters["@FAMILY_ID"].Value = this.getNextID();
                obj_cmd.Parameters.Add("@FAMILY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@FAMILY_CODE"].Value = model.family_code;
                obj_cmd.Parameters.Add("@FAMILY_NAME_TH", SqlDbType.VarChar); obj_cmd.Parameters["@FAMILY_NAME_TH"].Value = model.family_name_th;
                obj_cmd.Parameters.Add("@FAMILY_NAME_EN", SqlDbType.VarChar); obj_cmd.Parameters["@FAMILY_NAME_EN"].Value = model.family_name_en;
                obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@CREATED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;
     
                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();
                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(FAMILY.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_MTFamily model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_MT_FAMILY SET ");

                obj_str.Append(" FAMILY_CODE=@FAMILY_CODE ");
                obj_str.Append(", FAMILY_NAME_TH=@FAMILY_NAME_TH ");
                obj_str.Append(", FAMILY_NAME_EN=@FAMILY_NAME_EN ");
                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");
                obj_str.Append(", FLAG=@FLAG ");
                
                obj_str.Append(" WHERE FAMILY_ID=@FAMILY_ID ");
                                
                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@FAMILY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@FAMILY_CODE"].Value = model.family_code;
                obj_cmd.Parameters.Add("@FAMILY_NAME_TH", SqlDbType.VarChar); obj_cmd.Parameters["@FAMILY_NAME_TH"].Value = model.family_name_th;
                obj_cmd.Parameters.Add("@FAMILY_NAME_EN", SqlDbType.VarChar); obj_cmd.Parameters["@FAMILY_NAME_EN"].Value = model.family_name_en;
                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;

                obj_cmd.Parameters.Add("@FAMILY_ID", SqlDbType.Int); obj_cmd.Parameters["@FAMILY_ID"].Value = model.family_id;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(FAMILY.update)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
