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
    public class cls_ctMTDep
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctMTDep() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_MTDep> getData(string condition)
        {
            List<cls_MTDep> list_model = new List<cls_MTDep>();
            cls_MTDep model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("DEP_ID");
                obj_str.Append(", DEP_CODE");

                obj_str.Append(", ISNULL(DEP_NAME_TH, '') AS DEP_NAME_TH");
                obj_str.Append(", ISNULL(DEP_NAME_EN, '') AS DEP_NAME_EN");

                obj_str.Append(", DEP_PARENT");
                obj_str.Append(", DEP_LEVEL");

                obj_str.Append(", COMPANY_CODE");
                obj_str.Append(", ISNULL(MODIFIED_BY, CREATED_BY) AS MODIFIED_BY");
                obj_str.Append(", ISNULL(MODIFIED_DATE, CREATED_DATE) AS MODIFIED_DATE");

                obj_str.Append(" FROM HRM_MT_DEP");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY DEP_CODE");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_MTDep();

                    model.dep_id = Convert.ToInt32(dr["DEP_ID"]);
                    model.dep_code = dr["DEP_CODE"].ToString();
                    model.dep_name_th = dr["DEP_NAME_TH"].ToString();
                    model.dep_name_en = dr["DEP_NAME_EN"].ToString();

                    model.dep_parent = dr["DEP_PARENT"].ToString();
                    model.dep_level = dr["DEP_LEVEL"].ToString();

                    model.company_code = dr["COMPANY_CODE"].ToString();

                    model.modified_by = dr["MODIFIED_BY"].ToString();
                    model.modified_date = Convert.ToDateTime(dr["MODIFIED_DATE"]);
                                                                                                                      
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Dep.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_MTDep> getDataByFillter(string com, string level, string parent, string id, string code)
        {
            string strCondition = "";

            strCondition += " AND COMPANY_CODE='" + com + "'";

            if (!id.Equals(""))
                strCondition += " AND DEP_ID='" + id + "'";

            if (!level.Equals(""))
                strCondition += " AND DEP_LEVEL='" + level + "'";

            if (!parent.Equals(""))
                strCondition += " AND DEP_PARENT='" + parent + "'";

            if (!code.Equals(""))
                strCondition += " AND DEP_CODE='" + code + "'";
            
            return this.getData(strCondition);
        }

        public bool checkDataOld(string com, string code)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT DEP_ID");
                obj_str.Append(" FROM HRM_MT_DEP");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND DEP_CODE='" + code + "'");
                                                
                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Dep.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public int getNextID()
        {
            int intResult = 1;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT MAX(DEP_ID) ");
                obj_str.Append(" FROM HRM_MT_DEP");                

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    intResult = Convert.ToInt32(dt.Rows[0][0]) + 1;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Dep.getNextID)" + ex.ToString();
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

                obj_str.Append(" DELETE FROM HRM_MT_DEP");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND DEP_ID='" + id + "'");
                                              
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Dep.delete)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(cls_MTDep model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.company_code, model.dep_code))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_MT_DEP");
                obj_str.Append(" (");
                obj_str.Append("DEP_ID ");
                obj_str.Append(", DEP_CODE ");
                obj_str.Append(", DEP_NAME_TH ");
                obj_str.Append(", DEP_NAME_EN ");

                obj_str.Append(", DEP_PARENT ");
                obj_str.Append(", DEP_LEVEL ");

                obj_str.Append(", COMPANY_CODE ");
                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append(" @DEP_ID ");
                obj_str.Append(", @DEP_CODE ");
                obj_str.Append(", @DEP_NAME_TH ");
                obj_str.Append(", @DEP_NAME_EN ");

                obj_str.Append(", @DEP_PARENT ");
                obj_str.Append(", @DEP_LEVEL ");

                obj_str.Append(", @COMPANY_CODE ");
                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");              
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@DEP_ID", SqlDbType.Int); obj_cmd.Parameters["@DEP_ID"].Value = this.getNextID();
                obj_cmd.Parameters.Add("@DEP_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@DEP_CODE"].Value = model.dep_code;
                obj_cmd.Parameters.Add("@DEP_NAME_TH", SqlDbType.VarChar); obj_cmd.Parameters["@DEP_NAME_TH"].Value = model.dep_name_th;
                obj_cmd.Parameters.Add("@DEP_NAME_EN", SqlDbType.VarChar); obj_cmd.Parameters["@DEP_NAME_EN"].Value = model.dep_name_en;

                obj_cmd.Parameters.Add("@DEP_PARENT", SqlDbType.VarChar); obj_cmd.Parameters["@DEP_PARENT"].Value = model.dep_parent;
                obj_cmd.Parameters.Add("@DEP_LEVEL", SqlDbType.VarChar); obj_cmd.Parameters["@DEP_LEVEL"].Value = model.dep_level;

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
                Message = "ERROR::(Dep.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_MTDep model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_MT_DEP SET ");

                obj_str.Append(" DEP_CODE=@DEP_CODE ");
                obj_str.Append(", DEP_NAME_TH=@DEP_NAME_TH ");
                obj_str.Append(", DEP_NAME_EN=@DEP_NAME_EN ");

                obj_str.Append(", DEP_PARENT=@DEP_PARENT ");
                obj_str.Append(", DEP_LEVEL=@DEP_LEVEL ");

                obj_str.Append(", COMPANY_CODE=@COMPANY_CODE ");
                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");
                obj_str.Append(", FLAG=@FLAG ");


                obj_str.Append(" WHERE DEP_ID=@DEP_ID ");

                
                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                
                obj_cmd.Parameters.Add("@DEP_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@DEP_CODE"].Value = model.dep_code;
                obj_cmd.Parameters.Add("@DEP_NAME_TH", SqlDbType.VarChar); obj_cmd.Parameters["@DEP_NAME_TH"].Value = model.dep_name_th;
                obj_cmd.Parameters.Add("@DEP_NAME_EN", SqlDbType.VarChar); obj_cmd.Parameters["@DEP_NAME_EN"].Value = model.dep_name_en;

                obj_cmd.Parameters.Add("@DEP_PARENT", SqlDbType.VarChar); obj_cmd.Parameters["@DEP_PARENT"].Value = model.dep_parent;
                obj_cmd.Parameters.Add("@DEP_LEVEL", SqlDbType.VarChar); obj_cmd.Parameters["@DEP_LEVEL"].Value = model.dep_level;

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;

                obj_cmd.Parameters.Add("@DEP_ID", SqlDbType.Int); obj_cmd.Parameters["@DEP_ID"].Value = model.dep_id;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Dep.update)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
