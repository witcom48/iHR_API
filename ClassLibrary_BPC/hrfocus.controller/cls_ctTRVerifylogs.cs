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
    public class cls_ctTRVerifylogs
  {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTRVerifylogs() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TRVerifylogs> getData(string condition)
        {
            List<cls_TRVerifylogs> list_model = new List<cls_TRVerifylogs>();
            cls_TRVerifylogs model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("HRM_TR_VERIFY_LOGS.COMPANY_CODE");
                obj_str.Append(", HRM_TR_VERIFY_LOGS.ITEM_CODE");
                obj_str.Append(", HRM_TR_VERIFY_LOGS.EMPTYPE_ID");
                obj_str.Append(", HRM_TR_VERIFY_LOGS.PAYITEM_DATE");
                obj_str.Append(", VERIFY_QUANTITY");
                obj_str.Append(", VERIFY_AMOUNT");
                obj_str.Append(", VERIFY_NOTE");
                obj_str.Append(", CREATE_BY");
                obj_str.Append(", CREATE_DATE");
                //obj_str.Append(", ISNULL(HRM_TR_PAYITEM.MODIFIED_BY, HRM_TR_PAYITEM.CREATED_BY) AS MODIFIED_BY");
                //obj_str.Append(", ISNULL(HRM_TR_PAYITEM.MODIFIED_DATE, HRM_TR_PAYITEM.CREATED_DATE) AS MODIFIED_DATE");


                obj_str.Append(" FROM HRM_TR_VERIFY_LOGS");
                //obj_str.Append("  INNER JOIN HRM_TR_PAYITEM ON HRM_TR_PAYITEM.COMPANY_CODE=HRM_TR_VERIFY_LOGS.COMPANY_CODE AND HRM_TR_PAYITEM.ITEM_CODE=HRM_TR_VERIFY_LOGS.ITEM_CODE ");

                  

                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY HRM_TR_VERIFY_LOGS.COMPANY_CODE");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TRVerifylogs();
                    model.company_code = Convert.ToString(dr["COMPANY_CODE"]);
                    model.item_code = Convert.ToString(dr["ITEM_CODE"]);
                    model.emptype_id = Convert.ToString(dr["EMPTYPE_ID"]);
                    model.payitem_date = Convert.ToDateTime(dr["PAYITEM_DATE"]);
                    model.verify_quantity = Convert.ToString(dr["VERIFY_QUANTITY"]);
                    model.verify_amount = Convert.ToString(dr["VERIFY_AMOUNT"]);
                    model.verify_note = Convert.ToString(dr["VERIFY_NOTE"]);

                    model.create_by = dr["CREATE_BY"].ToString();
                    model.create_date = Convert.ToDateTime(dr["CREATE_DATE"]);
                    list_model.Add(model);
                }

            }
            catch (Exception ex)
            {
                Message = "ERROR::(verify.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TRVerifylogs> getDataByFillter(string com, string item_code )
        {
            string strCondition = "";

            strCondition += " AND HRM_TR_VERIFY_LOGS.COMPANY_CODE='" + com + "'";

            if (!item_code.Equals(""))
                strCondition += " AND HRM_TR_VERIFY_LOGS.ITEM_CODE='" + item_code + "'";
            //if (!id.Equals(""))
            //    strCondition += " AND EMPTYPE_ID='" + id + "'";

            //if (!payitem_date.Equals(""))
            //    strCondition += " AND PAYITEM_DATE='" + payitem_date + "'";

            //if (!status.Equals(""))
            //    strCondition += " AND VERIFY_STATUS='" + status + "'";
            return this.getData(strCondition);
        }

        public int getNextID()
        {
            int intResult = 1;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT MAX(EMPTYPE_ID) ");
                obj_str.Append(" FROM HRM_TR_VERIFY_LOGS");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    intResult = Convert.ToInt32(dt.Rows[0][0]) + 1;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Worker.getNextID)" + ex.ToString();
            }

            return intResult;
        }

        public bool checkDataOld(string com, string item_code )
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT COMPANY_CODE");
                obj_str.Append(" FROM HRM_TR_VERIFY_LOGS");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "' ");
                obj_str.Append(" AND ITEM_CODE='" + item_code + "' ");
                //obj_str.Append(" AND PAYITEM_DATE='" + position_level + "' ");

                //obj_str.Append(" AND VERIFY_STATUS='" + status + "' ");


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

        public bool delete(string com, string item_code, string id )
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_TR_VERIFY_LOGS");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "' ");
                if (!item_code.Equals(""))
                    obj_str.Append(" AND ITEM_CODE='" + item_code + "' ");
                if (!id.Equals(""))
                    obj_str.Append(" AND EMPTYPE_ID='" + id + "' ");
                //if (!position_level.Equals(""))
                //    obj_str.Append(" AND PAYITEM_DATE='" + position_level + "' ");

                //if (!status.Equals(""))
                //    obj_str.Append(" AND VERIFY_STATUS='" + status + "' ");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());


            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(verify.delete)" + ex.ToString();
            }

            return blnResult;
        }
        public string insert(cls_TRVerifylogs model)
        {
            string blnResult = "";
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.company_code, model.item_code ))
                {
                    return "";
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();
                obj_str.Append("INSERT INTO HRM_TR_VERIFY_LOGS");
                obj_str.Append(" (");
                obj_str.Append("COMPANY_CODE ");
                obj_str.Append(", ITEM_CODE ");
                obj_str.Append(", EMPTYPE_ID ");
                obj_str.Append(", PAYITEM_DATE ");

                obj_str.Append(", VERIFY_QUANTITY ");
                obj_str.Append(", VERIFY_AMOUNT ");
                obj_str.Append(", VERIFY_NOTE ");

                obj_str.Append(", CREATE_BY ");
                obj_str.Append(", CREATE_DATE ");
                 obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@COMPANY_CODE ");
                obj_str.Append(", @ITEM_CODE ");
                obj_str.Append(", @EMPTYPE_ID ");
                obj_str.Append(", @PAYITEM_DATE ");
                obj_str.Append(", @VERIFY_QUANTITY ");
                obj_str.Append(", @VERIFY_AMOUNT ");
                obj_str.Append(", @VERIFY_NOTE ");


                obj_str.Append(", @CREATE_BY ");
                obj_str.Append(", @CREATE_DATE ");
                 obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@ITEM_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@ITEM_CODE"].Value = model.item_code;
                obj_cmd.Parameters.Add("@EMPTYPE_ID", SqlDbType.VarChar); obj_cmd.Parameters["@EMPTYPE_ID"].Value = model.emptype_id;

                obj_cmd.Parameters.Add("@PAYITEM_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@PAYITEM_DATE"].Value = model.payitem_date;
                
                obj_cmd.Parameters.Add("@VERIFY_QUANTITY", SqlDbType.VarChar); obj_cmd.Parameters["@VERIFY_QUANTITY"].Value = model.verify_quantity;
                obj_cmd.Parameters.Add("@VERIFY_AMOUNT", SqlDbType.VarChar); obj_cmd.Parameters["@VERIFY_AMOUNT"].Value = model.verify_amount;
                obj_cmd.Parameters.Add("@VERIFY_NOTE", SqlDbType.VarChar); obj_cmd.Parameters["@VERIFY_NOTE"].Value = model.verify_note;

                obj_cmd.Parameters.Add("@CREATE_BY", SqlDbType.DateTime); obj_cmd.Parameters["@CREATE_BY"].Value = model.create_by;
                obj_cmd.Parameters.Add("@CREATE_DATE", SqlDbType.VarChar); obj_cmd.Parameters["@CREATE_DATE"].Value = model.create_date;


                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = model.item_code.ToString();
            }
            catch (Exception ex)
            {
                Message = "ERROR::(verify.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert2(List<cls_TRVerifylogs> list_model, string username)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();
                int id = this.getNextID();

                obj_str.Append("INSERT INTO HRM_TR_VERIFY_LOGS");
                obj_str.Append(" (");
                obj_str.Append("COMPANY_CODE ");
                obj_str.Append(", ITEM_CODE ");
                obj_str.Append(", EMPTYPE_ID ");
                obj_str.Append(", PAYITEM_DATE ");
       

                obj_str.Append(", VERIFY_QUANTITY ");
                obj_str.Append(", VERIFY_AMOUNT ");
                obj_str.Append(", VERIFY_NOTE ");



                obj_str.Append(", CREATE_BY ");
                obj_str.Append(", CREATE_DATE ");
                 obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@COMPANY_CODE ");
                obj_str.Append(", @ITEM_CODE ");
                obj_str.Append(", @EMPTYPE_ID ");
                obj_str.Append(", @PAYITEM_DATE ");

                obj_str.Append(", @VERIFY_QUANTITY ");
                obj_str.Append(", @VERIFY_AMOUNT ");
                obj_str.Append(", @VERIFY_NOTE ");

       

                obj_str.Append(", @CREATE_BY ");
                obj_str.Append(", @CREATE_DATE ");
                 obj_str.Append(" )");


                obj_conn.doConnect();

                obj_conn.doOpenTransaction();

               

                System.Text.StringBuilder obj_str2 = new System.Text.StringBuilder();

 

                obj_str2.Append(" DELETE FROM HRM_TR_VERIFY_LOGS");
                obj_str2.Append(" WHERE 1=1 ");
                obj_str2.Append(" AND COMPANY_CODE='" + list_model[0].company_code + "'");
                obj_str2.Append(" AND ITEM_CODE='" + list_model[0].item_code + "' ");
                //obj_str2.Append(" AND PAYITEM_DATE IN (" + posilevel + ")");

                obj_str2.Append(" AND VERIFY_QUANTITY='" + list_model[0].verify_quantity + "'");
                obj_str2.Append(" AND VERIFY_AMOUNT='" + list_model[0].verify_amount + "'");
                obj_str2.Append(" AND VERIFY_NOTE='" + list_model[0].verify_note + "'");

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
 
                    obj_cmd.Parameters.Add("@VERIFY_QUANTITY", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@VERIFY_AMOUNT", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@VERIFY_NOTE", SqlDbType.VarChar);

                    obj_cmd.Parameters.Add("@CREATE_BY", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@CREATE_DATE", SqlDbType.DateTime);
                     foreach (cls_TRVerifylogs model in list_model)
                    {

                        obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                        obj_cmd.Parameters["@ITEM_CODE"].Value = model.item_code;
                        obj_cmd.Parameters["@EMPTYPE_ID"].Value = this.getNextID();
                        obj_cmd.Parameters["@PAYITEM_DATE"].Value = model.payitem_date;
                        obj_cmd.Parameters["@VERIFY_QUANTITY"].Value = model.verify_quantity;
                        obj_cmd.Parameters["@VERIFY_AMOUNT"].Value = model.verify_amount;
                        obj_cmd.Parameters["@VERIFY_NOTE"].Value = model.verify_note;

                        obj_cmd.Parameters["@CREATE_BY"].Value = username;
                        obj_cmd.Parameters["@CREATE_DATE"].Value = DateTime.Now;
                        
 
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

        public string update(cls_TRVerifylogs model)
        {
            string blnResult = "";
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_TR_VERIFY_LOGS SET ");

                obj_str.Append(" ITEM_CODE=@ITEM_CODE ");
                obj_str.Append(", EMPTYPE_ID=@EMPTYPE_ID ");
                obj_str.Append(", PAYITEM_DATE=@PAYITEM_DATE ");
                obj_str.Append(", VERIFY_QUANTITY=@VERIFY_QUANTITY ");
                obj_str.Append(", VERIFY_AMOUNT=@VERIFY_AMOUNT ");
                obj_str.Append(", VERIFY_NOTE=@VERIFY_NOTE ");


                obj_str.Append(", CREATE_BY=@CREATE_BY ");
                obj_str.Append(", CREATE_DATE=@CREATE_DATE ");
 

                obj_str.Append(" WHERE EMPTYPE_ID=@EMPTYPE_ID ");


                obj_conn.doConnect();
                
                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@ITEM_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@ITEM_CODE"].Value = model.item_code;

                 obj_cmd.Parameters.Add("@EMPTYPE_ID", SqlDbType.Int); obj_cmd.Parameters["@EMPTYPE_ID"].Value = this.getNextID();

                 obj_cmd.Parameters.Add("@PAYITEM_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@PAYITEM_DATE"].Value = model.payitem_date;


                 obj_cmd.Parameters.Add("@VERIFY_QUANTITY", SqlDbType.Bit); obj_cmd.Parameters["@VERIFY_QUANTITY"].Value = model.verify_quantity;
                 obj_cmd.Parameters.Add("@VERIFY_AMOUNT", SqlDbType.Bit); obj_cmd.Parameters["@VERIFY_AMOUNT"].Value = model.verify_amount;
                 obj_cmd.Parameters.Add("@VERIFY_NOTE", SqlDbType.Bit); obj_cmd.Parameters["@VERIFY_NOTE"].Value = model.verify_note;


                 obj_cmd.Parameters.Add("@CREATE_BY", SqlDbType.VarChar); obj_cmd.Parameters["@CREATE_BY"].Value = model.create_by;
                 obj_cmd.Parameters.Add("@CREATE_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@CREATE_DATE"].Value = DateTime.Now;
 
                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = model.item_code;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(verify.update)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
