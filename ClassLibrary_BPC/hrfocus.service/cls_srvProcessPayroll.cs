using ClassLibrary_BPC.hrfocus.controller;
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
                List<cls_TRCombank> list_combank = objCombank.getDataByFillter(com);
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
                string sequence = "000001";
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
                     if (task_bank.Length > 0)
                    {
                        switch (paybank.paybank_bankcode)

                        {
                            case "002":/// ธนาคารกรุงเทพ
                                {
                                    //กำหนดค่า tmpData โดยรวมข้อมูล combank.combank_bankcode และ comdetail.company_name_en
                                    tmpData = "H" + sequence + combank.combank_bankcode + combank.combank_bankaccount + comdetail.company_name_en + datePay.ToString("ddMMyy", DateTimeFormatInfo.CurrentInfo) + spare + "\r\n";

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

                                        foreach (cls_MTWorker worker in list_worker)
                                        {
                                            if (paytran.worker_code.Equals(worker.worker_code))
                                            {
                                                empname = " " + worker.initial_name_en + " " + worker.worker_fname_en + " " + worker.worker_lname_en + " " + datePay.ToString("ddMMyy", DateTimeFormatInfo.CurrentInfo) + " ";
                                                empname2 = " " + worker.initial_name_en + " " + worker.worker_fname_en + " " + worker.worker_lname_en + " ";

                                                break;
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


                                        sequence = Convert.ToString(index + 2).PadLeft(6, '0');

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
                                                empaccbank = "C";
                                                break;
                                            }
                                            else
                                            {
                                                if (worker.empbank_bankpercent != 0)
                                                {
                                                    empaccbank = "C";
                                                }
                                                else if (worker.empbank_cashpercent != 0)
                                                {
                                                    empaccbank = "D";
                                                }
                                                break;
                                            }
                                        }
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
                                    sequence = (index + 2).ToString().PadLeft(6, '0');

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


                                    int empbankrecord = list_empbank.Count;
                                    sequence1 = (douTotal).ToString().PadLeft(7, '0');
                                    string empbankrecord1 = "";
                                    if (record > 1)
                                    {
                                        empbankrecord1 = (empbankrecord).ToString().PadLeft(7, '0');
                                    }

                                    sequence2 = (douTotal2).ToString().PadLeft(7, '0');
                                    string empbankrecord2 = "";
                                    if (record > 1)
                                    {
                                        empbankrecord2 = (empbankrecord2).ToString().PadLeft(7, '0');
                                    }


                                    if (spare3.Length < 68)
                                        spare3 = spare3.PadRight(68, '0');





                                    string total1 = douTotal.ToString("#.#0").Trim().Replace(".", "").PadLeft(13, '0');
                                    string total2 = douTotal2.ToString("#.#0").Trim().Replace(".", "").PadLeft(13, '0');


                                    bkData = "T" + sequence + combank.combank_bankcode + combank.combank_bankaccount + empbankrecord1 + total1 + sequence2 + total2 + spare3;

                                    bkData = bkData.PadRight(81, '0');



                                    tmpData += bkData;

                                }
                                break;

                            case "014": //scb
                                {
                                    string companyid = "";
                                    string combankid1 = "";
                                    string combankid2 = "";

                                    string filedate = "";
                                    string filetime = "";
                                    string batchreference = "";

                                    string space2 = ""; //ค่าว่าง
                                    string space3 = ""; //ค่าว่าง
                                    string space4 = ""; //ค่าว่าง
                                    string space8 = ""; //ค่าว่าง
                                    string space9 = ""; //ค่าว่าง
                                    string space10 = ""; //ค่าว่าง
                                    string space14 = ""; //ค่าว่าง
                                    string space18 = ""; //ค่าว่าง
                                    string space20 = ""; //ค่าว่าง
                                    string space35 = ""; //ค่าว่าง
                                    string space40 = ""; //ค่าว่าง
                                    string space70 = ""; //ค่าว่าง
                                    string space100 = ""; //ค่าว่าง

                                    if (space4.Length < 4)
                                    {
                                        space4 = space4.PadLeft(4);
                                    }
                                    if (space14.Length < 14)
                                    {
                                        space14 = space14.PadLeft(14);
                                    }

                                    if (space40.Length < 40)
                                    {
                                        space40 = space40.PadLeft(40);
                                    }
                                    if (space8.Length < 8)
                                    {
                                        space8 = space8.PadLeft(8);
                                    }
                                    if (space35.Length < 35)
                                    {
                                        space35 = space35.PadLeft(35);
                                    }
                                    if (space20.Length < 20)
                                    {
                                        space20 = space20.PadLeft(20);
                                    }
                                    if (space3.Length < 3)
                                    {
                                        space3 = space3.PadLeft(3);
                                    }
                                    if (space2.Length < 2)
                                    {
                                        space2 = space2.PadLeft(2);
                                    }
                                    if (space18.Length < 18)
                                    {
                                        space18 = space18.PadLeft(18);
                                    }

                                    if (space70.Length < 70)
                                    {
                                        space70 = space18.PadLeft(70);
                                    }
                                    if (space10.Length < 10)
                                    {
                                        space10 = space10.PadLeft(10);
                                    }

                                    if (combank.company_code.Length < 12)
                                        companyid = combank.company_code.PadRight(12, ' ');

                                    string firstFourDigits = combank.company_code.PadRight(4, ' '); // เพิ่มเติมค่าว่างเพื่อให้มีความยาว 4 ตัวอักษร
                                    //string firstFourDigits = combank.company_code.Substring(0, 4);   
                                    string datePart1 = datePay.ToString("yyyyMMdd", DateTimeFormatInfo.CurrentInfo);
                                    string datePart2 = datePay.ToString("HHmmss", DateTimeFormatInfo.CurrentInfo);
                                    string description = firstFourDigits + datePart1;

                                    if (description.Length <= 32)
                                        description = description.PadRight(32, ' ');

                                    if (datePart1.Length <= 8)
                                        filedate = datePart1.PadRight(8, '0');

                                    if (datePart2.Length <= 6)
                                        filetime = datePart2.PadRight(6, '0');

                                    if (firstFourDigits.Length <= 32)
                                        batchreference = firstFourDigits.PadRight(32, ' ');
                                    double douTotal = 0;
                                    int index = 0;
                                    string bkData;
                                    foreach (cls_TRPaytran paytran in list_paytran)
                                    {

                                        string empacc = "";
                                        string debitsccountno = "";
                                        string accounttype1 = "";
                                        string accounttype2 = "";

                                        if (empacc.Length <= 25)
                                            debitsccountno = empacc.PadRight(25, ' ');

                                        string accountNumber1 = combank.combank_bankaccount;
                                        char fourthDigit = accountNumber1[3];
                                        string result1 = "0" + fourthDigit;
                                        if (result1.Length <= 2)
                                            accounttype1 = result1.PadRight(2, '0');

                                        string accountNumber2 = combank.combank_bankaccount;
                                        string firstThreeDigits = accountNumber2.Substring(0, 3);
                                        string result2 = "0" + firstThreeDigits;
                                        if (result2.Length <= 4)
                                            accounttype2 = result2.PadRight(4, '0');


                                        double myNumber = paytran.paytran_netpay_b;
                                        string myStringNumber = myNumber.ToString("F2").Replace(".", "").Replace(",", "");

                                        if (myStringNumber.Length < 16)
                                        {
                                            myStringNumber = myStringNumber.PadLeft(16, '0');
                                        }

                                        if (space9.Length < 9)
                                        {
                                            space9 = space9.PadLeft(9);
                                        }


                                        double total = 0;
                                        if (paytran.paytran_netpay_b > 1)
                                        {
                                            total += paytran.paytran_netpay_b;
                                        }
                                        if (combank.combank_bankaccount.Length < 25)
                                            combankid1 = combank.combank_bankaccount.PadRight(25, ' ');

                                        if (combank.combank_bankaccount.Length < 15)
                                            combankid2 = combank.combank_bankaccount.PadRight(15, ' ');


                                        //string total2 = total.ToString().PadLeft(16, '0');
                                        string total2 = "";
                                        total2 = total.ToString("#.#0").Trim().Replace(".", "").PadLeft(16, '0');


                                        //              //เลขบัญชีธนาคาร  //Customer Reference  //Date of generate/extract data  //Time of generate/extract data //เลขอ้างอิง
                                        tmpData = "001" + companyid + description + filedate + filetime + "BMC" + batchreference + "\r\n";

                                        int totalData = list_paytran.Count;
                                        string totalDataString = totalData.ToString().PadLeft(6, '0'); // แปลงจำนวนข้อมูลให้เป็น string และใส่ 0 ด้านหน้าให้ครบ 7 หลัก

                                        ///                             " วันที่จ่ายเงินให้บริษัท" "บัญชีที่หักเงินต้น"                      "ระบุ จำนวนเงินรวมที่ต้องจ่ายให้บริษัท"ระบุ จำนวนบริษัทที่ทำการจ่าย"
                                        bkData = "002" + "PAY" + filedate + combankid1 + accounttype1 + accounttype2 + "THB" + total2 + "00000001" + totalDataString + combankid2 + space9 + " " + accounttype1 + accounttype2;

                                        bkData = bkData.PadRight(109, '0');
                                        tmpData += bkData.PadRight(109, ' ') + "\r\n";
                                    }

                                    //003
                                    foreach (cls_TRPaytran paytran in list_paytran)
                                    {

                                        string empacc = "";
                                        string empname = "";
                                        string empname2 = "";



                                        foreach (cls_MTWorker worker in list_worker)
                                        {
                                            if (paytran.worker_code.Equals(worker.worker_code))
                                            {
                                                empname = " " + worker.initial_name_en + " " + worker.worker_fname_en + " " + worker.worker_lname_en + " " + datePay.ToString("ddMMyy", DateTimeFormatInfo.CurrentInfo) + " ";
                                                empname2 = " " + worker.initial_name_en + " " + worker.worker_fname_en + " " + worker.worker_lname_en + " ";

                                                break;
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

                                        if (empname.Equals("") || empacc.Equals(""))
                                            continue;
                                        sequence = Convert.ToString(index + 1).PadLeft(6, '0');
                                        string crediaccount = "";
                                        if (empacc.Length < 25)
                                            crediaccount = empacc.PadRight(25, ' ');

                                        string receivingbankname = "SIAM COMMERCIAL BANK";
                                        if (receivingbankname.Length < 35)
                                        {
                                            receivingbankname = receivingbankname.PadRight(35, ' ');
                                        }

                                        string receivingbranchcode = empacc;
                                        string accountNumber = empacc; // เลขที่บัญชีพนักงาน
                                        string modifiedAccountNumber = "0" + accountNumber.Substring(0, 3); // เพิ่ม "0" ไว้ด้านหน้า 3 ตัวแรกของเลขที่บัญชี
                                        if (modifiedAccountNumber.Length <= 4)
                                        {
                                            receivingbranchcode = modifiedAccountNumber.PadLeft(4);
                                        }

                                        // "ระบุ ยอดเงินจ่ายให้บริษัท"
                                        double myNumber = paytran.paytran_netpay_b;
                                        string myStringNumber = Math.Floor(myNumber).ToString(); // ตัดทศนิยมออก
                                        if (myStringNumber.Length < 16) // ตรวจสอบความยาวของ string
                                        {
                                            myStringNumber = myStringNumber.PadLeft(16, '0'); // ใส่เลข 0 ด้านหน้าให้ครบ 16 หลัก
                                        }

                                        //                                 " วันที่ระบุ เลขที่บัญชีของบริษัท"                                                                                                                                                                                                                                                                                                                                                                                                                                                                         จะเป็นข้อความในการส่งให้ลุกค้า   
                                        bkData = "003" + sequence + crediaccount + myStringNumber + "THB" + "00000001" + "N" + "N" + "Y" + "S" + space4 + "00 " + space14 + "000000" + "00" + " 0000000000000000" + " 000000" + " 0000000000000000" + "0" + space40 + space8 + "014" + receivingbankname + receivingbranchcode + space35 + " " + "N " + space20 + " " + space3 + space2 + " " + space18 + space2;
                                        bkData = bkData.PadRight(32, '0');

                                        tmpData += bkData.PadRight(128, ' ') + "\r\n";

                                        if (empname2.Length > 100)
                                            empname2 = empname2.Substring(0, 100);

                                        bkData = "004" + "00000001" + sequence + "000000000000000" + empname2 + space70 + space70 + space70 + space10 + space70 + space100 + space70 + space70 + space70;

                                        bkData = bkData.PadRight(32, '0');
                                        tmpData += bkData.PadRight(128, ' ') + "\r\n";
                                        douTotal += paytran.paytran_netpay_b;
                                        index++;
                                    }
                                    //999
                                    int record = list_paytran.Count;
                                    double totals = 0;
                                    foreach (cls_TRPaytran paytran in list_paytran)
                                    {
                                        if (paytran.paytran_netpay_b > 1)
                                        {
                                            totals += paytran.paytran_netpay_b;
                                        }
                                    }
                                    string totals2 = totals.ToString().PadLeft(16, '0');
                                    sequence = (index).ToString().PadLeft(6, '0');
                                    bkData = "999" + "000001" + sequence + totals2;
                                    bkData = bkData.PadRight(31, '0');
                                    tmpData += bkData;
                                }
                                break;

                            case "004": // กสิกรไทย 
                                {
                                    string companyid = "";
                                    string filedate = "";
                                    string filetime = "";
                                    string batchreference = "";
                                    string branchid = "";
                                    string bankaccount = "";


                                    string space14 = ""; //ค่าว่าง
                                    string space1 = ""; //ค่าว่าง
                                    string space25 = ""; //ค่าว่าง
                                    string space8 = ""; //ค่าว่าง
                                    string free9 = ""; //ค่าว่าง
                                    string free10 = ""; //ค่าว่าง
                                    string free14 = ""; //ค่าว่าง
                                    string free18 = ""; //ค่าว่าง
                                    string free20 = ""; //ค่าว่าง
                                    string free35 = ""; //ค่าว่าง
                                    string free40 = ""; //ค่าว่าง
                                    string free70 = ""; //ค่าว่าง
                                    string free100 = ""; //ค่าว่าง
                                    string amount = "";
                                    if (space25.Length < 25)
                                    {
                                        space25 = space25.PadLeft(25);
                                    }
                                    if (free14.Length < 14)
                                    {
                                        free14 = free14.PadLeft(14);
                                    }

                                    if (free40.Length < 40)
                                    {
                                        free40 = free40.PadLeft(40);
                                    }
                                    if (space8.Length < 8)
                                    {
                                        space8 = space8.PadLeft(8);
                                    }
                                    if (free35.Length < 35)
                                    {
                                        free35 = free35.PadLeft(35);
                                    }
                                    if (free20.Length < 20)
                                    {
                                        free20 = free20.PadLeft(20);
                                    }
                                    if (space1.Length < 1)
                                    {
                                        space1 = space1.PadLeft(1);
                                    }
                                    if (space14.Length < 14)
                                    {
                                        space14 = space14.PadLeft(14);
                                    }
                                    if (free18.Length < 18)
                                    {
                                        free18 = free18.PadLeft(18);
                                    }

                                    if (free70.Length < 70)
                                    {
                                        free70 = free18.PadLeft(70);
                                    }
                                    if (free10.Length < 10)
                                    {
                                        free10 = free10.PadLeft(10);
                                    }


                                    foreach (var branch in list_combranch)
                                    {
                                        if (branch.combranch_code.Length <= 5)
                                        {
                                            branchid = branch.combranch_code.PadRight(5, ' ');
                                        }



                                        foreach (var data in list_combank)
                                        {
                                            if (data.combank_bankcode.StartsWith("004"))
                                            {


                                                if (data.company_code.Length < 50)
                                                {
                                                    companyid = data.company_code.PadRight(50, ' ');
                                                }


                                                //if (data.combank_branch.Length < 5)
                                                //{
                                                //    branchid = data.combank_branch.PadRight(5, ' '); 
                                                //}



                                                if (data.combank_bankaccount.Length <= 10)
                                                {
                                                    bankaccount = data.combank_bankaccount.PadRight(10, ' ');
                                                }


                                                string firstFourDigits = combank.company_code.PadRight(4, ' '); // เพิ่มเติมค่าว่างเพื่อให้มีความยาว 4 ตัวอักษร
                                                //string firstFourDigits = combank.company_code.Substring(0, 4); 

                                                string batchref = "TRAN" + "-" + DateTime.Now.ToString("yyyyMMdd");


                                                string datePart1 = datePay.ToString("yyMMdd", DateTimeFormatInfo.CurrentInfo);
                                                string datePart2 = datePay.ToString("HHmmss", DateTimeFormatInfo.CurrentInfo);
                                                string description = firstFourDigits + datePart1;

                                                if (batchref.Length <= 16)
                                                    batchref = batchref.PadRight(16, ' ');

                                                if (description.Length <= 32)
                                                    description = description.PadRight(32, ' ');

                                                if (datePart1.Length <= 6)
                                                    filedate = datePart1.PadRight(6, '0');

                                                if (datePart2.Length <= 6)
                                                    filetime = datePart2.PadRight(6, '0');

                                                if (firstFourDigits.Length <= 50)
                                                    batchreference = firstFourDigits.PadRight(50, ' ');

                                                double douTotal = 0;
                                                int index = 0;
                                                string bkData;
                                                foreach (cls_TRPaytran paytran in list_paytran)
                                                {

                                                    string empacc = "";
                                                    string debitsccountno = "";
                                                    string accounttype1 = "";
                                                    string accounttype2 = "";

                                                    if (empacc.Length <= 25)
                                                        debitsccountno = empacc.PadRight(25, ' ');

                                                    string accountNumber1 = combank.combank_bankaccount;
                                                    if (accountNumber1.Length <= 10)
                                                        bankaccount = accountNumber1.PadRight(10, ' ');



                                                    char fourthDigit = accountNumber1[3];
                                                    string result1 = "0" + fourthDigit;
                                                    if (result1.Length <= 2)
                                                        accounttype1 = result1.PadRight(2, '0');

                                                    string accountNumber2 = combank.combank_bankaccount;
                                                    string firstThreeDigits = accountNumber2.Substring(0, 3);
                                                    string result2 = "0" + firstThreeDigits;
                                                    if (result2.Length <= 4)
                                                        accounttype2 = result2.PadRight(4, '0');


                                                    double myNumber = paytran.paytran_netpay_b;
                                                    string myStringNumber = myNumber.ToString("F2").Replace(".", "").Replace(",", "");

                                                    if (myStringNumber.Length < 16)
                                                    {
                                                        myStringNumber = myStringNumber.PadLeft(16, '0');
                                                    }

                                                    if (free9.Length < 9)
                                                    {
                                                        free9 = free9.PadLeft(9);
                                                    }

                                                    double total = 0;
                                                    if (paytran.paytran_netpay_b > 1)
                                                    {
                                                        total += paytran.paytran_netpay_b;
                                                    }
                                                    //string total2 = total.ToString().PadLeft(16, '0');
                                                    string total2 = "";
                                                    total2 = total.ToString("#.#0").Trim().Replace(".", "").PadLeft(16, '0');




                                                    decimal temp = (decimal)paytran.paytran_netpay_b;
                                                    amount = temp.ToString("#.#0").Trim().Replace(".", "").PadLeft(15, '0');


                                                    int totalData = list_paytran.Count;
                                                    string totalDataString = totalData.ToString().PadLeft(18, '0');



                                                    //Header
                                                    tmpData = "H" + "|" + "PCT" + "|" + batchref + "|" + "000000" + "|" + space14 + "|"
                                                        + bankaccount + "|" + space1 + "|" + amount + "|" + space1 + "|"
                                                        + datePart1 + "|" + space25 + "|" + companyid + "|" + datePart1 + "|" + totalDataString + "|" + "N" + "|" + branchid + "|" + "\r\n";

                                                }

                                                //Detail
                                                foreach (cls_TRPaytran paytran in list_paytran)
                                                {

                                                    string empacc = "";
                                                    string empname = "";
                                                    string empname2 = "";
                                                    string amounts = "";
                                                    string tax = "";//หักณที่จ่าย
                                                    string payee_tax_id = "";//บัตรประชาชน
                                                    string payee_personal_id = "";


                                                    foreach (cls_MTWorker worker in list_worker)
                                                    {
                                                        if (paytran.worker_code.Equals(worker.worker_code))
                                                        {
                                                            empname = " " + worker.initial_name_en + " " + worker.worker_fname_en + " " + worker.worker_lname_en + " " + datePay.ToString("ddMMyy", DateTimeFormatInfo.CurrentInfo) + " ";
                                                            empname2 = " " + worker.initial_name_en + " " + worker.worker_fname_en + " " + worker.worker_lname_en + " ";

                                                            break;
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

                                                    if (empname.Equals("") || empacc.Equals(""))
                                                        continue;
                                                    string crediaccount = "";
                                                    if (empacc.Length <= 10)
                                                        crediaccount = empacc.PadRight(10, ' ');

                                                    ///
                                                    sequence = Convert.ToString(index + 1).PadLeft(6, '0');

                                                    string beneref = (index + 1).ToString("000").PadRight(16);

                                                    decimal temp = (decimal)paytran.paytran_netpay_b;
                                                    amounts = temp.ToString("#.#0").Trim().Replace(".", "").PadLeft(15, '0');
                                                    string total_inv_amt_bef_vat = "";
                                                    string total_tax_deducted_amt = "";
                                                    string total_Inv_amt_After_vat = "";

                                                    //กรณีที่มีการหักภาษีณ ที่จ่ายควรใส่ข้อมูลให้สอดคล้องกัน
                                                    if (paytran.paytran_tax_401 != null && Convert.ToDecimal(paytran.paytran_tax_401) > 0.00m)
                                                    {
                                                        total_inv_amt_bef_vat += paytran.paytran_tax_401;
                                                        if (total_inv_amt_bef_vat.Length <= 10.2)
                                                        {
                                                            total_inv_amt_bef_vat = total_inv_amt_bef_vat.PadLeft(10);
                                                        }

                                                        total_tax_deducted_amt += paytran.paytran_tax_4012;
                                                        if (total_tax_deducted_amt.Length <= 10.2)
                                                        {
                                                            total_tax_deducted_amt = total_tax_deducted_amt.PadLeft(10);
                                                        }
                                                        total_Inv_amt_After_vat += paytran.paytran_tax_4013;
                                                        if (total_Inv_amt_After_vat.Length <= 10.2)
                                                        {
                                                            total_Inv_amt_After_vat = total_Inv_amt_After_vat.PadLeft(10);
                                                        }

                                                        foreach (cls_TREmpcard card in list_empcard)
                                                        {
                                                            if (paytran.worker_code.Equals(card.worker_code))
                                                            {
                                                                obj_card = card;
                                                                break;
                                                            }
                                                        }



                                                        //foreach (cls_TREmpcard worker in list_worker)
                                                        //{
                                                        //    if (worker.worker_cardno.Equals(worker.worker_cardno))
                                                        //    {
                                                        //        payee_tax_id = "".PadRight(10);

                                                        //        break;
                                                        //    }
                                                        //    if (worker.worker_cardno.Equals(worker.worker_cardno))
                                                        //    {
                                                        //        payee_personal_id = "".PadRight(13);
                                                        //        break;
                                                        //    }
                                                        //}
                                                    }
                                                    else
                                                    {
                                                        total_inv_amt_bef_vat = "".PadRight(10);
                                                    }
                                                    {
                                                        total_tax_deducted_amt = "".PadRight(10);
                                                    }
                                                    {
                                                        total_Inv_amt_After_vat = "".PadRight(10);
                                                    }
                                                    {
                                                        payee_tax_id = "".PadRight(10);
                                                    }
                                                    {
                                                        payee_personal_id = "".PadRight(13);
                                                    }

                                                    //"ชื่อไฟล์เอกสาร" 
                                                    string attachment = "".PadRight(50);
                                                    // " รูปแบบการแจ้งเตือน" 
                                                    string advicemode = "".PadRight(1);
                                                    //" หมายเลขโทรสาร" 
                                                    string faxno = "".PadRight(50);
                                                    //" E"  
                                                    string emailid = "".PadRight(50);
                                                    //  ayee Address1 
                                                    string address1 = "".PadRight(30);
                                                    //  ayee Address2
                                                    string address2 = "".PadRight(30);
                                                    //  ayee Address3
                                                    string address3 = "".PadRight(30);
                                                    //  ayee Address4
                                                    string address4 = "".PadRight(30);





                                                    bkData = "D" + "|" + sequence + "|" + space8 + "|" + crediaccount + "|" + space1 + "|" + amounts + "จํานวนเงินที่ต้องการโอนเข้าในแต่ละบัญชี" + "|" + space1 + "|" + datePart1 + "วันที่" + "|" + space25 + "|" + empname2 + "ชื่อผู้รับเงิน" + "|" + datePart1 + "วันที่ " + "|" + "000" + "จำนวนรายการทั้งหมด" + "|" + beneref + "หมายเลขอ้างอิง" + "|" + attachment + "|" + advicemode + "|" + faxno + "|" + emailid + "|" + total_inv_amt_bef_vat + "|" + total_tax_deducted_amt + "|" + total_Inv_amt_After_vat + "|" + payee_tax_id + "|" + payee_personal_id + "|" + address1 + "|" + address2 + "|" + address3 + "|" + address4;

                                                    bkData = bkData.PadRight(32, '0');
                                                    tmpData += bkData.PadRight(128, ' ') + "\r\n";
                                                    douTotal += paytran.paytran_netpay_b;
                                                    index++;
                                                }
                                            }
                                        }
                                    }
                                }
                                break;
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
                List<cls_TRCombank> list_combank = objCombank.getDataByFillter(com);
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
                List<cls_TRCombank> list_combank = objCombank.getDataByFillter(com);
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
                            
                            //2.เลขประจำตัวประชาชนผู้มี่หน้าที่หัก ณ ที่จ่าย<CardNo>	comcard.card_type
                            if (comcard.comcard_code.Length == 13)
                                bkData += comcard.comcard_code + "|";
                            else
                                bkData += "0000000000000|";

                            //3.เลขประจำตัวผู้เสียภาษีอากรผู้มีหน้าที่หัก ณ ที่จ่าย<TaxNo>
                            if (comcard.card_type.Length == 10)
                                bkData += comcard.comcard_code + "|";
                            else
                                bkData += "0000000000|";

                            //4.เลขที่สาขา ผู้มีหน้าที่หักภาษี ณ ที่จ่าย<BranchID>

                            if (combank.company_code.Length == 4)
                                bkData += combank.company_code + "|";
                            else
                                bkData += "00000|";

                            //5.เลขประจำตัวประชาชนผู้มีเงินได้<CardNo>	
                            if (obj_card.empcard_code.Length == 13)
                                bkData += obj_card.empcard_code + "|";
                            else
                                bkData += "0000000000000|";

                            //6.เลขประจำตัวผู้เสียภาษีอากรผู้มีเงินได้ <TaxNo>
                            if (obj_card.empcard_code.Length == 13)
                                bkData += obj_card.empcard_code + "|";
                            else
                                bkData += "0000000000|";

                            //7.คำนำหน้าชื่อผู้มีเงินได้<InitialNameT>
                            bkData += obj_worker.initial_name_en + "|";

                            //8.ชื่อผู้มีเงินได้<EmpFNameT>				
                            bkData += obj_worker.worker_fname_en + "|";

                            //9.นามสกุลผู้มีเงินได้<EmpLNameT>
                            bkData += obj_worker.worker_lname_en + "|";

                            //10.ที่อยู่ 1<Address>
                            string temp = obj_address.empaddress_no  + obj_address.empaddress_soi + " " + obj_address.empaddress_road + " " + obj_address.empaddress_tambon + " " + obj_address.empaddress_amphur + " " + obj_province.province_name_en;
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
                List<cls_TRCombank> list_combank = objCombank.getDataByFillter(com);
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

                StringBuilder objStr = new StringBuilder();
                foreach (cls_TRTaskwhose whose in listWhose)
                {
                    objStr.Append("'" + whose.worker_code + "',");
                }

                string strEmp = objStr.ToString().Substring(0, objStr.ToString().Length - 1);



                //-- Get worker
                cls_ctMTWorker objWorker = new cls_ctMTWorker();
                List<cls_MTWorker> list_worker = objWorker.getDataMultipleEmp(com, strEmp);

                //-- Step 2 Get Paypf
                cls_ctTRPaypf objPay = new cls_ctTRPaypf();
                List<cls_TRPaypf> list_pf = objPay.getDataMultipleEmp("TH", com, datePay, datePay, strEmp);

                
                //-- Step 3 Get Company acc
                cls_ctTRCombank objCombank = new cls_ctTRCombank();
                List<cls_TRCombank> list_combank = objCombank.getDataByFillter(com);
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
                string[] task_pf_detail = task_detail.taskdetail_process.Split('|');


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

                        string empname = "";

                        cls_MTWorker obj_worker = new cls_MTWorker();                                            
                        cls_TREmpcard obj_pfcard = new cls_TREmpcard();
                        cls_TREmpcard obj_taxcard = new cls_TREmpcard();
                        cls_TREmpdep obj_empdep = new cls_TREmpdep();

                        foreach (cls_MTWorker worker in list_worker)
                        {
                            if (pf.worker_code.Equals(worker.worker_code))
                            {
                                empname = worker.initial_name_en + " " + worker.worker_fname_en + " " + worker.worker_lname_en;
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



                        if (empname.Equals("") || obj_taxcard.empcard_code.Equals("") || obj_pfcard.empcard_code.Equals(""))
                            continue;

                        bkData = bkData + '\r' + '\n';

                        // 1, 2. Record Type, Member ID-Type
                        bkData += "D,EM,";

                        // 3. Member ID
                        bkData += obj_pfcard.empcard_code + ",";

                        // 4. Title Name
                        bkData += obj_worker.initial_name_th + ",";

                        // 5. First Name
                        bkData += obj_worker.worker_fname_th + ",";

                        // 6. Last Name
                        bkData += obj_worker.worker_lname_th + ",";

                        // 7. Position Code
                        bkData += ",";

                        // 8. Location Code
                        bkData += "0" + ",";

                        // 9. Department Code
                        bkData += obj_empdep.empdep_level01 + ",";

                        // 10. Division Code
                        bkData += obj_empdep.empdep_level02 + ",";

                        // 11. Section Code
                        bkData += obj_empdep.empdep_level03 + ",";

                        // 12. Citizen ID
                        bkData += obj_taxcard.empcard_code + ",";

                        // 13. Tax ID
                        bkData += obj_taxcard.empcard_code + ",";

                        // 14. รหัสรูปแบบลงทุน
                        //-- F edit 16/03/2014
                        bkData += "01,";
                        //Detail += txtPatternCode.Text + ",";

                        // 15. รหัสกองทุนย่อย 1
                        //-- F edit 16/03/2014
                        bkData += "SINSA1,";
                        //Detail += txtPFCodeSub.Text + ",";

                        // 16. %เงินลงทุน 1
                        bkData += "100.00,";

                        // 17. เงินสะสม 1 (บาท)
                        Amount = pf.paypf_emp_amount;
                        bkData += Amount.ToString("0.00").Trim() + ",";
                        TotalAmountEmp += Amount;

                        // 17. เงินสมทบ 1 (บาท)
                        Amount = pf.paypf_com_amount;
                        bkData += Amount.ToString("0.00").Trim();
                        TotalAmountComp += Amount;

                        TotalRecord++;
                        index++;
                    }

                    

                    double TotalAmount = TotalAmountEmp + TotalAmountComp;

                    // Header 

                    // 1. Record Type, 2. Batch Reference, 3. Verify Status, 4. Transaction Code
                    string Header = "H3,,,CB,";

                    // 5. Fund Code
                    //Header += txtPFCode.Text.TrimEnd() + ",";
                    Header += task_pf_detail[2] + ",";

                    // 6. Company Code
                    //Header += txtCompCode.Text.TrimEnd() + ",";
                    Header += task_pf_detail[1] + ",";

                    // 7. Total Records
                    Header += TotalRecord.ToString() + ",";

                    // 8. Generate Date
                    Header += DateTime.Now.ToString("dd/MM/yyyy") + ",";

                    // 9. Total Amount
                    Header += TotalAmount.ToString("0.00") + ",";

                    // 10. Fund Manager Code
                    Header += "BBLAM,";

                    // 11. Contribution Group
                    //Header += txtCBGroup.Text.TrimEnd() + ",";
                    Header += "" + ",";

                    // 12. Payment Month
                    //if (PeriodMonthly != string.Empty)
                    //    Header += PeriodMonthly + "/" + Year.Year.ToString() + ",";
                    //else
                    //    Header += PeriodDaily + "/" + Year.Year.ToString() + ",";
                    Header += period.period_no + "/" + datePay.Year.ToString() + ",";


                    // 13. Payment Term
                    Header += "1,";

                    // 14. Payment Date, 15-18
                    Header += datePay.ToString("dd/MM/yyyy") + ",,,,,";

                    // 19. Transaction Flag
                    Header += "New";

                    //int record = list_paytran.Count;

                    tmpData += Header + bkData;

                    try
                    {
                        //-- Step 1 create file
                        string filename = "TRN_PF_" + DateTime.Now.ToString("yyMMddHHmm") + "." + "txt";
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
                List<cls_TRCombank> list_combank = objCombank.getDataByFillter(com);
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

    }
}
