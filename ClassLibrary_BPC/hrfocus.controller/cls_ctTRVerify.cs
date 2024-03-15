using ClassLibrary_BPC.hrfocus.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.controller
{
   public class cls_ctTRVerify
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTRVerify() { }

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

                obj_str.Append(" HRM_TR_VERIFY.COMPANY_CODE ");
                obj_str.Append(", ITEM_CODE");
                obj_str.Append(", EMPTYPE_ID");
                obj_str.Append(", PAYITEM_DATE");

                obj_str.Append(", ISNULL(VERIFY_STATUS, 0) AS VERIFY_STATUS ");

                obj_str.Append(", HRM_MT_WORKER.WORKER_CODE");

                obj_str.Append(" FROM HRM_TR_VERIFY");
                obj_str.Append(" LEFT JOIN HRM_MT_WORKER ON HRM_MT_WORKER.COMPANY_CODE=HRM_TR_VERIFY.COMPANY_CODE AND HRM_MT_WORKER.WORKER_EMPTYPE=HRM_TR_VERIFY.EMPTYPE_ID");

                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY HRM_TR_VERIFY.COMPANY_CODE");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TRVerify();
                    model.company_code = Convert.ToString(dr["COMPANY_CODE"]);
                    model.item_code = Convert.ToString(dr["ITEM_CODE"]);
                    model.emptype_id = Convert.ToString(dr["EMPTYPE_ID"]);
                    model.payitem_date = Convert.ToDateTime(dr["PAYITEM_DATE"]);
                    model.verify_status = Convert.ToString(dr["VERIFY_STATUS"]);
                    model.worker_code = Convert.ToString(dr["WORKER_CODE"]);


                    list_model.Add(model);
                }

            }
            catch (Exception ex)
            {
                Message = "ERROR::(verify.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TRVerify> getDataByFillter(string com, string item_code, string emp, string status, string emptype_id)
        {
            string strCondition = "";

            strCondition += " AND HRM_TR_VERIFY.COMPANY_CODE='" + com + "'";

            if (!item_code.Equals(""))
                strCondition += " AND ITEM_CODE='" + item_code + "'";
            if (!emp.Equals(""))
                strCondition += " AND HRM_MT_WORKER.WORKER_CODE='" + emp + "'";

            

            if (!status.Equals(""))
                strCondition += " AND VERIFY_STATUS='" + status + "'";

            if (!emptype_id.Equals(""))
                strCondition += " AND EMPTYPE_ID='" + emptype_id + "'";
            return this.getData(strCondition);
        }

        public int getNextID()
        {
            int intResult = 1;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT MAX(EMPTYPE_ID) ");
                obj_str.Append(" FROM HRM_TR_VERIFY");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
                {
                    intResult = Convert.ToInt32(dt.Rows[0][0]) + 1;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(VERIFY.getNextID)" + ex.ToString();
            }

            return intResult;
        }



         

        public bool checkDataOld(string com, string item_code, string status)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT COMPANY_CODE");
                obj_str.Append(" FROM HRM_TR_VERIFY");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "' ");
                obj_str.Append(" AND ITEM_CODE='" + item_code + "' ");
                //obj_str.Append(" AND PAYITEM_DATE='" + position_level + "' ");

                obj_str.Append(" AND VERIFY_STATUS='" + status + "' ");


                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(verify.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public bool delete(string com, string item_code, string id, string status)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_TR_VERIFY");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "' ");
                if (!item_code.Equals(""))
                    obj_str.Append(" AND ITEM_CODE='" + item_code + "' ");
                if (!id.Equals(""))
                    obj_str.Append(" AND EMPTYPE_ID='" + id + "' ");
                //if (!position_level.Equals(""))
                //    obj_str.Append(" AND PAYITEM_DATE='" + position_level + "' ");

                if (!status.Equals(""))
                    obj_str.Append(" AND VERIFY_STATUS='" + status + "' ");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());


            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(verify.delete)" + ex.ToString();
            }

            return blnResult;
        }
        public string insert(cls_TRVerify model)
        {
            string blnResult = "";
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.company_code, model.item_code, model.verify_status))
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
                obj_cmd.Parameters.Add("@ITEM_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@ITEM_CODE"].Value = model.item_code;
                obj_cmd.Parameters.Add("@EMPTYPE_ID", SqlDbType.VarChar); obj_cmd.Parameters["@EMPTYPE_ID"].Value = model.emptype_id;

                obj_cmd.Parameters.Add("@PAYITEM_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@PAYITEM_DATE"].Value = model.payitem_date;


                obj_cmd.Parameters.Add("@VERIFY_STATUS", SqlDbType.Bit); obj_cmd.Parameters["@VERIFY_STATUS"].Value = model.verify_status;

  

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = model.verify_status.ToString();
            }
            catch (Exception ex)
            {
                Message = "ERROR::(verify.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool inserts(List<cls_TRVerify> list_model, string username)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();
                int id = this.getNextID();

                obj_str.Append("INSERT INTO HRM_TR_VERIFY");
                obj_str.Append(" (");
                obj_str.Append("COMPANY_CODE ");
                obj_str.Append(", ITEM_CODE ");
                obj_str.Append(", EMPTYPE_ID ");
                obj_str.Append(", PAYITEM_DATE ");
                obj_str.Append(", VERIFY_STATUS ");

          
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@COMPANY_CODE ");
                obj_str.Append(", @ITEM_CODE ");
                obj_str.Append(", @EMPTYPE_ID ");
                obj_str.Append(", @PAYITEM_DATE ");
                obj_str.Append(", @VERIFY_STATUS ");
          
                obj_str.Append(" )");


                obj_conn.doConnect();

                obj_conn.doOpenTransaction();

                //-- Step 1 delete data old
                //string posilevel = "";
                //foreach (cls_TRVerify model in list_model)
                //{
                //    posilevel += "'" + model.payitem_date + "',";
                //}
                //if (posilevel.Length > 0)
                //    posilevel = posilevel.Substring(0, posilevel.Length - 1);

                System.Text.StringBuilder obj_str2 = new System.Text.StringBuilder();

                obj_str2.Append(" DELETE FROM HRM_TR_VERIFY");
                obj_str2.Append(" WHERE 1=1 ");
                obj_str2.Append(" AND COMPANY_CODE='" + list_model[0].company_code + "'");
                obj_str2.Append(" AND ITEM_CODE='" + list_model[0].item_code + "' ");
                //obj_str2.Append(" AND PAYITEM_DATE IN (" + list_model[0].payitem_date + ")");
                obj_str2.Append(" AND VERIFY_STATUS='" + list_model[0].verify_status + "'");

                obj_str2.Append(" AND EMPTYPE_ID='" + list_model[0].emptype_id + "' ");

                blnResult = obj_conn.doExecuteSQL_transaction(obj_str2.ToString());
               
                if (blnResult)
                {
                    SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
                    obj_cmd.Transaction = obj_conn.getTransaction();

                    obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@ITEM_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@EMPTYPE_ID", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@PAYITEM_DATE", SqlDbType.Date);

                    obj_cmd.Parameters.Add("@VERIFY_STATUS", SqlDbType.VarChar);

                    
                    foreach (cls_TRVerify model in list_model)
                    {

                        obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                        obj_cmd.Parameters["@ITEM_CODE"].Value = model.item_code;
                        obj_cmd.Parameters["@EMPTYPE_ID"].Value = this.getNextID();
                        obj_cmd.Parameters["@PAYITEM_DATE"].Value = model.payitem_date;
                        obj_cmd.Parameters["@VERIFY_STATUS"].Value = model.verify_status;
  
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
                Message = "ERROR::(verify.insert)" + ex.ToString();
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


               


                obj_str.Append(" WHERE EMPTYPE_ID=@EMPTYPE_ID ");


                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@ITEM_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@ITEM_CODE"].Value = model.item_code;

                 obj_cmd.Parameters.Add("@EMPTYPE_ID", SqlDbType.Int); obj_cmd.Parameters["@EMPTYPE_ID"].Value = this.getNextID();

                 obj_cmd.Parameters.Add("@PAYITEM_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@PAYITEM_DATE"].Value = model.payitem_date;


                 obj_cmd.Parameters.Add("@VERIFY_STATUS", SqlDbType.Bit); obj_cmd.Parameters["@VERIFY_STATUS"].Value = model.verify_status;

 
              

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = model.verify_status;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(verify.update)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
