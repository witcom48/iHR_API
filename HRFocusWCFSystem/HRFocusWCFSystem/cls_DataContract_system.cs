using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HRFocusWCFSystem
{
    public class cls_DataContract_system
    {
    }

    [DataContract]
    public class InputMTBank
    {
        [DataMember]
        public int bank_id { get; set; }
        [DataMember]
        public string bank_code { get; set; }
        [DataMember]
        public string bank_name_th { get; set; }
        [DataMember]
        public string bank_name_en { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }

        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputMTLocation
    {
        [DataMember]
        public int location_id { get; set; }
        [DataMember]
        public string location_code { get; set; }
        [DataMember]
        public string location_name_th { get; set; }
        [DataMember]
        public string location_name_en { get; set; }
        [DataMember]
        public string location_detail { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }

        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputMTRound
    {
        [DataMember]
        public int round_id { get; set; }
        [DataMember]
        public string round_code { get; set; }
        [DataMember]
        public string round_name_th { get; set; }
        [DataMember]
        public string round_name_en { get; set; }
        [DataMember]
        public string round_group { get; set; }

        [DataMember]
        public string round_data { get; set; }

        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }

        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputTRRound
    {
        [DataMember]
        public string round_code { get; set; }
        [DataMember]
        public string round_from { get; set; }
        [DataMember]
        public string round_to { get; set; }
        [DataMember]
        public string round_result { get; set; }

        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputMTReason
    {
        [DataMember]
        public int reason_id { get; set; }
        [DataMember]
        public string reason_code { get; set; }
        [DataMember]
        public string reason_name_th { get; set; }
        [DataMember]
        public string reason_name_en { get; set; }
        [DataMember]
        public string reason_group { get; set; }

        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }

        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputMTCompany
    {
        [DataMember]
        public int company_id { get; set; }
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string company_name_th { get; set; }
        [DataMember]
        public string company_name_en { get; set; }
        [DataMember]
        public double hrs_perday { get; set; }

        [DataMember]
        public double sso_com_rate { get; set; }
        [DataMember]
        public double sso_emp_rate { get; set; }
        [DataMember]
        public double sso_min_wage { get; set; }
        [DataMember]
        public double sso_max_wage { get; set; }
        [DataMember]
        public int sso_min_age { get; set; }
        [DataMember]
        public int sso_max_age { get; set; }

        [DataMember]
        public string card_data { get; set; }

        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }

        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputTRComcard
    {
        [DataMember]
        public int comcard_id { get; set; }
        [DataMember]
        public string comcard_code { get; set; }
        [DataMember]
        public string card_type { get; set; }
        [DataMember]
        public string comcard_issue { get; set; }
        [DataMember]
        public string comcard_expire { get; set; }
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string combranch_code { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }
    }
        
    [DataContract]
    public class InputMTLevel
    {
        [DataMember]
        public int level_id { get; set; }
        [DataMember]
        public string level_code { get; set; }
        [DataMember]
        public string level_name_th { get; set; }
        [DataMember]
        public string level_name_en { get; set; }
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }
    }
        
    
    [DataContract]
    public class InputMTReduce
    {
        [DataMember]
        public int reduce_id { get; set; }
        [DataMember]
        public string reduce_code { get; set; }
        [DataMember]
        public string reduce_name_th { get; set; }
        [DataMember]
        public string reduce_name_en { get; set; }

        [DataMember]
        public double reduce_amount { get; set; }
        [DataMember]
        public double reduce_percent { get; set; }
        [DataMember]
        public double reduce_percent_max { get; set; }

        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }

        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputMTCardtype
    {
        [DataMember]
        public int cardtype_id { get; set; }
        [DataMember]
        public string cardtype_code { get; set; }
        [DataMember]
        public string cardtype_name_th { get; set; }
        [DataMember]
        public string cardtype_name_en { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }

        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputMTFamily
    {
        [DataMember]
        public int family_id { get; set; }
        [DataMember]
        public string family_code { get; set; }
        [DataMember]
        public string family_name_th { get; set; }
        [DataMember]
        public string family_name_en { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }

        [DataMember]
        public int index { get; set; }
    }
                
    [DataContract]
    public class InputTRList
    {        
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string worker_code { get; set; }
        [DataMember]
        public string transaction_data { get; set; }       
        [DataMember]
        public string modified_by { get; set; }            
    }
        
    [DataContract]
    public class InputMTYear
    {
        [DataMember]
        public int year_id { get; set; }
        [DataMember]
        public string year_code { get; set; }
        [DataMember]
        public string year_name_th { get; set; }
        [DataMember]
        public string year_name_en { get; set; }
        [DataMember]
        public string year_fromdate { get; set; }
        [DataMember]
        public string year_todate { get; set; }
        [DataMember]
        public string year_group { get; set; }
        [DataMember]
        public string company_code { get; set; }      
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }

        [DataMember]
        public int index { get; set; }
    }


    [DataContract]
    public class InputMTTask
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public int task_id { get; set; }
        [DataMember]
        public string task_type { get; set; }
        [DataMember]
        public string task_status { get; set; }
        [DataMember]
        public string task_start { get; set; }
        [DataMember]
        public string task_end { get; set; }
        [DataMember]
        public string task_note { get; set; }

        [DataMember]
        public string detail_data { get; set; }

        [DataMember]
        public string whose_data { get; set; }

        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputMTCourse
    {
        [DataMember]
        public int course_id { get; set; }
        [DataMember]
        public string course_code { get; set; }
        [DataMember]
        public string course_name_th { get; set; }
        [DataMember]
        public string course_name_en { get; set; }
        [DataMember]
        public string company_code { get; set; }        
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputMTFaculty
    {
        [DataMember]
        public int faculty_id { get; set; }
        [DataMember]
        public string faculty_code { get; set; }
        [DataMember]
        public string faculty_name_th { get; set; }
        [DataMember]
        public string faculty_name_en { get; set; }
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputMTMajor
    {
        [DataMember]
        public int major_id { get; set; }
        [DataMember]
        public string major_code { get; set; }
        [DataMember]
        public string major_name_th { get; set; }
        [DataMember]
        public string major_name_en { get; set; }
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputMTInstitute
    {
        [DataMember]
        public int institute_id { get; set; }
        [DataMember]
        public string institute_code { get; set; }
        [DataMember]
        public string institute_name_th { get; set; }
        [DataMember]
        public string institute_name_en { get; set; }
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputMTQualification
    {
        [DataMember]
        public int qualification_id { get; set; }
        [DataMember]
        public string qualification_code { get; set; }
        [DataMember]
        public string qualification_name_th { get; set; }
        [DataMember]
        public string qualification_name_en { get; set; }
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputTRTaxrate
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public int taxrate_id { get; set; }
        [DataMember]
        public double taxrate_from { get; set; }
        [DataMember]
        public double taxrate_to { get; set; }
        [DataMember]
        public double taxrate_tax { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }

        [DataMember]
        public int index { get; set; }
    }

    //-- Code structure
    [DataContract]
    public class InputSYSCodestructure
    {        
        [DataMember]
        public string codestructure_code { get; set; }
        [DataMember]
        public string codestructure_name_th { get; set; }
        [DataMember]
        public string codestructure_name_en { get; set; }        
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputMTPolcode
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public int polcode_id { get; set; }
        [DataMember]
        public string polcode_type { get; set; }

        [DataMember]
        public string polcode_data { get; set; }

        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputTRPolcode
    {       
        [DataMember]
        public int polcode_id { get; set; }
        [DataMember]
        public string codestructure_code { get; set; }
        [DataMember]
        public int polcode_lenght { get; set; }
        [DataMember]
        public string polcode_text { get; set; }
        [DataMember]
        public int polcode_order { get; set; }        
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputMTPeriod
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public int period_id { get; set; }
        [DataMember]
        public string period_type { get; set; }
        [DataMember]
        public string emptype_code { get; set; }
        [DataMember]
        public string year_code { get; set; }
        [DataMember]
        public string period_no { get; set; }
        [DataMember]
        public string period_name_th { get; set; }
        [DataMember]
        public string period_name_en { get; set; }
        [DataMember]
        public string period_from { get; set; }
        [DataMember]
        public string period_to { get; set; }
        [DataMember]
        public string period_payment { get; set; }
        [DataMember]
        public bool period_dayonperiod { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }

    }

    [DataContract]
    public class InputTRCombranch
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public int combranch_id { get; set; }
        [DataMember]
        public string combranch_code { get; set; }
        [DataMember]
        public string combranch_name_th { get; set; }
        [DataMember]
        public string combranch_name_en { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }

        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputTRComaddress
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string combranch_code { get; set; }
        [DataMember]
        public string comaddress_type { get; set; }
        [DataMember]
        public string comaddress_no { get; set; }
        [DataMember]
        public string comaddress_moo { get; set; }
        [DataMember]
        public string comaddress_soi { get; set; }
        [DataMember]
        public string comaddress_road { get; set; }
        [DataMember]
        public string comaddress_tambon { get; set; }
        [DataMember]
        public string comaddress_amphur { get; set; }
        [DataMember]
        public string comaddress_zipcode { get; set; }
        [DataMember]
        public string comaddress_tel { get; set; }
        [DataMember]
        public string comaddress_email { get; set; }
        [DataMember]
        public string comaddress_line { get; set; }
        [DataMember]
        public string comaddress_facebook { get; set; }
        [DataMember]
        public string province_code { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public string modified_date { get; set; }
    }

    [DataContract]
    public class InputMTProvince
    {
        [DataMember]
        public int province_id { get; set; }
        [DataMember]
        public string province_code { get; set; }
        [DataMember]
        public string province_name_th { get; set; }
        [DataMember]
        public string province_name_en { get; set; }        
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputTRCombank
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string worker_code { get; set; }
        [DataMember]
        public int combank_id { get; set; }
        [DataMember]
        public string combank_bankcode { get; set; }
        [DataMember]
        public string combank_bankaccount { get; set; }       
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }

    }
    
    [DataContract]
    public class InputSYSMainmenu
    {
        [DataMember]
        public string mainmenu_code { get; set; }
        [DataMember]
        public string mainmenu_detail_th { get; set; }
        [DataMember]
        public string mainmenu_detail_en { get; set; }
        [DataMember]
        public int mainmenu_order { get; set; }       
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputSYSSubmenu
    {
        [DataMember]
        public string mainmenu_code { get; set; }
        [DataMember]
        public string submenu_code { get; set; }
        [DataMember]
        public string submenu_detail_th { get; set; }
        [DataMember]
        public string submenu_detail_en { get; set; }
        [DataMember]
        public int submenu_order { get; set; }
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputSYSItemmenu
    {
        [DataMember]
        public string submenu_code { get; set; }
        [DataMember]
        public string itemmenu_code { get; set; }
        [DataMember]
        public string itemmenu_detail_th { get; set; }
        [DataMember]
        public string itemmenu_detail_en { get; set; }
        [DataMember]
        public int itemmenu_order { get; set; }
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputSYSAccount
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public int account_id { get; set; }
        [DataMember]
        public string account_usr { get; set; }
        [DataMember]
        public string account_pwd { get; set; }
        [DataMember]
        public string account_detail { get; set; }

        [DataMember]
        public string account_email { get; set; }
        [DataMember]
        public bool account_emailalert { get; set; }
        [DataMember]
        public string account_line { get; set; }
        [DataMember]
        public bool account_linealert { get; set; }

        [DataMember]
        public bool account_lock { get; set; }
        [DataMember]
        public int account_faillogin { get; set; }
        [DataMember]
        public string account_lastlogin { get; set; }
                
        [DataMember]
        public bool account_monthly { get; set; }

        [DataMember]
        public bool account_daily { get; set; }

        [DataMember]
        public string polmenu_code { get; set; }

        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputSYSAccessmenu
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string polmenu_code { get; set; }
        [DataMember]
        public string accessmenu_module { get; set; }
        [DataMember]
        public string accessmenu_type { get; set; }
        [DataMember]
        public string accessmenu_code { get; set; }        
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputSYSAccessposition
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string account_usr { get; set; }
        [DataMember]
        public string accessposition_position { get; set; }        
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputSYSAccessdep
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string account_usr { get; set; }
        [DataMember]
        public string accessdep_level { get; set; }
        [DataMember]
        public string accessdep_dep { get; set; }
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputSYSPolmenu
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public int polmenu_id { get; set; }
        [DataMember]
        public string polmenu_code { get; set; }
        [DataMember]
        public string polmenu_name_th { get; set; }
        [DataMember]
        public string polmenu_name_en { get; set; }       
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }

        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputSYSAccessdata
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string polmenu_code { get; set; }
        [DataMember]
        public bool accessdata_module { get; set; }
        [DataMember]
        public bool accessdata_new { get; set; }
        [DataMember]
        public bool accessdata_edit { get; set; }
        [DataMember]
        public bool accessdata_delete { get; set; }
        [DataMember]
        public bool accessdata_salary { get; set; }
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputSYSReportjob
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public int reportjob_id { get; set; }
        [DataMember]
        public string reportjob_ref { get; set; }
        [DataMember]
        public string reportjob_type { get; set; }
        [DataMember]
        public string reportjob_status { get; set; }
        [DataMember]
        public string reportjob_language { get; set; }
        [DataMember]
        public string reportjob_fromdate { get; set; }
        [DataMember]
        public string reportjob_todate { get; set; }
        [DataMember]
        public string reportjob_paydate { get; set; }

        [DataMember]
        public string reportjob_whose { get; set; }

        [DataMember]
        public string created_by { get; set; }
        
    }

}