using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HRFocusWCFSystem
{
    public class cls_DataContract_employeecs
    {
    }

    [DataContract]
    public class InputMTInitial
    {
        [DataMember]
        public int initial_id { get; set; }
        [DataMember]
        public string initial_code { get; set; }
        [DataMember]
        public string initial_name_th { get; set; }
        [DataMember]
        public string initial_name_en { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputMTPosition
    {
        [DataMember]
        public int position_id { get; set; }
        [DataMember]
        public string position_code { get; set; }
        [DataMember]
        public string position_name_th { get; set; }
        [DataMember]
        public string position_name_en { get; set; }
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
    public class InputMTDep
    {
        [DataMember]
        public int dep_id { get; set; }
        [DataMember]
        public string dep_code { get; set; }
        [DataMember]
        public string dep_name_th { get; set; }
        [DataMember]
        public string dep_name_en { get; set; }

        [DataMember]
        public string dep_parent { get; set; }

        [DataMember]
        public string dep_level { get; set; }

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
    public class InputMTWorker
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public int worker_id { get; set; }
        [DataMember]
        public string worker_code { get; set; }
        [DataMember]
        public string worker_card { get; set; }
        [DataMember]
        public string worker_initial { get; set; }
        [DataMember]
        public string worker_fname_th { get; set; }
        [DataMember]
        public string worker_lname_th { get; set; }
        [DataMember]
        public string worker_fname_en { get; set; }
        [DataMember]
        public string worker_lname_en { get; set; }

        [DataMember]
        public string worker_emptype { get; set; }
        [DataMember]
        public string worker_gender { get; set; }
        [DataMember]
        public string worker_birthdate { get; set; }
        [DataMember]
        public string worker_hiredate { get; set; }
        [DataMember]
        public string worker_resigndate { get; set; }
        [DataMember]
        public bool worker_resignstatus { get; set; }
        [DataMember]
        public string worker_resignreason { get; set; }

        [DataMember]
        public string worker_probationdate { get; set; }
        [DataMember]
        public string worker_probationenddate { get; set; }

        [DataMember]
        public double hrs_perday { get; set; }

        [DataMember]
        public string worker_taxmethod { get; set; }

        //-- Transaction
        [DataMember]
        public string card_data { get; set; }
        [DataMember]
        public string reduce_data { get; set; }
        [DataMember]
        public string salary_data { get; set; }
        [DataMember]
        public string family_data { get; set; }
        [DataMember]
        public string dep_data { get; set; }
        //--

        [DataMember]
        public bool self_admin { get; set; }

        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }

        [DataMember]
        public string worker_pwd { get; set; }
    }
    
    [DataContract]
    public class InputTREmpcard
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string worker_code { get; set; }

        [DataMember]
        public int empcard_id { get; set; }
        [DataMember]
        public string empcard_code { get; set; }
        [DataMember]
        public string card_type { get; set; }
        [DataMember]
        public string empcard_issue { get; set; }
        [DataMember]
        public string empcard_expire { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputTREmpreduce
    {
        [DataMember]
        public int empreduce_id { get; set; }
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string worker_code { get; set; }
        [DataMember]
        public string reduce_type { get; set; }
        [DataMember]
        public double empreduce_amount { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputTREmpfamily
    {
        [DataMember]
        public int empfamily_id { get; set; }
        [DataMember]
        public string empfamily_code { get; set; }
        [DataMember]
        public string family_type { get; set; }
        [DataMember]
        public string empfamily_fname_th { get; set; }
        [DataMember]
        public string empfamily_lname_th { get; set; }
        [DataMember]
        public string empfamily_fname_en { get; set; }
        [DataMember]
        public string empfamily_lname_en { get; set; }
        [DataMember]
        public string empfamily_birthdate { get; set; }

        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string worker_code { get; set; }

        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }

        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputTREmpsalary
    {
        [DataMember]
        public int empsalary_id { get; set; }
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string worker_code { get; set; }
        [DataMember]
        public double empsalary_amount { get; set; }
        [DataMember]
        public string empsalary_date { get; set; }
        [DataMember]
        public string empsalary_reason { get; set; }
        [DataMember]
        public double empsalary_incamount { get; set; }
        [DataMember]
        public double empsalary_incpercent { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }

        [DataMember]
        public string emp_data { get; set; }
    }

    [DataContract]
    public class InputTREmpbenefit
    {
        [DataMember]
        public int empbenefit_id { get; set; }
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string worker_code { get; set; }
        [DataMember]
        public string item_code { get; set; }
        [DataMember]
        public double empbenefit_amount { get; set; }
        [DataMember]
        public string empbenefit_startdate { get; set; }
        [DataMember]
        public string empbenefit_enddate { get; set; }
        [DataMember]
        public string empbenefit_reason { get; set; }
        [DataMember]
        public string empbenefit_note { get; set; }
        [DataMember]
        public string empbenefit_paytype { get; set; }
        [DataMember]
        public bool empbenefit_break { get; set; }
        [DataMember]
        public string empbenefit_breakreason { get; set; }

        [DataMember]
        public string empbenefit_conditionpay { get; set; }

        [DataMember]
        public string empbenefit_payfirst { get; set; }

        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }

        [DataMember]
        public string emp_data { get; set; }
    }

    [DataContract]
    public class InputTREmpdep
    {
        [DataMember]
        public int empdep_id { get; set; }
        [DataMember]
        public string empdep_date { get; set; }
        [DataMember]
        public string empdep_level01 { get; set; }
        [DataMember]
        public string empdep_level02 { get; set; }
        [DataMember]
        public string empdep_level03 { get; set; }
        [DataMember]
        public string empdep_level04 { get; set; }
        [DataMember]
        public string empdep_level05 { get; set; }
        [DataMember]
        public string empdep_level06 { get; set; }
        [DataMember]
        public string empdep_level07 { get; set; }
        [DataMember]
        public string empdep_level08 { get; set; }
        [DataMember]
        public string empdep_level09 { get; set; }
        [DataMember]
        public string empdep_level10 { get; set; }
        [DataMember]
        public string empdep_reason { get; set; }
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string worker_code { get; set; }

        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputTREmpposition
    {
        [DataMember]
        public int empposition_id { get; set; }
        [DataMember]
        public string empposition_date { get; set; }
        [DataMember]
        public string empposition_position { get; set; }
        [DataMember]
        public string empposition_reason { get; set; }
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string worker_code { get; set; }

        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputTREmpaddress
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string worker_code { get; set; }
        [DataMember]
        public string empaddress_type { get; set; }
        [DataMember]
        public string empaddress_no { get; set; }
        [DataMember]
        public string empaddress_moo { get; set; }
        [DataMember]
        public string empaddress_soi { get; set; }
        [DataMember]
        public string empaddress_road { get; set; }
        [DataMember]
        public string empaddress_tambon { get; set; }
        [DataMember]
        public string empaddress_amphur { get; set; }
        [DataMember]
        public string empaddress_zipcode { get; set; }
        [DataMember]
        public string empaddress_tel { get; set; }
        [DataMember]
        public string empaddress_email { get; set; }
        [DataMember]
        public string empaddress_line { get; set; }
        [DataMember]
        public string empaddress_facebook { get; set; }
        [DataMember]
        public string province_code { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public string modified_date { get; set; }
    }

    [DataContract]
    public class InputTREmpeducation
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string worker_code { get; set; }
        [DataMember]
        public int empeducation_no { get; set; }
        [DataMember]
        public string empeducation_gpa { get; set; }
        [DataMember]
        public string empeducation_start { get; set; }
        [DataMember]
        public string empeducation_finish { get; set; }
        [DataMember]
        public string institute_code { get; set; }
        [DataMember]
        public string faculty_code { get; set; }
        [DataMember]
        public string major_code { get; set; }
        [DataMember]
        public string qualification_code { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public string modified_date { get; set; }
    }

    [DataContract]
    public class InputTREmptraining
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string worker_code { get; set; }
        [DataMember]
        public int emptraining_no { get; set; }
        [DataMember]
        public DateTime emptraining_start { get; set; }
        [DataMember]
        public DateTime emptraining_finish { get; set; }
        [DataMember]
        public string emptraining_status { get; set; }
        [DataMember]
        public double emptraining_hours { get; set; }
        [DataMember]
        public double emptraining_cost { get; set; }
        [DataMember]
        public string emptraining_note { get; set; }
        [DataMember]
        public string institute_code { get; set; }
        [DataMember]
        public string institute_other { get; set; }
        [DataMember]
        public string course_code { get; set; }
        [DataMember]
        public string course_other { get; set; }
    }

    [DataContract]
    public class InputTREmpexperience
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string worker_code { get; set; }
        [DataMember]
        public int empexperience_no { get; set; }
        [DataMember]
        public string empexperience_at { get; set; }
        [DataMember]
        public string empexperience_position { get; set; }
        [DataMember]
        public string empexperience_reasonchange { get; set; }
        [DataMember]
        public string empexperience_start { get; set; }
        [DataMember]
        public string empexperience_finish { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public string modified_date { get; set; }
    }

    [DataContract]
    public class InputTREmpprovident
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string worker_code { get; set; }
        [DataMember]
        public string provident_code { get; set; }
        [DataMember]
        public string empprovident_card { get; set; }
        [DataMember]
        public string empprovident_entry { get; set; }
        [DataMember]
        public string empprovident_start { get; set; }
        [DataMember]
        public string empprovident_end { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public string modified_date { get; set; }
    }

    [DataContract]
    public class InputTREmplocation
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string worker_code { get; set; }
        [DataMember]
        public string location_code { get; set; }
        [DataMember]
        public string emplocation_startdate { get; set; }
        [DataMember]
        public string emplocation_enddate { get; set; }
        [DataMember]
        public string emplocation_note { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }

        [DataMember]
        public string emp_data { get; set; }
    }

    [DataContract]
    public class InputTREmpbank
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string worker_code { get; set; }
        [DataMember]
        public int empbank_id { get; set; }
        [DataMember]
        public string empbank_bankcode { get; set; }
        [DataMember]
        public string empbank_bankaccount { get; set; }
        [DataMember]
        public double empbank_bankpercent { get; set; }
        [DataMember]
        public double empbank_cashpercent { get; set; }        
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }

        [DataMember]
        public double empbank_bankname { get; set; }

    }


    [DataContract]
    public class FillterEmp
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string worker_id { get; set; }
        [DataMember]
        public string worker_code { get; set; }
        [DataMember]
        public string worker_card { get; set; }
        [DataMember]
        public string worker_initial { get; set; }
        [DataMember]
        public string worker_fname_th { get; set; }
        [DataMember]
        public string worker_lname_th { get; set; }
        [DataMember]
        public string worker_fname_en { get; set; }
        [DataMember]
        public string worker_lname_en { get; set; }
        [DataMember]
        public string worker_emptype { get; set; }
        [DataMember]
        public string worker_gender { get; set; }
        [DataMember]
        public bool include_resign { get; set; }
        [DataMember]
        public string level_code { get; set; }
        [DataMember]
        public string dep_code { get; set; }
        [DataMember]
        public string position_code { get; set; }
        [DataMember]
        public string group_code { get; set; }

        [DataMember]
        public string location_code { get; set; }

        [DataMember]
        public string date_fill { get; set; }      
        
    }


    [DataContract]
    public class InputTREmpawpt
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string worker_code { get; set; }
        [DataMember]
        public int empawpt_no { get; set; }
        [DataMember]
        public string empawpt_start { get; set; }
        [DataMember]
        public string empawpt_finish { get; set; }
        [DataMember]
        public string empawpt_type { get; set; }
        [DataMember]
        public string empawpt_location { get; set; }
        [DataMember]
        public string empawpt_reason { get; set; }
        [DataMember]
        public string empawpt_note { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }

    }


    [DataContract]
    public class InputTREmpkt20
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string worker_code { get; set; }
        [DataMember]
        public string year_code { get; set; }
        [DataMember]
        public double empkt20_rate { get; set; }        
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }
    }
    [DataContract]
    public class InputPackate
    {
        [DataMember]
        public string package_ref { get; set; }
        [DataMember]
        public string package_name { get; set; }
        [DataMember]
        public int package_com { get; set; }
        [DataMember]
        public string package_branch { get; set; }
        [DataMember]
        public string package_emp { get; set; }
        [DataMember]
        public string package_user { get; set; }
    }
    [DataContract]
    public class InputAllow
    {
        [DataMember]
        public string allow_ip { get; set; }
        [DataMember]
        public string allow_port { get; set; }
    }

}