using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using ClassLibrary_BPC.hrfocus.model;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
namespace ClassLibrary_BPC.hrfocus.controller
{
   public class cls_ctApprovegetmail
    {
         string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctApprovegetmail() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }
        public string Approvesendmail(ref int appcount,string com, string job_type, string username, string workflow, string jobtableid, string fromdate, string todate, string docdetail)
        {
            if (jobtableid.Equals("0"))
            {
                return "";
            }
         System.Text.StringBuilder obj_str = new System.Text.StringBuilder();
         obj_str.Append("DECLARE @count INT ");
         obj_str.Append("EXEC [dbo].[SELF_MT_APPROVESENDMAIL] ");
         obj_str.Append("@CompID = '" + com + "'");
         obj_str.Append(", @JobType = '" + job_type + "'");
         obj_str.Append(", @Username = '" + username + "'");
         obj_str.Append(", @Workflow = '" + workflow + "'");
         obj_str.Append(", @Jobtableid = '" + jobtableid + "'");
         DataTable dt = Obj_conn.doGetTable(obj_str.ToString());
         foreach (DataRow dr in dt.Rows)
         {
             try
             {
                 appcount++;
                 if (!Convert.ToBoolean(dr["ACCOUNT_EMAIL_ALERT"]))
                 {
                     continue;
                 }
                 cls_ctMTWorker controller = new cls_ctMTWorker();
                 cls_MTWorker app = controller.doLogin(dr["ACCOUNT_USER"].ToString(), "");
                 cls_MTWorker user = controller.doLogin(username, "");
                 string supervisorName = app.initial_name_th + " " + app.worker_fname_th + " " + app.worker_lname_th;
                 string employeeName = user.initial_name_th + " " + user.worker_fname_th + " " + user.worker_lname_th;
                 string startDate = fromdate;
                 string endDate = todate;
                 string requestLetter = "";
                 if (job_type.Equals("LEA"))
                 {
                     cls_ctMTLeave leave = new cls_ctMTLeave();
                     List<cls_MTLeave> leavetype = leave.getDataByFillter(com, "", docdetail);
                     requestLetter = string.Format("เรียน {3}<br /><br />" +
                                         "{0} ได้ยื่นคำร้อง{4} ตั้งแต่วันที่ {1} ถึงวันที่ {2}<br /><br />" +
                                         "กรุณาพิจารณาอนุมัติ/ปฏิเสธคำร้องภายใน 2 วันทำการ<br /><br />" +
                                         "https://83.118.28.242:8805" +
                                         "<br /><br />ขอบคุณค่ะ/ครับ",
                                         employeeName, startDate, endDate, supervisorName, leavetype[0].leave_name_th);
                 }
                 if (job_type.Equals("OT"))
                 {
                     requestLetter = string.Format("เรียน {3}<br /><br />" +
                                         "{0} ได้ยื่นคำร้องขอทำงานล่วงเวลา จำนวน {4} ชั่วโมง ตั้งแต่วันที่ {1} ถึงวันที่ {2}<br /><br />" +
                                         "กรุณาพิจารณาอนุมัติ/ปฏิเสธคำร้องภายใน 2 วันทำการ<br /><br />" +
                                         "https://83.118.28.242:8805" +
                                         "<br /><br />ขอบคุณค่ะ/ครับ",
                                         employeeName, startDate, endDate, supervisorName, docdetail);
                 }
                 this.SendEmailAsync(requestLetter, dr["ACCOUNT_EMAIL"].ToString(), com);
             }
             catch (Exception ex)
             {
                 Message = "ERROR::(TRTRAccountpos.getDataworkflow)" + ex.ToString();

                 return Message;
             }
         }
         return "";
     }
     public async Task SendEmailAsync(string body, string toMail, string com)
     {
         try
         {
             var mailConfig = GetMailConfig(com);
             if (mailConfig != null)
             {
                 using (var message = CreateEmailMessage(body, toMail, mailConfig))
                 {
                     await SendEmailMessageAsync(message, mailConfig);
                 }
             }
         }
         catch (Exception ex)
         {
             HandleEmailError(ex);
         }
     }

     public async Task SendEmailMessageAsync(MailMessage message, cls_MTMailconfig mailConfig)
     {
         using (var smtpClient = new SmtpClient(mailConfig.mail_server, Convert.ToInt32(mailConfig.mail_serverport)))
         {
             smtpClient.EnableSsl = true;
             smtpClient.Credentials = new System.Net.NetworkCredential(mailConfig.mail_login, mailConfig.mail_password);
             System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)(3072);
             await smtpClient.SendMailAsync(message);
         }
     }
     //public void SendEmail(string body, string toMail, string com)
     //{
     //    try
     //    {
     //        var mailConfig = GetMailConfig(com);
     //        if (mailConfig != null)
     //        {
     //            using (var message = CreateEmailMessage(body, toMail, mailConfig))
     //            {
     //                SendEmailMessage(message, mailConfig);
     //            }
     //        }
     //    }
     //    catch (Exception ex)
     //    {
     //        HandleEmailError(ex);
     //    }
     //}

     private cls_MTMailconfig GetMailConfig(string com)
     {
         var controller = new cls_ctMTMailconfig();
         var mailConfigs = controller.getDataByFillter(com, 0);
         return mailConfigs.Count > 0 ? mailConfigs[0] : null;
     }

     private MailMessage CreateEmailMessage(string body, string toMail, cls_MTMailconfig mailConfig)
     {
         var message = new MailMessage
         {
             From = new MailAddress(mailConfig.mail_login, "IHR APT"),
             To = { toMail },
             IsBodyHtml = true,
             Body = body,
             Subject = "Self Services"
         };
         return message;
     }
     private void HandleEmailError(Exception ex)
     {
         Message += " | ERROR::(Approve from send mail) " + ex.ToString();
     }
    }

    }
