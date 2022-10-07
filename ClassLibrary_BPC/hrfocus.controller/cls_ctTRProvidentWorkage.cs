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
    public class cls_ctTRProvidentWorkage
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTRProvidentWorkage() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TRProvidentWorkage> getData(string condition)
        {
            List<cls_TRProvidentWorkage> list_model = new List<cls_TRProvidentWorkage>();
            cls_TRProvidentWorkage model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("COMPANY_CODE");               
                obj_str.Append(", PROVIDENT_CODE");
                obj_str.Append(", WORKAGE_FROM");
                obj_str.Append(", WORKAGE_TO");
                obj_str.Append(", RATE_EMP");
                obj_str.Append(", RATE_COM");

                obj_str.Append(" FROM HRM_TR_PROVIDENT_WORKAGE");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY COMPANY_CODE, PROVIDENT_CODE, WORKAGE_FROM");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TRProvidentWorkage();

                    model.company_code = Convert.ToString(dr["COMPANY_CODE"]);                    
                    model.provident_code = Convert.ToString(dr["PROVIDENT_CODE"]);
                    model.workage_from = Convert.ToDouble(dr["WORKAGE_FROM"]);
                    model.workage_to = Convert.ToDouble(dr["WORKAGE_TO"]);
                    model.rate_emp = Convert.ToDouble(dr["RATE_EMP"]);
                    model.rate_com = Convert.ToDouble(dr["RATE_COM"]);

                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(ProvidentWorkage.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TRProvidentWorkage> getDataByFillter(string com, string code)
        {
            string strCondition = "";

            strCondition += " AND COMPANY_CODE='" + com + "'";
            
            if (!code.Equals(""))
                strCondition += " AND PROVIDENT_CODE='" + code + "'";
            
            return this.getData(strCondition);
        }

        public bool checkDataOld(string com, string code, double workagefrom)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT PROVIDENT_ID");
                obj_str.Append(" FROM HRM_TR_PROVIDENT_WORKAGE");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND PROVIDENT_CODE='" + code + "'");
                obj_str.Append(" AND WORKAGE_FROM='" + workagefrom + "'");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(ProvidentWorkage.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }
        
        public bool delete(string com, string code)
        {
            bool blnResult = true;
            try
            {
                //cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_TR_PROVIDENT_WORKAGE");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND COMPANY_CODE ='" + com + "'");
                obj_str.Append(" AND PROVIDENT_CODE ='" + code + "'");

                blnResult = Obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(ProvidentWorkage.delete)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(List<cls_TRProvidentWorkage> list_model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_PROVIDENT_WORKAGE");
                obj_str.Append(" (");
                obj_str.Append("COMPANY_CODE ");
                obj_str.Append(", PROVIDENT_CODE ");
                obj_str.Append(", WORKAGE_FROM ");
                obj_str.Append(", WORKAGE_TO ");
                obj_str.Append(", RATE_EMP ");
                obj_str.Append(", RATE_COM ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@COMPANY_CODE ");
                obj_str.Append(", @PROVIDENT_CODE ");
                obj_str.Append(", @WORKAGE_FROM ");
                obj_str.Append(", @WORKAGE_TO ");
                obj_str.Append(", @RATE_EMP ");
                obj_str.Append(", @RATE_COM ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                obj_conn.doOpenTransaction();

                System.Text.StringBuilder obj_str2 = new System.Text.StringBuilder();

                obj_str2.Append(" DELETE FROM HRM_TR_PROVIDENT_WORKAGE");
                obj_str2.Append(" WHERE 1=1 ");
                obj_str2.Append(" AND COMPANY_CODE='" + list_model[0].company_code + "'");
                obj_str2.Append(" AND PROVIDENT_CODE='" + list_model[0].provident_code + "'");
                
                blnResult = obj_conn.doExecuteSQL_transaction(obj_str2.ToString());

                if (blnResult)
                {
                    SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
                    obj_cmd.Transaction = obj_conn.getTransaction();

                    obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@PROVIDENT_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@WORKAGE_FROM", SqlDbType.Decimal);
                    obj_cmd.Parameters.Add("@WORKAGE_TO", SqlDbType.Decimal);
                    obj_cmd.Parameters.Add("@RATE_EMP", SqlDbType.Decimal);
                    obj_cmd.Parameters.Add("@RATE_COM", SqlDbType.Decimal);

                    foreach (cls_TRProvidentWorkage model in list_model)
                    {

                        obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                        obj_cmd.Parameters["@PROVIDENT_CODE"].Value = model.provident_code;
                        obj_cmd.Parameters["@WORKAGE_FROM"].Value = model.workage_from;
                        obj_cmd.Parameters["@WORKAGE_TO"].Value = model.workage_to;
                        obj_cmd.Parameters["@RATE_EMP"].Value = model.rate_emp;
                        obj_cmd.Parameters["@RATE_COM"].Value = model.rate_com;

                        obj_cmd.ExecuteNonQuery();

                    }

                    blnResult = obj_conn.doCommit();

                    if(!blnResult)
                        obj_conn.doRollback();

                }
                else
                {
                    obj_conn.doRollback();
                }

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {              
                Message = "ERROR::(ProvidentWorkage.insert)" + ex.ToString();
            }

            return blnResult;
        }

    }
}
