﻿using System;
using System.Collections.Generic;
namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TRTimeleaveself
    {
        public cls_TRTimeleaveself() { }

        public string company_code { get; set; }
        public string worker_code { get; set; }


        public int timeleave_id { get; set; }
        public string timeleave_doc { get; set; }

        public DateTime timeleave_fromdate { get; set; }
        public DateTime timeleave_todate { get; set; }

        public string timeleave_type { get; set; }
        public int timeleave_min { get; set; }

        public int timeleave_actualday { get; set; }
        public bool timeleave_incholiday { get; set; }
        public bool timeleave_deduct { get; set; }

        public string timeleave_note { get; set; }
        public string leave_code { get; set; }
        public string reason_code { get; set; }
        public int status { get; set; }
        public string status_job { get; set; }

        public string created_by { get; set; }
        public DateTime created_date { get; set; }
        public string modified_by { get; set; }
        public DateTime modified_date { get; set; }
        public string reject_note { get; set; }
        public bool flag { get; set; }

        //-- Show only
        public string worker_detail_th { get; set; }
        public string leave_detail_th { get; set; }
        public string worker_detail_en { get; set; }
        public string leave_detail_en { get; set; }
        public string reason_th { get; set; }
        public string reason_en { get; set; }
        public string yaer_code { get; set; }
        public List<cls_MTReqdocument> reqdoc_data { get; set; }
    }
}
