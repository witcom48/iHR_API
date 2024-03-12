﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using ClassLibrary_BPC.hrfocus.model;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
namespace ClassLibrary_BPC.hrfocus.controller
{
    public class cls_ctMTAccount
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctMTAccount() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_MTAccount> getData(string condition)
        {
            List<cls_MTAccount> list_model = new List<cls_MTAccount>();
            cls_MTAccount model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append(" COMPANY_CODE");
                obj_str.Append(", ACCOUNT_ID");
                obj_str.Append(", ACCOUNT_USER");
                obj_str.Append(", ACCOUNT_PWD");
                obj_str.Append(", ACCOUNT_TYPE");
                obj_str.Append(", ACCOUNT_LEVEL");

                obj_str.Append(", ACCOUNT_EMAIL");
                obj_str.Append(", ACCOUNT_EMAIL_ALERT");
                obj_str.Append(", ACCOUNT_LINE");
                obj_str.Append(", ACCOUNT_LINE_ALERT");

                obj_str.Append(", ISNULL(POLMENU_CODE, '') AS POLMENU_CODE");
                obj_str.Append(", ISNULL(MODIFIED_BY, CREATED_BY) AS MODIFIED_BY");
                obj_str.Append(", ISNULL(MODIFIED_DATE, CREATED_DATE) AS MODIFIED_DATE");
                obj_str.Append(", FLAG");

                obj_str.Append(" FROM SELF_MT_ACCOUNT");

                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_MTAccount();

                    model.company_code = dr["COMPANY_CODE"].ToString();
                    model.account_id = Convert.ToInt32(dr["ACCOUNT_ID"]);
                    model.account_user = dr["ACCOUNT_USER"].ToString();
                    model.account_pwd = this.Decrypt(dr["ACCOUNT_PWD"].ToString());

                    model.account_type = dr["ACCOUNT_TYPE"].ToString();
                    model.account_level = Convert.ToInt32(dr["ACCOUNT_LEVEL"].ToString());
                    model.account_email = dr["ACCOUNT_EMAIL"].ToString();
                    model.account_email_alert = Convert.ToBoolean(dr["ACCOUNT_EMAIL_ALERT"].ToString());
                    model.account_line = dr["ACCOUNT_LINE"].ToString();
                    model.account_line_alert = Convert.ToBoolean(dr["ACCOUNT_LINE_ALERT"].ToString());
                    model.polmenu_code = dr["POLMENU_CODE"].ToString();
                    model.flag = Convert.ToBoolean(dr["FLAG"].ToString());

                    model.modified_by = dr["MODIFIED_BY"].ToString();
                    model.modified_date = Convert.ToDateTime(dr["MODIFIED_DATE"]);

                    list_model.Add(model);
                }

            }
            catch (Exception ex)
            {
                Message = "ERROR::(Account.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_MTAccount> getDataByFillter(string com, string user, string type, int id, string typenotin)
        {
            string strCondition = "";
            if (!com.Equals(""))
                strCondition += " AND COMPANY_CODE='" + com + "'";
            if (!user.Equals(""))
                strCondition += " AND ACCOUNT_USER='" + user + "'";
            if (!type.Equals(""))
                strCondition += " AND ACCOUNT_TYPE='" + type + "'";
            if (!id.Equals(0))
                strCondition += " AND ACCOUNT_ID='" + id + "'";
            if (!typenotin.Equals(""))
                strCondition += " AND ACCOUNT_TYPE NOT IN (" + typenotin + ")";

            return this.getData(strCondition);
        }
        public List<cls_MTAccount> getDatabyworker(string com, string worker)
        {
            List<cls_MTAccount> list_model = new List<cls_MTAccount>();
            cls_MTAccount model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");
                obj_str.Append(" SELF_MT_ACCOUNT.*");
                obj_str.Append(" FROM SELF_TR_ACCOUNT");
                obj_str.Append(" JOIN SELF_MT_ACCOUNT ON SELF_MT_ACCOUNT.COMPANY_CODE = SELF_TR_ACCOUNT.COMPANY_CODE");
                obj_str.Append(" AND SELF_MT_ACCOUNT.ACCOUNT_USER = SELF_TR_ACCOUNT.ACCOUNT_USER");
                obj_str.Append(" WHERE SELF_TR_ACCOUNT.ACCOUNT_TYPE = 'Emp'");
                obj_str.Append(" AND SELF_MT_ACCOUNT.ACCOUNT_EMAIL_ALERT = 1");
                obj_str.Append(" AND SELF_TR_ACCOUNT.WORKER_CODE = '" + worker + "'");
                obj_str.Append(" AND SELF_TR_ACCOUNT.COMPANY_CODE = '" + com + "'");
                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_MTAccount();

                    model.company_code = dr["COMPANY_CODE"].ToString();
                    model.account_id = Convert.ToInt32(dr["ACCOUNT_ID"]);
                    model.account_user = dr["ACCOUNT_USER"].ToString();
                    //model.account_pwd = this.Decrypt(dr["ACCOUNT_PWD"].ToString());

                    model.account_type = dr["ACCOUNT_TYPE"].ToString();
                    model.account_level = Convert.ToInt32(dr["ACCOUNT_LEVEL"].ToString());
                    model.account_email = dr["ACCOUNT_EMAIL"].ToString();
                    model.account_email_alert = Convert.ToBoolean(dr["ACCOUNT_EMAIL_ALERT"].ToString());
                    model.account_line = dr["ACCOUNT_LINE"].ToString();
                    model.account_line_alert = Convert.ToBoolean(dr["ACCOUNT_LINE_ALERT"].ToString());
                    model.polmenu_code = dr["POLMENU_CODE"].ToString();
                    model.flag = Convert.ToBoolean(dr["FLAG"].ToString());

                    model.modified_by = dr["MODIFIED_BY"].ToString();
                    model.modified_date = Convert.ToDateTime(dr["MODIFIED_DATE"]);

                    list_model.Add(model);
                }

            }
            catch (Exception ex)
            {
                Message = "ERROR::(Account.getData)" + ex.ToString();
            }

            return list_model;
        }
        public bool checkDataOld(string com, string user, string type)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT COMPANY_CODE");
                obj_str.Append(" FROM SELF_MT_ACCOUNT");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND ACCOUNT_USER='" + user + "'");
                obj_str.Append(" AND ACCOUNT_TYPE='" + type + "'");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Account.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }
        public bool delete(string com, string user, string type, int id)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM SELF_MT_ACCOUNT");
                obj_str.Append(" WHERE COMPANY_CODE='" + com + "'");
                obj_str.Append(" AND ACCOUNT_USER='" + user + "'");
                obj_str.Append(" AND ACCOUNT_TYPE='" + type + "'");
                obj_str.Append(" AND ACCOUNT_ID='" + id + "'");

                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Account.delete)" + ex.ToString();
            }

            return blnResult;
        }
        public int getCountTypeAccount(string com,string worker)
        {
            int intResult = 0;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT TOP 1 COUNT(SELF_MT_ACCOUNT.ACCOUNT_TYPE) ");
                obj_str.Append(" FROM SELF_TR_ACCOUNT");
                obj_str.Append(" JOIN SELF_MT_ACCOUNT ON SELF_MT_ACCOUNT.COMPANY_CODE = SELF_TR_ACCOUNT.COMPANY_CODE");
                obj_str.Append(" AND SELF_MT_ACCOUNT.ACCOUNT_USER = SELF_TR_ACCOUNT.ACCOUNT_USER");
                obj_str.Append(" WHERE WORKER_CODE = '" + worker + "'");
                obj_str.Append(" AND SELF_TR_ACCOUNT.ACCOUNT_TYPE = 'Emp'");
                obj_str.Append(" AND SELF_TR_ACCOUNT.COMPANY_CODE = '"+com+"'");
                obj_str.Append(" GROUP BY SELF_MT_ACCOUNT.ACCOUNT_PWD ");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    intResult = Convert.ToInt32(dt.Rows[0][0]);
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Account.getCountTypeAccount)" + ex.ToString();
            }

            return intResult;
        }
        public int getNextID()
        {
            int intResult = 1;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT MAX(ACCOUNT_ID) ");
                obj_str.Append(" FROM SELF_MT_ACCOUNT");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    intResult = Convert.ToInt32(dt.Rows[0][0]) + 1;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Account.getNextID)" + ex.ToString();
            }

            return intResult;
        }
        public string insert(cls_MTAccount model)
        {
            string blnResult = "";
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.company_code, model.account_user, model.account_type))
                {
                    if (model.account_id.Equals(0))
                    {
                        return "D";
                    }
                    else
                    {
                        return this.update(model);
                    }
                }
                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();
                int id = this.getNextID();
                obj_str.Append("INSERT INTO SELF_MT_ACCOUNT");
                obj_str.Append(" (");
                obj_str.Append("COMPANY_CODE ");
                obj_str.Append(", ACCOUNT_ID ");
                obj_str.Append(", ACCOUNT_USER ");
                obj_str.Append(", ACCOUNT_PWD ");
                obj_str.Append(", ACCOUNT_TYPE ");
                obj_str.Append(", ACCOUNT_LEVEL ");
                obj_str.Append(", ACCOUNT_EMAIL ");
                obj_str.Append(", ACCOUNT_EMAIL_ALERT ");
                obj_str.Append(", ACCOUNT_LINE ");
                obj_str.Append(", ACCOUNT_LINE_ALERT ");
                obj_str.Append(", POLMENU_CODE ");

                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@COMPANY_CODE ");
                obj_str.Append(", @ACCOUNT_ID ");
                obj_str.Append(", @ACCOUNT_USER ");
                obj_str.Append(", @ACCOUNT_PWD ");
                obj_str.Append(", @ACCOUNT_TYPE ");
                obj_str.Append(", @ACCOUNT_LEVEL ");
                obj_str.Append(", @ACCOUNT_EMAIL ");
                obj_str.Append(", @ACCOUNT_EMAIL_ALERT ");
                obj_str.Append(", @ACCOUNT_LINE ");
                obj_str.Append(", @ACCOUNT_LINE_ALERT ");
                obj_str.Append(", @POLMENU_CODE ");

                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@ACCOUNT_ID", SqlDbType.Int); obj_cmd.Parameters["@ACCOUNT_ID"].Value = id;
                obj_cmd.Parameters.Add("@ACCOUNT_USER", SqlDbType.VarChar); obj_cmd.Parameters["@ACCOUNT_USER"].Value = model.account_user;
                obj_cmd.Parameters.Add("@ACCOUNT_PWD", SqlDbType.VarChar); obj_cmd.Parameters["@ACCOUNT_PWD"].Value = this.Encrypt(model.account_pwd);
                obj_cmd.Parameters.Add("@ACCOUNT_TYPE", SqlDbType.VarChar); obj_cmd.Parameters["@ACCOUNT_TYPE"].Value = model.account_type;
                obj_cmd.Parameters.Add("@ACCOUNT_LEVEL", SqlDbType.Int); obj_cmd.Parameters["@ACCOUNT_LEVEL"].Value = model.account_level;
                obj_cmd.Parameters.Add("@ACCOUNT_EMAIL", SqlDbType.VarChar); obj_cmd.Parameters["@ACCOUNT_EMAIL"].Value = model.account_email;
                obj_cmd.Parameters.Add("@ACCOUNT_EMAIL_ALERT", SqlDbType.Bit); obj_cmd.Parameters["@ACCOUNT_EMAIL_ALERT"].Value = model.account_email_alert;
                obj_cmd.Parameters.Add("@ACCOUNT_LINE", SqlDbType.VarChar); obj_cmd.Parameters["@ACCOUNT_LINE"].Value = model.account_line;
                obj_cmd.Parameters.Add("@ACCOUNT_LINE_ALERT", SqlDbType.Bit); obj_cmd.Parameters["@ACCOUNT_LINE_ALERT"].Value = model.account_line_alert;
                obj_cmd.Parameters.Add("@POLMENU_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@POLMENU_CODE"].Value = model.polmenu_code;

                obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@CREATED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = model.flag;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();
                blnResult = model.account_user;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Account.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public string update(cls_MTAccount model)
        {
            string blnResult = "";
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE SELF_MT_ACCOUNT SET ");

                obj_str.Append("ACCOUNT_USER=@ACCOUNT_USER ");
                if (!model.account_pwd.Equals(""))
                    obj_str.Append(", ACCOUNT_PWD=@ACCOUNT_PWD ");

                obj_str.Append(", ACCOUNT_TYPE=@ACCOUNT_TYPE ");
                obj_str.Append(", ACCOUNT_LEVEL=@ACCOUNT_LEVEL ");
                obj_str.Append(", ACCOUNT_EMAIL=@ACCOUNT_EMAIL ");
                obj_str.Append(", ACCOUNT_EMAIL_ALERT=@ACCOUNT_EMAIL_ALERT ");
                obj_str.Append(", ACCOUNT_LINE=@ACCOUNT_LINE ");
                obj_str.Append(", ACCOUNT_LINE_ALERT=@ACCOUNT_LINE_ALERT ");
                obj_str.Append(", POLMENU_CODE=@POLMENU_CODE ");

                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");

                obj_str.Append(" WHERE COMPANY_CODE=@COMPANY_CODE ");
                obj_str.Append(" AND ACCOUNT_USER=@ACCOUNT_USER ");
                obj_str.Append(" AND ACCOUNT_TYPE=@ACCOUNT_TYPE ");
                obj_str.Append(" AND ACCOUNT_ID=@ACCOUNT_ID ");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@ACCOUNT_ID", SqlDbType.Int); obj_cmd.Parameters["@ACCOUNT_ID"].Value = model.account_id;
                obj_cmd.Parameters.Add("@ACCOUNT_USER", SqlDbType.VarChar); obj_cmd.Parameters["@ACCOUNT_USER"].Value = model.account_user;
                if (!model.account_pwd.Equals(""))
                {
                    obj_cmd.Parameters.Add("@ACCOUNT_PWD", SqlDbType.VarChar); obj_cmd.Parameters["@ACCOUNT_PWD"].Value = this.Encrypt(model.account_pwd);
                }
                obj_cmd.Parameters.Add("@ACCOUNT_TYPE", SqlDbType.VarChar); obj_cmd.Parameters["@ACCOUNT_TYPE"].Value = model.account_type;
                obj_cmd.Parameters.Add("@ACCOUNT_LEVEL", SqlDbType.Int); obj_cmd.Parameters["@ACCOUNT_LEVEL"].Value = model.account_level;
                obj_cmd.Parameters.Add("@ACCOUNT_EMAIL", SqlDbType.VarChar); obj_cmd.Parameters["@ACCOUNT_EMAIL"].Value = model.account_email;
                obj_cmd.Parameters.Add("@ACCOUNT_EMAIL_ALERT", SqlDbType.Bit); obj_cmd.Parameters["@ACCOUNT_EMAIL_ALERT"].Value = model.account_email_alert;
                obj_cmd.Parameters.Add("@ACCOUNT_LINE", SqlDbType.VarChar); obj_cmd.Parameters["@ACCOUNT_LINE"].Value = model.account_line;
                obj_cmd.Parameters.Add("@ACCOUNT_LINE_ALERT", SqlDbType.Bit); obj_cmd.Parameters["@ACCOUNT_LINE_ALERT"].Value = model.account_line_alert;
                obj_cmd.Parameters.Add("@POLMENU_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@POLMENU_CODE"].Value = model.polmenu_code;

                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = model.flag;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = model.account_user.ToString();
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Account.update)" + ex.ToString();
            }

            return blnResult;
        }



        public JArray getData(string com,string worker)
        {
            JArray result = new JArray();
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.AppendLine("SELECT * FROM HRM_TR_EMPPOSITION WHERE EMPPOSITION_POSITION IN (SELECT POSITION_CODE FROM SELF_TR_ACCOUNTPOS");
                obj_str.AppendLine("WHERE ACCOUNT_USER = '" + worker + "' AND COMPANY_CODE ='" + com + "')");
                obj_str.AppendLine("AND WORKER_CODE IN (SELECT WORKER_CODE FROM HRM_TR_EMPDEP WHERE EMPDEP_LEVEL01 IN(SELECT DEP_CODE FROM SELF_TR_ACCOUNTDEP");
                obj_str.AppendLine("WHERE ACCOUNT_USER = '" + worker + "' AND COMPANY_CODE ='" + com + "'))");
                obj_str.AppendLine("AND COMPANY_CODE ='" + com + "'");
                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    result.Add(dr["WORKER_CODE"].ToString());
                }

            }
            catch (Exception ex)
            {
                Message = "ERROR::(Account.getData)" + ex.ToString();
            }

            return result;
        }
        private const string ENCRYPTION_KEY = "d42262e6-17c0-45da-bc34-1bd04f8b6928";
        private readonly byte[] SALT = Encoding.ASCII.GetBytes(ENCRYPTION_KEY.Length.ToString());
        private string Encrypt(string input)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            byte[] plainText = Encoding.Unicode.GetBytes(input);
            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(ENCRYPTION_KEY, SALT);

            using (ICryptoTransform encryptor = rijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16)))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainText, 0, plainText.Length);
                        cryptoStream.FlushFinalBlock();
                        return Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }
        }
        private string Decrypt(string input)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            byte[] encryptedData = Convert.FromBase64String(input);
            PasswordDeriveBytes secretKey = new PasswordDeriveBytes(ENCRYPTION_KEY, SALT);

            using (ICryptoTransform decryptor = rijndaelCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
            {
                using (MemoryStream memoryStream = new MemoryStream(encryptedData))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        byte[] plainText = new byte[encryptedData.Length];
                        int decryptedCount = cryptoStream.Read(plainText, 0, plainText.Length);
                        return Encoding.Unicode.GetString(plainText, 0, decryptedCount);
                    }
                }
            }

        }
    }
}
