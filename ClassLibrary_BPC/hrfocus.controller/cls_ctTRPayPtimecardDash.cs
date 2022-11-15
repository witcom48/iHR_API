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
    public class cls_ctTRPayPtimecardDash
    {
        string Message = string.Empty;
    

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTRPayPtimecardDash() { }

        public string getMessage() { return this.Message; }

        //private string FormatDateDB = "MM/dd/yyyy";

        public void dispose()
        {
            Obj_conn.doClose();
        }


        private List<cls_TRPayPtimecardDash> getData(string condition)
        {
            List<cls_TRPayPtimecardDash> list_model = new List<cls_TRPayPtimecardDash>();
            cls_TRPayPtimecardDash model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append("SUM(HRM_TR_PAYITEM.PAYITEM_AMOUNT) as AMOUNT");
                obj_str.Append(", HRM_MT_ITEM.ITEM_NAME_TH");
                obj_str.Append(", HRM_MT_ITEM.ITEM_NAME_EN");
                obj_str.Append(", HRM_TR_PAYITEM.ITEM_CODE");
                obj_str.Append(" from HRM_TR_PAYITEM");
                obj_str.Append(" inner join HRM_MT_ITEM on HRM_TR_PAYITEM.ITEM_CODE = HRM_MT_ITEM.ITEM_CODE");
                obj_str.Append(" where HRM_TR_PAYITEM.COMPANY_CODE = 'APT' AND HRM_MT_ITEM.ITEM_TYPE = 'DE'");

                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append("GROUP BY HRM_MT_ITEM.ITEM_NAME_TH,HRM_MT_ITEM.ITEM_NAME_EN,HRM_TR_PAYITEM.ITEM_CODE");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TRPayPtimecardDash();


                    model.WORKER_CODE = Convert.ToInt32(dr["WORKER_CODE"]);
                    model.ITEM_NAME_TH = dr["ITEM_NAME_TH"].ToString();
                    model.ITEM_NAME_EN = dr["ITEM_NAME_EN"].ToString();
                    model.ITEM_CODE = dr["ITEM_CODE"].ToString();


                    list_model.Add(model);
                }

            }
            catch (Exception ex)
            {
                Message = "ERROR::(Worker.getData)" + ex.ToString();
            }

            return list_model;

        }
    }
}



