using ClassLibrary_BPC.hrfocus.controller;
using ClassLibrary_BPC.hrfocus.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.service
{
    public class cls_srvProcessEmployee
    {

        public string doChangeWorkerCode(string company_code, string worker_code, string new_code)
        {
            string strResult = "";

            cls_ctConnection obj_conn = new cls_ctConnection();

            try
            {
               
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_conn.doConnect();
                //obj_conn.doOpenTransaction();

                obj_str.Append(" EXEC [dbo].[HRM_PRO_CHANGEWORKERCODE] '" + company_code + "', '" + worker_code + "', '" + new_code + "' ");

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
               
                obj_cmd.CommandType = CommandType.Text;

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


         //golf 07/12/2023
        public string doExportEMP(string com, string taskid)
        {
            string strResult = "";
            cls_ctMTTask objMTTask = new cls_ctMTTask();
            List<cls_MTTask> listMTTask = objMTTask.getDataByFillter(com, taskid, "EMP_TIME", "");
            List<string> listError = new List<string>();
            if (listMTTask.Count > 0)
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
 
                //-- Get  Position
                cls_ctMTPosition objPosition = new cls_ctMTPosition();
                List<cls_MTPosition> list_TRPosition = objPosition.getDataByFillter("", "", "");
                 
                //-- Get Emp card
                cls_ctTREmpcard objEmpcard = new cls_ctTREmpcard();
                List<cls_TREmpcard> list_empcard = objEmpcard.getDataEmp(com, strEmp );

                //-- Get Emp address
                cls_ctTREmpaddress objEmpadd = new cls_ctTREmpaddress();
                List<cls_TREmpaddress> list_empaddress = objEmpadd.getDataMultipleEmp(com, strEmp);

                //-- Get Emp acc
                cls_ctTREmpbank objEmpbank = new cls_ctTREmpbank();
                List<cls_TREmpbank> list_empbank = objEmpbank.getDataMultipleEmp(com, strEmp);
                
                //-- Get Province
                cls_ctMTProvince objProvince = new cls_ctMTProvince();
                List<cls_MTProvince> list_province = objProvince.getDataByFillter("", "");

                //-- Get Reason
                cls_ctMTReason objMTReason = new cls_ctMTReason();
                List<cls_MTReason> list_TReason = objMTReason.getDatareson("", "");

                //-- Get MTPosition
                cls_ctMTPosition objMTPosition = new cls_ctMTPosition();
                List<cls_MTPosition> list_MTPosition = objMTPosition.getDataByFillter(com, "", "");

                //-- Get MTDep 
                cls_ctMTDep objTDep = new cls_ctMTDep();
                List<cls_MTDep> list_TDep= objTDep.getDataByFillter(com, "", "", "", "");

                //-- Get MTEmpStatus
                cls_ctMTEmpStatus objMTEmpStatus = new cls_ctMTEmpStatus();
                List<cls_MTEmpStatus> list_MTEmpStatus = objMTEmpStatus.getDataByFillter("", "");
               
                
                string tmpData = "";
                if (list_worker.Count > 0)
                {
                    string bkData;

                    foreach (cls_MTWorker MTWorkers in list_worker)
                    {
                        string empname = "";

                        cls_MTWorker obj_worker = new cls_MTWorker();
                        cls_TREmpdep obj_workerdep = new cls_TREmpdep();
                        cls_TREmpdep obj_workerdep1 = new cls_TREmpdep();
                        cls_TREmpdep obj_workerdep2 = new cls_TREmpdep();
                        cls_TREmpdep obj_workerdep3 = new cls_TREmpdep();
                        cls_TREmpposition obj_workerpos = new cls_TREmpposition();
                        cls_TRTimeleave obj_timeleave = new cls_TRTimeleave();
                        cls_TREmpaddress obj_empaddress = new cls_TREmpaddress();
                        cls_TREmpbank obj_empbank = new cls_TREmpbank();
                        cls_TREmpcard obj_empcard = new cls_TREmpcard();
                        cls_TREmpcard obj_empcardsso = new cls_TREmpcard();
                        cls_TREmpcard obj_empcardpas = new cls_TREmpcard();
                        cls_MTProvince obj_province = new cls_MTProvince();
                        cls_TREmpaddress obj_address = new cls_TREmpaddress();
                        cls_MTPosition obj_MTPosition = new cls_MTPosition();
                        cls_MTReason bj_MTReason = new cls_MTReason();
                        cls_MTEmpStatus bj_MTEmpStatus = new cls_MTEmpStatus();

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

                        if (empname.Equals(""))
                            continue;

                        if (list_worker.Count > 0)

                        {

                            //1 รหัสพนักงาน
                            bkData = obj_worker.worker_code + "|";

                            //2 รหัสบัตร
                            bkData += obj_worker.worker_card + "|";

                            //3 คำนำหน้าชื่อ
                            bkData += obj_worker.initial_name_th + "|";

                            //4 ชื่อ
                            bkData +=  obj_worker.worker_fname_th + "|";
 
                            //5 นามสกุล
                             bkData += obj_worker.worker_lname_th + "|";

                            //6 ชื่อเล่น
                            bkData += " " + "|";
 
                            //7 ประเภทพนักงาน
                            if (obj_worker.worker_emptype.Equals("M"))
                            {
                                bkData += "รายเดือน" + "|";
                            }
                            else if (obj_worker.worker_emptype.Equals("D"))
                            {
                                bkData += "รายวัน" + "|";
                            }
                            else
                            {
                                bkData += " " + "|";
                            }

                            //8 สถานะพนักงาน
                            cls_MTEmpStatus bj_MTEmpStatus1 = null; // สร้างตัวแปร bj_MTEmpStatus1 เพื่อให้มีค่าเริ่มต้นเป็น null
                            cls_MTWorker obj_worker4 = null; // สร้างตัวแปร obj_worker4 เพื่อให้มีค่าเริ่มต้นเป็น null

                            bool foundStatus = false; // สร้างตัวแปรตรวจสอบว่ามีเหตุผลที่เลิกจ้างหรือไม่

                            foreach (cls_MTWorker worker4 in list_worker)
                            {
                                if (MTWorkers.worker_code.Equals(worker4.worker_code))
                                {
                                    foreach (cls_MTEmpStatus MTEmpStatus in list_MTEmpStatus)
                                    {
                                        if (worker4.worker_empstatus != null && worker4.worker_empstatus.Equals(MTEmpStatus.empstatus_code))
                                        {
                                            bj_MTEmpStatus1 = MTEmpStatus;
                                            bkData += bj_MTEmpStatus1.empstatus_name_th + "|";
                                            foundStatus = true;
                                            break;
                                        }
                                    }

                                    if (!foundStatus)
                                    {
                                        if (obj_worker4 != null)
                                        {
                                            bkData += obj_worker4.worker_empstatus_name + "|";
                                        }
                                        else
                                        {
                                            bkData += " " + " |";
                                        }
                                    }

                                    obj_worker4 = worker4; 
                                    break;
                                }
                            }


 
                            //9 วันที่เริ่มงาน
                            bkData += obj_worker.worker_hiredate.ToString("dd/MM/yyyy") + "|";
 
 
                            //10 อายุงาน
                            //
                            DateTime hireDate = obj_worker.worker_hiredate;
                            DateTime currentDate = DateTime.Now;
                            int yearsWorked = currentDate.Year - hireDate.Year;
                            if (currentDate.Month < hireDate.Month || (currentDate.Month == hireDate.Month && currentDate.Day < hireDate.Day))
                            {
                                yearsWorked--;
                            }
                            TimeSpan timeWorked = currentDate - hireDate;
                            int daysWorked1 = timeWorked.Days;
                            DateTime anniversaryThisYear = hireDate.AddYears(yearsWorked);
                            int monthsWorked = 0;
                            while (anniversaryThisYear.AddMonths(monthsWorked) <= currentDate)
                            {
                                monthsWorked++;
                            }
                            monthsWorked--;  
                            bkData += yearsWorked + "y" + monthsWorked + "m" + " |";
                            //                            

                            //11 จำนวนวันทดลองงาน
                            DateTime probationStartDate = obj_worker.worker_probationdate; // วันที่เริ่มต้น
                            DateTime probationEndDate = obj_worker.worker_probationenddate; // วันที่สิ้นสุด

                            TimeSpan totalProbationPeriod = probationEndDate - probationStartDate; // หาความแตกต่างระหว่างวันที่เริ่มต้นและสิ้นสุดของการทดลองงาน
                            int daysInProbation = totalProbationPeriod.Days; // หาจำนวนวันที่ผ่านมา

                            if (daysInProbation > 0)
                            {
                                bkData += daysInProbation + "|";
                            }
                            else
                            {
                                bkData += "" + "|";
                            }



                            //12 วันที่พ้นทดลองงาน
                            bkData += obj_worker.worker_probationenddate.ToString("dd/MM/yyyy") + "|";

                            //13 สถานะลาออก
                            bkData += obj_worker.worker_resignstatus + "|";

                            //14 วันที่ลาออก
                            if (obj_worker.worker_resignstatus)  
                            {
                                bkData += obj_worker.worker_resigndate.ToString("dd/MM/yyyy") + "|"; 
                            }
                            else
                            {
                                bkData += "" + "|";  
                            }

                            //15 สาเหตุการลาออก
                            cls_MTReason bj_MTReasons1 = null; 
                            cls_MTWorker obj_worker1 = null; 

                            foreach (cls_MTWorker worker in list_worker)
                            {
                                if (MTWorkers.worker_code.Equals(worker.worker_code))
                                {
                                    bool foundReason = false; 

                                    foreach (cls_MTReason pos1 in list_TReason)
                                    {
                                        if (worker.worker_resignreason != null && worker.worker_resignreason.Equals(pos1.reason_code))
                                        {
                                            bj_MTReasons1 = pos1; 
                                            bkData += bj_MTReasons1.reason_name_th + "|";
                                            foundReason = true; 
                                            break; 
                                        }
                                    }

                                    if (!foundReason)
                                    {
                                        if (obj_worker1 != null)
                                        {
                                            bkData += obj_worker1.worker_resignreason + "|"; 
                                        }
                                        else
                                        {
                                            bkData += "-" + " |"; 
                                        }
                                    }

                                    obj_worker1 = worker; 
                                    break; 
                                }
                            }




                            //16 รายละเอียดการลาออก
                            bkData += "" + "|";

                             //17 ระดับ01

                            cls_MTDep bj_MTDep1 = null; 
                            cls_MTWorker cls_TREmpdep1 = null; 

                            foreach (cls_TREmpdep dep1 in list_TRdep)
                            {
                                if (MTWorkers.worker_code.Equals(dep1.worker_code))
                                {
                                    bool foundDep1 = false;

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

                                    obj_workerdep1 = dep1; 

                                    if (!foundDep1)
                                    {
                                        if (cls_TREmpdep1 != null)
                                        {
                                            bkData += obj_workerdep2.empdep_level01 + "|"; 
                                        }
                                        else
                                        {
                                            bkData += " " + " |";
                                        }
                                    }

                                    break; 
                                }
                            }
                           
                            //18 ระดับ02
                            cls_MTDep bj_MTDep2 = null;
                            cls_MTWorker cls_TREmpdep2 = null; 

                            foreach (cls_TREmpdep dep2 in list_TRdep)
                            {
                                if (MTWorkers.worker_code.Equals(dep2.worker_code))
                                {
                                    bool foundDep2 = false;

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

                                    obj_workerdep2 = dep2; 

                                    if (!foundDep2)
                                    {
                                        if (cls_TREmpdep2 != null)
                                        {
                                            bkData += obj_workerdep2.empdep_level02 + "|"; 
                                        }
                                        else
                                        {
                                            bkData += " " + " |";
                                        }
                                    }
                                    break; 
                                }
                            }

                            //19 ระดับ03
                            //
                            cls_MTDep bj_MTDep3 = null; 
                            cls_MTWorker cls_TREmpdep3 = null; 

                            foreach (cls_TREmpdep dep3 in list_TRdep)
                            {
                                if (MTWorkers.worker_code.Equals(dep3.worker_code))
                                {
                                    bool foundDep3 = false; 

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

                                    obj_workerdep3 = dep3; 

                                    if (!foundDep3)
                                    {
                                        if (cls_TREmpdep3 != null)
                                        {
                                            bkData += obj_workerdep2.empdep_level03 + "|"; 
                                        }
                                        else
                                        {
                                            bkData += " " + " |";
                                        }
                                    }

                                    break; 
                                }
                            }

                           
                            //20 ตำแหน่งปัจจุบัน Position
                            cls_MTPosition bj_Position1 = null; 
 
                            foreach (cls_TREmpposition emppos in list_TRpos)
                            {
                                if (MTWorkers.worker_code.Equals(emppos.worker_code))
                                {
                                    foreach (cls_MTPosition pos1 in list_MTPosition)
                                    {
                                        if (emppos.empposition_position.Equals(pos1.position_code))
                                        {
                                            bj_Position1 = pos1; 
                                            break; 
                                        }
                                    }
                                    obj_workerpos = emppos; 
                                    break; 
                                }
                            }

                            bkData += bj_Position1.position_name_th + "|";
 
                            //21 วันที่รับตำแหน่ง
                            bkData += obj_workerpos.empposition_date.ToString("dd/MM/yyyy") + "|";
 
                            //22 เหตุผลการปรับตำแหน่งงาน
                            cls_MTReason bj_MTReason1 = null; 
                            cls_TREmpposition obj_workerpos1 = null; 

                            foreach (cls_TREmpposition pos in list_TRpos)
                            {
                                if (MTWorkers.worker_code.Equals(pos.worker_code))
                                {
                                    foreach (cls_MTReason pos1 in list_TReason)
                                    {
                                        if (pos.empposition_reason.Equals(pos1.reason_code))
                                        {
                                            bj_MTReason1 = pos1; 
                                            break; 
                                        }
                                    }

                                    obj_workerpos1 = pos;
                                    break;
                                }
                            }
                            bkData += bj_MTReason1.reason_name_th + "|";

                            //23 วันเกิด
                            bkData += obj_worker.worker_birthdate.ToString("dd/MM/yyyy") + "|";

                            //24 อายุ
                            DateTime workerBirthdate = obj_worker.worker_birthdate;
                            DateTime currentDate1 = DateTime.Today;

                            int ageYears = currentDate1.Year - workerBirthdate.Year;
                            int ageMonths = currentDate1.Month - workerBirthdate.Month;
                             if (currentDate1.Month < workerBirthdate.Month || (currentDate1.Month == workerBirthdate.Month && currentDate1.Day < workerBirthdate.Day))
                            {
                                ageYears--;  
                                if (ageMonths < 0)
                                {
                                    ageMonths += 12; 
                                }
                            }
                             bkData += ageYears + "y" + ageMonths + "m" + " |";

                            //25 เลขที่บัตรประชาชน
                            foreach (cls_TREmpcard card in list_empcard)
                            {
                                if (MTWorkers.worker_code.Equals(card.worker_code) && card.card_type.Equals("NTID"))
                                {
                                    obj_empcard = card;
                                    break;
                                }
                            }
                            bkData += obj_empcard.empcard_code + "|";
 
                            //26 บัตรประชาชนหมดอายุ
                            bkData += obj_empcard.empcard_expire.ToString("dd/MM/yyyy") + "|";

                            //27 เลขที่บัตรประกันสังคม
                            foreach (cls_TREmpcard card in list_empcard)
                            {
                                if (MTWorkers.worker_code.Equals(card.worker_code) && card.card_type.Equals("SSO"))
                                {
                                    obj_empcardsso = card;
                                    break;
                                }
                            }
                            bkData += obj_empcardsso.empcard_code + "|";

                            //28 บัตรประกันสังคมหมดอายุ
                            bkData += obj_empcardsso.empcard_expire.ToString("dd/MM/yyyy") + "|";

                            //29 เลขที่หนังสือเดินทาง
                            foreach (cls_TREmpcard card in list_empcard)
                            {
                                if (MTWorkers.worker_code.Equals(card.worker_code) && card.card_type.Equals("PAS"))
                                {
                                    obj_empcardpas = card;
                                    break;
                                }
                            }
                            bkData += obj_empcardpas.empcard_code + "|";

                              //30 หนังสือเดินทางหมดอายุ
                            bkData += obj_empcardpas.empcard_expire.ToString("dd/MM/yyyy") + "|";


                            foreach (cls_TREmpaddress address in list_empaddress)
                            {
                                if (MTWorkers.worker_code.Equals(address.worker_code))
                                {
                                    obj_empaddress = address;
                                    break;
                                }
                            }



                            //31 ที่อยู่ปัจจุบัน1
                            bkData += obj_empaddress.empaddress_no + "|";
                             //32 หมู่1
                            bkData += obj_empaddress.empaddress_moo + "|";

                            //33 ซอย1
                            bkData += obj_empaddress.empaddress_soi + "|";

                            //34 ถนน1
                            bkData += obj_empaddress.empaddress_road + "|";
        
                            //35 ตำบล1
                            bkData += obj_empaddress.empaddress_tambon + "|";

                            //36 อำเภอ1
                            bkData += obj_empaddress.empaddress_amphur + "|";

                            //37 จังหวัด1
                            foreach (cls_MTProvince province in list_province)
                            {
                                if (obj_empaddress.province_code.Equals(province.province_code))
                                {
                                    obj_province = province;
                                    break;
                                }
                            }
                            bkData += obj_province.province_code + "|";


                            //38 รหัสไปรษณีย์์1
                            bkData += obj_empaddress.empaddress_zipcode + "|";

                            //39 โทรศัพท์1
                            bkData += obj_empaddress.empaddress_tel + "|";

                            //40 โทรสาร1
                            bkData += " " + "|";

                            //41 EMail1v
                            bkData += obj_empaddress.empaddress_email + "|";


                            foreach (cls_TREmpbank empbank in list_empbank)
                            {
                                if (MTWorkers.worker_code.Equals(empbank.worker_code))
                                {
                                    obj_empbank = empbank;

                                     break;
                                }
                            }


                            //42 เลขที่บัญชีพนักงาน
                            bkData += obj_empbank.empbank_bankaccount + "|";

                            //43 ชื่อบัญชี
                            bkData += obj_empbank.empbank_bankname + "|";


                            tmpData += bkData + '\r' + '\n';
                        }
                        
                    }

                    int record = list_worker.Count;

                    try
                    {
                        //-- Step 1 create file
                        string filename = "TRN_EMP" + DateTime.Now.ToString("yyMMddHHmm") + "." + "xls";
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
                        dataTable.Columns.AddRange(new DataColumn[43] { new DataColumn("รหัสพนักงาน"), new DataColumn("รหัสบัตร"), new DataColumn("คำนำหน้าชื่อ"), new DataColumn("ชื่อ"), new DataColumn("นามสกุล"), new DataColumn("ชื่อเล่น"), 
                                                                      new DataColumn("ประเภทพนักงาน"), new DataColumn("สถานะพนักงาน"), new DataColumn("วันที่เริ่มงาน"), new DataColumn("อายุงาน"), new DataColumn("จำนวนวันทดลองงาน"), new DataColumn("วันที่พ้นทดลองงาน"), 
                                                                      new DataColumn("สถานะลาออก"), new DataColumn("วันที่ลาออก"), new DataColumn("สาเหตุการลาออก"), new DataColumn("รายละเอียดการลาออก"), new DataColumn("ระดับ01"), new DataColumn("ระดับ02"), 
                                                                      new DataColumn("ระดับ03"), new DataColumn("ตำแหน่งปัจจุบัน"),new DataColumn("วันที่รับตำแหน่ง"), new DataColumn("เหตุผลการปรับตำแหน่งงาน"), new DataColumn("วันเกิด"), new DataColumn("อายุ"), 
                                                                      new DataColumn("เลขที่บัตรประชาชน"), new DataColumn("บัตรประชาชนหมดอายุ"), new DataColumn("เลขที่บัตรประกันสังคม"), new DataColumn("บัตรประกันสังคมหมดอายุ"), new DataColumn("เลขที่หนังสือเดินทาง"), new DataColumn("หนังสือเดินทางหมดอายุ"),
                                                                      new DataColumn("ที่อยู่ปัจจุบัน1"), new DataColumn("หมู่1"), new DataColumn("ซอย1"), new DataColumn("ถนน1"), new DataColumn("ตำบล1"), new DataColumn("อำเภอ1"), 
                                                                      new DataColumn("จังหวัด1"), new DataColumn("รหัสไปรษณีย์1"), new DataColumn("โทรศัพท์1"), new DataColumn("โทรสาร1"), new DataColumn("EMail1"), new DataColumn("เลขที่บัญชีพนักงาน"), new DataColumn("ชื่อบัญชี")});
                        foreach (var i in data)
                        {
                            if (i.Equals(""))
                                continue;
                            string[] array = i.Split('|');
                            dataTable.Rows.Add(array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7], array[8], array[9], array[10], array[11], array[12], array[13], array[14], array[15], array[16], array[17], array[18], array[19], array[20]
                                                       , array[21], array[22], array[23], array[24], array[25], array[26], array[27], array[28], array[29], array[30], array[31], array[32], array[33], array[34], array[35], array[36], array[37], array[38], array[39], array[40]
                                                       , array[41], array[42]);
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
        
    }
///
 
}
