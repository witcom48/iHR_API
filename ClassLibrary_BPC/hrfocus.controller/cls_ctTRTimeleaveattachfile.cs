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
   public class cls_ctTRTimeleaveattachfile
    {
          string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTRTimeleaveattachfile() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }
        public List<cls_TRTimeleaveattachfile> getData(string timeleave_doc, string com)
        {
            List<cls_TRTimeleaveattachfile> list_model = new List<cls_TRTimeleaveattachfile>();
            cls_TRTimeleaveattachfile model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT *");
                obj_str.Append(" FROM HRM_TR_TIMELEAVEATTACHFILE");

                obj_str.Append(" WHERE COMPANY_CODE ='"+com+"'");
                obj_str.Append(" AND TIMELEAVE_DOC ='" + timeleave_doc + "'");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TRTimeleaveattachfile();

                    model.COMPANY_CODE = dr["COMPANY_CODE"].ToString();
                    model.TIMELEAVE_DOC = dr["TIMELEAVE_DOC"].ToString();
                    model.FILE_NO = dr["FILE_NO"].ToString();
                    model.FILE_NAME = dr["FILE_NAME"].ToString();
                    model.FILE_PATH = dr["FILE_PATH"].ToString();

                    model.CREATED_DATE = Convert.ToDateTime(dr["CREATED_DATE"]);
                    model.CREATED_BY = dr["CREATED_BY"].ToString();
                    if (!dr["MODIFIED_BY"].ToString().Equals(""))
                    {
                        model.MODIFIED_DATE = Convert.ToDateTime(dr["MODIFIED_DATE"]);
                        model.MODIFIED_BY = dr["MODIFIED_BY"].ToString();
                    }
   
                    list_model.Add(model);
                }

            }
            catch (Exception ex)
            {
                Message = "ERROR::(HRM_TR_TIMELEAVEATTACHFILE.getData)" + ex.ToString();
            }

            return list_model;
        }
        public bool checkDataOld(string com, string fileno,string docno)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT COMPANY_CODE");
                obj_str.Append(" FROM HRM_TR_TIMELEAVEATTACHFILE");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND FILE_NO='" + fileno + "'");
                obj_str.Append(" AND TIMELEAVE_DOC='" + docno + "'");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(HRM_TR_TIMELEAVEATTACHFILE.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }
        public bool insert(cls_TRTimeleaveattachfile model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.COMPANY_CODE, model.FILE_NO, model.TIMELEAVE_DOC))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_TIMELEAVEATTACHFILE");
                obj_str.Append(" (");
                obj_str.Append("COMPANY_CODE ");

                obj_str.Append(", TIMELEAVE_DOC ");
                //obj_str.Append(", FILE_NO ");
                obj_str.Append(", FILE_NAME ");
                obj_str.Append(", FILE_PATH ");

                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", CREATED_BY ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@COMPANY_CODE ");

                obj_str.Append(", @TIMELEAVE_DOC ");
                obj_str.Append(", @FILE_NAME ");
                obj_str.Append(", @FILE_PATH ");

                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.COMPANY_CODE;

                obj_cmd.Parameters.Add("@TIMELEAVE_DOC", SqlDbType.VarChar); obj_cmd.Parameters["@TIMELEAVE_DOC"].Value = model.TIMELEAVE_DOC;
                obj_cmd.Parameters.Add("@FILE_NAME", SqlDbType.VarChar); obj_cmd.Parameters["@FILE_NAME"].Value = model.FILE_NAME;
                obj_cmd.Parameters.Add("@FILE_PATH", SqlDbType.VarChar); obj_cmd.Parameters["@FILE_PATH"].Value = model.FILE_PATH;

                obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@CREATED_DATE"].Value = model.CREATED_DATE;
                obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@CREATED_BY"].Value = model.CREATED_BY;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();
                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(HRM_TR_TIMELEAVEATTACHFILE.insert)" + ex.ToString();
            }

            return blnResult;
        }
        public bool update(cls_TRTimeleaveattachfile model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_TR_TIMELEAVEATTACHFILE SET ");
                obj_str.Append(" FILE_NAME=@FILE_NAME ");
                obj_str.Append(", FILE_PATH=@FILE_PATH ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");
                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(" WHERE FILE_NO=@FILE_NO");


                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
                obj_cmd.Parameters.Add("@FILE_NAME", SqlDbType.VarChar); obj_cmd.Parameters["@FILE_NAME"].Value = model.FILE_NAME;
                obj_cmd.Parameters.Add("@FILE_PATH", SqlDbType.VarChar); obj_cmd.Parameters["@FILE_PATH"].Value = model.FILE_PATH;
                obj_cmd.Parameters.Add("@FILE_NO", SqlDbType.VarChar); obj_cmd.Parameters["@FILE_NO"].Value = model.FILE_NO;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = model.MODIFIED_DATE;
                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.MODIFIED_BY;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(HRM_TR_TIMELEAVEATTACHFILE.update)" + ex.ToString();
            }

            return blnResult;
        }
        public bool delete(string id,string com)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_TR_TIMELEAVEATTACHFILE");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND FILE_NO='" + id + "'");
                obj_str.Append(" AND COMPANY_CODE='" + com + "'");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(HRM_TR_TIMELEAVEATTACHFILE.delete)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
