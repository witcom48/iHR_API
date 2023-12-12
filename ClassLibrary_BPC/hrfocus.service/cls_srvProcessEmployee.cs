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
        public string doExportEMP(string com, string taskid )
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
               
                 //-- Get MTEmpStatus
                cls_ctTREmpsalary objEmpsalary = new cls_ctTREmpsalary();
                List<cls_TREmpsalary> list_Empsalary = objEmpsalary.getDataByFillter(com, "");

                //-- Get Empbenefit
                cls_ctTREmpbenefit objEmpbenefit  = new cls_ctTREmpbenefit();
                List<cls_TREmpbenefit> list_Empbenefit = objEmpbenefit.getDataByFillter(com, "");

                //-- Get Empbenefit
                cls_ctTREmpreduce objEmpreduce  = new cls_ctTREmpreduce();
                List<cls_TREmpreduce> list_Empreduce = objEmpreduce.getDataByFillter(com, "");

                //-- Get Empbenefit
                cls_ctTREmpprovident objEmpprovident = new cls_ctTREmpprovident();
                List<cls_TREmpprovident> list_Empprovident = objEmpprovident.getDataByFillter(com, "", "");
                
                
                  //-- Get Empbenefit
                cls_ctMTProvident objMTProvident = new cls_ctMTProvident();
                List<cls_MTProvident> list_MTProvident = objMTProvident.getDataByFillter(com, "", "");

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
                        cls_TREmpsalary bj_TREmpsalary = new cls_TREmpsalary();
                        cls_TREmpbenefit bj_TREmpbenefit = new cls_TREmpbenefit();
                        cls_TREmpreduce bj_TREmpreduce = new cls_TREmpreduce();
                        cls_TREmpprovident bj_TREmpprovident = new cls_TREmpprovident();
                        cls_MTProvident bj_MTProvident = new cls_MTProvident(); 
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

                            //3 คำนำหน้าชื่อ(ไทย)
                            bkData += obj_worker.initial_name_th + "|";

                            //4 ชื่อ(ไทย)
                            bkData +=  obj_worker.worker_fname_th + "|";

                            //5 นามสกุล(ไทย)
                             bkData += obj_worker.worker_lname_th + "|";

                            //6 คำนำหน้าชื่อ(อังกฤษ)
                            bkData += obj_worker.initial_name_en + "|";

                            //7 ชื่อ(อังกฤษ)
                            bkData += obj_worker.worker_fname_en + "|";

                            //8 นามสกุล(อังกฤษ)
                            bkData += obj_worker.worker_lname_en + "|";

                            //9 ชื่อเล่น
                            bkData += " " + "|";

                            //10 เพศ
                             if (obj_worker.worker_gender.Equals("M"))
                            {
                                bkData += "ชาย" + "|";
                            }
                            else if (obj_worker.worker_gender.Equals("F"))
                            {
                                bkData += "หญิง" + "|";
                            }
                             else
                             {
                                 bkData += " " + "|";
                             }
                              
 
                            //11 ประเภทพนักงาน
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

                            //12 สถานะพนักงาน
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
 
                            //13 วันที่เริ่มงาน
                            bkData += obj_worker.worker_hiredate.ToString("dd/MM/yyyy") + "|";
 
 
                            //14 อายุงาน
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

                            //15 จำนวนวันทดลองงาน
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

                            //16 วันที่พ้นทดลองงาน
                            bkData += obj_worker.worker_probationenddate.ToString("dd/MM/yyyy") + "|";

                            //17 วิธีการคำนวณภาษี WORKER_TAXMETHOD
                            if (obj_worker.worker_taxmethod.Equals("1"))
                            {
                                bkData += "พนักงานจ่ายเอง" + "|";
                            }
                            else if (obj_worker.worker_taxmethod.Equals("2"))
                            {
                                bkData += "บริษัทออกให้ครั้งเดียว" + "|";
                            }
                            else if (obj_worker.worker_taxmethod.Equals("3"))
                            {
                                bkData += "บริษัทออกให้ตลอด" + "|";
                            }
                            else
                            {
                                bkData += " " + "|";
                            }
 

                            //18 เงินเดือนล่าสุด
                            foreach (cls_TREmpsalary Empsalary in list_Empsalary)
                            {
                                if (MTWorkers.worker_code.Equals(Empsalary.worker_code))
                                {
                                    bj_TREmpsalary = Empsalary;
                                    break;
                                }
                            }

                            if (bj_TREmpsalary != null)
                            {
                                // แสดงเงินเดือนล่าสุด
                                bkData += bj_TREmpsalary.empsalary_amount + "|";
                            }
                            else
                            {
                                // ไม่พบข้อมูลเงินเดือน
                                bkData += "" + "|"; 
                            }

                            //19 สวัสดิการ (ผลรวม)cls_TREmpbenefit
                            double totalEmpBenefitAmount = 0;  

                            foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                            {
                                if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code))
                                {
                                    totalEmpBenefitAmount += (double)TREmpbenefit.empbenefit_amount;
                                }
                            }

                            decimal resultBenefit = (decimal)totalEmpBenefitAmount;
                            bkData += resultBenefit + "|"; 


                            //20 ค่าลดหย่อน(ผลรวม)

                            double totalEmpreduceAmount = 0;
                            foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                            {
                                if (MTWorkers.worker_code.Equals(TREmpreduce.worker_code))
                                {
                                    totalEmpreduceAmount += (double)TREmpreduce.empreduce_amount;
                                }
                            }
                            decimal resultEmpreduce = (decimal)totalEmpreduceAmount;
                            bkData += resultEmpreduce + "|";


                            //21นโยบายกองทุน (ล่าสุด)cls_MTProvident bj_MTProvident

                            bool foundData = false; 
                            foreach (cls_MTWorker worker in list_worker)
                            {
                                if (MTWorkers.worker_code.Equals(worker.worker_code))
                                {
                                    foreach (cls_TREmpprovident Provident in list_Empprovident)
                                    {
                                        if (worker.worker_code != null && worker.worker_code.Equals(Provident.worker_code))
                                        {
                                            foreach (cls_MTProvident Empprovident in list_MTProvident)
                                            {
                                                if (Provident.provident_code.Equals(Empprovident.provident_code))
                                                {
                                                    bkData += Empprovident.provident_name_th + "|";
                                                    foundData = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            if (!foundData)
                            {
                                bkData += "" + "|"; 
                            }


                            //22 สถานะลาออก
                            bkData += obj_worker.worker_resignstatus + "|";

                            //23 วันที่ลาออก
                            if (obj_worker.worker_resignstatus)  
                            {
                                bkData += obj_worker.worker_resigndate.ToString("dd/MM/yyyy") + "|"; 
                            }
                            else
                            {
                                bkData += "" + "|";  
                            }

                            //24 สาเหตุการลาออก
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

                            //25 รายละเอียดการลาออก
                            bkData += "" + "|";

                             //26 ระดับ01
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

                            //27 ระดับ02
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

                            //28 ระดับ03
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
                           
                            //29 ตำแหน่งปัจจุบัน Position
                            cls_MTPosition bj_Position1 = null;
                            bool foundPosition  = false;

                            foreach (cls_TREmpposition emppos in list_TRpos)
                            {
                                if (MTWorkers.worker_code.Equals(emppos.worker_code))
                                {
                                    foreach (cls_MTPosition pos1 in list_MTPosition)
                                    {
                                        if (emppos.empposition_position != null && emppos.empposition_position.Equals(pos1.position_code))
                                        {
                                            bj_Position1 = pos1;
                                            bkData += bj_Position1.position_name_th + "|";
                                            foundPosition = true;
                                            break;
                                        }
                                    }
                                }
                            }

                            if (!foundPosition)
                            {
                                bkData += " " + "|";
                            }

                            //30 วันที่รับตำแหน่ง
                            bkData += obj_workerpos.empposition_date.ToString("dd/MM/yyyy") + "|";
 
                            //31เหตุผลการปรับตำแหน่งงาน
                            cls_MTReason bj_MTReason1 = null;
                            bool foundMTReason = false;

                            foreach (cls_TREmpposition pos in list_TRpos)
                            {
                                if (MTWorkers.worker_code.Equals(pos.worker_code))
                                {
                                    foreach (cls_MTReason pos1 in list_TReason)
                                    {
                                        if (pos.empposition_reason != null && pos.empposition_reason.Equals(pos1.reason_code))
                                        {
                                            bj_MTReason1 = pos1;
                                            bkData += bj_MTReason1.reason_name_th + "|";
                                            foundMTReason = true;
                                            break;
                                        }
                                    }
                                }
                            }

                            if (!foundMTReason)
                            {
                                bkData += " " + "|";
                            }

                            //32 วันเกิด
                            bkData += obj_worker.worker_birthdate.ToString("dd/MM/yyyy") + "|";

                            //33 อายุ
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

                            //34 เลขที่บัตรประชาชน
                            foreach (cls_TREmpcard card in list_empcard)
                            {
                                if (MTWorkers.worker_code.Equals(card.worker_code) && card.card_type.Equals("NTID"))
                                {
                                    obj_empcard = card;
                                    break;
                                }
                            }
                            bkData += obj_empcard.empcard_code + "|";
 
                            //35 บัตรประชาชนหมดอายุ
                            bkData += obj_empcard.empcard_expire.ToString("dd/MM/yyyy") + "|";

                            //36 เลขที่บัตรประกันสังคม
                            foreach (cls_TREmpcard card in list_empcard)
                            {
                                if (MTWorkers.worker_code.Equals(card.worker_code) && card.card_type.Equals("SSO"))
                                {
                                    obj_empcardsso = card;
                                    break;
                                }
                            }
                            bkData += obj_empcardsso.empcard_code + "|";

                            //37 บัตรประกันสังคมหมดอายุ
                            bkData += obj_empcardsso.empcard_expire.ToString("dd/MM/yyyy") + "|";

                            //38 เลขที่หนังสือเดินทาง
                            foreach (cls_TREmpcard card in list_empcard)
                            {
                                if (MTWorkers.worker_code.Equals(card.worker_code) && card.card_type.Equals("PAS"))
                                {
                                    obj_empcardpas = card;
                                    break;
                                }
                            }
                            bkData += obj_empcardpas.empcard_code + "|";

                              //39 หนังสือเดินทางหมดอายุ
                            bkData += obj_empcardpas.empcard_expire.ToString("dd/MM/yyyy") + "|";


                            foreach (cls_TREmpaddress address in list_empaddress)
                            {
                                if (MTWorkers.worker_code.Equals(address.worker_code))
                                {
                                    obj_empaddress = address;
                                    break;
                                }
                            }

                            //40 ที่อยู่ปัจจุบัน1
                            bkData += obj_empaddress.empaddress_no + "|";
                             //41 หมู่1
                            bkData += obj_empaddress.empaddress_moo + "|";

                            //42 ซอย1
                            bkData += obj_empaddress.empaddress_soi + "|";

                            //43 ถนน1
                            bkData += obj_empaddress.empaddress_road + "|";
        
                            //44 ตำบล1
                            bkData += obj_empaddress.empaddress_tambon + "|";

                            //45 อำเภอ1
                            bkData += obj_empaddress.empaddress_amphur + "|";

                            //46 จังหวัด1
                            foreach (cls_MTProvince province in list_province)
                            {
                                if (obj_empaddress.province_code.Equals(province.province_code))
                                {
                                    obj_province = province;
                                    break;
                                }
                            }
                            bkData += obj_province.province_code + "|";


                            //47 รหัสไปรษณีย์์1
                            bkData += obj_empaddress.empaddress_zipcode + "|";

                            //48 โทรศัพท์1
                            bkData += obj_empaddress.empaddress_tel + "|";

                            //49 โทรสาร1
                            bkData += " " + "|";

                            //50 EMail1v
                            bkData += obj_empaddress.empaddress_email + "|";


                            foreach (cls_TREmpbank empbank in list_empbank)
                            {
                                if (MTWorkers.worker_code.Equals(empbank.worker_code))
                                {
                                    obj_empbank = empbank;

                                     break;
                                }
                            }
                            //51 เลขที่บัญชีพนักงาน
                            bkData += obj_empbank.empbank_bankaccount + "|";

                            //52 ชื่อบัญชี
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
                        dataTable.Columns.AddRange(new DataColumn[53] { new DataColumn("รหัสพนักงาน"), new DataColumn("รหัสบัตร"), new DataColumn("คำนำหน้าชื่อ(ไทย)"), new DataColumn("ชื่อ(ไทย)"), new DataColumn("นามสกุล(ไทย)"), new DataColumn("คำนำหน้าชื่อ(อังกฤษ)"),
                                                                      new DataColumn("ชื่อ(อังกฤษ)"), new DataColumn("นามสกุล(อังกฤษ)"), new DataColumn("ชื่อเล่น"), new DataColumn("เพศ"), 
                                                                      new DataColumn("ประเภทพนักงาน"), new DataColumn("สถานะพนักงาน"), new DataColumn("วันที่เริ่มงาน"), new DataColumn("อายุงาน"), new DataColumn("จำนวนวันทดลองงาน"), new DataColumn("วันที่พ้นทดลองงาน"),
                                                                      new DataColumn("วิธีการคำนวณภาษี"),new DataColumn("เงินเดือน"),new DataColumn("สวัสดิการ"),new DataColumn("ค่าลดหย่อน"),new DataColumn("นโยบายกองทุน"),
                                                                      new DataColumn("สถานะลาออก"), new DataColumn("วันที่ลาออก"), new DataColumn("สาเหตุการลาออก"), new DataColumn("รายละเอียดการลาออก"), new DataColumn("ระดับ01"), new DataColumn("ระดับ02"), 
                                                                      new DataColumn("ระดับ03"), new DataColumn("ตำแหน่งปัจจุบัน"),new DataColumn("วันที่รับตำแหน่ง"), new DataColumn("เหตุผลการปรับตำแหน่งงาน"), new DataColumn("วันเกิด"), new DataColumn("อายุ"), 
                                                                      new DataColumn("เลขที่บัตรประชาชน"), new DataColumn("บัตรประชาชนหมดอายุ"), new DataColumn("เลขที่บัตรประกันสังคม"), new DataColumn("บัตรประกันสังคมหมดอายุ"), new DataColumn("เลขที่หนังสือเดินทาง"), new DataColumn("หนังสือเดินทางหมดอายุ"),
                                                                      new DataColumn("ที่อยู่ปัจจุบัน1"), new DataColumn("หมู่1"), new DataColumn("ซอย1"), new DataColumn("ถนน1"), new DataColumn("ตำบล1"), new DataColumn("อำเภอ1"), 
                                                                      new DataColumn("จังหวัด1"), new DataColumn("รหัสไปรษณีย์1"), new DataColumn("โทรศัพท์1"), new DataColumn("โทรสาร1"), new DataColumn("EMail1"), new DataColumn("เลขที่บัญชีพนักงาน"), new DataColumn("ชื่อบัญชี"), new DataColumn(" ")});
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
        
    }
///
 
}
