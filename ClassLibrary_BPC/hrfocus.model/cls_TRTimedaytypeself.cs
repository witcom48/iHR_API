﻿using System;
using System.Collections.Generic;
namespace ClassLibrary_BPC.hrfocus.model
{
    public class cls_TRTimedaytypeself
    {
        public cls_TRTimedaytypeself() { }

        public string company_code { get; set; }
        public string worker_code { get; set; }
        public int timedaytype_id { get; set; }
        public string timedaytype_doc { get; set; }
        public DateTime timedaytype_workdate { get; set; }
        public string timedaytype_old { get; set; }
        public string timedaytype_new { get; set; }
        public string timedaytype_note { get; set; }
        public string reason_code { get; set; }
        public int status { get; set; }
        public string created_by { get; set; }
        public DateTime created_date { get; set; }
        public string modified_by { get; set; }
        public DateTime modified_date { get; set; }
        public bool flag { get; set; }

        public string worker_detail_th { get; set; }
        public string worker_detail_en { get; set; }
        public string reason_name_th { get; set; }
        public string reason_name_en { get; set; }
        public string status_job { get; set; }

        public List<cls_MTReqdocument> reqdoc_data { get; set; }
    }
}
