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
    public class cls_srvImport
    {

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

            }
            finally
            {
                xlConn.Close();
            }

            return dt;
        }

        public string doImportExcel(string com, string taskid)
        {
            string strResult = "";

            bool blnResult = false;

            cls_ctMTTask objMTTask = new cls_ctMTTask();
            List<cls_MTTask> listMTTask = objMTTask.getDataByFillter(com, taskid, "IMP_XLS", "");
            List<string> listError = new List<string>();

            if (listMTTask.Count > 0)
            {
                cls_MTTask task = listMTTask[0];

                task.task_start = DateTime.Now;

                cls_ctMTTask objTaskDetail = new cls_ctMTTask();
                cls_TRTaskdetail task_detail = objTaskDetail.getTaskDetail(task.task_id.ToString());

                DateTime dateFrom = task_detail.taskdetail_fromdate;
                DateTime dateTo = task_detail.taskdetail_todate;

                string fileName = task_detail.taskdetail_process;
                                
                try
                {
                    string import_code = fileName.Substring(0, 5);

                    int success = 0;
                    StringBuilder objStr = new StringBuilder();

                    switch (import_code)
                    {
                        case "EM001":

                            DataTable dt = doReadExcel(fileName);
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    //com	emp_code	emp_card	initial_code	firstname_th	lastname_th	firstname_en	lastname_en	emptype_code	emp_gender	emp_birthday	emp_startdate	
                                    //emp_resign_status	emp_resign_date	emp_resign_reason_code	emp_probationstart	emp_probationend	emp_hrs	emp_taxmethod


                                    cls_ctMTWorker objWorker = new cls_ctMTWorker();
                                    cls_MTWorker model = new cls_MTWorker();

                                    model.company_code = dr["com"].ToString();

                                    model.worker_code = dr["emp_code"].ToString();
                                    model.worker_card = dr["emp_card"].ToString();
                                    model.worker_initial = dr["initial_code"].ToString();
                                    model.worker_fname_th = dr["firstname_th"].ToString();
                                    model.worker_lname_th = dr["lastname_th"].ToString();
                                    model.worker_fname_en = dr["firstname_en"].ToString();
                                    model.worker_lname_en = dr["lastname_en"].ToString();
                                    model.worker_emptype = dr["emptype_code"].ToString();
                                    model.worker_gender = dr["emp_gender"].ToString();
                                    model.worker_birthdate = Convert.ToDateTime(dr["emp_birthday"]);
                                    model.worker_hiredate = Convert.ToDateTime(dr["emp_startdate"]);

                                    if (dr["emp_resign_status"].ToString().Equals("1"))
                                    {
                                        model.worker_resigndate = Convert.ToDateTime(dr["emp_resign_date"]);
                                        model.worker_resignstatus = true;
                                        model.worker_resignreason = dr["emp_resign_reason_code"].ToString();
                                    }
                                    else
                                    {
                                        model.worker_resignstatus = false;
                                    }
                                    
                                    model.worker_probationdate = Convert.ToDateTime(dr["emp_probationstart"]);
                                    model.worker_probationenddate = Convert.ToDateTime(dr["emp_probationend"]);
                                    model.hrs_perday = Convert.ToDouble(dr["emp_hrs"]);
                                    model.worker_taxmethod = dr["emp_taxmethod"].ToString();
                                    model.worker_pwd = "+PH1MsvnDonmqUuzB4TZ8g==";
                                    model.self_admin = false;

                                    model.modified_by = task.modified_by;
                                    model.flag = model.flag;

                                    string strID = objWorker.insert(model);

                                    if (!strID.Equals(""))
                                    {
                                        success++;
                                    }
                                    else
                                    {
                                        objStr.Append(model.company_code + "-" + model.worker_code);
                                    }

                                }

                                strResult = "";

                                if (success > 0)
                                    strResult += "Success : " + success.ToString();

                                if(objStr.Length > 0 )
                                    strResult += " Fail : " + objStr.ToString();

                            }

                            break;

                        case "EM002":
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
