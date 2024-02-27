using ClassLibrary_BPC.hrfocus.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_BPC.hrfocus.controller
{
    public class cls_ctFNTCompareamount
  {
        string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctFNTCompareamount() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        private List<cls_FNTCompareamount> getData(string condition)
        {
            List<cls_FNTCompareamount> list_model = new List<cls_FNTCompareamount>();
            cls_FNTCompareamount model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT ");

                obj_str.Append(" EmpID");
                obj_str.Append(", EmpName");
                obj_str.Append(", Amount");
                obj_str.Append(", AmountOld");
                obj_str.Append(", Filldate");
                obj_str.Append(", Resigndate");
                obj_str.Append(" FROM HRM_FNT_COMPARE_AMOUNT('12/25/2023', '11/24/2023', 'APT', 'OT01')");
                obj_str.Append(" WHERE 1=1");

                if (!condition.Equals(""))
                    obj_str.Append(" " + condition);

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_FNTCompareamount();
                    model.EmpID = Convert.ToInt32(dr["EmpID"]);
                    model.EmpName = dr["EmpName"].ToString();
                    model.Amount = dr["Amount"].ToString();
                    model.AmountOld = dr["AmountOld"].ToString();
                    model.Filldate = Convert.ToDateTime(dr["Filldate"]);
                    model.Resigndate = Convert.ToDateTime(dr["Resigndate"]);
                    list_model.Add(model);
                }

            }
            catch (Exception ex)
            {
                Message = "ERROR::(Compareamount.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_FNTCompareamount> getDataByFillter(int id, string user )
        {
            string strCondition = "";
            if (!id.Equals(""))
                strCondition += " AND EmpID='" + id + "'";
            if (!user.Equals(""))
                strCondition += " AND EmpName='" + user + "'";
            //if (!amount.Equals(""))
            //    strCondition += " AND Amount='" + amount + "'";
           

            return this.getData(strCondition);
        }
       
        public bool checkDataOld(int id  )
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT EmpID");
                obj_str.Append(" FROM HRM_FNT_COMPARE_AMOUNT('12/25/2023', '11/24/2023', 'APT', 'OT01')");
                obj_str.Append(" WHERE EmpID='" + id + "'");
                //obj_str.Append(" AND ACCOUNT_USER='" + user + "'");
                //obj_str.Append(" AND ACCOUNT_TYPE='" + type + "'");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Compareamount.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }
        public bool delete(int id)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_FNT_COMPARE_AMOUNT('12/25/2023', '11/24/2023', 'APT', 'OT01')");
                obj_str.Append(" WHERE EmpID='" + id + "'");
 
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(Compareamount.delete)" + ex.ToString();
            }

            return blnResult;
        }
        public int getNextID()
        {
            int intResult = 1;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT MAX(EmpID) ");
                obj_str.Append(" FROM HRM_FNT_COMPARE_AMOUNT('12/25/2023', '11/24/2023', 'APT', 'OT01')");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    intResult = Convert.ToInt32(dt.Rows[0][0]) + 1;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Compareamount.getNextID)" + ex.ToString();
            }

            return intResult;
        }

        //        public string insert(cls_FNTCompareamount model)

        public bool insert2(  List<cls_FNTCompareamount> list_model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();
                //int id = this.getNextID();

                obj_str.Append("INSERT INTO HRM_FNT_COMPARE_AMOUNT('12/25/2023', '11/24/2023', 'APT', 'OT01')");
                obj_str.Append(" (");
                obj_str.Append("EmpID ");
                obj_str.Append(", EmpName ");
                obj_str.Append(", Amount ");
                obj_str.Append(", AmountOld ");
                obj_str.Append(", Filldate ");
                obj_str.Append(", Resigndate ");

                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@EmpID ");
                obj_str.Append(", @EmpName ");
                obj_str.Append(", @Amount ");
                obj_str.Append(", @AmountOld ");
                obj_str.Append(", @Filldate ");
                obj_str.Append(", @Resigndate ");

                obj_str.Append(" )");

                obj_conn.doConnect();

                obj_conn.doOpenTransaction();

                //System.Text.StringBuilder obj_str2 = new System.Text.StringBuilder();

                //obj_str2.Append(" DELETE FROM HRM_FNT_COMPARE_AMOUNT('12/25/2023', '11/24/2023', 'APT', 'OT01')");
                //obj_str2.Append(" WHERE 1=1 ");
                //obj_str2.Append(" AND EmpID='" + empId + "'");
 
                //blnResult = obj_conn.doExecuteSQL_transaction(obj_str2.ToString());

                if (blnResult)
                {
                    SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());
                    obj_cmd.Transaction = obj_conn.getTransaction();

                    obj_cmd.Parameters.Add("@EmpID", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@EmpName", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@Amount", SqlDbType.Int);
                    obj_cmd.Parameters.Add("@AmountOld", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@Filldate", SqlDbType.VarChar);
                    obj_cmd.Parameters.Add("@Resigndate", SqlDbType.Int);

                    foreach (cls_FNTCompareamount model in list_model)
                    {

                        obj_cmd.Parameters["@EmpID"].Value = this.getNextID();
                        obj_cmd.Parameters["@EmpName"].Value = model.EmpName;
                        obj_cmd.Parameters["@Amount"].Value = model.Amount;
                        obj_cmd.Parameters["@AmountOld"].Value = model.AmountOld;
                        obj_cmd.Parameters["@Filldate"].Value = model.Filldate;
                        obj_cmd.Parameters["@Resigndate"].Value = model.Resigndate;

                        obj_cmd.ExecuteNonQuery();

                    }

                    blnResult = obj_conn.doCommit();

                    if (!blnResult)
                    {
                        obj_conn.doRollback();
                    }
                }
                else
                {
                    obj_conn.doRollback();
                }
                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Shiftbreak.insert)" + ex.ToString();
            }

            return blnResult;
        }

        //public bool insert2(List<cls_FNTCompareamount> list_model)
        //{
        //    bool blnResult = false;
        //    cls_ctConnection obj_conn = new cls_ctConnection();

        //    try
        //    {
        //        obj_conn.doConnect();
        //        obj_conn.doOpenTransaction();

        //        foreach (cls_FNTCompareamount model in list_model)
        //        {
        //            string sql = "INSERT INTO HRM_FNT_COMPARE_AMOUNT('12/25/2023', '11/24/2023', 'APT', 'OT01') " +
        //                         "VALUES (@EmpID, @EmpName, @Amount, @AmountOld, @Filldate, @Resigndate)";

        //            SqlCommand obj_cmd = new SqlCommand(sql, obj_conn.getConnection());
        //            obj_cmd.Transaction = obj_conn.getTransaction();

        //            obj_cmd.Parameters.Add("@EmpID", SqlDbType.VarChar).Value = model.EmpID;
        //            obj_cmd.Parameters.Add("@EmpName", SqlDbType.VarChar).Value = model.EmpName;
        //            obj_cmd.Parameters.Add("@Amount", SqlDbType.Int).Value = model.Amount;
        //            obj_cmd.Parameters.Add("@AmountOld", SqlDbType.VarChar).Value = model.AmountOld;
        //            obj_cmd.Parameters.Add("@Filldate", SqlDbType.DateTime).Value = model.Filldate;
        //            obj_cmd.Parameters.Add("@Resigndate", SqlDbType.DateTime).Value = model.Resigndate;

        //            obj_cmd.ExecuteNonQuery();
        //        }

        //        blnResult = obj_conn.doCommit();
        //    }
        //    //catch (Exception ex)
        //    //{
        //    //    blnResult = false;
        //    //    Message =  "ERROR::(Shiftbreak.InsertData) {ex.Message}\n{ex.StackTrace}";

        //    //    obj_conn.doRollback();
        //    //    // Log the exception details here.
        //    //}
        //    finally
        //    {
        //        obj_conn.doClose();
        //    }

        //    return blnResult;
        //}

        //
        public string insert(cls_FNTCompareamount model)
        {
            string blnResult = "";
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.EmpID))
                {
                    if (model.EmpID.Equals(0))
                    {
                        return "";
                    }
                    else
                    {
                        return this.update(model);
                    }
                }
                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();
                int id = this.getNextID();
                obj_str.Append("INSERT INTO HRM_FNT_COMPARE_AMOUNT('12/25/2023', '11/24/2023', 'APT', 'OT01')");
                obj_str.Append(" (");
                obj_str.Append("EmpID ");
                obj_str.Append(", EmpName ");
                obj_str.Append(", Amount ");
                obj_str.Append(", AmountOld ");
                obj_str.Append(", Filldate ");
                obj_str.Append(", Resigndate ");
               
                obj_str.Append(" )");

                obj_str.Append(" VALUES(");
                obj_str.Append("@EmpID ");
                obj_str.Append(", @EmpName ");
                obj_str.Append(", @Amount ");
                obj_str.Append(", @AmountOld ");
                obj_str.Append(", @Filldate ");
                obj_str.Append(", @Resigndate ");
                
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                 obj_cmd.Parameters.Add("@EmpID", SqlDbType.Int); obj_cmd.Parameters["@EmpID"].Value = id;
                 obj_cmd.Parameters.Add("@EmpName", SqlDbType.VarChar); obj_cmd.Parameters["@EmpName"].Value = model.EmpName;
                 obj_cmd.Parameters.Add("@Amount", SqlDbType.VarChar); obj_cmd.Parameters["@Amount"].Value = model.Amount;
                 obj_cmd.Parameters.Add("@AmountOld", SqlDbType.Int); obj_cmd.Parameters["@AmountOld"].Value = model.AmountOld;
                 obj_cmd.Parameters.Add("@Filldate", SqlDbType.DateTime); obj_cmd.Parameters["@Filldate"].Value = model.Filldate;
                  obj_cmd.Parameters.Add("@Resigndate", SqlDbType.DateTime); obj_cmd.Parameters["@Resigndate"].Value = model.Resigndate;

                
             

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();
                blnResult = model.EmpName;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Compareamount.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public string update(cls_FNTCompareamount model)
        {
            string blnResult = "";
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_FNT_COMPARE_AMOUNT('12/25/2023', '11/24/2023', 'APT', 'OT01') SET ");
                obj_str.Append("EmpName=@EmpName ");
                obj_str.Append(", Amount=@Amount ");
                obj_str.Append(", AmountOld=@AmountOld ");
                obj_str.Append(", Filldate=@Filldate ");
                obj_str.Append(", Resigndate=@Resigndate ");
                obj_str.Append(" WHERE EmpID=@EmpID ");
                
 
                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@EmpName", SqlDbType.Int); obj_cmd.Parameters["@EmpName"].Value = model.EmpName;
                obj_cmd.Parameters.Add("@Amount", SqlDbType.Decimal); obj_cmd.Parameters["@Amount"].Value = model.Amount;
                obj_cmd.Parameters.Add("@AmountOld", SqlDbType.Decimal); obj_cmd.Parameters["@AmountOld"].Value = model.AmountOld;
                obj_cmd.Parameters.Add("@Filldate", SqlDbType.DateTime); obj_cmd.Parameters["@Filldate"].Value = model.Filldate;
                obj_cmd.Parameters.Add("@Resigndate", SqlDbType.DateTime); obj_cmd.Parameters["@Resigndate"].Value = model.Resigndate;

                 

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = model.EmpID.ToString();
            }
            catch (Exception ex)
            {
                Message = "ERROR::(Compareamount.update)" + ex.ToString();
            }

            return blnResult;
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
