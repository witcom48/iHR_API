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
    public class cls_ctTRShiftflexible
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTRShiftflexible() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TRShiftflexible> getData(string condition, string order)
        {
            List<cls_TRShiftflexible> list_model = new List<cls_TRShiftflexible>();
            cls_TRShiftflexible model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append(" HRM_TR_SHIFTFLEXIBLE.SHIFT_CODE");
                obj_str.Append(", PLANSHIFTFLEXIBLE_CODE");
                obj_str.Append(", HRM_TR_SHIFTFLEXIBLE.COMPANY_CODE");

                obj_str.Append(", ISNULL(SHIFT_NAME_TH, '') AS SHIFT_NAME_TH");
                obj_str.Append(", ISNULL(SHIFT_NAME_EN, '') AS SHIFT_NAME_EN");
                            
                obj_str.Append(" FROM HRM_TR_SHIFTFLEXIBLE");
                obj_str.Append(" INNER JOIN HRM_MT_SHIFT ON HRM_TR_SHIFTFLEXIBLE.COMPANY_CODE=HRM_MT_SHIFT.COMPANY_CODE AND HRM_TR_SHIFTFLEXIBLE.SHIFT_CODE=HRM_MT_SHIFT.SHIFT_CODE");

                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                if (!order.Equals(""))
                    obj_str.Append(" " + order);
                else
                    obj_str.Append(" ORDER BY PLANSHIFTFLEXIBLE_CODE, HRM_TR_SHIFTFLEXIBLE.SHIFT_CODE");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TRShiftflexible();

                    model.planshiftflexible_code = dr["PLANSHIFTFLEXIBLE_CODE"].ToString();
                    model.shift_code = dr["SHIFT_CODE"].ToString();
                    model.company_code = dr["COMPANY_CODE"].ToString();

                    model.shift_name_th = dr["SHIFT_NAME_TH"].ToString();
                    model.shift_name_en = dr["SHIFT_NAME_EN"].ToString();
                                                                                                                      
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(SHIFTFLEXIBLE.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TRShiftflexible> getDataByFillter(string com, string plan)
        {
            string strCondition = "";

            strCondition += " AND HRM_TR_SHIFTFLEXIBLE.COMPANY_CODE='" + com + "'";

            if (!plan.Equals(""))
                strCondition += " AND PLANSHIFTFLEXIBLE_CODE='" + plan + "'";
            
            return this.getData(strCondition, "");
        }

        public List<cls_TRShiftflexible> getDataByWorker(string com, string worker)
        {
            string strCondition = "";

            strCondition += " AND HRM_TR_SHIFTFLEXIBLE.COMPANY_CODE='" + com + "'";
            strCondition += " AND PLANSHIFTFLEXIBLE_CODE IN (SELECT EMPPOLATT_POLICY_CODE FROM HRM_TR_EMPPOLATT WHERE COMPANY_CODE='" + com + "' AND EMPPOLATT_POLICY_TYPE='FX' AND WORKER_CODE='" + worker + "')";

            return this.getData(strCondition, "");
        }

        public string getShiftFlexibel(string com, string worker, string timein, string timeout)
        {
            string strResult = "";
            string strCondition = "";

            strCondition += " AND HRM_TR_SHIFTFLEXIBLE.COMPANY_CODE='" + com + "'";
            strCondition += " AND PLANSHIFTFLEXIBLE_CODE IN (SELECT EMPPOLATT_POLICY_CODE FROM HRM_TR_EMPPOLATT WHERE COMPANY_CODE='" + com + "' AND EMPPOLATT_POLICY_TYPE='FX' AND WORKER_CODE='" + worker + "')";

            if (!timein.Equals(""))
                strCondition += " AND HRM_MT_SHIFT.SHIFT_CH3 >= '" + timein + "' ";
            
            List<cls_TRShiftflexible> list_model = this.getData(strCondition, " ORDER BY SHIFT_CH3 ");

            if (list_model.Count > 0)
            {
                strResult = list_model[0].shift_code;
            }
            
            return strResult;
        }

        

        public bool delete(string com, string plan)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_TR_SHIFTFLEXIBLE");
                obj_str.Append(" WHERE COMPANY_CODE ='" + com + "' ");
                obj_str.Append(" AND PLANSHIFTFLEXIBLE_CODE='" + plan + "'");
                                                             
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(SHIFTFLEXIBLE.delete)" + ex.ToString();
            }

            return blnResult;
        }
        
        public bool insert(string com, string plan, List<cls_TRShiftflexible> list_model)
        {
            bool blnResult = false;
            cls_ctConnection obj_conn = new cls_ctConnection();
            try
            {

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_conn.doConnect();

                obj_conn.doOpenTransaction();

                //-- Step 1 delete data old
                obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_TR_SHIFTFLEXIBLE");
                obj_str.Append(" WHERE COMPANY_CODE ='" + com + "' ");
                obj_str.Append(" AND PLANSHIFTFLEXIBLE_CODE='" + plan + "'");                

                blnResult = obj_conn.doExecuteSQL_transaction(obj_str.ToString());

                //-- Step 2 insert
                if (blnResult)
                {
                    obj_str = new System.Text.StringBuilder();
                    obj_str.Append("INSERT INTO HRM_TR_SHIFTFLEXIBLE");
                    obj_str.Append(" (");
                    obj_str.Append("COMPANY_CODE ");
                    obj_str.Append(", PLANSHIFTFLEXIBLE_CODE ");
                    obj_str.Append(", SHIFT_CODE ");  
                    obj_str.Append(" )");

                    obj_str.Append(" VALUES(");
                    obj_str.Append("@COMPANY_CODE ");
                    obj_str.Append(", @PLANSHIFTFLEXIBLE_CODE ");
                    obj_str.Append(", @SHIFT_CODE ");      
                    obj_str.Append(" )");

                    SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
                    obj_cmd.Transaction = obj_conn.getTransaction();

                    obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@PLANSHIFTFLEXIBLE_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@SHIFT_CODE", SqlDbType.VarChar);
                    
                                        
                    foreach (cls_TRShiftflexible model in list_model)
                    {

                        obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                        obj_cmd.Parameters["@PLANSHIFTFLEXIBLE_CODE"].Value = model.planshiftflexible_code;
                        obj_cmd.Parameters["@SHIFT_CODE"].Value = model.shift_code;
                                            
                        obj_cmd.ExecuteNonQuery();

                    }

                    blnResult = obj_conn.doCommit();

                }
                else
                {
                    obj_conn.doRollback();
                }

            }


            catch (Exception ex)
            {
                Message = "ERROR::(SHIFTFLEXIBLE.insert)" + ex.ToString();
                obj_conn.doRollback();
            }
            finally
            {
                obj_conn.doClose();
            }

            return blnResult;
        }
          
    }
}
