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
    public class cls_ctTRCombank
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTRCombank() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TRCombank> getData(string language, string condition)
        {
            List<cls_TRCombank> list_model = new List<cls_TRCombank>();
            cls_TRCombank model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("COMPANY_CODE");                
                obj_str.Append(", COMBANK_ID");      
                obj_str.Append(", ISNULL(COMBANK_BANKCODE, '') AS COMBANK_BANKCODE");
                obj_str.Append(", ISNULL(COMBANK_BANKACCOUNT, '') AS COMBANK_BANKACCOUNT");

                obj_str.Append(", ISNULL(HRM_TR_COMBANK.MODIFIED_BY, HRM_TR_COMBANK.CREATED_BY) AS MODIFIED_BY");
                obj_str.Append(", ISNULL(HRM_TR_COMBANK.MODIFIED_DATE, HRM_TR_COMBANK.CREATED_DATE) AS MODIFIED_DATE");

                if (language.Equals("TH"))
                {
                    obj_str.Append(", HRM_MT_BANK.BANK_NAME_EN AS NAME_DETAIL");

                }
                else
                {
                    obj_str.Append(", HRM_MT_BANK.BANK_NAME_TH AS NAME_DETAIL");
                }
                obj_str.Append(" FROM HRM_TR_COMBANK");
                obj_str.Append(" INNER JOIN HRM_MT_BANK  ON HRM_TR_COMBANK.COMBANK_BANKCODE = HRM_MT_BANK.BANK_CODE ");

                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY COMPANY_CODE");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TRCombank();

                    model.company_code = dr["COMPANY_CODE"].ToString();                   
                    model.combank_id = Convert.ToInt32(dr["COMBANK_ID"]);
                    model.combank_bankcode = dr["COMBANK_BANKCODE"].ToString();
                    model.combank_bankaccount = dr["COMBANK_BANKACCOUNT"].ToString();
                    model.name_detail = dr["NAME_DETAIL"].ToString();

                    model.modified_by = dr["MODIFIED_BY"].ToString();
                    model.modified_date = Convert.ToDateTime(dr["MODIFIED_DATE"]);
                                                                                                                      
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Combank.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TRCombank> getDataByFillter(string language, string com)
        {
            string strCondition = "";
            strCondition += " AND COMPANY_CODE='" + com + "'";
            //strCondition += " AND COMBANK_BANKCODE IN (" + bankcode + ") ";

            return this.getData(language, strCondition);
        }

        public bool checkDataOld(string com, string bankcode)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT COMBANK_BANKCODE");
                obj_str.Append(" FROM HRM_TR_COMBANK");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND COMBANK_BANKCODE='" + bankcode + "'");
                                                
                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Combank.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }


        public bool delete(string id)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_TR_COMBANK");
                obj_str.Append(" WHERE COMBANK_ID='" + id + "'");
                                          
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Combank.delete)" + ex.ToString();
            }

            return blnResult;
        }

        public bool clear(string com)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_TR_COMBANK");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
               
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Combank.clear)" + ex.ToString();
            }

            return blnResult;
        }

        public int getNextID()
        {
            int intResult = 1;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT MAX(COMBANK_ID) ");
                obj_str.Append(" FROM HRM_TR_COMBANK");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    intResult = Convert.ToInt32(dt.Rows[0][0]) + 1;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Combank.getNextID)" + ex.ToString();
            }

            return intResult;
        }

        public bool insert(cls_TRCombank model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.company_code, model.combank_bankcode))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_COMBANK");
                obj_str.Append(" (");
                obj_str.Append("COMPANY_CODE ");               
                obj_str.Append(", COMBANK_ID ");
                obj_str.Append(", COMBANK_BANKCODE ");
                obj_str.Append(", COMBANK_BANKACCOUNT ");                                         
                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@COMPANY_CODE ");               
                obj_str.Append(", @COMBANK_ID ");
                obj_str.Append(", @COMBANK_BANKCODE ");
                obj_str.Append(", @COMBANK_BANKACCOUNT ");             
                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                
                obj_cmd.Parameters.Add("@COMBANK_ID", SqlDbType.Int); obj_cmd.Parameters["@COMBANK_ID"].Value = this.getNextID();
                obj_cmd.Parameters.Add("@COMBANK_BANKCODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMBANK_BANKCODE"].Value = model.combank_bankcode;
                obj_cmd.Parameters.Add("@COMBANK_BANKACCOUNT", SqlDbType.VarChar); obj_cmd.Parameters["@COMBANK_BANKACCOUNT"].Value = model.combank_bankaccount;
                                               
                obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@CREATED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;
     
                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();
                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Combank.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_TRCombank model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_TR_COMBANK SET ");

                obj_str.Append(" COMBANK_BANKCODE=@COMBANK_BANKCODE ");
                obj_str.Append(", COMBANK_BANKACCOUNT=@COMBANK_BANKACCOUNT ");
          
                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");
                obj_str.Append(", FLAG=@FLAG ");

                obj_str.Append(" WHERE COMBANK_ID=@COMBANK_ID ");
                               
                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@COMBANK_BANKCODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMBANK_BANKCODE"].Value = model.combank_bankcode;
                obj_cmd.Parameters.Add("@COMBANK_BANKACCOUNT", SqlDbType.VarChar); obj_cmd.Parameters["@COMBANK_BANKACCOUNT"].Value = model.combank_bankaccount;
                                                 
                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;

                obj_cmd.Parameters.Add("@COMBANK_ID", SqlDbType.Int); obj_cmd.Parameters["@COMBANK_ID"].Value = model.combank_id;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Combank.update)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
