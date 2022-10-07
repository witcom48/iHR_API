using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HRFocusWCFSystem
{
    public class cls_DataContract_attendance
    {
    }

    [DataContract]
    public class InputMTHoliday
    {
        [DataMember]
        public int holiday_id { get; set; }
        [DataMember]
        public string holiday_date { get; set; }
        [DataMember]
        public string holiday_name_th { get; set; }
        [DataMember]
        public string holiday_name_en { get; set; }
        [DataMember]
        public string year_code { get; set; }
        [DataMember]
        public string company_code { get; set; }

        [DataMember]
        public string holiday_daytype { get; set; }

        [DataMember]
        public double holiday_payper { get; set; }

        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }

        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputMTPlanshift
    {
        [DataMember]
        public int planshift_id { get; set; }
        [DataMember]
        public string planshift_code { get; set; }
        [DataMember]
        public string planshift_name_th { get; set; }
        [DataMember]
        public string planshift_name_en { get; set; }
        [DataMember]
        public string company_code { get; set; }

        //-- Transaction
        [DataMember]
        public string schedule_data { get; set; }


        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }

        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputTRPlanschedule
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string planshift_code { get; set; }
        [DataMember]
        public string planschedule_fromdate { get; set; }
        [DataMember]
        public string planschedule_todate { get; set; }
        [DataMember]
        public string shift_code { get; set; }

        [DataMember]
        public string planschedule_sun_off { get; set; }
        [DataMember]
        public string planschedule_mon_off { get; set; }
        [DataMember]
        public string planschedule_tue_off { get; set; }
        [DataMember]
        public string planschedule_wed_off { get; set; }
        [DataMember]
        public string planschedule_thu_off { get; set; }
        [DataMember]
        public string planschedule_fri_off { get; set; }
        [DataMember]
        public string planschedule_sat_off { get; set; }

        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }

        [DataMember]
        public int index { get; set; }
    }


    [DataContract]
    public class InputBatchPlanshift
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string planshift_code { get; set; }
        [DataMember]
        public string year_code { get; set; }
        [DataMember]
        public string transaction_data { get; set; }
        [DataMember]
        public string modified_by { get; set; }

    }

    public class InputTRTimecard
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string worker_code { get; set; }
        [DataMember]
        public string shift_code { get; set; }
        [DataMember]
        public string timecard_workdate { get; set; }
        [DataMember]
        public string timecard_daytype { get; set; }
        [DataMember]
        public string timecard_color { get; set; }

        [DataMember]
        public string timecard_ch1 { get; set; }
        [DataMember]
        public string timecard_ch2 { get; set; }
        [DataMember]
        public string timecard_ch3 { get; set; }
        [DataMember]
        public string timecard_ch4 { get; set; }
        [DataMember]
        public string timecard_ch5 { get; set; }
        [DataMember]
        public string timecard_ch6 { get; set; }
        [DataMember]
        public string timecard_ch7 { get; set; }
        [DataMember]
        public string timecard_ch8 { get; set; }
        [DataMember]
        public string timecard_ch9 { get; set; }
        [DataMember]
        public string timecard_ch10 { get; set; }

        [DataMember]
        public string timecard_in { get; set; }
        [DataMember]
        public string timecard_out { get; set; }

        [DataMember]
        public int timecard_before_min { get; set; }
        [DataMember]
        public int timecard_work1_min { get; set; }
        [DataMember]
        public int timecard_work2_min { get; set; }
        [DataMember]
        public int timecard_break_min { get; set; }
        [DataMember]
        public int timecard_after_min { get; set; }
        [DataMember]
        public int timecard_late_min { get; set; }

        [DataMember]
        public int timecard_before_min_app { get; set; }
        [DataMember]
        public int timecard_work1_min_app { get; set; }
        [DataMember]
        public int timecard_work2_min_app { get; set; }
        [DataMember]
        public int timecard_break_min_app { get; set; }
        [DataMember]
        public int timecard_after_min_app { get; set; }
        [DataMember]
        public int timecard_late_min_app { get; set; }

        [DataMember]
        public bool timecard_lock { get; set; }

        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }

        [DataMember]
        public int index { get; set; }


    }

    public class InputMTLeave
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string leave_id { get; set; }
        [DataMember]
        public string leave_code { get; set; }
        [DataMember]
        public string leave_name_th { get; set; }
        [DataMember]
        public string leave_name_en { get; set; }
        [DataMember]
        public double leave_day_peryear { get; set; }
        [DataMember]
        public double leave_day_acc { get; set; }
        [DataMember]
        public string leave_day_accexpire { get; set; }
        [DataMember]
        public string leave_incholiday { get; set; }
        [DataMember]
        public string leave_passpro { get; set; }
        [DataMember]
        public string leave_deduct { get; set; }
        [DataMember]
        public string leave_caldiligence { get; set; }
        [DataMember]
        public string leave_agework { get; set; }
        [DataMember]
        public int leave_ahead { get; set; }
        [DataMember]
        public string leave_min_hrs { get; set; }
        [DataMember]
        public int leave_max_day { get; set; }

        [DataMember]
        public string workage_data { get; set; }

        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }

        [DataMember]
        public int index { get; set; }

    }

    [DataContract]
    public class InputTRLeaveWorkage
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string leave_code { get; set; }
        [DataMember]
        public double workage_from { get; set; }
        [DataMember]
        public double workage_to { get; set; }
        [DataMember]
        public double workage_leaveday { get; set; }

    }
    
    [DataContract]
    public class InputMTLate
    {
        [DataMember]
        public int late_id { get; set; }
        [DataMember]
        public string late_code { get; set; }
        [DataMember]
        public string late_name_th { get; set; }
        [DataMember]
        public string late_name_en { get; set; }
        [DataMember]
        public string company_code { get; set; }

        [DataMember]
        public string late_data { get; set; }

        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }

        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputTRLate
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string late_code { get; set; }
        [DataMember]
        public string late_from { get; set; }
        [DataMember]
        public string late_to { get; set; }
        [DataMember]
        public string late_deduct_type { get; set; }
        [DataMember]
        public double late_deduct_amount { get; set; }

    }

    [DataContract]
    public class InputMTShift
    {
        [DataMember]
        public string company_code { get; set; }

        [DataMember]
        public int shift_id { get; set; }
        [DataMember]
        public string shift_code { get; set; }
        [DataMember]
        public string shift_name_th { get; set; }
        [DataMember]
        public string shift_name_en { get; set; }

        [DataMember]
        public string shift_ch1 { get; set; }
        [DataMember]
        public string shift_ch2 { get; set; }
        [DataMember]
        public string shift_ch3 { get; set; }
        [DataMember]
        public string shift_ch4 { get; set; }
        [DataMember]
        public string shift_ch5 { get; set; }
        [DataMember]
        public string shift_ch6 { get; set; }
        [DataMember]
        public string shift_ch7 { get; set; }
        [DataMember]
        public string shift_ch8 { get; set; }
        [DataMember]
        public string shift_ch9 { get; set; }
        [DataMember]
        public string shift_ch10 { get; set; }

        [DataMember]
        public string shift_ch3_from { get; set; }
        [DataMember]
        public string shift_ch3_to { get; set; }
        [DataMember]
        public string shift_ch4_from { get; set; }
        [DataMember]
        public string shift_ch4_to { get; set; }

        [DataMember]
        public string shift_ch7_from { get; set; }
        [DataMember]
        public string shift_ch7_to { get; set; }
        [DataMember]
        public string shift_ch8_from { get; set; }
        [DataMember]
        public string shift_ch8_to { get; set; }

        [DataMember]
        public int shift_otin_min { get; set; }
        [DataMember]
        public int shift_otin_max { get; set; }

        [DataMember]
        public int shift_otout_min { get; set; }
        [DataMember]
        public int shift_otout_max { get; set; }

        [DataMember]
        public bool shift_flexiblebreak { get; set; }

        [DataMember]
        public string shiftallowance_data { get; set; }

        [DataMember]
        public string break_data { get; set; }

        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }

        [DataMember]
        public int index { get; set; }
    }
    
    [DataContract]
    public class InputMTRateot
    {        
        [DataMember]
        public int rateot_id { get; set; }
        [DataMember]
        public string rateot_code { get; set; }
        [DataMember]
        public string rateot_name_th { get; set; }
        [DataMember]
        public string rateot_name_en { get; set; }
        [DataMember]
        public string company_code { get; set; }

        [DataMember]
        public string rateot_data { get; set; }

        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputTRRateot
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string rateot_code { get; set; }
        [DataMember]
        public string rateot_daytype { get; set; }
        [DataMember]
        public double rateot_before { get; set; }
        [DataMember]
        public double rateot_normal { get; set; }
        [DataMember]
        public double rateot_break { get; set; }
        [DataMember]
        public double rateot_after { get; set; }
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputMTPlanleave
    {
        [DataMember]
        public int planleave_id { get; set; }
        [DataMember]
        public string planleave_code { get; set; }
        [DataMember]
        public string planleave_name_th { get; set; }
        [DataMember]
        public string planleave_name_en { get; set; }
        [DataMember]
        public string company_code { get; set; }

        [DataMember]
        public string planleave_data { get; set; }

        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputTRPlanleave
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string planleave_code { get; set; }
        [DataMember]
        public string leave_code { get; set; }        
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputMTDiligence
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public int diligence_id { get; set; }
        [DataMember]
        public string diligence_code { get; set; }
        [DataMember]
        public string diligence_name_th { get; set; }
        [DataMember]
        public string diligence_name_en { get; set; }
        [DataMember]
        public string diligence_punchcard { get; set; }
        [DataMember]
        public int diligence_punchcard_times { get; set; }
        [DataMember]
        public int diligence_punchcard_timespermonth { get; set; }
        [DataMember]
        public string diligence_late { get; set; }
        [DataMember]
        public int diligence_late_times { get; set; }
        [DataMember]
        public int diligence_late_timespermonth { get; set; }
        [DataMember]
        public int diligence_late_acc { get; set; }

        [DataMember]
        public string diligence_ba { get; set; }
        [DataMember]
        public int diligence_before_min { get; set; }
        [DataMember]
        public int diligence_after_min { get; set; }

        [DataMember]
        public string diligence_passpro { get; set; }
        [DataMember]
        public string diligence_wrongcondition { get; set; }
        [DataMember]
        public string diligence_someperiod { get; set; }
        [DataMember]
        public string diligence_someperiod_first { get; set; }

        [DataMember]
        public string steppay_data { get; set; }

        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputTRDiligenceSteppay
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string diligence_code { get; set; }
        [DataMember]
        public int steppay_step { get; set; }
        [DataMember]
        public string steppay_type { get; set; }
        [DataMember]
        public double steppay_amount { get; set; }       
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputTRShiftallowance
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string shift_code { get; set; }
        [DataMember]
        public int shiftallowance_no { get; set; }
        [DataMember]
        public string shiftallowance_name_th { get; set; }
        [DataMember]
        public string shiftallowance_name_en { get; set; }
        [DataMember]
        public string shiftallowance_hhmm { get; set; }
        [DataMember]
        public double shiftallowance_amount { get; set; }
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputTRShiftbreak
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string shift_code { get; set; }
        [DataMember]
        public int shiftbreak_no { get; set; }
        [DataMember]
        public string shiftbreak_from { get; set; }
        [DataMember]
        public string shiftbreak_to { get; set; }
        [DataMember]
        public int shiftbreak_break { get; set; }
        [DataMember]
        public int index { get; set; }
    }
    
    public class InputTREmppolatt
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string worker_code { get; set; }
        [DataMember]
        public string emppolatt_policy_code { get; set; }
        [DataMember]
        public string emppolatt_policy_type { get; set; }
        [DataMember]
        public string emppolatt_policy_note { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputSetPolicyAtt
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string pol_code { get; set; }
        [DataMember]
        public string pol_type { get; set; }
        [DataMember]
        public string pol_note { get; set; }
        [DataMember]
        public string emp_data { get; set; }
        [DataMember]
        public string modified_by { get; set; }

    }

    [DataContract]
    public class InputSetPolicyAttItem
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string item_sa { get; set; }
        [DataMember]
        public string item_ot { get; set; }
        [DataMember]
        public string item_aw { get; set; }
        [DataMember]
        public string item_dg { get; set; }
        [DataMember]
        public string item_lv { get; set; }
        [DataMember]
        public string item_ab { get; set; }
        [DataMember]
        public string item_lt { get; set; }        
        [DataMember]
        public string emp_data { get; set; }
        [DataMember]
        public string modified_by { get; set; }

    }

    [DataContract]
    public class InputTRAttwageday
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string worker_code { get; set; }
        [DataMember]
        public string wageday_date { get; set; }
        [DataMember]
        public string wageday_daytype { get; set; }
        [DataMember]
        public double wageday_wagemoney { get; set; }
        [DataMember]
        public string wageday_wagehhmm { get; set; }
        [DataMember]
        public double wageday_latemoney { get; set; }
        [DataMember]
        public string wageday_latehhmm { get; set; }
        [DataMember]
        public double wageday_leavemoney { get; set; }
        [DataMember]
        public string wageday_leavehhmm { get; set; }
        [DataMember]
        public double wageday_absentmoney { get; set; }
        [DataMember]
        public string wageday_absenthhmm { get; set; }
        [DataMember]
        public double wageday_ot1money { get; set; }
        [DataMember]
        public double wageday_ot15money { get; set; }
        [DataMember]
        public double wageday_ot2money { get; set; }
        [DataMember]
        public double wageday_ot3money { get; set; }
        [DataMember]
        public string wageday_ot1hhmm { get; set; }
        [DataMember]
        public string wageday_ot15hhmm { get; set; }
        [DataMember]
        public string wageday_ot2hhmm { get; set; }
        [DataMember]
        public string wageday_ot3hhmm { get; set; }
        [DataMember]
        public double wageday_allowance { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }

    }

    [DataContract]
    public class InputTRTimeinput
    {
        [DataMember]
        public string timeinput_card { get; set; }
        [DataMember]
        public string timeinput_date { get; set; }
        [DataMember]
        public string timeinput_hhmm { get; set; }
        [DataMember]
        public string timeinput_terminal { get; set; }
        [DataMember]
        public string timeinput_function { get; set; }
        [DataMember]
        public string timeinput_compare { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public int index { get; set; }

    }

    [DataContract]
    public class InputMTTimeimpformat
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string date_format { get; set; }
        [DataMember]
        public int card_start { get; set; }
        [DataMember]
        public int card_lenght { get; set; }
        [DataMember]
        public int date_start { get; set; }
        [DataMember]
        public int date_lenght { get; set; }
        [DataMember]
        public int hours_start { get; set; }
        [DataMember]
        public int hours_lenght { get; set; }
        [DataMember]
        public int minute_start { get; set; }
        [DataMember]
        public int minute_lenght { get; set; }
        [DataMember]
        public int function_start { get; set; }
        [DataMember]
        public int function_lenght { get; set; }
        [DataMember]
        public int machine_start { get; set; }
        [DataMember]
        public int machine_lenght { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }

    }

    [DataContract]
    public class InputTRTimedoc
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string worker_code { get; set; }
        [DataMember]
        public DateTime timedoc_workdate { get; set; }
        [DataMember]
        public string timedoc_doctype { get; set; }
        [DataMember]
        public string timedoc_docno { get; set; }
        [DataMember]
        public string timedoc_value1 { get; set; }
        [DataMember]
        public string timedoc_value2 { get; set; }
        [DataMember]
        public string timedoc_value3 { get; set; }
        [DataMember]
        public string timedoc_value4 { get; set; }
        [DataMember]
        public string timedoc_reasoncode { get; set; }
        [DataMember]
        public string timedoc_note { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }

    }

    [DataContract]
    public class InputTRTimeleave
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string worker_code { get; set; }
        [DataMember]
        public string worker_name { get; set; }
        [DataMember]
        public int timeleave_id { get; set; }
        [DataMember]
        public string timeleave_doc { get; set; }
        [DataMember]
        public string timeleave_fromdate { get; set; }
        [DataMember]
        public string timeleave_todate { get; set; }
        [DataMember]
        public string timeleave_type { get; set; }
        [DataMember]
        public int timeleave_min { get; set; }
        [DataMember]
        public int timeleave_actualday { get; set; }
        [DataMember]
        public bool timeleave_incholiday { get; set; }
        [DataMember]
        public bool timeleave_deduct { get; set; }
        [DataMember]
        public string timeleave_note { get; set; }
        [DataMember]
        public string leave_code { get; set; }
        [DataMember]
        public string reason_code { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputTRTimeot
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string worker_code { get; set; }
        [DataMember]
        public string worker_name { get; set; }
        [DataMember]
        public int timeot_id { get; set; }
        [DataMember]
        public string timeot_doc { get; set; }
        [DataMember]
        public string timeot_workdate { get; set; }
        [DataMember]
        public int timeot_beforemin { get; set; }
        [DataMember]
        public int timeot_normalmin { get; set; }
        [DataMember]
        public int timeot_aftermin { get; set; }
        [DataMember]
        public string timeot_note { get; set; }
        [DataMember]
        public string location_code { get; set; }
        [DataMember]
        public string reason_code { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputTRTimeonsite
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string worker_code { get; set; }
        [DataMember]
        public string worker_name { get; set; }
        [DataMember]
        public int timeonsite_id { get; set; }
        [DataMember]
        public string timeonsite_doc { get; set; }
        [DataMember]
        public string timeonsite_workdate { get; set; }
        [DataMember]
        public string timeonsite_in { get; set; }
        [DataMember]
        public string timeonsite_out { get; set; }
        [DataMember]
        public string timeonsite_note { get; set; }
        [DataMember]
        public string location_code { get; set; }
        [DataMember]
        public string reason_code { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputTRTimeshift
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string worker_code { get; set; }
        [DataMember]
        public string worker_name { get; set; }
        [DataMember]
        public int timeshift_id { get; set; }
        [DataMember]
        public string timeshift_doc { get; set; }
        [DataMember]
        public string timeshift_workdate { get; set; }
        [DataMember]
        public string timeshift_old { get; set; }
        [DataMember]
        public string timeshift_new { get; set; }
        [DataMember]
        public string timeshift_note { get; set; }
        [DataMember]
        public string reason_code { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputTRTimedaytype
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string worker_code { get; set; }
        [DataMember]
        public string worker_name { get; set; }
        [DataMember]
        public int timedaytype_id { get; set; }
        [DataMember]
        public string timedaytype_doc { get; set; }
        [DataMember]
        public string timedaytype_workdate { get; set; }
        [DataMember]
        public string timedaytype_old { get; set; }
        [DataMember]
        public string timedaytype_new { get; set; }
        [DataMember]
        public string timedaytype_note { get; set; }
        [DataMember]
        public string reason_code { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputTREmpleaveacc
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string worker_code { get; set; }
        [DataMember]
        public string leave_code { get; set; }
        [DataMember]
        public string year_code { get; set; }
        [DataMember]
        public double empleaveacc_bf { get; set; }
        [DataMember]
        public double empleaveacc_annual { get; set; }
        [DataMember]
        public double empleaveacc_used { get; set; }
        [DataMember]
        public double empleaveacc_remain { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }
    }


    public class InputMTPlanholiday
    {
        [DataMember]
        public int planholiday_id { get; set; }
        [DataMember]
        public string planholiday_code { get; set; }
        [DataMember]
        public string planholiday_name_th { get; set; }
        [DataMember]
        public string planholiday_name_en { get; set; }
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string year_code { get; set; }

        [DataMember]
        public string holiday_data { get; set; }

        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }

    }

    public class InputMTPlantimeallw
    {   
        [DataMember]
        public int plantimeallw_id { get; set; }
        [DataMember]
        public string plantimeallw_code { get; set; }
        [DataMember]
        public string plantimeallw_name_th { get; set; }
        [DataMember]
        public string plantimeallw_name_en { get; set; }

        [DataMember]
        public string plantimeallw_passpro { get; set; }
        [DataMember]
        public string plantimeallw_lastperiod { get; set; }

        [DataMember]
        public string timeallw_data { get; set; }

        [DataMember]
        public string company_code { get; set; }

        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }

    }

    public class InputMTPlanshiftflexible
    {
        [DataMember]
        public int planshiftflexible_id { get; set; }
        [DataMember]
        public string planshiftflexible_code { get; set; }
        [DataMember]
        public string planshiftflexible_name_th { get; set; }
        [DataMember]
        public string planshiftflexible_name_en { get; set; }
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string year_code { get; set; }

        [DataMember]
        public string shiftflexible_data { get; set; }

        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }

    }
    

}