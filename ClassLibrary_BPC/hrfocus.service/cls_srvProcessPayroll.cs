﻿using ClassLibrary_BPC.hrfocus.controller;
using ClassLibrary_BPC.hrfocus.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelLibrary;

namespace ClassLibrary_BPC.hrfocus.service
{
    public class cls_srvProcessPayroll
    {

        public string doCalculateTax(string com, string taskid)
        {
            string strResult = "";

            cls_ctConnection obj_conn = new cls_ctConnection();

            try
            {
                //string sql = "{ CALL [dbo].[TA_CalWorkTimeEmp] (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?) }";
                //if (conn.State == ConnectionState.Open)
                //    conn.Close();
                //conn.Open();
                //OdbcCommand CalWorkTime = new OdbcCommand();
                //CalWorkTime.CommandType = CommandType.Text;
                //CalWorkTime.CommandText = sql;
                //CalWorkTime.CommandTimeout = 1000;
                //CalWorkTime.Connection = conn;
                //CalWorkTime.Parameters.Add("@CompID", Initial.CompanyID);
                //CalWorkTime.Parameters.Add("@EmpID", lvEmployeeId);
                //CalWorkTime.Parameters.Add("@EmpType", cboEmpTypePeriod.SelectedValue.ToString());
                //CalWorkTime.Parameters.Add("@CalOT", false);
                //CalWorkTime.Parameters.Add("@SelectDate", false);
                //CalWorkTime.Parameters.Add("@FromDateSelect", dtFromDate.Value.Date);
                //CalWorkTime.Parameters.Add("@ToDateSelect", dtToDate.Value.Date);
                //CalWorkTime.Parameters.Add("@FromDatePeriod", FromDatePeriod);
                //CalWorkTime.Parameters.Add("@ToDatePeriod", ToDatePeriod);
                //CalWorkTime.Parameters.Add("@PayDate", Convert.ToDateTime(cboShowPayDate.Text));
                //CalWorkTime.Parameters.Add("@CalShift", false);
                //CalWorkTime.Parameters.Add("@CalLeave", false);
                //CalWorkTime.ExecuteNonQuery();
                                
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_conn.doConnect();
                //obj_conn.doOpenTransaction();

                obj_str.Append(" EXEC [dbo].[HRM_PRO_CALTAX] '" + com + "', '" + taskid + "' ");

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
                //obj_cmd.Transaction = obj_conn.getTransaction();

                obj_cmd.CommandType = CommandType.Text;

                //obj_cmd.Parameters.Add("@CompanyCode", SqlDbType.VarChar).Value = com;
                //obj_cmd.Parameters.Add("@TaskID", SqlDbType.VarChar).Value = taskid;

                int intCountSuccess = obj_cmd.ExecuteNonQuery();

                if (intCountSuccess > 0)
                {
                    //obj_conn.doCommit();
                    strResult = "Success::" + intCountSuccess.ToString();
                }
                    

            }
            catch (Exception ex)
            {

            }

            return strResult;
        }

        public string doCalculateBonus(string com, string taskid)
        {
            string strResult = "";

            cls_ctConnection obj_conn = new cls_ctConnection();

            try
            {                
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_conn.doConnect();
              
                obj_str.Append(" EXEC [dbo].[HRM_PRO_CALBONUS] '" + com + "', '" + taskid + "' ");
                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());                

                obj_cmd.CommandType = CommandType.Text;
                
                int intCountSuccess = obj_cmd.ExecuteNonQuery();

                if (intCountSuccess > 0)
                {               
                    strResult = "Success::" + intCountSuccess.ToString();
                }                
            }
            catch (Exception ex)
            {

            }

            return strResult;
        }
        //TRN_BANK เริ่ม
        public string doExportBank(string com, string taskid)
        {
            string strResult = "";

            cls_ctMTTask objMTTask = new cls_ctMTTask();
            List<cls_MTTask> listMTTask = objMTTask.getDataByFillter(com, taskid, "TRN_BANK", "");
            List<string> listError = new List<string>();

            if (listMTTask.Count > 0)
            {
                cls_MTTask task = listMTTask[0];

                task.task_start = DateTime.Now;

                cls_ctMTTask objTaskDetail = new cls_ctMTTask();
                cls_TRTaskdetail task_detail = objTaskDetail.getTaskDetail(task.task_id.ToString());

                cls_ctMTTask objTaskWhose = new cls_ctMTTask();
                List<cls_TRTaskwhose> listWhose = objTaskWhose.getTaskWhose(task.task_id.ToString());

                DateTime dateEff = task_detail.taskdetail_fromdate;
                DateTime datePay = task_detail.taskdetail_paydate;

                StringBuilder objStr = new StringBuilder();
                foreach (cls_TRTaskwhose whose in listWhose)
                {
                    objStr.Append("'" + whose.worker_code + "',");
                }

                string strEmp = objStr.ToString().Substring(0, objStr.ToString().Length - 1);

                 //ตัวเช็คธนาคาร
                string[] task_bank = task_detail.taskdetail_process.Split('|');


                //-- Get worker
                cls_ctMTWorker objWorker = new cls_ctMTWorker();
                List<cls_MTWorker> list_worker = objWorker.getDataMultipleEmp( com, strEmp);

                //-- Step 2 Get Paytran
                cls_ctTRPaytran objPay = new cls_ctTRPaytran();
                List<cls_TRPaytran> list_paytran = objPay.getDataMultipleEmp("TH", com, datePay, datePay, strEmp);



                //-- Step 3 Get Company acc
                cls_ctTRCombank objCombank = new cls_ctTRCombank();
                List<cls_TRCombank> list_combank = objCombank.getDataByFillter("", com);
                cls_TRCombank combank = list_combank[0];

                //-- Step 4 Get Company detail
                cls_ctMTCompany objCom = new cls_ctMTCompany();
                List<cls_MTCompany> list_com = objCom.getDataByFillter("", com);
                cls_MTCompany comdetail = list_com[0];



                //-- Step 5 Get Emp acc
                cls_ctTREmpbank objEmpbank = new cls_ctTREmpbank();
                List<cls_TREmpbank> list_empbank = objEmpbank.getDataMultipleEmp(com, strEmp);

                //-- Step 6 Get pay bank
                cls_ctTRPaybank objPaybank = new cls_ctTRPaybank();
                List<cls_TRPaybank> list_paybank = objPaybank.getDataByFillter(com, strEmp);
                cls_TRPaybank paybank = list_paybank[0];

                //-- Step 7 Get Branch
                cls_ctTRCombranch objcombranch = new cls_ctTRCombranch();
                List<cls_TRCombranch> list_combranch = objcombranch.getDataTaxMultipleCombranch(com);
                cls_TRCombranch combranch = list_combranch[0];
                //-- Step 8 GetCompany detail
                cls_ctTREmpcard objEmpcard = new cls_ctTREmpcard();
                List<cls_TREmpcard> list_empcard = objEmpcard.getDataTaxMultipleEmp(com, strEmp);
                string tmpData = "";
                string sequence = "000000";
                string sequence1 = "000001";
                string sequence2 = "000001";

                //string beneref = "001";

                string spare = "";

                string spare1 = "";

                string departmentcode = "";
                string user = "";
                string spare2 = "";
                string spare3 = "";
                string empacc1 = "";
                string empaccbank = "";


                if (list_paytran.Count > 0)
                {

                    // ตรวจสอบความยาวของ comdetail.company_name_en หากมากกว่า 25 ตัวอักษรให้ตัดทิ้งเหลือเพียง 25 ตัวอักษร
                    if (comdetail.company_name_en.Length > 25)
                        comdetail.company_name_en = comdetail.company_name_en.Substring(0, 25);
                    // ตรวจสอบความยาวของ comdetail.company_name_en หากน้อยกว่า 25 ตัวอักษรให้เติมด้วยช่องว่างจนครบ 25 ตัวอักษร
                    else if (comdetail.company_name_en.Length < 25)
                        comdetail.company_name_en = comdetail.company_name_en.PadRight(25, ' ');
                    if (spare.Length < 77)
                        spare = spare.PadRight(77, '0');



                    if (spare1.Length < 32)
                        spare1 = spare1.PadRight(32, '0');

                    cls_TREmpcard obj_card = new cls_TREmpcard();

                    //
                    //if (list_worker.Count > 0)
                    if (task_bank.Length > 0)
                    {
                        switch (combank.combank_bankcode)
                        {
                            case "002": /// ธนาคารกรุงเทพ
                                {
                                    foreach (cls_TREmpbank workerlist in list_empbank)
                                    {

                                        if (combank.combank_bankcode.Equals(workerlist.empbank_bankcode))
                                        {
                                            {


                                                //กำหนดค่า tmpData โดยรวมข้อมูล combank.combank_bankcode และ comdetail.company_name_en
                                                tmpData = "H" + "000001" + combank.combank_bankcode + combank.combank_bankaccount + comdetail.company_name_en + datePay.ToString("ddMMyy", DateTimeFormatInfo.CurrentInfo) + spare + "\r\n";

                                                double douTotal = 0;
                                                double douTotal2 = 0;

                                                double douTotal3 = 0;

                                                int index = 0;
                                                string amount;
                                                string bkData;

                                                foreach (cls_TRPaytran paytran in list_paytran)
                                                {

                                                    string empacc = "";
                                                    string empname = "";
                                                    string empname2 = "";

                                                    cls_TREmpbank obj_Empbank = new cls_TREmpbank();

                                                    foreach (cls_MTWorker worker in list_worker)
                                                    {
                                                        if (paytran.worker_code.Equals(worker.worker_code))
                                                        {
                                                            foreach (cls_TREmpbank workerlistt in list_empbank)
                                                            {
                                                                if (worker.worker_code.Equals(workerlistt.worker_code) && workerlistt.empbank_bankcode.Equals(combank.combank_bankcode))
                                                                {
                                                                    // ทำงานตามที่ต้องการเมื่อพบข้อมูลที่ตรง
                                                                    empname = " " + worker.initial_name_en + " " + worker.worker_fname_en + " " + worker.worker_lname_en + " " + datePay.ToString("ddMMyy", DateTimeFormatInfo.CurrentInfo) + " ";
                                                                    empname2 = " " + worker.initial_name_en + " " + worker.worker_fname_en + " " + worker.worker_lname_en + " ";

                                                                    break;
                                                                }
                                                            }
                                                        }
                                                    }




                                                    foreach (cls_TREmpbank worker in list_empbank)
                                                    {
                                                        if (paytran.worker_code.Equals(worker.worker_code))
                                                        {
                                                            empacc = worker.empbank_bankaccount.Replace("-", "");
                                                            break;
                                                        }
                                                    }


                                                    //foreach (cls_TREmpbank worker in list_empbank)
                                                    //{
                                                    //    if (pf.worker_code.Equals(card.worker_code) && card.card_type.Equals("NTID"))
                                                    //    {
                                                    //        obj_taxcard = card;
                                                    //        break;
                                                    //    }
                                                    //}

                                                    //string empacc = "";  // กำหนดค่าเริ่มต้นเป็นข้อความว่าง








                                                    //"Header Record  (ส่วนที่ 1)"   
                                                    if (empname.Equals("") || empacc.Equals(""))
                                                        continue;


                                                    sequence = Convert.ToString(index + 1).PadLeft(6, '0');

                                                    decimal temp = (decimal)paytran.paytran_netpay_b;
                                                    amount = temp.ToString("#.#0").Trim().Replace(".", "").PadLeft(10, '0');

                                                    if (spare1.Length < 32)
                                                        spare1 = spare1.PadRight(32, '0');

                                                    if (departmentcode.Length < 4)
                                                        departmentcode = departmentcode.PadRight(4, '0');

                                                    if (user.Length < 10)
                                                        user = user.PadRight(10, '0');

                                                    if (spare2.Length < 13)
                                                        spare2 = spare2.PadRight(13, '0');


                                                    if (spare1.Length < 32)
                                                        spare1 = spare1.PadRight(32, '0');


                                                    foreach (cls_TREmpbank worker in list_empbank)
                                                    {
                                                        if (paytran.worker_code.Equals(worker.worker_code))
                                                        {
                                                            if (worker.empbank_bankpercent != 0)
                                                            {
                                                                empaccbank = "C";
                                                                break; // เพิ่ม break เมื่อพบเงื่อนไขที่ตรง
                                                            }
                                                            else if (worker.empbank_cashpercent != 0)
                                                            {
                                                                empaccbank = "D";
                                                                break; // เพิ่ม break เมื่อพบเงื่อนไขที่ตรง
                                                            }
                                                        }
                                                    }

                                                    foreach (cls_TREmpbank worker in list_empbank)
                                                    {
                                                        if (paytran.worker_code.Equals(worker.worker_code))
                                                        {
                                                            if (worker.empbank_bankpercent != 0)
                                                            {
                                                                empaccbank = "C";
                                                                break; // เพิ่ม break เมื่อพบเงื่อนไขที่ตรง
                                                            }
                                                            else if (worker.empbank_cashpercent != 0)
                                                            {
                                                                empaccbank = "D";
                                                                break; // เพิ่ม break เมื่อพบเงื่อนไขที่ตรง
                                                            }
                                                        }
                                                    }

                                                    // ทำต่อที่นี่หากต้องการจัดการหลังลูป


                                                    //// ให้ตรวจสอบค่า empaccbank หลังจากลูป
                                                    //if (string.IsNullOrEmpty(empaccbank))
                                                    //{
                                                    //    // กำหนดค่าเริ่มต้นหรือทำอย่างอื่นที่คุณต้องการ
                                                    //    empaccbank = "A";
                                                    //}

                                                    bkData = "D" + sequence + combank.combank_bankcode + empacc + empaccbank + amount + "02" + "9" + spare1 + departmentcode + user + spare2;
                                                    bkData = bkData.PadRight(32, '0');

                                                    if (empname2.Length > 35)
                                                        empname2 = empname2.Substring(0, 35);

                                                    bkData = bkData + empname2.ToUpper();
                                                    tmpData += bkData.PadRight(128, ' ') + "\r\n";

                                                    douTotal += paytran.paytran_netpay_b;
                                                    douTotal2 += paytran.paytran_netpay_c;

                                                    index++;
                                                }



                                                int record = list_paytran.Count;
                                                sequence = (index + 1).ToString().PadLeft(6, '0');
                                                //string total1 = douTotal.ToString("#.#0").Trim().Replace(".", "").PadLeft(13, '0');
                                                //string total2 = douTotal2.ToString("#.#0").Trim().Replace(".", "").PadLeft(13, '0');
                                                //
                                                //string total1record1 = "";
                                                //int startingValue1 = 1;

                                                //if (douTotal > 0)
                                                //{
                                                //    total1record1 = (startingValue1 + (long)Math.Floor(douTotal) - 1).ToString("D13");
                                                //}
                                                //else
                                                //{
                                                //    total1record1 = "0000000000000";
                                                //}

                                                //decimal douTotal = paytran.paytran_netpay_b;
                                                int countdouTotalRecord1 = list_empbank.Count(worker => worker.empbank_bankpercent != 0 && worker.empbank_bankcode == combank.combank_bankcode);

                                                string total1record1 = "";

                                                if (douTotal > 0)
                                                {
                                                    decimal numericDouTotal = decimal.Parse(douTotal.ToString("#.#0").Trim().Replace(".", "").PadLeft(13, '0'));
                                                    long roundedDouTotal = (long)Math.Floor(numericDouTotal * 100);
                                                    total1record1 = (  roundedDouTotal  ).ToString("D13");
                                                }
                                                else
                                                {
                                                    total1record1 = "0000000000000";
                                                }


                                                ////
                                                int countdouTotalRecord2 = list_empbank.Count(worker => worker.empbank_bankpercent != 0 && worker.empbank_bankcode == combank.combank_bankcode);
                                                string total1record2 = "";
                                                if (douTotal > 0)
                                                {
                                                    decimal numericDouTotal2 = decimal.Parse(douTotal2.ToString("#.#0").Trim().Replace(".", "").PadLeft(13, '0'));
                                                    long roundedDouTotal2 = (long)Math.Floor(numericDouTotal2 * 100);
                                                    total1record2 = (  roundedDouTotal2  ).ToString("D13");
                                                }
                                                else
                                                {
                                                    total1record2 = "0000000000000";
                                                }
 

                                                //string total1record1 = "";
                                                //int startingValue1 = 1;

                                                //if (douTotal > 0)
                                                //{
                                                //    long roundedDouTotal = (long)Math.Floor(douTotal * 100);  
                                                //    total1record1 = (startingValue1 + roundedDouTotal - 1).ToString("D13");
                                                //}
                                                //else
                                                //{
                                                //    total1record1 = "0000000000000";
                                                //}


                                                //
                                                string formattedAmount = "";
                                                if (record > 1)
                                                {
                                                    formattedAmount = (record).ToString().PadLeft(6, '0');
                                                }
                                                else
                                                {
                                                    formattedAmount = (index).ToString().PadLeft(6, '0');
                                                }

                                                if (spare3.Length < 68)
                                                    spare3 = spare3.PadRight(68, '0');

                                                //
                                                int countEmpbankRecord1 = list_empbank.Count(worker => worker.empbank_bankpercent != 0 && worker.empbank_bankcode == combank.combank_bankcode);

                                               
                                                int countCashEmpbankRecord1 = list_empbank.Count(worker => worker.empbank_cashpercent != 0 && worker.empbank_bankcode == combank.combank_bankcode);

                                                // sequence1
                                                sequence1 = (douTotal).ToString().PadLeft(7, '0');
                                                string empbankrecord1 = "";
                                                if (countEmpbankRecord1 > 0)
                                                {
                                                    int startingValue = 1;
                                                    empbankrecord1 = ( countEmpbankRecord1  ).ToString().PadLeft(7, '0');
                                                }
                                                else
                                                {
                                                    empbankrecord1 = "000000";
                                                }



                                                // sequence2
                                                sequence2 = (douTotal2).ToString().PadLeft(7, '0');
                                                string empbankrecord2 = "";
                                                if (countCashEmpbankRecord1 >= 0)
                                                {
                                                    int startingValue = 1;
                                                    empbankrecord2 = ( countCashEmpbankRecord1  ).ToString().PadLeft(7, '0');
                                                }
                                                else
                                                {
                                                    empbankrecord2 += "000000";
                                                }


                                                //if (countCashEmpbankRecord1 > 1)
                                                //{
                                                //    empbankrecord2 = countCashEmpbankRecord1.ToString().PadLeft(7, '0');
                                                //}



                                                if (spare3.Length < 68)
                                                    spare3 = spare3.PadRight(68, '0');








                                                bkData = "T" + sequence + combank.combank_bankcode + combank.combank_bankaccount + empbankrecord1 + total1record1 + empbankrecord2 + total1record2 + spare3;

                                                bkData = bkData.PadRight(81, '0');



                                                tmpData += bkData;

                                            }
                                        }
                                        else {
                                            continue;
                                        }
                                    }
                                    break;

                                }
                            default:
                                break;
                        }
                    }
                    try
                    {
                        //-- Step 1 create file
                        string filename = "TRN_BANK_" + DateTime.Now.ToString("yyMMddHHmm") + "." + "txt";
                        string filepath = Path.Combine
                       (ClassLibrary_BPC.Config.PathFileExport, filename);

                        // Check if file already exists. If yes, delete it.     
                        if (File.Exists(filepath))
                        {
                            File.Delete(filepath);
                        }

                        // Create a new file     
                        using (FileStream fs = File.Create(filepath))
                        {
                            // Add some text to file    
                            Byte[] title = new UTF8Encoding(true).GetBytes(tmpData);
                            fs.Write(title, 0, title.Length);
                        }

                        strResult = filename;

                    }
                    catch
                    {
                        strResult = "";
                    }

                }


                task.task_end = DateTime.Now;
                task.task_status = "F";
                task.task_note = strResult;
                objMTTask.updateStatus(task);

            }
            else
            {

            }

            return strResult;
        }
        //TRN_BANK
        
        //SSOเริ่ม
        public string doExportSso(string com, string taskid)
        {
            string strResult = "";

            cls_ctMTTask objMTTask = new cls_ctMTTask();
            List<cls_MTTask> listMTTask = objMTTask.getDataByFillter(com, taskid, "TRN_SSO", "");
            List<string> listError = new List<string>();

            if (listMTTask.Count > 0)
            {
                cls_MTTask task = listMTTask[0];

                task.task_start = DateTime.Now;

                cls_ctMTTask objTaskDetail = new cls_ctMTTask();
                cls_TRTaskdetail task_detail = objTaskDetail.getTaskDetail(task.task_id.ToString());

                cls_ctMTTask objTaskWhose = new cls_ctMTTask();
                List<cls_TRTaskwhose> listWhose = objTaskWhose.getTaskWhose(task.task_id.ToString());

                DateTime dateEff = task_detail.taskdetail_fromdate;
                DateTime datePay = task_detail.taskdetail_paydate;

                StringBuilder objStr = new StringBuilder();
                foreach (cls_TRTaskwhose whose in listWhose)
                {
                    objStr.Append("'" + whose.worker_code + "',");
                }

                string strEmp = objStr.ToString().Substring(0, objStr.ToString().Length - 1);



                //-- Get worker
                cls_ctMTWorker objWorker = new cls_ctMTWorker();
                List<cls_MTWorker> list_worker = objWorker.getDataMultipleEmp(com, strEmp);

                //-- Step 2 Get Paytran
                cls_ctTRPaytran objPay = new cls_ctTRPaytran();
                List<cls_TRPaytran> list_paytran = objPay.getDataMultipleEmp("TH", com, datePay, datePay, strEmp);
                cls_TRPaytran paybank = list_paytran[0];


                //-- Step 3 Get Company acc
                cls_ctTRCombank objCombank = new cls_ctTRCombank();
                List<cls_TRCombank> list_combank = objCombank.getDataByFillter("", com);
                cls_TRCombank combank = list_combank[0];

                //-- Step 4 Get Company detail
                cls_ctMTCompany objCom = new cls_ctMTCompany();
                List<cls_MTCompany> list_com = objCom.getDataByFillter("", com);
                cls_MTCompany comdetail = list_com[0];

                //-- Step 5 Get Emp address
                cls_ctTREmpaddress objEmpadd = new cls_ctTREmpaddress();
                List<cls_TREmpaddress> list_empaddress = objEmpadd.getDataMultipleEmp(com, strEmp);

                //-- Step 6 GetCompany detail
                cls_ctTREmpcard objEmpcard = new cls_ctTREmpcard();
                List<cls_TREmpcard> list_empcard = objEmpcard.getDataTaxMultipleEmp(com, strEmp);

                //-- Step 7 Get Company card
                cls_ctTRComcard objComcard = new cls_ctTRComcard();
                List<cls_TRComcard> list_comcard = objComcard.getDataByFillter(com, "NTID", "", "", "");
                cls_TRComcard comcard = list_comcard[0];

                cls_ctMTProvince objProvince = new cls_ctMTProvince();
                List<cls_MTProvince> list_province = objProvince.getDataByFillter("", "");



                string tmpData = "";


                if (list_paytran.Count > 0)
                {

                    double douTotal = 0;

                    int index = 0;
                    string bkData;

                    foreach (cls_TRPaytran paytran in list_paytran)
                    {

                        string empname = "";

                        cls_MTWorker obj_worker = new cls_MTWorker();
                        cls_TREmpaddress obj_address = new cls_TREmpaddress();
                        cls_MTProvince obj_province = new cls_MTProvince();
                        cls_TREmpcard obj_card = new cls_TREmpcard();

                        foreach (cls_MTWorker worker in list_worker)
                        {
                            if (paytran.worker_code.Equals(worker.worker_code))
                            {
                                empname = worker.initial_name_en + " " + worker.worker_fname_en + " " + worker.worker_lname_en;
                                obj_worker = worker;
                                break;
                            }
                        }

                        foreach (cls_TREmpaddress address in list_empaddress)
                        {
                            if (paytran.worker_code.Equals(address.worker_code))
                            {
                                obj_address = address;
                                break;
                            }
                        }

                        foreach (cls_TREmpcard card in list_empcard)
                        {
                            if (paytran.worker_code.Equals(card.worker_code))
                            {
                                obj_card = card;
                                break;
                            }
                        }

                        foreach (cls_MTProvince province in list_province)
                        {
                            if (obj_address.province_code.Equals(province.province_code))
                            {
                                obj_province = province;
                                break;
                            }
                        }
                        //"Header Record  (ส่วนที่ 1)"     

                        if (empname.Equals("") || obj_card.empcard_code.Equals(""))
                            continue;

                    
                        if (paytran.paytran_income_401 > 0)
                        {
                            
                            //1.ประเภทข้อมูล
                            bkData = obj_card.empcard_id + "|";
                            
                            //2.เลขที่บัญชีนายจ้าง
                            if (combank.combank_bankaccount.Length == 10)
                                bkData += combank.combank_bankaccount + "|";
                            else
                                bkData += combank.combank_bankaccount + "|";



                            //3.ลำดับที่สาขา
                            if (combank.combank_bankcode.Length == 6)
                                bkData += combank.combank_bankcode + "|";
                            else
                                bkData += combank.combank_bankcode + "|";

                            //4.วันที่ชำระเงิน <>

                            bkData += datePay.ToString("ddMMyy") + "|";

                            //5.งวดค่าจ้าง<>	
                            bkData += datePay.ToString("MM") + dateEff.ToString("yy") + "|";
                            

                            //6.ชื่อสถานประกอบการ 
                            
                            bkData += comdetail.company_name_en + "|";

                            //7.อัตราเงินสมทบ
                            if (paybank.paytran_ssocom == 4)
                                bkData += paybank.paytran_ssocom;
                            else
                                bkData += paybank.paytran_ssocom.ToString("0000") + "|";

                                //bkData += paybank.paytran_ssocom + "|";
                            
                         


                            //8.จำนวนผู้ประกันตน			
                            //bkData += paytran.paytran_tax_401.ToString("0.00") + "|";
                            if (paytran.paytran_tax_401 == 6)
                                bkData += paytran.paytran_tax_401;
                            else
                                bkData += paytran.paytran_tax_401.ToString("000000") + "|";



                            //9.ค่าจ้างรวม PAYTRAN_INCOME_TOTAL
                            if (paytran.paytran_income_total == 15)
                                bkData += paytran.paytran_income_total + "|";
                            else 
                                bkData += paytran.paytran_income_total.ToString("000000000000000") + "|";

                            //10.เงินสมทบรวม 
                                if (paytran.paytran_pfcom == 14)
                                    bkData += paytran.paytran_pfcom + "|";
                                else 
                                    bkData += paytran.paytran_pfcom.ToString("00000000000000") + "|";

                            //11.เงินสมทบรวมส่วนผปต. 
                                if (paytran.paytran_income_notax == 12)
                                    bkData += paytran.paytran_income_notax + "|";
                                else
                                    bkData += paytran.paytran_income_notax.ToString("000000000000") + "|";

                            //12.เงินสมทบส่วนนายจ้าง <Poscod>
                               if (paytran.paytran_tax_401 == 12)
                                   bkData += paytran.paytran_tax_401 + "|";
                               else 
                                   bkData += paytran.paytran_tax_401.ToString("000000000000") + "|";
                         


                       

                            tmpData += bkData + '\r' + '\n';
                        }

                        //Detail Record 2
                        if (empname.Equals("") || obj_card.empcard_code.Equals(""))
                            continue;


                        if (paytran.paytran_income_401 > 0)
                        {

                            //1.ประเภทข้อมูล
                            bkData = "2|";


                            //2.เลขที่บัตรประชาชน
                            if (comcard.comcard_code.Length == 13)
                                bkData += comcard.comcard_code + "|";
                            else
                                bkData += "0000000000000|";


                            //3.คำนำหน้าชื่อ
                            bkData += obj_worker.initial_name_th + "|";

                            //4.ชื่อผู้ประกันตน 

                            bkData += obj_worker.worker_fname_th + "|";


                            //5.นามสกุลผู้ประกันตน
                            bkData += obj_worker.worker_lname_th + "|";


                            //6.ค่าจ้าง

                            if (paytran.paytran_income_total == 14)
                                 bkData += paytran.paytran_income_total + "|";
                            else
                                bkData += paytran.paytran_income_total.ToString("00000000000000") + "|";

                            //7.จำนวนเงินสมทบ
                            if (paytran.paytran_pfemp == 12)
                                bkData += paytran.paytran_pfemp + "|";
                            else
                                bkData += paytran.paytran_pfemp.ToString("000000000000") + "|";


                            //8.คอลัมน์ว่าง			                          
                                bkData += "|";


                            tmpData += bkData + '\r' + '\n';
                        }





                        douTotal += paytran.paytran_netpay_b;

                        index++;
                    }

                    int record = list_paytran.Count;


                    try
                    {
                        //-- Step 1 create file
                        string filename = "TRN_SSO_" + DateTime.Now.ToString("yyMMddHHmm") + "." + "txt";
                        string filepath = Path.Combine
                       (ClassLibrary_BPC.Config.PathFileExport, filename);

                        // Check if file already exists. If yes, delete it.     
                        if (File.Exists(filepath))
                        {
                            File.Delete(filepath);
                        }

                        // Create a new file     
                        using (FileStream fs = File.Create(filepath))
                        {
                            // Add some text to file    
                            Byte[] title = new UTF8Encoding(true).GetBytes(tmpData);
                            fs.Write(title, 0, title.Length);
                        }



                        strResult = filename;

                    }
                    catch
                    {
                        strResult = "";
                    }

                }


                task.task_end = DateTime.Now;
                task.task_status = "F";
                task.task_note = strResult;
                objMTTask.updateStatus(task);

            }
            else
            {

            }

            return strResult;
        }
//SSO

        //
        //ภงด 91  PND 91
        public string doExportPND91(string com, string taskid)
        {
            string strResult = "";

            cls_ctMTTask objMTTask = new cls_ctMTTask();
            List<cls_MTTask> listMTTask = objMTTask.getDataByFillter(com, taskid, "TRN_PND91", "");
            List<string> listError = new List<string>();

            if (listMTTask.Count > 0)
            {
                cls_MTTask task = listMTTask[0];

                task.task_start = DateTime.Now;

                cls_ctMTTask objTaskDetail = new cls_ctMTTask();
                cls_TRTaskdetail task_detail = objTaskDetail.getTaskDetail(task.task_id.ToString());

                cls_ctMTTask objTaskWhose = new cls_ctMTTask();
                List<cls_TRTaskwhose> listWhose = objTaskWhose.getTaskWhose(task.task_id.ToString());

                DateTime dateEff = task_detail.taskdetail_fromdate;
                DateTime datePay = task_detail.taskdetail_paydate;

                StringBuilder objStr = new StringBuilder();
                foreach (cls_TRTaskwhose whose in listWhose)
                {
                    objStr.Append("'" + whose.worker_code + "',");
                }

                string strEmp = objStr.ToString().Substring(0, objStr.ToString().Length - 1);



                //-- Get worker
                cls_ctMTWorker objWorker = new cls_ctMTWorker();
                List<cls_MTWorker> list_worker = objWorker.getDataMultipleEmp(com, strEmp);

                //-- Step 2 Get Paytran
                cls_ctTRPaytran objPay = new cls_ctTRPaytran();
                List<cls_TRPaytran> list_paytran = objPay.getDataMultipleEmp("TH", com, datePay, datePay, strEmp);



                //-- Step 3 Get Company acc
                cls_ctTRCombank objCombank = new cls_ctTRCombank();
                List<cls_TRCombank> list_combank = objCombank.getDataByFillter("", com);
                cls_TRCombank combank = list_combank[0];

                //-- Step 4 Get Company detail
                cls_ctMTCompany objCom = new cls_ctMTCompany();
                List<cls_MTCompany> list_com = objCom.getDataByFillter("", com);
                cls_MTCompany comdetail = list_com[0];

                //-- Step 5 Get Emp address
                cls_ctTREmpaddress objEmpadd = new cls_ctTREmpaddress();
                List<cls_TREmpaddress> list_empaddress = objEmpadd.getDataMultipleEmp(com, strEmp);

                //-- Step 6 Get Emp card
                cls_ctTREmpcard objEmpcard = new cls_ctTREmpcard();
                List<cls_TREmpcard> list_empcard = objEmpcard.getDataTaxMultipleEmp(com, strEmp);

                //-- Step 7 Get Company card
                cls_ctTRComcard objComcard = new cls_ctTRComcard();
                List<cls_TRComcard> list_comcard = objComcard.getDataByFillter(com, "NTID", "", "", "");
                cls_TRComcard comcard = list_comcard[0];

                cls_ctMTProvince objProvince = new cls_ctMTProvince();
                List<cls_MTProvince> list_province = objProvince.getDataByFillter("", "");

                 cls_ctTREmpfamily objEmpfamily = new cls_ctTREmpfamily();
                 List<cls_TREmpfamily> list_Empfamily = objEmpfamily.getDataByFillter2(com, strEmp);
                //cls_TREmpfamily Empfamily = list_Empfamily[0];


                //-- Get Empbenefit
                cls_ctTREmpreduce objEmpreduce = new cls_ctTREmpreduce();
                List<cls_TREmpreduce> list_Empreduce = objEmpreduce.getDataByFillter(com, "");


                string tmpData = "";


                if (list_paytran.Count > 0)
                {

                    double douTotal = 0;

                    int index = 0;
                    string bkData;
                    bool reduceData = false;  

                    foreach (cls_TRPaytran paytran in list_paytran)
                    {

                        string empname = "";

                        cls_MTWorker obj_worker = new cls_MTWorker();
                        cls_TREmpaddress obj_address = new cls_TREmpaddress();
                        cls_MTProvince obj_province = new cls_MTProvince();
                        cls_TREmpcard obj_card = new cls_TREmpcard();
                        cls_TREmpfamily obj_TREmpfamily = new cls_TREmpfamily();

                        cls_TREmpreduce obj_TREmpreduce = new cls_TREmpreduce();

                        foreach (cls_MTWorker worker in list_worker)
                        {
                            if (paytran.worker_code.Equals(worker.worker_code))
                            {
                                empname = worker.initial_name_en + " " + worker.worker_fname_en + " " + worker.worker_lname_en;
                                obj_worker = worker;
                                break;
                            }
                        }

                        foreach (cls_TREmpaddress address in list_empaddress)
                        {
                            if (paytran.worker_code.Equals(address.worker_code))
                            {
                                obj_address = address;
                                break;
                            }
                        }

                        foreach (cls_TREmpcard card in list_empcard)
                        {
                            if (paytran.worker_code.Equals(card.worker_code))
                            {
                                obj_card = card;
                                break;
                            }
                        }

                        foreach (cls_MTProvince province in list_province)
                        {
                            if (obj_address.province_code.Equals(province.province_code))
                            {
                                obj_province = province;
                                break;
                            }
                        }


                       


                        if (empname.Equals("") || obj_card.empcard_code.Equals(""))
                            continue;
                            bkData = "";

                            if (paytran.paytran_income_401 > 0)
                            {
                                //1.เลขประจำตัวประชาชน (ผู้มีเงินได้)
                                if (comcard.comcard_code.Length == 13)
                                    bkData += comcard.comcard_code + ",";
                                else
                                    bkData += "0000000000000" + "1." + ",";

                                //2.คำนำหน้าชื่อผู้มีเงินได้<InitialNameT>
                                bkData += obj_worker.initial_name_en + "2." + ",";



                                //4.ชื่อผู้มีเงินได้<EmpFNameT>				
                                bkData += obj_worker.worker_fname_en + "3." + ",";

                                //3.ชื่อกลาง (ถ้ามี)		
                                bkData += " " + "4." + ",";

                                //5.นามสกุลผู้มีเงินได้<EmpLNameT>
                                bkData += obj_worker.worker_lname_en + "5." + ",";

                                //6	สถานะผู้มีเงินได้"“0” = โสด “1” = สมรส “2” = หม้าย"
                                foreach (cls_TREmpfamily worker in list_Empfamily)
                                {
                                    if (paytran.worker_code.Equals(worker.worker_code))
                                    {
                                        bool foundMatchingCondition = false;


                                        if (worker.family_type.Equals("00") || worker.family_type.Equals("02") || worker.family_type.Equals("11"))
                                        {
                                            bkData += "1,= สมรส,"; // “1” = สมรส
                                            foundMatchingCondition = true;
                                        }
                                        //else if (worker.family_type.Equals(""))
                                        //{
                                        //    bkData += "2,= หม้าย"; // “2” = หม้าย
                                        //    foundMatchingCondition = true;
                                        //}

                                        if (!foundMatchingCondition)
                                        {
                                            bkData += "0,= โสด"; // “0” = โสด
                                        }
                                    } break;
                                }


                                //7	สถานภาพการสมรส cls_ctTREmpfamily
                                foreach (cls_TREmpfamily worker in list_Empfamily)
                                {
                                    if (paytran.worker_code.Equals(worker.worker_code))
                                    {
                                        bool foundMatchingCondition = false;
                                        if (worker.family_type.Equals("00"))
                                        {
                                            bkData += "2,= คู่สมรสมีเงินได้,"; //“2” = คู่สมรสมีเงินได้ และอยู่ร่วมกันตลอดปีภาษี
                                            foundMatchingCondition = true;
                                        }
                                        if (!foundMatchingCondition)
                                        {
                                            bkData += "1,= ไม่มีคู่สมรส,"; // “1” = ไม่มีคู่สมรส
                                        }
                                        if (worker.family_type.Equals(""))
                                        {
                                            bkData = "3,"; // “3” = คู่สมรสมีเงินได้ สมรสระหว่างปีภาษี
                                            break;
                                        }
                                        if (worker.family_type.Equals(""))
                                        {
                                            bkData = "4,"; // “3” = คู่สมรสมีเงินได้ สมรสระหว่างปีภาษี
                                            break;
                                        }
                                        if (worker.family_type.Equals(""))
                                        {
                                            bkData = "5,"; // “3” = คู่สมรสมีเงินได้ สมรสระหว่างปีภาษี
                                            break;
                                        }
                                        if (worker.family_type.Equals(""))
                                        {
                                            bkData = "6,"; // “3” = คู่สมรสมีเงินได้ สมรสระหว่างปีภาษี
                                            break;
                                        }
                                        if (worker.family_type.Equals(""))
                                        {
                                            bkData = "7,"; // “3” = คู่สมรสมีเงินได้ สมรสระหว่างปีภาษี
                                            break;
                                        }
                                        if (worker.family_type.Equals(""))
                                        {
                                            bkData = "8,"; // “3” = คู่สมรสมีเงินได้ สมรสระหว่างปีภาษี
                                            break;
                                        }
                                        if (worker.family_type.Equals(""))
                                        {
                                            bkData = "9,"; // “3” = คู่สมรสมีเงินได้ สมรสระหว่างปีภาษี
                                            break;
                                        }
                                    } break;
                                }





                                //foreach (cls_TREmpfamily family in list_Empfamily)
                                //{
                                //    if (paytran.worker_code.Equals(family.worker_code))
                                //    {
                                //        foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                //        {
                                //            if (obj_TREmpreduce != null && paytran.worker_code.Equals(family.worker_code))
                                //            {
                                //                if (TREmpreduce.reduce_type.Equals(""))
                                //                {
                                //                    bkData = "1,"; // “1” = ไม่มีคู่สมรส
                                //                    break;
                                //                }
                                //                else if (family.worker_code.Equals("00") || family.worker_code.Equals("02") || family.worker_code.Equals("11"))
                                //                {
                                //                    bkData = "2,"; // “2” = คู่สมรสมีเงินได้ และอยู่ร่วมกันตลอดปีภาษี
                                //                    break;
                                //                }
                                //                else if (TREmpreduce.reduce_type.Equals("3"))
                                //                {
                                //                    bkData = "3,"; // “3” = คู่สมรสมีเงินได้ สมรสระหว่างปีภาษี
                                //                    break;
                                //                }
                                //                else if (TREmpreduce.reduce_type.Equals("4"))
                                //                {
                                //                    bkData = "4,"; // “4” = คู่สมรสมีเงินได้ หย่าระหว่างปีภาษี
                                //                    break;
                                //                }
                                //                else if (TREmpreduce.reduce_type.Equals("5"))
                                //                {
                                //                    bkData = "5,"; // “5” = คู่สมรสมีเงินได้ ตายระหว่างปีภาษี
                                //                    break;
                                //                }
                                //                else if (TREmpreduce.reduce_type.Equals("6"))
                                //                {
                                //                    bkData = "6,"; // “6” = คู่สมรสไม่มีเงินได้ และอยู่รวมกันตลอดปีภาษี
                                //                    break;
                                //                }
                                //                else if (TREmpreduce.reduce_type.Equals("7"))
                                //                {
                                //                    bkData = "7,"; // “7” = คู่สมรสไม่มีเงินได้ สมรสระหว่างปีภาษี
                                //                    break;
                                //                }
                                //                else if (TREmpreduce.reduce_type.Equals("8"))
                                //                {
                                //                    bkData = "8,"; // “8” = คู่สมรสไม่มีเงินได้ หย่าระหว่างปีภาษี
                                //                    break;
                                //                }
                                //                else if (TREmpreduce.reduce_type.Equals("9"))
                                //                {
                                //                    bkData = "9,"; // “9” = คู่สมรสไม่มีเงินได้ ตายระหว่างปีภาษี
                                //                    break;
                                //                }
                                //            }
                                //        }
                                //    }
                                //}



                                // //8	เลขประจำตัวประชาชน (คู่สมรส)
                                foreach (cls_TREmpfamily family in list_Empfamily)
                                {
                                    if (paytran.worker_code.Equals(family.worker_code))
                                    {


                                        foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                        {
                                            if (family.family_type == "00")
                                            {
                                                if (family.empfamily_code != null && family.empfamily_code.Length == 13)
                                                {
                                                    bkData += family.empfamily_code + "8." + ",";
                                                }
                                                else
                                                {
                                                    bkData += " " + "8." + ",";
                                                }

                                                break;
                                            }
                                        }
                                    }
                                }


                                // //9	คำนำหน้าชื่อ (คู่สมรส)
                                foreach (cls_TREmpfamily family in list_Empfamily)
                                {
                                    if (paytran.worker_code.Equals(family.worker_code))
                                    {
                                        // เพิ่มคำนำหน้าชื่อของคู่สมรสลงใน bkData
                                        //bkData += obj_worker.initial_name_en  ;

                                        // ตรวจสอบประเภทของครอบครัว
                                        if (family.family_type == "00")
                                        {
                                            // หากเป็นประเภท "00" กำหนดค่าให้เป็น "9."
                                            bkData += "ว่าง,";
                                        }


                                        break; // พบข้อมูลแล้วจึงออกจากลูป
                                    }
                                }



                                //foreach (cls_TREmpfamily family in list_Empfamily)
                                //{
                                //    if (paytran.worker_code.Equals(family.worker_code))
                                //    {


                                //        foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                //        {


                                //            if (TREmpreduce.reduce_type.Equals(family.family_type))
                                //            {

                                //                if (family.family_type == "00" || TREmpreduce.reduce_type.Equals("02") || TREmpreduce.reduce_type.Equals("11"))

                                //                    {
                                //                        bkData += " " + "9." + ",";
                                //                    }
                                //                    else
                                //                    {
                                //                        bkData += " " + "9." + ",";
                                //                    }

                                //                    break;
                                //                }

                                //        }
                                //    }
                                //}

                                //10	ชื่อ (คู่สมรส)
                                foreach (cls_TREmpfamily family in list_Empfamily)
                                {
                                    if (paytran.worker_code.Equals(family.worker_code))
                                    {
                                        foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                        {

                                            if (family.family_type == "00")
                                            {
                                                bkData += family.empfamily_fname_en + "10.,";
                                                break;
                                            }

                                        }
                                    }
                                }




                                // //11	ชื่อกลาง (คู่สมรส) (ถ้ามี)
                                foreach (cls_TREmpfamily family in list_Empfamily)
                                {
                                    if (paytran.worker_code.Equals(family.worker_code))
                                    {
                                        foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                        {

                                            if (family.family_type == "00")
                                            {
                                                bkData += "" + "11." + ",";
                                                break;
                                            }

                                        }
                                    }
                                }

                                //    }
                                // //12	นามสกุล (คู่สมรส)
                                foreach (cls_TREmpfamily family in list_Empfamily)
                                {
                                    if (paytran.worker_code.Equals(family.worker_code))
                                    {
                                        foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                        {

                                            if (family.family_type == "00")
                                            {
                                                bkData += family.empfamily_lname_en + "12.,";
                                                break;
                                            }

                                        }
                                    }
                                }


                                // //13	เงินได้พึงประเมิน (ก.1)



                                // //14	หัก เงินบริจาคสนับสนุนการศึกษา (ก.8)
                                foreach (cls_TREmpfamily family in list_Empfamily)
                                {
                                    bool reduceData30 = false; // เพิ่มตัวแปรนี้เพื่อตรวจสอบว่ามีข้อมูลลดหย่อนประเภท "30" หรือไม่

                                    if (paytran.worker_code.Equals(family.worker_code))
                                    {
                                        foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                        {
                                            if (TREmpreduce.reduce_type.Equals("30"))
                                            {
                                                bkData += TREmpreduce.empreduce_amount + "14.,"; // เพิ่มข้อมูลลดหย่อนและเครื่องหมาย "14."
                                                reduceData30 = true;
                                                break;
                                            }
                                        }

                                        if (!reduceData30)
                                        {
                                            bkData += ",";
                                        }
                                    } break;
                                }


                                // //15	หัก เงินบริจาค (ก.10)////////////
                                foreach (cls_TREmpfamily family in list_Empfamily)
                                {

                                    if (paytran.worker_code.Equals(family.worker_code))
                                    {
                                        foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                        {
                                            if (TREmpreduce.reduce_type.Equals("31"))
                                            {
                                                bkData += TREmpreduce.empreduce_amount + "15." + ",";
                                                reduceData = true;
                                                break;

                                            }
                                        }

                                        if (!reduceData)
                                        {
                                            bkData += ",";
                                        }
                                    } break;
                                }


                                // //16	หัก ภาษีหัก ณ ที่จ่าย (ก.13)///
                                foreach (cls_TREmpfamily family in list_Empfamily)
                                {

                                    if (paytran.worker_code.Equals(family.worker_code))
                                    {

                                        bkData += paytran.paytran_tax_401 + "16." + ",";
                                        break;
                                    }
                                    else
                                    {
                                        bkData += ",";
                                        break;
                                    }


                                }



                                // //17	รวมภาษีที่ต้องชำระ (ก.18, 20)
                                foreach (cls_TREmpfamily family in list_Empfamily)
                                {

                                    if (paytran.worker_code.Equals(family.worker_code))
                                    {

                                        bkData += paytran.paytran_tax_4012 + paytran.paytran_tax_4012 + paytran.paytran_tax_4013 + paytran.paytran_tax_402I + paytran.paytran_tax_402O + "17.,";
                                        break;
                                    }
                                    else
                                    {
                                        bkData += ",";
                                        break;
                                    }


                                }
                                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



                                // //18	เงินสะสมกองทุนสำรองเลี้ยงชีพ (ส่วนที่เกิน 10,000) (ข.1)///////////////////////
                                foreach (cls_TREmpfamily family in list_Empfamily)
                                {
                                    if (paytran.worker_code.Equals(family.worker_code))
                                    {
                                        foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                        {
                                            if (TREmpreduce.reduce_type.Equals("PF") && TREmpreduce.empreduce_amount > 10000)
                                            {
                                                bkData += TREmpreduce.empreduce_amount + "ข.1" + ",";
                                                break;
                                            }
                                        }
                                        if (!bkData.EndsWith("ข.1,"))
                                        {
                                            bkData += ",";
                                        }
                                    }
                                }

                                // //19	เงินสะสม กบข. (ข.2)//แก้ไข
                                decimal totalpfAmountk2 = 0m;
                                foreach (cls_TREmpfamily family in list_Empfamily)
                                {
                                    foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                    {
                                        foreach (cls_TREmpreduce empReduce in list_Empreduce)
                                        {
                                            if (obj_worker.worker_code.Equals(empReduce.worker_code))
                                            {
                                                if (empReduce.reduce_type.Equals(" "))
                                                {
                                                    decimal amountToAdd = (decimal)empReduce.empreduce_amount;
                                                    totalpfAmountk2 += amountToAdd;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                                bkData += "0.00" + "19." + ",";

                                // //20	เงินสะสมกองทุนสงเคราะห์ครูฯ (ข.3)//แก้ไข
                                decimal totalpfAmountk3 = 0m;
                                foreach (cls_TREmpfamily family in list_Empfamily)
                                {
                                    foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                    {
                                        foreach (cls_TREmpreduce empReduce in list_Empreduce)
                                        {
                                            if (obj_worker.worker_code.Equals(empReduce.worker_code))
                                            {
                                                if (empReduce.reduce_type.Equals(" "))
                                                {
                                                    decimal amountToAdd = (decimal)empReduce.empreduce_amount;
                                                    totalpfAmountk3 += amountToAdd;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                                bkData += "0.00" + "20." + ",";

                                // //21	ค่าลดหย่อนคู่สมรส (ใบแนบฯ.2)//////////////////แก้ไข
                                double reduce02Data = 0;
                                foreach (cls_TREmpfamily family in list_Empfamily)
                                {
                                    if ((family.family_type == "00" || family.family_type.Equals("02") || family.family_type.Equals("	11")))
                                    {
                                        foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                        {
                                            if (TREmpreduce.reduce_type.Equals("02"))
                                            {
                                                bkData += TREmpreduce.empreduce_amount.ToString("0.00") + "ค่าลดหย่อนคู่,";
                                                reduce02Data += (double)TREmpreduce.empreduce_amount;
                                                break; // หยุดการวนลูปเมื่อพบข้อมูลที่ต้องการ
                                            }
                                        }
                                    }
                                }

                                // หากไม่พบข้อมูลที่ต้องการ
                                if (reduce02Data == 0)
                                {
                                    bkData += "ค่าลดหย่อนคู่// ,"; // ใส่ช่องว่างเมื่อไม่พบข้อมูล
                                }





                              

                                // //22	จำนวนบุตร 30,000 บาท (ใบแนบฯ.3)  มีกี่คน
                                foreach (cls_TREmpfamily family in list_Empfamily)
                                {
                                    bool foundChildReduce = false;
                                    int countChildren = 0;

                                    foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                    {
                                        if (TREmpreduce.reduce_type.Equals("03") || family.family_type == "07" || family.family_type == "10")
                                        {
                                            foundChildReduce = true;
                                            break;
                                        }
                                    }

                                    if (foundChildReduce)
                                    {
                                        bkData += (countChildren + 1) + "f.,";
                                        break;
                                    }
                                    else
                                    {
                                        bkData += "g.,";
                                    }
                                }



                                //bool foundChild7 = false;  
                                bool foundChildReduce1 = false;
                                int countChildren23 = 0;

                                //23 เลขประจำตัวประชาชนบุตร 1
                                foreach (cls_TREmpfamily family in list_Empfamily)
                                {
                                    if (paytran.worker_code.Equals(family.worker_code))
                                    {
                                        foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                        {

                                            if ((family.family_type == "07" || family.family_type == "10") && !foundChildReduce1 && TREmpreduce.reduce_type.Equals("03"))
                                            {
                                                foreach (cls_TREmpfamily prevFamily in list_Empfamily)
                                                {
                                                    // ตรวจสอบว่าวันเกิดของลูกที่กำลังตรวจสอบน้อยกว่าวันเกิดของบุตรปัจจุบัน
                                                    if (prevFamily.empfamily_birthdate <= family.empfamily_birthdate)
                                                    {
                                                        if (prevFamily.empfamily_code != null && prevFamily.empfamily_code.Length == 13)
                                                        {
                                                            bkData += prevFamily.empfamily_code + "ssa,";
                                                            foundChildReduce1 = true;
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            bkData += "ลูกคนที่1";
                                                            foundChildReduce1 = true;

                                                        }
                                                        countChildren23++; // เพิ่มจำนวนบุตรที่พบ
                                                        break; // หยุดการวนลูปเมื่อพบบุตรที่เกิดก่อน
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                int countChildren2 = 0; // เก็บจำนวนลูกที่พบครั้งที่สอง อั๋นโทษทีพอดีจะถามหน่อยAPI ของ Mobile มันตั้งค่า Connection ที่ไฟล์ไหนอะ พอจะจำได้ไหม
                                bool foundChildReduce2 = false; // เช็คว่าพบลูกที่มีการลดหย่อนแล้วหรือไม่ (ครั้งที่สอง)

                                foreach (cls_TREmpfamily family1 in list_Empfamily)
                                {
                                    if (paytran.worker_code.Equals(family1.worker_code))
                                    {
                                        foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                        {
                                            if ((family1.family_type == "07" || family1.family_type == "10") && !foundChildReduce1 && TREmpreduce.reduce_type.Equals("03"))
                                            {
                                                foreach (cls_TREmpfamily prevFamily in list_Empfamily)
                                                {
                                                    // ตรวจสอบว่าวันเกิดของลูกที่กำลังตรวจสอบมากกว่าวันเกิดของบุตรปัจจุบัน
                                                    if (prevFamily.empfamily_birthdate > family1.empfamily_birthdate)
                                                    {
                                                        if (!foundChildReduce2) // ตรวจสอบว่ายังไม่พบลูกคนที่สอง
                                                        {
                                                            foundChildReduce2 = true;
                                                            countChildren2++; // เพิ่มจำนวนลูกคนที่สองที่พบ

                                                            // ตรวจสอบว่าลูกมีรหัสหรือไม่
                                                            if (prevFamily.empfamily_code != null && prevFamily.empfamily_code.Length == 13)
                                                            {
                                                                bkData += prevFamily.empfamily_code + "ssa,";
                                                            }
                                                            else
                                                            {
                                                                bkData += "2aaa3d.,";
                                                            }


                                                            // หยุดการวนลูปเมื่อพบบุตรที่มากกว่า
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    // หากไม่พบลูกคนที่สอง
                                    if (!foundChildReduce2)
                                    {
                                        bkData += "ลูกคนที่สอง,"; break;
                                    }
                                }




                                int countChildren3 = 0; // เก็บจำนวนลูกที่พบครั้งที่สาม
                                bool foundChildReduce3 = false; // เช็คว่าพบลูกที่มีการลดหย่อนแล้วหรือไม่ (ครั้งที่สาม)

                                foreach (cls_TREmpfamily family2 in list_Empfamily)
                                {
                                    if (paytran.worker_code.Equals(family2.worker_code))
                                    {
                                        foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                        {
                                            if ((family2.family_type == "07" || family2.family_type == "10") && !foundChildReduce3 && TREmpreduce.reduce_type.Equals("03"))
                                            {
                                                foreach (cls_TREmpfamily prevFamily in list_Empfamily)
                                                {
                                                    // ตรวจสอบว่าวันเกิดของลูกที่กำลังตรวจสอบมากกว่าวันเกิดของบุตรปัจจุบัน
                                                    if (prevFamily.empfamily_birthdate > family2.empfamily_birthdate && prevFamily.empfamily_birthdate > prevFamily.empfamily_birthdate)
                                                    {
                                                        if (!foundChildReduce3) // ตรวจสอบว่ายังไม่พบลูกคนที่สอง
                                                        {
                                                            foundChildReduce3 = true;
                                                            countChildren3++; // เพิ่มจำนวนลูกคนที่สองที่พบ

                                                            // ตรวจสอบว่าลูกมีรหัสหรือไม่
                                                            if (prevFamily.empfamily_code != null && prevFamily.empfamily_code.Length == 13)
                                                            {
                                                                bkData += prevFamily.empfamily_code + "ssa,";
                                                            }
                                                            else
                                                            {
                                                                bkData += "2aaa3d.,";
                                                            }
                                                        }
                                                        // หยุดการวนลูปเมื่อพบบุตรที่มากกว่า
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    // หากไม่พบลูกคนที่สอง
                                    if (!foundChildReduce3)
                                    {
                                        bkData += "ลูกคนที่3,"; break;
                                    }
                                }





                                int countChildren4 = 0; // เก็บจำนวนลูกที่พบครั้งที่สี่
                                bool foundChildReduce4 = false; // เช็คว่าพบลูกที่มีการลดหย่อนแล้วหรือไม่ (ครั้งที่สี่)

                                foreach (cls_TREmpfamily family3 in list_Empfamily)
                                {
                                    if (paytran.worker_code.Equals(family3.worker_code))
                                    {
                                        foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                        {
                                            if ((family3.family_type == "07" || family3.family_type == "10") && !foundChildReduce1 && TREmpreduce.reduce_type.Equals("03"))
                                            {
                                                foreach (cls_TREmpfamily prevFamily in list_Empfamily)
                                                {
                                                    // ตรวจสอบว่าวันเกิดของลูกที่กำลังตรวจสอบมากกว่าวันเกิดของบุตรที่หนึ่งและสอง และสาม
                                                    if (prevFamily.empfamily_birthdate > family3.empfamily_birthdate && prevFamily.empfamily_birthdate > prevFamily.empfamily_birthdate && prevFamily.empfamily_birthdate > prevFamily.empfamily_birthdate)
                                                    {
                                                        if (!foundChildReduce4) // ตรวจสอบว่ายังไม่พบลูกคนที่สี่
                                                        {
                                                            foundChildReduce4 = true;
                                                            countChildren4++; // เพิ่มจำนวนลูกคนที่สี่ที่พบ

                                                            // ตรวจสอบว่าลูกมีรหัสหรือไม่
                                                            if (prevFamily.empfamily_code != null && prevFamily.empfamily_code.Length == 13)
                                                            {
                                                                bkData += prevFamily.empfamily_code + "ssa,";
                                                            }
                                                            else
                                                            {
                                                                bkData += "2aaa3d.,";
                                                            }
                                                        }

                                                        // หยุดการวนลูปเมื่อพบบุตรที่มากกว่าทั้งคนที่หนึ่ง สอง และสาม
                                                        break;
                                                }
                                            }
                                        }
                                    }
                                }



                                // หากไม่พบลูกคนที่สี่
                                if (!foundChildReduce4)
                                {
                                    bkData += "ลูกคนที่4,"; break;
                                }
                            }


                             int countChildren5 = 0; // เก็บจำนวนลูกที่พบครั้งที่ห้า
                             bool foundChildReduce5 = false; // เช็คว่าพบลูกที่มีการลดหย่อนแล้วหรือไม่ (ครั้งที่ห้า)

                             foreach (cls_TREmpfamily family4 in list_Empfamily)
                             {
                                 if (paytran.worker_code.Equals(family4.worker_code))
                                 {
                                     foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                     {
                                         if ((family4.family_type == "07" || family4.family_type == "10") && !foundChildReduce1 && TREmpreduce.reduce_type.Equals("03"))
                                         {
                                             foreach (cls_TREmpfamily prevFamily in list_Empfamily)
                                             {
                                                 // ตรวจสอบว่าวันเกิดของลูกที่กำลังตรวจสอบมากกว่าวันเกิดของบุตรที่หนึ่ง สอง สาม และสี่
                                                 if (prevFamily.empfamily_birthdate > family4.empfamily_birthdate && prevFamily.empfamily_birthdate > prevFamily.empfamily_birthdate && prevFamily.empfamily_birthdate > prevFamily.empfamily_birthdate && prevFamily.empfamily_birthdate > prevFamily.empfamily_birthdate)
                                                 {
                                                     if (!foundChildReduce5) // ตรวจสอบว่ายังไม่พบลูกคนที่ห้า
                                                     {
                                                         foundChildReduce5 = true;
                                                         countChildren5++; // เพิ่มจำนวนลูกคนที่ห้าที่พบ

                                                         // ตรวจสอบว่าลูกมีรหัสหรือไม่
                                                         if (prevFamily.empfamily_code != null && prevFamily.empfamily_code.Length == 13)
                                                         {
                                                             bkData += prevFamily.empfamily_code + "ssa,";
                                                         }
                                                         else
                                                         {
                                                             bkData += "2aaa3d.,";
                                                         }
                                                     }

                                                     // หยุดการวนลูปเมื่อพบบุตรที่มากกว่าทั้งคนที่หนึ่ง สอง สาม และสี่
                                                     break;
                                                 }
                                             }
                                         }
                                     }
                                 }
                                 //}

                                 // หากไม่พบลูกคนที่ห้า
                                 if (!foundChildReduce5)
                                 {
                                     bkData += "ลูกคนที่5,"; break;
                                 }
                             }


                             int countChildren6 = 0; // เก็บจำนวนลูกที่พบครั้งที่หก
                             bool foundChildReduce6 = false; // เช็คว่าพบลูกที่มีการลดหย่อนแล้วหรือไม่ (ครั้งที่หก)

                             foreach (cls_TREmpfamily family5 in list_Empfamily)
                             {
                                 if (paytran.worker_code.Equals(family5.worker_code))
                                 {
                                     foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                     {
                                         if ((family5.family_type == "07" || family5.family_type == "10") && !foundChildReduce1 && TREmpreduce.reduce_type.Equals("03"))
                                         {
                                             foreach (cls_TREmpfamily prevFamily in list_Empfamily)
                                             {
                                                 // ตรวจสอบว่าวันเกิดของลูกที่กำลังตรวจสอบมากกว่าวันเกิดของบุตรที่หนึ่ง สอง สาม สี่ และห้า
                                                 if (prevFamily.empfamily_birthdate > family5.empfamily_birthdate && prevFamily.empfamily_birthdate > prevFamily.empfamily_birthdate && prevFamily.empfamily_birthdate > prevFamily.empfamily_birthdate && prevFamily.empfamily_birthdate > prevFamily.empfamily_birthdate && prevFamily.empfamily_birthdate > prevFamily.empfamily_birthdate)
                                                 {
                                                     if (!foundChildReduce6) // ตรวจสอบว่ายังไม่พบลูกคนที่หก
                                                     {
                                                         foundChildReduce6 = true;
                                                         countChildren6++; // เพิ่มจำนวนลูกคนที่หกที่พบ

                                                         // ตรวจสอบว่าลูกมีรหัสหรือไม่
                                                         if (prevFamily.empfamily_code != null && prevFamily.empfamily_code.Length == 13)
                                                         {
                                                             bkData += prevFamily.empfamily_code + "ssa,";
                                                         }
                                                         else
                                                         {
                                                             bkData += "2aaa3d.,";
                                                         }
                                                     }

                                                     // หยุดการวนลูปเมื่อพบบุตรที่มากกว่าทั้งคนที่หนึ่ง สอง สาม สี่ และห้า
                                                     break;
                                                 }
                                             }
                                         }
                                     }

                                 }

                                 // หากไม่พบลูกคนที่หก
                                 if (!foundChildReduce6)
                                 {
                                     bkData += "ลูกคนที่6,"; break;
                                 }
                             }

                             int countChildren7 = 0; // เก็บจำนวนลูกที่พบครั้งที่เจ็ด
                             bool foundChildReduce7 = false; // เช็คว่าพบลูกที่มีการลดหย่อนแล้วหรือไม่ (ครั้งที่เจ็ด)

                             foreach (cls_TREmpfamily family6 in list_Empfamily)
                             {
                                 if (paytran.worker_code.Equals(family6.worker_code))
                                 {
                                     foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                     {
                                         if ((family6.family_type == "07" || family6.family_type == "10") && !foundChildReduce1 && TREmpreduce.reduce_type.Equals("03"))
                                         {
                                             foreach (cls_TREmpfamily prevFamily in list_Empfamily)
                                             {
                                                 // ตรวจสอบว่าวันเกิดของลูกที่กำลังตรวจสอบมากกว่าวันเกิดของบุตรที่หนึ่ง สอง สาม สี่ ห้า และหก
                                                 if (prevFamily.empfamily_birthdate > family6.empfamily_birthdate && prevFamily.empfamily_birthdate > prevFamily.empfamily_birthdate && prevFamily.empfamily_birthdate > prevFamily.empfamily_birthdate && prevFamily.empfamily_birthdate > prevFamily.empfamily_birthdate && prevFamily.empfamily_birthdate > prevFamily.empfamily_birthdate && prevFamily.empfamily_birthdate > prevFamily.empfamily_birthdate)
                                                 {
                                                     if (!foundChildReduce7) // ตรวจสอบว่ายังไม่พบลูกคนที่เจ็ด
                                                     {
                                                         foundChildReduce7 = true;
                                                         countChildren7++; // เพิ่มจำนวนลูกคนที่เจ็ดที่พบ

                                                         // ตรวจสอบว่าลูกมีรหัสหรือไม่
                                                         if (prevFamily.empfamily_code != null && prevFamily.empfamily_code.Length == 13)
                                                         {
                                                             bkData += prevFamily.empfamily_code + "ssa,";
                                                         }
                                                         else
                                                         {
                                                             bkData += "2aaa3d.,";
                                                         }
                                                     }

                                                     // หยุดการวนลูปเมื่อพบบุตรที่มากกว่าทั้งคนที่หนึ่ง สอง สาม สี่ ห้า และหก
                                                     break;
                                                 }
                                             }
                                         }

                                     }
                                 }

                                 // หากไม่พบลูกคนที่เจ็ด
                                 if (!foundChildReduce7)
                                 {
                                     bkData += "ลูกคนที่7,"; break;
                                 }
                             }

 


                             // //30	ค่าลดหย่อนบุตร 30,000 บาท (ใบแนบฯ.3)

                             foreach (cls_TREmpfamily family in list_Empfamily)
                             {
                                 bool reduce03Data = false; // ตั้งค่าตัวแปรใหม่ในทุกการวนลูป

                                 if (paytran.worker_code.Equals(family.worker_code))
                                 {
                                     foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                     {
                                         if ((family.family_type == "07" || family.family_type == "10") && TREmpreduce.reduce_type.Equals("03"))
                                         {
                                             bkData += TREmpreduce.empreduce_amount + "30d1,";
                                             reduce03Data = true;
                                             break;
                                         }
                                     }
                                 }
                             }
  

                           // //31	จำนวนบุตร 60,000 บาท (ใบแนบฯ.3) หาจำนวน เช็คปี 2561 หา คศ
                             int checkyear = 0; // สร้างตัวแปรเพื่อเก็บจำนวนเช็คปี 2561

                              foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                             {
                                 foreach (cls_TREmpfamily family in list_Empfamily)
                                 {
                                     int year = family.empfamily_birthdate.Year + 543;

                                     if (year == 2561)
                                     {
                                         if (TREmpreduce.reduce_type.Equals("04"))
                                         {
                                             checkyear++;
                                         }
                                     }
                                 }
                             }
                             bkData += checkyear.ToString() + "3e1." + ",";


                             //bool foundChild7 = false;  
                             bool foundChildReduce104 = false;
                             int countChildren04 = 0;

                             //23 เลขประจำตัวประชาชนบุตร 1
                             foreach (cls_TREmpfamily family in list_Empfamily)
                             {
                                 if (paytran.worker_code.Equals(family.worker_code))
                                 {
                                     foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                     {

                                         if ((family.family_type == "07" || family.family_type == "10") && !foundChildReduce104 && TREmpreduce.reduce_type.Equals("04"))
                                         {
                                             foreach (cls_TREmpfamily prevFamily in list_Empfamily)
                                             {
                                                 // ตรวจสอบว่าวันเกิดของลูกที่กำลังตรวจสอบน้อยกว่าวันเกิดของบุตรปัจจุบัน
                                                 if (prevFamily.empfamily_birthdate > prevFamily.empfamily_birthdate)
                                                 {
                                                     if (prevFamily.empfamily_code != null && prevFamily.empfamily_code.Length == 13)
                                                     {
                                                         bkData += prevFamily.empfamily_code + "ssa,";
                                                         foundChildReduce104 = true;
                                                         break;
                                                     }
                                                     
                                                     countChildren04++; // เพิ่มจำนวนบุตรที่พบ
                                                     break; // หยุดการวนลูปเมื่อพบบุตรที่เกิดก่อน
                                                 }
                                                 else
                                                 {
                                                     bkData += "ลูกคนที่1"; 
                                                     foundChildReduce104 = true;

                                                 }break;
                                             }
                                         }
                                     }
                                 }
                             }


                             int countChildren042 = 0; // เก็บจำนวนลูกที่พบครั้งที่สอง อั๋นโทษทีพอดีจะถามหน่อยAPI ของ Mobile มันตั้งค่า Connection ที่ไฟล์ไหนอะ พอจะจำได้ไหม
                             bool foundChildReduce042 = false; // เช็คว่าพบลูกที่มีการลดหย่อนแล้วหรือไม่ (ครั้งที่สอง)

                             foreach (cls_TREmpfamily family1 in list_Empfamily)
                             {
                                 if (paytran.worker_code.Equals(family1.worker_code))
                                 {
                                     foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                     {
                                         if ((family1.family_type == "07" || family1.family_type == "10") && !foundChildReduce104 && TREmpreduce.reduce_type.Equals("04"))
                                         {
                                             foreach (cls_TREmpfamily prevFamily in list_Empfamily)
                                             {
                                                 // ตรวจสอบว่าวันเกิดของลูกที่กำลังตรวจสอบมากกว่าวันเกิดของบุตรปัจจุบัน
                                                 if (prevFamily.empfamily_birthdate > family1.empfamily_birthdate)
                                                 {
                                                     if (!foundChildReduce042) // ตรวจสอบว่ายังไม่พบลูกคนที่สอง
                                                     {
                                                         foundChildReduce042 = true;
                                                         countChildren042++; // เพิ่มจำนวนลูกคนที่สองที่พบ

                                                         // ตรวจสอบว่าลูกมีรหัสหรือไม่
                                                         if (prevFamily.empfamily_code != null && prevFamily.empfamily_code.Length == 13)
                                                         {
                                                             bkData += prevFamily.empfamily_code + "ssa,";
                                                         }
                                                         else
                                                         {
                                                             bkData += "2aaa3d.,";
                                                         }


                                                         // หยุดการวนลูปเมื่อพบบุตรที่มากกว่า
                                                         break;
                                                     }
                                                 }
                                             }
                                         }
                                     }
                                 }
                                 // หากไม่พบลูกคนที่สอง
                                 if (!foundChildReduce042)
                                 {
                                     bkData += "ลูกคนที่สอง,"; break;
                                 }
                             }




                             int countChildren043 = 0; // เก็บจำนวนลูกที่พบครั้งที่สาม
                             bool foundChildReduce043 = false; // เช็คว่าพบลูกที่มีการลดหย่อนแล้วหรือไม่ (ครั้งที่สาม)

                             foreach (cls_TREmpfamily family2 in list_Empfamily)
                             {
                                 if (paytran.worker_code.Equals(family2.worker_code))
                                 {
                                     foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                     {
                                         if ((family2.family_type == "07" || family2.family_type == "10") && !foundChildReduce104 && TREmpreduce.reduce_type.Equals("04"))
                                         {
                                             foreach (cls_TREmpfamily prevFamily in list_Empfamily)
                                             {
                                                 // ตรวจสอบว่าวันเกิดของลูกที่กำลังตรวจสอบมากกว่าวันเกิดของบุตรปัจจุบัน
                                                 if (prevFamily.empfamily_birthdate > family2.empfamily_birthdate && prevFamily.empfamily_birthdate > prevFamily.empfamily_birthdate)
                                                 {
                                                     if (!foundChildReduce043) // ตรวจสอบว่ายังไม่พบลูกคนที่สอง
                                                     {
                                                         foundChildReduce043 = true;
                                                         countChildren043++; // เพิ่มจำนวนลูกคนที่สองที่พบ

                                                         // ตรวจสอบว่าลูกมีรหัสหรือไม่
                                                         if (prevFamily.empfamily_code != null && prevFamily.empfamily_code.Length == 13)
                                                         {
                                                             bkData += prevFamily.empfamily_code + "ssa,";
                                                         }
                                                         else
                                                         {
                                                             bkData += "2aaa3d.,";
                                                         }
                                                     }
                                                     // หยุดการวนลูปเมื่อพบบุตรที่มากกว่า
                                                     break;
                                                 }
                                             }
                                         }
                                     }
                                 }

                                 // หากไม่พบลูกคนที่สอง
                                 if (!foundChildReduce043)
                                 {
                                     bkData += "ลูกคนที่3,"; break;
                                 }
                             }





                             int countChildren044 = 0; // เก็บจำนวนลูกที่พบครั้งที่สี่
                             bool foundChildReduce044 = false; // เช็คว่าพบลูกที่มีการลดหย่อนแล้วหรือไม่ (ครั้งที่สี่)

                             foreach (cls_TREmpfamily family3 in list_Empfamily)
                             {
                                 if (paytran.worker_code.Equals(family3.worker_code))
                                 {
                                     foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                     {
                                         if ((family3.family_type == "07" || family3.family_type == "10") && !foundChildReduce104 && TREmpreduce.reduce_type.Equals("04"))
                                         {
                                             foreach (cls_TREmpfamily prevFamily in list_Empfamily)
                                             {
                                                 // ตรวจสอบว่าวันเกิดของลูกที่กำลังตรวจสอบมากกว่าวันเกิดของบุตรที่หนึ่งและสอง และสาม
                                                 if (prevFamily.empfamily_birthdate > family3.empfamily_birthdate && prevFamily.empfamily_birthdate > prevFamily.empfamily_birthdate && prevFamily.empfamily_birthdate > prevFamily.empfamily_birthdate)
                                                 {
                                                     if (!foundChildReduce044) // ตรวจสอบว่ายังไม่พบลูกคนที่สี่
                                                     {
                                                         foundChildReduce044 = true;
                                                         countChildren044++; // เพิ่มจำนวนลูกคนที่สี่ที่พบ

                                                         // ตรวจสอบว่าลูกมีรหัสหรือไม่
                                                         if (prevFamily.empfamily_code != null && prevFamily.empfamily_code.Length == 13)
                                                         {
                                                             bkData += prevFamily.empfamily_code + "ssa,";
                                                         }
                                                         else
                                                         {
                                                             bkData += "2aaa3d.,";
                                                         }
                                                     }

                                                     // หยุดการวนลูปเมื่อพบบุตรที่มากกว่าทั้งคนที่หนึ่ง สอง และสาม
                                                     break;
                                                 }
                                             }
                                         }
                                     }
                                 }



                                 // หากไม่พบลูกคนที่สี่
                                 if (!foundChildReduce044)
                                 {
                                     bkData += "ลูกคนที่4,"; break;
                                 }
                             }


                             int countChildren045 = 0; // เก็บจำนวนลูกที่พบครั้งที่ห้า
                             bool foundChildReduce055 = false; // เช็คว่าพบลูกที่มีการลดหย่อนแล้วหรือไม่ (ครั้งที่ห้า)

                             foreach (cls_TREmpfamily family4 in list_Empfamily)
                             {
                                 if (paytran.worker_code.Equals(family4.worker_code))
                                 {
                                     foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                     {
                                         if ((family4.family_type == "07" || family4.family_type == "10") && !foundChildReduce104 && TREmpreduce.reduce_type.Equals("04"))
                                         {
                                             foreach (cls_TREmpfamily prevFamily in list_Empfamily)
                                             {
                                                 // ตรวจสอบว่าวันเกิดของลูกที่กำลังตรวจสอบมากกว่าวันเกิดของบุตรที่หนึ่ง สอง สาม และสี่
                                                 if (prevFamily.empfamily_birthdate > family4.empfamily_birthdate && prevFamily.empfamily_birthdate > prevFamily.empfamily_birthdate && prevFamily.empfamily_birthdate > prevFamily.empfamily_birthdate && prevFamily.empfamily_birthdate > prevFamily.empfamily_birthdate)
                                                 {
                                                     if (!foundChildReduce055) // ตรวจสอบว่ายังไม่พบลูกคนที่ห้า
                                                     {
                                                         foundChildReduce055 = true;
                                                         countChildren045++; // เพิ่มจำนวนลูกคนที่ห้าที่พบ

                                                         // ตรวจสอบว่าลูกมีรหัสหรือไม่
                                                         if (prevFamily.empfamily_code != null && prevFamily.empfamily_code.Length == 13)
                                                         {
                                                             bkData += prevFamily.empfamily_code + "ssa,";
                                                         }
                                                         else
                                                         {
                                                             bkData += "2aaa3d.,";
                                                         }
                                                     }

                                                     // หยุดการวนลูปเมื่อพบบุตรที่มากกว่าทั้งคนที่หนึ่ง สอง สาม และสี่
                                                     break;
                                                 }
                                             }
                                         }
                                     }
                                 }
                                 //}

                                 // หากไม่พบลูกคนที่ห้า
                                 if (!foundChildReduce055)
                                 {
                                     bkData += "ลูกคนที่5,"; break;
                                 }
                             }
 


 
 
                           // //37	ค่าลดหย่อนบุตร 60,000 บาท (ใบแนบฯ.3)
                             bool reduce04Data = false;
                             foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                             {
                                 if (obj_worker.worker_code.Equals(TREmpreduce.worker_code) && TREmpreduce.reduce_type.Equals("04"))
                                 {
                                     bkData += TREmpreduce.empreduce_amount + ",";
                                     reduce04Data = true;
                                 }
                             }
                             if (!reduce04Data)
                             {
                                 bkData += ",";
                             }

                           // //38	ค่าลดหย่อนบิดา ผู้มีเงินได้ (ใบแนบฯ.4)
 
                             double totalFatherReduce = 0;
                             foreach (cls_TREmpfamily family in list_Empfamily)
                             {
                                 if ((family.family_type == "04" ))
                                 {
                                     foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                     {
                                         if (TREmpreduce.reduce_type.Equals("05"))
                                         {
                                             bkData += TREmpreduce.empreduce_amount.ToString("0.00") + "ค่าลดหย่อนบิดา1,";
                                             totalFatherReduce += (double)TREmpreduce.empreduce_amount;
                                             break; // หยุดการวนลูปเมื่อพบข้อมูลที่ต้องการ
                                         }
                                     }
                                 }
                             }

                             // หากไม่พบข้อมูลที่ต้องการ
                             if (totalFatherReduce == 0)
                             {
                                 bkData += " ,"; // ใส่ช่องว่างเมื่อไม่พบข้อมูล
                             }


                              



                             ///

                           // //39	เลขประจำตัวประชาชนบิดา ผู้มีเงินได้ (ใบแนบฯ.4)
                             foreach (cls_TREmpfamily family in list_Empfamily)
                             {
                                 if (paytran.worker_code.Equals(family.worker_code))
                                 {
                                     foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                     {
                                         if (TREmpreduce.reduce_type.Equals("05") && family.family_type.Equals("04"))
                                         {
                                             if (family.empfamily_code != null && family.empfamily_code.Length == 13)
                                             {
                                                 bkData += family.empfamily_code + "เลขประจำตัวประชาชนบิดา,";
                                                 break;
                                             }
                                             else
                                             {
                                                 bkData += " " + "เลขประจำตัวประชาชนบิดา,";
                                             }
                                             break;
                                         }
                                     }
                                 }
                             }

 

                           // //40	ค่าลดหย่อนมารดา ผู้มีเงินได้ (ใบแนบฯ.4)
                             double reduce06Data = 0;
                             foreach (cls_TREmpfamily family in list_Empfamily)
                             {
                                 if ((family.family_type == "09"))
                                 {
                                     foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                     {
                                         if (TREmpreduce.reduce_type.Equals("06"))
                                         {
                                             bkData += TREmpreduce.empreduce_amount.ToString("0.00") + "ค่าลดหย่อนมารดา11,";
                                             reduce06Data += (double)TREmpreduce.empreduce_amount;
                                             break; // หยุดการวนลูปเมื่อพบข้อมูลที่ต้องการ
                                         }
                                     }
                                 }
                             }

                             // หากไม่พบข้อมูลที่ต้องการ
                             if (reduce06Data == 0)
                             {
                                 bkData += "ค่าลดหย่อนมารดา// ,"; // ใส่ช่องว่างเมื่อไม่พบข้อมูล
                             }


 

                           // //41	เลขประจำตัวประชาชนมารดา ผู้มีเงินได้ (ใบแนบฯ.4)
                             foreach (cls_TREmpfamily family in list_Empfamily)
                             {
                                 if (paytran.worker_code.Equals(family.worker_code))
                                 {
                                     foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                     {
                                         if (TREmpreduce.reduce_type.Equals("06") && family.family_type.Equals("09"))
                                         {
                                             if (family.empfamily_code != null && family.empfamily_code.Length == 13)
                                             {
                                                 bkData += family.empfamily_code + ",";
                                                 break;
                                             }
                                             else
                                             {
                                                 bkData += " " + ",";
                                             }
                                             break;
                                         }
                                     }
                                 }
                             }

                                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                           // //42	ค่าลดหย่อนบิดา คู่สมรส (ใบแนบฯ.4)
                             double reduce1617Data = 0;
                             foreach (cls_TREmpfamily family in list_Empfamily)
                             {
                                 if ((family.family_type == "17" || family.family_type.Equals("16")))
                                 {
                                     foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                     {
                                         if (TREmpreduce.reduce_type.Equals("07"))
                                         {
                                             bkData += TREmpreduce.empreduce_amount.ToString("0.00") + "ค่าลดหย่อนบิดา,";
                                             reduce1617Data += (double)TREmpreduce.empreduce_amount;
                                             break; // หยุดการวนลูปเมื่อพบข้อมูลที่ต้องการ
                                         }
                                     }
                                 }
                             }

                             // หากไม่พบข้อมูลที่ต้องการ
                             if (reduce1617Data == 0)
                             {
                                 bkData += "ค่าลดหย่อนบิดา// ,"; // ใส่ช่องว่างเมื่อไม่พบข้อมูล
                             }

 

                          

                           // //43	เลขประจำตัวประชาชนบิดา คู่สมรส (ใบแนบฯ. 4)
                             foreach (cls_TREmpfamily family in list_Empfamily)
                             {
                                 if (paytran.worker_code.Equals(family.worker_code))
                                 {
                                     foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                     {
                                         if (TREmpreduce.reduce_type.Equals("07") && (family.family_type.Equals("17") || family.family_type.Equals("16")))
                                         {
                                             if (family.empfamily_code != null && family.empfamily_code.Length == 13)
                                             {
                                                 bkData += family.empfamily_code + ",";
                                                 break;
                                             }
                                             else
                                             {
                                                 bkData += " " + ",";
                                             }
                                             break;
                                         }
                                     }
                                 }
                             }


                           // //44	ค่าลดหย่อนมารดา คู่สมรส (ใบแนบฯ.4)
                             double reduce08Data = 0;
                             foreach (cls_TREmpfamily family in list_Empfamily)
                             {
                                 if ((family.family_type == "17" || family.family_type.Equals("09")))
                                 {
                                     foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                     {
                                         if (TREmpreduce.reduce_type.Equals("08"))
                                         {
                                             bkData += TREmpreduce.empreduce_amount.ToString("0.00") + "ค่าลดหย่อนมารดา,";
                                             reduce08Data += (double)TREmpreduce.empreduce_amount;
                                             break; // หยุดการวนลูปเมื่อพบข้อมูลที่ต้องการ
                                         }
                                     }
                                 }
                             }

                             // หากไม่พบข้อมูลที่ต้องการ
                             if (reduce08Data == 0)
                             {
                                 bkData += "ค่าลดหย่อนมารดา// ,"; // ใส่ช่องว่างเมื่อไม่พบข้อมูล
                             }


 




                           // //45	เลขประจำตัวประชาชนมารดา คู่สมรส (ใบแนบฯ.4)
                             foreach (cls_TREmpfamily family in list_Empfamily)
                             {
                                 if (paytran.worker_code.Equals(family.worker_code))
                                 {
                                     foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                     {
                                         if (TREmpreduce.reduce_type.Equals("08") && (family.family_type.Equals(" ") || family.family_type.Equals(" ")))//ยังไม่มีข้อมูล
                                         {
                                             if (family.empfamily_code != null && family.empfamily_code.Length == 13)
                                             {
                                                 bkData += family.empfamily_code + ",";
                                                 break;
                                             }
                                             else
                                             {
                                                 bkData += " " + ",";
                                             }
                                             break;
                                         }
                                     }
                                 }
                             }

                           // //46	เลขประจำตัวประชาชนบิดา ผู้มีเงินได้ ที่ประกันสุขภาพ (ใบแนบฯ.6)
                             foreach (cls_TREmpfamily family in list_Empfamily)
                             {
                                 if (paytran.worker_code.Equals(family.worker_code))
                                 {
                                     foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                     {
                                         if (TREmpreduce.reduce_type.Equals("10") && family.family_type.Equals("04"))
                                         {
                                             if (family.empfamily_code != null && family.empfamily_code.Length == 13)
                                             {
                                                 bkData += family.empfamily_code + ",";
                                                 break;
                                             }
                                             else
                                             {
                                                 bkData += " " + ",";
                                             }
                                             break;
                                         }
                                     }
                                 }
                             }

                           // //47	เลขประจำตัวประชาชนมารดา ผู้มีเงินได้ ที่ประกันสุขภาพ(ใบแนบฯ.6)
                             foreach (cls_TREmpfamily family in list_Empfamily)
                             {
                                 if (paytran.worker_code.Equals(family.worker_code))
                                 {
                                     foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                     {
                                         if (TREmpreduce.reduce_type.Equals("11") && family.family_type.Equals("09"))
                                         {
                                             if (family.empfamily_code != null && family.empfamily_code.Length == 13)
                                             {
                                                 bkData += family.empfamily_code + ",";
                                                 break;
                                             }
                                             else
                                             {
                                                 bkData += " " + ",";
                                             }
                                             break;
                                         }
                                     }
                                 }
                             }


                           // //48	เบี้ยประกันสุขภาพบิดา, มารดา ผู้มีเงินได้ (ใบแนบฯ.6)
                             decimal totalInsuranceAmount = 0m;

                             foreach (cls_TREmpreduce empReduce in list_Empreduce)
                             {
                                 if (obj_worker.worker_code.Equals(empReduce.worker_code))
                                 {
                                     if (empReduce.reduce_type.Equals("10"))
                                     {
                                         decimal amountToAdd10 = (decimal)empReduce.empreduce_amount;
                                         totalInsuranceAmount += amountToAdd10;
                                     }
                                     if (empReduce.reduce_type.Equals("11"))
                                     {
                                         decimal amountToAdd11 = (decimal)empReduce.empreduce_amount;
                                         totalInsuranceAmount += amountToAdd11;
                                     }
                                 }
                             }

                             bkData += totalInsuranceAmount.ToString("0.00") + ",";



                           // //49	เลขประจำตัวประชาชนบิดา คู่สมรส ที่ประกันสุขภาพ (ใบแนบฯ.6)
                             foreach (cls_TREmpfamily family in list_Empfamily)
                             {
                                 if (paytran.worker_code.Equals(family.worker_code))
                                 {
                                     foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                     {
                                         if (TREmpreduce.reduce_type.Equals("12") && (family.family_type.Equals("17") || family.family_type.Equals("16")))
                                         {
                                             if (family.empfamily_code != null && family.empfamily_code.Length == 13)
                                             {
                                                 bkData += family.empfamily_code + ",";
                                                 break;
                                             }
                                             else
                                             {
                                                 bkData += " " + ",";
                                             }
                                             break;
                                         }
                                     }
                                 }
                             }

                           // //50	เลขประจำตัวประชาชนมารดา คู่สมรส ที่ประกันสุขภาพ (ใบแนบฯ.6)
                          foreach (cls_TREmpfamily family in list_Empfamily)
                             {
                                 if (paytran.worker_code.Equals(family.worker_code))
                                 {
                                     foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                     {
                                         if (TREmpreduce.reduce_type.Equals("16") && family.family_type.Equals(""))//ยังไม่มีข้อมูล
                                         {
                                             if (family.empfamily_code != null && family.empfamily_code.Length == 13)
                                             {
                                                 bkData += family.empfamily_code + ",";
                                                 break;
                                             }
                                             else
                                             {
                                                 bkData += " " + ",";
                                             }
                                             break;
                                         }
                                     }
                                 }
                             }


                           // //51	เบี้ยประกันสุขภาพบิดา, มารดา คู่สมรส (ใบแนบฯ.6)
                          decimal totalSpouseAmount = 0m;
                          foreach (cls_TREmpreduce empReduce in list_Empreduce)
                          {
                              if (obj_worker.worker_code.Equals(empReduce.worker_code))
                              {
                                  if (empReduce.reduce_type.Equals("12"))
                                  {
                                      decimal amountToAdd10 = (decimal)empReduce.empreduce_amount;
                                      totalSpouseAmount += amountToAdd10;
                                  }
                                  if (empReduce.reduce_type.Equals("13"))
                                  {
                                      decimal amountToAdd11 = (decimal)empReduce.empreduce_amount;
                                      totalSpouseAmount += amountToAdd11;
                                  }
                              }
                          }

                          bkData += totalSpouseAmount.ToString("0.00") + ",";







                           // //52	เบี้ยประกันชีวิต (ใบแนบฯ.7)
                          decimal totalpremiumsAmount = 0m;
                          foreach (cls_TREmpreduce empReduce in list_Empreduce)
                          {
                              if (obj_worker.worker_code.Equals(empReduce.worker_code))
                              {
                                  if (empReduce.reduce_type.Equals("15"))
                                  {
                                      decimal amountToAdd = (decimal)empReduce.empreduce_amount;
                                      totalpremiumsAmount += amountToAdd;
                                      break;
                                  }
                              }
                          }
                          bkData += totalpremiumsAmount.ToString("0.00") + ",";





                           // //53	เบี้ยประกันสุขภาพผู้มีเงินได้ (ใบแนบฯ.7)
                          decimal totalpremiumsAmount16 = 0m;
                          foreach (cls_TREmpreduce empReduce in list_Empreduce)
                          {
                              if (obj_worker.worker_code.Equals(empReduce.worker_code))
                              {
                                  if (empReduce.reduce_type.Equals("16"))
                                  {
                                      decimal amountToAdd = (decimal)empReduce.empreduce_amount;
                                      totalpremiumsAmount16 += amountToAdd;
                                      break;
                                  }
                              }
                          }
                          bkData += totalpremiumsAmount16.ToString("0.00") + ",";


                           // //54	เบี้ยประกันชีวิตแบบบำนาญ (ใบแนบฯ.7)
                          decimal totalpremiumsAmount17 = 0m;
                          foreach (cls_TREmpreduce empReduce in list_Empreduce)
                          {
                              if (obj_worker.worker_code.Equals(empReduce.worker_code))
                              {
                                  if (empReduce.reduce_type.Equals("17"))
                                  {
                                      decimal amountToAdd = (decimal)empReduce.empreduce_amount;
                                      totalpremiumsAmount17 += amountToAdd;
                                      break;
                                  }
                              }
                          }
                          bkData += totalpremiumsAmount17.ToString("0.00") + ",";

                           // //55	เบี้ยประกันชีวิต คู่สมรส (ใบแนบฯ.7)
                          decimal totalpremiumsAmount15 = 0m;
                          foreach (cls_TREmpfamily family in list_Empfamily)
                          {
                              foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                              {
                                  foreach (cls_TREmpreduce empReduce in list_Empreduce)
                                  {
                                      if (obj_worker.worker_code.Equals(empReduce.worker_code))
                                      {
                                          if (empReduce.reduce_type.Equals("15"))
                                          {
                                              decimal amountToAdd = (decimal)empReduce.empreduce_amount;
                                              totalpremiumsAmount15 += amountToAdd;
                                              break;
                                          }
                                      }
                                  }
                              }
                          }
                          bkData += totalpremiumsAmount15.ToString("0.00") + ",";




                           // //56	เงินสะสมกองทุนสำรองเลี้ยงชีพ(ส่วนที่ไม่เกิน 10,000) (ใบแนบฯ.8)
                          decimal totalpfAmount = 0m;
                          foreach (cls_TREmpfamily family in list_Empfamily)
                          {
                              foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                              {
                                  foreach (cls_TREmpreduce empReduce in list_Empreduce)
                                  {
                                      if (obj_worker.worker_code.Equals(empReduce.worker_code))
                                      {
                                          if (empReduce.reduce_type.Equals("PF"))
                                          {
                                              decimal amountToAdd = (decimal)empReduce.empreduce_amount;
                                              totalpfAmount += amountToAdd;
                                              break;
                                          }
                                      }
                                  }
                              }
                          }
                          bkData += totalpfAmount.ToString("0.00") + ",";



                           // //57	ค่าซื้อหน่วยลงทุนในกองทุนรวมเพื่อการเลี้ยงชีพ (ใบแนบฯ.10)
                          decimal totalRMFAmount = 0m;
                          foreach (cls_TREmpfamily family in list_Empfamily)
                          {
                              foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                              {
                                  foreach (cls_TREmpreduce empReduce in list_Empreduce)
                                  {
                                      if (obj_worker.worker_code.Equals(empReduce.worker_code))
                                      {
                                          if (empReduce.reduce_type.Equals("RMF"))
                                          {
                                              decimal amountToAdd = (decimal)empReduce.empreduce_amount;
                                              totalRMFAmount += amountToAdd;
                                              break;
                                          }
                                      }
                                  }
                              }
                          }
                          bkData += totalRMFAmount.ToString("0.00") + ",";


                           // //58	ค่าซื้อหน่วยลงทุนในกองทุนรวมเพื่อการออม SSF (ใบแนบฯ.11)
                          decimal totalssfAmount = 0m;
                          foreach (cls_TREmpfamily family in list_Empfamily)
                          {
                              foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                              {
                                  foreach (cls_TREmpreduce empReduce in list_Empreduce)
                                  {
                                      if (obj_worker.worker_code.Equals(empReduce.worker_code))
                                      {
                                          if (empReduce.reduce_type.Equals("SSF"))
                                          {
                                              decimal amountToAdd = (decimal)empReduce.empreduce_amount;
                                              totalssfAmount += amountToAdd;
                                              break;
                                          }
                                      }
                                  }
                              }
                          }
                          bkData += totalssfAmount.ToString("0.00") + ",";



                           // //59	ดอกเบี้ยเงินกู้ยืมฯ (ใบแนบฯ.12)
                          decimal totalInterestonloansAmount = 0m;
                          foreach (cls_TREmpfamily family in list_Empfamily)
                          {
                              foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                              {
                                  foreach (cls_TREmpreduce empReduce in list_Empreduce)
                                  {
                                      if (obj_worker.worker_code.Equals(empReduce.worker_code))
                                      {
                                          if (empReduce.reduce_type.Equals("22"))
                                          {
                                              decimal amountToAdd = (decimal)empReduce.empreduce_amount;
                                              totalInterestonloansAmount += amountToAdd;
                                              break;
                                          }
                                      }
                                  }
                              }
                          }
                          bkData += totalInterestonloansAmount.ToString("0.00") + ",";


                           // //60	เงินสมทบกองทุนประกันสังคม (ใบแนบ.13)
                          decimal totalSSOAmount = 0m;
                          foreach (cls_TREmpfamily family in list_Empfamily)
                          {
                              foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                              {
                                  foreach (cls_TREmpreduce empReduce in list_Empreduce)
                                  {
                                      if (obj_worker.worker_code.Equals(empReduce.worker_code))
                                      {
                                          if (empReduce.reduce_type.Equals("SSO"))
                                          {
                                              decimal amountToAdd = (decimal)empReduce.empreduce_amount;
                                              totalSSOAmount += amountToAdd;
                                              break;
                                          }
                                      }
                                  }
                              }
                          }
                          bkData += totalSSOAmount.ToString("0.00") + ",";

                           // //61	เงินสมทบกองทุนประกันสังคมคู่สมรส (สูงสุดไม่เกิน 5,100)(ใบแนบฯ.13)
                          decimal totalSocialsecuritycontributionsAmount = 0m;
                          foreach (cls_TREmpfamily family in list_Empfamily)
                          {
                              foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                              {
                                  foreach (cls_TREmpreduce empReduce in list_Empreduce)
                                  {
                                      if (obj_worker.worker_code.Equals(empReduce.worker_code))
                                      {
                                          if (empReduce.reduce_type.Equals("SSO") && family.family_type.Equals("11"))
                                          {
                                              decimal amountToAdd = (decimal)empReduce.empreduce_amount;
                                              totalSocialsecuritycontributionsAmount += amountToAdd;
                                              break;
                                          }
                                      }
                                  }
                              }
                          }
                          bkData += "0.00" + ",";

                           // //62	ค่าฝากครรภ์และค่าคลอดบุตร (ใบแนบฯ.16)
                          decimal totalAmount26 = 0m;
                          foreach (cls_TREmpfamily family in list_Empfamily)
                          {

                              foreach (cls_TREmpreduce empReduce in list_Empreduce)
                              {
                                  if (obj_worker.worker_code.Equals(empReduce.worker_code))
                                  {
                                      if (empReduce.reduce_type.Equals("26"))
                                      {
                                          decimal amountToAdd = (decimal)empReduce.empreduce_amount;
                                          totalAmount26 += amountToAdd;
                                          break;
                                      }
                                  }

                              }
                          }
                          bkData += totalAmount26.ToString("0.00") + ",";

                           // //63	รวมค่าลดหย่อน (ใบแนบฯ.19)
                          double totalEmpreduceAmount = 0;
                          foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                          {
                              if (obj_worker.worker_code.Equals(TREmpreduce.worker_code))
                              {
                                  totalEmpreduceAmount += (double)TREmpreduce.empreduce_amount;
                              }
                          }
                          decimal resultEmpreduce = (decimal)totalEmpreduceAmount;
                          bkData += resultEmpreduce + "|";


                            ///

 

                            tmpData += bkData + '\r' + '\n';
                        }





                        douTotal += paytran.paytran_netpay_b;

                        index++;
                    }

                    int record = list_paytran.Count;


                    try
                    {
                        //-- Step 1 create file
                        string filename = "TRN_PND91_" + DateTime.Now.ToString("yyMMddHHmm") + "." + "txt";
                        string filepath = Path.Combine
                       (ClassLibrary_BPC.Config.PathFileExport, filename);

                        // Check if file already exists. If yes, delete it.     
                        if (File.Exists(filepath))
                        {
                            File.Delete(filepath);
                        }

                        // Create a new file     
                        using (FileStream fs = File.Create(filepath))
                        {
                            // Add some text to file    
                            Byte[] title = new UTF8Encoding(true).GetBytes(tmpData);
                            fs.Write(title, 0, title.Length);
                        }

                        strResult = filename;

                    }
                    catch
                    {
                        strResult = "";
                    }

                }


                task.task_end = DateTime.Now;
                task.task_status = "F";
                task.task_note = strResult;
                objMTTask.updateStatus(task);

            }
            else
            {

            }

            return strResult;
        }
        ///ภงด 91  PND 91
        //

        //TAX เริ่ม
        public string doExportTax(string com, string taskid)
        {
            string strResult = "";

            cls_ctMTTask objMTTask = new cls_ctMTTask();
            List<cls_MTTask> listMTTask = objMTTask.getDataByFillter(com, taskid, "TRN_TAX", "");
            List<string> listError = new List<string>();

            if (listMTTask.Count > 0)
            {
                cls_MTTask task = listMTTask[0];

                task.task_start = DateTime.Now;

                cls_ctMTTask objTaskDetail = new cls_ctMTTask();
                cls_TRTaskdetail task_detail = objTaskDetail.getTaskDetail(task.task_id.ToString());

                cls_ctMTTask objTaskWhose = new cls_ctMTTask();
                List<cls_TRTaskwhose> listWhose = objTaskWhose.getTaskWhose(task.task_id.ToString());

                DateTime dateEff = task_detail.taskdetail_fromdate;
                DateTime datePay = task_detail.taskdetail_paydate;

                StringBuilder objStr = new StringBuilder();
                foreach (cls_TRTaskwhose whose in listWhose)
                {
                    objStr.Append("'" + whose.worker_code + "',");
                }

                string strEmp = objStr.ToString().Substring(0, objStr.ToString().Length - 1);



                //-- Get worker
                cls_ctMTWorker objWorker = new cls_ctMTWorker();
                List<cls_MTWorker> list_worker = objWorker.getDataMultipleEmp(com, strEmp);

                //-- Step 2 Get Paytran
                cls_ctTRPaytran objPay = new cls_ctTRPaytran();
                List<cls_TRPaytran> list_paytran = objPay.getDataMultipleEmp("TH", com, datePay, datePay, strEmp);



                //-- Step 3 Get Company acc
                cls_ctTRCombank objCombank = new cls_ctTRCombank();
                List<cls_TRCombank> list_combank = objCombank.getDataByFillter("", com);
                cls_TRCombank combank = list_combank[0];

                //-- Step 4 Get Company detail
                cls_ctMTCompany objCom = new cls_ctMTCompany();
                List<cls_MTCompany> list_com = objCom.getDataByFillter("", com);
                cls_MTCompany comdetail = list_com[0];

                //-- Step 5 Get Emp address
                cls_ctTREmpaddress objEmpadd = new cls_ctTREmpaddress();
                List<cls_TREmpaddress> list_empaddress = objEmpadd.getDataMultipleEmp(com, strEmp);

                //-- Step 6 Get Emp card
                cls_ctTREmpcard objEmpcard = new cls_ctTREmpcard();
                List<cls_TREmpcard> list_empcard = objEmpcard.getDataTaxMultipleEmp(com, strEmp);

                //-- Step 7 Get Company card
                cls_ctTRComcard objComcard = new cls_ctTRComcard();
                List<cls_TRComcard> list_comcard = objComcard.getDataByFillter(com, "NTID", "", "", "");
                cls_TRComcard comcard = list_comcard[0];

                cls_ctMTProvince objProvince = new cls_ctMTProvince();
                List<cls_MTProvince> list_province = objProvince.getDataByFillter("", "");

              
        

                

                string tmpData = "";


                if (list_paytran.Count > 0)
                {
                    
                    double douTotal = 0;

                    int index = 0;
                    string bkData;

                    foreach (cls_TRPaytran paytran in list_paytran)
                    {
                   
                        string empname = "";
                        
                        cls_MTWorker obj_worker = new cls_MTWorker();
                        cls_TREmpaddress obj_address = new cls_TREmpaddress();
                        cls_MTProvince obj_province = new cls_MTProvince();
                        cls_TREmpcard obj_card = new cls_TREmpcard();

                        foreach (cls_MTWorker worker in list_worker)
                        {
                            if (paytran.worker_code.Equals(worker.worker_code))
                            {
                                empname = worker.initial_name_en + " " + worker.worker_fname_en + " " + worker.worker_lname_en;
                                obj_worker = worker;
                                break;
                            }
                        }

                        foreach (cls_TREmpaddress address in list_empaddress)
                        {
                            if (paytran.worker_code.Equals(address.worker_code))
                            {
                                obj_address = address;
                                break;
                            }
                        }

                        foreach (cls_TREmpcard card in list_empcard)
                        {
                            if (paytran.worker_code.Equals(card.worker_code))
                            {
                                obj_card = card;
                                break;
                            }
                        }

                        foreach (cls_MTProvince province in list_province)
                        {
                            if (obj_address.province_code.Equals(province.province_code))
                            {
                                obj_province = province;
                                break;
                            }
                        }


                        if (empname.Equals("") || obj_card.empcard_code.Equals(""))
                            continue;


                        if (paytran.paytran_income_401 > 0)
                        {
                            //1.ลักษณะการยื่นแบบปกติ
                            bkData = "00" + ",";
                            
                            //2.เลขประจำตัวประชาชนผู้มี่หน้าที่หัก ณ ที่จ่าย<CardNo>	comcard.card_type
                            if (comcard.comcard_code.Length == 13)
                                bkData += comcard.comcard_code + ",";
                            else
                                bkData += "0000000000000" + ",";

                            //3.เลขประจำตัวผู้เสียภาษีอากรผู้มีหน้าที่หัก ณ ที่จ่าย<TaxNo>
                            if (comcard.card_type.Length == 10)
                                bkData += comcard.comcard_code + ",";
                            else
                                bkData += "0000000000" + ",";

                            //4.เลขที่สาขา ผู้มีหน้าที่หักภาษี ณ ที่จ่าย<BranchID>

                            if (combank.company_code.Length == 4)
                                bkData += combank.company_code + ",";
                            else
                                bkData += "00000" + ",";

                            //5.เลขประจำตัวประชาชนผู้มีเงินได้<CardNo>	
                            if (obj_card.empcard_code.Length == 13)
                                bkData += obj_card.empcard_code + ",";
                            else
                                bkData += "0000000000000" + ",";

                            //6.เลขประจำตัวผู้เสียภาษีอากรผู้มีเงินได้ <TaxNo>
                            if (obj_card.empcard_code.Length == 13)
                                bkData += obj_card.empcard_code + ",";
                            else
                                bkData += "0000000000" + ",";

                            //7.คำนำหน้าชื่อผู้มีเงินได้<InitialNameT>
                            bkData += obj_worker.initial_name_en + ",";

                            //8.ชื่อผู้มีเงินได้<EmpFNameT>				
                            bkData += obj_worker.worker_fname_en + ",";

                            //9.นามสกุลผู้มีเงินได้<EmpLNameT>
                            bkData += obj_worker.worker_lname_en + ",";

                            //10.ที่อยู่ 1<Address>
                            string temp = obj_address.empaddress_no  + obj_address.empaddress_soi + " " + obj_address.empaddress_road + " " + obj_address.empaddress_tambon + " " + obj_address.empaddress_amphur + " " + obj_province.province_name_en;
                            bkData += temp + ",";

                            //11.ที่อยู่2 
                            bkData += ",";

                            //12.รหัสไปรษณีย์ <Poscod>
                            bkData += obj_address.empaddress_zipcode + ",";


                            //13.เดือนภาษี<TaxMonth>                            
                            bkData += datePay.Month.ToString().PadLeft(2, '0') + ",";

                            //14.ปีภาษี<TaxYear>                          
                            int n = Convert.ToInt32(datePay.Year);
                            if (n < 2400)
                                n += 543;
                            bkData += n.ToString() + ",";

                            //15.รหัสเงินได้<AllwonceCode>				
                            bkData += "1" + ",";

                            //16.วันที่จ่ายเงินได้ <TaxDate>+<TaxMonth>+<TaxYear>	
                            bkData += datePay.ToString("ddMM") + n.ToString() + ",";

                            //17.อัตราภาษีร้อยละ				
                            bkData += "0" + ",";
                            
                            //18.จำนวนเงินที่จ่าย<PayMent>
                            bkData += paytran.paytran_income_401.ToString("0.00") + ",";
                            
                            //19.จำนวนเงินภาษีที่หักและนำส่ง<Tax>
                            bkData += paytran.paytran_tax_401.ToString("0.00") + ",";
                            
                            //20.เงื่อนไขการหักภาษี ณ จ่าย <TaxCondition>				
                            bkData += "1";


                            tmpData += bkData + '\r' + '\n';
                        }



                        

                        douTotal += paytran.paytran_netpay_b;

                        index++;
                    }

                    int record = list_paytran.Count;

                    
                    try
                    {
                        //-- Step 1 create file
                        string filename = "TRN_TAX_" + DateTime.Now.ToString("yyMMddHHmm") + "." + "txt";
                        string filepath = Path.Combine
                       (ClassLibrary_BPC.Config.PathFileExport, filename);

                        // Check if file already exists. If yes, delete it.     
                        if (File.Exists(filepath))
                        {
                            File.Delete(filepath);
                        }

                        // Create a new file     
                        using (FileStream fs = File.Create(filepath))
                        {
                            // Add some text to file    
                            Byte[] title = new UTF8Encoding(true).GetBytes(tmpData);
                            fs.Write(title, 0, title.Length);
                        }

                        strResult = filename;

                    }
                    catch
                    {
                        strResult = "";
                    }

                }


                task.task_end = DateTime.Now;
                task.task_status = "F";
                task.task_note = strResult;
                objMTTask.updateStatus(task);

            }
            else
            {

            }

            return strResult;
        }
        //TAX 


        //SAP เริ่ม
        public string doExportSap(string com, string taskid)
        {
            string strResult = "";

            cls_ctMTTask objMTTask = new cls_ctMTTask();
            List<cls_MTTask> listMTTask = objMTTask.getDataByFillter(com, taskid, "TRN_SAP", "");
            List<string> listError = new List<string>();

            if (listMTTask.Count > 0)
            {
                cls_MTTask task = listMTTask[0];

                task.task_start = DateTime.Now;

                cls_ctMTTask objTaskDetail = new cls_ctMTTask();
                cls_TRTaskdetail task_detail = objTaskDetail.getTaskDetail(task.task_id.ToString());

                cls_ctMTTask objTaskWhose = new cls_ctMTTask();
                List<cls_TRTaskwhose> listWhose = objTaskWhose.getTaskWhose(task.task_id.ToString());

                DateTime dateEff = task_detail.taskdetail_fromdate;
                DateTime datePay = task_detail.taskdetail_paydate;

                StringBuilder objStr = new StringBuilder();
                foreach (cls_TRTaskwhose whose in listWhose)
                {
                    objStr.Append("'" + whose.worker_code + "',");
                }

                string strEmp = objStr.ToString().Substring(0, objStr.ToString().Length - 1);



                //-- Get worker
                cls_ctMTWorker objWorker = new cls_ctMTWorker();
                List<cls_MTWorker> list_worker = objWorker.getDataMultipleEmp( com, strEmp);

                //-- Step 2 Get Paytran
                cls_ctTRPaytran objPay = new cls_ctTRPaytran();
                List<cls_TRPaytran> list_paytran = objPay.getDataMultipleEmp("TH", com, datePay, datePay, strEmp);



                //-- Step 3 Get Company acc
                cls_ctTRCombank objCombank = new cls_ctTRCombank();
                List<cls_TRCombank> list_combank = objCombank.getDataByFillter("", com);
                cls_TRCombank combank = list_combank[0];

                //-- Step 4 Get Company detail
                cls_ctMTCompany objCom = new cls_ctMTCompany();
                List<cls_MTCompany> list_com = objCom.getDataByFillter("", com);
                cls_MTCompany comdetail = list_com[0];

                //-- Step 5 Get Emp address
                cls_ctTREmpaddress objEmpadd = new cls_ctTREmpaddress();
                List<cls_TREmpaddress> list_empaddress = objEmpadd.getDataMultipleEmp(com, strEmp);

                //-- Step 6 Get Emp card
                cls_ctTREmpcard objEmpcard = new cls_ctTREmpcard();
                List<cls_TREmpcard> list_empcard = objEmpcard.getDataTaxMultipleEmp(com, strEmp);

                //-- Step 7 Get Company card
                cls_ctTRComcard objComcard = new cls_ctTRComcard();
                List<cls_TRComcard> list_comcard = objComcard.getDataByFillter(com, "NTID", "", "", "");
                cls_TRComcard comcard = list_comcard[0];

                cls_ctMTProvince objProvince = new cls_ctMTProvince();
                List<cls_MTProvince> list_province = objProvince.getDataByFillter("", "");



                string tmpData = "";


                if (list_paytran.Count > 0)
                {

                    double douTotal = 0;

                    int index = 0;
                    string bkData;

                    foreach (cls_TRPaytran paytran in list_paytran)
                    {

                        string empname = "";

                        cls_MTWorker obj_worker = new cls_MTWorker();
                        cls_TREmpaddress obj_address = new cls_TREmpaddress();
                        cls_MTProvince obj_province = new cls_MTProvince();
                        cls_TREmpcard obj_card = new cls_TREmpcard();

                        foreach (cls_MTWorker worker in list_worker)
                        {
                            if (paytran.worker_code.Equals(worker.worker_code))
                            {
                                empname = worker.initial_name_en + " " + worker.worker_fname_en + " " + worker.worker_lname_en;
                                obj_worker = worker;
                                break;
                            }
                        }

                        foreach (cls_TREmpaddress address in list_empaddress)
                        {
                            if (paytran.worker_code.Equals(address.worker_code))
                            {
                                obj_address = address;
                                break;
                            }
                        }

                        foreach (cls_TREmpcard card in list_empcard)
                        {
                            if (paytran.worker_code.Equals(card.worker_code))
                            {
                                obj_card = card;
                                break;
                            }
                        }

                        foreach (cls_MTProvince province in list_province)
                        {
                            if (obj_address.province_code.Equals(province.province_code))
                            {
                                obj_province = province;
                                break;
                            }
                        }


                        if (empname.Equals("") || obj_card.empcard_code.Equals(""))
                            continue;


                        if (paytran.paytran_income_401 > 0)
                        {
                            //1.ลักษณะการยื่นแบบปกติ
                            bkData = "00|";

                            //2.เลขประจำตัวประชาชนผู้มี่หน้าที่หัก ณ ที่จ่าย<CardNo>	
                            if (comcard.comcard_code.Length == 13)
                                bkData += comcard.comcard_code + "|";
                            else
                                bkData += "0000000000000|";

                            //3.เลขประจำตัวผู้เสียภาษีอากรผู้มีหน้าที่หัก ณ ที่จ่าย<TaxNo>
                            if (comcard.comcard_code.Length == 10)
                                bkData += comcard.comcard_code + "|";
                            else
                                bkData += "0000000000|";

                            //4.เลขที่สาขา ผู้มีหน้าที่หักภาษี ณ ที่จ่าย<BranchID>
                            bkData += "00000|";

                            //5.เลขประจำตัวประชาชนผู้มีเงินได้<CardNo>	
                            if (obj_card.empcard_code.Length == 13)
                                bkData += obj_card.empcard_code + "|";
                            else
                                bkData += "0000000000000|";

                            //6.เลขประจำตัวผู้เสียภาษีอากรผู้มีเงินได้ <TaxNo>
                            if (obj_card.empcard_code.Length == 10)
                                bkData += obj_card.empcard_code + "|";
                            else
                                bkData += "0000000000|";

                            //7.คำนำหน้าชื่อผู้มีเงินได้<InitialNameT>
                            bkData += obj_worker.initial_name_th + "|";

                            //8.ชื่อผู้มีเงินได้<EmpFNameT>				
                            bkData += obj_worker.worker_fname_th + "|";

                            //9.นามสกุลผู้มีเงินได้<EmpLNameT>
                            bkData += obj_worker.worker_lname_th + "|";

                            //10.ที่อยู่ 1<Address>
                            string temp = obj_address.empaddress_no + " " + obj_address.empaddress_soi + " " + obj_address.empaddress_road + " " + obj_address.empaddress_tambon + " " + obj_address.empaddress_amphur + " " + obj_province.province_name_th;
                            bkData += temp + "|";

                            //11.ที่อยู่2 
                            bkData += "|";

                            //12.รหัสไปรษณีย์ <Poscod>
                            bkData += obj_address.empaddress_zipcode + "|";


                            //13.เดือนภาษี<TaxMonth>                            
                            bkData += datePay.Month.ToString().PadLeft(2, '0') + "|";

                            //14.ปีภาษี<TaxYear>                          
                            int n = Convert.ToInt32(datePay.Year);
                            if (n < 2400)
                                n += 543;
                            bkData += n.ToString() + "|";

                            //15.รหัสเงินได้<AllwonceCode>				
                            bkData += "1|";

                            //16.วันที่จ่ายเงินได้ <TaxDate>+<TaxMonth>+<TaxYear>	
                            bkData += datePay.ToString("ddMM") + n.ToString() + "|";

                            //17.อัตราภาษีร้อยละ				
                            bkData += "0" + "|";

                            //18.จำนวนเงินที่จ่าย<PayMent>
                            bkData += paytran.paytran_income_401.ToString("0.00") + "|";

                            //19.จำนวนเงินภาษีที่หักและนำส่ง<Tax>
                            bkData += paytran.paytran_tax_401.ToString("0.00") + "|";

                            //20.เงื่อนไขการหักภาษี ณ จ่าย <TaxCondition>				
                            bkData += "1";


                            tmpData += bkData + '\r' + '\n';
                        }





                        douTotal += paytran.paytran_netpay_b;

                        index++;
                    }

                    int record = list_paytran.Count;


                    try
                    {
                        //-- Step 1 create file
                        string filename = "TRN_SAP_" + DateTime.Now.ToString("yyMMddHHmm") + "." + "txt";
                        string filepath = Path.Combine
                       (ClassLibrary_BPC.Config.PathFileExport, filename);

                        // Check if file already exists. If yes, delete it.     
                        if (File.Exists(filepath))
                        {
                            File.Delete(filepath);
                        }

                        // Create a new file     
                        using (FileStream fs = File.Create(filepath))
                        {
                            // Add some text to file    
                            Byte[] title = new UTF8Encoding(true).GetBytes(tmpData);
                            fs.Write(title, 0, title.Length);
                        }

                        strResult = filename;

                    }
                    catch
                    {
                        strResult = "";
                    }

                }


                task.task_end = DateTime.Now;
                task.task_status = "F";
                task.task_note = strResult;
                objMTTask.updateStatus(task);

            }
            else
            {

            }

            return strResult;
        }
        //SAP 


        //PF เริ่ม    
        public string doExportPF(string com, string taskid)
        {
            string strResult = "";

            cls_ctMTTask objMTTask = new cls_ctMTTask();
            List<cls_MTTask> listMTTask = objMTTask.getDataByFillter(com, taskid, "TRN_PF", "");
            List<string> listError = new List<string>();

            if (listMTTask.Count > 0)
            {
                cls_MTTask task = listMTTask[0];

                task.task_start = DateTime.Now;

                cls_ctMTTask objTaskDetail = new cls_ctMTTask();
                cls_TRTaskdetail task_detail = objTaskDetail.getTaskDetail(task.task_id.ToString());

                cls_ctMTTask objTaskWhose = new cls_ctMTTask();
                List<cls_TRTaskwhose> listWhose = objTaskWhose.getTaskWhose(task.task_id.ToString());

                DateTime dateEff = task_detail.taskdetail_fromdate;
                DateTime datePay = task_detail.taskdetail_paydate;
                 string[] task_bank = task_detail.taskdetail_process.Split('|');
                //string[] task_bank = task_detail.taskdetail_process.Split('|');
                string[] task_details = task_detail.taskdetail_process.Split('|');
                //string[] task_bank = task_details;
                string[] task_pf_detail = task_details;


                StringBuilder objStr = new StringBuilder();
                foreach (cls_TRTaskwhose whose in listWhose)
                {
                    objStr.Append("'" + whose.worker_code + "',");
                }

                string strEmp = objStr.ToString().Substring(0, objStr.ToString().Length - 1);
                //ตัวเช็คธนาคาร

                string bankCode = new String(task_detail.taskdetail_process.Where(Char.IsDigit).ToArray());



                //-- Get worker
                cls_ctMTWorker objWorker = new cls_ctMTWorker();
                List<cls_MTWorker> list_worker = objWorker.getDataMultipleEmp(com, strEmp);

                //-- Step 2 Get Paypf
                cls_ctTRPaypf objPay = new cls_ctTRPaypf();
                List<cls_TRPaypf> list_pf = objPay.getDataMultipleEmp("TH", com, datePay, datePay, strEmp);

                
                //-- Step 3 Get Company acc
                cls_ctTRCombank objCombank = new cls_ctTRCombank();
                List<cls_TRCombank> list_combank = objCombank.getDataByFillter("",com);
                cls_TRCombank combank = list_combank[0];


                //-- Step 6 Get Emp card
                cls_ctTREmpcard objEmpcard = new cls_ctTREmpcard();
                List<cls_TREmpcard> list_empcard = objEmpcard.getDataTaxMultipleEmp(com, strEmp);

                //-- Step 7 Get Company card
                cls_ctTRComcard objComcard = new cls_ctTRComcard();
                List<cls_TRComcard> list_comcard = objComcard.getDataByFillter(com, "PVF", "", "", "");

                cls_TRComcard comcard = new cls_TRComcard();

                if(list_comcard.Count > 0)
                    comcard = list_comcard[0];

                cls_ctMTPeriod objPeriod = new cls_ctMTPeriod();
                List<cls_MTPeriod> list_period = objPeriod.getDataByFillter("", com, "PAY", datePay.Year.ToString(), "M");


                cls_ctTREmpdep objEmpdep = new cls_ctTREmpdep();
                List<cls_TREmpdep> list_empdep = objEmpdep.getDataTaxMultipleEmp("", "", datePay);




                //-- Get Emp bankacc
                cls_ctTREmpbank objEmpbank = new cls_ctTREmpbank();
                List<cls_TREmpbank> list_empbank = objEmpbank.getDataMultipleEmp(com, strEmp);
                cls_TREmpbank bank = list_empbank[0];

                //-- Get bank
                cls_ctMTBank objbank = new cls_ctMTBank();
                List<cls_MTBank> list_bank = objbank.getDataByFillter("", "");

                cls_MTPeriod period = new cls_MTPeriod();

                foreach (cls_MTPeriod tmp in list_period)
                {
                    if (tmp.period_payment.Equals(datePay))
                    {
                        period = tmp;
                        break;
                    }
                }


                string tmpData = "";

                //-- this.taskDetail.taskdetail_process = "PF" + "|" + this.CompanyCode + "|" + this.PFCode + "|" + this.PatternCode;
                //string[] task_pf_detail = task_detail.taskdetail_process.Split('|');


                if (list_pf.Count > 0)
                {

                    //double douTotal = 0;

                    int index = 0;
                    string bkData="";

                    int TotalRecord = 0;
                    double Amount;
                    double TotalAmountEmp = 0;
                    double TotalAmountComp = 0;

                    foreach (cls_TRPaypf pf in list_pf)
                    {
                        string empnameen = "";
                        string empnameth = "";
                        string Header = "";
                        string empname = "";

                        cls_MTWorker obj_worker = new cls_MTWorker();                                            
                        cls_TREmpcard obj_pfcard = new cls_TREmpcard();
                        cls_TREmpcard obj_taxcard = new cls_TREmpcard();
                        cls_TREmpdep obj_empdep = new cls_TREmpdep();

                        foreach (cls_MTWorker worker in list_worker)
                        {
                            if (pf.worker_code.Equals(worker.worker_code))
                            {
                                empnameen = worker.initial_name_en + " " + worker.worker_fname_en + " " + worker.worker_lname_en;
                                obj_worker = worker;
                                break;
                            }
                        }

                        foreach (cls_MTWorker worker in list_worker)
                        {
                            if (pf.worker_code.Equals(worker.worker_code))
                            {
                                empnameth = worker.initial_name_th + " " + worker.worker_fname_th + " " + worker.worker_lname_th;
                                obj_worker = worker;
                                break;
                            }
                        }


                        foreach (cls_TREmpcard card in list_empcard)
                        {
                            if (pf.worker_code.Equals(card.worker_code) && card.card_type.Equals("PVF"))
                            {
                                obj_pfcard = card;
                                break;
                            }
                        }

                        foreach (cls_TREmpcard card in list_empcard)
                        {
                            if (pf.worker_code.Equals(card.worker_code) && card.card_type.Equals("NTID"))
                            {
                                obj_taxcard = card;
                                break;
                            }
                        }

                        foreach (cls_TREmpdep dep in list_empdep)
                        {
                            if (pf.worker_code.Equals(dep.worker_code))
                            {
                                obj_empdep = dep;
                                break;
                            }
                        }

                        //
                         double TotalAmount = TotalAmountEmp + TotalAmountComp;

                        //
                         if (task_bank.Length > 0 || task_pf_detail.Length > 0)
                         {

                             switch (bankCode)
                            //switch (task_detail.taskdetail_process )
                            {
                                case "014" :
                                case "001"://scb
                                    {
                                        // Header 

                                       
                                        // 1. รหัสกองทัน Fund Code
                                        Header += task_pf_detail[2] + "|";

                                        // 2. รหัสบริษัท Company Code
                                        Header += task_pf_detail[1] + "|";


                                        // 3. MEM_FUND (Employee Code)
                                        bkData += obj_worker.worker_code + "|";

                                        // 4. NAME_THAI (Name th) 
                                        bkData += empnameth + "|";

                                        // 5.  NAME_ENG (Name en) 
                                        bkData += empnameen + "|";

                                        // 6. M_AMT(เงินสะสม (EMP CONT.))  
                                        Amount = pf.paypf_emp_amount;
                                        bkData += Amount.ToString("0.00").Trim() + "|";
                                        TotalAmountEmp += Amount;

                                        // 7. เงินสมทบ (COMP CONT.)  
                                        Amount = pf.paypf_com_amount;
                                        bkData += Amount.ToString("0.00").Trim() + "|";
                                        TotalAmountComp += Amount;

                                        tmpData += Header;
                                        tmpData += bkData;
                                    }
                                    break;

                                case "004": // กสิกรไทย (10 หลัก)

                                    {
                                        // Header 
                                        // 1. A
                                        Header += "A" + "|";

                                        // 2. D
                                        Header += "D" + "|";

                                        // 3. รหัสกองทัน Fund Code
                                        Header += task_pf_detail[1] + "|";

                                        // 4. วันที่
                                        string datePart1 = datePay.ToString("ddMMyy", DateTimeFormatInfo.CurrentInfo);
                                        Header += datePart1 + "|";
                                        // 5. วันที่
                                        string datePart2 = datePay.ToString("ddMMyy", DateTimeFormatInfo.CurrentInfo);
                                        Header += datePart2 + "|";

                                        // 6. รหัสบริษัท Company Code
                                        Header += task_pf_detail[2] + "|";


                                        ////
                                        string initial = "";
                                        string fname = "";
                                        string lname = "";

                                        foreach (cls_MTWorker worker in list_worker)
                                        {
                                            if (pf.worker_code.Equals(worker.worker_code))
                                            {
                                                initial = worker.initial_name_th;
                                                fname = worker.worker_fname_th;
                                                lname = worker.worker_lname_th;

                                                
                                                
                                                
                                                obj_worker = worker;
                                                break;
                                            }
                                        }


                                        ///
                                        // 7.  
                                        bkData += "B" + "|";

                                        // 8. 
                                        bkData += "A" + "|";

                                        // 9. MEM_FUND (Employee Code)
                                        bkData += obj_worker.worker_code + "|";


                                        // 10. MEM_FUND (Employee Code)
                                        bkData += initial + "|";

                                        // 11. NAME_THAI (Name th) 
                                        bkData += fname + "|";
                                        

                                        // 12.  NAME_ENG (Name en) 
                                        bkData += lname + "|";

                                        //13. M_AMT(เงินสะสม (EMP CONT.))  
                                        Amount = pf.paypf_emp_amount;
                                        bkData += Amount.ToString("0.00").Trim() + "|";
                                        TotalAmountEmp += Amount;

                                        // 14. เงินสมทบ (COMP CONT.)  
                                        Amount = pf.paypf_com_amount;
                                        bkData += Amount.ToString("0.00").Trim() + "|";
                                        TotalAmountComp += Amount;

                                        // 15. เงินสมทบ (COMP CONT.)  
                                        bkData += "" + "|";

                                        // 16. เงินสมทบ (COMP CONT.)  
                                        bkData += "" + "|";

                                        // 17. เงินสมทบ (COMP CONT.)  
                                        bkData += "" + "|";

                                        // 18. รหัสบัตรประชาชนcomcard.comcard_code
                                        if (comcard.comcard_code.Length == 13)
                                            bkData += comcard.comcard_code + "|";
                                        else
                                            bkData += "0000000000000" + "|";


                                      

                                        tmpData += Header + '\r' + '\n'+ bkData;
                                    }
                                    break;


                                default:
                                    break;
                            }
                        }
                        try
                        {
                            //-- Step 1 create file
 
                            string filename = "TRN_PF" + DateTime.Now.ToString("yyMMddHHmm") + "." + "xls";
                            string filepath = Path.Combine
                           (ClassLibrary_BPC.Config.PathFileExport, filename);

                            // Check if file already exists. If yes, delete it.     
                            if (File.Exists(filepath))
                            {
                                File.Delete(filepath);
                            }
                            DataSet ds = new DataSet();
                            string str = tmpData.Replace("\r\n", "]");
                            string[] data = str.Split(']');
                            DataTable dataTable = ds.Tables.Add();

                            bool bank_code014 = (bankCode == "014" || bankCode == "001"); // scb

                            //bool bank_code  = (bank.bank_code == ""); // SCB-MasterFund
                            bool bank_code004 = (bankCode == "004"); // กสิกรไทย


                            //int columnsCount = Math.Min(dataTable.Columns.Count, 6);

                            if (bank_code014)
                            {
                                dataTable.Columns.AddRange(new DataColumn[7] { new DataColumn("FUND_CODE"), new DataColumn("COMP_CODE"), new DataColumn("MEM_FUND"), new DataColumn("NAME_THAI"), new DataColumn("NAME_ENG"), new DataColumn("M_AMT"), new DataColumn("F_AMT") });
                            }
                            else if (bank_code004)
                            {
                                // เพิ่ม DataColumn 6 คอลัมน์แรก
                                dataTable.Columns.AddRange(new DataColumn[18] { new DataColumn("A"), new DataColumn("B"), new DataColumn("C"), new DataColumn("D"), new DataColumn("E"), new DataColumn("F") ,new DataColumn("A1"), new DataColumn("B1"), new DataColumn("C1"), new DataColumn("D1"), new DataColumn("E1"), new DataColumn("F1"), new DataColumn("G1"), new DataColumn("H1"), new DataColumn("I1"), new DataColumn("J1"), new DataColumn("K1"),  new DataColumn("L1") });
                            }
                            //else if (bank_code002)
                            //{
                            //    dataTable.Columns.AddRange(new DataColumn[37] { new DataColumn("No."), new DataColumn("Company Code"), new DataColumn("Company Name"), new DataColumn("Department Code"), new DataColumn("Employee Code"), new DataColumn("Title Name"), new DataColumn("First Name"), new DataColumn("Last Name"), new DataColumn("เงินสะสม (Emp Cont.)"), new DataColumn("เงินสมทบ (Com Cont.)"), new DataColumn("ID. No"), new DataColumn("MENU"), new DataColumn("PVDMPFMM"), new DataColumn("PVDMPFFI"), new DataColumn("PVDMPFEQ"), new DataColumn("PVDMGLDH"), new DataColumn("SF-5"), new DataColumn("SF-6"), new DataColumn("SF-7"), new DataColumn("SF-8"), new DataColumn("SF-9"), new DataColumn("SF-10"), new DataColumn("SF-11"), new DataColumn("SF-12"), new DataColumn("SF-13"), new DataColumn("SF-14"), new DataColumn("SF-15"), new DataColumn("SF-16"), new DataColumn("SF-17"), new DataColumn("SF-18"), new DataColumn("SF-19"), new DataColumn("SF-20"), new DataColumn("Total %"), new DataColumn("เบอร์โทรศัพท์"), new DataColumn("เบอร์โทรศัพท์มือถือ"), new DataColumn("อีเมล"), new DataColumn("ต้องการให้ระบบ  Gen Password ผ่านทางอีเมล") });
                            // }
 
                            foreach (var i in data)
                            {
                                if (!string.IsNullOrEmpty(i))
                                {
                                    string[] array = i.Split('|');

                                    if (bank_code014)
                                    {
                                        dataTable.Rows.Add(array[0], array[1], array[2], array[3], array[4], array[5], array[6]);
                                    }
                                    else if (bank_code004)
                                    {
                                        
                                        dataTable.Rows.Add(array[0], array[1], array[2], array[3], array[4], array[5] , array[6], array[7], array[8], array[9], array[10], array[11], array[12] ,  array[13], array[14], array[15], array[16], array[17] );

                                    }
                                    //else if (bank_code)
                                    //{
                                    //    dataTable.Rows.Add(array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7], array[8], array[9], array[10], array[11], array[12], array[13], array[14], array[15], array[16], array[17], array[18], array[19], array[20], array[21], array[22], array[23], array[24], array[25], array[26], array[27], array[28], array[29], array[30], array[31], array[32], array[33], array[34], array[35], array[36], array[37]);

                                    //}
                                }
                            }




                            ExcelLibrary.DataSetHelper.CreateWorkbook(filepath, ds);

                            strResult = filename;

                        }
                        catch (Exception ex)
                        {
                            strResult = ex.ToString();
                        }
                    }

                
                    task.task_end = DateTime.Now;
                    task.task_status = "F";
                    task.task_note = strResult;
                    objMTTask.updateStatus(task);

                }}
                else
                {

                }

                return strResult;
            }
                        //

        //                if (empname.Equals("") || obj_taxcard.empcard_code.Equals("") || obj_pfcard.empcard_code.Equals(""))
        //                    continue;

        //                bkData = bkData + '\r' + '\n';

        //                // 1, 2. Record Type, Member ID-Type
        //                bkData += "D,EM,";

        //                // 3. Member ID
        //                bkData += obj_pfcard.empcard_code + ",";

        //                // 4. Title Name
        //                bkData += obj_worker.initial_name_th + ",";

        //                // 5. First Name
        //                bkData += obj_worker.worker_fname_th + ",";

        //                // 6. Last Name
        //                bkData += obj_worker.worker_lname_th + ",";

        //                // 7. Position Code
        //                bkData += ",";

        //                // 8. Location Code
        //                bkData += "0" + ",";

        //                // 9. Department Code
        //                bkData += obj_empdep.empdep_level01 + ",";

        //                // 10. Division Code
        //                bkData += obj_empdep.empdep_level02 + ",";

        //                // 11. Section Code
        //                bkData += obj_empdep.empdep_level03 + ",";

        //                // 12. Citizen ID
        //                bkData += obj_taxcard.empcard_code + ",";

        //                // 13. Tax ID
        //                bkData += obj_taxcard.empcard_code + ",";

        //                // 14. รหัสรูปแบบลงทุน
        //                //-- F edit 16/03/2014
        //                bkData += "01,";
        //                //Detail += txtPatternCode.Text + ",";

        //                // 15. รหัสกองทุนย่อย 1
        //                //-- F edit 16/03/2014
        //                bkData += "SINSA1,";
        //                //Detail += txtPFCodeSub.Text + ",";

        //                // 16. %เงินลงทุน 1
        //                bkData += "100.00,";

        //                // 17. เงินสะสม 1 (บาท)
        //                Amount = pf.paypf_emp_amount;
        //                bkData += Amount.ToString("0.00").Trim() + ",";
        //                TotalAmountEmp += Amount;

        //                // 17. เงินสมทบ 1 (บาท)
        //                Amount = pf.paypf_com_amount;
        //                bkData += Amount.ToString("0.00").Trim();
        //                TotalAmountComp += Amount;

        //                TotalRecord++;
        //                index++;
        //            }

                    

        //            double TotalAmount = TotalAmountEmp + TotalAmountComp;

        //            // Header 

        //            // 1. Record Type, 2. Batch Reference, 3. Verify Status, 4. Transaction Code
        //            string Header = "H3,,,CB,";

        //            // 5. Fund Code
        //            //Header += txtPFCode.Text.TrimEnd() + ",";
        //            Header += task_pf_detail[2] + ",";

        //            // 6. Company Code
        //            //Header += txtCompCode.Text.TrimEnd() + ",";
        //            Header += task_pf_detail[1] + ",";

        //            // 7. Total Records
        //            Header += TotalRecord.ToString() + ",";

        //            // 8. Generate Date
        //            Header += DateTime.Now.ToString("dd/MM/yyyy") + ",";

        //            // 9. Total Amount
        //            Header += TotalAmount.ToString("0.00") + ",";

        //            // 10. Fund Manager Code
        //            Header += "BBLAM,";

        //            // 11. Contribution Group
        //            //Header += txtCBGroup.Text.TrimEnd() + ",";
        //            Header += "" + ",";

        //            // 12. Payment Month
        //            //if (PeriodMonthly != string.Empty)
        //            //    Header += PeriodMonthly + "/" + Year.Year.ToString() + ",";
        //            //else
        //            //    Header += PeriodDaily + "/" + Year.Year.ToString() + ",";
        //            Header += period.period_no + "/" + datePay.Year.ToString() + ",";


        //            // 13. Payment Term
        //            Header += "1,";

        //            // 14. Payment Date, 15-18
        //            Header += datePay.ToString("dd/MM/yyyy") + ",,,,,";

        //            // 19. Transaction Flag
        //            Header += "New";

        //            //int record = list_paytran.Count;

        //            tmpData += Header + bkData;

        //            try
        //            {
        //                //-- Step 1 create file
        //                string filename = "TRN_PF_" + DateTime.Now.ToString("yyMMddHHmm") + "." + "txt";
        //                string filepath = Path.Combine
        //               (ClassLibrary_BPC.Config.PathFileExport, filename);

        //                // Check if file already exists. If yes, delete it.     
        //                if (File.Exists(filepath))
        //                {
        //                    File.Delete(filepath);
        //                }

        //                // Create a new file     
        //                using (FileStream fs = File.Create(filepath))
        //                {
        //                    // Add some text to file    
        //                    Byte[] title = new UTF8Encoding(true).GetBytes(tmpData);
        //                    fs.Write(title, 0, title.Length);
        //                }

        //                strResult = filename;

        //            }
        //            catch
        //            {
        //                strResult = "";
        //            }

        //        }

        //        task.task_end = DateTime.Now;
        //        task.task_status = "F";
        //        task.task_note = strResult;
        //        objMTTask.updateStatus(task);

        //    }
        //    else
        //    {

        //    }

        //    return strResult;
        //}
        //TRN_SSF เริ่ม


       
        public string doExportSSF(string com, string taskid)
        {
            string strResult = "";

            cls_ctMTTask objMTTask = new cls_ctMTTask();
            List<cls_MTTask> listMTTask = objMTTask.getDataByFillter(com, taskid, "TRN_SSF", "");
            List<string> listError = new List<string>();

            if (listMTTask.Count > 0)
            {
                cls_MTTask task = listMTTask[0];

                task.task_start = DateTime.Now;

                cls_ctMTTask objTaskDetail = new cls_ctMTTask();
                cls_TRTaskdetail task_detail = objTaskDetail.getTaskDetail(task.task_id.ToString());

                cls_ctMTTask objTaskWhose = new cls_ctMTTask();
                List<cls_TRTaskwhose> listWhose = objTaskWhose.getTaskWhose(task.task_id.ToString());

                DateTime dateEff = task_detail.taskdetail_fromdate;
                DateTime datePay = task_detail.taskdetail_paydate;

                StringBuilder objStr = new StringBuilder();
                foreach (cls_TRTaskwhose whose in listWhose)
                {
                    objStr.Append("'" + whose.worker_code + "',");
                }

                string strEmp = objStr.ToString().Substring(0, objStr.ToString().Length - 1);



                //-- Get worker
                cls_ctMTWorker objWorker = new cls_ctMTWorker();
                List<cls_MTWorker> list_worker = objWorker.getDataMultipleEmp(com, strEmp);

                //-- Step 2 Get Paytran
                cls_ctTRPaytran objPay = new cls_ctTRPaytran();
                List<cls_TRPaytran> list_paytran = objPay.getDataMultipleEmp("TH", com, datePay, datePay, strEmp);



                //-- Step 3 Get Company acc
                cls_ctTRCombank objCombank = new cls_ctTRCombank();
                List<cls_TRCombank> list_combank = objCombank.getDataByFillter("", com);
                cls_TRCombank combank = list_combank[0];

                //-- Step 4 Get Company detail
                cls_ctMTCompany objCom = new cls_ctMTCompany();
                List<cls_MTCompany> list_com = objCom.getDataByFillter("", com);
                cls_MTCompany comdetail = list_com[0];

                //-- Step 5 Get Emp address
                cls_ctTREmpaddress objEmpadd = new cls_ctTREmpaddress();
                List<cls_TREmpaddress> list_empaddress = objEmpadd.getDataMultipleEmp(com, strEmp);

                //-- Step 6 Get Emp card
                cls_ctTREmpcard objEmpcard = new cls_ctTREmpcard();
                List<cls_TREmpcard> list_empcard = objEmpcard.getDataTaxMultipleEmp(com, strEmp);

                //-- Step 7 Get Company card
                cls_ctTRComcard objComcard = new cls_ctTRComcard();
                List<cls_TRComcard> list_comcard = objComcard.getDataByFillter(com, "NTID", "", "", "");
                cls_TRComcard comcard = list_comcard[0];

                //-- Step 8 Get Payitem

                cls_ctTRPayitem objPayitem = new cls_ctTRPayitem();
                List<cls_TRPayitem> list_payitem = objPayitem.getDataitemMultipleEmp("TH", com, strEmp, datePay, "SLF1", "");
                cls_TRPayitem payitem = list_payitem[0];
                if (list_payitem.Count > 0)
                {
                    cls_TRPayitem Payitem = list_payitem[0];
                }

                //-- Step 9  Get Payitemm

                cls_ctTRPayitem objPayitemm = new cls_ctTRPayitem();
                List<cls_TRPayitem> list_payitemm = objPayitemm.getDataitemMultipleEmp("TH", com, strEmp, datePay, "SLF2", "");
                cls_TRPayitem payitemm = list_payitemm[0];
                if (list_payitemm.Count > 0)
                {
                    cls_TRPayitem Payitemm = list_payitemm[0];
                }
             

           
                
                



               

                if (list_comcard.Count > 0)
                    comcard = list_comcard[0];

                cls_ctMTPeriod objPeriod = new cls_ctMTPeriod();
                List<cls_MTPeriod> list_period = objPeriod.getDataByFillter("", com, "PAY", datePay.Year.ToString(), "M");


                cls_ctTREmpdep objEmpdep = new cls_ctTREmpdep();
                List<cls_TREmpdep> list_empdep = objEmpdep.getDataTaxMultipleEmp("", "", datePay);




                cls_ctMTProvince objProvince = new cls_ctMTProvince();
                List<cls_MTProvince> list_province = objProvince.getDataByFillter("", "");

                cls_MTPeriod period = new cls_MTPeriod();
                //cls_TRPayitem payitem = new cls_TRPayitem();

                foreach (cls_MTPeriod tmp in list_period)
                {
                    if (tmp.period_payment.Equals(datePay))
                    {
                        period = tmp;
                        break;
                    }
                }


                string tmpData = "";

                if (list_paytran.Count > 0)
                {

                    double douTotal = 0;

                    int index = 1;
                    string bkData;

                    foreach (cls_TRPaytran paytran in list_paytran)
                    {

                        string empname = "";

                        cls_MTWorker obj_worker = new cls_MTWorker();
                        cls_TREmpaddress obj_address = new cls_TREmpaddress();
                        cls_MTProvince obj_province = new cls_MTProvince();
                        cls_TREmpcard obj_card = new cls_TREmpcard();
                        cls_TRPayitem obj_Payitem = new cls_TRPayitem();

                        foreach (cls_MTWorker worker in list_worker)
                        {
                            if (paytran.worker_code.Equals(worker.worker_code))
                            {
                                empname = worker.initial_name_en + " " + worker.worker_fname_en + " " + worker.worker_lname_en;
                                obj_worker = worker;
                                break;
                            }
                        }

                        foreach (cls_TREmpaddress address in list_empaddress)
                        {
                            if (paytran.worker_code.Equals(address.worker_code))
                            {
                                obj_address = address;
                                break;
                            }
                        }

                        foreach (cls_TREmpcard card in list_empcard)
                        {
                            if (paytran.worker_code.Equals(card.worker_code))
                            {
                                obj_card = card;
                                break;
                            }
                        }

                        foreach (cls_MTProvince province in list_province)
                        {
                            if (obj_worker.worker_code.Equals(province.province_code))
                            {
                                obj_province = province;
                                break;
                            }
                        }


                        //foreach (cls_TRPayitem payitem in list_payitem)
                        //{
                        //    if (paytran.worker_code.Equals(payitem.item_code))
                        //    {
                        //        obj_Payitem = payitem;
                        //        break;
                        //    }
                        //}



                        if (empname.Equals("") || obj_card.empcard_code.Equals(""))
                            continue;


                        if (paytran.paytran_income_401 > 0)
                        {
                            //1.ลำดับที่
                            bkData = index++ + "|";

                            //2.เดือน / ปี
                            bkData += period.period_name_th + "/" + datePay.Year.ToString() + "|";

                            

                            //3.เลขประจำตัวประชาชน
                            if (comcard.comcard_code.Length == 13)
                                bkData += comcard.comcard_code + "|";
                            else
                                bkData += "0000000000000|";

                            //4.ชื่อ-นามสกุล
                            bkData += obj_worker.initial_name_th + " " + obj_worker.worker_fname_th + " " + obj_worker.worker_lname_th + "|";

                            //5.กยศ	

                            if (payitem.item_code.Length == 5)
                                bkData += payitem.payitem_amount.ToString("0000") + "|";
                            else
                                bkData += payitem.payitem_amount.ToString("0,00").Trim() + "|";


                            //6.กรอ
                            if (payitemm.item_code.Length == 5)
                                bkData += payitemm.payitem_amount.ToString("0000") + "|";
                            else
                                bkData += payitemm.payitem_amount.ToString("0,00").Trim() + "|";



                            //7.จำนวนเงิน

                            bkData += payitem.payitem_amount.ToString(".").Trim() + payitemm.payitem_amount.ToString(".").Trim() + "|"; ;
                            //bkData += payitemm.payitem_amount.ToString("0,00").Trim() + "|";
                          

                            

                            //8.ยอดยืนยันนำส่ง			
                            bkData += payitem.payitem_amount.ToString(".").Trim() + payitemm.payitem_amount.ToString(".").Trim() + "|"; ;
                       
                          


                            //9.วันที่หักเงินเดือน
                            bkData += datePay.ToString("dd/MM/yyyy") + "|";

                            //10.ไม่ได้นำส่งเงิน
                             bkData += "|";

                            //11.รหัสสาเหตุ
                            bkData += "|";

                            //12.ไฟล์แนบ
                            bkData += "|";


                            tmpData += bkData + '\r' + '\n';
                        }





                        douTotal += paytran.paytran_netpay_b;

                        index++;
                    }

                    int record = list_paytran.Count;


                    try
                    {
                        //-- Step 1 create file
                        string filename = "TRN_SSF_" + DateTime.Now.ToString("yyMMddHHmm") + "." + "xls";
                        string filepath = Path.Combine
                       (ClassLibrary_BPC.Config.PathFileExport, filename);



                        // Check if file already exists. If yes, delete it.     
                        if (File.Exists(filepath))
                        {
                            File.Delete(filepath);
                        }
                        DataSet ds = new DataSet();
                        string str = tmpData.Replace("\r\n", "]");
                        string[] data = str.Split(']');
                        DataTable dataTable = ds.Tables.Add();
                        dataTable.Columns.AddRange(new DataColumn[12] { new DataColumn("ลำดับที่"), new DataColumn("เดือน / ปี"), new DataColumn("เลขประจำตัวประชาชน"), new DataColumn("ชื่อ-นามสกุล"), new DataColumn("กยศ."), new DataColumn("กรอ."), new DataColumn("จำนวนเงิน"), new DataColumn("ยอดยืนยันนำส่ง"), new DataColumn("วันที่หักเงินเดือน"), new DataColumn("ไม่ได้นำส่งเงิน"), new DataColumn("รหัสสาเหตุ"), new DataColumn("ไฟล์แนบ") });
                        foreach (var i in data)
                        {
                            if (i.Equals(""))
                                continue;
                            string[] array = i.Split('|');
                            dataTable.Rows.Add(array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7], array[8], array[9], array[10], array[11]);
                        }
                        ExcelLibrary.DataSetHelper.CreateWorkbook(filepath, ds);

                        // Create a new file     
                        //using (FileStream fs = File.Create(filepath))
                        //{
                        //    // Add some text to file    
                        //    Byte[] Table = new UTF8Encoding(true).GetBytes(tmpData);
                        //    fs.Write(Table, 0, Table.Length);


                        //}

                        strResult = filename;

                    }
                catch (Exception ex)
                {
                    strResult = ex.ToString();
                }

                }


                task.task_end = DateTime.Now;
                task.task_status = "F";
                task.task_note = strResult;
                objMTTask.updateStatus(task);

            }
            else
            {

            }

            return strResult;
        }
        //TRN_SSF

        //Mizuho
        public string doExportmizuho(string com, string taskid)
        { 
            string strResult = "";
            cls_ctMTTask objMTTask = new cls_ctMTTask();
            List<cls_MTTask> listMTTask = objMTTask.getDataByFillter(com, taskid, "TRN_MIZUHO", "");
            List<string> listError = new List<string>();
            if (listMTTask.Count > 0)
            {
                cls_MTTask task = listMTTask[0];

                task.task_start = DateTime.Now;

                cls_ctMTTask objTaskDetail = new cls_ctMTTask();
                cls_TRTaskdetail task_detail = objTaskDetail.getTaskDetail(task.task_id.ToString());

                cls_ctMTTask objTaskWhose = new cls_ctMTTask();
                List<cls_TRTaskwhose> listWhose = objTaskWhose.getTaskWhose(task.task_id.ToString());

                DateTime dateEff = task_detail.taskdetail_fromdate;
                DateTime datePay = task_detail.taskdetail_paydate;
                DateTime dateto = task_detail.taskdetail_todate;

                StringBuilder objStr = new StringBuilder();
                foreach (cls_TRTaskwhose whose in listWhose)
                {
                    objStr.Append("'" + whose.worker_code + "',");
                }

                string strEmp = objStr.ToString().Substring(0, objStr.ToString().Length - 1);

                //-- Get worker
                cls_ctMTWorker objWorker = new cls_ctMTWorker();
                List<cls_MTWorker> list_worker = objWorker.getDataMultipleEmp(com, strEmp);

                //-- Get worker
                cls_ctMTInitial objInitial = new cls_ctMTInitial();
                List<cls_MTInitial> list_initial = objInitial.getDataByFillter("","");

                //-- Get Emp bankacc
                cls_ctTREmpbank objEmpbank = new cls_ctTREmpbank();
                List<cls_TREmpbank> list_empbank = objEmpbank.getDataMultipleEmp(com, strEmp);

                //-- Get bank
                cls_ctMTBank objbank = new cls_ctMTBank();
                List<cls_MTBank> list_bank = objbank.getDataByFillter("", "");

                //-- Get Paytran
                cls_ctTRPaytran objPay = new cls_ctTRPaytran();
                List<cls_TRPaytran> list_paytran = objPay.getDataMultipleEmp("TH", com, datePay, datePay, strEmp);
                cls_TRPaytran paybank = list_paytran[0];

                //-- Step 6 Get pay bank
                cls_ctTRPaybank objPaybank = new cls_ctTRPaybank();
                List<cls_TRPaybank> list_paybank = objPaybank.getDataByFillterDate(com, strEmp, datePay);

                string tmpData = "";
                if (list_worker.Count > 0)
                {
                    string bkData;

                    foreach (cls_MTWorker MTWorkers in list_worker)
                    {
                        string empname = "";

                        cls_MTWorker obj_worker = new cls_MTWorker();
                        cls_MTInitial obj_initial = new cls_MTInitial();
                        cls_TREmpbank obj_empbank = new cls_TREmpbank();
                        cls_TRPaytran obj_paytran = new cls_TRPaytran();
                        cls_TRPaybank obj_paybank = new cls_TRPaybank();


                        foreach (cls_MTWorker worker in list_worker)
                        {
                            if (MTWorkers.worker_code.Equals(worker.worker_code))
                            {
                                empname = worker.initial_name_en + " " + worker.worker_fname_en + " " + worker.worker_lname_en;
                                obj_worker = worker;
                                break;
                            }
                        }

                        foreach (cls_TREmpbank bank in list_empbank)
                        {
                            if (MTWorkers.worker_code.Equals(bank.worker_code))
                            {
                                obj_empbank = bank;
                                break;
                            }
                        }

                        foreach (cls_TRPaybank pbank in list_paybank)
                        {
                            if (MTWorkers.worker_code.Equals(pbank.worker_code))
                            {
                                obj_paybank = pbank;
                                break;
                            }
                        }

                        if (empname.Equals(""))
                            continue;

                        if (list_worker.Count > 0)
                        {
                            //1 รหัสพนักงาน
                            bkData = obj_worker.worker_code + "|";

                            //2 คำนำหน้าชื่อ(อังกฤษ)
                            bkData += obj_worker.initial_name_en + "|";

                            //3 ชื่อ(อังกฤษ)
                            bkData += obj_worker.worker_fname_en + "|";

                            //4 นามสกุล(อังกฤษ)
                            bkData += obj_worker.worker_lname_en + "|";

                            //5 ธนาคาร
                            cls_MTBank obj_Bank1 = null;
                            bool foundbank = false;

                            foreach (cls_TREmpbank empbank in list_empbank)
                            {
                                if (MTWorkers.worker_code.Equals(empbank.worker_code))
                                {
                                    foreach (cls_MTBank bank in list_bank)
                                    {
                                        if (empbank.empbank_bankcode != null && empbank.empbank_bankcode.Equals(bank.bank_code))
                                        {
                                            obj_Bank1 = bank;
                                            bkData += obj_Bank1.bank_name_en + " |";
                                            //6 เลขที่บัญชี
                                            bkData += obj_empbank.empbank_bankaccount + "|";
                                            foundbank = true;
                                            break;
                                        }
                                    }
                                }
                            }
                            if (!foundbank)
                            {
                                bkData += " |";

                                bkData += " |";
                            }

                            

                            //7 วันที่สิ้นสุด
                            bkData += dateto.ToString("dd/MM/yyyy") + "|";

                            //8 วันที่จ่าย
                            bkData += datePay.ToString("dd/MM/yyyy") + "|";

                            //9 โอนธนาคาร
                            if (!obj_paybank.paybank_bankamount.Equals(""))
                            {
                                bkData += obj_paybank.paybank_bankamount + "|";
                            }
                            else
                            {
                                bkData += " |";
                            }
                            

                            tmpData += bkData + '\r' + '\n';

                        }
                    }

                    int record = list_worker.Count;

                    try
                    {
                        //-- Step 1 create file
                        string filename = "TRN_MIZUHO" + DateTime.Now.ToString("yyMMddHHmm") + "." + "xls";
                        string filepath = Path.Combine
                       (ClassLibrary_BPC.Config.PathFileExport, filename);

                        // Check if file already exists. If yes, delete it.     
                        if (File.Exists(filepath))
                        {
                            File.Delete(filepath);
                        }
                        DataSet ds = new DataSet();
                        string str = tmpData.Replace("\r\n", "]");
                        string[] data = str.Split(']');

                         DataTable dataTable = ds.Tables.Add();
                         dataTable.Columns.AddRange(new DataColumn[10] { new DataColumn("Emp ID"), new DataColumn("Name Prefix"), new DataColumn("First Name"), new DataColumn("Last Name"), new DataColumn("Bank"), new DataColumn("Bank Account Number"),
                                                                        new DataColumn("Period Close Date"), new DataColumn("Pay Date"),new DataColumn("Sum Pay"),new DataColumn(" ")});

                         // ใช้ลูปเพื่อเพิ่มข้อมูลจาก array เข้า DataTable
                         string[] rows = str.Split(']');
                         foreach (var row in rows)
                         {
                             if (string.IsNullOrEmpty(row))
                                 continue;
                             string[] rowData = row.Split('|');
                             dataTable.Rows.Add(rowData);
                         }
                         ExcelLibrary.DataSetHelper.CreateWorkbook(filepath, ds);
                         strResult = filename;
                    }
                    catch (Exception ex)
                    {
                        strResult = ex.ToString();
                    }
                }
                task.task_end = DateTime.Now;
                task.task_status = "F";
                task.task_note = strResult;
                objMTTask.updateStatus(task);
            }
            else
            {

            }

            return strResult;
        }

        //CalKT20
        public string doCalculateKT20(string com, string taskid)
        {
            string strResult = "";

            cls_ctConnection obj_conn = new cls_ctConnection();

            try
            {

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_conn.doConnect();

                obj_str.Append(" EXEC [dbo].[HRM_PRO_CALKT20] '" + com + "', '" + taskid + "' ");

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.CommandType = CommandType.Text;

                int intCountSuccess = obj_cmd.ExecuteNonQuery();

                if (intCountSuccess > 0)
                {
                    strResult = "Success::" + intCountSuccess.ToString();
                }


            }
            catch (Exception ex)
            {

            }

            return strResult;
        }

        //Export Payroll
        public string doExportPR1(string com, string taskid)
        {
            string error = "";
            string strResult = "";
            cls_ctMTTask objMTTask = new cls_ctMTTask();
            List<cls_MTTask> listMTTask = objMTTask.getDataByFillter(com, taskid, "TRN_INDE", "");
            List<string> listError = new List<string>();
            if (listMTTask.Count > 0)
            {
                try
                {
                    cls_MTTask task = listMTTask[0];

                    task.task_start = DateTime.Now;

                    cls_ctMTTask objTaskDetail = new cls_ctMTTask();
                    cls_TRTaskdetail task_detail = objTaskDetail.getTaskDetail(task.task_id.ToString());

                    cls_ctMTTask objTaskWhose = new cls_ctMTTask();
                    List<cls_TRTaskwhose> listWhose = objTaskWhose.getTaskWhose(task.task_id.ToString());

                    DateTime datePay = task_detail.taskdetail_paydate;
                    DateTime dateFrom = task_detail.taskdetail_fromdate;
                    DateTime dateTo = task_detail.taskdetail_todate;

                    StringBuilder objStr = new StringBuilder();
                    foreach (cls_TRTaskwhose whose in listWhose)
                    {
                        objStr.Append("'" + whose.worker_code + "',");
                    }

                    string strEmp = objStr.ToString().Substring(0, objStr.ToString().Length - 1);

                    //-- Get worker
                    cls_ctMTWorker objWorker = new cls_ctMTWorker();
                    List<cls_MTWorker> list_worker = objWorker.getDataMultipleEmp(com, strEmp);

                    //-- Get dep
                    cls_ctTREmpdep objDep = new cls_ctTREmpdep();
                    List<cls_TREmpdep> list_TRdep = objDep.getDataTaxMultipleEmp(com, strEmp, dateTo);

                    //-- Get worker position
                    cls_ctTREmpposition objPos = new cls_ctTREmpposition();
                    List<cls_TREmpposition> list_TRpos = objPos.getDataMultipleEmp(com, strEmp, dateTo);

                    //-- Get MTPosition
                    cls_ctMTPosition objMTPosition = new cls_ctMTPosition();
                    List<cls_MTPosition> list_MTPosition = objMTPosition.getDataByFillter(com, "", "");

                    //-- Get MTDep 
                    cls_ctMTDep objTDep = new cls_ctMTDep();
                    List<cls_MTDep> list_TDep = objTDep.getDataByFillter(com, "", "", "", "");

                    //-- Get Paytran
                    cls_ctTRPaytran objPay = new cls_ctTRPaytran();
                    List<cls_TRPaytran> list_paytran = objPay.getDataMultipleEmp("TH", com, datePay, datePay, strEmp);

                    //-- Get Payitem
                    cls_ctTRPayitem objTPItem = new cls_ctTRPayitem();
                    List<cls_TRPayitem> list_payitem = objTPItem.getDataitemMultipleEmp("TH", com, strEmp, datePay, "", "");

                    //-- Get PayOT
                    cls_ctTRPayOT objot = new cls_ctTRPayOT();
                    List<cls_TRPayOT> list_payot = objot.getDataitemMultipleEmp("TH", com, strEmp, datePay);

                    string tmpData = "";
                    if (list_worker.Count > 0)
                    {
                        string bkData;

                        foreach (cls_MTWorker MTWorkers in list_worker)
                        {
                            error = MTWorkers.worker_code;
                            string empname = "";
                            cls_MTWorker obj_worker = new cls_MTWorker();
                            cls_TREmpdep obj_workerdep = new cls_TREmpdep();
                            cls_TREmpdep obj_workerdep1 = new cls_TREmpdep();
                            cls_TREmpdep obj_workerdep2 = new cls_TREmpdep();
                            cls_TREmpdep obj_workerdep3 = new cls_TREmpdep();
                            cls_TREmpposition obj_workerpos = new cls_TREmpposition();
                            cls_MTPosition obj_MTPosition = new cls_MTPosition();
                            cls_TRPaytran obj_TRPaytran = new cls_TRPaytran();

                            foreach (cls_MTWorker worker in list_worker)
                            {
                                if (MTWorkers.worker_code.Equals(worker.worker_code))
                                {
                                    empname = worker.initial_name_en + " " + worker.worker_fname_en + " " + worker.worker_lname_en;
                                    obj_worker = worker;
                                    break;
                                }
                            }

                            foreach (cls_TREmpdep dep in list_TRdep)
                            {
                                if (MTWorkers.worker_code.Equals(dep.worker_code))
                                {
                                    obj_workerdep = dep;
                                    break;
                                }
                            }

                            foreach (cls_TREmpposition pos in list_TRpos)
                            {
                                if (MTWorkers.worker_code.Equals(pos.worker_code))
                                {
                                    obj_workerpos = pos;
                                    break;
                                }
                            }

                            foreach (cls_TRPaytran tran in list_paytran)
                            {
                                if (MTWorkers.worker_code.Equals(tran.worker_code))
                                {
                                    obj_TRPaytran = tran;
                                    break;
                                }
                            }

                            if (empname.Equals(""))
                                continue;

                            if (list_worker.Count > 0)
                            {
                                //1 รหัสพนักงาน
                                bkData = obj_worker.worker_code + "|";

                                //2 ชื่อ(อังกฤษ)
                                bkData += obj_worker.worker_fname_en + "|";

                                //3 นามสกุล(อังกฤษ)
                                bkData += obj_worker.worker_lname_en + "|";

                                //4 ระดับ01
                                cls_MTDep bj_MTDep1 = null;
                                bool foundDep1 = false;
                                foreach (cls_TREmpdep dep1 in list_TRdep)
                                {
                                    if (MTWorkers.worker_code.Equals(dep1.worker_code))
                                    {
                                        foreach (cls_MTDep MTDep1 in list_TDep)
                                        {
                                            if (dep1.empdep_level01 != null && dep1.empdep_level01.Equals(MTDep1.dep_code))
                                            {
                                                bj_MTDep1 = MTDep1;
                                                bkData += bj_MTDep1.dep_name_th + "|";
                                                foundDep1 = true;
                                                break;
                                            }
                                        }
                                    }
                                }

                                if (!foundDep1)
                                {
                                    bkData += " " + "|";
                                }

                                //5 ระดับ02
                                cls_MTDep bj_MTDep2 = null;
                                bool foundDep2 = false;
                                foreach (cls_TREmpdep dep2 in list_TRdep)
                                {
                                    if (MTWorkers.worker_code.Equals(dep2.worker_code))
                                    {
                                        foreach (cls_MTDep MTDep2 in list_TDep)
                                        {
                                            if (dep2.empdep_level02 != null && dep2.empdep_level02.Equals(MTDep2.dep_code))
                                            {
                                                bj_MTDep2 = MTDep2;
                                                bkData += bj_MTDep2.dep_name_th + "|";
                                                foundDep2 = true;
                                                break;
                                            }
                                        }
                                    }
                                }

                                if (!foundDep2)
                                {
                                    bkData += " " + "|";
                                }

                                //6 ระดับ03
                                //
                                cls_MTDep bj_MTDep3 = null;
                                bool foundDep3 = false;

                                foreach (cls_TREmpdep dep3 in list_TRdep)
                                {
                                    if (MTWorkers.worker_code.Equals(dep3.worker_code))
                                    {
                                        foreach (cls_MTDep MTDep3 in list_TDep)
                                        {
                                            if (dep3.empdep_level03 != null && dep3.empdep_level03.Equals(MTDep3.dep_code))
                                            {
                                                bj_MTDep3 = MTDep3;
                                                bkData += bj_MTDep3.dep_name_th + "|";
                                                foundDep3 = true;
                                                break;
                                            }
                                        }
                                    }
                                }

                                if (!foundDep3)
                                {
                                    bkData += " " + "|";
                                }

                                //7&8 ตำแหน่งปัจจุบัน Position
                                cls_MTPosition bj_Position1 = null;
                                bool foundPosition = false;

                                foreach (cls_TREmpposition emppos in list_TRpos)
                                {
                                    if (MTWorkers.worker_code.Equals(emppos.worker_code))
                                    {
                                        foreach (cls_MTPosition pos1 in list_MTPosition)
                                        {
                                            if (emppos.empposition_position != null && emppos.empposition_position.Equals(pos1.position_code))
                                            {
                                                bj_Position1 = pos1;
                                                bkData += bj_Position1.position_code + "|";
                                                bkData += bj_Position1.position_name_en + "|";
                                                foundPosition = true;
                                                break;
                                            }
                                        }
                                    }
                                }

                                if (!foundPosition)
                                {
                                    bkData += " " + "|";
                                    bkData += " " + "|";
                                }

                                //9 Salary
                                bool hasSAData = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && (TRPayitem.item_code.Equals("SA1") || TRPayitem.item_code.Equals("SA2") || TRPayitem.item_code.Equals("SA03")))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ EA ลงใน bkData
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasSAData = true;
                                    }
                                }
                                if (!hasSAData)
                                {
                                    bkData += "0|";
                                }

                                //10 Position
                                bool hasPAData = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("PA"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ EA ลงใน bkData
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasPAData = true;
                                    }
                                }
                                if (!hasPAData)
                                {
                                    bkData += "0|";
                                }

                                //11 House
                                bool hasHAData = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("HA"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ EA ลงใน bkData
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasHAData = true;
                                    }
                                }
                                if (!hasHAData)
                                {
                                    bkData += "0|";
                                }

                                //12 Commuting
                                bool hasCAData = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("CA"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ EA ลงใน bkData
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasCAData = true;
                                    }
                                }
                                if (!hasCAData)
                                {
                                    bkData += "0|";
                                }

                                //13 EN
                                bool hasEAData = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("EA"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ EA ลงใน bkData
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasEAData = true;
                                    }
                                }
                                if (!hasEAData)
                                {
                                    bkData += "0|";
                                }

                                //14 JA
                                bool hasJAData = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("JA"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ EA ลงใน bkData
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasJAData = true;
                                    }
                                }
                                if (!hasJAData)
                                {
                                    bkData += "0|";
                                }

                                //15 Meal
                                bool hasMAData = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("MA"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ EA ลงใน bkData
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasMAData = true;
                                    }
                                }
                                if (!hasMAData)
                                {
                                    bkData += "0|";
                                }

                                //16-19 OT
                                bool hasOTData = false;
                                foreach (cls_TRPayOT TRPayot in list_payot)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayot.worker_code))
                                    {
                                        bkData += TRPayot.payot_ot1_amount + "|";
                                        bkData += TRPayot.payot_ot15_amount + "|";
                                        bkData += TRPayot.payot_ot2_amount + "|";
                                        bkData += TRPayot.payot_ot3_amount + "|";

                                        hasOTData = true;
                                    }
                                }
                                if (!hasOTData)
                                {
                                    bkData += "0|";
                                    bkData += "0|";
                                    bkData += "0|";
                                    bkData += "0|";
                                }

                                //20 Transportation
                                bool hasTAData = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("TA"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ EA ลงใน bkData
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasTAData = true;
                                    }
                                }
                                if (!hasTAData)
                                {
                                    bkData += "0|";
                                }

                                //21 Up country
                                bool hasUAData = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("UA"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ EA ลงใน bkData
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasUAData = true;
                                    }
                                }
                                if (!hasUAData)
                                {
                                    bkData += "0|";
                                }

                                //22 Overnight
                                bool hasOAData = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("OA"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ EA ลงใน bkData
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasOAData = true;
                                    }
                                }
                                if (!hasOAData)
                                {
                                    bkData += "0|";
                                }

                                //23 Mobile
                                bool hasJPNData = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("JPN"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ EA ลงใน bkData
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasJPNData = true;
                                    }
                                }
                                if (!hasJPNData)
                                {
                                    bkData += "0|";
                                }

                                //24 Other income
                                bool hasOIData = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("OI"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ EA ลงใน bkData
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasOIData = true;
                                    }
                                }
                                if (!hasOIData)
                                {
                                    bkData += "0|";
                                }

                                //25 Bonus
                                bool hasBOData = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("BO"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ EA ลงใน bkData
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasBOData = true;
                                    }
                                }
                                if (!hasBOData)
                                {
                                    bkData += "0|";
                                }

                                //26 Annual Leave
                                bool hasALData = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("AL"))
                                    {
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasALData = true;
                                    }
                                }
                                if (!hasALData)
                                {
                                    bkData += "0|";
                                }

                                //27 AC1
                                bool hasAC1Data = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("AC1"))
                                    {
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasAC1Data = true;
                                    }
                                }
                                if (!hasAC1Data)
                                {
                                    bkData += "0|";
                                }

                                //28 AV1
                                bool hasAV1Data = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("AV1"))
                                    {
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasAV1Data = true;
                                    }
                                }
                                if (!hasAV1Data)
                                {
                                    bkData += "0|";
                                }

                                //29 HA1
                                bool hasHA1Data = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("HA1"))
                                    {
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasHA1Data = true;
                                    }
                                }
                                if (!hasHA1Data)
                                {
                                    bkData += "0|";
                                }

                                //30 OC1
                                bool hasOC1Data = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("OC1"))
                                    {
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasOC1Data = true;
                                    }
                                }
                                if (!hasOC1Data)
                                {
                                    bkData += "0|";
                                }

                                //31 SS1
                                bool hasSS1Data = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("SS1"))
                                    {
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasSS1Data = true;
                                    }
                                }
                                if (!hasSS1Data)
                                {
                                    bkData += "0|";
                                }

                                //32 FA
                                bool hasFAData = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("FA"))
                                    {
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasFAData = true;
                                    }
                                }
                                if (!hasFAData)
                                {
                                    bkData += "0|";
                                }

                                //33 CommutingExpat
                                bool hasCommutingExpatData = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("CommutingExpat"))
                                    {
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasCommutingExpatData = true;
                                    }
                                }
                                if (!hasCommutingExpatData)
                                {
                                    bkData += "0|";
                                }

                                //34 ACC
                                bool hasACCData = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("ACC"))
                                    {
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasACCData = true;
                                    }
                                }
                                if (!hasACCData)
                                {
                                    bkData += "0|";
                                }

                                //35 Cold
                                bool hasColdData = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("Cold"))
                                    {
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasColdData = true;
                                    }
                                }
                                if (!hasColdData)
                                {
                                    bkData += "0|";
                                }

                                //36 COM
                                bool hasCOMData = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("COM"))
                                    {
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasCOMData = true;
                                    }
                                }
                                if (!hasCOMData)
                                {
                                    bkData += "0|";
                                }

                                //37 Emergency
                                bool hasEmergencyData = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("Emergency"))
                                    {
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasEmergencyData = true;
                                    }
                                }
                                if (!hasEmergencyData)
                                {
                                    bkData += "0|";
                                }

                                //38 INCOME_ACC
                                bool hasINCOME_ACCData = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("INCOME_ACC"))
                                    {
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasINCOME_ACCData = true;
                                    }
                                }
                                if (!hasINCOME_ACCData)
                                {
                                    bkData += "0|";
                                }

                                //39 PHA
                                bool hasPHAData = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("PHA"))
                                    {
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasPHAData = true;
                                    }
                                }
                                if (!hasPHAData)
                                {
                                    bkData += "0|";
                                }

                                //40 SBA
                                bool hasSBAData = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("SBA"))
                                    {
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasSBAData = true;
                                    }
                                }
                                if (!hasSBAData)
                                {
                                    bkData += "0|";
                                }

                                //41 SW
                                bool hasSWData = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("SW"))
                                    {
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasSWData = true;
                                    }
                                }
                                if (!hasSWData)
                                {
                                    bkData += "0|";
                                }

                                //42 INTAX
                                bool hasINTAXData = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("INTAX"))
                                    {
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasINTAXData = true;
                                    }
                                }
                                if (!hasINTAXData)
                                {
                                    bkData += "0|";
                                }

                                //43 OS
                                bool hasOSData = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("OS"))
                                    {
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasOSData = true;
                                    }
                                }
                                if (!hasOSData)
                                {
                                    bkData += "0|";
                                }

                                //44 Total Income
                                bool hasTTinData = false;
                                foreach (cls_TRPaytran TRPaytran in list_paytran)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPaytran.worker_code))
                                    {
                                        bkData += TRPaytran.paytran_income_total + "|";
                                        hasTTinData = true;
                                    }
                                }
                                if (!hasTTinData)
                                {
                                    bkData += "0|";
                                }

                                //45 Tax 
                                bool hasTaxData = false;
                                foreach (cls_TRPaytran TRPaytran in list_paytran)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPaytran.worker_code))
                                    {
                                        double tax = TRPaytran.paytran_tax_401 + TRPaytran.paytran_tax_4012 + TRPaytran.paytran_tax_4013 + TRPaytran.paytran_tax_402I + TRPaytran.paytran_tax_402O;
                                        bkData += tax + "|";
                                        hasTaxData = true;
                                    }
                                }
                                if (!hasTaxData)
                                {
                                    bkData += "0|";
                                }

                                //46 SSO
                                bool hasSSOData = false;
                                foreach (cls_TRPaytran TRPaytran in list_paytran)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPaytran.worker_code))
                                    {
                                        bkData += TRPaytran.paytran_ssoemp + "|";
                                        hasSSOData = true;
                                    }
                                }
                                if (!hasSSOData)
                                {
                                    bkData += "0|";
                                }

                                //47 PF
                                bool hasPFData = false;
                                foreach (cls_TRPaytran TRPaytran in list_paytran)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPaytran.worker_code))
                                    {
                                        bkData += TRPaytran.paytran_pfemp + "|";
                                        hasPFData = true;
                                    }
                                }
                                if (!hasPFData)
                                {
                                    bkData += "0|";
                                }

                                //48 AB
                                bool hasAB01Data = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("AB01"))
                                    {
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasAB01Data = true;
                                    }
                                }
                                if (!hasAB01Data)
                                {
                                    bkData += "0|";
                                }

                                //49 CH
                                bool hasCHData = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("CH"))
                                    {
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasCHData = true;
                                    }
                                }
                                if (!hasCHData)
                                {
                                    bkData += "0|";
                                }

                                //50 HD
                                bool hasHDData = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("HD"))
                                    {
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasHDData = true;
                                    }
                                }
                                if (!hasHDData)
                                {
                                    bkData += "0|";
                                }

                                //51 HA2
                                bool hasHA2Data = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("HA2"))
                                    {
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasHA2Data = true;
                                    }
                                }
                                if (!hasHA2Data)
                                {
                                    bkData += "0|";
                                }

                                //52 LV01
                                bool hasLV01Data = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("LV01"))
                                    {
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasLV01Data = true;
                                    }
                                }
                                if (!hasLV01Data)
                                {
                                    bkData += "0|";
                                }

                                //53 LT01
                                bool hasLT01Data = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("LT01"))
                                    {
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasLT01Data = true;
                                    }
                                }
                                if (!hasLT01Data)
                                {
                                    bkData += "0|";
                                }

                                //54 AV2
                                bool hasAV2Data = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("AV2"))
                                    {
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasAV2Data = true;
                                    }
                                }
                                if (!hasAV2Data)
                                {
                                    bkData += "0|";
                                }

                                //55 SLF2
                                bool hasSLF2Data = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("SLF2"))
                                    {
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasSLF2Data = true;
                                    }
                                }
                                if (!hasSLF2Data)
                                {
                                    bkData += "0|";
                                }

                                //56 SLF1
                                bool hasSLF1Data = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("SLF1"))
                                    {
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasSLF1Data = true;
                                    }
                                }
                                if (!hasSLF1Data)
                                {
                                    bkData += "0|";
                                }

                                //57 OD2
                                bool hasOD2Data = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("OD2"))
                                    {
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasOD2Data = true;
                                    }
                                }
                                if (!hasOD2Data)
                                {
                                    bkData += "0|";
                                }

                                //58 OC2
                                bool hasOC2Data = false;
                                foreach (cls_TRPayitem TRPayitem in list_payitem)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPayitem.worker_code) && TRPayitem.item_code.Equals("OC2"))
                                    {
                                        bkData += TRPayitem.payitem_amount + "|";
                                        hasOC2Data = true;
                                    }
                                }
                                if (!hasOC2Data)
                                {
                                    bkData += "0|";
                                }

                                //59 Total Deduct
                                bool hasTTdeData = false;
                                foreach (cls_TRPaytran TRPaytran in list_paytran)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPaytran.worker_code))
                                    {
                                        bkData += TRPaytran.paytran_deduct_total + "|";
                                        hasTTdeData = true;
                                    }
                                }
                                if (!hasTTdeData)
                                {
                                    bkData += "0|";
                                }

                                //60-62 netpay
                                bool hasNetCData = false;
                                foreach (cls_TRPaytran TRPaytran in list_paytran)
                                {
                                    if (MTWorkers.worker_code.Equals(TRPaytran.worker_code))
                                    {
                                        bkData += TRPaytran.paytran_netpay_c + "|";
                                        bkData += TRPaytran.paytran_netpay_b + "|";
                                        double net = TRPaytran.paytran_netpay_c + TRPaytran.paytran_netpay_b;
                                        bkData += net + "|";
                                        hasNetCData = true;
                                    }
                                }
                                if (!hasNetCData)
                                {
                                    bkData += "0|";
                                    bkData += "0|";
                                    bkData += "0|";
                                }

                                tmpData += bkData + '\r' + '\n';
                            }

                        }

                        int record = list_worker.Count;

                        //-- Step 1 create file
                        string filename = "TRN_INDE" + DateTime.Now.ToString("yyMMddHHmm") + "." + "xls";
                        string filepath = Path.Combine
                       (ClassLibrary_BPC.Config.PathFileExport, filename);

                        // Check if file already exists. If yes, delete it.     
                        if (File.Exists(filepath))
                        {
                            File.Delete(filepath);
                        }
                        DataSet ds = new DataSet();
                        string str = tmpData.Replace("\r\n", "]");
                        string[] data = str.Split(']');

                        DataTable dataTable = ds.Tables.Add();
                        dataTable.Columns.AddRange(new DataColumn[63] { new DataColumn("Emp ID"), new DataColumn("First Name"), new DataColumn("Last Name"), new DataColumn("Level 01"), new DataColumn("Level 02"),
                                                                        new DataColumn("Level 03"),new DataColumn("Position Code"),new DataColumn("Position Title"),new DataColumn("Salary"),new DataColumn("Position"),
                                                                        new DataColumn("House"),new DataColumn("Commuting"),new DataColumn("EN"),new DataColumn("JPN"),new DataColumn("Meal"),new DataColumn("OT1"),
                                                                        new DataColumn("OT1.5"),new DataColumn("OT2"),new DataColumn("OT3"),new DataColumn("Transportation"),new DataColumn("Up Country"),new DataColumn("Over Night"),
                                                                        new DataColumn("Mobile"),new DataColumn("Other Income"),new DataColumn("Bonus"),new DataColumn("Annual Leave"),new DataColumn("Allowance CTAX"),
                                                                        new DataColumn("Advance CTAX_income"),new DataColumn("House CTAX_income"),new DataColumn("Other CTAX_income"),new DataColumn("SSSF"),new DataColumn("Fix Allowance"),
                                                                        new DataColumn("Commuting Expat"),new DataColumn("Accommodation Allowance"),new DataColumn("Cold Allowance"),new DataColumn("Compensation Allowance"),
                                                                        new DataColumn("Emergency"),new DataColumn("Income brought forward for tax calculation"),new DataColumn("Phone Allowance"),new DataColumn("Standby"),
                                                                        new DataColumn("Stagged Work"),new DataColumn("INTAX"),new DataColumn("Others"),new DataColumn("Total Income"),new DataColumn("Tax"),new DataColumn("SSO"),
                                                                        new DataColumn("PF"),new DataColumn("Absent Deduct"),new DataColumn("Child Education_deduct"),new DataColumn("House DETAX"),
                                                                        new DataColumn("House CTAX_Deduct"),new DataColumn("Leave"),new DataColumn("Late"),new DataColumn("Advance CTAX_deduct"),
                                                                        new DataColumn("Court Payment"),new DataColumn("Student Loan"),new DataColumn("Other DETAX"),
                                                                        new DataColumn("Other CTAX_deduct"),new DataColumn("Totoal Deduct"),new DataColumn("Netpay Cash"),
                                                                        new DataColumn("Netpay Bank"),new DataColumn("Netpay")
                                                                        , new DataColumn(" ")
                        });

                        string[] rows = str.Split(']');
                        foreach (var row in rows)
                        {
                            if (string.IsNullOrEmpty(row))
                                continue;
                            string[] rowData = row.Split('|');
                            dataTable.Rows.Add(rowData);
                        }
                        ExcelLibrary.DataSetHelper.CreateWorkbook(filepath, ds);
                        strResult = filename;
                    }
                    task.task_end = DateTime.Now;
                    task.task_status = "F";
                    task.task_note = strResult;
                    objMTTask.updateStatus(task);
                }
                catch (Exception ex)
                {
                    strResult = ex.ToString() + error;
                }

            }
            else
            {

            }

            return strResult;
        }

    }
}
