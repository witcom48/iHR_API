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
            string emperror = "";
            string strResult = "";
            cls_ctMTTask objMTTask = new cls_ctMTTask();
            List<cls_MTTask> listMTTask = objMTTask.getDataByFillter(com, taskid, "EMP_TIME", "");
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

                    //-- Get Emp card
                    cls_ctTREmpcard objEmpcard = new cls_ctTREmpcard();
                    List<cls_TREmpcard> list_empcard = objEmpcard.getDataEmp(com, strEmp);

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
                    List<cls_MTDep> list_TDep = objTDep.getDataByFillter(com, "", "", "", "");

                    //-- Get MTEmpStatus
                    cls_ctMTEmpStatus objMTEmpStatus = new cls_ctMTEmpStatus();
                    List<cls_MTEmpStatus> list_MTEmpStatus = objMTEmpStatus.getDataByFillter("", "");

                    //-- Get MTEmpStatus
                    cls_ctTREmpsalary objEmpsalary = new cls_ctTREmpsalary();
                    List<cls_TREmpsalary> list_Empsalary = objEmpsalary.getDataByFillter(com, "");

                    //-- Get Empbenefit
                    cls_ctTREmpbenefit objEmpbenefit = new cls_ctTREmpbenefit();
                    List<cls_TREmpbenefit> list_Empbenefit = objEmpbenefit.getDataByFillter(com, "");

                    //-- Get Empbenefit
                    cls_ctTREmpreduce objEmpreduce = new cls_ctTREmpreduce();
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
                            emperror = MTWorkers.worker_code;
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
                                bkData += obj_worker.worker_fname_th + "|";

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

                               
                                //
                                //53 สวัสดิการ (ผลรวม)cls_TREmpbenefit
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
                                //53.1สวัสดิการ

                                bool hasACCData = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("ACC"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ ACC ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasACCData = true;
                                    }
                                }
                                if (!hasACCData)
                                {
                                    bkData += "|";
                                }
                                //53.2สวัสดิการ
                                bool hasCAData = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("CA"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ CA ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasCAData = true;
                                    }
                                }
                                if (!hasCAData)
                                {
                                    bkData += "|";
                                }
                                //
                                //535.3สวัสดิการ
                                bool hasCHData = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("CH"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ CH ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasCHData = true;
                                    }
                                }
                                if (!hasCHData)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.4สวัสดิการ
                                bool hasCommutingExpatData = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("CommutingExpat"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ CommutingExpat ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasCommutingExpatData = true;
                                    }
                                }
                                if (!hasCommutingExpatData)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.5สวัสดิการ
                                bool hasEAData = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("EA"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ EA ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasEAData = true;
                                    }
                                }
                                if (!hasEAData)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.6สวัสดิการ
                                bool hasFAData = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("FA"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ FA ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasFAData = true;
                                    }
                                }
                                if (!hasFAData)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.7สวัสดิการ
                                bool hasHAData = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("HA"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ HA ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasHAData = true;
                                    }
                                }
                                if (!hasHAData)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.8สวัสดิการ
                                bool hasHA1Data = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("HA1"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ HA1 ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasHA1Data = true;
                                    }
                                }
                                if (!hasHA1Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.9สวัสดิการ
                                bool hasHA2Data = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("HA2"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ HA2 ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasHA2Data = true;
                                    }
                                }
                                if (!hasHA2Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.10สวัสดิการ
                                bool hasJAData = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("JA"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ JA ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasJAData = true;
                                    }
                                }
                                if (!hasJAData)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.11สวัสดิการ
                                bool hasJLPTData = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("JLPT"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ JLPT ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasJLPTData = true;
                                    }
                                }
                                if (!hasJLPTData)
                                {
                                    bkData += "|";
                                }
                                //\
                                //53.12สวัสดิการ
                                bool hasJPNData = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("JPN"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ JPN ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasJPNData = true;
                                    }
                                }
                                if (!hasJPNData)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.13สวัสดิการ
                                bool hasMAData = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("MA"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ MA ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasMAData = true;
                                    }
                                }
                                if (!hasMAData)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.14สวัสดิการ
                                bool hasPAData = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("PA"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ PA ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasPAData = true;
                                    }
                                }
                                if (!hasPAData)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.15สวัสดิการ
                                bool hasSLF1Data = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("SLF1"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ SLF1 ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasSLF1Data = true;
                                    }
                                }
                                if (!hasSLF1Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.16สวัสดิการ
                                bool hasSS1Data = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("SS1"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ SS1 ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasSS1Data = true;
                                    }
                                }
                                if (!hasSS1Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.17สวัสดิการ
                                bool hasAC1Data = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("AC1"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ AC1 ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasAC1Data = true;
                                    }
                                }
                                if (!hasAC1Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.18สวัสดิการ
                                bool hasALData = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("AL"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ AL ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasALData = true;
                                    }
                                }
                                if (!hasALData)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.19สวัสดิการ
                                bool hasAV1Data = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("AV1"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ AV1 ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasAV1Data = true;
                                    }
                                }
                                if (!hasAV1Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.20สวัสดิการ
                                bool hasBOData = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("BO"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ BO ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasBOData = true;
                                    }
                                }
                                if (!hasBOData)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.21สวัสดิการ
                                bool hasCOMData = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("COM"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ COM ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasCOMData = true;
                                    }
                                }
                                if (!hasCOMData)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.22	INCOME_ACCสวัสดิการ
                                bool hasINCOME_ACCData = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("INCOME_ACC"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ INCOME_ACC ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasINCOME_ACCData = true;
                                    }
                                }
                                if (!hasINCOME_ACCData)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.23	INTAXสวัสดิการ
                                bool hasINTAXData = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("INTAX"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ INTAX ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasINTAXData = true;
                                    }
                                }
                                if (!hasINTAXData)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.24 	OAสวัสดิการ
                                bool hasOAData = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("OA"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ OA ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasOAData = true;
                                    }
                                }
                                if (!hasOAData)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.25	OC1สวัสดิการ
                                bool hasOC1Data = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("OC1"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ OC1 ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasOC1Data = true;
                                    }
                                }
                                if (!hasOC1Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.26สวัสดิการ
                                bool hasOIData = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("OI"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ OI ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasOIData = true;
                                    }
                                }
                                if (!hasOIData)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.27สวัสดิการ
                                bool hasOSData = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("OS"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ OS ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasOSData = true;
                                    }
                                }
                                if (!hasOSData)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.28สวัสดิการ
                                bool hasOT01Data = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("OT01"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ OT01 ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasOT01Data = true;
                                    }
                                }
                                if (!hasOT01Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.29สวัสดิการ
                                bool hasOT15Data = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("OT1.5"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ OT1.5 ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasOT15Data = true;
                                    }
                                }
                                if (!hasOT15Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.30สวัสดิการ
                                bool hasOT2Data = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("OT2"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ OT2 ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasOT2Data = true;
                                    }
                                }
                                if (!hasOT2Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.31สวัสดิการ
                                bool hasOT3Data = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("OT3"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ OT3 ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasOT3Data = true;
                                    }
                                }
                                if (!hasOT3Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.32สวัสดิการ
                                bool hasPHAData = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("PHA"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ PHAD ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasPHAData = true;
                                    }
                                }
                                if (!hasPHAData)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.33สวัสดิการ
                                bool hasSA03Data = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("SA03"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ SA03 ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasSA03Data = true;
                                    }
                                }
                                if (!hasSA03Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.34สวัสดิการ
                                bool hasSA1Data = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("SA1"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ SA1 ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasSA1Data = true;
                                    }
                                }
                                if (!hasSA1Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.35สวัสดิการ
                                bool hasSA2Data = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("SA2"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ SA2 ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasSA2Data = true;
                                    }
                                }
                                if (!hasSA2Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.36สวัสดิการ
                                bool hasSBAData = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("SBA"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ SBA ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasSBAData = true;
                                    }
                                }
                                if (!hasSBAData)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.37สวัสดิการ
                                bool hasSWData = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("SW"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ SW ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasSWData = true;
                                    }
                                }
                                if (!hasSWData)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.38สวัสดิการ
                                bool hasTAData = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("TA"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ TA ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasTAData = true;
                                    }
                                }
                                if (!hasTAData)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.39สวัสดิการ
                                bool hasUAData = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("UA"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ UA ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasUAData = true;
                                    }
                                }
                                if (!hasUAData)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.40สวัสดิการ
                                bool hasAB01Data = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("AB01"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ AB01 ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasAB01Data = true;
                                    }
                                }
                                if (!hasAB01Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.41สวัสดิการ
                                bool hasAV2Data = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("AV2"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ AV2 ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasAV2Data = true;
                                    }
                                }
                                if (!hasAV2Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.42สวัสดิการ
                                bool hasHDData = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("HD"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ HD ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasHDData = true;
                                    }
                                }
                                if (!hasHDData)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.43สวัสดิการ
                                bool hasLT01Data = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("LT01"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ LT01 ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasLT01Data = true;
                                    }
                                }
                                if (!hasLT01Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.44สวัสดิการ
                                bool hasSLV01Data = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("LV01"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ LV01 ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasSLV01Data = true;
                                    }
                                }
                                if (!hasSLV01Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.45สวัสดิการ
                                bool hasOC2Data = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("OC2"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ OC2 ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasOC2Data = true;
                                    }
                                }
                                if (!hasOC2Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.46สวัสดิการ
                                bool hasOD2Data = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("OD2"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ OD2 ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasOD2Data = true;
                                    }
                                }
                                if (!hasOD2Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //53.47สวัสดิการ
                                bool hasSLF2Data = false;
                                foreach (cls_TREmpbenefit TREmpbenefit in list_Empbenefit)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpbenefit.worker_code) && TREmpbenefit.item_code.Equals("SLF2"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ SLF2 ลงใน bkData
                                        bkData += TREmpbenefit.empbenefit_amount + "|";
                                        hasSLF2Data = true;
                                    }
                                }
                                if (!hasSLF2Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //

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

                                //20.1
                                bool reduce01Data = false;
                                foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpreduce.worker_code) && TREmpreduce.reduce_type.Equals("01"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ SBA ลงใน bkData
                                        bkData += TREmpreduce.empreduce_amount + "|";
                                        reduce01Data = true;
                                    }
                                }
                                if (!reduce01Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //20.2
                                bool reduce02Data = false;
                                foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpreduce.worker_code) && TREmpreduce.reduce_type.Equals("02"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ SBA ลงใน bkData
                                        bkData += TREmpreduce.empreduce_amount + "|";
                                        reduce02Data = true;
                                    }
                                }
                                if (!reduce02Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //20.3
                                bool reduce03Data = false;
                                foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpreduce.worker_code) && TREmpreduce.reduce_type.Equals("03"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ SBA ลงใน bkData
                                        bkData += TREmpreduce.empreduce_amount + "|";
                                        reduce03Data = true;
                                    }
                                }
                                if (!reduce03Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //20.4
                                bool reduce04Data = false;
                                foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpreduce.worker_code) && TREmpreduce.reduce_type.Equals("04"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ SBA ลงใน bkData
                                        bkData += TREmpreduce.empreduce_amount + "|";
                                        reduce04Data = true;
                                    }
                                }
                                if (!reduce04Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //20.5
                                bool reduce05Data = false;
                                foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpreduce.worker_code) && TREmpreduce.reduce_type.Equals("05"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ SBA ลงใน bkData
                                        bkData += TREmpreduce.empreduce_amount + "|";
                                        reduce05Data = true;
                                    }
                                }
                                if (!reduce05Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //20.6
                                bool reduce06Data = false;
                                foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpreduce.worker_code) && TREmpreduce.reduce_type.Equals("06"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ SBA ลงใน bkData
                                        bkData += TREmpreduce.empreduce_amount + "|";
                                        reduce06Data = true;
                                    }
                                }
                                if (!reduce06Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //20.7
                                bool reduce07Data = false;
                                foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpreduce.worker_code) && TREmpreduce.reduce_type.Equals("07"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ SBA ลงใน bkData
                                        bkData += TREmpreduce.empreduce_amount + "|";
                                        reduce07Data = true;
                                    }
                                }
                                if (!reduce07Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //20.8
                                bool reduce08Data = false;
                                foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpreduce.worker_code) && TREmpreduce.reduce_type.Equals("08"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ SBA ลงใน bkData
                                        bkData += TREmpreduce.empreduce_amount + "|";
                                        reduce08Data = true;
                                    }
                                }
                                if (!reduce08Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //20.9
                                bool reduce09Data = false;
                                foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpreduce.worker_code) && TREmpreduce.reduce_type.Equals("09"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ SBA ลงใน bkData
                                        bkData += TREmpreduce.empreduce_amount + "|";
                                        reduce09Data = true;
                                    }
                                }
                                if (!reduce09Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //20.10
                                bool reduce10Data = false;
                                foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpreduce.worker_code) && TREmpreduce.reduce_type.Equals("10"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ SBA ลงใน bkData
                                        bkData += TREmpreduce.empreduce_amount + "|";
                                        reduce10Data = true;
                                    }
                                }
                                if (!reduce10Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //20.11
                                bool reduce11Data = false;
                                foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpreduce.worker_code) && TREmpreduce.reduce_type.Equals("11"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ SBA ลงใน bkData
                                        bkData += TREmpreduce.empreduce_amount + "|";
                                        reduce11Data = true;
                                    }
                                }
                                if (!reduce11Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //20.12
                                bool reduce12Data = false;
                                foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpreduce.worker_code) && TREmpreduce.reduce_type.Equals("12"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ SBA ลงใน bkData
                                        bkData += TREmpreduce.empreduce_amount + "|";
                                        reduce12Data = true;
                                    }
                                }
                                if (!reduce12Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //20.13
                                bool reduce13Data = false;
                                foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpreduce.worker_code) && TREmpreduce.reduce_type.Equals("13"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ SBA ลงใน bkData
                                        bkData += TREmpreduce.empreduce_amount + "|";
                                        reduce13Data = true;
                                    }
                                }
                                if (!reduce13Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //20.14
                                bool reduce15Data = false;
                                foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpreduce.worker_code) && TREmpreduce.reduce_type.Equals("15"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ SBA ลงใน bkData
                                        bkData += TREmpreduce.empreduce_amount + "|";
                                        reduce15Data = true;
                                    }
                                }
                                if (!reduce15Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //20.15
                                bool reduce016Data = false;
                                foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpreduce.worker_code) && TREmpreduce.reduce_type.Equals("16"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ SBA ลงใน bkData
                                        bkData += TREmpreduce.empreduce_amount + "|";
                                        reduce016Data = true;
                                    }
                                }
                                if (!reduce016Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //20.16
                                bool reduce17Data = false;
                                foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpreduce.worker_code) && TREmpreduce.reduce_type.Equals("17"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ SBA ลงใน bkData
                                        bkData += TREmpreduce.empreduce_amount + "|";
                                        reduce17Data = true;
                                    }
                                }
                                if (!reduce17Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //20.17
                                bool reduce22Data = false;
                                foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpreduce.worker_code) && TREmpreduce.reduce_type.Equals("22"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ SBA ลงใน bkData
                                        bkData += TREmpreduce.empreduce_amount + "|";
                                        reduce22Data = true;
                                    }
                                }
                                if (!reduce22Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //20.18
                                bool reduce26Data = false;
                                foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpreduce.worker_code) && TREmpreduce.reduce_type.Equals("26"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ SBA ลงใน bkData
                                        bkData += TREmpreduce.empreduce_amount + "|";
                                        reduce26Data = true;
                                    }
                                }
                                if (!reduce26Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //20.19
                                bool reduce27Data = false;
                                foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpreduce.worker_code) && TREmpreduce.reduce_type.Equals("27"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ SBA ลงใน bkData
                                        bkData += TREmpreduce.empreduce_amount + "|";
                                        reduce27Data = true;
                                    }
                                }
                                if (!reduce27Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //20.20
                                bool reduce30Data = false;
                                foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpreduce.worker_code) && TREmpreduce.reduce_type.Equals("30"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ SBA ลงใน bkData
                                        bkData += TREmpreduce.empreduce_amount + "|";
                                        reduce30Data = true;
                                    }
                                }
                                if (!reduce30Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //20.21
                                bool reduce31Data = false;
                                foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpreduce.worker_code) && TREmpreduce.reduce_type.Equals("SBA"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ SBA ลงใน bkData
                                        bkData += TREmpreduce.empreduce_amount + "|";
                                        reduce31Data = true;
                                    }
                                }
                                if (!reduce31Data)
                                {
                                    bkData += "|";
                                }
                                //
                                //20.22
                                bool reducePFData = false;
                                foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpreduce.worker_code) && TREmpreduce.reduce_type.Equals("PF"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ SBA ลงใน bkData
                                        bkData += TREmpreduce.empreduce_amount + "|";
                                        reducePFData = true;
                                    }
                                }
                                if (!reducePFData)
                                {
                                    bkData += "|";
                                }
                                //
                                //20.23
                                bool reduceRMFData = false;
                                foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpreduce.worker_code) && TREmpreduce.reduce_type.Equals("RMF"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ SBA ลงใน bkData
                                        bkData += TREmpreduce.empreduce_amount + "|";
                                        reduceRMFData = true;
                                    }
                                }
                                if (!reduceRMFData)
                                {
                                    bkData += "|";
                                }
                                //
                                //20.24
                                bool reduceSSFData = false;
                                foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpreduce.worker_code) && TREmpreduce.reduce_type.Equals("SSF"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ SBA ลงใน bkData
                                        bkData += TREmpreduce.empreduce_amount + "|";
                                        reduceSSFData = true;
                                    }
                                }
                                if (!reduceSSFData)
                                {
                                    bkData += "|";
                                }
                                //
                                //20.25
                                bool reduceSSOData = false;
                                foreach (cls_TREmpreduce TREmpreduce in list_Empreduce)
                                {
                                    if (MTWorkers.worker_code.Equals(TREmpreduce.worker_code) && TREmpreduce.reduce_type.Equals("SSO"))
                                    {
                                        // เพิ่มข้อมูลที่มีหัวข้อ SBA ลงใน bkData
                                        bkData += TREmpreduce.empreduce_amount + "|";
                                        reduceSSOData = true;
                                    }
                                }
                                if (!reduceSSOData)
                                {
                                    bkData += "|";
                                }
                                //


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

                                //
                                //21.1 วันที่เข้า
                                bool entryData = false;
                                foreach (cls_MTWorker worker in list_worker)
                                {
                                    if (MTWorkers.worker_code.Equals(worker.worker_code))
                                    {
                                        foreach (cls_TREmpprovident Provident in list_Empprovident)
                                        {
                                            if (worker.worker_code != null && worker.worker_code.Equals(Provident.worker_code))
                                            {
                                                bkData += Provident.empprovident_entry.ToString("dd/MM/yyyy") + "|";
                                                entryData = true;
                                                break;
                                            }
                                        }
                                    }
                                }

                                if (!entryData)
                                {
                                    bkData += "" + "|";
                                }


                                 
                                //
                                //21.2 วันที่ออก
                                bool endData = false;
                                foreach (cls_MTWorker worker in list_worker)
                                {
                                    if (MTWorkers.worker_code.Equals(worker.worker_code))
                                    {
                                        foreach (cls_TREmpprovident Provident in list_Empprovident)
                                        {
                                            if (worker.worker_code != null && worker.worker_code.Equals(Provident.worker_code))
                                            {
                                                bkData += Provident.empprovident_end.ToString("dd/MM/yyyy") + "|";
                                                endData = true;
                                                break;
                                            }
                                        }
                                    }
                                }

                                if (!endData)
                                {
                                    bkData += "" + "|";
                                }
                                //


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
                        dataTable.Columns.AddRange(new DataColumn[127] { new DataColumn("รหัสพนักงาน"), new DataColumn("รหัสบัตร"), new DataColumn("คำนำหน้าชื่อ(ไทย)"), new DataColumn("ชื่อ(ไทย)"), new DataColumn("นามสกุล(ไทย)"), new DataColumn("คำนำหน้าชื่อ(อังกฤษ)"),
                                                                      new DataColumn("ชื่อ(อังกฤษ)"), new DataColumn("นามสกุล(อังกฤษ)"), new DataColumn("ชื่อเล่น"), new DataColumn("เพศ"), 
                                                                      new DataColumn("ประเภทพนักงาน"), new DataColumn("สถานะพนักงาน"), new DataColumn("วันที่เริ่มงาน"), new DataColumn("อายุงาน"), new DataColumn("จำนวนวันทดลองงาน"), new DataColumn("วันที่พ้นทดลองงาน"),
                                                                      new DataColumn("วิธีการคำนวณภาษี"),new DataColumn("เงินเดือน")

                                                                      //ส่วนของสวัสดิการ ถ้ามีข้อมูลที่เพิ่มในเว็บต้องเข้ามาเพิ่มในส่วนนี้ด้วยเพื่อเรียกใช้หัวข้อนั้นๆ
                                                                      ,new DataColumn("สวัสดิการ (ผลรวม)")
                                                                      ,new DataColumn("Accommodation Allowance"),new DataColumn("เงินช่วยเหลือค่าเดินทาง"),new DataColumn("ค่าช่วยเหลือการศึกษาบุตร")
                                                                      ,new DataColumn("Commuting Expat"),new DataColumn("ค่าภาษาอังกฤษ"),new DataColumn("Fix Allowance"),new DataColumn("เงินช่วยเหลือค่าบ้าน")
                                                                      ,new DataColumn("ค่าบ้าน(ภาษีบริษัทออกให้ขาเข้า)"),new DataColumn("ค่าเช่าบ้าน(ขาออก)"),new DataColumn("ค่าภาษาญี่ปุ่น"),new DataColumn("Language Allowance(JLPT)")
                                                                      ,new DataColumn("Mobile Allowance"),new DataColumn("เงินช่วยเหลือค่าอาหาร")
                                                                      ,new DataColumn("ค่าตำแหน่ง"),new DataColumn("กยศ."),new DataColumn("ประกันสังคมบริษัทออกให้"),new DataColumn("เงินสวัสดิการ"),new DataColumn("พักร้อน")
                                                                      ,new DataColumn("เงินจ่ายล่วงหน้า (บริษัทออกให้ ขาเข้า)"),new DataColumn("โบนัส"),new DataColumn("เงินลาชดเชย"),new DataColumn("เงินได้ยกมาเพื่อคำนวณภาษี"),new DataColumn("INTAX")
                                                                      ,new DataColumn("ค่าค้างคืน"),new DataColumn("อื่นๆ (บริษัทออกให้ ขาเข้า)"),new DataColumn("รายได้อื่นๆ"),new DataColumn("รายได้(ไม่รวมทวิ)"),new DataColumn("OT"),new DataColumn("OT1.5")
                                                                      ,new DataColumn("OT2"),new DataColumn("OT3"),new DataColumn("Phone Allowance"),new DataColumn("ค่าจ้างหักณที่จ่าย 3%")
                                                                      ,new DataColumn("เงินเดือนพนักงานรายเดือน"),new DataColumn("เงินเดือนพนักงาน ภาษีบริษัทออกให้"),new DataColumn("สแตนบาย"),new DataColumn("ทำงานกะ"),new DataColumn("ค่าเดินทาง")
                                                                      ,new DataColumn("เบี้ยเลี้ยงต่างจังหวัด	"),new DataColumn("ค่าขาดงาน"),new DataColumn("เงินล่วงหน้า(บริษัทออกให้ ขาออก)"),new DataColumn("ค่าบ้านไม่คำนวณ"),new DataColumn("สาย")
                                                                      ,new DataColumn("ลา"),new DataColumn("อื่นๆ(บริษัทออกให้ ขาออก)"),new DataColumn("อื่นไม่คำนวณ")
                                                                      ,new DataColumn("อื่อายัดเงินตามหมายศาล")
                                                                      //สวัสดิการ
                                                                      
                                                                           
                                                                      //ส่วนของค่าลดหย่อน
                                                                      ,new DataColumn("ค่าลดหย่อน(ผลรวม)") ,new DataColumn("ผู้มีเงินได้(60,000บาทหรือ 120,000บาทแล้วแต่กรณี)") ,new DataColumn("คู่สมรส (60,000 บาท กรณีมีเงินได้รวมคำ นวณภาษีหรือไม่มีเงินได้)") ,new DataColumn("บุตรคนที่ 1 30,000 บาท")
                                                                      ,new DataColumn("บุตรคนที่ 2 60,000 บาท") ,new DataColumn("บิดาของผู้มีเงินได้") ,new DataColumn("มารดาของผู้มีเงินได้") ,new DataColumn("บิดาของคู่สมรสที่มีเงินได้")
                                                                      ,new DataColumn("มารดาของคู่สมรสที่มีเงินได้") ,new DataColumn("อุปการะเลี้ยงดูคนพิการหรือคนทุพพลภาพ") ,new DataColumn("เบี้ยประกันสุขภาพบิดาของผู้มีเงินได้") ,new DataColumn("เบี้ยประกันสุขภาพมารดาของผู้มีเงินได้	")
                                                                      ,new DataColumn("เบี้ยประกันสุขภาพบิดาคู่สมรส") ,new DataColumn("เบี้ยประกันสุขภาพมารดาคู่สมรส") ,new DataColumn("เบี้ยประกันชีวิต") ,new DataColumn("เบี้ยประกันสุขภาพ")
                                                                      ,new DataColumn("เบี้ยประกันชีวิตแบบบำนาญ") ,new DataColumn("ดอกเบี้ยเงินกู้ยืมเพื่อซื้อ เช่าซื้อ หรือสร้างอาคารอยู่อาศัย") ,new DataColumn("ค่าฝากครรภ์และค่าคลอดบุตร	") ,new DataColumn("เงินที่บริจาคแก่พรรคการเมือง")
                                                                      ,new DataColumn("เงินสนับสนุนการศึกษา") ,new DataColumn("เงินบริจาค") ,new DataColumn("เงินสะสมกองทุนสำรองเลี้ยงชีพ") ,new DataColumn("ค่าซื้อหน่วยลงทุนในกองทุนรวมเพื่อการเลี้ยงชีพ(RMF)")
                                                                      ,new DataColumn("ค่าซื้อหน่วยลงทุนในกองทุนรวมเพื่อการออม SSF"),new DataColumn("เงินสมทบทุนประกันสังคม")
                                                                      //ส่วนของค่าลดหย่อน
                                                                      //ส่วนของกองทุน
                                                                      ,new DataColumn("นโยบายกองทุน"),new DataColumn("วันที่เข้ากองทุน"),new DataColumn("วันที่ออกกองทุน"),
                                                                      //ส่วนของกองทุน

                                                                      new DataColumn("สถานะลาออก"), new DataColumn("วันที่ลาออก"), new DataColumn("สาเหตุการลาออก"), new DataColumn("รายละเอียดการลาออก"), new DataColumn("ระดับ01"), new DataColumn("ระดับ02"), 
                                                                      new DataColumn("ระดับ03"), new DataColumn("ตำแหน่งปัจจุบัน"),new DataColumn("วันที่รับตำแหน่ง"), new DataColumn("เหตุผลการปรับตำแหน่งงาน"), new DataColumn("วันเกิด"), new DataColumn("อายุ"), 
                                                                      new DataColumn("เลขที่บัตรประชาชน"), new DataColumn("บัตรประชาชนหมดอายุ"), new DataColumn("เลขที่บัตรประกันสังคม"), new DataColumn("บัตรประกันสังคมหมดอายุ"), new DataColumn("เลขที่หนังสือเดินทาง"), new DataColumn("หนังสือเดินทางหมดอายุ"),
                                                                      new DataColumn("ที่อยู่ปัจจุบัน1"), new DataColumn("หมู่1"), new DataColumn("ซอย1"), new DataColumn("ถนน1"), new DataColumn("ตำบล1"), new DataColumn("อำเภอ1"), 
                                                                      new DataColumn("จังหวัด1"), new DataColumn("รหัสไปรษณีย์1"), new DataColumn("โทรศัพท์1"), new DataColumn("โทรสาร1"), new DataColumn("EMail1"), new DataColumn("เลขที่บัญชีพนักงาน"), new DataColumn("ชื่อบัญชี")
                                                                      
                                                                      , new DataColumn(" ")
                        
                        });
                        // ใช้ลูปเพื่อเพิ่มข้อมูลจาก array เข้า DataTable
                        string[] rows = str.Split(']');
                        foreach (var row in rows)
                        {
                            if (string.IsNullOrEmpty(row))
                                continue;

                            string[] rowData = row.Split('|');

                            bool hasData = rowData.Any(dataValue => !string.IsNullOrEmpty(dataValue.Trim()));

                            // หากมีข้อมูลในแถวนี้ให้เพิ่มเข้า DataTable
                            if (hasData)
                            {
                                dataTable.Rows.Add(rowData);
                            }
                        }

                        // ลบ DataColumn ทั้งหมดที่ไม่มีข้อมูลในทุกแถว
                        for (int i = dataTable.Columns.Count - 1; i >= 0; i--)
                        {
                            bool hasDataInColumn = dataTable.AsEnumerable().Any(row => !string.IsNullOrEmpty(row.Field<string>(i).Trim()));
    
                            // ถ้าไม่มีข้อมูลในคอลัมน์ทั้งหมด ให้ลบ DataColumn
                            if (!hasDataInColumn)
                            {
                                dataTable.Columns.RemoveAt(i);
                            }
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
                    strResult = ex.ToString() + emperror;
                }

            }
            else
            {

            }


            return strResult;
        }

    }
    ///

}