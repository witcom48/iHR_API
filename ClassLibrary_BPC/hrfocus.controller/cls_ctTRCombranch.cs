using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary_BPC.hrfocus.model;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.IO;

namespace ClassLibrary_BPC.hrfocus.controller
{
    public class cls_ctTRCombranch
    {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctTRCombranch() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_TRCombranch> getData(string condition)
        {
            List<cls_TRCombranch> list_model = new List<cls_TRCombranch>();
            cls_TRCombranch model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");
                obj_str.Append("COMPANY_CODE");
                obj_str.Append(", COMBRANCH_ID");
                obj_str.Append(", COMBRANCH_CODE");                
                obj_str.Append(", ISNULL(Combranch_NAME_TH, '') AS COMBRANCH_NAME_TH");
                obj_str.Append(", ISNULL(Combranch_NAME_EN, '') AS COMBRANCH_NAME_EN");

                obj_str.Append(", ISNULL(MODIFIED_BY, CREATED_BY) AS MODIFIED_BY");
                obj_str.Append(", ISNULL(MODIFIED_DATE, CREATED_DATE) AS MODIFIED_DATE");
                
                obj_str.Append(" FROM HRM_TR_COMBRANCH");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                obj_str.Append(" ORDER BY COMPANY_CODE, COMBRANCH_CODE");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_TRCombranch();
                    model.company_code = dr["COMPANY_CODE"].ToString();
                    model.combranch_id = Convert.ToInt32(dr["COMBRANCH_ID"]);
                    model.combranch_code = dr["COMBRANCH_CODE"].ToString();
                    model.combranch_name_th = dr["COMBRANCH_NAME_TH"].ToString();
                    model.combranch_name_en = dr["COMBRANCH_NAME_EN"].ToString();
                   
                    model.modified_by = dr["MODIFIED_BY"].ToString();
                    model.modified_date = Convert.ToDateTime(dr["MODIFIED_DATE"]);
                                                                                                                      
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(Combranch.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_TRCombranch> getDataByFillter(string com, string id, string code)
        {
            string strCondition = "";

            if (!com.Equals(""))
                strCondition += " AND COMPANY_CODE='" + com + "'";

            if (!id.Equals(""))
                strCondition += " AND COMBRANCH_ID='" + id + "'";

            if (!code.Equals(""))
                strCondition += " AND COMBRANCH_CODE='" + code + "'";
            
            return this.getData(strCondition);
        }

        public bool checkDataOld(string code)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT COMBRANCH_CODE");
                obj_str.Append(" FROM HRM_TR_COMBRANCH");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND COMBRANCH_CODE='" + code + "'");
                                                
                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Combranch.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public int getNextID()
        {
            int intResult = 1;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT MAX(Combranch_ID) ");
                obj_str.Append(" FROM HRM_TR_COMBRANCH");             

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    intResult = Convert.ToInt32(dt.Rows[0][0]) + 1;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Combranch.getNextID)" + ex.ToString();
            }

            return intResult;
        }

        public bool delete(string id)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_TR_COMBRANCH");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND COMBRANCH_ID='" + id + "'");
                                              
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Combranch.delete)" + ex.ToString();
            }

            return blnResult;
        }
        public int branchcount()
        {
            int intResult = 0;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("select COUNT(COMBRANCH_ID) as branchcount from HRM_TR_COMBRANCH");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    intResult = Convert.ToInt32(dt.Rows[0][0]);
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(HRM_TR_COMBRANCH.count)" + ex.ToString();
            }

            return intResult;
        }
        public string insert(cls_TRCombranch model)
        {
            string blnResult = "";
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.combranch_code))
                {
                    return this.update(model);
                }
                cls_ctSYSPackage Package = new cls_ctSYSPackage();
                var branchcounts = this.branchcount();
                List<cls_SYSPackage> listPackage = Package.getData();
                if (Convert.ToInt32(this.Decrypt(listPackage[0].packege_branch).Split('B')[1]) <= branchcounts)
                {
                    return "limit";
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_TR_COMBRANCH");
                obj_str.Append(" (");
                obj_str.Append("COMBRANCH_ID ");
                obj_str.Append(", COMBRANCH_CODE ");
                obj_str.Append(", COMBRANCH_NAME_TH ");
                obj_str.Append(", COMBRANCH_NAME_EN ");
                obj_str.Append(", COMPANY_CODE ");
                obj_str.Append(", CREATED_BY ");
                obj_str.Append(", CREATED_DATE ");
                obj_str.Append(", FLAG ");
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append(" @COMBRANCH_ID ");
                obj_str.Append(", @COMBRANCH_CODE ");
                obj_str.Append(", @COMBRANCH_NAME_TH ");
                obj_str.Append(", @COMBRANCH_NAME_EN ");
                obj_str.Append(", @COMPANY_CODE ");
                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(", @FLAG ");              
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@COMBRANCH_ID", SqlDbType.Int); obj_cmd.Parameters["@COMBRANCH_ID"].Value = this.getNextID();
                obj_cmd.Parameters.Add("@COMBRANCH_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMBRANCH_CODE"].Value = model.combranch_code;
                obj_cmd.Parameters.Add("@COMBRANCH_NAME_TH", SqlDbType.VarChar); obj_cmd.Parameters["@COMBRANCH_NAME_TH"].Value = model.combranch_name_th;
                obj_cmd.Parameters.Add("@COMBRANCH_NAME_EN", SqlDbType.VarChar); obj_cmd.Parameters["@COMBRANCH_NAME_EN"].Value = model.combranch_name_en;
                obj_cmd.Parameters.Add("@COMPANY_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMPANY_CODE"].Value = model.company_code;
                obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@CREATED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;
     
                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();
                blnResult = "yes";
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Combranch.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public string update(cls_TRCombranch model)
        {
            string blnResult = "";
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_TR_COMBRANCH SET ");

                obj_str.Append(" COMBRANCH_CODE=@COMBRANCH_CODE ");
                obj_str.Append(", COMBRANCH_NAME_TH=@COMBRANCH_NAME_TH ");
                obj_str.Append(", COMBRANCH_NAME_EN=@COMBRANCH_NAME_EN ");
                
                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");
                obj_str.Append(", FLAG=@FLAG ");
                
                obj_str.Append(" WHERE COMBRANCH_ID=@COMBRANCH_ID ");
                
                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
                
                obj_cmd.Parameters.Add("@COMBRANCH_CODE", SqlDbType.VarChar); obj_cmd.Parameters["@COMBRANCH_CODE"].Value = model.combranch_code;
                obj_cmd.Parameters.Add("@COMBRANCH_NAME_TH", SqlDbType.VarChar); obj_cmd.Parameters["@COMBRANCH_NAME_TH"].Value = model.combranch_name_th;
                obj_cmd.Parameters.Add("@COMBRANCH_NAME_EN", SqlDbType.VarChar); obj_cmd.Parameters["@COMBRANCH_NAME_EN"].Value = model.combranch_name_en;
                
                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.modified_by;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = DateTime.Now;
                obj_cmd.Parameters.Add("@FLAG", SqlDbType.Bit); obj_cmd.Parameters["@FLAG"].Value = false;

                obj_cmd.Parameters.Add("@COMBRANCH_ID", SqlDbType.Int); obj_cmd.Parameters["@COMBRANCH_ID"].Value = model.combranch_id;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = "yes";
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Combranch.update)" + ex.ToString();
            }

            return blnResult;
        }
        private const string ENCRYPTION_KEY = "d42262e6-17c0-45da-bc34-1bd04f8b6928";
        private readonly byte[] SALT = Encoding.ASCII.GetBytes(ENCRYPTION_KEY.Length.ToString());
        public string Decrypt(string inputText)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            byte[] encryptedData = Convert.FromBase64String(inputText);
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
