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
    public class cls_ctTRPayitem
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTRPayitem() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TRPayitem> getData(string language, string condition)
        {
            List<cls_TRPayitem> list_model = new List<cls_TRPayitem>();
            cls_TRPayitem model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("HRM_TR_PAYITEM.COMPANY_CODE");
                obj_str.Append(", HRM_TR_PAYITEM.WORKER_CODE");
                obj_str.Append(", HRM_TR_PAYITEM.ITEM_CODE");
                obj_str.Append(", PAYITEM_DATE");
                obj_str.Append(", PAYITEM_AMOUNT");
                obj_str.Append(", PAYITEM_QUANTITY");
                obj_str.Append(", PAYITEM_PAYTYPE");                
                obj_str.Append(", ISNULL(PAYITEM_NOTE, '') AS PAYITEM_NOTE");

                obj_str.Append(", ISNULL(HRM_TR_PAYITEM.MODIFIED_BY, HRM_TR_PAYITEM.CREATED_BY) AS MODIFIED_BY");
                obj_str.Append(", ISNULL(HRM_TR_PAYITEM.MODIFIED_DATE, HRM_TR_PAYITEM.CREATED_DATE) AS MODIFIED_DATE");


                obj_str.Append(", ITEM_TYPE");

                if (language.Equals("TH"))
                {
                    obj_str.Append(", ITEM_NAME_TH AS ITEM_DETAIL");
                    obj_str.Append(", INITIAL_NAME_TH + WORKER_FNAME_TH + ' ' + WORKER_LNAME_TH AS WORKER_DETAIL");
                }
                else
                {
                    obj_str.Append(", ITEM_NAME_EN AS ITEM_DETAIL");
                    obj_str.Append(", INITIAL_NAME_EN + WORKER_FNAME_EN + ' ' + WORKER_LNAME_EN AS WORKER_DETAIL");
                }


                obj_str.Append(" FROM HRM_TR_PAYITEM");

                obj_str.Append(" INNER JOIN HRM_MT_WORKER ON HRM_MT_WORKER.COMPANY_CODE=HRM_TR_PAYITEM.COMPANY_CODE AND HRM_MT_WORKER.WORKER_CODE=HRM_TR_PAYITEM.WORKER_CODE");
                obj_str.Append(" INNER JOIN HRM_MT_INITIAL ON HRM_MT_INITIAL.INITIAL_CODE=HRM_MT_WORKER.WORKER_INITIAL ");

                obj_str.Append(" INNER JOIN HRM_MT_ITEM ON HRM_MT_ITEM.COMPANY_CODE=HRM_TR_PAYITEM.COMPANY_CODE AND HRM_MT_ITEM.ITEM_CODE=HRM_TR_PAYITEM.ITEM_CODE");


                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY HRM_TR_PAYITEM.COMPANY_CODE, HRM_TR_PAYITEM.WORKER_CODE, PAYITEM_DATE DESC, ITEM_TYPE DESC, HRM_TR_PAYITEM.ITEM_CODE");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TRPayitem();

                    model.company_code = Convert.ToString(dr["COMPANY_CODE"]);
                    model.worker_code = Convert.ToString(dr["WORKER_CODE"]);
                    model.item_code = Convert.ToString(dr["ITEM_CODE"]);
                    model.payitem_date = Convert.ToDateTime(dr["PAYITEM_DATE"]);
                    model.payitem_amount = Convert.ToDouble(dr["PAYITEM_AMOUNT"]);
                    model.payitem_quantity = Convert.ToDouble(dr["PAYITEM_QUANTITY"]);
                    model.payitem_paytype = Convert.ToString(dr["PAYITEM_PAYTYPE"]);
                    
                    model.payitem_note = Convert.ToString(dr["PAYITEM_NOTE"]);

                    model.item_detail = Convert.ToString(dr["ITEM_DETAIL"]);
                    model.item_type = Convert.ToString(dr["ITEM_TYPE"]);
                    model.worker_detail = Convert.ToString(dr["WORKER_DETAIL"]);


                    model.modified_by = dr["MODIFIED_BY"].ToString();
                    model.modified_date = Convert.ToDateTime(dr["MODIFIED_DATE"]);

                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Payitem.getData)" + ex.ToString();
            }

            return list_model;
        }



        //
        private List<cls_TRPayitem> getVerify2Data(string language, string condition)
        {
            List<cls_TRPayitem> list_modelVerify2 = new List<cls_TRPayitem>();
            cls_TRPayitem model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("HRM_TR_PAYITEM.COMPANY_CODE");
                obj_str.Append(", HRM_TR_PAYITEM.WORKER_CODE");
                obj_str.Append(", HRM_TR_PAYITEM.ITEM_CODE");
                obj_str.Append(", HRM_TR_PAYITEM.PAYITEM_DATE");
                obj_str.Append(", HRM_TR_PAYITEM.PAYITEM_AMOUNT");
                obj_str.Append(", HRM_TR_PAYITEM.PAYITEM_QUANTITY");
                obj_str.Append(", HRM_TR_PAYITEM.PAYITEM_PAYTYPE");

                //obj_str.Append(", HRM_MT_WORKER.WORKER_RESIGNDATE");
                obj_str.Append(", ISNULL(HRM_MT_WORKER.WORKER_RESIGNDATE, '01/01/2999') AS WORKER_RESIGNDATE");
                obj_str.Append(", ISNULL(HRM_MT_WORKER.WORKER_RESIGNSTATUS, '0') AS WORKER_RESIGNSTATUS");

                obj_str.Append(", HRM_MT_WORKER.WORKER_HIREDATE");



                obj_str.Append(", SUM(CASE WHEN HRM_TR_PAYITEM.PAYITEM_DATE = '12/25/2023' THEN HRM_TR_PAYITEM.PAYITEM_AMOUNT ELSE 0 END) AS AMOUNT1");
                obj_str.Append(", SUM(CASE WHEN HRM_TR_PAYITEM.PAYITEM_DATE = '11/24/2023' THEN HRM_TR_PAYITEM.PAYITEM_AMOUNT ELSE 0 END) AS AMOUNT2");






                obj_str.Append(", ISNULL(HRM_TR_PAYITEM.PAYITEM_NOTE, '') AS PAYITEM_NOTE");

                obj_str.Append(", ISNULL(HRM_TR_PAYITEM.MODIFIED_BY, HRM_TR_PAYITEM.CREATED_BY) AS MODIFIED_BY");
                obj_str.Append(", ISNULL(HRM_TR_PAYITEM.MODIFIED_DATE, HRM_TR_PAYITEM.CREATED_DATE) AS MODIFIED_DATE");


                obj_str.Append(", ITEM_TYPE");

                if (language.Equals("TH"))
                {
                    obj_str.Append(", ITEM_NAME_TH AS ITEM_DETAIL");
                    obj_str.Append(", INITIAL_NAME_TH + WORKER_FNAME_TH + ' ' + WORKER_LNAME_TH AS WORKER_DETAIL");
                }
                else
                {
                    obj_str.Append(", ITEM_NAME_EN AS ITEM_DETAIL");
                    obj_str.Append(", INITIAL_NAME_EN + WORKER_FNAME_EN + ' ' + WORKER_LNAME_EN AS WORKER_DETAIL");
                }


                obj_str.Append(" FROM HRM_TR_PAYITEM");

                obj_str.Append(" INNER JOIN HRM_MT_WORKER ON HRM_MT_WORKER.COMPANY_CODE=HRM_TR_PAYITEM.COMPANY_CODE AND HRM_MT_WORKER.WORKER_CODE=HRM_TR_PAYITEM.WORKER_CODE");
                obj_str.Append(" INNER JOIN HRM_MT_INITIAL ON HRM_MT_INITIAL.INITIAL_CODE=HRM_MT_WORKER.WORKER_INITIAL ");

                obj_str.Append(" INNER JOIN HRM_MT_ITEM ON HRM_MT_ITEM.COMPANY_CODE=HRM_TR_PAYITEM.COMPANY_CODE AND HRM_MT_ITEM.ITEM_CODE=HRM_TR_PAYITEM.ITEM_CODE");


                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" GROUP BY HRM_TR_PAYITEM.COMPANY_CODE, HRM_TR_PAYITEM.WORKER_CODE, HRM_TR_PAYITEM.ITEM_CODE, HRM_TR_PAYITEM.PAYITEM_DATE,HRM_TR_PAYITEM.PAYITEM_AMOUNT, HRM_TR_PAYITEM.PAYITEM_QUANTITY, HRM_TR_PAYITEM.PAYITEM_PAYTYPE, HRM_TR_PAYITEM.PAYITEM_NOTE,HRM_TR_PAYITEM.MODIFIED_BY, HRM_TR_PAYITEM.MODIFIED_DATE,  ITEM_TYPE,HRM_MT_ITEM.ITEM_NAME_TH, HRM_MT_ITEM.ITEM_NAME_EN,HRM_MT_INITIAL.INITIAL_NAME_TH, HRM_MT_INITIAL.INITIAL_NAME_EN,HRM_MT_WORKER.WORKER_FNAME_TH, HRM_MT_WORKER.WORKER_LNAME_TH,HRM_MT_WORKER.WORKER_FNAME_EN, HRM_MT_WORKER.WORKER_LNAME_EN,HRM_TR_PAYITEM.CREATED_BY,HRM_TR_PAYITEM.CREATED_DATE,HRM_MT_WORKER.WORKER_RESIGNDATE,HRM_MT_WORKER.WORKER_HIREDATE,HRM_MT_WORKER.WORKER_RESIGNSTATUS");


                obj_str.Append(" ORDER BY HRM_TR_PAYITEM.COMPANY_CODE, HRM_TR_PAYITEM.WORKER_CODE, PAYITEM_DATE DESC, ITEM_TYPE DESC, HRM_TR_PAYITEM.ITEM_CODE");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TRPayitem();

                    model.company_code = Convert.ToString(dr["COMPANY_CODE"]);
                    model.worker_code = Convert.ToString(dr["WORKER_CODE"]);
                    model.item_code = Convert.ToString(dr["ITEM_CODE"]);
                    model.payitem_date = Convert.ToDateTime(dr["PAYITEM_DATE"]);
                    model.payitem_amount = Convert.ToDouble(dr["PAYITEM_AMOUNT"]);
                    model.payitem_quantity = Convert.ToDouble(dr["PAYITEM_QUANTITY"]);
                    model.payitem_paytype = Convert.ToString(dr["PAYITEM_PAYTYPE"]);

                    model.payitem_note = Convert.ToString(dr["PAYITEM_NOTE"]);

                    model.item_detail = Convert.ToString(dr["ITEM_DETAIL"]);
                    model.item_type = Convert.ToString(dr["ITEM_TYPE"]);
                    model.worker_detail = Convert.ToString(dr["WORKER_DETAIL"]);

                    model.amount1 = Convert.ToDouble(dr["AMOUNT1"]);
                    model.amount2 = Convert.ToDouble(dr["AMOUNT2"]);



                    model.worker_resigndate = Convert.ToDateTime(dr["WORKER_RESIGNDATE"]);
                    model.worker_resignstatus = Convert.ToBoolean(dr["WORKER_RESIGNSTATUS"]);

                    model.worker_hiredate = Convert.ToDateTime(dr["WORKER_HIREDATE"]);



                    model.modified_by = dr["MODIFIED_BY"].ToString();
                    model.modified_date = Convert.ToDateTime(dr["MODIFIED_DATE"]);

                    list_modelVerify2.Add(model);
                }

            }
            catch (Exception ex)
            {
                Message = "ERROR::(Payitem.getData)" + ex.ToString();
            }

            return list_modelVerify2;
        }

        public List<cls_TRPayitem> getDataByFillterVerify2(string language, string com, string emp, string item)
        {
            string strCondition = "";

            strCondition += " AND HRM_TR_PAYITEM.COMPANY_CODE='" + com + "'";

            if (!emp.Equals(""))
                strCondition += " AND HRM_TR_PAYITEM.WORKER_CODE='" + emp + "'";

            if (!item.Equals(""))
                strCondition += " AND HRM_TR_PAYITEM.ITEM_CODE='" + item + "'";



            return this.getVerify2Data(language, strCondition);
        }



        
        //
        //
        private List<cls_TRPayitem> getVerifyData(string language, string condition)
        {
            List<cls_TRPayitem> list_modelVerify = new List<cls_TRPayitem>();
            cls_TRPayitem model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("HRM_TR_PAYITEM.COMPANY_CODE");

                obj_str.Append(", HRM_TR_PAYITEM.ITEM_CODE");
                obj_str.Append(", SUM(CASE WHEN HRM_TR_PAYITEM.PAYITEM_DATE = '12/25/2023' THEN HRM_TR_PAYITEM.PAYITEM_AMOUNT ELSE 0 END) AS AMOUNT1");
                obj_str.Append(",  SUM(CASE WHEN HRM_TR_PAYITEM.PAYITEM_DATE = '11/24/2023' THEN HRM_TR_PAYITEM.PAYITEM_AMOUNT ELSE 0 END) AS AMOUNT2");


                if (language.Equals("TH"))
                {
                    obj_str.Append(", ITEM_NAME_TH AS ITEM_DETAIL");
                 }
                else
                {
                    obj_str.Append(", ITEM_NAME_EN AS ITEM_DETAIL");
                 }
                obj_str.Append(",  ISNULL(HRM_TR_VERIFY.VERIFY_STATUS, '0') AS VERIFY_STATUS ");

                obj_str.Append(", ISNULL( HRM_TR_VERIFY_LOGS.CREATE_BY, ' ') AS CREATE_BY ");
                obj_str.Append(", ISNULL( HRM_TR_VERIFY_LOGS.CREATE_DATE, ' ') AS CREATE_DATE ");


                obj_str.Append(" FROM HRM_TR_PAYITEM");
                obj_str.Append(" INNER JOIN HRM_MT_ITEM ON HRM_MT_ITEM.COMPANY_CODE=HRM_TR_PAYITEM.COMPANY_CODE AND HRM_MT_ITEM.ITEM_CODE=HRM_TR_PAYITEM.ITEM_CODE");
                obj_str.Append(" LEFT JOIN HRM_TR_VERIFY ON HRM_TR_VERIFY.COMPANY_CODE = HRM_TR_PAYITEM.COMPANY_CODE   AND HRM_TR_VERIFY.ITEM_CODE = HRM_TR_PAYITEM.ITEM_CODE ");

                obj_str.Append(" LEFT JOIN HRM_TR_VERIFY_LOGS ON HRM_TR_VERIFY_LOGS.COMPANY_CODE = HRM_TR_PAYITEM.COMPANY_CODE   AND HRM_TR_VERIFY_LOGS.ITEM_CODE = HRM_TR_PAYITEM.ITEM_CODE");

                obj_str.Append(" WHERE 1=1");
                obj_str.Append(" AND HRM_TR_PAYITEM.PAYITEM_DATE in ('12/25/2023' ,'11/24/2023') ");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" GROUP BY HRM_TR_PAYITEM.COMPANY_CODE,HRM_TR_PAYITEM.ITEM_CODE, HRM_MT_ITEM.ITEM_NAME_TH,HRM_MT_ITEM.ITEM_NAME_EN,HRM_TR_PAYITEM.PAYITEM_DATE, HRM_TR_VERIFY.VERIFY_STATUS,HRM_TR_VERIFY_LOGS.CREATE_BY,HRM_TR_VERIFY_LOGS.CREATE_DATE");

                obj_str.Append(" ORDER BY HRM_TR_PAYITEM.ITEM_CODE");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TRPayitem();

                    model.company_code = Convert.ToString(dr["COMPANY_CODE"]);
                     model.item_code = Convert.ToString(dr["ITEM_CODE"]);
                     model.amount1 = Convert.ToDouble(dr["AMOUNT1"]);
                     model.amount2 = Convert.ToDouble(dr["AMOUNT2"]);

                    model.item_detail = Convert.ToString(dr["ITEM_DETAIL"]);
                    model.status = Convert.ToString(dr["VERIFY_STATUS"]);
                    model.create_by = Convert.ToString(dr["CREATE_BY"]);
                    model.create_date = Convert.ToDateTime(dr["CREATE_DATE"]);

                    
                     list_modelVerify.Add(model);
                }

            }
            catch (Exception ex)
            {
                Message = "ERROR::(Payitem.getData)" + ex.ToString();
            }

            return list_modelVerify;
        }

        public List<cls_TRPayitem> getDataByVerifyFillter(string language, string com, string emp, DateTime date, string item_type, string item )
        {
            string strCondition = "";

            strCondition += " AND HRM_TR_PAYITEM.COMPANY_CODE='" + com + "'";
            //strCondition += " AND HRM_TR_PAYITEM.PAYITEM_DATE='" + date.ToString("MM/dd/yyyy") + "'";

            if (!emp.Equals(""))
                strCondition += " AND HRM_TR_PAYITEM.WORKER_CODE='" + emp + "'";

            if (!item.Equals(""))
                strCondition += " AND HRM_TR_PAYITEM.ITEM_CODE='" + item + "'";

            //if (!item_type.Equals(""))
            //{
            //    strCondition += " AND HRM_TR_PAYITEM.ITEM_CODE IN (SELECT ITEM_CODE FROM HRM_MT_ITEM WHERE COMPANY_CODE='" + com + "' AND ITEM_TYPE='" + item_type + "')";
            //}

            //if (!status.Equals(""))
            //{
            //    strCondition += " HRM_TR_VERIFY.VERIFY_STATUS='" + status + "')";
            //}


            return this.getVerifyData(language, strCondition);
        }
        //
        public List<cls_TRPayitem> getDataByFillter(string language, string com, string emp, DateTime date, string item_type, string item)
        {
            string strCondition = "";

            strCondition += " AND HRM_TR_PAYITEM.COMPANY_CODE='" + com + "'";
            strCondition += " AND HRM_TR_PAYITEM.PAYITEM_DATE='" + date.ToString("MM/dd/yyyy") + "'";

            if (!emp.Equals(""))
                strCondition += " AND HRM_TR_PAYITEM.WORKER_CODE='" + emp + "'";

            if (!item.Equals(""))
                strCondition += " AND HRM_TR_PAYITEM.ITEM_CODE='" + item + "'";

            if (!item_type.Equals(""))
            {
                strCondition += " AND HRM_TR_PAYITEM.ITEM_CODE IN (SELECT ITEM_CODE FROM HRM_MT_ITEM WHERE COMPANY_CODE='" + com + "' AND ITEM_TYPE='" + item_type + "')";
            }

            return this.getData(language, strCondition);
        }


        public List<cls_TRPayitem> getDataByFillter2(string language, string com, string emp,    string item)
        {
            string strCondition = "";

            strCondition += " AND HRM_TR_PAYITEM.COMPANY_CODE='" + com + "'";
 
            if (!emp.Equals(""))
                strCondition += " AND HRM_TR_PAYITEM.WORKER_CODE='" + emp + "'";

            if (!item.Equals(""))
                strCondition += " AND HRM_TR_PAYITEM.ITEM_CODE='" + item + "'";

             

            return this.getData(language, strCondition);
        }
        public List<cls_TRPayitem> getDataByCreateDate(string language, string com, string emp, DateTime date, DateTime datecreate)
        {
            string strCondition = "";

            strCondition += " AND HRM_TR_PAYITEM.COMPANY_CODE='" + com + "'";
            strCondition += " AND HRM_TR_PAYITEM.PAYITEM_DATE='" + date.ToString("MM/dd/yyyy") + "'";

            if (!emp.Equals(""))
                strCondition += " AND HRM_TR_PAYITEM.WORKER_CODE='" + emp + "'";

            strCondition += " AND (HRM_TR_PAYITEM.CREATED_DATE >= '" + datecreate.ToString("MM/dd/yyyy") + "' OR HRM_TR_PAYITEM.MODIFIED_DATE >= '" + datecreate.ToString("MM/dd/yyyy") + "')";



            return this.getData(language, strCondition);
        }
        public List<cls_TRPayitem> getDataitemMultipleEmp(string language, string com, string worker, DateTime date, string item_type, string item)
        {
            string strCondition = "";

            strCondition += " AND HRM_TR_PAYITEM.COMPANY_CODE='" + com + "'";
            strCondition += " AND HRM_TR_PAYITEM.PAYITEM_DATE='" + date.ToString("MM/dd/yyyy") + "'";

            strCondition += " AND HRM_TR_PAYITEM.WORKER_CODE IN (" + worker + ") ";

            if (!item.Equals(""))
                strCondition += " AND HRM_TR_PAYITEM.ITEM_CODE='" + item + "'";

            if (!item_type.Equals(""))
            {
                strCondition += " AND HRM_TR_PAYITEM.ITEM_CODE IN (SELECT ITEM_CODE FROM HRM_MT_ITEM WHERE COMPANY_CODE='" + com + "' AND ITEM_CODE='" + item_type + "')";
            }
            return this.getData(language, strCondition);
        }

        //
        //public List<cls_TRPayitem> getVerifyDataByFillter(string language, string com, string emp, DateTime date, string item_type, string item)
        //{
        //    string strCondition = "";

        //    strCondition += " AND HRM_TR_PAYITEM.COMPANY_CODE='" + com + "'";
        //    strCondition += " AND HRM_TR_PAYITEM.PAYITEM_DATE='" + date.ToString("MM/dd/yyyy") + "'";

        //    //if (!emp.Equals(""))
        //    //    strCondition += " AND HRM_TR_PAYITEM.WORKER_CODE='" + emp + "'";

        //    //if (!item.Equals(""))
        //    //    strCondition += " AND HRM_TR_PAYITEM.ITEM_CODE='" + item + "'";

        //    //if (!item_type.Equals(""))
        //    //{
        //    //    strCondition += " AND HRM_TR_PAYITEM.ITEM_CODE IN (SELECT DISTINCT ITEM_CODE FROM HRM_MT_ITEM WHERE COMPANY_CODE='" + com + "' AND ITEM_TYPE='" + item_type + "') AND HRM_TR_PAYITEM.ITEM_CODE IS NOT NULL";
        //    //}
        //    //else 
        //    {
        //        strCondition += " AND HRM_TR_PAYITEM.ITEM_CODE IS NOT NULL";
        //    }

        //    return this.getData(language, strCondition);
        //}

        //
        public bool checkDataOld(string com, string emp, string item, DateTime date)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ITEM_CODE");
                obj_str.Append(" FROM HRM_TR_PAYITEM");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "' ");
                obj_str.Append(" AND WORKER_CODE='" + emp + "' ");
                obj_str.Append(" AND ITEM_CODE='" + item + "' ");
                obj_str.Append(" AND PAYITEM_DATE='" + date.ToString("MM/dd/yyyy") + "' ");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Payitem.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }
                
        public bool delete(string com, string emp, string item, DateTime date)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_TR_PAYITEM");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "' ");
                obj_str.Append(" AND WORKER_CODE='" + emp + "' ");
                obj_str.Append(" AND ITEM_CODE='" + item + "' ");
                obj_str.Append(" AND PAYITEM_DATE='" + date.ToString("MM/dd/yyyy") + "' ");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());
                

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Payitem.delete)" + ex.ToString();
            }

            return blnResult;
        }
        public bool delete(string com, string emp, DateTime date)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_TR_PAYITEM");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "' ");
                obj_str.Append(" AND WORKER_CODE='" + emp + "' ");
                obj_str.Append(" AND PAYITEM_DATE='" + date.ToString("MM/dd/yyyy") + "' ");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());


            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Payitem.delete)" + ex.ToString();
            }

            return blnResult;
        }
        public bool delete(string com, string emp)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_TR_PAYITEM");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "' ");
                obj_str.Append(" AND WORKER_CODE='" + emp + "' ");
                
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());
            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Payitem.delete)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(cls_TRPayitem model)
        {
            bool blnResult = false;
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.company_code, model.worker_code, model.item_code, model.payitem_date))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_PAYITEM");
                obj_str.Append(" (");
                obj_str.Append("COMPANY_CODE ");
                obj_str.Append(", WORKER_CODE ");
                obj_str.Append(", ITEM_CODE ");
                obj_str.Append(", PAYITEM_DATE ");
                obj_str.Append(", PAYITEM_AMOUNT ");
                obj_str.Append(", PAYITEM_QUANTITY ");
                obj_str.Append(", PAYITEM_PAYTYPE ");                
                obj_str.Append(", PAYITEM_NOTE ");                
                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@COMPANY_CODE ");
                obj_str.Append(", @WORKER_CODE ");
                obj_str.Append(", @ITEM_CODE ");
                obj_str.Append(", @PAYITEM_DATE ");
                obj_str.Append(", @PAYITEM_AMOUNT ");
                obj_str.Append(", @PAYITEM_QUANTITY ");
                obj_str.Append(", @PAYITEM_PAYTYPE ");
                obj_str.Append(", @PAYITEM_NOTE ");
                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                obj_cmd.Parameters.Add("@ITEM_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@ITEM_CODE"].Value = model.item_code;
                obj_cmd.Parameters.Add("@PAYITEM_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@PAYITEM_DATE"].Value = model.payitem_date;
                obj_cmd.Parameters.Add("@PAYITEM_AMOUNT", SqlDbType.Decimal); obj_cmd.Parameters["@PAYITEM_AMOUNT"].Value = model.payitem_amount;
                obj_cmd.Parameters.Add("@PAYITEM_QUANTITY", SqlDbType.Decimal); obj_cmd.Parameters["@PAYITEM_QUANTITY"].Value = model.payitem_quantity;
                obj_cmd.Parameters.Add("@PAYITEM_PAYTYPE", SqlDbType.VarChar); obj_cmd.Parameters["@PAYITEM_PAYTYPE"].Value = model.payitem_paytype;                
                obj_cmd.Parameters.Add("@PAYITEM_NOTE", SqlDbType.VarChar); obj_cmd.Parameters["@PAYITEM_NOTE"].Value = model.payitem_note;
                
                obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@CREATED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {              
                Message = "ERROR::(Payitem.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(string com, string emp, DateTime date, List<cls_TRPayitem> list_model)
        {
            bool blnResult = false;
            cls_ctConnection obj_conn = new cls_ctConnection();
            try
            {
                
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_PAYITEM");
                obj_str.Append(" (");
                obj_str.Append("COMPANY_CODE ");
                obj_str.Append(", WORKER_CODE ");
                obj_str.Append(", ITEM_CODE ");
                obj_str.Append(", PAYITEM_DATE ");
                obj_str.Append(", PAYITEM_AMOUNT ");
                obj_str.Append(", PAYITEM_QUANTITY ");
                obj_str.Append(", PAYITEM_PAYTYPE ");
                obj_str.Append(", PAYITEM_NOTE ");
                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@COMPANY_CODE ");
                obj_str.Append(", @WORKER_CODE ");
                obj_str.Append(", @ITEM_CODE ");
                obj_str.Append(", @PAYITEM_DATE ");
                obj_str.Append(", @PAYITEM_AMOUNT ");
                obj_str.Append(", @PAYITEM_QUANTITY ");
                obj_str.Append(", @PAYITEM_PAYTYPE ");
                obj_str.Append(", @PAYITEM_NOTE ");
                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                obj_conn.doOpenTransaction();

                System.Text.StringBuilder obj_str2 = new System.Text.StringBuilder();

                obj_str2.Append(" DELETE FROM HRM_TR_PAYITEM");                
                obj_str2.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str2.Append(" AND WORKER_CODE='" + emp + "'");
                obj_str2.Append(" AND PAYITEM_DATE='" + date.ToString("MM/dd/yyyy") + "'");

                blnResult = obj_conn.doExecuteSQL_transaction(obj_str2.ToString());

                if (blnResult)
                {
                    SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
                    obj_cmd.Transaction = obj_conn.getTransaction();

                    obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); 
                    obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@ITEM_CODE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@PAYITEM_DATE", SqlDbType.DateTime);
                    obj_cmd.Parameters.Add("@PAYITEM_AMOUNT", SqlDbType.Decimal);
                    obj_cmd.Parameters.Add("@PAYITEM_QUANTITY", SqlDbType.Decimal);
                    obj_cmd.Parameters.Add("@PAYITEM_PAYTYPE", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@PAYITEM_NOTE", SqlDbType.VarChar);

                    obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime);
                    obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit);

                    foreach (cls_TRPayitem model in list_model)
                    {

                        obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                        obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                        obj_cmd.Parameters["@ITEM_CODE"].Value = model.item_code;
                        obj_cmd.Parameters["@PAYITEM_DATE"].Value = model.payitem_date;
                        obj_cmd.Parameters["@PAYITEM_AMOUNT"].Value = model.payitem_amount;
                        obj_cmd.Parameters["@PAYITEM_QUANTITY"].Value = model.payitem_quantity;
                        obj_cmd.Parameters["@PAYITEM_PAYTYPE"].Value = model.payitem_paytype;
                        obj_cmd.Parameters["@PAYITEM_NOTE"].Value = model.payitem_note;

                        obj_cmd.Parameters["@CREATED_BY"].Value = model.modified_by;
                        obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                        obj_cmd.Parameters["@FLAG"].Value = false;

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
                

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Payitem.insert)" + ex.ToString();
                obj_conn.doRollback();
            }
            finally
            {
                obj_conn.doClose();
            }

            return blnResult;
        }

        public bool update(cls_TRPayitem model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_TR_PAYITEM SET ");
                
                obj_str.Append("PAYITEM_AMOUNT=@PAYITEM_AMOUNT ");
                obj_str.Append(", PAYITEM_QUANTITY=@PAYITEM_QUANTITY ");
                obj_str.Append(", PAYITEM_PAYTYPE=@PAYITEM_PAYTYPE ");
                obj_str.Append(", PAYITEM_NOTE=@PAYITEM_NOTE ");  

                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");

                obj_str.Append(" WHERE COMPANY_CODE=@COMPANY_CODE ");
                obj_str.Append(" AND WORKER_CODE=@WORKER_CODE ");
                obj_str.Append(" AND ITEM_CODE=@ITEM_CODE ");
                obj_str.Append(" AND PAYITEM_DATE=@PAYITEM_DATE ");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@PAYITEM_AMOUNT", SqlDbType.Decimal); obj_cmd.Parameters["@PAYITEM_AMOUNT"].Value = model.payitem_amount;
                obj_cmd.Parameters.Add("@PAYITEM_QUANTITY", SqlDbType.Decimal); obj_cmd.Parameters["@PAYITEM_QUANTITY"].Value = model.payitem_quantity;
                obj_cmd.Parameters.Add("@PAYITEM_PAYTYPE", SqlDbType.VarChar); obj_cmd.Parameters["@PAYITEM_PAYTYPE"].Value = model.payitem_paytype;
                obj_cmd.Parameters.Add("@PAYITEM_NOTE", SqlDbType.VarChar); obj_cmd.Parameters["@PAYITEM_NOTE"].Value = model.payitem_note;

                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = DateTime.Now;

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@WORKER_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@WORKER_CODE"].Value = model.worker_code;
                obj_cmd.Parameters.Add("@ITEM_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@ITEM_CODE"].Value = model.item_code;
                obj_cmd.Parameters.Add("@PAYITEM_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@PAYITEM_DATE"].Value = model.payitem_date;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Payitem.update)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
