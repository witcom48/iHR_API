using ClassLibrary_BPC.hrfocus.model;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.controller
{
    public class cls_ctTwRVerify
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTwRVerify() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TRVerify> getData(string condition)
        {
            List<cls_TRVerify> list_model = new List<cls_TRVerify>();
            cls_TRVerify model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("COMPANY_CODE");
                obj_str.Append(", ITEM_CODE");
                obj_str.Append(", EMPTYPE_ID");
                obj_str.Append(", PAYITEM_DATE");

                obj_str.Append(", VERIFY_STATUS");
                obj_str.Append(" FROM HRM_TR_VERIFY");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY COMPANY_CODE");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TRVerify();
                    model.company_code = Convert.ToString(dr["COMPANY_CODE"]);
                    model.workflow_type = Convert.ToString(dr["ITEM_CODE"]);
                    model.workflow_code = Convert.ToString(dr["EMPTYPE_ID"]);
                    model.position_level = Convert.ToString(dr["PAYITEM_DATE"]);
                    model.position_level = Convert.ToString(dr["VERIFY_STATUS"]);


                    list_model.Add(model);
                }

            }
            catch (Exception ex)
            {
                Message = "ERROR::(Lineapprove.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TRVerify> getDataByFillter(string com, string workflow_type, string workflow_code, string position_level)
        {
            string strCondition = "";

            strCondition += " AND COMPANY_CODE='" + com + "'";

            if (!workflow_type.Equals(""))
                strCondition += " AND ITEM_CODE='" + workflow_type + "'";
            if (!workflow_code.Equals(""))
                strCondition += " AND EMPTYPE_ID='" + workflow_code + "'";
            if (!position_level.Equals(""))
                strCondition += " AND PAYITEM_DATE='" + position_level + "'";

            if (!position_level.Equals(""))
                strCondition += " AND VERIFY_STATUS='" + position_level + "'";
            return this.getData(strCondition);
        }

        public bool checkDataOld(string com, string workflow_type, string position_level)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT COMPANY_CODE");
                obj_str.Append(" FROM HRM_TR_VERIFY");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "' ");
                obj_str.Append(" AND ITEM_CODE='" + workflow_type + "' ");
                obj_str.Append(" AND PAYITEM_DATE='" + position_level + "' ");

                obj_str.Append(" AND VERIFY_STATUS='" + position_level + "' ");


                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Lineapprove.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public bool delete(string com, string workflow_type, string workflow_code, string position_level)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_TR_VERIFY");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "' ");
                if (!workflow_type.Equals(""))
                    obj_str.Append(" AND ITEM_CODE='" + workflow_type + "' ");
                if (!workflow_code.Equals(""))
                    obj_str.Append(" AND EMPTYPE_ID='" + workflow_code + "' ");
                if (!position_level.Equals(""))
                    obj_str.Append(" AND PAYITEM_DATE='" + position_level + "' ");

                if (!position_level.Equals(""))
                    obj_str.Append(" AND VERIFY_STATUS='" + position_level + "' ");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());


            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Lineapprove.delete)" + ex.ToString();
            }

            return blnResult;
        }
        public string insert(cls_TRVerify model)
        {
            string blnResult = "";
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.company_code, model.workflow_type, model.position_level))
                {
                    return "D";
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();
                obj_str.Append("INSERT INTO HRM_TR_VERIFY");
                obj_str.Append(" (");
                obj_str.Append("COMPANY_CODE ");
                obj_str.Append(", ITEM_CODE ");
                obj_str.Append(", EMPTYPE_ID ");
                obj_str.Append(", PAYITEM_DATE ");
                obj_str.Append(", VERIFY_STATUS ");

                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@COMPANY_CODE ");
                obj_str.Append(", @ITEM_CODE ");
                obj_str.Append(", @EMPTYPE_ID ");
                obj_str.Append(", @PAYITEM_DATE ");
                obj_str.Append(", @VERIFY_STATUS ");

                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@ITEM_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@ITEM_CODE"].Value = model.workflow_type;
                obj_cmd.Parameters.Add("@EMPTYPE_ID", SqlDbType.VarChar); obj_cmd.Parameters["@EMPTYPE_ID"].Value = model.workflow_code;
                obj_cmd.Parameters.Add("@PAYITEM_DATE", SqlDbType.VarChar); obj_cmd.Parameters["@PAYITEM_DATE"].Value = model.position_level;
                obj_cmd.Parameters.Add("@VERIFY_STATUS", SqlDbType.VarChar); obj_cmd.Parameters["@VERIFY_STATUS"].Value = model.position_level;

                obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@CREATED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = model.flag;


                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = model.workflow_code.ToString();
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Lineapprove.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(List<cls_TRVerify> list_model, string username)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_VERIFY");
                obj_str.Append(" (");
                obj_str.Append("COMPANY_CODE ");
                obj_str.Append(", ITEM_CODE ");
                obj_str.Append(", EMPTYPE_ID ");
                obj_str.Append(", PAYITEM_DATE ");
                obj_str.Append(", VERIFY_STATUS ");

                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@COMPANY_CODE ");
                obj_str.Append(", @ITEM_CODE ");
                obj_str.Append(", @EMPTYPE_ID ");
                obj_str.Append(", @PAYITEM_DATE ");
                obj_str.Append(", @ ");

                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");


                obj_conn.doConnect();

                obj_conn.doOpenTransaction();

                //-- Step 1 delete data old
                string posilevel = "";
                foreach (cls_TRVerify model in list_model)
                {
                    posilevel += "'" + model.position_level + "',";
                }
                if (posilevel.Length > 0)
                    posilevel = posilevel.Substring(0, posilevel.Length - 1);

                System.Text.StringBuilder obj_str2 = new System.Text.StringBuilder();

                obj_str2.Append(" DELETE FROM HRM_TR_VERIFY");
                obj_str2.Append(" WHERE 1=1 ");
                obj_str2.Append(" AND COMPANY_CODE='" + list_model[0].company_code + "'");
                obj_str2.Append(" AND ITEM_CODE='" + list_model[0].workflow_type + "' ");
                obj_str2.Append(" AND PAYITEM_DATE IN (" + posilevel + ")");
                obj_str2.Append(" AND VERIFY_STATUS='" + list_model[0].company_code + "'");

                //obj_str2.Append(" AND EMPTYPE_ID='" + list_model[0].workflow_code + "' ");

                blnResult = obj_conn.doExecuteSQL_transaction(obj_str2.ToString());
                //int id = 1;
                //try
                //{
                //    System.Text.StringBuilder obj_str3 = new System.Text.StringBuilder();

                //    obj_str3.Append("SELECT MAX(LINEAPPROVE_ID) ");
                //    obj_str3.Append(" FROM HRM_TR_VERIFY");

                //    DataTable dt = obj_conn.doGetTableTransaction(obj_str3.ToString());
                //    if (dt.Rows.Count > 0)
                //    {
                //        id = Convert.ToInt32(dt.Rows[0][0]) + 1;
                //    }
                //}
                //catch
                //{

                //}
                if (blnResult)
                {
                    SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
                    obj_cmd.Transaction = obj_conn.getTransaction();

                    obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@ITEM_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@EMPTYPE_ID", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@PAYITEM_DATE", SqlDbType.VarChar);

                    obj_cmd.Parameters.Add("@VERIFY_STATUS", SqlDbType.VarChar);

                    obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime);
                    obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit);
                    foreach (cls_TRVerify model in list_model)
                    {

                        obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                        obj_cmd.Parameters["@ITEM_CODE"].Value = model.workflow_type;
                        obj_cmd.Parameters["@EMPTYPE_ID"].Value = model.workflow_code;
                        obj_cmd.Parameters["@PAYITEM_DATE"].Value = model.position_level;
                        obj_cmd.Parameters["@VERIFY_STATUS"].Value = model.position_level;

                        obj_cmd.Parameters["@CREATED_BY"].Value = username;
                        obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                        obj_cmd.Parameters["@FLAG"].Value = model.flag;
                        obj_cmd.ExecuteNonQuery();

                    }

                    blnResult = obj_conn.doCommit();

                    if (!blnResult)
                    {
                        obj_conn.doRollback();
                    }
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
                Message = "ERROR::(Lineapprove.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public string update(cls_TRVerify model)
        {
            string blnResult = "";
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_TR_VERIFY SET ");

                obj_str.Append(" ITEM_CODE=@ITEM_CODE ");
                obj_str.Append(", EMPTYPE_ID=@EMPTYPE_ID ");
                obj_str.Append(", PAYITEM_DATE=@PAYITEM_DATE ");
                obj_str.Append(", VERIFY_STATUS=@VERIFY_STATUS ");


                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");
                obj_str.Append(", FLAG=@FLAG ");


                obj_str.Append(" WHERE EMPTYPE_ID=@EMPTYPE_ID ");


                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@ITEM_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@ITEM_CODE"].Value = model.workflow_type;
                obj_cmd.Parameters.Add("@EMPTYPE_ID", SqlDbType.Int); obj_cmd.Parameters["@EMPTYPE_ID"].Value = model.workflow_code;
                obj_cmd.Parameters.Add("@PAYITEM_DATE", SqlDbType.VarChar); obj_cmd.Parameters["@PAYITEM_DATE"].Value = model.position_level;

                obj_cmd.Parameters.Add("@VERIFY_STATUS", SqlDbType.VarChar); obj_cmd.Parameters["@VERIFY_STATUS"].Value = model.position_level;

                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = model.flag;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = model.workflow_code;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Lineapprove.update)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
