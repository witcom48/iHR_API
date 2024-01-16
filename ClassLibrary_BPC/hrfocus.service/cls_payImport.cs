using ClassLibrary_BPC.hrfocus.controller;
using ClassLibrary_BPC.hrfocus.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.service
{
    public class cls_payImport
  {
 
        public string TEST = "";
        private static DataTable doConvertCSVtoDataTable(string path)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(path))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }

            }

            return dt;
        }

        public DataTable doReadExcel(string fileName)
        {
            DataTable dt = new DataTable();
            string filePath = Path.Combine(ClassLibrary_BPC.Config.PathFileImport + "\\Imports\\", fileName);
            string xlConnStr = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=Yes;';";
            var xlConn = new OleDbConnection(xlConnStr);

            try
            {

                var da = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", xlConn);
          
                da.Fill(dt);
                

            }
            catch (Exception ex)
            {
                TEST = ex.ToString();
            }
            finally
            {
                xlConn.Close();
            }

            return dt;
        }

        public string doImportPayExcel(string com, string taskid)
        {
            string strResult = "";
            string ggg = "";
            cls_ctMTTask objMTTask = new cls_ctMTTask();
            List<cls_MTTask> listMTTask = objMTTask.getDataByFillter(com, taskid, "PAY_XLS", "");
            List<string> listError = new List<string>();

            if (listMTTask.Count > 0)
            {
                cls_MTTask task = listMTTask[0];
                try
                {
                task.task_start = DateTime.Now;

                cls_ctMTTask objTaskDetail = new cls_ctMTTask();
                cls_TRTaskdetail task_detail = objTaskDetail.getTaskDetail(task.task_id.ToString());

                DateTime dateFrom = task_detail.taskdetail_fromdate;
                DateTime dateTo = task_detail.taskdetail_todate;

                string fileName = task_detail.taskdetail_process;


                    string import_code = fileName.Substring(0, 5);

                    int success = 0;
                    StringBuilder objStr = new StringBuilder();

                    switch (import_code)
                    {
                        case "pitem":
                            try
                            {
                                DataTable dt = doReadExcel(fileName);
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    cls_ctTRPayitem objTRPayitem = new cls_ctTRPayitem();
                                    cls_TRPayitem model = new cls_TRPayitem();
                                    if (dr["com"].ToString().Equals(""))
                                        continue;
                                    model.company_code = dr["com"].ToString();
                                    model.worker_code = dr["emp_code"].ToString();
                                    model.item_code = dr["item_code"].ToString();
                                    model.payitem_date = Convert.ToDateTime(dr["payitem_date"]);
                                    model.payitem_amount = Convert.ToDouble(dr["payitem_amount"]);
                                    model.payitem_quantity = Convert.ToDouble(dr["payitem_quantity"]);
                                    model.payitem_paytype = dr["payitem_paytype"].ToString();
                                    model.payitem_note = dr["payitem_note"].ToString();
                                    model.modified_by = task.modified_by;
                                    model.flag = model.flag;

                                    bool strID = objTRPayitem.insert(model);
                                    if (strID.Equals("limit")){
                                        objStr.Append("Limit License");
                                        ggg += strID;
                                        break;
                                    }

                                    if (strID.Equals("yes"))
                                    {
                                        success++;
                                    }
                                    else
                                    {
                                        objStr.Append(model.company_code + "-" + model.worker_code);
                                    }

                                }

                                strResult = "TTTT" + ggg;

                                if (success > 0)
                                    strResult += "Success : " + success.ToString();

                                if (objStr.Length > 0)
                                    strResult += " Fail : " + objStr.ToString();

                            }
                            }
                            catch (Exception ex)
                            {
                                strResult = "ERROR::(Read Xcel)" + ex.ToString();
                            }
                            strResult = TEST;
                            break;
                        }
                    }

                
                catch (Exception ex)
                {
                    strResult = ex.ToString();
                }

                task.task_end = DateTime.Now;
                task.task_status = "F";
                task.task_note = strResult;
                objMTTask.updateStatus(task);
            }
            else
            {
                strResult = "Task not found::" + taskid;
            }

            return strResult;
        }
    }

}
