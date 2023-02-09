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
    public class cls_ctSYSTopic
    {
           string Message = string.Empty;

        cls_ctConnection Obj_conn = new cls_ctConnection();

        public cls_ctSYSTopic() { }

        public string getMessage() { return this.Message; }

        public void dispose()
        {
            Obj_conn.doClose();
        }

        public List<cls_SYSTopic> getData(string Condition)
        {
            List<cls_SYSTopic> list_model = new List<cls_SYSTopic>();
            cls_SYSTopic model;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT *");
                obj_str.Append(" FROM HRM_SYS_TOPIC");
                obj_str.Append(" WHERE STATUS = 'true'");
                if (!Condition.Equals(""))
                    obj_str.Append(Condition);
                obj_str.Append(" ORDER BY CREATED_DATE DESC");
                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                foreach (DataRow dr in dt.Rows)
                {
                    model = new cls_SYSTopic();

                    model.topic_id = dr["TOPIC_ID"].ToString();
                    model.detail = dr["DETAIL"].ToString();
                    model.status = dr["STATUS"].ToString();
                    model.create_by = dr["CREATED_BY"].ToString();
                    model.create_date = Convert.ToDateTime(dr["CREATED_DATE"]);
                    model.modified_by = dr["MODIFIED_BY"].ToString();
                    if (!dr["MODIFIED_DATE"].ToString().Equals(""))
                        model.modified_date =   Convert.ToDateTime(dr["MODIFIED_DATE"]);                          
                    list_model.Add(model);
                }

            }
            catch(Exception ex)
            {
                Message = "ERROR::(HRM_SYS_TOPIC.getData)" + ex.ToString();
            }

            return list_model;
        }

        public List<cls_SYSTopic> getDataTopicByFillter(string emp)
        {
            string strCondition = "";
            if (!emp.Equals(""))
                strCondition += " OR CREATED_BY ='" + emp + "'";
            return this.getData(strCondition);
        }
        public bool checkDataOld(string id)
        {
            bool blnResult = false;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT * FROM HRM_SYS_TOPIC");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND TOPIC_ID='" + id + "'");                     
                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    blnResult = true;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(HRM_SYS_TOPIC.checkDataOld)" + ex.ToString();
            }

            return blnResult;
        }

        public bool delete(string id)
        {
            bool blnResult = true;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append(" DELETE FROM HRM_SYS_TOPIC");
                obj_str.Append(" WHERE 1=1 ");
                obj_str.Append(" AND TOPIC_ID='" + id + "'");
                                              
                blnResult = obj_conn.doExecuteSQL(obj_str.ToString());

            }
            catch (Exception ex)
            {
                blnResult = false;
                Message = "ERROR::(HRM_SYS_TOPIC.delete)" + ex.ToString();
            }

            return blnResult;
        }
        public int getNextID()
        {
            int intResult = 1;
            try
            {
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("SELECT MAX(TOPIC_ID) ");
                obj_str.Append(" FROM HRM_SYS_TOPIC");

                DataTable dt = Obj_conn.doGetTable(obj_str.ToString());

                if (dt.Rows.Count > 0)
                {
                    intResult = Convert.ToInt32(dt.Rows[0][0]) + 1;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR::(HRM_SYS_TOPIC.getNextID)" + ex.ToString();
            }

            return intResult;
        }
        public bool insert(cls_SYSTopic model)
        {
            bool blnResult = false;
 
            try
            {
                //-- Check data old
                if (this.checkDataOld(model.topic_id))
                {
                    return this.update(model);
                }

                cls_ctConnection obj_conn = new cls_ctConnection();
                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("INSERT INTO HRM_SYS_TOPIC");
                obj_str.Append(" (");
                obj_str.Append("TOPIC_ID ");
                obj_str.Append(",DETAIL ");
                obj_str.Append(",STATUS ");
                obj_str.Append(",CREATED_BY ");
                obj_str.Append(",CREATED_DATE ");
                obj_str.Append(" )");
                obj_str.Append(" VALUES(");
                obj_str.Append("@TOPIC_ID ");
                obj_str.Append(", @DETAIL ");
                obj_str.Append(", @STATUS ");
                obj_str.Append(", @CREATED_BY ");
                obj_str.Append(", @CREATED_DATE ");
                obj_str.Append(" )");

                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@TOPIC_ID", SqlDbType.Int); obj_cmd.Parameters["@TOPIC_ID"].Value = this.getNextID();
                obj_cmd.Parameters.Add("@DETAIL", SqlDbType.VarChar); obj_cmd.Parameters["@DETAIL"].Value = model.detail;
                obj_cmd.Parameters.Add("@STATUS", SqlDbType.Bit); obj_cmd.Parameters["@STATUS"].Value = model.status.Equals("1") ? true : false ;
                obj_cmd.Parameters.Add("@CREATED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@CREATED_BY"].Value = model.create_by;
                obj_cmd.Parameters.Add("@CREATED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@CREATED_DATE"].Value = DateTime.Now;
     
                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();
                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(HRM_SYS_TOPIC.insert)" + ex.ToString();
            }

            return blnResult;
        }

        public bool update(cls_SYSTopic model)
        {
            bool blnResult = false;
            try
            {
                cls_ctConnection obj_conn = new cls_ctConnection();

                System.Text.StringBuilder obj_str = new System.Text.StringBuilder();

                obj_str.Append("UPDATE HRM_SYS_TOPIC SET ");
                obj_str.Append(" DETAIL=@DETAIL ");
                obj_str.Append(", STATUS=@STATUS ");
                obj_str.Append(", MODIFIED_BY=@MODIFIED_BY ");
                obj_str.Append(", MODIFIED_DATE=@MODIFIED_DATE ");
                obj_str.Append(" WHERE TOPIC_ID=@TOPIC_ID ");
               
                obj_conn.doConnect();

                SqlCommand obj_cmd = new SqlCommand(obj_str.ToString(), obj_conn.getConnection());

                obj_cmd.Parameters.Add("@TOPIC_ID", SqlDbType.VarChar); obj_cmd.Parameters["@TOPIC_ID"].Value = Convert.ToInt32(model.topic_id);
                obj_cmd.Parameters.Add("@DETAIL", SqlDbType.VarChar); obj_cmd.Parameters["@DETAIL"].Value = model.detail;
                obj_cmd.Parameters.Add("@STATUS", SqlDbType.Bit); obj_cmd.Parameters["@STATUS"].Value = model.status.Equals("1") ? true : false;
                obj_cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.VarChar); obj_cmd.Parameters["@MODIFIED_BY"].Value = model.create_by;
                obj_cmd.Parameters.Add("@MODIFIED_DATE", SqlDbType.DateTime); obj_cmd.Parameters["@MODIFIED_DATE"].Value = DateTime.Now;

                obj_cmd.ExecuteNonQuery();

                obj_conn.doClose();

                blnResult = true;
            }
            catch (Exception ex)
            {
                Message = "ERROR::(HRM_SYS_TOPIC.update)" + ex.ToString();
            }

            return blnResult;
        }
    }
    }
