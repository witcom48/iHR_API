using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary_BPC.hrfocus.model;
using System.Data.SqlClient;
using System.Data;

namespace ClassLibrary_BPC.hrfocus.controller
{
    public class cls_ctSYSPackage
    {
         string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctSYSPackage() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        public List<cls_SYSPackage> getData()
        {
            List<cls_SYSPackage> list_model = new List<cls_SYSPackage>();
            cls_SYSPackage model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT *");
                obj_str.Append(" FROM HRM_SYS_PACKAGE");
                obj_str.Append(" WHERE 1=1");
                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_SYSPackage();

                    model.package_ref = dr["PACKAGE_REF"].ToString();
                    model.packege_name = dr["PACKAGE_NAME"].ToString();
                    model.packege_com = dr["PACKAGE_COM"].ToString();
                    model.packege_branch = dr["PACKAGE_BRANCH"].ToString();
                    model.packege_emp = dr["PACKAGE_EMP"].ToString();
                    model.packege_user = dr["PACKAGE_USER"].ToString();                                                                  
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(HRM_SYS_PACKAGE.getData)" + ex.ToString();
            }

            return list_model;
        }
        public bool checkDataOld()
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT * FROM HRM_SYS_PACKAGE");
                                                                
                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(HRM_SYS_PACKAGE.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public string CreateDocnumber()
        {

            string timestr = "";
            timestr = System.DateTime.Now.Year.ToString().Substring(2, 2);
            timestr += System.DateTime.Now.Month.ToString("00");
            timestr += System.DateTime.Now.Date.ToString("dd");

            System.Text.StringBuilder obj_str = new System.Text.StringBuilder();
            obj_str.Append("SELECT TOP 1 ISNULL(PACKAGE_REF, 'PYYMMDD0000') as lastNum ");
            obj_str.Append("FROM tbTRWebChangeShift ");
            DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

            string strKey = "0001";

            if (dt.Rows.Count > 0)
            {
                try
                {
                    string strLast = dt.Rows[0]["lastNum"].ToString();

                    strLast = strLast.Substring(8, strLast.Length - 8);

                    int intNext = Convert.ToInt32(strLast) + 1;

                    strKey = intNext.ToString().PadLeft(4, '0');
                }
                catch { }
            }
            else
            {
                strKey = "0001";
            }

            string refdoc = "";
            refdoc =  'P'+timestr;

            return refdoc;
        }

        public bool delete(string id)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_SYS_PACKAGE");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND PACKAGE_REF='" + id + "'");
                                              
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(HRM_SYS_PACKAGE.delete)" + ex.ToString();
            }

            return blnResult;
        }

        public bool insert(cls_SYSPackage model)
        {
            bool blnResult = false;
            
            string timestr = "";
            timestr = System.DateTime.Now.Year.ToString();
            timestr += System.DateTime.Now.Month.ToString("00");
            timestr += System.DateTime.Now.Date.ToString("dd");
            try
            {
                //-- Check data old
                if (this.checkDataOld())
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_SYS_PACKAGE");
                obj_str.Append(" (");
                obj_str.Append("PACKAGE_REF ");
                obj_str.Append(", PACKAGE_NAME ");
                obj_str.Append(", PACKAGE_COM ");
                obj_str.Append(", PACKAGE_BRANCH ");
                obj_str.Append(", PACKAGE_EMP ");
                obj_str.Append(", PACKAGE_USER ");
                obj_str.Append(" )");
                obj_str.Append(" VALUES(");
                obj_str.Append("@PACKAGE_REF ");
                obj_str.Append(", @PACKAGE_NAME ");
                obj_str.Append(", @PACKAGE_COM ");
                obj_str.Append(", @PACKAGE_BRANCH ");
                obj_str.Append(", @PACKAGE_EMP ");
                obj_str.Append(", @PACKAGE_USER ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@PACKAGE_REF", SqlDbType.VarChar); obj_cmd.Parameters["@PACKAGE_REF"].Value = "P"+timestr;
                obj_cmd.Parameters.Add("@PACKAGE_NAME", SqlDbType.VarChar); obj_cmd.Parameters["@PACKAGE_NAME"].Value = model.packege_name;
                obj_cmd.Parameters.Add("@PACKAGE_COM", SqlDbType.VarChar); obj_cmd.Parameters["@PACKAGE_COM"].Value = model.packege_com;
                obj_cmd.Parameters.Add("@PACKAGE_BRANCH", SqlDbType.VarChar); obj_cmd.Parameters["@PACKAGE_BRANCH"].Value = model.packege_branch;
                obj_cmd.Parameters.Add("@PACKAGE_EMP", SqlDbType.VarChar); obj_cmd.Parameters["@PACKAGE_EMP"].Value = model.packege_emp;
                obj_cmd.Parameters.Add("@PACKAGE_USER", SqlDbType.VarChar); obj_cmd.Parameters["@PACKAGE_USER"].Value = model.packege_user;
     
                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();
                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(HRM_SYS_PACKAGE.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_SYSPackage model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_SYS_PACKAGE SET ");
                obj_str.Append(" PACKAGE_NAME=@PACKAGE_NAME ");
                obj_str.Append(", PACKAGE_COM=@PACKAGE_COM ");
                obj_str.Append(", PACKAGE_BRANCH=@PACKAGE_BRANCH ");
                obj_str.Append(", PACKAGE_EMP=@PACKAGE_EMP ");
                obj_str.Append(", PACKAGE_USER=@PACKAGE_USER ");
               
                
                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@PACKAGE_NAME", SqlDbType.VarChar); obj_cmd.Parameters["@PACKAGE_NAME"].Value = model.packege_name;
                obj_cmd.Parameters.Add("@PACKAGE_COM", SqlDbType.VarChar); obj_cmd.Parameters["@PACKAGE_COM"].Value = model.packege_com;
                obj_cmd.Parameters.Add("@PACKAGE_BRANCH", SqlDbType.VarChar); obj_cmd.Parameters["@PACKAGE_BRANCH"].Value = model.packege_branch;
                obj_cmd.Parameters.Add("@PACKAGE_EMP", SqlDbType.VarChar); obj_cmd.Parameters["@PACKAGE_EMP"].Value = model.packege_emp;
                obj_cmd.Parameters.Add("@PACKAGE_USER", SqlDbType.VarChar); obj_cmd.Parameters["@PACKAGE_USER"].Value = model.packege_user;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(HRM_SYS_PACKAGE.update)" + ex.ToString();
            }

            return blnResult;
        }
    }
}
