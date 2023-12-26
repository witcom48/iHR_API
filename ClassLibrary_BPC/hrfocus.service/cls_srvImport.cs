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
//using c.Excel;
namespace ClassLibrary_BPC.hrfocus.service
{
    public class cls_srvImport
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

        public string doImportExcel(string com, string taskid)
        {
            string strResult = "";
            string ggg = "";
            cls_ctMTTask objMTTask = new cls_ctMTTask();
            List<cls_MTTask> listMTTask = objMTTask.getDataByFillter(com, taskid, "IMP_XLS", "");
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
                        case "EM001":
                            try
                            {
                                DataTable dt = doReadExcel(fileName);
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    //com	emp_code	emp_card	initial_code	firstname_th	lastname_th	firstname_en	lastname_en	emptype_code	emp_gender	emp_birthday	emp_startdate	
                                    //emp_resign_status	emp_resign_date	emp_resign_reason_code	emp_probationstart	emp_probationend	emp_hrs	emp_taxmethod


                                    cls_ctMTWorker objWorker = new cls_ctMTWorker();
                                    cls_MTWorker model = new cls_MTWorker();

                                    if (dr["com"].ToString().Equals(""))
                                        continue;
                                    model.company_code = dr["com"].ToString();

                                    model.worker_code = dr["emp_code"].ToString();
                                    model.worker_card = dr["emp_card"].ToString();
                                    model.worker_initial = dr["initial_code"].ToString();
                                    model.worker_fname_th = dr["firstname_th"].ToString();
                                    model.worker_lname_th = dr["lastname_th"].ToString();
                                    model.worker_fname_en = dr["firstname_en"].ToString();
                                    model.worker_lname_en = dr["lastname_en"].ToString();
                                    model.worker_emptype = dr["emptype_code"].ToString();

                                    model.worker_empstatus = dr["empstatus_code"].ToString();


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

                    //EM002
                    
                       
                        switch (import_code)
                        {
                            case "EM002":

                                DataTable dt = doReadExcel(fileName);
                                if (dt.Rows.Count > 0)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        //com	emp_code	emp_card	initial_code	firstname_th	lastname_th	firstname_en	lastname_en	
                                        // EMPCARD_CODE	CARD_TYPE	EMPCARD_ISSUE	EMPCARD_EXPIRE	


                                        cls_ctTREmpcard objEmpcard = new cls_ctTREmpcard();
                                        cls_TREmpcard model = new cls_TREmpcard();



                                        if (dr["com"].ToString().Equals(""))
                                            continue;
                                        model.company_code = dr["com"].ToString();

                                        model.worker_code = dr["emp_code"].ToString();
                                        //model.worker_card = dr["emp_card"].ToString();
                                        //model.worker_initial = dr["initial_code"].ToString();
                                        //model.worker_fname_th = dr["firstname_th"].ToString();
                                        //model.worker_lname_th = dr["lastname_th"].ToString();
                                        //model.worker_fname_en = dr["firstname_en"].ToString();
                                        //model.worker_lname_en = dr["lastname_en"].ToString();
                                        model.empcard_code = dr["empcard_code"].ToString();
                                        model.card_type = dr["card_type"].ToString();
                                        model.empcard_issue = Convert.ToDateTime(dr["empcard_issue"]);
                                        model.empcard_expire = Convert.ToDateTime(dr["empcard_expire"]);
                                       


                                        model.modified_by = task.modified_by;
                                        model.flag = model.flag;

                                        bool strID = objEmpcard.insert(model);

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

                                    if (objStr.Length > 0)
                                        strResult += " Fail : " + objStr.ToString();

                                }

                                break;

  

                        }
                    
                    //EM002
                    //EM003
                    

                        switch (import_code)
                        {
                            case "EM003":

                                DataTable dt = doReadExcel(fileName);
                                if (dt.Rows.Count > 0)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        //com	emp_code	emp_card	initial_code	firstname_th	lastname_th	firstname_en	lastname_en		
                                        // worker_code empdep_id empdep_date empdep_level01 empdep_level02 empdep_level03 empdep_level04 empdep_level05 empdep_level06 empdep_level07 empdep_level08 empdep_level09 empdep_level010 empdep_reason created_date modified_date


                                        cls_ctTREmpdep objEmpdep = new cls_ctTREmpdep();
                                        cls_TREmpdep model = new cls_TREmpdep();

                                        if (dr["com"].ToString().Equals(""))
                                            continue;
                                        model.company_code = dr["com"].ToString();

                                        model.worker_code = dr["emp_code"].ToString();
                                        //model.worker_card = dr["emp_card"].ToString();
                                        //model.worker_initial = dr["initial_code"].ToString();
                                        //model.worker_fname_th = dr["firstname_th"].ToString();
                                        //model.worker_lname_th = dr["lastname_th"].ToString();
                                        //model.worker_fname_en = dr["firstname_en"].ToString();
                                        //model.worker_lname_en = dr["lastname_en"].ToString();

                                        //model.worker_code = dr["worker_code"].ToString();
                                        
                                        model.empdep_date = Convert.ToDateTime(dr["empdep_date"]);
                                        model.empdep_level01 = dr["empdep_level01"].ToString();
                                        model.empdep_level02 = dr["empdep_level02"].ToString();
                                        model.empdep_level03 = dr["empdep_level03"].ToString();
                                        model.empdep_level04 = dr["empdep_level04"].ToString();
                                        model.empdep_level05 = dr["empdep_level05"].ToString();
                                        model.empdep_level06 = dr["empdep_level06"].ToString();
                                        model.empdep_level07 = dr["empdep_level07"].ToString();
                                        model.empdep_level08 = dr["empdep_level08"].ToString();
                                        model.empdep_level09 = dr["empdep_level09"].ToString();
                                        model.empdep_level10 = dr["empdep_level10"].ToString();
                                        model.empdep_reason = dr["empdep_reason"].ToString();


                                        //model.created_date = Convert.ToDateTime(dr["created_date"]);
                                       

                                       

                                        model.modified_by = task.modified_by;
                                        model.flag = model.flag;

                                        bool strID = objEmpdep.insert(model);

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

                                    if (objStr.Length > 0)
                                        strResult += " Fail : " + objStr.ToString();

                                }

                                break;

                        }
                    

                    //

                    

                        switch (import_code)
                        {
                            case "EM004":

                                DataTable dt = doReadExcel(fileName);
                                if (dt.Rows.Count > 0)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        //com	emp_code	emp_card	initial_code	firstname_th	lastname_th	firstname_en	lastname_en	emptype_code	emp_gender	emp_birthday	emp_startdate	
                                        //empaddress_no	empaddress_moo  empaddress_soi empaddress_road empaddress_tambon empaddress_amphur empaddress_zipcode empaddress_tel empaddress_email empaddress_line empaddress_facebook province_code


                                        cls_ctTREmpaddress objEmpaddress = new cls_ctTREmpaddress();
                                        cls_TREmpaddress model = new cls_TREmpaddress();

                                        if (dr["com"].ToString().Equals(""))
                                            continue;
                                        model.company_code = dr["com"].ToString();

                                        model.worker_code = dr["emp_code"].ToString();
                                        //model.worker_card = dr["emp_card"].ToString();
                                        //model.worker_initial = dr["initial_code"].ToString();
                                        //model.worker_fname_th = dr["firstname_th"].ToString();
                                        //model.worker_lname_th = dr["lastname_th"].ToString();
                                        //model.worker_fname_en = dr["firstname_en"].ToString();
                                        //model.worker_lname_en = dr["lastname_en"].ToString();

                                        model.empaddress_type = dr["empaddress_type"].ToString();
                                        model.empaddress_no = dr["empaddress_no"].ToString();
                                        model.empaddress_moo = dr["empaddress_moo"].ToString();
                                        model.empaddress_soi = dr["empaddress_soi"].ToString();
                                        model.empaddress_road = dr["empaddress_road"].ToString();
                                        model.empaddress_tambon = dr["empaddress_tambon"].ToString();
                                        model.empaddress_amphur = dr["empaddress_amphur"].ToString();
                                        model.empaddress_zipcode = dr["empaddress_zipcode"].ToString();
                                        model.empaddress_tel = dr["empaddress_tel"].ToString();
                                        model.empaddress_email = dr["empaddress_email"].ToString();
                                        model.empaddress_line = dr["empaddress_line"].ToString();
                                        model.empaddress_facebook = dr["empaddress_facebook"].ToString();
                                        model.province_code = dr["province_code"].ToString();
                                        

                                        

                                        model.modified_by = task.modified_by;
                                        model.flag = model.flag;

                                        bool strID = objEmpaddress.insert(model);

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

                                    if (objStr.Length > 0)
                                        strResult += " Fail : " + objStr.ToString();

                                }

                                break;



                        }
                    

                    //
                    //EM005

                    

                        switch (import_code)
                        {
                            case "EM005":

                                DataTable dt = doReadExcel(fileName);
                                if (dt.Rows.Count > 0)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        //com	emp_code	emp_card	initial_code	firstname_th	lastname_th	firstname_en	lastname_en	
                                        //empposition_date empposition_position empposition_reason


                                        cls_ctTREmpposition objPosition = new cls_ctTREmpposition();
                                        cls_TREmpposition model = new cls_TREmpposition();

                                        if (dr["com"].ToString().Equals(""))
                                            continue;
                                        model.company_code = dr["com"].ToString();

                                        model.worker_code = dr["emp_code"].ToString();
                                        //model.worker_card = dr["emp_card"].ToString();
                                        //model.worker_initial = dr["initial_code"].ToString();
                                        //model.worker_fname_th = dr["firstname_th"].ToString();
                                        //model.worker_lname_th = dr["lastname_th"].ToString();
                                        //model.worker_fname_en = dr["firstname_en"].ToString();
                                        //model.worker_lname_en = dr["lastname_en"].ToString();

                                        model.empposition_date = Convert.ToDateTime(dr["empposition_date"]);
                                        model.empposition_position = dr["empposition_position"].ToString();

                                        if (!dr["empposition_reason"].ToString().Equals(""))
                                        {
                                            model.empposition_reason = dr["empposition_reason"].ToString();
                                        }
                                        else
                                        {

                                        }
                                        
                                       


                                        model.modified_by = task.modified_by;
                                        model.flag = model.flag;

                                        bool strID = objPosition.insert(model);

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

                                    if (objStr.Length > 0)
                                        strResult += " Fail : " + objStr.ToString();

                                }

                                break;



                        }
                    
                    //EM006

                    

                        switch (import_code)
                        {
                            case "EM006":

                                DataTable dt = doReadExcel(fileName);
                                if (dt.Rows.Count > 0)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        //com	emp_code	emp_card	initial_code	firstname_th	lastname_th	firstname_en	lastname_en	emptype_code	emp_gender	emp_birthday	emp_startdate	
                                        //reduce_type empreduce_amount


                                        cls_ctTREmpreduce objEmpreduce = new cls_ctTREmpreduce();
                                        cls_TREmpreduce model = new cls_TREmpreduce();

                                        if (dr["com"].ToString().Equals(""))
                                            continue;
                                        model.company_code = dr["com"].ToString();

                                        model.worker_code = dr["emp_code"].ToString();
                                        //model.worker_card = dr["emp_card"].ToString();
                                        //model.worker_initial = dr["initial_code"].ToString();
                                        //model.worker_fname_th = dr["firstname_th"].ToString();
                                        //model.worker_lname_th = dr["lastname_th"].ToString();
                                        //model.worker_fname_en = dr["firstname_en"].ToString();
                                        //model.worker_lname_en = dr["lastname_en"].ToString();


                                        model.reduce_type = dr["reduce_type"].ToString();
                                        model.empreduce_amount = Convert.ToDouble(dr["empreduce_amount"]);
                                        
                                       

                                        model.modified_by = task.modified_by;
                                        model.flag = model.flag;

                                        bool strID = objEmpreduce.insert(model);

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

                                    if (objStr.Length > 0)
                                        strResult += " Fail : " + objStr.ToString();

                                }

                                break;



                        }
                    

                    //EM007
                    

                        switch (import_code)
                        {
                            case "EM007":

                                DataTable dt = doReadExcel(fileName);
                                if (dt.Rows.Count > 0)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        //com	emp_code	emp_card	initial_code	firstname_th	lastname_th	firstname_en	lastname_en	emptype_code	emp_gender	emp_birthday	emp_startdate	
                                        //empfamily_code empfamily_fname_th empfamily_lname_th empfamily_fname_en empfamily_lname_en empfamily_birthdate family_type


                                        cls_ctTREmpfamily objEmpfamily = new cls_ctTREmpfamily();
                                        cls_TREmpfamily model = new cls_TREmpfamily();

                                        if (dr["com"].ToString().Equals(""))
                                            continue;
                                        model.company_code = dr["com"].ToString();

                                        model.worker_code = dr["emp_code"].ToString();
                                        //model.worker_card = dr["emp_card"].ToString();
                                        //model.worker_initial = dr["initial_code"].ToString();
                                        //model.worker_fname_th = dr["firstname_th"].ToString();
                                        //model.worker_lname_th = dr["lastname_th"].ToString();
                                        //model.worker_fname_en = dr["firstname_en"].ToString();
                                        //model.worker_lname_en = dr["lastname_en"].ToString();


                                        model.empfamily_code = dr["empfamily_code"].ToString();
                                        model.empfamily_fname_th = dr["empfamily_fname_th"].ToString();
                                        model.empfamily_lname_th = dr["empfamily_lname_th"].ToString();

                                        model.empfamily_fname_en = dr["empfamily_fname_en"].ToString();
                                        model.empfamily_lname_en = dr["empfamily_lname_en"].ToString();

                                        string strExpire = "9999-12-31";
                                        try
                                        {
                                            if (dr["empfamily_birthdate"].ToString() != "")
                                                strExpire = dr["empfamily_birthdate"].ToString();
                                        }
                                        catch { }

                                        model.empfamily_birthdate = Convert.ToDateTime(strExpire);

                                        //model.empfamily_birthdate = Convert.ToDateTime(dr["empfamily_birthdate"]);

                               
                                        //if (dr["empfamily_birthdate"] != DBNull.Value && !string.IsNullOrEmpty(dr["empfamily_birthdate"].ToString()))
                                        //{
                                        //    model.empfamily_birthdate = Convert.ToDateTime(dr["empfamily_birthdate"]);
                                        //}
                                        //else
                                        //{
                                        //     model.empfamily_birthdate = DateTime.MinValue;
                                            
                                        //}

                                        model.family_type = dr["family_type"].ToString();


                                        model.modified_by = task.modified_by;
                                        model.flag = model.flag;

                                        bool strID = objEmpfamily.insert(model);

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

                                    if (objStr.Length > 0)
                                        strResult += " Fail : " + objStr.ToString();

                                }

                                strResult = TEST;
                                break;

                       }
                    

                    //
                       
                        //EM008
                   


                        switch (import_code)
                        {
                            case "EM008":

                                DataTable dt = doReadExcel(fileName);
                                if (dt.Rows.Count > 0)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        //com	emp_code	emp_card	initial_code	firstname_th	lastname_th	firstname_en	lastname_en	emptype_code	emp_gender	emp_birthday	emp_startdate	
                                        //

                                        cls_ctTREmpbank objEmpbanky = new cls_ctTREmpbank();
                                        cls_TREmpbank model = new cls_TREmpbank();


                                        if (dr["com"].ToString().Equals(""))
                                            continue;
                                        model.company_code = dr["com"].ToString();

                                        model.worker_code = dr["emp_code"].ToString();
                                        //model.worker_card = dr["emp_card"].ToString();
                                        //model.worker_initial = dr["initial_code"].ToString();
                                        //model.worker_fname_th = dr["firstname_th"].ToString();
                                        //model.worker_lname_th = dr["lastname_th"].ToString();
                                        //model.worker_fname_en = dr["firstname_en"].ToString();
                                        //model.worker_lname_en = dr["lastname_en"].ToString();

                                        model.empbank_bankcode = dr["bankcode"].ToString();
                                        model.empbank_bankaccount = dr["bankaccount"].ToString();

                                        model.empbank_bankpercent = Convert.ToDouble(dr["bankpercent"]);
                                        model.empbank_cashpercent = Convert.ToDouble(dr["cashpercent"]);

                                        model.empbank_bankname = dr["bankname"].ToString();
                                        
                                        model.modified_by = task.modified_by;
                                        //model.empbenefit_break = model.empbenefit_break;

                                        bool strID = objEmpbanky.insert(model);

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

                                    if (objStr.Length > 0)
                                        strResult += " Fail : " + objStr.ToString();

                                }

                                break;


                        }


                        //

                    //EM009
                    

                        switch (import_code)
                        {
                            case "EM009":

                                DataTable dt = doReadExcel(fileName);
                                if (dt.Rows.Count > 0)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        //com	emp_code	emp_card	initial_code	firstname_th	lastname_th	firstname_en	lastname_en	emptype_code	emp_gender	emp_birthday	emp_startdate	
                                        //


                                        cls_ctTREmpbenefit objEmpbenefit = new cls_ctTREmpbenefit();
                                        cls_TREmpbenefit model = new cls_TREmpbenefit();

                                        if (dr["com"].ToString().Equals(""))
                                            continue;
                                        model.company_code = dr["com"].ToString();

                                        model.worker_code = dr["emp_code"].ToString();
                                        //model.worker_card = dr["emp_card"].ToString();
                                        //model.worker_initial = dr["initial_code"].ToString();
                                        //model.worker_fname_th = dr["firstname_th"].ToString();
                                        //model.worker_lname_th = dr["lastname_th"].ToString();
                                        //model.worker_fname_en = dr["firstname_en"].ToString();
                                        //model.worker_lname_en = dr["lastname_en"].ToString();

                                        model.item_code = dr["item_code"].ToString();
                                        model.empbenefit_startdate = Convert.ToDateTime(dr["empbenefit_startdate"]);
                                        model.empbenefit_enddate = Convert.ToDateTime(dr["empbenefit_enddate"]);

                                        model.empbenefit_amount = Convert.ToDouble(dr["empbenefit_amount"]);
                                        model.empbenefit_reason = dr["empbenefit_reason"].ToString();
                                        model.empbenefit_note = dr["empbenefit_note"].ToString();
                                        model.empbenefit_paytype = dr["empbenefit_paytype"].ToString();
                                        

                                        model.empbenefit_breakreason = dr["empbenefit_breakreason"].ToString();
                                        model.empbenefit_conditionpay = dr["empbenefit_conditionpay"].ToString();
                                        model.empbenefit_payfirst = dr["empbenefit_payfirst"].ToString();
                                        
                                        model.modified_by = task.modified_by;
                                        model.empbenefit_break = model.empbenefit_break;

                                        bool strID = objEmpbenefit.insert(model);

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

                                    if (objStr.Length > 0)
                                        strResult += " Fail : " + objStr.ToString();

                                }

                                break;


                        }
                    

                    //
                    //EM010
                    

                        switch (import_code)
                        {
                            case "EM010":

                                DataTable dt = doReadExcel(fileName);
                                if (dt.Rows.Count > 0)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        //com	emp_code	emp_card	initial_code	firstname_th	lastname_th	firstname_en	lastname_en	emptype_code	emp_gender	emp_birthday	emp_startdate	
                                        //

                                        cls_ctTREmpeducation objEmpeducation = new cls_ctTREmpeducation();
                                        cls_TREmpeducation model = new cls_TREmpeducation();

                                        if (dr["com"].ToString().Equals(""))
                                            continue;
                                        model.company_code = dr["com"].ToString();

                                        model.worker_code = dr["emp_code"].ToString();
                                        //model.worker_card = dr["emp_card"].ToString();
                                        //model.worker_initial = dr["initial_code"].ToString();
                                        //model.worker_fname_th = dr["firstname_th"].ToString();
                                        //model.worker_lname_th = dr["lastname_th"].ToString();
                                        //model.worker_fname_en = dr["firstname_en"].ToString();
                                        //model.worker_lname_en = dr["lastname_en"].ToString();



                                      
                                        model.empeducation_gpa = dr["empeducation_gpa"].ToString();
                                        model.empeducation_start = Convert.ToDateTime(dr["empeducation_start"]);
                                        model.empeducation_finish = Convert.ToDateTime(dr["empeducation_finish"]);
                                        model.institute_code = dr["institute_code"].ToString();
                                        model.faculty_code = dr["faculty_code"].ToString();
                                        model.major_code = dr["major_code"].ToString();
                                        model.qualification_code = dr["qualification_code"].ToString();



                                        model.modified_by = task.modified_by;
                                        model.flag = model.flag;

                                        bool strID = objEmpeducation.insert(model);

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

                                    if (objStr.Length > 0)
                                        strResult += " Fail : " + objStr.ToString();

                                }

                                break;


                        }
                    

                    //EM011
                    

                        switch (import_code)
                        {
                            case "EM011":

                                DataTable dt = doReadExcel(fileName);
                                if (dt.Rows.Count > 0)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        //com	emp_code	emp_card	initial_code	firstname_th	lastname_th	firstname_en	lastname_en	emptype_code	emp_gender	emp_birthday	emp_startdate	
                                        //emptraining_start emptraining_finish emptraining_status emptraining_hours  emptraining_cost emptraining_note institute_code institute_other  course_code course_other


                                        cls_ctTREmptraining objEmptraining = new cls_ctTREmptraining();
                                        cls_TREmptraining model = new cls_TREmptraining();

                                        if (dr["com"].ToString().Equals(""))
                                            continue;
                                        model.company_code = dr["com"].ToString();

                                        model.worker_code = dr["emp_code"].ToString();
                                        //model.worker_card = dr["emp_card"].ToString();
                                        //model.worker_initial = dr["initial_code"].ToString();
                                        //model.worker_fname_th = dr["firstname_th"].ToString();
                                        //model.worker_lname_th = dr["lastname_th"].ToString();
                                        //model.worker_fname_en = dr["firstname_en"].ToString();
                                        //model.worker_lname_en = dr["lastname_en"].ToString();


                                        model.emptraining_start = Convert.ToDateTime(dr["emptraining_start"]);
                                        model.emptraining_finish = Convert.ToDateTime(dr["emptraining_finish"]);
                                      
                                        model.emptraining_status = dr["emptraining_status"].ToString();

                                        if (!dr["emptraining_hours"].ToString().Equals(""))
                                        {
                                            model.emptraining_hours = Convert.ToDouble(dr["emptraining_hours"]);
                                        }
                                        else
                                        {

                                        }
                                        if (!dr["emptraining_cost"].ToString().Equals(""))
                                        {
                                            model.emptraining_cost = Convert.ToDouble(dr["emptraining_cost"]);
                                        }
                                        else
                                        {

                                        }
                                      
                                        model.emptraining_note = dr["emptraining_note"].ToString();
                                        model.institute_code = dr["institute_code"].ToString();
                                        model.institute_other = dr["institute_other"].ToString();
                                        model.course_code = dr["course_code"].ToString();
                                        model.course_other = dr["course_other"].ToString();
                                       

                                        

                                        model.modified_by = task.modified_by;
                                        model.flag = model.flag;


                                        bool strID = objEmptraining.insert(model);

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

                                    if (objStr.Length > 0)
                                        strResult += " Fail : " + objStr.ToString();

                                }

                                break;
                        }
                    //11
                        //EM012


                        switch (import_code)
                        {
                            case "EM012":

                                DataTable dt = doReadExcel(fileName);
                                if (dt.Rows.Count > 0)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        //com	emp_code	emp_card	initial_code	firstname_th	lastname_th	firstname_en	lastname_en	emptype_code	emp_gender	emp_birthday	emp_startdate	
                                        //amount  incamount  incpercent   date  reason

                                        cls_ctTREmpsalary objEmpsalary = new cls_ctTREmpsalary();
                                        cls_TREmpsalary model = new cls_TREmpsalary();

                                        if (dr["com"].ToString().Equals(""))
                                            continue;
                                        model.company_code = dr["com"].ToString();

                                        model.worker_code = dr["emp_code"].ToString();
                                        //model.worker_card = dr["emp_card"].ToString();
                                        //model.worker_initial = dr["initial_code"].ToString();
                                        //model.worker_fname_th = dr["firstname_th"].ToString();
                                        //model.worker_lname_th = dr["lastname_th"].ToString();
                                        //model.worker_fname_en = dr["firstname_en"].ToString();
                                        //model.worker_lname_en = dr["lastname_en"].ToString();

                                        model.empsalary_amount = Convert.ToDouble(dr["amount"]);
                                        model.empsalary_date = Convert.ToDateTime(dr["date"]);
                                        model.empsalary_reason = dr["reason"].ToString();

                                        if (!dr["incamount"].ToString().Equals(""))
                                        {
                                            model.empsalary_incamount = Convert.ToDouble(dr["incamount"]);
                                        }
                                        else
                                        {

                                        }

                                        if (!dr["incpercent"].ToString().Equals(""))
                                        {
                                        model.empsalary_incpercent = Convert.ToDouble(dr["incpercent"]);
                                          }
                                        else
                                        {

                                        }

                                        model.modified_by = task.modified_by;
                                        model.flag = model.flag;

                                        bool strID = objEmpsalary.insert(model);

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

                                    if (objStr.Length > 0)
                                        strResult += " Fail : " + objStr.ToString();

                                }

                                break;


                        }


                        //EM012

                        //EM013


                        switch (import_code)
                        {
                            case "EM013":

                                DataTable dt = doReadExcel(fileName);
                                if (dt.Rows.Count > 0)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        //com	emp_code	emp_card	initial_code	firstname_th	lastname_th	firstname_en	lastname_en	emptype_code	emp_gender	emp_birthday	emp_startdate	
                                        //provident_code empprovident_card empprovident_entry  empprovident_start

                                        cls_ctTREmpprovident objEmpprovident = new cls_ctTREmpprovident();
                                        cls_TREmpprovident model = new cls_TREmpprovident();

                                        if (dr["com"].ToString().Equals(""))
                                            continue;
                                        model.company_code = dr["com"].ToString();

                                        model.worker_code = dr["emp_code"].ToString();
                                        //model.worker_card = dr["emp_card"].ToString();
                                        //model.worker_initial = dr["initial_code"].ToString();
                                        //model.worker_fname_th = dr["firstname_th"].ToString();
                                        //model.worker_lname_th = dr["lastname_th"].ToString();
                                        //model.worker_fname_en = dr["firstname_en"].ToString();
                                        //model.worker_lname_en = dr["lastname_en"].ToString();



                                        model.provident_code = dr["provident_code"].ToString();
                                        model.empprovident_card = dr["empprovident_card"].ToString();
                                        model.empprovident_entry = Convert.ToDateTime(dr["entry"]);
                                        model.empprovident_start = Convert.ToDateTime(dr["start"]);

                                        if (!dr["end"].ToString().Equals(""))
                                        {
                                            model.empprovident_end = Convert.ToDateTime(dr["end"]);
                                        }
                                        else
                                        {

                                        }
                                        


                                        model.modified_by = task.modified_by;
                                        model.flag = model.flag;

                                        bool strID = objEmpprovident.insert(model);

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

                                    if (objStr.Length > 0)
                                        strResult += " Fail : " + objStr.ToString();

                                }

                                break;


                        }


                    //EM013

                        //EM014


                        switch (import_code)
                        {
                            case "EM014":

                                DataTable dt = doReadExcel(fileName);
                                if (dt.Rows.Count > 0)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        //com	emp_code	emp_card	initial_code	firstname_th	lastname_th	firstname_en	lastname_en	emptype_code	emp_gender	emp_birthday	emp_startdate	
                                        //at position  start finish reasonchange

                                        cls_ctTREmpexperience objEmpexperience = new cls_ctTREmpexperience();
                                        cls_TREmpexperience model = new cls_TREmpexperience();

                                        if (dr["com"].ToString().Equals(""))
                                            continue;
                                        model.company_code = dr["com"].ToString();

                                        model.worker_code = dr["emp_code"].ToString();
                                        //model.worker_card = dr["emp_card"].ToString();
                                        //model.worker_initial = dr["initial_code"].ToString();
                                        //model.worker_fname_th = dr["firstname_th"].ToString();
                                        //model.worker_lname_th = dr["lastname_th"].ToString();
                                        //model.worker_fname_en = dr["firstname_en"].ToString();
                                        //model.worker_lname_en = dr["lastname_en"].ToString();



                                        model.empexperience_at = dr["at"].ToString();
                                        model.empexperience_position = dr["position"].ToString();
                                        model.empexperience_start = Convert.ToDateTime(dr["start"]);
                                        model.empexperience_finish = Convert.ToDateTime(dr["finish"]);
                                        model.empexperience_reasonchange = dr["reasonchange"].ToString();


                                        model.modified_by = task.modified_by;
                                        model.flag = model.flag;

                                        bool strID = objEmpexperience.insert(model);

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

                                    if (objStr.Length > 0)
                                        strResult += " Fail : " + objStr.ToString();

                                }

                                break;


                        }


                    //EM014

                        //EM015


                        switch (import_code)
                        {
                            case "EM015":

                                DataTable dt = doReadExcel(fileName);
                                if (dt.Rows.Count > 0)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        //com	emp_code	emp_card	initial_code	firstname_th	lastname_th	firstname_en	lastname_en	emptype_code	emp_gender	emp_birthday	emp_startdate	
                                        //

                                        cls_ctTREmplocation objEmplocation = new cls_ctTREmplocation();
                                        cls_TREmplocation model = new cls_TREmplocation();

                                        if (dr["com"].ToString().Equals(""))
                                            continue;
                                        model.company_code = dr["com"].ToString();

                                        model.worker_code = dr["emp_code"].ToString();
                                        //model.worker_card = dr["emp_card"].ToString();
                                        //model.worker_initial = dr["initial_code"].ToString();
                                        //model.worker_fname_th = dr["firstname_th"].ToString();
                                        //model.worker_lname_th = dr["lastname_th"].ToString();
                                        //model.worker_fname_en = dr["firstname_en"].ToString();
                                        //model.worker_lname_en = dr["lastname_en"].ToString();




                                        model.location_code = dr["location_code"].ToString();
                                        model.emplocation_startdate = Convert.ToDateTime(dr["emplocation_startdate"]);
                                        model.emplocation_enddate = Convert.ToDateTime(dr["emplocation_enddate"]);
                                        model.emplocation_note = dr["emplocation_note"].ToString();
                                       



                                        model.modified_by = task.modified_by;
                                        model.flag = model.flag;

                                        bool strID = objEmplocation.insert(model);

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

                                    if (objStr.Length > 0)
                                        strResult += " Fail : " + objStr.ToString();

                                }

                                break;


                        }


                    //EM015

                        //EM016


                        switch (import_code)
                        {
                            case "EM016":

                                DataTable dt = doReadExcel(fileName);
                                if (dt.Rows.Count > 0)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        //com	emp_code	emp_card	initial_code	firstname_th	lastname_th	firstname_en	lastname_en	emptype_code	emp_gender	emp_birthday	emp_startdate	
                                        //start finish  note location reason

                                        cls_ctTREmpawpt objpawpt = new cls_ctTREmpawpt();
                                        cls_TREmpawpt model = new cls_TREmpawpt();

                                        if (dr["com"].ToString().Equals(""))
                                            continue;
                                        model.company_code = dr["com"].ToString();

                                        model.worker_code = dr["emp_code"].ToString();
                                        //model.worker_card = dr["emp_card"].ToString();
                                        //model.worker_initial = dr["initial_code"].ToString();
                                        //model.worker_fname_th = dr["firstname_th"].ToString();
                                        //model.worker_lname_th = dr["lastname_th"].ToString();
                                        //model.worker_fname_en = dr["firstname_en"].ToString();
                                        //model.worker_lname_en = dr["lastname_en"].ToString();



                                        model.empawpt_start = Convert.ToDateTime(dr["start"]);
                                        model.empawpt_finish = Convert.ToDateTime(dr["finish"]);
                                        model.empawpt_location = dr["location"].ToString();
                                        model.empawpt_reason = dr["reason"].ToString();
                                        model.empawpt_note = dr["note"].ToString();
                                        model.empawpt_type = dr["empawpt_type"].ToString();



                                        model.modified_by = task.modified_by;
                                        model.flag = model.flag;

                                        bool strID = objpawpt.insert(model);

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

                                    if (objStr.Length > 0)
                                        strResult += " Fail : " + objStr.ToString();

                                }

                                break;


                        }


                    //EM016

                        //EM017


                        switch (import_code)
                        {
                            case "EM017":

                                DataTable dt = doReadExcel(fileName);
                                if (dt.Rows.Count > 0)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        //com	emp_code	emp_card	initial_code	firstname_th	lastname_th	firstname_en	lastname_en	emptype_code	emp_gender	emp_birthday	emp_startdate	
                                        //start finish location reason note

                                        cls_ctTREmpawpt objEmpawpt = new cls_ctTREmpawpt();
                                        cls_TREmpawpt model = new cls_TREmpawpt();

                                        if (dr["com"].ToString().Equals(""))
                                            continue;
                                        model.company_code = dr["com"].ToString();

                                        model.worker_code = dr["emp_code"].ToString();
                                        //model.worker_card = dr["emp_card"].ToString();
                                        //model.worker_initial = dr["initial_code"].ToString();
                                        //model.worker_fname_th = dr["firstname_th"].ToString();
                                        //model.worker_lname_th = dr["lastname_th"].ToString();
                                        //model.worker_fname_en = dr["firstname_en"].ToString();
                                        //model.worker_lname_en = dr["lastname_en"].ToString();



                                        //model.empawpt_no = Convert.ToInt32(dr["empawpt_no"]);
                                        model.empawpt_start = Convert.ToDateTime(dr["start"]);
                                        model.empawpt_finish = Convert.ToDateTime(dr["finish"]);
                                        model.empawpt_location = dr["location"].ToString();
                                        model.empawpt_reason = dr["reason"].ToString();
                                        model.empawpt_note = dr["note"].ToString();
                                        model.empawpt_type = dr["empawpt_type"].ToString();
                                       
                                        



                                        model.modified_by = task.modified_by;
                                        model.flag = model.flag;

                                        bool strID = objEmpawpt.insert(model);

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

                                    if (objStr.Length > 0)
                                        strResult += " Fail : " + objStr.ToString();

                                }

                                break;


                        }


                    //EM017
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
