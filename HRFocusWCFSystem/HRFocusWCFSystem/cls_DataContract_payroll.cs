using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using ClassLibrary_BPC.hrfocus.model;

namespace HRFocusWCFSystem
{
    public class cls_DataContract_payroll
    {
    }

    [DataContract]
    public class InputMTItem
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public int item_id { get; set; }
        [DataMember]
        public string item_code { get; set; }
        [DataMember]
        public string item_name_th { get; set; }
        [DataMember]
        public string item_name_en { get; set; }
        [DataMember]
        public string item_type { get; set; }
        [DataMember]
        public string item_regular { get; set; }
        [DataMember]
        public string item_caltax { get; set; }
        [DataMember]
        public string item_calpf { get; set; }
        [DataMember]
        public string item_calsso { get; set; }
        [DataMember]
        public string item_calot { get; set; }

        [DataMember]
        public string item_calallw { get; set; }

        [DataMember]
        public string item_contax { get; set; }
        [DataMember]
        public string item_section { get; set; }
        [DataMember]
        public double item_rate { get; set; }
        [DataMember]
        public string item_account { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }

        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputMTProvident
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public int provident_id { get; set; }
        [DataMember]
        public string provident_code { get; set; }
        [DataMember]
        public string provident_name_th { get; set; }
        [DataMember]
        public string provident_name_en { get; set; }
        
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
    public class InputTRProvidentWorkage
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string provident_code { get; set; }
        [DataMember]
        public double workage_from { get; set; }
        [DataMember]
        public double workage_to { get; set; }
        [DataMember]
        public double rate_emp { get; set; }
        [DataMember]
        public double rate_com { get; set; }

    }

    [DataContract]
    public class InputTRPayitem
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string worker_code { get; set; }
        [DataMember]
        public string item_code { get; set; }
        [DataMember]
        public string payitem_date { get; set; }
        [DataMember]
        public double payitem_amount { get; set; }
        [DataMember]
        public double payitem_quantity { get; set; }
        [DataMember]
        public string payitem_paytype { get; set; }
        [DataMember]
        public string payitem_note { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public string modified_date { get; set; }
    }

    [DataContract]
    public class InputTRPaytran
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string worker_code { get; set; }
        [DataMember]
        public DateTime paytran_date { get; set; }
        [DataMember]
        public double paytran_ssoemp { get; set; }
        [DataMember]
        public double paytran_ssocom { get; set; }
        [DataMember]
        public double paytran_ssorateemp { get; set; }
        [DataMember]
        public double paytran_ssoratecom { get; set; }
        [DataMember]
        public double paytran_pfemp { get; set; }
        [DataMember]
        public double paytran_pfcom { get; set; }
        [DataMember]
        public double paytran_income_401 { get; set; }
        [DataMember]
        public double paytran_deduct_401 { get; set; }
        [DataMember]
        public double paytran_tax_401 { get; set; }
        [DataMember]
        public double paytran_income_4012 { get; set; }
        [DataMember]
        public double paytran_deduct_4012 { get; set; }
        [DataMember]
        public double paytran_tax_4012 { get; set; }
        [DataMember]
        public double paytran_income_4013 { get; set; }
        [DataMember]
        public double paytran_deduct_4013 { get; set; }
        [DataMember]
        public double paytran_tax_4013 { get; set; }
        [DataMember]
        public double paytran_income_402I { get; set; }
        [DataMember]
        public double paytran_deduct_402I { get; set; }
        [DataMember]
        public double paytran_tax_402I { get; set; }
        [DataMember]
        public double paytran_income_402O { get; set; }
        [DataMember]
        public double paytran_deduct_402O { get; set; }
        [DataMember]
        public double paytran_tax_402O { get; set; }
        [DataMember]
        public double paytran_income_notax { get; set; }
        [DataMember]
        public double paytran_deduct_notax { get; set; }
        [DataMember]
        public double paytran_income_total { get; set; }
        [DataMember]
        public double paytran_deduct_total { get; set; }
        [DataMember]
        public double paytran_netpay_b { get; set; }
        [DataMember]
        public double paytran_netpay_c { get; set; }        

        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public string modified_date { get; set; }

        [DataMember]
        public string language { get; set; }
        [DataMember]
        public string com { get; set; }
        [DataMember]
        public List<cls_MTWorker> emp_data { get; set; }
        [DataMember]
        public string fromdate { get; set; }
        [DataMember]
        public string todate { get; set; }
    }

    [DataContract]
    public class InputTRPayreduce
    {        
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string worker_code { get; set; }
        [DataMember]
        public string payreduce_paydate { get; set; }
        [DataMember]
        public string reduce_code { get; set; }
        [DataMember]
        public double payreduce_amount { get; set; }

        [DataMember]
        public string reduce_name_th { get; set; }
        [DataMember]
        public string reduce_name_en { get; set; }

        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputMTBonus
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public int bonus_id { get; set; }
        [DataMember]
        public string bonus_code { get; set; }
        [DataMember]
        public string bonus_name_th { get; set; }
        [DataMember]
        public string bonus_name_en { get; set; }
        [DataMember]
        public string item_code { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }

        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputTRBonusrate
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string bonus_code { get; set; }
        [DataMember]
        public double bonusrate_from { get; set; }
        [DataMember]
        public double bonusrate_to { get; set; }
        [DataMember]
        public double bonusrate_rate { get; set; }
        
        [DataMember]
        public int index { get; set; }
    }

    [DataContract]
    public class InputTRPaybonus
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string worker_code { get; set; }        
        [DataMember]
        public string paybonus_date { get; set; }
        [DataMember]
        public double paybonus_amount { get; set; }
        [DataMember]
        public double paybonus_quantity { get; set; }
        [DataMember]
        public double paybonus_rate { get; set; }
        [DataMember]
        public double paybonus_tax { get; set; }
        [DataMember]
        public string paybonus_note { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public string modified_date { get; set; }
    }

    public class InputTRPaypolbonus
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string worker_code { get; set; }
        [DataMember]
        public string worker_detail { get; set; }
        [DataMember]
        public string paypolbonus_code { get; set; }        
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public DateTime modified_date { get; set; }
        [DataMember]
        public int index { get; set; }
    }


    [DataContract]
    public class InputTRPaytranAcc
    {
        [DataMember]
        public string company_code { get; set; }
        [DataMember]
        public string worker_code { get; set; }
        [DataMember]
        public DateTime paytran_date { get; set; }
        [DataMember]
        public double paytran_ssoemp { get; set; }
        [DataMember]
        public double paytran_ssocom { get; set; }        
        [DataMember]
        public double paytran_pfemp { get; set; }
        [DataMember]
        public double paytran_pfcom { get; set; }
        [DataMember]
        public double paytran_income_401 { get; set; }        
        [DataMember]
        public double paytran_tax_401 { get; set; }
        [DataMember]
        public double paytran_income_4012 { get; set; }        
        [DataMember]
        public double paytran_tax_4012 { get; set; }
        [DataMember]
        public double paytran_income_4013 { get; set; }        
        [DataMember]
        public double paytran_tax_4013 { get; set; }
        [DataMember]
        public double paytran_income_402I { get; set; }        
        [DataMember]
        public double paytran_tax_402I { get; set; }
        [DataMember]
        public double paytran_income_402O { get; set; }        
        [DataMember]
        public double paytran_tax_402O { get; set; }
        [DataMember]
        public string modified_by { get; set; }
        [DataMember]
        public string modified_date { get; set; }
    }
}