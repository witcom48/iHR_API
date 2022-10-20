using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace HRFocusWCFSystem
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IServiceSys
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here


        //-- *************************
        //-- System
        //-- *************************
        #region System
        //-- MTBank
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTBankList();
        
        [OperationContract(Name = "doManageMTBank")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageMTBank(InputMTBank input);
        
        [OperationContract(Name = "doDeleteMTBank")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTBank(InputMTBank input);

        //-- MTProvince
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTProvinceList();

        [OperationContract(Name = "doManageMTProvince")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageMTProvince(InputMTProvince input);

        [OperationContract(Name = "doDeleteMTProvince")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTProvince(InputMTProvince input);


        //-- MTReduce
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTReduceList();

        [OperationContract(Name = "doManageMTReduce")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageMTReduce(InputMTReduce input);

        [OperationContract(Name = "doDeleteMTReduce")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTReduce(InputMTReduce input);

        //-- MTCardtype
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTCardtypeList();

        [OperationContract(Name = "doManageMTCardtype")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageMTCardtype(InputMTCardtype input);

        [OperationContract(Name = "doDeleteMTCardtype")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTCardtype(InputMTCardtype input);
        
        //-- MTLocation
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTLocationList();
        
        [OperationContract(Name = "doManageMTLocation")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageMTLocation(InputMTLocation input);

        [OperationContract(Name = "doDeleteMTLocation")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTLocation(InputMTLocation input);

        //-- MTFamily
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTFamilyList();

        [OperationContract(Name = "doManageMTFamily")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageMTFamily(InputMTFamily input);

        [OperationContract(Name = "doDeleteMTFamily")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTFamily(InputMTFamily input);

        //-- Round
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTRoundList(string group);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTRRoundList(string id);

        [OperationContract(Name = "doManageRound")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageRound(InputMTRound input);

        [OperationContract(Name = "doDeleteRound")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteRound(InputMTRound input);

        //-- Reason
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTReasonList(string group);

        [OperationContract(Name = "doManageMTReason")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageMTReason(InputMTReason input);

        [OperationContract(Name = "doDeleteMTReason")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTReason(InputMTReason input);

        //-- Company
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTCompanyList();

        [OperationContract(Name = "doManageMTCompany")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageMTCompany(InputMTCompany input);

        [OperationContract(Name = "doDeleteMTCompany")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTCompany(InputMTCompany input);

        //-- Comcard
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTRComcardList(string com, string branch, string type);

        [OperationContract(Name = "doManageTRComcard")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTRComcard(InputTRComcard input);

        [OperationContract(Name = "doDeleteTRComcard")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTRComcard(InputTRComcard input);

        [OperationContract(Name = "doManageTRComcardList")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTRComcardList(InputTRList input);


        //-- Position
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTPositionList(string com);

        [OperationContract(Name = "doManageMTPosition")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageMTPosition(InputMTPosition input);

        [OperationContract(Name = "doDeleteMTPosition")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTPosition(InputMTPosition input);

        //-- Level
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTLevelList(string com);

        [OperationContract(Name = "doManageMTLevel")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageMTLevel(InputMTLevel input);

        [OperationContract(Name = "doDeleteMTLevel")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTLevel(InputMTLevel input);

        //-- Year
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTYearList(string com, string group);

        [OperationContract(Name = "doManageMTYear")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageMTYear(InputMTYear input);

        [OperationContract(Name = "doDeleteMTYear")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTYear(InputMTYear input);

        //-- Dep
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTDepList(string com, string level, string parent);

        [OperationContract(Name = "doManageMTDep")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageMTDep(InputMTDep input);

        [OperationContract(Name = "doDeleteMTDep")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTDep(InputMTDep input);

        //-- Initial
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTInitialList();

        [OperationContract(Name = "doManageMTInitial")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageMTInitial(InputMTInitial input);

        [OperationContract(Name = "doDeleteMTInitial")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTInitial(InputMTInitial input);

        //-- Task
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTTaskList(string com, string type, string status);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]        
        string getTRTaskdetail(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]        
        string getTRTaskwhose(string id);
        
        [OperationContract(Name = "doManageTask")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTask(InputMTTask input);

        [OperationContract(Name = "doDeleteMTTask")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTTask(InputMTTask input);

        //-- Course
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTCourseList(string com);

        [OperationContract(Name = "doManageMTCourse")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageMTCourse(InputMTCourse input);

        [OperationContract(Name = "doDeleteMTCourse")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTCourse(InputMTCourse input);

        //-- Faculty
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTFacultyList(string com);

        [OperationContract(Name = "doManageMTFaculty")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageMTFaculty(InputMTFaculty input);

        [OperationContract(Name = "doDeleteMTFaculty")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTFaculty(InputMTFaculty input);

        //-- Major
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTMajorList(string com);

        [OperationContract(Name = "doManageMTMajor")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageMTMajor(InputMTMajor input);

        [OperationContract(Name = "doDeleteMTMajor")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTMajor(InputMTMajor input);

        //-- Institute
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTInstituteList(string com);

        [OperationContract(Name = "doManageMTInstitute")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageMTInstitute(InputMTInstitute input);

        [OperationContract(Name = "doDeleteMTInstitute")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTInstitute(InputMTInstitute input);

        //-- Qualification
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTQualificationList(string com);

        [OperationContract(Name = "doManageMTQualification")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageMTQualification(InputMTQualification input);

        [OperationContract(Name = "doDeleteMTQualification")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTQualification(InputMTQualification input);

        //-- Code structure
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getSYSCodestructureList();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTPolcode(string com, string type);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getNewCode(string com, string type, string emptype);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTRPolcode(string id);

        [OperationContract(Name = "doManagePolcode")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManagePolcode(InputMTPolcode input);

        [OperationContract(Name = "doDeleteMTPolcode")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTPolcode(InputMTPolcode input);


        //-- Period
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTPeriodList(string com, string type, string emptype, string year);

        [OperationContract(Name = "doManageMTPeriod")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageMTPeriod(InputMTPeriod input);

        [OperationContract(Name = "doDeleteMTPeriod")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTPeriod(InputMTPeriod input);
        
        //-- Combranch
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTRCombranchList(string com);

        [OperationContract(Name = "doManageTRCombranch")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTRCombranch(InputTRCombranch input);

        [OperationContract(Name = "doDeleteTRCombranch")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTRCombranch(InputTRCombranch input);

        //-- Comaddress
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTRComaddressList(string com, string branch, string type);

        [OperationContract(Name = "doManageTRComaddressList")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTRComaddressList(InputTRList input);

        [OperationContract(Name = "doDeleteTRComaddress")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTRComaddress(InputTRComaddress input);


        //-- Combank
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTRCombankList(string com);

        [OperationContract(Name = "doManageTRCombankList")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTRCombankList(InputTRList input);

        [OperationContract(Name = "doDeleteTRCombank")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTRCombank(InputTRCombank input);


        //-- SYS Menu
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getSYSMainmenuList();
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getSYSSubmenuList();
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getSYSItemmenuList();


        //-- Account
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getSYSAccountList();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getSYSAccountUserList(string usr);

        [OperationContract(Name = "doManageSYSAccount")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageSYSAccount(InputSYSAccount input);

        [OperationContract(Name = "doDeleteSYSAccount")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteSYSAccount(InputSYSAccount input);

        //-- Access menu
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getSYSAccessmenuList(string com, string polcode);

        [OperationContract(Name = "doManageSYSAccessmenuList")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageSYSAccessmenuList(InputTRList input);

        [OperationContract(Name = "doDeleteSYSAccessmenu")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteSYSAccessmenu(InputSYSAccessmenu input);

        //-- Access position
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getSYSAccesspositionList(string com, string username);

        [OperationContract(Name = "doManageSYSAccesspositionList")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageSYSAccesspositionList(InputTRList input);

        [OperationContract(Name = "doDeleteSYSAccessposition")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteSYSAccessposition(InputSYSAccessposition input);

        //-- Access dep
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getSYSAccessdepList(string com, string username);

        [OperationContract(Name = "doManageSYSAccessdepList")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageSYSAccessdepList(InputTRList input);

        [OperationContract(Name = "doDeleteSYSAccessdep")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteSYSAccessdep(InputSYSAccessdep input);

        //-- SYS policy menu
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getSYSPolmenuList(string com, string polcode);

        [OperationContract(Name = "doManageSYSPolmenu")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageSYSPolmenu(InputSYSPolmenu input);

        [OperationContract(Name = "doDeleteSYSPolmenu")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteSYSPolmenu(InputSYSPolmenu input);

        //-- Access data
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getSYSAccessdataList(string com, string polcode);

        [OperationContract(Name = "doManageSYSAccessdataList")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageSYSAccessdataList(InputTRList input);

        [OperationContract(Name = "doDeleteSYSAccessdata")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteSYSAccessdata(InputSYSAccessdata input);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getSYSReportjobList(string com);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getSYSReportjobWhoseList(string id);

        [OperationContract(Name = "doManageSYSReportjob")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageSYSReportjob(InputSYSReportjob input);

        [OperationContract(Name = "doDeleteSYSReportjob")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteSYSReportjob(InputSYSReportjob input);

        #endregion
               
        //-- ********************
        //-- Employee
        //-- ********************
        #region Employee
        //-- Worker
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTWorkerList(string com, string id, string code, string fname, string lname, string level, string depcod);

        [OperationContract(Name = "getMTWorkerFillterList")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string getMTWorkerFillterList(FillterEmp input);

        [OperationContract(Name = "doManageMTWorker")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageMTWorker(InputMTWorker input);

        [OperationContract(Name = "doDeleteMTWorker")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTWorker(InputMTWorker input);

        //-- Empcard
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTREmpcardList(string com, string emp);

        [OperationContract(Name = "doManageTREmpcard")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTREmpcard(InputTREmpcard input);

        [OperationContract(Name = "doDeleteTREmpcard")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTREmpcard(InputTREmpcard input);

        //-- Empreduce
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTREmpreduceList(string com, string emp);

        [OperationContract(Name = "doManageTREmpreduce")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTREmpreduce(InputTREmpreduce input);

        [OperationContract(Name = "doDeleteTREmpreduce")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTREmpreduce(InputTREmpreduce input);

        //-- Empfamily
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTREmpfamilyList(string com, string emp);

        [OperationContract(Name = "doManageTREmpfamily")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTREmpfamily(InputTREmpfamily input);

        [OperationContract(Name = "doDeleteTREmpfamily")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTREmpfamily(InputTREmpfamily input);

        //-- Empsalary
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTREmpsalaryList(string com, string emp);

        [OperationContract(Name = "doManageTREmpsalary")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTREmpsalary(InputTREmpsalary input);

        [OperationContract(Name = "doManageTREmpsalaryList")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTREmpsalaryList(InputTRList input);

        [OperationContract(Name = "doDeleteTREmpsalary")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTREmpsalary(InputTREmpsalary input);

        //-- Empbenefit
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTREmpbenefitList(string com, string emp);

        [OperationContract(Name = "doManageTREmpbenefit")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTREmpbenefit(InputTREmpbenefit input);

        [OperationContract(Name = "doManageTREmpbenefitList")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTREmpbenefitList(InputTRList input);

        [OperationContract(Name = "doDeleteTREmpbenefit")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTREmpbenefit(InputTREmpbenefit input);

        //-- Empdep
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTREmpdepList(string com, string emp);
        
        [OperationContract(Name = "doManageTREmpdepList")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTREmpdepList(InputTRList input);

        [OperationContract(Name = "doDeleteTREmpdep")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTREmpdep(InputTREmpdep input);

        //-- Empposition
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTREmppositionList(string com, string emp);

        [OperationContract(Name = "doManageTREmppositionList")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTREmppositionList(InputTRList input);

        [OperationContract(Name = "doDeleteTREmpposition")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTREmpposition(InputTREmpposition input);

        //-- Empaddress
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTREmpaddressList(string com, string emp);

        [OperationContract(Name = "doManageTREmpaddressList")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTREmpaddressList(InputTRList input);

        [OperationContract(Name = "doDeleteTREmpaddress")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTREmpaddress(InputTREmpaddress input);

        //-- Empeducation
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTREmpeducationList(string com, string emp);

        [OperationContract(Name = "doManageTREmpeducationList")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTREmpeducationList(InputTRList input);

        [OperationContract(Name = "doDeleteTREmpeducation")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTREmpeducation(InputTREmpeducation input);

        //-- Emptraining
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTREmptrainingList(string com, string emp);

        [OperationContract(Name = "doManageTREmptrainingList")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTREmptrainingList(InputTRList input);

        [OperationContract(Name = "doDeleteTREmptraining")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTREmptraining(InputTREmptraining input);

        //-- Empexperience
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTREmpexperienceList(string com, string emp);

        [OperationContract(Name = "doManageTREmpexperienceList")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTREmpexperienceList(InputTRList input);

        [OperationContract(Name = "doDeleteTREmpexperience")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTREmpexperience(InputTREmpexperience input);

        //-- Empprovident
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTREmpprovidentList(string com, string emp);

        [OperationContract(Name = "doManageTREmpprovidentList")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTREmpprovidentList(InputTRList input);

        [OperationContract(Name = "doDeleteTREmpprovident")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTREmpprovident(InputTREmpprovident input);

        //-- Emplocation
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTREmplocationList(string com, string emp, string location);

        [OperationContract(Name = "doManageTREmplocation")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTREmplocation(InputTREmplocation input);

        [OperationContract(Name = "doManageTREmplocationList")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTREmplocationList(InputTRList input);

        [OperationContract(Name = "doDeleteTREmplocation")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTREmplocation(InputTREmplocation input);

        //-- Empbank
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTREmpbankList(string com, string emp);

        [OperationContract(Name = "doManageTREmpbankList")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTREmpbankList(InputTRList input);

        [OperationContract(Name = "doDeleteTREmpbank")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTREmpbank(InputTREmpbank input);

        //-- Empawpt
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTREmpawptList(string com, string emp, string type);

        [OperationContract(Name = "doManageTREmpawptList")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTREmpawptList(InputTRList input);

        [OperationContract(Name = "doDeleteTREmpawpt")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTREmpawpt(InputTREmpawpt input);

        //-- Empkt20
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTREmpkt20List(string com, string year, double rate);

        [OperationContract(Name = "doManageTREmpkt20List")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTREmpkt20List(InputTRList input);

        [OperationContract(Name = "doDeleteTREmpkt20")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTREmpkt20(InputTREmpkt20 input);

        
        //-- Batch benefit        
        [OperationContract(Name = "doSetBatchBenefit")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doSetBatchBenefit(InputTREmpbenefit input);

        //-- Batch salary        
        [OperationContract(Name = "doSetBatchSalary")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doSetBatchSalary(InputTREmpsalary input);

        //-- Batch location        
        [OperationContract(Name = "doSetBatchLocation")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doSetBatchLocation(InputTREmplocation input);
        
        #endregion

        //-- ********************
        //-- Payroll
        //-- ********************
        #region Payroll
        //-- Item
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTItemList(string com);

        [OperationContract(Name = "doManageMTItem")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageMTItem(InputMTItem input);

        [OperationContract(Name = "doDeleteMTItem")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTItem(InputMTItem input);


        //-- Provident
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTProvidentList(string com);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTRProvidentWorkageList(string com, string code);

        [OperationContract(Name = "doManageMTProvident")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageMTProvident(InputMTProvident input);

        [OperationContract(Name = "doDeleteMTProvident")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTProvident(InputMTProvident input);

        //-- Payitem
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTRPayitemList(string language, string com, string emp, string paydate, string itemtype, string itemcode);

        [OperationContract(Name = "doManageTRPayitemList")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTRPayitemList(InputTRList input);

        [OperationContract(Name = "doManageTRPayitem")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTRPayitem(InputTRPayitem input);

        [OperationContract(Name = "doDeleteTRPayitem")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTRPayitem(InputTRPayitem input);

        //-- TRTaxrate
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTRTaxrateList(string com);

        [OperationContract(Name = "doManageTRTaxrate")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTRTaxrate(InputTRTaxrate input);

        [OperationContract(Name = "doDeleteTRTaxrate")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTRTaxrate(InputTRTaxrate input);
        
        //-- Paytran
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTRPaytranList(string language, string com, string emp, string fromdate, string todate);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTRPaytranAccList(string language, string com, string emp, string year, string paydate);

        [OperationContract(Name = "doManageTRPaytran")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTRPaytran(InputTRPaytran input);

        [OperationContract(Name = "doDeleteTRPaytran")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTRPaytran(InputTRPaytran input);

        //-- Payreduce
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTRPayreduceList(string com, string emp, string paydate);

        //-- MTBonus
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTBonusList(string com);

        [OperationContract(Name = "doManageMTBonus")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageMTBonus(InputMTBonus input);

        [OperationContract(Name = "doDeleteMTBonus")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTBonus(InputMTBonus input);

        //-- TRBonusrate
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTRBonusrateList(string com, string bonuscode);

        [OperationContract(Name = "doManageTRBonusrateList")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTRBonusrateList(InputTRList input);

        [OperationContract(Name = "doDeleteTRBonusrate")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTRBonusrate(InputTRBonusrate input);

        //-- TRPaybonus
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTRPaybonusList(string username, string language, string com, string paydate);

        [OperationContract(Name = "doManageTRPaybonusList")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTRPaybonusList(InputTRList input);

        [OperationContract(Name = "doManageTRPaybonus")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTRPaybonus(InputTRPaybonus input);

        [OperationContract(Name = "doDeleteTRPaybonus")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTRPaybonus(InputTRPaybonus input);

        //-- TRPaypolbonus
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTRPaypolbonusList(string language, string username, string com, string bonuscode);

        [OperationContract(Name = "doManageTRPaypolbonusList")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTRPaypolbonusList(InputTRList input);

        [OperationContract(Name = "doDeleteTRPaypolbonus")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTRPaypolbonus(InputTRPaypolbonus input);

        #endregion

        //-- ********************
        //-- Attendance
        //-- ********************
        #region Attendance
        //-- Shift
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTShiftList(string com);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTRShiftallowanceList(string com, string shift);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTRShiftbreakList(string com, string shift);

        [OperationContract(Name = "doManageMTShift")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageMTShift(InputMTShift input);

        [OperationContract(Name = "doDeleteMTShift")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTShift(InputMTShift input);

        //-- Late
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTLateList(string com);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTRLateList(string com, string code);

        [OperationContract(Name = "doManageMTLate")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageMTLate(InputMTLate input);

        [OperationContract(Name = "doDeleteMTLate")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTLate(InputMTLate input);

        //-- Holiday
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTHolidayList(string com, string year);

        [OperationContract(Name = "doManageMTHoliday")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageMTHoliday(InputMTHoliday input);

        [OperationContract(Name = "doDeleteMTHoliday")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTHoliday(InputMTHoliday input);

        //-- Leave
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTLeaveList(string com);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getLeaveEmpList(string com, string emp, string year);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTRLeaveWorkageList(string com, string code);

        [OperationContract(Name = "doManageMTLeave")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageMTLeave(InputMTLeave input);

        [OperationContract(Name = "doDeleteMTLeave")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTLeave(InputMTLeave input);

        //-- Rateot
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTRateotList(string com);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTRRateotList(string com, string code);

        [OperationContract(Name = "doManageMTRateot")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageMTRateot(InputMTRateot input);

        [OperationContract(Name = "doDeleteMTRateot")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTRateot(InputMTRateot input);

        //-- Planleave
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTPlanleaveList(string com);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTRPlanleaveList(string com, string code);

        [OperationContract(Name = "doManageMTPlanleave")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageMTPlanleave(InputMTPlanleave input);

        [OperationContract(Name = "doDeleteMTPlanleave")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTPlanleave(InputMTPlanleave input);

        //-- Planshift
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTPlanshiftList(string com);

        [OperationContract(Name = "doManageMTPlanshift")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageMTPlanshift(InputMTPlanshift input);

        [OperationContract(Name = "doDeleteMTPlanshift")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTPlanshift(InputMTPlanshift input);

        //-- Planschedule
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTRPlanscheduleList(string com, string plan);

        [OperationContract(Name = "doManageTRPlanschedule")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTRPlanschedule(InputTRPlanschedule input);

        [OperationContract(Name = "doDeleteTRPlanschedule")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTRPlanschedule(InputTRPlanschedule input);

        [OperationContract(Name = "doSetBatchPlanshift")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doSetBatchPlanshift(InputBatchPlanshift input);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTRTimecardList(string com, string emp, string fromdate, string todate);

        [OperationContract(Name = "doManageTRTimecard")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTRTimecard(InputTRTimecard input);


        //-- Diligence
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTDiligenceList(string com);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTRSteppayDiligenceList(string com, string code);

        [OperationContract(Name = "doManageMTDiligence")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageMTDiligence(InputMTDiligence input);

        [OperationContract(Name = "doDeleteMTDiligence")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTDiligence(InputMTDiligence input);

        //-- Emppolatt
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getPolicyAttendance(string com, string username, string type);

        [OperationContract(Name = "doSetPolicyAttendance")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doSetPolicyAttendance(InputSetPolicyAtt input);

        [OperationContract(Name = "doDeleteTREmppolatt")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTREmppolatt(InputTREmppolatt input);

        //-- EmppolattItem
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getPolicyAttendanceItem(string com);

        [OperationContract(Name = "doSetPolicyAttendanceItem")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doSetPolicyAttendanceItem(InputSetPolicyAttItem input);

        [OperationContract(Name = "doDeleteTREmppolattItem")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTREmppolattItem(InputTREmppolatt input);

        //-- Attwageday
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTRAttwagedayList(string language, string com, string fromdate, string todate, string emp);

        [OperationContract(Name = "doManageTRAttwageday")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTRAttwageday(InputTRAttwageday input);

        [OperationContract(Name = "doDeleteTRAttwageday")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTRAttwageday(InputTRAttwageday input);

        //-- Timeinput
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTRTimeinputList(string com, string emp, string fromdate, string todate);

        [OperationContract(Name = "doManageTRTimeinput")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTRTimeinput(InputTRTimeinput input);

        [OperationContract(Name = "doDeleteTRTimeinput")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTRTimeinput(InputTRTimeinput input);
               

        //-- Timeimpformat
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTTimeimpformatList(string com);

        [OperationContract(Name = "doManageMTTimeimpformat")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageMTTimeimpformat(InputMTTimeimpformat input);

        [OperationContract(Name = "doDeleteMTTimeimpformat")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTTimeimpformat(InputMTTimeimpformat input);

        //-- Empleaveacc
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTREmpleaveaccList(string language, string com, string emp, string year);

        [OperationContract(Name = "doManageTREmpleaveacc")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTREmpleaveacc(InputTREmpleaveacc input);

        [OperationContract(Name = "doDeleteTREmpleaveacc")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTREmpleaveacc(InputTREmpleaveacc input);



        //-- Timedoc
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTRTimedocList(string com, string emp, string workdate, string type);

        [OperationContract(Name = "doManageTRTimedoc")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTRTimedoc(InputTRTimedoc input);

        [OperationContract(Name = "doDeleteTRTimedoc")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTRTimedoc(InputTRTimedoc input);


        //-- Timeleave
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTRTimeleaveList(string language, string com, string emp, string fromdate, string todate);

        [OperationContract(Name = "doManageTRTimeleave")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTRTimeleave(InputTRTimeleave input);

        [OperationContract(Name = "doDeleteTRTimeleave")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTRTimeleave(InputTRTimeleave input);

        //-- Timeot
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTRTimeotList(string language, string com, string emp, string fromdate, string todate);

        [OperationContract(Name = "doManageTRTimeot")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTRTimeot(InputTRTimeot input);

        [OperationContract(Name = "doDeleteTRTimeot")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTRTimeot(InputTRTimeot input);

        //-- Timeonsite
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTRTimeonsiteList(string language, string com, string emp, string fromdate, string todate);

        [OperationContract(Name = "doManageTRTimeonsite")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTRTimeonsite(InputTRTimeonsite input);

        [OperationContract(Name = "doDeleteTRTimeonsite")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTRTimeonsite(InputTRTimeonsite input);

        //-- Timeshift
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTRTimeshiftList(string language, string com, string emp, string fromdate, string todate);

        [OperationContract(Name = "doManageTRTimeshift")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTRTimeshift(InputTRTimeshift input);

        [OperationContract(Name = "doDeleteTRTimeshift")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTRTimeshift(InputTRTimeshift input);

        //-- Timedaytype
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTRTimedaytypeList(string language, string com, string emp, string fromdate, string todate);

        [OperationContract(Name = "doManageTRTimedaytype")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageTRTimedaytype(InputTRTimedaytype input);

        [OperationContract(Name = "doDeleteTRTimedaytype")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteTRTimedaytype(InputTRTimedaytype input);

        //-- Plan Holiday
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTPlanholidayList(string com, string year);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTRHolidayList(string com, string plan);

        [OperationContract(Name = "doManageMTPlanholiday")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageMTPlanholiday(InputMTPlanholiday input);

        [OperationContract(Name = "doDeleteMTPlanholiday")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTPlanholiday(InputMTPlanholiday input);

        //-- Plan Timeallw
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTPlantimeallwList(string com);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTRTimeallwList(string com, string plan);

        [OperationContract(Name = "doManageMTPlantimeallw")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageMTPlantimeallw(InputMTPlantimeallw input);

        [OperationContract(Name = "doDeleteMTPlantimeallw")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTPlantimeallw(InputMTPlantimeallw input);

        //-- Plan Shift flexible
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getMTPlanshiftflexibleList(string com, string year);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getTRShiftflexibleList(string com, string plan);

        [OperationContract(Name = "doManageMTPlanshiftflexible")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doManageMTPlanshiftflexible(InputMTPlanshiftflexible input);

        [OperationContract(Name = "doDeleteMTPlanshiftflexible")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doDeleteMTPlanshiftflexible(InputMTPlanshiftflexible input);
        
        #endregion

        //-- ********************
        //-- Dashboard
        //-- ********************
        #region Dashboard
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getDashLeaveList(string com);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getDashLateList(string com);
        #endregion

        //-- SummaryWage
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getSummaryWageList(string language, string com, string fromdate, string todate, string createdate);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string getShfitDetail(string com, string emp, string workdate, string language);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string doGetLeaveActualDay(string com, string emp, string fromdate, string todate);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string doVerify(string usr, string token);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string doAuthen(string usr, string pwd, string token);
        
        [OperationContract]
        [WebGet(UriTemplate = "File/{fileName}/{fileExtension}")]
        Stream doDownloadFile(string fileName, string fileExtension);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/doUploadTimeInput?fileName={fileName}", ResponseFormat = WebMessageFormat.Json)]
        string doUploadTimeInput(string fileName, Stream stream);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/doReadSimpleTimeInput?fileName={fileName}", ResponseFormat = WebMessageFormat.Json)]
        string doReadSimpleTimeInput(string fileName, Stream stream);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string doSummarizeTime(string com, string taskid);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string doImportTime(string com, string taskid);


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/doUploadWorkerImages?ref_to={ref_to}", ResponseFormat = WebMessageFormat.Json)]
        string doUploadWorkerImages(string ref_to, Stream stream);



        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/doGetWorkerImages?com={com}&emp={emp}", ResponseFormat = WebMessageFormat.Json)]
        string doGetWorkerImages(string com, string emp);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        string doLogin(string usr, string pwd, string token);

        [OperationContract(Name = "doChangePass")]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string doChangePass(InputMTWorker input);
        
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }


    [DataContract]
    public class UploadedFile
    {
        [DataMember]
        public string FilePath { get; set; }

        [DataMember]
        public int FileLength
        {
            get; set;
        }

        [DataMember]
        public string FileName { get; set; }

    }

}
