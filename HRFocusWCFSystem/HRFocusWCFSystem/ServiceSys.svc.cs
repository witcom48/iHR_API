using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using ClassLibrary_BPC.hrfocus.controller;
using ClassLibrary_BPC.hrfocus.model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Hosting;
using ClassLibrary_BPC.hrfocus.service;
using System.Drawing;
using AntsCode.Util;
using Google.Authenticator;
using System.Web.Security;

namespace HRFocusWCFSystem
{

    
    
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

    public class ServiceSys : IServiceSys
    {

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType modelposite)
        {
            if (modelposite == null)
            {
                throw new ArgumentNullException("modelposite");
            }
            if (modelposite.BoolValue)
            {
                modelposite.StringValue += "Suffix";
            }
            return modelposite;
        }

        
        #region System

        #region MTBank
        public string getMTBankList()
        {
            JObject output = new JObject();

            cls_ctMTBank objBank = new cls_ctMTBank();
            List<cls_MTBank> listBank = objBank.getDataByFillter("", "");

            JArray array = new JArray();

            if (listBank.Count > 0)
            {                
                int index = 1;

                foreach (cls_MTBank model in listBank)
                {
                    JObject json = new JObject();

                    json.Add("bank_id", model.bank_id);
                    json.Add("bank_code", model.bank_code);
                    json.Add("bank_name_th", model.bank_name_th);
                    json.Add("bank_name_en", model.bank_name_en);
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;
                    
                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageMTBank(InputMTBank input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTBank objBank = new cls_ctMTBank();
                cls_MTBank model = new cls_MTBank();

                model.bank_id = input.bank_id;
                model.bank_code = input.bank_code;

                model.bank_name_th = input.bank_name_th;
                model.bank_name_en = input.bank_name_en;
                model.modified_by = input.modified_by;
                //model.modified_date = input.modified_date;
                model.flag = model.flag;

                bool blnResult = objBank.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objBank.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTBank(InputMTBank input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTBank objBank = new cls_ctMTBank();

                bool blnResult = objBank.delete(input.bank_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objBank.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region MTLocation
        public string getMTLocationList()
        {
            JObject output = new JObject();

            cls_ctMTLocation objLocation = new cls_ctMTLocation();
            List<cls_MTLocation> listLocation = objLocation.getDataByFillter("", "");

            JArray array = new JArray();

            if (listLocation.Count > 0)
            {
                
                int index = 1;

                foreach (cls_MTLocation model in listLocation)
                {
                    JObject json = new JObject();

                    json.Add("location_id", model.location_id);
                    json.Add("location_code", model.location_code);
                    json.Add("location_name_th", model.location_name_th);
                    json.Add("location_name_en", model.location_name_en);
                    json.Add("location_detail", model.location_detail);
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageMTLocation(InputMTLocation input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTLocation objLocation = new cls_ctMTLocation();
                cls_MTLocation model = new cls_MTLocation();

                model.location_id = input.location_id;
                model.location_code = input.location_code;
                model.location_name_th = input.location_name_th;
                model.location_name_en = input.location_name_en;
                model.location_detail = input.location_detail;
                model.modified_by = input.modified_by;                
                model.flag = model.flag;

                bool blnResult = objLocation.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objLocation.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTLocation(InputMTLocation input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTLocation objLocation = new cls_ctMTLocation();

                bool blnResult = objLocation.delete(input.location_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objLocation.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region MTRound
        public string getMTRoundList(string group)
        {
            JObject output = new JObject();

            cls_ctMTRound objRound = new cls_ctMTRound();
            List<cls_MTRound> listRound = objRound.getDataByFillter(group, "", "");

            JArray array = new JArray();

            if (listRound.Count > 0)
            {
                
                int index = 1;

                foreach (cls_MTRound model in listRound)
                {
                    JObject json = new JObject();

                    json.Add("round_id", model.round_id);
                    json.Add("round_code", model.round_code);
                    json.Add("round_name_th", model.round_name_th);
                    json.Add("round_name_en", model.round_name_en);
                    json.Add("round_group", model.round_group);
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string getTRRoundList(string id)
        {
            JObject output = new JObject();

            cls_ctTRRound objRound = new cls_ctTRRound();
            List<cls_TRRound> listRound = objRound.getDataByFillter(id);

            if (listRound.Count > 0)
            {
                JArray array = new JArray();

                int index = 1;

                foreach (cls_TRRound model in listRound)
                {
                    JObject json = new JObject();

                    json.Add("round_id", model.round_id);
                    json.Add("round_from", model.round_from);
                    json.Add("round_to", model.round_to);
                    json.Add("round_result", model.round_result);
                    
                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
            }

            return output.ToString(Formatting.None);
        }
        public string doManageRound(InputMTRound input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTRound objRound = new cls_ctMTRound();
                cls_MTRound model = new cls_MTRound();

                model.round_id = input.round_id;
                model.round_code = input.round_code;

                model.round_name_th = input.round_name_th;
                model.round_name_en = input.round_name_en;
                model.round_group = input.round_group;
                model.modified_by = input.modified_by;               
                model.flag = model.flag;

                string strID = objRound.insert(model);

                if (!strID.Equals(""))
                {

                    string round_data = input.round_data;

                    try
                    {
                        JObject jsonObject = new JObject();
                        var jsonArray = JsonConvert.DeserializeObject<List<cls_TRRound>>(round_data);


                        List<cls_TRRound> list_model = new List<cls_TRRound>();

                        foreach (cls_TRRound item in jsonArray)
                        {
                            item.round_id = strID;
                            list_model.Add(item);
                        }

                        if (list_model.Count > 0)
                        {
                            cls_ctTRRound objItem = new cls_ctTRRound();
                            if (objItem.delete(strID))
                                objItem.insert(list_model);
                        }

                    }
                    catch { }


                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objRound.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteRound(InputMTRound input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTRound objRound = new cls_ctMTRound();

                bool blnResult = objRound.delete(input.round_id.ToString());

                if (blnResult)
                {
                    cls_ctTRRound objRound_ = new cls_ctTRRound();

                    objRound_.delete(input.round_code);

                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objRound.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region MTCompany
        public string getMTCompanyList()
        {
            JObject output = new JObject();

            cls_ctMTCompany objCom = new cls_ctMTCompany();
            List<cls_MTCompany> listCom = objCom.getDataByFillter("", "");
            JArray array = new JArray();

            if (listCom.Count > 0)
            {
                
                int index = 1;

                foreach (cls_MTCompany model in listCom)
                {
                    JObject json = new JObject();

                    json.Add("company_id", model.company_id);
                    json.Add("company_code", model.company_code);
                    json.Add("company_name_th", model.company_name_th);
                    json.Add("company_name_en", model.company_name_en);
                    json.Add("hrs_perday", model.hrs_perday);

                    json.Add("sso_com_rate", model.sso_com_rate);
                    json.Add("sso_emp_rate", model.sso_emp_rate);
                    json.Add("sso_min_wage", model.sso_min_wage);
                    json.Add("sso_max_wage", model.sso_max_wage);
                    json.Add("sso_min_age", model.sso_min_age);
                    json.Add("sso_max_age", model.sso_max_age);

                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageMTCompany(InputMTCompany input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTCompany objCom = new cls_ctMTCompany();
                cls_MTCompany model = new cls_MTCompany();

                model.company_id = input.company_id;
                model.company_code = input.company_code;
                model.company_name_th = input.company_name_th;
                model.company_name_en = input.company_name_en;
                model.hrs_perday = input.hrs_perday;
                model.sso_com_rate = input.sso_com_rate;
                model.sso_emp_rate = input.sso_emp_rate;
                model.sso_min_wage = input.sso_min_wage;
                model.sso_max_wage = input.sso_max_wage;
                model.sso_min_age = input.sso_min_age;
                model.sso_max_age = input.sso_max_age;

                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objCom.insert(model);

                if (blnResult)
                {
                    //-- Comcards
                    string card_data = input.card_data;

                    try
                    {
                        JObject jsonObject = new JObject();
                        var jsonArray = JsonConvert.DeserializeObject<List<cls_TRComcard>>(card_data);
                        List<cls_TRComcard> list_model = new List<cls_TRComcard>();
                        cls_ctTRComcard objCard = new cls_ctTRComcard();

                        bool blnClear = objCard.clear(input.company_code);
                        if (blnClear)
                        {
                            foreach (cls_TRComcard item in jsonArray)
                            {
                                item.company_code = input.company_code;
                                item.modified_by = input.modified_by;
                                objCard.insert(item);
                            }
                        }

                    }
                    catch { }



                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objCom.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTCompany(InputMTCompany input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTCompany objCom = new cls_ctMTCompany();

                bool blnResult = objCom.delete(input.company_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objCom.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region MTReason
        public string getMTReasonList(string group)
        {
            JObject output = new JObject();

            cls_ctMTReason objReason = new cls_ctMTReason();
            List<cls_MTReason> listReason = objReason.getDataByFillter(group, "", "");
            JArray array = new JArray();

            if (listReason.Count > 0)
            {
                
                int index = 1;

                foreach (cls_MTReason model in listReason)
                {
                    JObject json = new JObject();

                    json.Add("reason_id", model.reason_id);
                    json.Add("reason_code", model.reason_code);
                    json.Add("reason_name_th", model.reason_name_th);
                    json.Add("reason_name_en", model.reason_name_en);
                    json.Add("reason_group", model.reason_group);
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageMTReason(InputMTReason input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTReason objReason = new cls_ctMTReason();
                cls_MTReason model = new cls_MTReason();

                model.reason_id = input.reason_id;
                model.reason_code = input.reason_code;
                model.reason_name_th = input.reason_name_th;
                model.reason_name_en = input.reason_name_en;
                model.reason_group = input.reason_group;
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objReason.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objReason.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTReason(InputMTReason input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTReason objReason = new cls_ctMTReason();

                bool blnResult = objReason.delete(input.reason_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objReason.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion
        
        #region MTPosition
        public string getMTPositionList(string com)
        {
            JObject output = new JObject();

            cls_ctMTPosition objPosition = new cls_ctMTPosition();
            List<cls_MTPosition> listPosition = objPosition.getDataByFillter(com, "", "");
            JArray array = new JArray();

            if (listPosition.Count > 0)
            {
                
                int index = 1;

                foreach (cls_MTPosition model in listPosition)
                {
                    JObject json = new JObject();

                    json.Add("position_id", model.position_id);
                    json.Add("position_code", model.position_code);
                    json.Add("position_name_th", model.position_name_th);
                    json.Add("position_name_en", model.position_name_en);
                    json.Add("company_code", model.company_code);
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageMTPosition(InputMTPosition input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTPosition objPosition = new cls_ctMTPosition();
                cls_MTPosition model = new cls_MTPosition();

                model.position_id = input.position_id;
                model.position_code = input.position_code;

                model.position_name_th = input.position_name_th;
                model.position_name_en = input.position_name_en;
                model.company_code = input.company_code;
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objPosition.insert(model);

                if (blnResult)
                {                    
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objPosition.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTPosition(InputMTPosition input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTPosition objPosition = new cls_ctMTPosition();

                bool blnResult = objPosition.delete(input.position_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objPosition.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region MTLevel
        public string getMTLevelList(string com)
        {
            JObject output = new JObject();

            cls_ctMTLevel objLevel = new cls_ctMTLevel();
            List<cls_MTLevel> listLevel = objLevel.getDataByFillter(com, "", "");
            JArray array = new JArray();

            if (listLevel.Count > 0)
            {

                int index = 1;

                foreach (cls_MTLevel model in listLevel)
                {
                    JObject json = new JObject();

                    json.Add("level_id", model.level_id);
                    json.Add("level_code", model.level_code);
                    json.Add("level_name_th", model.level_name_th);
                    json.Add("level_name_en", model.level_name_en);
                    json.Add("company_code", model.company_code);
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageMTLevel(InputMTLevel input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTLevel objLevel = new cls_ctMTLevel();
                cls_MTLevel model = new cls_MTLevel();

                model.level_id = input.level_id;
                model.level_code = input.level_code;

                model.level_name_th = input.level_name_th;
                model.level_name_en = input.level_name_en;
                model.company_code = input.company_code;
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objLevel.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objLevel.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTLevel(InputMTLevel input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTLevel objLevel = new cls_ctMTLevel();

                bool blnResult = objLevel.delete(input.level_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objLevel.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region MTDep
        public string getMTDepList(string com, string level, string parent)
        {
            JObject output = new JObject();

            cls_ctMTDep objDep = new cls_ctMTDep();
            List<cls_MTDep> listDep = objDep.getDataByFillter(com, level, parent, "", "");
            JArray array = new JArray();

            if (listDep.Count > 0)
            {
                
                int index = 1;

                foreach (cls_MTDep model in listDep)
                {
                    JObject json = new JObject();

                    json.Add("dep_id", model.dep_id);
                    json.Add("dep_code", model.dep_code);
                    json.Add("dep_name_th", model.dep_name_th);
                    json.Add("dep_name_en", model.dep_name_en);

                    json.Add("dep_parent", model.dep_parent);
                    json.Add("dep_level", model.dep_level);

                    json.Add("company_code", model.company_code);
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageMTDep(InputMTDep input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTDep objDep = new cls_ctMTDep();
                cls_MTDep model = new cls_MTDep();

                model.dep_id = input.dep_id;
                model.dep_code = input.dep_code;

                model.dep_name_th = input.dep_name_th;
                model.dep_name_en = input.dep_name_en;

                model.dep_parent = input.dep_parent;
                model.dep_level = input.dep_level;

                model.company_code = input.company_code;
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objDep.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objDep.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTDep(InputMTDep input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTDep objDep = new cls_ctMTDep();

                bool blnResult = objDep.delete(input.dep_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objDep.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region MTReduce
        public string getMTReduceList()
        {
            JObject output = new JObject();

            cls_ctMTReduce objReduce = new cls_ctMTReduce();
            List<cls_MTReduce> listReduce = objReduce.getDataByFillter("", "");

            JArray array = new JArray();

            if (listReduce.Count > 0)
            {
                int index = 1;

                foreach (cls_MTReduce model in listReduce)
                {
                    JObject json = new JObject();

                    json.Add("reduce_id", model.reduce_id);
                    json.Add("reduce_code", model.reduce_code);
                    json.Add("reduce_name_th", model.reduce_name_th);
                    json.Add("reduce_name_en", model.reduce_name_en);

                    json.Add("reduce_amount", model.reduce_amount);
                    json.Add("reduce_percent", model.reduce_percent);
                    json.Add("reduce_percent_max", model.reduce_percent_max);

                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageMTReduce(InputMTReduce input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTReduce objReduce = new cls_ctMTReduce();
                cls_MTReduce model = new cls_MTReduce();

                model.reduce_id = input.reduce_id;
                model.reduce_code = input.reduce_code;

                model.reduce_name_th = input.reduce_name_th;
                model.reduce_name_en = input.reduce_name_en;

                model.reduce_amount = input.reduce_amount;
                model.reduce_percent = input.reduce_percent;
                model.reduce_percent_max = input.reduce_percent_max;
                
                model.modified_by = input.modified_by;
                //model.modified_date = input.modified_date;
                model.flag = model.flag;

                bool blnResult = objReduce.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objReduce.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTReduce(InputMTReduce input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTReduce objReduce = new cls_ctMTReduce();

                bool blnResult = objReduce.delete(input.reduce_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objReduce.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region MTCardtype
        public string getMTCardtypeList()
        {
            JObject output = new JObject();

            cls_ctMTCardtype objCardtype = new cls_ctMTCardtype();
            List<cls_MTCardtype> listCardtype = objCardtype.getDataByFillter("", "");

            JArray array = new JArray();

            if (listCardtype.Count > 0)
            {
                int index = 1;

                foreach (cls_MTCardtype model in listCardtype)
                {
                    JObject json = new JObject();

                    json.Add("cardtype_id", model.cardtype_id);
                    json.Add("cardtype_code", model.cardtype_code);
                    json.Add("cardtype_name_th", model.cardtype_name_th);
                    json.Add("cardtype_name_en", model.cardtype_name_en);
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageMTCardtype(InputMTCardtype input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTCardtype objCardtype = new cls_ctMTCardtype();
                cls_MTCardtype model = new cls_MTCardtype();

                model.cardtype_id = input.cardtype_id;
                model.cardtype_code = input.cardtype_code;

                model.cardtype_name_th = input.cardtype_name_th;
                model.cardtype_name_en = input.cardtype_name_en;
                model.modified_by = input.modified_by;
                //model.modified_date = input.modified_date;
                model.flag = model.flag;

                bool blnResult = objCardtype.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objCardtype.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTCardtype(InputMTCardtype input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTCardtype objCardtype = new cls_ctMTCardtype();

                bool blnResult = objCardtype.delete(input.cardtype_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objCardtype.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region MTFamily
        public string getMTFamilyList()
        {
            JObject output = new JObject();

            cls_ctMTFamily objFamily = new cls_ctMTFamily();
            List<cls_MTFamily> listFamily = objFamily.getDataByFillter("", "");

            JArray array = new JArray();

            if (listFamily.Count > 0)
            {
                int index = 1;

                foreach (cls_MTFamily model in listFamily)
                {
                    JObject json = new JObject();

                    json.Add("family_id", model.family_id);
                    json.Add("family_code", model.family_code);
                    json.Add("family_name_th", model.family_name_th);
                    json.Add("family_name_en", model.family_name_en);
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageMTFamily(InputMTFamily input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTFamily objFamily = new cls_ctMTFamily();
                cls_MTFamily model = new cls_MTFamily();

                model.family_id = input.family_id;
                model.family_code = input.family_code;
                model.family_name_th = input.family_name_th;
                model.family_name_en = input.family_name_en;
                model.modified_by = input.modified_by;           
                model.flag = model.flag;

                bool blnResult = objFamily.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objFamily.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTFamily(InputMTFamily input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTFamily objFamily = new cls_ctMTFamily();

                bool blnResult = objFamily.delete(input.family_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objFamily.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion
        
        #region MTItem
        public string getMTItemList(string com)
        {
            JObject output = new JObject();

            cls_ctMTItem objItem = new cls_ctMTItem();
            List<cls_MTItem> listItem = objItem.getDataByFillter(com, "", "");

            JArray array = new JArray();

            if (listItem.Count > 0)
            {
                int index = 1;

                foreach (cls_MTItem model in listItem)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("item_id", model.item_id);
                    json.Add("item_code", model.item_code);
                    json.Add("item_name_th", model.item_name_th);
                    json.Add("item_name_en", model.item_name_en);
                    json.Add("item_type", model.item_type);
                    json.Add("item_regular", model.item_regular);
                    json.Add("item_caltax", model.item_caltax);
                    json.Add("item_calpf", model.item_calpf);
                    json.Add("item_calsso", model.item_calsso);
                    json.Add("item_calot", model.item_calot);
                    json.Add("item_contax", model.item_contax);
                    json.Add("item_section", model.item_section);
                    json.Add("item_rate", model.item_rate);
                    json.Add("item_account", model.item_account);
                    
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageMTItem(InputMTItem input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTItem objItem = new cls_ctMTItem();
                cls_MTItem model = new cls_MTItem();

                model.company_code = input.company_code;
                
                model.item_id = input.item_id;
                model.item_code = input.item_code;
                model.item_name_th = input.item_name_th;
                model.item_name_en = input.item_name_en;
                model.item_type = input.item_type;
                model.item_regular = input.item_regular;
                model.item_caltax = input.item_caltax;
                model.item_calpf = input.item_calpf;
                model.item_calsso = input.item_calsso;
                model.item_calot = input.item_calot;
                model.item_contax = input.item_contax;
                model.item_section = input.item_section;
                model.item_rate = input.item_rate;
                model.item_account = input.item_account;
                
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objItem.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objItem.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTItem(InputMTItem input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTItem objItem = new cls_ctMTItem();

                bool blnResult = objItem.delete(input.item_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objItem.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion
                
        #region MTYear
        public string getMTYearList(string com, string group)
        {
            JObject output = new JObject();

            cls_ctMTYear objYear = new cls_ctMTYear();
            List<cls_MTYear> listYear = objYear.getDataByFillter(com, group, "", "");

            JArray array = new JArray();

            if (listYear.Count > 0)
            {
                int index = 1;

                foreach (cls_MTYear model in listYear)
                {
                    JObject json = new JObject();

                    json.Add("year_id", model.year_id);
                    json.Add("year_code", model.year_code);
                    json.Add("year_name_th", model.year_name_th);
                    json.Add("year_name_en", model.year_name_en);

                    json.Add("year_fromdate", model.year_fromdate);
                    json.Add("year_todate", model.year_todate);
                    json.Add("year_group", model.year_group);

                    json.Add("company_code", model.company_code);
                    
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageMTYear(InputMTYear input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTYear objYear = new cls_ctMTYear();
                cls_MTYear model = new cls_MTYear();

                model.company_code = input.company_code;

                model.year_id = input.year_id;
                model.year_code = input.year_code;
                model.year_name_th = input.year_name_th;
                model.year_name_en = input.year_name_en;
                model.year_fromdate = Convert.ToDateTime(input.year_fromdate);
                model.year_todate = Convert.ToDateTime(input.year_todate);
                model.year_group = input.year_group;
                model.company_code = input.company_code;
               
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objYear.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objYear.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTYear(InputMTYear input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTYear objYear = new cls_ctMTYear();

                bool blnResult = objYear.delete(input.year_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objYear.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region Task
        public string getMTTaskList(string com, string type, string status)
        {
            JObject output = new JObject();

            cls_ctMTTask objTask = new cls_ctMTTask();
            List<cls_MTTask> listTask = objTask.getDataByFillter(com, "", type, status);

            JArray array = new JArray();

            if (listTask.Count > 0)
            {
                int index = 1;

                foreach (cls_MTTask model in listTask)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("task_id", model.task_id);
                    json.Add("task_type", model.task_type);
                    json.Add("task_status", model.task_status);

                    json.Add("task_start", this.doCheckDateTimeEmpty(model.task_start));
                    json.Add("task_end", this.doCheckDateTimeEmpty(model.task_end));
                    json.Add("task_note", model.task_note);

                    if (model.task_type.Equals("IMP_TIME"))
                    {
                        json.Add("task_detail", model.taskdetail_process);
                    }

                    else if (model.task_type.Equals("IMP_XLS"))
                    {
                        json.Add("task_detail", model.task_note);
                    }

                    else
                    {
                        json.Add("task_detail", model.taskdetail_process + " (" + model.taskdetail_fromdate.ToString("dd/MM/yy") + "-" + model.taskdetail_todate.ToString("dd/MM/yy") + ":" + model.taskdetail_paydate.ToString("dd/MM/yy") + ")");
                    }
                   
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string getTRTaskdetail(string id)
        {
            JObject output = new JObject();

            cls_ctMTTask objTask = new cls_ctMTTask();
            cls_TRTaskdetail task_detail = objTask.getTaskDetail(id);

            JArray array = new JArray();

            if (task_detail != null)
            {
               
                JObject json = new JObject();

                json.Add("task_id", task_detail.task_id);
                json.Add("taskdetail_process", task_detail.taskdetail_process);
                json.Add("taskdetail_fromdate", task_detail.taskdetail_fromdate);
                json.Add("taskdetail_todate", task_detail.taskdetail_todate);
                json.Add("taskdetail_paydate", task_detail.taskdetail_paydate);               

                array.Add(json);

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string getTRTaskwhose(string id)
        {
            JObject output = new JObject();

            cls_ctMTTask objTask = new cls_ctMTTask();
            List<cls_TRTaskwhose> listWhose = objTask.getTaskWhose(id);

            JArray array = new JArray();

            if (listWhose.Count > 0)
            {
                int index = 1;

                foreach (cls_TRTaskwhose model in listWhose)
                {
                    JObject json = new JObject();
                                        
                    json.Add("task_id", model.task_id);
                    json.Add("worker_code", model.worker_code);
                    
                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageTask(InputMTTask input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTTask objTask = new cls_ctMTTask();
                cls_MTTask model = new cls_MTTask();

                cls_TRTaskdetail detail = new cls_TRTaskdetail();

                List<cls_TRTaskwhose> list_whose = new List<cls_TRTaskwhose>();

                model.company_code = input.company_code;

                model.task_id = input.task_id;
                model.task_type = input.task_type;
                model.task_status = input.task_status;
             
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                //-- Task detail
                string detail_data = input.detail_data;

                string whose_data = input.whose_data;

                try
                {                    
                    var jsonArray = JsonConvert.DeserializeObject<List<cls_TRTaskdetail>>(detail_data);
                    List<cls_TRTaskdetail> list_model = new List<cls_TRTaskdetail>();
                    foreach (cls_TRTaskdetail item in jsonArray)
                    {
                        item.task_id = model.task_id;                        
                        list_model.Add(item);
                    }

                    if (list_model.Count > 0)
                    {
                        detail = list_model[0];
                    }

                    //--
                    var jsonArray2 = JsonConvert.DeserializeObject<List<cls_TRTaskwhose>>(whose_data);
                    foreach (cls_TRTaskwhose item in jsonArray2)
                    {
                        item.task_id = model.task_id;
                        list_whose.Add(item);
                    }

                }
                catch (Exception ex)
                {
                    string str = ex.ToString();
                }

                int intTaskID = objTask.insert(model, detail, list_whose);

                if (intTaskID > 0)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                    //SUM_TAX
                    if (input.task_type.Trim().Equals("CAL_TAX"))
                    {
                        cls_srvProcessPayroll srvPay = new cls_srvProcessPayroll();
                        srvPay.doCalculateTax(input.company_code, intTaskID.ToString());
                    }
                    //SUM_TAX


                    //SUM_TIME
                    else if (input.task_type.Trim().Equals("SUM_TIME"))
                    {
                        cls_srvProcessTime srvTime = new cls_srvProcessTime();
                        srvTime.doSummarizeTime(input.company_code, intTaskID.ToString());
                    }
                        //SUM_TIME



                    //TIME
                    else if (input.task_type.Trim().Equals("CAL_TIME"))
                    {
                        cls_srvProcessTime srvTime = new cls_srvProcessTime();
                        srvTime.doCalculateTime(input.company_code, intTaskID.ToString());
                    }
                    //TIME
                    else if (input.task_type.Trim().Equals("IMP_TIME"))
                    {
                        cls_srvProcessTime srvTime = new cls_srvProcessTime();
                        srvTime.doImportTime(input.company_code, intTaskID.ToString());
                    }
                    //TIME


                    //BONUS
                    else if (input.task_type.Trim().Equals("CAL_BONUS"))
                    {
                        cls_srvProcessPayroll srvPay = new cls_srvProcessPayroll();
                        srvPay.doCalculateBonus(input.company_code, intTaskID.ToString());
                    }
                    //BONUS

                    //BANK
                    else if (input.task_type.Trim().Equals("TRN_BANK"))
                    {
                        cls_srvProcessPayroll srvPay = new cls_srvProcessPayroll();
                        string link = srvPay.doExportBank(input.company_code, intTaskID.ToString());

                        output["result_link"] = link;
                    }
                        //BANK
                    //SSF
                    else if (input.task_type.Trim().Equals("TRN_SSF"))
                    {
                        cls_srvProcessPayroll srvPay = new cls_srvProcessPayroll();
                        string link = srvPay.doExportSSF(input.company_code, intTaskID.ToString());
                        output["result_link"] = link;
                    }
                    //SSF

                        //SSO
                    else if (input.task_type.Trim().Equals("TRN_SSO"))
                    {
                        cls_srvProcessPayroll srvPay = new cls_srvProcessPayroll();
                        string link = srvPay.doExportSso(input.company_code, intTaskID.ToString());
                        //string link = srvPay.doExportSso(input.company_code, intTaskID.ToString());
                        output["result_link"] = link;
                    }
                        //SSO

                        //ATX
                    else if (input.task_type.Trim().Equals("TRN_TAX"))
                    {
                        cls_srvProcessPayroll srvPay = new cls_srvProcessPayroll();
                        string link = srvPay.doExportTax(input.company_code, intTaskID.ToString());

                        output["result_link"] = link;
                    }
                    //ATX

                    //SAP
                    else if (input.task_type.Trim().Equals("TRN_SAP"))
                    {
                        cls_srvProcessPayroll srvPay = new cls_srvProcessPayroll();
                        string link = srvPay.doExportSap(input.company_code, intTaskID.ToString());

                        output["result_link"] = link;
                    }
                    //SAP

                        //APF
                    else if (input.task_type.Trim().Equals("TRN_PF"))
                    {
                        cls_srvProcessPayroll srvPay = new cls_srvProcessPayroll();
                        string link = srvPay.doExportPF(input.company_code, intTaskID.ToString());

                        output["result_link"] = link;
                    }
                    //IMP_XLS
                    else if (input.task_type.Trim().Equals("IMP_XLS"))
                    {
                        cls_srvImport srvImport = new cls_srvImport();
                        string link = srvImport.doImportExcel(input.company_code, intTaskID.ToString());

                    }

                 

                    //IMP_XLS
                   


                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objTask.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }

        public string doDeleteMTTask(InputMTTask input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTTask objTask = new cls_ctMTTask();

                bool blnResult = objTask.delete(input.task_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objTask.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region MTCourse
        public string getMTCourseList(string com)
        {
            JObject output = new JObject();

            cls_ctMTCourse objCourse = new cls_ctMTCourse();
            List<cls_MTCourse> listCourse = objCourse.getDataByFillter(com, "", "");

            JArray array = new JArray();

            if (listCourse.Count > 0)
            {

                int index = 1;

                foreach (cls_MTCourse model in listCourse)
                {
                    JObject json = new JObject();

                    json.Add("course_id", model.course_id);
                    json.Add("course_code", model.course_code);
                    json.Add("course_name_th", model.course_name_th);
                    json.Add("course_name_en", model.course_name_en);
                    json.Add("company_code", model.company_code);
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }

        public string doManageMTCourse(InputMTCourse input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTCourse objCourse = new cls_ctMTCourse();
                cls_MTCourse model = new cls_MTCourse();

                model.course_id = input.course_id;
                model.course_code = input.course_code;

                model.course_name_th = input.course_name_th;
                model.course_name_en = input.course_name_en;
                model.company_code = input.company_code;
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objCourse.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objCourse.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTCourse(InputMTCourse input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTCourse objCourse = new cls_ctMTCourse();

                bool blnResult = objCourse.delete(input.course_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objCourse.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region MTFaculty
        public string getMTFacultyList(string com)
        {
            JObject output = new JObject();

            cls_ctMTFaculty objFaculty = new cls_ctMTFaculty();
            List<cls_MTFaculty> listFaculty = objFaculty.getDataByFillter(com, "", "");

            JArray array = new JArray();

            if (listFaculty.Count > 0)
            {

                int index = 1;

                foreach (cls_MTFaculty model in listFaculty)
                {
                    JObject json = new JObject();

                    json.Add("faculty_id", model.faculty_id);
                    json.Add("faculty_code", model.faculty_code);
                    json.Add("faculty_name_th", model.faculty_name_th);
                    json.Add("faculty_name_en", model.faculty_name_en);
                    json.Add("company_code", model.company_code);
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }

        public string doManageMTFaculty(InputMTFaculty input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTFaculty objFaculty = new cls_ctMTFaculty();
                cls_MTFaculty model = new cls_MTFaculty();

                model.faculty_id = input.faculty_id;
                model.faculty_code = input.faculty_code;

                model.faculty_name_th = input.faculty_name_th;
                model.faculty_name_en = input.faculty_name_en;
                model.company_code = input.company_code;
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objFaculty.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objFaculty.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTFaculty(InputMTFaculty input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTFaculty objFaculty = new cls_ctMTFaculty();

                bool blnResult = objFaculty.delete(input.faculty_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objFaculty.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region MTMajor
        public string getMTMajorList(string com)
        {
            JObject output = new JObject();

            cls_ctMTMajor objMajor = new cls_ctMTMajor();
            List<cls_MTMajor> listMajor = objMajor.getDataByFillter(com, "", "");

            JArray array = new JArray();

            if (listMajor.Count > 0)
            {

                int index = 1;

                foreach (cls_MTMajor model in listMajor)
                {
                    JObject json = new JObject();

                    json.Add("major_id", model.major_id);
                    json.Add("major_code", model.major_code);
                    json.Add("major_name_th", model.major_name_th);
                    json.Add("major_name_en", model.major_name_en);
                    json.Add("company_code", model.company_code);
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }

        public string doManageMTMajor(InputMTMajor input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTMajor objMajor = new cls_ctMTMajor();
                cls_MTMajor model = new cls_MTMajor();

                model.major_id = input.major_id;
                model.major_code = input.major_code;

                model.major_name_th = input.major_name_th;
                model.major_name_en = input.major_name_en;
                model.company_code = input.company_code;
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objMajor.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objMajor.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTMajor(InputMTMajor input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTMajor objMajor = new cls_ctMTMajor();

                bool blnResult = objMajor.delete(input.major_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objMajor.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region MTInstitute
        public string getMTInstituteList(string com)
        {
            JObject output = new JObject();

            cls_ctMTInstitute objInstitute = new cls_ctMTInstitute();
            List<cls_MTInstitute> listInstitute = objInstitute.getDataByFillter(com, "", "");

            JArray array = new JArray();

            if (listInstitute.Count > 0)
            {

                int index = 1;

                foreach (cls_MTInstitute model in listInstitute)
                {
                    JObject json = new JObject();

                    json.Add("institute_id", model.institute_id);
                    json.Add("institute_code", model.institute_code);
                    json.Add("institute_name_th", model.institute_name_th);
                    json.Add("institute_name_en", model.institute_name_en);
                    json.Add("company_code", model.company_code);
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }

        public string doManageMTInstitute(InputMTInstitute input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTInstitute objInstitute = new cls_ctMTInstitute();
                cls_MTInstitute model = new cls_MTInstitute();

                model.institute_id = input.institute_id;
                model.institute_code = input.institute_code;

                model.institute_name_th = input.institute_name_th;
                model.institute_name_en = input.institute_name_en;
                model.company_code = input.company_code;
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objInstitute.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objInstitute.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTInstitute(InputMTInstitute input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTInstitute objInstitute = new cls_ctMTInstitute();

                bool blnResult = objInstitute.delete(input.institute_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objInstitute.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region MTQualification
        public string getMTQualificationList(string com)
        {
            JObject output = new JObject();

            cls_ctMTQualification objQualification = new cls_ctMTQualification();
            List<cls_MTQualification> listQualification = objQualification.getDataByFillter(com, "", "");

            JArray array = new JArray();

            if (listQualification.Count > 0)
            {

                int index = 1;

                foreach (cls_MTQualification model in listQualification)
                {
                    JObject json = new JObject();

                    json.Add("qualification_id", model.qualification_id);
                    json.Add("qualification_code", model.qualification_code);
                    json.Add("qualification_name_th", model.qualification_name_th);
                    json.Add("qualification_name_en", model.qualification_name_en);
                    json.Add("company_code", model.company_code);
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }

        public string doManageMTQualification(InputMTQualification input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTQualification objQualification = new cls_ctMTQualification();
                cls_MTQualification model = new cls_MTQualification();

                model.qualification_id = input.qualification_id;
                model.qualification_code = input.qualification_code;

                model.qualification_name_th = input.qualification_name_th;
                model.qualification_name_en = input.qualification_name_en;
                model.company_code = input.company_code;
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objQualification.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objQualification.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTQualification(InputMTQualification input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTQualification objQualification = new cls_ctMTQualification();

                bool blnResult = objQualification.delete(input.qualification_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objQualification.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region TRTaxrate
        public string getTRTaxrateList(string com)
        {
            JObject output = new JObject();

            cls_ctTRTaxrate objRate = new cls_ctTRTaxrate();
            List<cls_TRTaxrate> listRate = objRate.getDataByFillter(com);

            JArray array = new JArray();

            if (listRate.Count > 0)
            {
                int index = 1;

                foreach (cls_TRTaxrate model in listRate)
                {
                    JObject json = new JObject();

                    json.Add("taxrate_id", model.taxrate_id);
                    json.Add("taxrate_from", model.taxrate_from);
                    json.Add("taxrate_to", model.taxrate_to);
                    json.Add("taxrate_tax", model.taxrate_tax);
                    
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageTRTaxrate(InputTRTaxrate input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRTaxrate objRate = new cls_ctTRTaxrate();
                cls_TRTaxrate model = new cls_TRTaxrate();

                model.company_code = input.company_code;
                model.taxrate_id = input.taxrate_id;
                model.taxrate_from = input.taxrate_from;
                model.taxrate_to = input.taxrate_to;
                model.taxrate_tax = input.taxrate_tax;
                model.modified_by = input.modified_by;                
                model.flag = model.flag;

                bool blnResult = objRate.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objRate.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTRTaxrate(InputTRTaxrate input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRTaxrate objRate = new cls_ctTRTaxrate();

                bool blnResult = objRate.delete(input.taxrate_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objRate.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region Policy structure code
        public string getSYSCodestructureList()
        {
            JObject output = new JObject();

            cls_ctSYSCodestructure objStruc = new cls_ctSYSCodestructure();
            List<cls_SYSCodestructure> listStruc = objStruc.getData();

            JArray array = new JArray();

            if (listStruc.Count > 0)
            {
                int index = 1;

                foreach (cls_SYSCodestructure model in listStruc)
                {
                    JObject json = new JObject();

                    json.Add("codestructure_code", model.codestructure_code);
                    json.Add("codestructure_name_th", model.codestructure_name_th);
                    json.Add("codestructure_name_en", model.codestructure_name_en);
                    
                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string getMTPolcode(string com, string type)
        {
            JObject output = new JObject();

            cls_ctMTPolcode objPol = new cls_ctMTPolcode();
            
            List<cls_MTPolcode> listPol = objPol.getDataByFillter(com, "", type);

            JArray array = new JArray();

            if (listPol.Count > 0)
            {
                int index = 1;

                foreach (cls_MTPolcode model in listPol)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("polcode_id", model.polcode_id);
                    json.Add("polcode_type", model.polcode_type);
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string getTRPolcode(string id)
        {
            JObject output = new JObject();

            cls_ctTRPolcode objTRPolcode = new cls_ctTRPolcode();
            List<cls_TRPolcode> listTRPolcode = objTRPolcode.getDataByFillter(id);

            JArray array = new JArray();

            if (listTRPolcode.Count > 0)
            {
                int index = 1;

                foreach (cls_TRPolcode model in listTRPolcode)
                {
                    JObject json = new JObject();

                    json.Add("polcode_id", model.polcode_id);
                    json.Add("codestructure_code", model.codestructure_code);
                    json.Add("polcode_lenght", model.polcode_lenght);
                    json.Add("polcode_text", model.polcode_text);
                    json.Add("polcode_order", model.polcode_order);
                   
                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManagePolcode(InputMTPolcode input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTPolcode objMTPolcode = new cls_ctMTPolcode();
                cls_MTPolcode model = new cls_MTPolcode();
                
                model.company_code = input.company_code;
                model.polcode_id = input.polcode_id;
                model.polcode_type = input.polcode_type;
                model.modified_by = input.modified_by;
                model.flag = model.flag;                               

                string strID = objMTPolcode.insert(model);

                if (!strID.Equals(""))
                {
                    string polcode_data = input.polcode_data;

                    try
                    {
                        JObject jsonObject = new JObject();
                        var jsonArray = JsonConvert.DeserializeObject<List<cls_TRPolcode>>(polcode_data);


                        List<cls_TRPolcode> list_model = new List<cls_TRPolcode>();

                        int intID = Convert.ToInt32(strID);

                        foreach (cls_TRPolcode item in jsonArray)
                        {
                            item.polcode_id = intID;                           
                            list_model.Add(item);
                        }

                        if (list_model.Count > 0)
                        {
                            cls_ctTRPolcode objTRPolcode = new cls_ctTRPolcode();
                            objTRPolcode.insert(list_model);
                        }

                    }
                    catch (Exception ex)
                    {
                        string str = ex.ToString();
                    }


                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objMTPolcode.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTPolcode(InputMTPolcode input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTPolcode objMTPolcode = new cls_ctMTPolcode();

                bool blnResult = objMTPolcode.delete(input.polcode_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objMTPolcode.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }

        public string getNewCode(string com, string type, string emptype)
        {
            JObject output = new JObject();

            cls_ctMTPolcode objPol = new cls_ctMTPolcode();

            List<cls_MTPolcode> listPol = objPol.getDataByFillter(com, "", type);


            if (listPol.Count > 0)
            {
                string strID = "";

                cls_MTPolcode polcode = listPol[0];

                cls_ctTRPolcode objTRPolcode = new cls_ctTRPolcode();
                List<cls_TRPolcode> listTRPolcode = objTRPolcode.getDataByFillter(polcode.polcode_id.ToString());

                foreach (cls_TRPolcode model in listTRPolcode)
                {

                    switch (model.codestructure_code)
                    {

                        case "1CHA":
                            strID += model.polcode_text.Substring(0, model.polcode_lenght);
                            break;

                        case "2COM":
                            strID += com.Substring(0, model.polcode_lenght);
                            break;

                        case "3BRA":
                            break;

                        case "4EMT":
                            strID += emptype;
                            break;

                        case "5YEA":
                            DateTime dateNowY = DateTime.Now;
                            string formatY = "";
                            for(int i=0; i<model.polcode_lenght; i++)
                            {
                                formatY += "y";
                            }                            
                            strID += dateNowY.ToString(formatY);
                            break;

                        case "6MON":
                            DateTime dateNowM = DateTime.Now;
                            string formatM = "";
                            for (int i = 0; i < model.polcode_lenght; i++)
                            {
                                formatM += "M";
                            }
                            strID += dateNowM.ToString(formatM);
                            break;

                        case "MAUT":
                            cls_ctMTWorker objWorker = new cls_ctMTWorker();
                            int intRunningID = objWorker.doGetNextRunningID(com, strID);
                            strID += intRunningID.ToString().PadLeft(model.polcode_lenght, '0');
                            break;

                    }


                }




                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = strID;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = "";
            }

            return output.ToString(Formatting.None);
        }

        #endregion

        #region MTPeriod
        public string getMTPeriodList(string com, string type, string emptype, string year)
        {
            JObject output = new JObject();

            cls_ctMTPeriod objPeriod = new cls_ctMTPeriod();
            List<cls_MTPeriod> listPeriod = objPeriod.getDataByFillter("", com, type, year, emptype);

            JArray array = new JArray();

            if (listPeriod.Count > 0)
            {

                int index = 1;

                foreach (cls_MTPeriod model in listPeriod)
                {
                    JObject json = new JObject();
                    json.Add("company_code", model.company_code);
                    json.Add("period_id", model.period_id);
                    json.Add("period_type", model.period_type);
                    json.Add("emptype_code", model.emptype_code);
                    json.Add("year_code", model.year_code);
                    json.Add("period_no", model.period_no);              

                    json.Add("period_name_th", model.period_name_th);
                    json.Add("period_name_en", model.period_name_en);

                    json.Add("period_from", model.period_from);
                    json.Add("period_to", model.period_to);
                    json.Add("period_payment", model.period_payment);
                    json.Add("period_dayonperiod", model.period_dayonperiod);
                    
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageMTPeriod(InputMTPeriod input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTPeriod objPeriod = new cls_ctMTPeriod();
                cls_MTPeriod model = new cls_MTPeriod();

                model.company_code = input.company_code;

                model.period_id = input.period_id;
                model.period_type = input.period_type;
                model.emptype_code = input.emptype_code;
                model.year_code = input.year_code;
                model.period_no = input.period_no;

                model.period_name_th = input.period_name_th;
                model.period_name_en = input.period_name_en;

                model.period_from = Convert.ToDateTime(input.period_from);
                model.period_to = Convert.ToDateTime(input.period_to);
                model.period_payment = Convert.ToDateTime(input.period_payment);

                model.period_dayonperiod = input.period_dayonperiod;
                
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objPeriod.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objPeriod.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTPeriod(InputMTPeriod input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTPeriod objPeriod = new cls_ctMTPeriod();

                bool blnResult = objPeriod.delete(input.period_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objPeriod.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region TRCombranch
        public string getTRCombranchList(string com)
        {
            JObject output = new JObject();

            cls_ctTRCombranch objCombranch = new cls_ctTRCombranch();
            List<cls_TRCombranch> listCombranch = objCombranch.getDataByFillter(com, "", "");
            JArray array = new JArray();

            if (listCombranch.Count > 0)
            {

                int index = 1;

                foreach (cls_TRCombranch model in listCombranch)
                {
                    JObject json = new JObject();

                    json.Add("combranch_id", model.combranch_id);
                    json.Add("combranch_code", model.combranch_code);
                    json.Add("combranch_name_th", model.combranch_name_th);
                    json.Add("combranch_name_en", model.combranch_name_en);
                    json.Add("company_code", model.company_code);
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageTRCombranch(InputTRCombranch input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRCombranch objCombranch = new cls_ctTRCombranch();
                cls_TRCombranch model = new cls_TRCombranch();

                model.combranch_id = input.combranch_id;
                model.combranch_code = input.combranch_code;
                model.combranch_name_th = input.combranch_name_th;
                model.combranch_name_en = input.combranch_name_en;
                model.company_code = input.company_code;
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objCombranch.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objCombranch.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTRCombranch(InputTRCombranch input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRCombranch objCombranch = new cls_ctTRCombranch();

                bool blnResult = objCombranch.delete(input.combranch_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objCombranch.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region TRComaddress
        public string getTRComaddressList(string com, string branch, string type)
        {
            JObject output = new JObject();

            cls_ctTRComaddress objAddr = new cls_ctTRComaddress();
            List<cls_TRComaddress> listAddr = objAddr.getDataByFillter(com, branch, type);
            JArray array = new JArray();

            if (listAddr.Count > 0)
            {
                int index = 1;

                foreach (cls_TRComaddress model in listAddr)
                {
                    JObject json = new JObject();

                    json.Add("comaddress_type", model.comaddress_type);
                    json.Add("comaddress_no", model.comaddress_no);
                    json.Add("comaddress_moo", model.comaddress_moo);
                    json.Add("comaddress_soi", model.comaddress_soi);
                    json.Add("comaddress_road", model.comaddress_road);
                    json.Add("comaddress_tambon", model.comaddress_tambon);
                    json.Add("comaddress_amphur", model.comaddress_amphur);
                    json.Add("comaddress_zipcode", model.comaddress_zipcode);
                    json.Add("comaddress_tel", model.comaddress_tel);
                    json.Add("comaddress_email", model.comaddress_email);
                    json.Add("comaddress_line", model.comaddress_line);
                    json.Add("comaddress_facebook", model.comaddress_facebook);
                    json.Add("province_code", model.province_code);

                    json.Add("company_code", model.company_code);
                    json.Add("combranch_code", model.combranch_code);

                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);
                    json.Add("change", false);
                    json.Add("index", index);
                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageTRComaddressList(InputTRList input)
        {
            JObject output = new JObject();

            try
            {
                string company_code = input.company_code;
                

                //-- Transaction
                string addr_data = input.transaction_data;
                bool blnResult = true;
                string strMessage = "";

                try
                {
                    JObject jsonObject = new JObject();
                    var jsonArray = JsonConvert.DeserializeObject<List<cls_TRComaddress>>(addr_data);
                    cls_ctTRComaddress objAddr = new cls_ctTRComaddress();

                    foreach (cls_TRComaddress item in jsonArray)
                    {                        
                        item.company_code = company_code;
                        item.modified_by = input.modified_by;
                        blnResult = objAddr.insert(item);

                        if (!blnResult)
                        {
                            strMessage = objAddr.getMessage();
                        }
                    }
                }
                catch
                {
                    blnResult = false;
                }

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = strMessage;
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTRComaddress(InputTRComaddress input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRComaddress objAddr = new cls_ctTRComaddress();

                bool blnResult = objAddr.delete(input.company_code, input.combranch_code, input.comaddress_type);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objAddr.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region MTProvince
        public string getMTProvinceList()
        {
            JObject output = new JObject();

            cls_ctMTProvince objProvince = new cls_ctMTProvince();
            List<cls_MTProvince> listProvince = objProvince.getDataByFillter("", "");
            JArray array = new JArray();

            if (listProvince.Count > 0)
            {

                int index = 1;

                foreach (cls_MTProvince model in listProvince)
                {
                    JObject json = new JObject();

                    json.Add("province_id", model.province_id);
                    json.Add("province_code", model.province_code);
                    json.Add("province_name_th", model.province_name_th);
                    json.Add("province_name_en", model.province_name_en);                    
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageMTProvince(InputMTProvince input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTProvince objProvince = new cls_ctMTProvince();
                cls_MTProvince model = new cls_MTProvince();

                model.province_id = input.province_id;
                model.province_code = input.province_code;
                model.province_name_th = input.province_name_th;
                model.province_name_en = input.province_name_en;                
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objProvince.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objProvince.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTProvince(InputMTProvince input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTProvince objProvince = new cls_ctMTProvince();

                bool blnResult = objProvince.delete(input.province_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objProvince.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region TRCombank
        public string getTRCombankList(string com)
        {
            JObject output = new JObject();

            cls_ctTRCombank objBank = new cls_ctTRCombank();
            List<cls_TRCombank> listBank = objBank.getDataByFillter(com);
            JArray array = new JArray();

            if (listBank.Count > 0)
            {
                int index = 1;

                foreach (cls_TRCombank model in listBank)
                {
                    JObject json = new JObject();


            

                    json.Add("company_code", model.combank_bankaccount);

                    json.Add("combank_id", model.combank_id);
                    json.Add("combank_bankcode", model.combank_bankcode);
                    json.Add("combank_bankaccount", model.combank_bankaccount);
                    
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("change", false);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageTRCombankList(InputTRList input)
        {
            JObject output = new JObject();

            try
            {
                string company_code = input.company_code;
               
                //-- Transaction
                string bank_data = input.transaction_data;
                bool blnResult = true;
                string strMessage = "";

                try
                {
                    JObject jsonObject = new JObject();
                    var jsonArray = JsonConvert.DeserializeObject<List<cls_TRCombank>>(bank_data);
                    cls_ctTRCombank objBank = new cls_ctTRCombank();

                    //objBank.clear

                    foreach (cls_TRCombank item in jsonArray)
                    {                        

                        item.combank_bankaccount = company_code;


                        item.modified_by = input.modified_by;
                        blnResult = objBank.insert(item);

                        if (!blnResult)
                        {
                            strMessage = objBank.getMessage();
                        }
                    }
                }
                catch (Exception ex)
                {
                    blnResult = false;
                }

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = strMessage;
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTRCombank(InputTRCombank input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRCombank objBank = new cls_ctTRCombank();

                bool blnResult = objBank.delete(input.combank_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objBank.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region SYS Menu
        public string getSYSMainmenuList()
        {
            JObject output = new JObject();

            cls_ctSYSMainmenu objMenu = new cls_ctSYSMainmenu();
            List<cls_SYSMainmenu> listMenu = objMenu.getData();
            JArray array = new JArray();

            if (listMenu.Count > 0)
            {
                int index = 1;
                foreach (cls_SYSMainmenu model in listMenu)
                {
                    JObject json = new JObject();
                    json.Add("mainmenu_code", model.mainmenu_code);
                    json.Add("mainmenu_detail_th", model.mainmenu_detail_th);
                    json.Add("mainmenu_detail_en", model.mainmenu_detail_en);
                    json.Add("mainmenu_order", model.mainmenu_order);
                    json.Add("index", index);
                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }

        public string getSYSSubmenuList()
        {
            JObject output = new JObject();

            cls_ctSYSSubmenu objMenu = new cls_ctSYSSubmenu();
            List<cls_SYSSubmenu> listMenu = objMenu.getData();
            JArray array = new JArray();

            if (listMenu.Count > 0)
            {
                int index = 1;
                foreach (cls_SYSSubmenu model in listMenu)
                {
                    JObject json = new JObject();
                    json.Add("mainmenu_code", model.mainmenu_code);
                    json.Add("submenu_code", model.submenu_code);
                    json.Add("submenu_detail_th", model.submenu_detail_th);
                    json.Add("submenu_detail_en", model.submenu_detail_en);
                    json.Add("submenu_order", model.submenu_order);
                    json.Add("index", index);
                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }

        public string getSYSItemmenuList()
        {
            JObject output = new JObject();

            cls_ctSYSItemmenu objMenu = new cls_ctSYSItemmenu();
            List<cls_SYSItemmenu> listMenu = objMenu.getData();
            JArray array = new JArray();

            if (listMenu.Count > 0)
            {
                int index = 1;
                foreach (cls_SYSItemmenu model in listMenu)
                {
                    JObject json = new JObject();
                    json.Add("submenu_code", model.submenu_code);
                    json.Add("itemmenu_code", model.itemmenu_code);
                    json.Add("itemmenu_detail_th", model.itemmenu_detail_th);
                    json.Add("itemmenu_detail_en", model.itemmenu_detail_en);
                    json.Add("itemmenu_order", model.itemmenu_order);
                    json.Add("index", index);
                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        #endregion

        #region SYSAccount
        public string getSYSAccountList()
        {
            return getSYSAccountList_JSON("");
        }

        public string getSYSAccountUserList(string usr)
        {
            return getSYSAccountList_JSON(usr);
        }

        private string getSYSAccountList_JSON(string username)
        {
            JObject output = new JObject();

            cls_ctSYSAccount objAccount = new cls_ctSYSAccount();

            List<cls_SYSAccount> listAccount = new List<cls_SYSAccount>();

            if (username.Equals(""))
                listAccount = objAccount.getData();
            else
                listAccount = objAccount.getDataByUsername(username);

            JArray array = new JArray();

            if (listAccount.Count > 0)
            {

                int index = 1;

                foreach (cls_SYSAccount model in listAccount)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("account_id", model.account_id);

                    json.Add("account_usr", model.account_usr);
                    json.Add("account_pwd", model.account_pwd);
                    json.Add("account_detail", model.account_detail);

                    json.Add("account_email", model.account_email);
                    json.Add("account_emailalert", model.account_emailalert);
                    json.Add("account_line", model.account_line);
                    json.Add("account_linealert", model.account_linealert);

                    json.Add("account_lock", model.account_lock);
                    json.Add("account_faillogin", model.account_faillogin);
                    json.Add("account_lastlogin", model.account_lastlogin);
                    
                    json.Add("account_monthly", model.account_monthly);
                    json.Add("account_daily", model.account_daily);

                    json.Add("polmenu_code", model.polmenu_code);

                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }

        public string doManageSYSAccount(InputSYSAccount input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctSYSAccount objAccount = new cls_ctSYSAccount();
                cls_SYSAccount model = new cls_SYSAccount();

                model.company_code = input.company_code;
                model.account_id = input.account_id;

                model.account_usr = input.account_usr;
                model.account_pwd = input.account_pwd;
                model.account_detail = input.account_detail;

                model.account_email = input.account_email;
                model.account_emailalert = input.account_emailalert;
                model.account_line = input.account_line;
                model.account_linealert = input.account_linealert;

                model.account_lock = false;
                
                model.account_monthly = input.account_monthly;
                model.account_daily = input.account_daily;

                model.polmenu_code = input.polmenu_code;
                
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objAccount.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objAccount.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteSYSAccount(InputSYSAccount input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctSYSAccount objAccount = new cls_ctSYSAccount();

                bool blnResult = objAccount.delete(input.account_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objAccount.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region SYSAccessmenu
        public string getSYSAccessmenuList(string com, string polcode)
        {
            JObject output = new JObject();

            cls_ctSYSAccessmenu objAccess = new cls_ctSYSAccessmenu();
            List<cls_SYSAccessmenu> listAccess = objAccess.getData(com, polcode);
            JArray array = new JArray();

            if (listAccess.Count > 0)
            {

                int index = 1;

                foreach (cls_SYSAccessmenu model in listAccess)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("polmenu_code", model.polmenu_code);
                    json.Add("accessmenu_module", model.accessmenu_module);
                    json.Add("accessmenu_type", model.accessmenu_type);
                    json.Add("accessmenu_code", model.accessmenu_code);
                    
                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageSYSAccessmenuList(InputTRList input)
        {
            JObject output = new JObject();

            try
            {
                string company_code = input.company_code;
                string pol_code = input.worker_code;

                //-- Transaction
                string access_data = input.transaction_data;
                bool blnResult = true;
                string strMessage = "";

                try
                {
                    JObject jsonObject = new JObject();
                    var jsonArray = JsonConvert.DeserializeObject<List<cls_SYSAccessmenu>>(access_data);                    

                    List<cls_SYSAccessmenu> listAccess = new List<cls_SYSAccessmenu>();

                    foreach (cls_SYSAccessmenu item in jsonArray)
                    {                      
                        listAccess.Add(item);
                    }

                    if (listAccess.Count > 0)
                    {
                        cls_ctSYSAccessmenu objAccess = new cls_ctSYSAccessmenu();
                        blnResult = objAccess.insert(company_code, pol_code, listAccess);
                    }

                }
                catch
                {
                    blnResult = false;
                }

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = strMessage;
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);
        }
        public string doDeleteSYSAccessmenu(InputSYSAccessmenu input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctSYSAccessmenu objAccess = new cls_ctSYSAccessmenu();
                cls_SYSAccessmenu model = new cls_SYSAccessmenu();

                bool blnResult = objAccess.clear(input.company_code, input.polmenu_code);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objAccess.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region SYSAccessposition
        public string getSYSAccesspositionList(string com, string username)
        {
            JObject output = new JObject();

            cls_ctSYSAccessposition objAccess = new cls_ctSYSAccessposition();
            List<cls_SYSAccessposition> listAccess = objAccess.getData(com, username);
            JArray array = new JArray();

            if (listAccess.Count > 0)
            {

                int index = 1;

                foreach (cls_SYSAccessposition model in listAccess)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("account_usr", model.account_usr);
                    json.Add("accessposition_position", model.accessposition_position);
                   
                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageSYSAccesspositionList(InputTRList input)
        {
            JObject output = new JObject();

            try
            {
                string company_code = input.company_code;
                string worker_code = input.worker_code;

                //-- Transaction
                string access_data = input.transaction_data;
                bool blnResult = true;
                string strMessage = "";

                try
                {
                    JObject jsonObject = new JObject();
                    var jsonArray = JsonConvert.DeserializeObject<List<cls_SYSAccessposition>>(access_data);

                    List<cls_SYSAccessposition> listAccess = new List<cls_SYSAccessposition>();

                    foreach (cls_SYSAccessposition item in jsonArray)
                    {                        
                        listAccess.Add(item);
                    }

                    if (listAccess.Count > 0)
                    {
                        cls_ctSYSAccessposition objAccess = new cls_ctSYSAccessposition();
                        blnResult = objAccess.insert(company_code, worker_code, listAccess);
                    }

                }
                catch
                {
                    blnResult = false;
                }

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = strMessage;
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);
        }
        public string doDeleteSYSAccessposition(InputSYSAccessposition input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctSYSAccessposition objAccess = new cls_ctSYSAccessposition();
                bool blnResult = objAccess.clear(input.company_code, input.account_usr);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objAccess.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region SYSAccessdep
        public string getSYSAccessdepList(string com, string username)
        {
            JObject output = new JObject();

            cls_ctSYSAccessdep objAccess = new cls_ctSYSAccessdep();
            List<cls_SYSAccessdep> listAccess = objAccess.getData(com, username);
            JArray array = new JArray();

            if (listAccess.Count > 0)
            {

                int index = 1;

                foreach (cls_SYSAccessdep model in listAccess)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("account_usr", model.account_usr);
                    json.Add("accessdep_level", model.accessdep_level);
                    json.Add("accessdep_dep", model.accessdep_dep);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageSYSAccessdepList(InputTRList input)
        {
            JObject output = new JObject();

            try
            {
                string company_code = input.company_code;
                string worker_code = input.worker_code;

                //-- Transaction
                string access_data = input.transaction_data;
                bool blnResult = true;
                string strMessage = "";

                try
                {
                    JObject jsonObject = new JObject();
                    var jsonArray = JsonConvert.DeserializeObject<List<cls_SYSAccessdep>>(access_data);

                    List<cls_SYSAccessdep> listAccess = new List<cls_SYSAccessdep>();

                    foreach (cls_SYSAccessdep item in jsonArray)
                    {                        
                        listAccess.Add(item);
                    }

                    if (listAccess.Count > 0)
                    {
                        cls_ctSYSAccessdep objAccess = new cls_ctSYSAccessdep();
                        blnResult = objAccess.insert(company_code, worker_code, listAccess);
                    }

                }
                catch
                {
                    blnResult = false;
                }

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = strMessage;
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);
        }
        public string doDeleteSYSAccessdep(InputSYSAccessdep input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctSYSAccessdep objAccess = new cls_ctSYSAccessdep();
                
                bool blnResult = objAccess.clear(input.company_code, input.account_usr);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objAccess.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region SYSPolmenu
        public string getSYSPolmenuList(string com, string polcode)
        {
            JObject output = new JObject();

            cls_ctSYSPolmenu objAccess = new cls_ctSYSPolmenu();
            List<cls_SYSPolmenu> listAccess = objAccess.getDataByFillter(com, "", polcode);
            JArray array = new JArray();

            if (listAccess.Count > 0)
            {

                int index = 1;

                foreach (cls_SYSPolmenu model in listAccess)
                {
                    JObject json = new JObject();

                    json.Add("polmenu_id", model.polmenu_id);
                    json.Add("polmenu_code", model.polmenu_code);
                    json.Add("polmenu_name_th", model.polmenu_name_th);
                    json.Add("polmenu_name_en", model.polmenu_name_en);
                    json.Add("company_code", model.company_code);
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageSYSPolmenu(InputSYSPolmenu input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctSYSPolmenu objPol = new cls_ctSYSPolmenu();
                cls_SYSPolmenu model = new cls_SYSPolmenu();

                model.company_code = input.company_code;

                model.polmenu_id = input.polmenu_id;
                model.polmenu_code = input.polmenu_code;
                model.polmenu_name_th = input.polmenu_name_th;
                model.polmenu_name_en = input.polmenu_name_en;
               
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objPol.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objPol.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteSYSPolmenu(InputSYSPolmenu input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctSYSPolmenu objPol = new cls_ctSYSPolmenu();
                
                bool blnResult = objPol.delete(input.polmenu_code);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objPol.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region SYSAccessdata
        public string getSYSAccessdataList(string com, string polcode)
        {
            JObject output = new JObject();

            cls_ctSYSAccessdata objAccess = new cls_ctSYSAccessdata();
            List<cls_SYSAccessdata> listAccess = objAccess.getData(com, polcode);
            JArray array = new JArray();

            if (listAccess.Count > 0)
            {

                int index = 1;

                foreach (cls_SYSAccessdata model in listAccess)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("polmenu_code", model.polmenu_code);
                    json.Add("accessdata_module", model.accessdata_module);
                    json.Add("accessdata_new", model.accessdata_new);
                    json.Add("accessdata_edit", model.accessdata_edit);
                    json.Add("accessdata_delete", model.accessdata_delete);
                    json.Add("accessdata_salary", model.accessdata_salary);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageSYSAccessdataList(InputTRList input)
        {
            JObject output = new JObject();

            try
            {
                string company_code = input.company_code;
                string pol_code = input.worker_code;

                //-- Transaction
                string access_data = input.transaction_data;
                bool blnResult = true;
                string strMessage = "";

                try
                {
                    JObject jsonObject = new JObject();
                    var jsonArray = JsonConvert.DeserializeObject<List<cls_SYSAccessdata>>(access_data);

                    List<cls_SYSAccessdata> listAccess = new List<cls_SYSAccessdata>();

                    foreach (cls_SYSAccessdata item in jsonArray)
                    {
                        listAccess.Add(item);
                    }

                    if (listAccess.Count > 0)
                    {
                        cls_ctSYSAccessdata objAccess = new cls_ctSYSAccessdata();
                        blnResult = objAccess.insert(company_code, pol_code, listAccess);
                    }

                }
                catch
                {
                    blnResult = false;
                }

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = strMessage;
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);
        }
        public string doDeleteSYSAccessdata(InputSYSAccessdata input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctSYSAccessdata objAccess = new cls_ctSYSAccessdata();
                
                bool blnResult = objAccess.clear(input.company_code, input.polmenu_code);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objAccess.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion


        #region Reportjob
        public string getSYSReportjobList(string com)
        {
            JObject output = new JObject();

            cls_ctSYSReportjob objReportjob = new cls_ctSYSReportjob();
            List<cls_SYSReportjob> listReportjob = objReportjob.getDataByFillter(com, "", "", "");
            JArray array = new JArray();

            if (listReportjob.Count > 0)
            {

                int index = 1;

                foreach (cls_SYSReportjob model in listReportjob)
                {
                    JObject json = new JObject();

                    json.Add("reportjob_id", model.reportjob_id);
                    json.Add("reportjob_ref", model.reportjob_ref);
                    json.Add("reportjob_type", model.reportjob_type);
                    json.Add("reportjob_status", model.reportjob_status);
                    json.Add("reportjob_fromdate", model.reportjob_fromdate);
                    json.Add("reportjob_todate", model.reportjob_todate);
                    json.Add("reportjob_paydate", model.reportjob_paydate);

                    json.Add("reportjob_language", model.reportjob_language);

                    json.Add("created_by", model.created_by);
                    json.Add("created_date", model.created_date);
                   
                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string getSYSReportjobWhoseList(string id)
        {
            JObject output = new JObject();

            cls_ctSYSReportjob objReportjob = new cls_ctSYSReportjob();
            List<cls_SYSReportjobwhose> listReportjob = objReportjob.getDataWhose(id);
            JArray array = new JArray();

            if (listReportjob.Count > 0)
            {

                int index = 1;

                foreach (cls_SYSReportjobwhose model in listReportjob)
                {
                    JObject json = new JObject();

                    json.Add("reportjob_id", model.reportjob_id);
                    json.Add("worker_code", model.worker_code);
                   
                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageSYSReportjob(InputSYSReportjob input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctSYSReportjob objReportjob = new cls_ctSYSReportjob();
                cls_SYSReportjob model = new cls_SYSReportjob();

                model.reportjob_id = input.reportjob_id;
                model.reportjob_ref = input.reportjob_ref;

                model.reportjob_status = input.reportjob_status;
                model.reportjob_language = input.reportjob_language;
                model.reportjob_type = input.reportjob_type;

                model.reportjob_fromdate = Convert.ToDateTime(input.reportjob_fromdate);
                model.reportjob_todate = Convert.ToDateTime(input.reportjob_todate);
                model.reportjob_paydate = Convert.ToDateTime(input.reportjob_paydate);

                model.company_code = input.company_code;
                model.created_by = input.created_by;

                JObject jsonObject = new JObject();
                var jsonArray = JsonConvert.DeserializeObject<List<cls_SYSReportjobwhose>>(input.reportjob_whose);
                cls_ctTRShiftflexible objShift = new cls_ctTRShiftflexible();

                List<cls_SYSReportjobwhose> listWhose = new List<cls_SYSReportjobwhose>();

                foreach (cls_SYSReportjobwhose item in jsonArray)
                {                    
                    listWhose.Add(item);
                }

                string strResult = objReportjob.insert(model, listWhose);

                if (!strResult.Equals(""))
                {
                    
                    output["result"] = "1";
                    output["result_text"] = strResult;
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objReportjob.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteSYSReportjob(InputSYSReportjob input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctSYSReportjob objReportjob = new cls_ctSYSReportjob();

                bool blnResult = objReportjob.delete(input.reportjob_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objReportjob.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        
        #endregion

        #endregion
               
        #region Employee

        #region MTInitial
        public string getMTInitialList()
        {
            JObject output = new JObject();

            cls_ctMTInitial objInitial = new cls_ctMTInitial();
            List<cls_MTInitial> listInitial = objInitial.getDataByFillter("", "");
            JArray array = new JArray();

            if (listInitial.Count > 0)
            {

                int index = 1;

                foreach (cls_MTInitial model in listInitial)
                {
                    JObject json = new JObject();

                    json.Add("initial_id", model.initial_id);
                    json.Add("initial_code", model.initial_code);
                    json.Add("initial_name_th", model.initial_name_th);
                    json.Add("initial_name_en", model.initial_name_en);
                   
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageMTInitial(InputMTInitial input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTInitial objInitial = new cls_ctMTInitial();
                cls_MTInitial model = new cls_MTInitial();

                model.initial_id = input.initial_id;
                model.initial_code = input.initial_code;

                model.initial_name_th = input.initial_name_th;
                model.initial_name_en = input.initial_name_en;
                
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objInitial.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objInitial.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTInitial(InputMTInitial input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTInitial objInitial = new cls_ctMTInitial();

                bool blnResult = objInitial.delete(input.initial_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objInitial.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region MTWorker
        public string getMTWorkerList(string com, string id, string code, string fname, string lname, string level, string depcod)
        {
            JObject output = new JObject();

            cls_ctMTWorker objWorker = new cls_ctMTWorker();
            List<cls_MTWorker> listWorker = objWorker.getDataByFillter(com, id, code, fname, lname, "", "", "", level, depcod, "", "", false, "", DateTime.Now.Date);

            JArray array = new JArray();

            if (listWorker.Count > 0)
            {
                

                int index = 1;

                foreach (cls_MTWorker model in listWorker)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("worker_id", model.worker_id);
                    json.Add("worker_code", model.worker_code);
                    json.Add("worker_card", model.worker_card);
                    json.Add("worker_initial", model.worker_initial);

                    json.Add("worker_fname_th", model.worker_fname_th);
                    json.Add("worker_lname_th", model.worker_lname_th);
                    json.Add("worker_fname_en", model.worker_fname_en);
                    json.Add("worker_lname_en", model.worker_lname_en);

                    json.Add("worker_emptype", model.worker_emptype);
                    json.Add("worker_gender", model.worker_gender);
                    json.Add("worker_birthdate", model.worker_birthdate);
                    json.Add("worker_hiredate", model.worker_hiredate);

                    json.Add("worker_resigndate", model.worker_resigndate);
                    json.Add("worker_resignstatus", model.worker_resignstatus);
                    json.Add("worker_resignreason", model.worker_resignreason);

                    json.Add("worker_probationdate", model.worker_probationdate);
                    json.Add("worker_probationenddate", model.worker_probationenddate);

                    json.Add("worker_taxmethod", model.worker_taxmethod);
                                        
                    json.Add("hrs_perday", model.hrs_perday);
                  
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);

                    json.Add("self_admin", model.self_admin);

                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }

        public string getMTWorkerFillterList(FillterEmp input)
        {
            JObject output = new JObject();

            cls_ctMTWorker objWorker = new cls_ctMTWorker();

            DateTime date_fill = Convert.ToDateTime(input.date_fill);
            
            List<cls_MTWorker> listWorker = objWorker.getDataByFillter(input.company_code
                , input.worker_id
                , input.worker_code
                , input.worker_fname_th
                , input.worker_lname_th
                , input.worker_fname_en
                , input.worker_lname_en
                , input.worker_emptype
                , input.level_code
                , input.dep_code
                , input.position_code
                , input.group_code
                , input.include_resign

                , input.location_code
                , date_fill
                );

            JArray array = new JArray();

            if (listWorker.Count > 0)
            {


                int index = 1;

                foreach (cls_MTWorker model in listWorker)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("worker_id", model.worker_id);
                    json.Add("worker_code", model.worker_code);
                    json.Add("worker_card", model.worker_card);
                    json.Add("worker_initial", model.worker_initial);

                    json.Add("worker_fname_th", model.worker_fname_th);
                    json.Add("worker_lname_th", model.worker_lname_th);
                    json.Add("worker_fname_en", model.worker_fname_en);
                    json.Add("worker_lname_en", model.worker_lname_en);

                    json.Add("worker_emptype", model.worker_emptype);
                    json.Add("worker_gender", model.worker_gender);
                    json.Add("worker_birthdate", model.worker_birthdate);
                    json.Add("worker_hiredate", model.worker_hiredate);

                    json.Add("worker_resigndate", model.worker_resigndate);
                    json.Add("worker_resignstatus", model.worker_resignstatus);
                    json.Add("worker_resignreason", model.worker_resignreason);

                    json.Add("worker_probationdate", model.worker_probationdate);
                    json.Add("worker_probationenddate", model.worker_probationenddate);

                    json.Add("hrs_perday", model.hrs_perday);

                    json.Add("worker_taxmethod", model.worker_taxmethod);

                    json.Add("self_admin", model.self_admin);

                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageMTWorker(InputMTWorker input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTWorker objWorker = new cls_ctMTWorker();
                cls_MTWorker model = new cls_MTWorker();

                string strWorkerCode = input.worker_code;
                string strComCode = input.company_code;

                model.company_code = strComCode;
                model.worker_id = input.worker_id;
                model.worker_code = strWorkerCode;
                model.worker_card = input.worker_card;
                model.worker_initial = input.worker_initial;
                model.worker_fname_th = input.worker_fname_th;
                model.worker_lname_th = input.worker_lname_th;
                model.worker_fname_en = input.worker_fname_en;
                model.worker_lname_en = input.worker_lname_en;
                model.worker_emptype = input.worker_emptype;
                model.worker_gender = input.worker_gender;
                model.worker_birthdate = Convert.ToDateTime(input.worker_birthdate);
                model.worker_hiredate = Convert.ToDateTime(input.worker_hiredate);

                model.worker_resigndate = Convert.ToDateTime(input.worker_resigndate);
                model.worker_resignstatus = input.worker_resignstatus;
                model.worker_resignreason = input.worker_resignreason;

                model.worker_probationdate = Convert.ToDateTime(input.worker_probationdate);
                model.worker_probationenddate = Convert.ToDateTime(input.worker_probationenddate);

                model.hrs_perday = input.hrs_perday;

                model.worker_taxmethod = input.worker_taxmethod;

                model.worker_pwd = "+PH1MsvnDonmqUuzB4TZ8g==";

                model.self_admin = input.self_admin;
                
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                string strResult = objWorker.insert(model);

                if (!strResult.Equals(""))
                {
                    

                    //-- Transaction

                    //-- Empcards
                    string card_data = input.card_data;
                    try
                    {
                        JObject jsonObject = new JObject();
                        var jsonArray = JsonConvert.DeserializeObject<List<cls_TREmpcard>>(card_data);      
                        cls_ctTREmpcard objCard = new cls_ctTREmpcard();

                        bool blnClear = objCard.clear(strComCode, strWorkerCode);
                        if (blnClear)
                        {
                            foreach (cls_TREmpcard item in jsonArray)
                            {
                                item.worker_code = strWorkerCode;
                                item.company_code = strComCode;
                                item.modified_by = input.modified_by;
                                objCard.insert(item);
                            }
                        }
                    }
                    catch { }

                    //-- Empreduces
                    string reduce_data = input.reduce_data;
                    try
                    {
                        JObject jsonObject = new JObject();
                        var jsonArray = JsonConvert.DeserializeObject<List<cls_TREmpreduce>>(reduce_data);                        
                        cls_ctTREmpreduce objReduce = new cls_ctTREmpreduce();

                        bool blnClear = objReduce.clear(strComCode, strWorkerCode);
                        if (blnClear)
                        {
                            foreach (cls_TREmpreduce item in jsonArray)
                            {
                                item.worker_code = strWorkerCode;
                                item.company_code = strComCode;
                                item.modified_by = input.modified_by;
                                objReduce.insert(item);
                            }
                        }
                    }
                    catch { }

                    //-- Empfamilys
                    string family_data = input.family_data;
                    try
                    {
                        JObject jsonObject = new JObject();
                        var jsonArray = JsonConvert.DeserializeObject<List<cls_TREmpfamily>>(family_data);                        
                        cls_ctTREmpfamily objFamily = new cls_ctTREmpfamily();
                        bool blnClear = objFamily.clear(strComCode, strWorkerCode);
                        if (blnClear)
                        {
                            foreach (cls_TREmpfamily item in jsonArray)
                            {
                                item.worker_code = strWorkerCode;
                                item.company_code = strComCode;
                                item.modified_by = input.modified_by;
                                objFamily.insert(item);
                            }
                        }
                    }
                    catch { }

                    ////-- Empdeps
                    //string dep_data = input.dep_data;
                    //try
                    //{
                    //    JObject jsonObject = new JObject();
                    //    var jsonArray = JsonConvert.DeserializeObject<List<cls_TREmpdep>>(dep_data);
                    //    cls_ctTREmpdep objDep = new cls_ctTREmpdep();
                    //    bool blnClear = objDep.clear(strComCode, strWorkerCode);
                    //    if (blnClear)
                    //    {
                    //        foreach (cls_TREmpdep item in jsonArray)
                    //        {
                    //            item.worker_code = strWorkerCode;
                    //            item.company_code = strComCode;
                    //            item.modified_by = input.modified_by;
                    //            objDep.insert(item);
                    //        }
                    //    }
                    //}
                    //catch { }



                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objWorker.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTWorker(InputMTWorker input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTWorker objWorker = new cls_ctMTWorker();

                bool blnResult = objWorker.delete(input.worker_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objWorker.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region TRComcard
        public string getTRComcardList(string com, string branch, string type)
        {
            JObject output = new JObject();

            cls_ctTRComcard objCom = new cls_ctTRComcard();
            List<cls_TRComcard> listCom = objCom.getDataByFillter(com, type, "", "", branch);
            JArray array = new JArray();

            if (listCom.Count > 0)
            {


                int index = 1;

                foreach (cls_TRComcard model in listCom)
                {
                    JObject json = new JObject();

                    json.Add("comcard_id", model.comcard_id);
                    json.Add("comcard_code", model.comcard_code);
                   
                    json.Add("comcard_issue", model.comcard_issue);
                    json.Add("comcard_expire", model.comcard_expire);

                    json.Add("company_code", model.company_code);
                    json.Add("combranch_code", model.combranch_code);
                    json.Add("change", false);

                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageTRComcard(InputTRComcard input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRComcard objCom = new cls_ctTRComcard();
                cls_TRComcard model = new cls_TRComcard();

                model.comcard_id = input.comcard_id;
                model.comcard_code = input.comcard_code;
                model.comcard_issue = Convert.ToDateTime(input.comcard_issue);
                model.comcard_expire = Convert.ToDateTime(input.comcard_expire);
                model.company_code = input.company_code;
                model.combranch_code = input.combranch_code;

                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objCom.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objCom.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTRComcard(InputTRComcard input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRComcard objCom = new cls_ctTRComcard();

                bool blnResult = objCom.delete(input.comcard_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objCom.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }

        public string doManageTRComcardList(InputTRList input)
        {
            JObject output = new JObject();

            try
            {
                string company_code = input.company_code;
                string branch_code = input.worker_code;

                //-- Transaction
                string addr_data = input.transaction_data;
                bool blnResult = true;
                string strMessage = "";

                try
                {
                    JObject jsonObject = new JObject();
                    var jsonArray = JsonConvert.DeserializeObject<List<cls_TRComcard>>(addr_data);
                    cls_ctTRComcard objAddr = new cls_ctTRComcard();

                    foreach (cls_TRComcard item in jsonArray)
                    {
                        item.company_code = company_code;
                        item.combranch_code = branch_code;
                        item.modified_by = input.modified_by;
                        blnResult = objAddr.insert(item);

                        if (!blnResult)
                        {
                            strMessage = objAddr.getMessage();
                        }
                    }
                }
                catch
                {
                    blnResult = false;
                }

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = strMessage;
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region TREmpcard
        public string getTREmpcardList(string com, string emp)
        {
            JObject output = new JObject();

            cls_ctTREmpcard objCom = new cls_ctTREmpcard();
            List<cls_TREmpcard> listCard = objCom.getDataByFillter(com, "", "", emp);
            JArray array = new JArray();

            if (listCard.Count > 0)
            {


                int index = 1;

                foreach (cls_TREmpcard model in listCard)
                {
                    JObject json = new JObject();

                    json.Add("empcard_id", model.empcard_id);
                    json.Add("empcard_code", model.empcard_code);
                    json.Add("card_type", model.card_type);
                    json.Add("empcard_issue", model.empcard_issue);
                    json.Add("empcard_expire", model.empcard_expire);

                    json.Add("company_code", model.company_code);


                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);
                    json.Add("change", false);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageTREmpcard(InputTREmpcard input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTREmpcard objCard = new cls_ctTREmpcard();
                cls_TREmpcard model = new cls_TREmpcard();

                model.empcard_id = input.empcard_id;
                model.empcard_code = input.empcard_code;
                model.card_type = input.card_type;
                model.empcard_issue = Convert.ToDateTime(input.empcard_issue);
                model.empcard_expire = Convert.ToDateTime(input.empcard_expire);
                model.company_code = input.company_code;
                model.worker_code = input.worker_code;

                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objCard.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objCard.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTREmpcard(InputTREmpcard input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTREmpcard objCard = new cls_ctTREmpcard();

                bool blnResult = objCard.delete(input.empcard_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objCard.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region TREmpreduce
        public string getTREmpreduceList(string com, string emp)
        {
            JObject output = new JObject();

            cls_ctTREmpreduce objReduce = new cls_ctTREmpreduce();
            List<cls_TREmpreduce> listReduce = objReduce.getDataByFillter(com, emp);
            JArray array = new JArray();

            if (listReduce.Count > 0)
            {


                int index = 1;

                foreach (cls_TREmpreduce model in listReduce)
                {
                    JObject json = new JObject();

                    json.Add("empreduce_id", model.empreduce_id);
                    json.Add("company_code", model.company_code);
                    json.Add("worker_code", model.worker_code);
                    json.Add("reduce_type", model.reduce_type);
                    json.Add("empreduce_amount", model.empreduce_amount);
                    
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);
                    json.Add("change", false);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageTREmpreduce(InputTREmpreduce input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTREmpreduce objCard = new cls_ctTREmpreduce();
                cls_TREmpreduce model = new cls_TREmpreduce();

                model.empreduce_id = input.empreduce_id;
                model.company_code = input.company_code;
                model.worker_code = input.worker_code;
                model.reduce_type = input.reduce_type;
                model.empreduce_amount = input.empreduce_amount;
                
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objCard.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objCard.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTREmpreduce(InputTREmpreduce input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTREmpcard objReduce = new cls_ctTREmpcard();

                bool blnResult = objReduce.delete(input.empreduce_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objReduce.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region TREmpfamily
        public string getTREmpfamilyList(string com, string emp)
        {
            JObject output = new JObject();

            cls_ctTREmpfamily objEmpfamily = new cls_ctTREmpfamily();
            List<cls_TREmpfamily> listEmpfamily = objEmpfamily.getDataByFillter(com, emp);
            JArray array = new JArray();

            if (listEmpfamily.Count > 0)
            {


                int index = 1;

                foreach (cls_TREmpfamily model in listEmpfamily)
                {
                    JObject json = new JObject();

                    json.Add("empfamily_id", model.empfamily_id);
                    json.Add("empfamily_code", model.empfamily_code);
                    json.Add("family_type", model.family_type);
                    json.Add("empfamily_fname_th", model.empfamily_fname_th);
                    json.Add("empfamily_lname_th", model.empfamily_lname_th);
                    json.Add("empfamily_fname_en", model.empfamily_fname_en);
                    json.Add("empfamily_lname_en", model.empfamily_lname_en);
                    json.Add("empfamily_birthdate", model.empfamily_birthdate);
                   
                    json.Add("company_code", model.company_code);
                    json.Add("worker_code", model.worker_code);

                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);
                    json.Add("change", false);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageTREmpfamily(InputTREmpfamily input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTREmpfamily objEmpfamily = new cls_ctTREmpfamily();
                cls_TREmpfamily model = new cls_TREmpfamily();

                model.empfamily_id = input.empfamily_id;
                model.empfamily_code = input.empfamily_code;
                model.family_type = input.family_type;
                model.empfamily_fname_th = input.empfamily_fname_th;
                model.empfamily_lname_th = input.empfamily_lname_th;
                model.empfamily_fname_en = input.empfamily_fname_en;
                model.empfamily_lname_en = input.empfamily_lname_en;
                model.empfamily_birthdate = Convert.ToDateTime(input.empfamily_birthdate);


                model.company_code = input.company_code;
                model.worker_code = input.worker_code;

                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objEmpfamily.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objEmpfamily.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTREmpfamily(InputTREmpfamily input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTREmpfamily objEmpfamily = new cls_ctTREmpfamily();

                bool blnResult = objEmpfamily.delete(input.empfamily_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objEmpfamily.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region TREmpsalary
        public string getTREmpsalaryList(string com, string emp)
        {
            JObject output = new JObject();

            cls_ctTREmpsalary objSalary = new cls_ctTREmpsalary();
            List<cls_TREmpsalary> listSalary = objSalary.getDataByFillter(com, emp);
            JArray array = new JArray();

            if (listSalary.Count > 0)
            {
                int index = 1;

                foreach (cls_TREmpsalary model in listSalary)
                {
                    JObject json = new JObject();

                    json.Add("empsalary_id", model.empsalary_id);
                    json.Add("company_code", model.company_code);
                    json.Add("worker_code", model.worker_code);
                    json.Add("empsalary_amount", model.empsalary_amount);
                    json.Add("empsalary_date", model.empsalary_date);
                    json.Add("empsalary_reason", model.empsalary_reason);

                    json.Add("empsalary_incamount", model.empsalary_incamount);
                    json.Add("empsalary_incpercent", model.empsalary_incpercent);

                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("change", false);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageTREmpsalary(InputTREmpsalary input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTREmpsalary objSalary = new cls_ctTREmpsalary();
                cls_TREmpsalary model = new cls_TREmpsalary();

                model.empsalary_id = input.empsalary_id;
                model.company_code = input.company_code;
                model.worker_code = input.worker_code;
                model.empsalary_amount = input.empsalary_amount;
                model.empsalary_date = Convert.ToDateTime(input.empsalary_date);
                model.empsalary_reason = input.empsalary_reason;

                model.empsalary_incamount = input.empsalary_incamount;
                model.empsalary_incpercent = input.empsalary_incpercent;

                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objSalary.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objSalary.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doManageTREmpsalaryList(InputTRList input)
        {
            JObject output = new JObject();

            try
            {
                string company_code = input.company_code;
                string worker_code = input.worker_code;


                //-- Transaction
                string salary_data = input.transaction_data;
                bool blnResult = true;
                string strMessage = "";

                try
                {
                    JObject jsonObject = new JObject();
                    var jsonArray = JsonConvert.DeserializeObject<List<cls_TREmpsalary>>(salary_data);
                    cls_ctTREmpsalary objSalary = new cls_ctTREmpsalary();

                    blnResult = objSalary.clear(company_code, worker_code);

                    if (blnResult)
                    {
                        foreach (cls_TREmpsalary item in jsonArray)
                        {
                            item.worker_code = worker_code;
                            item.company_code = company_code;
                            item.modified_by = input.modified_by;
                            blnResult = objSalary.insert(item);

                            if (!blnResult)
                            {
                                strMessage = objSalary.getMessage();
                            }
                        }
                    }
                }
                catch {
                    blnResult = false;
                }
                
                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = strMessage;
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTREmpsalary(InputTREmpsalary input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTREmpsalary objSalary = new cls_ctTREmpsalary();

                bool blnResult = objSalary.delete(input.empsalary_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objSalary.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region TREmpbenefit
        public string getTREmpbenefitList(string com, string emp)
        {
            JObject output = new JObject();

            cls_ctTREmpbenefit objBenefit = new cls_ctTREmpbenefit();
            List<cls_TREmpbenefit> listBenefit = objBenefit.getDataByFillter(com, emp);
            JArray array = new JArray();

            if (listBenefit.Count > 0)
            {


                int index = 1;

                foreach (cls_TREmpbenefit model in listBenefit)
                {
                    JObject json = new JObject();

                    json.Add("empbenefit_id", model.empbenefit_id);
                    json.Add("company_code", model.company_code);
                    json.Add("worker_code", model.worker_code);
                    json.Add("item_code", model.item_code);
                    json.Add("empbenefit_amount", model.empbenefit_amount);
                    json.Add("empbenefit_startdate", model.empbenefit_startdate);
                    json.Add("empbenefit_enddate", model.empbenefit_enddate);
                    json.Add("empbenefit_reason", model.empbenefit_reason);
                    json.Add("empbenefit_note", model.empbenefit_note);

                    json.Add("empbenefit_paytype", model.empbenefit_paytype);
                    json.Add("empbenefit_break", model.empbenefit_break);
                    json.Add("empbenefit_breakreason", model.empbenefit_breakreason);

                    json.Add("empbenefit_conditionpay", model.empbenefit_conditionpay);
                    json.Add("empbenefit_payfirst", model.empbenefit_payfirst);

                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("change", false);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageTREmpbenefit(InputTREmpbenefit input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTREmpbenefit objBenefit = new cls_ctTREmpbenefit();
                cls_TREmpbenefit model = new cls_TREmpbenefit();

                model.empbenefit_id = input.empbenefit_id;
                model.company_code = input.company_code;
                model.worker_code = input.worker_code;
                model.item_code = input.item_code;
                model.empbenefit_amount = input.empbenefit_amount;
                model.empbenefit_startdate = Convert.ToDateTime(input.empbenefit_startdate);
                model.empbenefit_enddate = Convert.ToDateTime(input.empbenefit_enddate);
                model.empbenefit_reason = input.empbenefit_reason;
                model.empbenefit_note = input.empbenefit_note;

                model.empbenefit_paytype = input.empbenefit_paytype;
                model.empbenefit_break = input.empbenefit_break;
                model.empbenefit_breakreason = input.empbenefit_breakreason;

                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objBenefit.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objBenefit.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doManageTREmpbenefitList(InputTRList input)
        {
            JObject output = new JObject();

            try
            {
                string company_code = input.company_code;
                string worker_code = input.worker_code;


                //-- Transaction
                string benefit_data = input.transaction_data;
                bool blnResult = true;
                string strMessage = "";

                try
                {
                    JObject jsonObject = new JObject();
                    var jsonArray = JsonConvert.DeserializeObject<List<cls_TREmpbenefit>>(benefit_data);
                    cls_ctTREmpbenefit objBenefit = new cls_ctTREmpbenefit();


                    blnResult = objBenefit.clear(company_code, worker_code);

                    if (blnResult)
                    {
                        foreach (cls_TREmpbenefit item in jsonArray)
                        {

                            if (item.empbenefit_reason.ToString().Equals("undefined"))
                                item.empbenefit_reason = "";

                            if (item.empbenefit_breakreason.ToString().Equals("undefined"))
                                item.empbenefit_breakreason = "";

                            if (item.empbenefit_conditionpay.ToString().Equals("undefined"))
                                item.empbenefit_conditionpay = "F";

                            if (item.empbenefit_payfirst.ToString().Equals("undefined"))
                                item.empbenefit_payfirst = "Y";


                            item.worker_code = worker_code;
                            item.company_code = company_code;
                            item.modified_by = input.modified_by;
                            blnResult = objBenefit.insert(item);

                            if (!blnResult)
                            {
                                strMessage = objBenefit.getMessage();
                            }
                        }
                    }
                }
                catch
                {
                    blnResult = false;
                }

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = strMessage;
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTREmpbenefit(InputTREmpbenefit input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTREmpbenefit objBenefit = new cls_ctTREmpbenefit();

                bool blnResult = objBenefit.delete(input.empbenefit_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objBenefit.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region TREmpdep
        public string getTREmpdepList(string com, string emp)
        {
            JObject output = new JObject();

            cls_ctTREmpdep objDep = new cls_ctTREmpdep();
            List<cls_TREmpdep> listDep = objDep.getDataByFillter(com, emp);
            JArray array = new JArray();

            if (listDep.Count > 0)
            {
                int index = 1;

                foreach (cls_TREmpdep model in listDep)
                {
                    JObject json = new JObject();

                    json.Add("empdep_id", model.empdep_id);
                    
                    json.Add("empdep_date", model.empdep_date);
                    json.Add("empdep_level01", model.empdep_level01);
                    json.Add("empdep_level02", model.empdep_level02);
                    json.Add("empdep_level03", model.empdep_level03);
                    json.Add("empdep_level04", model.empdep_level04);
                    json.Add("empdep_level05", model.empdep_level05);
                    json.Add("empdep_level06", model.empdep_level06);
                    json.Add("empdep_level07", model.empdep_level07);
                    json.Add("empdep_level08", model.empdep_level08);
                    json.Add("empdep_level09", model.empdep_level09);
                    json.Add("empdep_level10", model.empdep_level10);
                    json.Add("empdep_reason", model.empdep_reason);

                    json.Add("company_code", model.company_code);
                    json.Add("worker_code", model.worker_code);

                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("change", false);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }        
        public string doManageTREmpdepList(InputTRList input)
        {
            JObject output = new JObject();

            try
            {
                string company_code = input.company_code;
                string worker_code = input.worker_code;


                //-- Transaction
                string dep_data = input.transaction_data;
                bool blnResult = true;
                string strMessage = "";

                try
                {
                    JObject jsonObject = new JObject();
                    var jsonArray = JsonConvert.DeserializeObject<List<cls_TREmpdep>>(dep_data);
                    cls_ctTREmpdep objDep = new cls_ctTREmpdep();

                    blnResult = objDep.clear(company_code, worker_code);

                    if (blnResult)
                    {

                        foreach (cls_TREmpdep item in jsonArray)
                        {
                            item.worker_code = worker_code;
                            item.company_code = company_code;
                            item.modified_by = input.modified_by;
                            blnResult = objDep.insert(item);

                            if (!blnResult)
                            {
                                strMessage = objDep.getMessage();
                            }
                        }
                    }
                }
                catch
                {
                    blnResult = false;
                }

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = strMessage;
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTREmpdep(InputTREmpdep input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTREmpdep objDep = new cls_ctTREmpdep();

                bool blnResult = objDep.delete(input.empdep_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objDep.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region TREmpposition
        public string getTREmppositionList(string com, string emp)
        {
            JObject output = new JObject();

            cls_ctTREmpposition objPos = new cls_ctTREmpposition();
            List<cls_TREmpposition> listPos = objPos.getDataByFillter(com, emp);
            JArray array = new JArray();

            if (listPos.Count > 0)
            {
                int index = 1;

                foreach (cls_TREmpposition model in listPos)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("worker_code", model.worker_code);
                    json.Add("empposition_id", model.empposition_id);
                    json.Add("empposition_date", model.empposition_date);
                    json.Add("empposition_position", model.empposition_position);
                    json.Add("empposition_reason", model.empposition_reason);
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);
                    
                    json.Add("index", index);
                    json.Add("change", false);
                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageTREmppositionList(InputTRList input)
        {
            JObject output = new JObject();

            try
            {
                string company_code = input.company_code;
                string worker_code = input.worker_code;
                
                //-- Transaction
                string dep_data = input.transaction_data;
                bool blnResult = true;
                string strMessage = "";

                try
                {
                    JObject jsonObject = new JObject();
                    var jsonArray = JsonConvert.DeserializeObject<List<cls_TREmpposition>>(dep_data);
                    cls_ctTREmpposition objPos = new cls_ctTREmpposition();

                    blnResult = objPos.clear(company_code, worker_code);

                    if (blnResult)
                    {
                        foreach (cls_TREmpposition item in jsonArray)
                        {
                            item.worker_code = worker_code;
                            item.company_code = company_code;
                            item.modified_by = input.modified_by;
                            blnResult = objPos.insert(item);

                            if (!blnResult)
                            {
                                strMessage = objPos.getMessage();
                            }
                        }
                    }
                }
                catch
                {
                    blnResult = false;
                }

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = strMessage;
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTREmpposition(InputTREmpposition input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTREmpposition objPos = new cls_ctTREmpposition();

                bool blnResult = objPos.delete(input.empposition_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objPos.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region TREmpeducation
        public string getTREmpeducationList(string com, string emp)
        {
            JObject output = new JObject();

            cls_ctTREmpeducation objEdu = new cls_ctTREmpeducation();
            List<cls_TREmpeducation> listEdu = objEdu.getDataByFillter(com, emp);
            JArray array = new JArray();

            if (listEdu.Count > 0)
            {
                int index = 1;

                foreach (cls_TREmpeducation model in listEdu)
                {
                    JObject json = new JObject();

                    json.Add("empeducation_no", model.empeducation_no);
                    json.Add("empeducation_gpa", model.empeducation_gpa);
                    json.Add("empeducation_start", model.empeducation_start);
                    json.Add("empeducation_finish", model.empeducation_finish);
                    json.Add("institute_code", model.institute_code);
                    json.Add("faculty_code", model.faculty_code);
                    json.Add("major_code", model.major_code);
                    json.Add("qualification_code", model.qualification_code);
                   
                    json.Add("company_code", model.company_code);
                    json.Add("worker_code", model.worker_code);

                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);
                    
                    json.Add("index", index);
                    json.Add("change", false);
                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageTREmpeducationList(InputTRList input)
        {
            JObject output = new JObject();

            try
            {
                string company_code = input.company_code;
                string worker_code = input.worker_code;


                //-- Transaction
                string edu_data = input.transaction_data;
                bool blnResult = true;
                string strMessage = "";

                try
                {
                    JObject jsonObject = new JObject();
                    var jsonArray = JsonConvert.DeserializeObject<List<cls_TREmpeducation>>(edu_data);
                    cls_ctTREmpeducation objEdu = new cls_ctTREmpeducation();


                    blnResult = objEdu.clear(company_code, worker_code);

                    if (blnResult)
                    {
                        int i = 1;
                        foreach (cls_TREmpeducation item in jsonArray)
                        {
                            item.empeducation_no = i;
                            item.worker_code = worker_code;
                            item.company_code = company_code;
                            item.modified_by = input.modified_by;
                            blnResult = objEdu.insert(item);

                            if (!blnResult)
                            {
                                strMessage = objEdu.getMessage();
                            }
                            else
                            {
                                i++;
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    string tmp = ex.ToString();
                    blnResult = false;
                }

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = strMessage;
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTREmpeducation(InputTREmpeducation input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTREmpeducation objEdu = new cls_ctTREmpeducation();

                bool blnResult = objEdu.delete(input.company_code, input.worker_code, input.empeducation_no.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objEdu.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region TREmpaddress
        public string getTREmpaddressList(string com, string emp)
        {
            JObject output = new JObject();

            cls_ctTREmpaddress objAddr = new cls_ctTREmpaddress();
            List<cls_TREmpaddress> listAddr = objAddr.getDataByFillter(com, emp);
            JArray array = new JArray();

            if (listAddr.Count > 0)
            {
                int index = 1;

                foreach (cls_TREmpaddress model in listAddr)
                {
                    JObject json = new JObject();

                    json.Add("empaddress_type", model.empaddress_type);
                    json.Add("empaddress_no", model.empaddress_no);
                    json.Add("empaddress_moo", model.empaddress_moo);
                    json.Add("empaddress_soi", model.empaddress_soi);
                    json.Add("empaddress_road", model.empaddress_road);
                    json.Add("empaddress_tambon", model.empaddress_tambon);
                    json.Add("empaddress_amphur", model.empaddress_amphur);
                    json.Add("empaddress_zipcode", model.empaddress_zipcode);
                    json.Add("empaddress_tel", model.empaddress_tel);
                    json.Add("empaddress_email", model.empaddress_email);
                    json.Add("empaddress_line", model.empaddress_line);
                    json.Add("empaddress_facebook", model.empaddress_facebook);
                    json.Add("province_code", model.province_code);

                    json.Add("company_code", model.company_code);
                    json.Add("worker_code", model.worker_code);

                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);
                    json.Add("change", false);
                    json.Add("index", index);
                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageTREmpaddressList(InputTRList input)
        {
            JObject output = new JObject();

            try
            {
                string company_code = input.company_code;
                string worker_code = input.worker_code;


                //-- Transaction
                string addr_data = input.transaction_data;
                bool blnResult = true;
                string strMessage = "";

                try
                {
                    JObject jsonObject = new JObject();
                    var jsonArray = JsonConvert.DeserializeObject<List<cls_TREmpaddress>>(addr_data);
                    cls_ctTREmpaddress objAddr = new cls_ctTREmpaddress();

                    foreach (cls_TREmpaddress item in jsonArray)
                    {
                        item.worker_code = worker_code;
                        item.company_code = company_code;
                        item.modified_by = input.modified_by;
                        blnResult = objAddr.insert(item);

                        if (!blnResult)
                        {
                            strMessage = objAddr.getMessage();
                        }
                    }
                }
                catch
                {
                    blnResult = false;
                }

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = strMessage;
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTREmpaddress(InputTREmpaddress input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTREmpaddress objAddr = new cls_ctTREmpaddress();

                bool blnResult = objAddr.delete(input.company_code, input.worker_code, input.empaddress_type);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objAddr.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region TREmptraining
        public string getTREmptrainingList(string com, string emp)
        {
            JObject output = new JObject();

            cls_ctTREmptraining objAddr = new cls_ctTREmptraining();
            List<cls_TREmptraining> listAddr = objAddr.getDataByFillter(com, emp);
            JArray array = new JArray();

            if (listAddr.Count > 0)
            {
                int index = 1;

                foreach (cls_TREmptraining model in listAddr)
                {
                    JObject json = new JObject();

                    json.Add("emptraining_no", model.emptraining_no);
                    json.Add("emptraining_start", model.emptraining_start);
                    json.Add("emptraining_finish", model.emptraining_finish);
                    json.Add("emptraining_status", model.emptraining_status);
                    json.Add("emptraining_hours", model.emptraining_hours);
                    json.Add("emptraining_cost", model.emptraining_cost);
                    json.Add("emptraining_note", model.emptraining_note);
                    json.Add("institute_code", model.institute_code);
                    json.Add("institute_other", model.institute_other);
                    json.Add("course_code", model.course_code);
                    json.Add("course_other", model.course_other);
                    
                    json.Add("company_code", model.company_code);
                    json.Add("worker_code", model.worker_code);

                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);
                    json.Add("change", false);
                    json.Add("index", index);
                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageTREmptrainingList(InputTRList input)
        {
            JObject output = new JObject();

            try
            {
                string company_code = input.company_code;
                string worker_code = input.worker_code;


                //-- Transaction
                string addr_data = input.transaction_data;
                bool blnResult = true;
                string strMessage = "";

                try
                {
                    JObject jsonObject = new JObject();
                    var jsonArray = JsonConvert.DeserializeObject<List<cls_TREmptraining>>(addr_data);
                    cls_ctTREmptraining objEdu = new cls_ctTREmptraining();

                    blnResult = objEdu.clear(company_code, worker_code);

                    if (blnResult)
                    {
                        int i = 1;

                        foreach (cls_TREmptraining item in jsonArray)
                        {
                            item.emptraining_no = i;
                            item.worker_code = worker_code;
                            item.company_code = company_code;
                            item.modified_by = input.modified_by;
                            blnResult = objEdu.insert(item);

                            if (!blnResult)
                            {
                                strMessage = objEdu.getMessage();
                            }
                            else
                            {
                                i++;
                            }
                        }
                    }
                }
                catch
                {
                    blnResult = false;
                }

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = strMessage;
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTREmptraining(InputTREmptraining input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTREmptraining objTra = new cls_ctTREmptraining();

                bool blnResult = objTra.delete(input.company_code, input.worker_code, input.emptraining_no.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objTra.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region TREmpexperience
        public string getTREmpexperienceList(string com, string emp)
        {
            JObject output = new JObject();

            cls_ctTREmpexperience objExper = new cls_ctTREmpexperience();
            List<cls_TREmpexperience> listExper = objExper.getDataByFillter(com, emp);
            JArray array = new JArray();

            if (listExper.Count > 0)
            {
                int index = 1;

                foreach (cls_TREmpexperience model in listExper)
                {
                    JObject json = new JObject();

                    json.Add("empexperience_no", model.empexperience_no);
                    json.Add("empexperience_at", model.empexperience_at);
                    json.Add("empexperience_position", model.empexperience_position);
                    json.Add("empexperience_reasonchange", model.empexperience_reasonchange);
                    json.Add("empexperience_start", model.empexperience_start);
                    json.Add("empexperience_finish", model.empexperience_finish);
                   
                    json.Add("company_code", model.company_code);
                    json.Add("worker_code", model.worker_code);

                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);
                    json.Add("change", false);

                    json.Add("index", index);
                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageTREmpexperienceList(InputTRList input)
        {
            JObject output = new JObject();

            try
            {
                string company_code = input.company_code;
                string worker_code = input.worker_code;


                //-- Transaction
                string addr_data = input.transaction_data;
                bool blnResult = true;
                string strMessage = "";

                try
                {
                    JObject jsonObject = new JObject();
                    var jsonArray = JsonConvert.DeserializeObject<List<cls_TREmpexperience>>(addr_data);
                    cls_ctTREmpexperience objExper = new cls_ctTREmpexperience();

                    blnResult = objExper.clear(company_code, worker_code);

                    if (blnResult)
                    {
                        int i = 1;
                        foreach (cls_TREmpexperience item in jsonArray)
                        {
                            item.empexperience_no = i;
                            item.worker_code = worker_code;
                            item.company_code = company_code;
                            item.modified_by = input.modified_by;
                            blnResult = objExper.insert(item);

                            if (!blnResult)
                            {
                                strMessage = objExper.getMessage();
                            }
                            else
                            {
                                i++;
                            }

                            
                        }
                    }
                }
                catch
                {
                    blnResult = false;
                }

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = strMessage;
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTREmpexperience(InputTREmpexperience input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTREmpexperience objExper = new cls_ctTREmpexperience();

                bool blnResult = objExper.delete(input.company_code, input.worker_code, input.empexperience_no.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objExper.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region TREmpprovident
        public string getTREmpprovidentList(string com, string emp)
        {
            JObject output = new JObject();

            cls_ctTREmpprovident objPro = new cls_ctTREmpprovident();
            List<cls_TREmpprovident> listPro = objPro.getDataByFillter(com, emp, "");
            JArray array = new JArray();

            if (listPro.Count > 0)
            {
                int index = 1;

                foreach (cls_TREmpprovident model in listPro)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("worker_code", model.worker_code);
                    json.Add("provident_code", model.provident_code);
                    json.Add("empprovident_card", model.empprovident_card);
                    json.Add("empprovident_entry", model.empprovident_entry);
                    json.Add("empprovident_start", model.empprovident_start);
                    json.Add("empprovident_end", model.empprovident_end);

                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);
                    json.Add("change", false);
                    json.Add("index", index);
                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageTREmpprovidentList(InputTRList input)
        {
            JObject output = new JObject();

            try
            {
                string company_code = input.company_code;
                string worker_code = input.worker_code;


                //-- Transaction
                string pro_data = input.transaction_data;
                bool blnResult = true;
                string strMessage = "";

                try
                {
                    JObject jsonObject = new JObject();
                    var jsonArray = JsonConvert.DeserializeObject<List<cls_TREmpprovident>>(pro_data);
                    cls_ctTREmpprovident objPro = new cls_ctTREmpprovident();

                    blnResult = objPro.clear(company_code, worker_code);

                    if (blnResult)
                    {

                        foreach (cls_TREmpprovident item in jsonArray)
                        {
                            item.worker_code = worker_code;
                            item.company_code = company_code;
                            item.modified_by = input.modified_by;
                            blnResult = objPro.insert(item);

                            if (!blnResult)
                            {
                                strMessage = objPro.getMessage();
                            }
                        }
                    }
                }
                catch
                {
                    blnResult = false;
                }

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = strMessage;
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTREmpprovident(InputTREmpprovident input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTREmpprovident objPro = new cls_ctTREmpprovident();

                bool blnResult = objPro.delete(input.company_code, input.worker_code, input.provident_code);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objPro.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region TREmplocation
        public string getTREmplocationList(string com, string emp, string location)
        {
            JObject output = new JObject();

            cls_ctTREmplocation objLocation = new cls_ctTREmplocation();
            List<cls_TREmplocation> listLocation = objLocation.getDataByFillter(com, emp, location);
            JArray array = new JArray();

            if (listLocation.Count > 0)
            {
                int index = 1;

                foreach (cls_TREmplocation model in listLocation)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("worker_code", model.worker_code);
                    json.Add("location_code", model.location_code);
                    json.Add("emplocation_startdate", model.emplocation_startdate);
                    json.Add("emplocation_enddate", model.emplocation_enddate);
                    json.Add("emplocation_note", model.emplocation_note);

                    json.Add("worker_initial", model.worker_initial);
                    json.Add("worker_fname_th", model.worker_fname_th);
                    json.Add("worker_lname_th", model.worker_lname_th);
                    json.Add("worker_fname_en", model.worker_fname_en);
                    json.Add("worker_lname_en", model.worker_lname_en);
                    json.Add("location_name_th", model.location_name_th);
                    json.Add("location_name_en", model.location_name_en);
                    
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);                    
                    json.Add("flag", model.flag);

                    json.Add("change", false);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageTREmplocation(InputTREmplocation input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTREmplocation objLocation = new cls_ctTREmplocation();
                cls_TREmplocation model = new cls_TREmplocation();

                model.company_code = input.company_code;
                model.worker_code = input.worker_code;
                model.location_code = input.location_code;
                model.emplocation_startdate = Convert.ToDateTime(input.emplocation_startdate);
                model.emplocation_enddate = Convert.ToDateTime(input.emplocation_enddate);
                model.emplocation_note = input.emplocation_note;
                
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objLocation.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objLocation.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doManageTREmplocationList(InputTRList input)
        {
            JObject output = new JObject();

            try
            {
                string company_code = input.company_code;
                string worker_code = input.worker_code;
                
                //-- Transaction
                string location_data = input.transaction_data;
                bool blnResult = true;
                string strMessage = "";

                try
                {
                    JObject jsonObject = new JObject();
                    var jsonArray = JsonConvert.DeserializeObject<List<cls_TREmplocation>>(location_data);
                    cls_ctTREmplocation objLocation = new cls_ctTREmplocation();

                    blnResult = objLocation.clear(company_code, worker_code);

                    if (blnResult)
                    {
                        foreach (cls_TREmplocation item in jsonArray)
                        {
                            item.worker_code = worker_code;
                            item.company_code = company_code;

                            item.emplocation_startdate = Convert.ToDateTime(item.emplocation_startdate);
                            item.emplocation_enddate = Convert.ToDateTime(item.emplocation_enddate);

                            item.modified_by = input.modified_by;
                            blnResult = objLocation.insert(item);

                            if (!blnResult)
                            {
                                strMessage = objLocation.getMessage();
                            }
                        }
                    }
                }
                catch
                {
                    blnResult = false;
                }

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = strMessage;
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTREmplocation(InputTREmplocation input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTREmplocation objLocation = new cls_ctTREmplocation();

                bool blnResult = objLocation.delete(input.company_code, input.worker_code, Convert.ToDateTime(input.emplocation_startdate));

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objLocation.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region TREmpbank
        public string getTREmpbankList(string com, string emp)
        {
            JObject output = new JObject();

            cls_ctTREmpbank objBank = new cls_ctTREmpbank();
            List<cls_TREmpbank> listBank = objBank.getDataByFillter(com, emp);
            JArray array = new JArray();

            if (listBank.Count > 0)
            {
                int index = 1;

                foreach (cls_TREmpbank model in listBank)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("worker_code", model.worker_code);
                    json.Add("empbank_id", model.empbank_id);
                    json.Add("empbank_bankcode", model.empbank_bankcode);
                    json.Add("empbank_bankaccount", model.empbank_bankaccount);
                    json.Add("empbank_bankpercent", model.empbank_bankpercent);
                    json.Add("empbank_cashpercent", model.empbank_cashpercent);

                    json.Add("empbank_bankname", model.empbank_bankname);
                    
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("change", false);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }        
        public string doManageTREmpbankList(InputTRList input)
        {
            JObject output = new JObject();

            try
            {
                string company_code = input.company_code;
                string worker_code = input.worker_code;

                //-- Transaction
                string bank_data = input.transaction_data;
                bool blnResult = true;
                string strMessage = "";

                try
                {
                    JObject jsonObject = new JObject();
                    var jsonArray = JsonConvert.DeserializeObject<List<cls_TREmpbank>>(bank_data);
                    cls_ctTREmpbank objBank = new cls_ctTREmpbank();

                    blnResult = objBank.clear(company_code, worker_code);

                    if (blnResult)
                    {
                        foreach (cls_TREmpbank item in jsonArray)
                        {
                            item.worker_code = worker_code;
                            item.company_code = company_code;

                            item.modified_by = input.modified_by;
                            blnResult = objBank.insert(item);

                            if (!blnResult)
                            {
                                strMessage = objBank.getMessage();
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    blnResult = false;
                }

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = strMessage;
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTREmpbank(InputTREmpbank input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTREmpbank objBank = new cls_ctTREmpbank();

                bool blnResult = objBank.delete(input.empbank_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objBank.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region TREmpawpt
        public string getTREmpawptList(string com, string emp, string type)
        {
            JObject output = new JObject();

            cls_ctTREmpawpt objBank = new cls_ctTREmpawpt();
            List<cls_TREmpawpt> listBank = objBank.getDataByFillter(com, emp, type);
            JArray array = new JArray();

            if (listBank.Count > 0)
            {
                int index = 1;

                foreach (cls_TREmpawpt model in listBank)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("worker_code", model.worker_code);

                    json.Add("empawpt_no", model.empawpt_no);
                    json.Add("empawpt_start", model.empawpt_start);
                    json.Add("empawpt_finish", model.empawpt_finish);
                    json.Add("empawpt_type", model.empawpt_type);
                    json.Add("empawpt_location", model.empawpt_location);
                    json.Add("empawpt_reason", model.empawpt_reason);
                    json.Add("empawpt_note", model.empawpt_note);
                    
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("change", false);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageTREmpawptList(InputTRList input)
        {
            JObject output = new JObject();

            try
            {
                string company_code = input.company_code;
                string worker_code = input.worker_code;

                //-- Transaction
                string awpt_data = input.transaction_data;
                bool blnResult = true;
                string strMessage = "";

                try
                {
                    JObject jsonObject = new JObject();
                    var jsonArray = JsonConvert.DeserializeObject<List<cls_TREmpawpt>>(awpt_data);
                    cls_ctTREmpawpt objAwpt = new cls_ctTREmpawpt();

                    blnResult = objAwpt.clear(company_code, worker_code);

                    if (blnResult)
                    {
                        int i = 1;

                        foreach (cls_TREmpawpt item in jsonArray)
                        {
                            item.empawpt_no = i;
                            item.worker_code = worker_code;
                            item.company_code = company_code;

                            item.modified_by = input.modified_by;
                            blnResult = objAwpt.insert(item);

                            if (!blnResult)
                            {
                                strMessage = objAwpt.getMessage();
                            }
                            else
                            {
                                i++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    blnResult = false;
                }

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = strMessage;
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTREmpawpt(InputTREmpawpt input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTREmpawpt objAwpt = new cls_ctTREmpawpt();

                bool blnResult = objAwpt.delete(input.company_code, input.worker_code, input.empawpt_no.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objAwpt.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region TREmpkt20
        public string getTREmpkt20List(string com, string year, double rate)
        {
            JObject output = new JObject();

            cls_ctTREmpkt20 objKT20 = new cls_ctTREmpkt20();
            List<cls_TREmpkt20> listKT20 = objKT20.getDataByFillter(com, year, rate);
            JArray array = new JArray();

            if (listKT20.Count > 0)
            {
                int index = 1;

                foreach (cls_TREmpkt20 model in listKT20)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("worker_code", model.worker_code);
                    json.Add("year_code", model.year_code);
                    json.Add("empkt20_rate", model.empkt20_rate);                    
                    json.Add("modified_by", model.created_by);
                    json.Add("modified_date", model.created_date);
                    
                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageTREmpkt20List(InputTRList input)
        {
            JObject output = new JObject();

            try
            {
                string company_code = input.company_code;
                string year_code = input.worker_code;

                //-- Transaction
                string awpt_data = input.transaction_data;
                bool blnResult = true;
                string strMessage = "";

                try
                {
                    JObject jsonObject = new JObject();
                    var jsonArray = JsonConvert.DeserializeObject<List<cls_TREmpkt20>>(awpt_data);
                    

                    List<cls_TREmpkt20> listKT20 = new List<cls_TREmpkt20>();

                    foreach (cls_TREmpkt20 item in jsonArray)
                    {
                        item.created_by = input.modified_by;
                        listKT20.Add(item);
                    }

                    if (listKT20.Count > 0)
                    {
                        cls_ctTREmpkt20 objKT20 = new cls_ctTREmpkt20();
                        blnResult = objKT20.insert(company_code, year_code, listKT20);
                    }
                    
                }
                catch (Exception ex)
                {
                    blnResult = false;
                }

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = strMessage;
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTREmpkt20(InputTREmpkt20 input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTREmpkt20 objKT20 = new cls_ctTREmpkt20();

                bool blnResult = objKT20.delete(input.company_code, input.worker_code, input.year_code);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objKT20.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region Batch
        public string doSetBatchBenefit(InputTREmpbenefit input)
        {
            JObject output = new JObject();

            try
            {
                cls_TREmpbenefit model = new cls_TREmpbenefit();

                model.empbenefit_id = input.empbenefit_id;
                model.company_code = input.company_code;               
                model.item_code = input.item_code;
                model.empbenefit_amount = input.empbenefit_amount;
                model.empbenefit_startdate = Convert.ToDateTime(input.empbenefit_startdate);
                model.empbenefit_enddate = Convert.ToDateTime(input.empbenefit_enddate);
                model.empbenefit_reason = input.empbenefit_reason;
                model.empbenefit_note = input.empbenefit_note;

                model.empbenefit_paytype = input.empbenefit_paytype;
                model.empbenefit_break = input.empbenefit_break;
                model.empbenefit_breakreason = input.empbenefit_breakreason;

                model.modified_by = input.modified_by;
                model.flag = model.flag;

                string worker_data = input.emp_data;
                

                JArray jsonArray = JArray.Parse(worker_data);

                System.Text.StringBuilder obj_fail = new System.Text.StringBuilder();
                List<cls_TREmpbenefit> listBenefit = new List<cls_TREmpbenefit>();

                foreach (JObject json in jsonArray.Children<JObject>())
                {
                    
                    cls_TREmpbenefit emp = new cls_TREmpbenefit();
                    emp.worker_code = json.GetValue("worker_code").ToString();
                    emp.company_code = model.company_code;
                    emp.item_code = model.item_code;
                    emp.empbenefit_amount = model.empbenefit_amount;
                    emp.empbenefit_startdate = model.empbenefit_startdate;
                    emp.empbenefit_enddate = model.empbenefit_enddate;
                    emp.empbenefit_reason = model.empbenefit_reason;
                    emp.empbenefit_note = model.empbenefit_note;

                    emp.empbenefit_paytype = model.empbenefit_paytype;
                    emp.empbenefit_break = model.empbenefit_break;
                    emp.empbenefit_breakreason = model.empbenefit_breakreason;

                    emp.modified_by = model.modified_by;

                    listBenefit.Add(emp);
                }

                bool blnResult = false;
                if (listBenefit.Count > 0)
                {
                    cls_ctTREmpbenefit objBenefit = new cls_ctTREmpbenefit();
                    blnResult = objBenefit.insert(listBenefit);

                    if (blnResult)
                    {
                        
                        output["result"] = "1";
                        output["result_text"] = "";

                        List<cls_TREmpbenefit> listBenefitSuccess = objBenefit.getDataByCreateDate(model.company_code, DateTime.Now.Date);

                        JArray array = new JArray();
                        int index = 1;

                        foreach (cls_TREmpbenefit benefit in listBenefitSuccess)
                        {
                            JObject json = new JObject();

                            json.Add("company_code", benefit.company_code);
                            json.Add("worker_code", benefit.worker_code);

                            json.Add("empbenefit_id", benefit.empbenefit_id);
                            json.Add("item_code", benefit.item_code);                            
                            json.Add("empbenefit_startdate", benefit.empbenefit_startdate);
                            json.Add("empbenefit_enddate", benefit.empbenefit_enddate);
                            json.Add("empbenefit_amount", benefit.empbenefit_amount);
                            json.Add("empbenefit_reason", benefit.empbenefit_reason);
                            json.Add("empbenefit_note", benefit.empbenefit_note);

                            json.Add("worker_initial", benefit.worker_initial);
                            json.Add("worker_fname_en", benefit.worker_fname_en);
                            json.Add("worker_lname_en", benefit.worker_lname_en);
                            json.Add("worker_fname_th", benefit.worker_fname_th);
                            json.Add("worker_lname_th", benefit.worker_lname_th);
                            json.Add("item_name_th", benefit.item_name_th);
                            json.Add("item_name_en", benefit.item_name_en);

                            json.Add("flag", model.flag);

                            json.Add("index", index);

                            index++;

                            array.Add(json);
                        }

                        output["data"] = array;

                    }
                    else
                    {
                        output["result"] = "0";
                        output["result_text"] = objBenefit.getMessage();
                    }
                }
                else
                {
                    output["result"] = "0";
                    output["result_text"] = "Data not found";
                }
            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);

        }

        public string doSetBatchSalary(InputTREmpsalary input)
        {
            JObject output = new JObject();

            try
            {
                cls_TREmpsalary model = new cls_TREmpsalary();

                model.empsalary_id = input.empsalary_id;
                model.company_code = input.company_code;
                model.worker_code = input.worker_code;
                model.empsalary_amount = input.empsalary_amount;
                model.empsalary_date = Convert.ToDateTime(input.empsalary_date);
                model.empsalary_reason = input.empsalary_reason;
                model.empsalary_incamount = input.empsalary_incamount;
                model.empsalary_incpercent = input.empsalary_incpercent;
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                string worker_data = input.emp_data;

                JArray jsonArray = JArray.Parse(worker_data);

                System.Text.StringBuilder obj_fail = new System.Text.StringBuilder();
                List<cls_TREmpsalary> listSalary = new List<cls_TREmpsalary>();

                foreach (JObject json in jsonArray.Children<JObject>())
                {

                    cls_TREmpsalary emp = new cls_TREmpsalary();
                    emp.worker_code = json.GetValue("worker_code").ToString();

                    emp.company_code = input.company_code;                    
                    emp.empsalary_amount = input.empsalary_amount;
                    emp.empsalary_date = Convert.ToDateTime(input.empsalary_date);
                    emp.empsalary_reason = input.empsalary_reason;
                    emp.empsalary_incamount = input.empsalary_incamount;
                    emp.empsalary_incpercent = input.empsalary_incpercent;
                    emp.modified_by = input.modified_by;
                    emp.flag = model.flag;

                    listSalary.Add(emp);
                }

                bool blnResult = false;
                if (listSalary.Count > 0)
                {
                    cls_ctTREmpsalary objSalary = new cls_ctTREmpsalary();
                    blnResult = objSalary.insert(model.empsalary_date, listSalary);

                    if (blnResult)
                    {
                        output["result"] = "1";
                        output["result_text"] = "";

                        List<cls_TREmpsalary> listSalarySuccess = objSalary.getDataByCreateDate(model.company_code, DateTime.Now.Date);

                        JArray array = new JArray();
                        int index = 1;

                        foreach (cls_TREmpsalary benefit in listSalarySuccess)
                        {
                            JObject json = new JObject();

                            json.Add("company_code", benefit.company_code);
                            json.Add("worker_code", benefit.worker_code);

                            json.Add("empbenefit_id", benefit.empsalary_id);

                            json.Add("empsalary_date", benefit.empsalary_date);
                            json.Add("empsalary_amount", benefit.empsalary_amount);
                            json.Add("empsalary_reason", benefit.empsalary_reason);
                            json.Add("empsalary_incamount", benefit.empsalary_incamount);
                            json.Add("empsalary_incpercent", benefit.empsalary_incpercent);
                            json.Add("worker_initial", benefit.worker_initial);
                            json.Add("worker_fname_en", benefit.worker_fname_en);
                            json.Add("worker_lname_en", benefit.worker_lname_en);
                            json.Add("worker_fname_th", benefit.worker_fname_th);
                            json.Add("worker_lname_th", benefit.worker_lname_th);                            

                            json.Add("flag", model.flag);

                            json.Add("index", index);

                            index++;

                            array.Add(json);
                        }

                        output["data"] = array;
                    }
                    else
                    {
                        output["result"] = "0";
                        output["result_text"] = objSalary.getMessage();
                    }
                }
                else
                {
                    output["result"] = "0";
                    output["result_text"] = "Data not found";
                }
            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);

        }

        public string doSetBatchLocation(InputTREmplocation input)
        {
            JObject output = new JObject();

            try
            {
                cls_TREmplocation model = new cls_TREmplocation();

                model.company_code = input.company_code;
                model.worker_code = input.worker_code;
                model.location_code = input.location_code;
                model.emplocation_startdate = Convert.ToDateTime(input.emplocation_startdate);
                model.emplocation_enddate = Convert.ToDateTime(input.emplocation_enddate);
                model.emplocation_note = input.emplocation_note;

                model.modified_by = input.modified_by;
                model.flag = model.flag;

                string worker_data = input.emp_data;

                JArray jsonArray = JArray.Parse(worker_data);

                System.Text.StringBuilder obj_fail = new System.Text.StringBuilder();
                List<cls_TREmplocation> listLocation = new List<cls_TREmplocation>();

                foreach (JObject json in jsonArray.Children<JObject>())
                {

                    cls_TREmplocation emp = new cls_TREmplocation();
                    emp.worker_code = json.GetValue("worker_code").ToString();

                    emp.company_code = input.company_code;
                    emp.location_code = input.location_code;
                    emp.emplocation_startdate = model.emplocation_startdate;
                    emp.emplocation_enddate = model.emplocation_enddate;
                    emp.emplocation_note = input.emplocation_note;

                    emp.modified_by = input.modified_by;
                    emp.flag = model.flag;

                    listLocation.Add(emp);
                }

                bool blnResult = false;
                if (listLocation.Count > 0)
                {
                    cls_ctTREmplocation objLocation = new cls_ctTREmplocation();
                    blnResult = objLocation.insert(model.emplocation_startdate, listLocation);

                    if (blnResult)
                    {
                        output["result"] = "1";
                        output["result_text"] = "";

                    }
                    else
                    {
                        output["result"] = "0";
                        output["result_text"] = objLocation.getMessage();
                    }
                }
                else
                {
                    output["result"] = "0";
                    output["result_text"] = "Data not found";
                }
            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);

        }        
        #endregion

        #endregion

        #region Payroll


        //paybank
        public string getpaybank(string com)
        {
            JObject output = new JObject();

            //cls_ctTRPaybank objProvident = new cls_ctTRPaybank();
            //List<cls_TRPaybank> listProvident = objpaybank.getDataByFillter(com, "", "");

            cls_ctTRPaybank objPaybank = new cls_ctTRPaybank();
            List<cls_TRPaybank> list_paybank = objPaybank.getDataByFillter(com, strEmp);

            JArray array = new JArray();

            if (list_paybank.Count > 0)
            {
                int index = 1;

                foreach (cls_TRPaybank model in list_paybank)
                {
                    JObject json = new JObject();

                    json.Add("paybank_code", model.paybank_code);
                    json.Add("paybank_bankcode", model.paybank_bankcode);
                    json.Add("paybank_bankaccount", model.paybank_bankaccount);
                    json.Add("paybank_bankamount", model.paybank_bankamount);

                    //json.Add("modified_by", model.modified_by);

                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                   
                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }



        #region MTProvident
        public string getMTProvidentList(string com)
        {
            JObject output = new JObject();

            cls_ctMTProvident objProvident = new cls_ctMTProvident();
            List<cls_MTProvident> listProvident = objProvident.getDataByFillter(com, "", "");

            JArray array = new JArray();

            if (listProvident.Count > 0)
            {
                int index = 1;

                foreach (cls_MTProvident model in listProvident)
                {
                    JObject json = new JObject();

                    json.Add("provident_id", model.provident_id);
                    json.Add("provident_code", model.provident_code);
                    json.Add("provident_name_th", model.provident_name_th);
                    json.Add("provident_name_en", model.provident_name_en);
                  
                    json.Add("company_code", model.company_code);

                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string getTRProvidentWorkageList(string com, string code)
        {
            JObject output = new JObject();

            cls_ctTRProvidentWorkage objWorkage = new cls_ctTRProvidentWorkage();
            List<cls_TRProvidentWorkage> listWorkage = objWorkage.getDataByFillter(com, code);

            if (listWorkage.Count > 0)
            {
                JArray array = new JArray();

                int index = 1;

                foreach (cls_TRProvidentWorkage model in listWorkage)
                {
                    JObject json = new JObject();

                    json.Add("leave_code", model.provident_code);
                    json.Add("workage_from", model.workage_from);
                    json.Add("workage_to", model.workage_to);
                    json.Add("rate_emp", model.rate_emp);
                    json.Add("rate_com", model.rate_com);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
            }

            return output.ToString(Formatting.None);
        }
        public string doManageMTProvident(InputMTProvident input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTProvident objProvident = new cls_ctMTProvident();
                cls_MTProvident model = new cls_MTProvident();

                model.provident_id = Convert.ToInt32(input.provident_id);

                model.company_code = input.company_code;
                model.provident_id = Convert.ToInt32(input.provident_id);
                model.provident_code = input.provident_code;
                model.provident_name_th = input.provident_name_th;
                model.provident_name_en = input.provident_name_en;                
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objProvident.insert(model);

                if (blnResult)
                {

                    string workage_data = input.workage_data;

                    try
                    {
                        JObject jsonObject = new JObject();
                        var jsonArray = JsonConvert.DeserializeObject<List<cls_TRProvidentWorkage>>(workage_data);


                        List<cls_TRProvidentWorkage> list_model = new List<cls_TRProvidentWorkage>();

                        foreach (cls_TRProvidentWorkage item in jsonArray)
                        {
                            item.provident_code = model.provident_code;
                            item.company_code = model.company_code;
                            list_model.Add(item);
                        }

                        cls_ctTRProvidentWorkage objTRWorkage = new cls_ctTRProvidentWorkage();
                        if (list_model.Count > 0)
                        {                            
                            objTRWorkage.insert(list_model);
                        }
                        else
                        {
                            //-- Clear
                            objTRWorkage.delete(model.company_code, model.provident_code);
                        }

                    }
                    catch (Exception ex)
                    {
                        string str = ex.ToString();
                    }


                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objProvident.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTProvident(InputMTProvident input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTProvident objProvident = new cls_ctMTProvident();

                bool blnResult = objProvident.delete(input.provident_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objProvident.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region Payitem
        public string getTRPayitemList(string language, string com, string emp, string paydate, string itemtype, string itemcode)
        {
            JObject output = new JObject();

            cls_ctTRPayitem objPayitem = new cls_ctTRPayitem();
            List<cls_TRPayitem> listPayitem = objPayitem.getDataByFillter(language, com, emp, Convert.ToDateTime(paydate), itemtype, itemcode);
            JArray array = new JArray();

            if (listPayitem.Count > 0)
            {
                int index = 1;

                foreach (cls_TRPayitem model in listPayitem)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("worker_code", model.worker_code);
                    json.Add("item_code", model.item_code);
                    json.Add("payitem_date", model.payitem_date);
                    json.Add("payitem_amount", model.payitem_amount);
                    json.Add("payitem_quantity", model.payitem_quantity);
                    json.Add("payitem_paytype", model.payitem_paytype);
                    json.Add("payitem_note", model.payitem_note);

                    json.Add("item_detail", model.item_detail);
                    json.Add("item_type", model.item_type);
                    json.Add("worker_detail", model.worker_detail);

                    
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);
                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageTRPayitemList(InputTRList input)
        {
            JObject output = new JObject();

            try
            {
                string company_code = input.company_code;
                string worker_code = input.worker_code;
                
                //-- Transaction
                string pay_data = input.transaction_data;
                bool blnResult = true;
                string strMessage = "";

                try
                {
                    JObject jsonObject = new JObject();
                    var jsonArray = JsonConvert.DeserializeObject<List<cls_TRPayitem>>(pay_data);
                    cls_ctTRPayitem objPayitem = new cls_ctTRPayitem();

                    foreach (cls_TRPayitem item in jsonArray)
                    {                        
                        item.modified_by = input.modified_by;
                        blnResult = objPayitem.insert(item);

                        if (!blnResult)
                        {
                            strMessage = objPayitem.getMessage();
                        }
                    }
                }
                catch(Exception ex)
                {
                    string str = ex.ToString();
                    blnResult = false;
                }

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = strMessage;
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doManageTRPayitem(InputTRPayitem input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRPayitem objPayitem = new cls_ctTRPayitem();
                cls_TRPayitem model = new cls_TRPayitem();
                
                model.company_code = input.company_code;
                model.worker_code = input.worker_code;
                model.item_code = input.item_code;
                model.payitem_date = Convert.ToDateTime(input.payitem_date);
                model.payitem_amount = input.payitem_amount;
                model.payitem_quantity = input.payitem_quantity;
                model.payitem_paytype = input.payitem_paytype;
                model.payitem_note = input.payitem_note;
            
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objPayitem.insert(model);

                if (blnResult)
                {                    
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objPayitem.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTRPayitem(InputTRPayitem input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRPayitem objPayitem = new cls_ctTRPayitem();

                bool blnResult = objPayitem.delete(input.company_code, input.worker_code, input.item_code, Convert.ToDateTime(input.payitem_date));

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objPayitem.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region Paytran
        public string getTRPaytranList(string language, string com, string emp, string fromdate, string todate)
        {
            JObject output = new JObject();

            cls_ctTRPaytran objPaytran = new cls_ctTRPaytran();
            List<cls_TRPaytran> listPaytran = objPaytran.getDataByFillter(language, com, Convert.ToDateTime(fromdate), Convert.ToDateTime(todate), emp);
            JArray array = new JArray();

            if (listPaytran.Count > 0)
            {
                int index = 1;

                foreach (cls_TRPaytran model in listPaytran)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("worker_code", model.worker_code);
                    json.Add("paytran_date", model.paytran_date);

                    json.Add("paytran_ssoemp", model.paytran_ssoemp);
                    json.Add("paytran_ssocom", model.paytran_ssocom);
                    json.Add("paytran_ssorateemp", model.paytran_ssorateemp);
                    json.Add("paytran_ssoratecom", model.paytran_ssoratecom);

                    json.Add("paytran_pfemp", model.paytran_pfemp);
                    json.Add("paytran_pfcom", model.paytran_pfcom);

                    json.Add("paytran_income_401", model.paytran_income_401);
                    json.Add("paytran_deduct_401", model.paytran_deduct_401);
                    json.Add("paytran_tax_401", model.paytran_tax_401);

                    json.Add("paytran_income_4012", model.paytran_income_4012);
                    json.Add("paytran_deduct_4012", model.paytran_deduct_4012);
                    json.Add("paytran_tax_4012", model.paytran_tax_4012);

                    json.Add("paytran_income_4013", model.paytran_income_4013);
                    json.Add("paytran_deduct_4013", model.paytran_deduct_4013);
                    json.Add("paytran_tax_4013", model.paytran_tax_4013);

                    json.Add("paytran_income_402I", model.paytran_income_402I);
                    json.Add("paytran_deduct_402I", model.paytran_deduct_402I);
                    json.Add("paytran_tax_402I", model.paytran_tax_402I);

                    json.Add("paytran_income_402O", model.paytran_income_402O);
                    json.Add("paytran_deduct_402O", model.paytran_deduct_402O);
                    json.Add("paytran_tax_402O", model.paytran_tax_402O);

                    json.Add("paytran_income_notax", model.paytran_income_notax);
                    json.Add("paytran_deduct_notax", model.paytran_deduct_notax);

                    json.Add("paytran_income_total", model.paytran_income_total);
                    json.Add("paytran_deduct_total", model.paytran_deduct_total);

                    json.Add("paytran_netpay_b", model.paytran_netpay_b);
                    json.Add("paytran_netpay_c", model.paytran_netpay_c);

                    double other_income = model.paytran_income_total - model.paytran_salary;
                    double other_deduct = model.paytran_deduct_total - (model.paytran_tax_401 + model.paytran_income_4013 + model.paytran_ssoemp + model.paytran_pfemp);

                    json.Add("worker_detail", model.worker_detail);
                    json.Add("paytran_salary", model.paytran_salary);
                    json.Add("paytran_tax_total", model.paytran_tax_401 + model.paytran_income_4013 + model.paytran_income_402I + model.paytran_income_402O);
                    json.Add("paytran_netpay", model.paytran_netpay_b + model.paytran_netpay_c);

                    json.Add("other_income", other_income);
                    json.Add("other_deduct", other_deduct);

                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);
                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }        
        public string doManageTRPaytran(InputTRPaytran input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRPaytran objPaytran = new cls_ctTRPaytran();
                cls_TRPaytran model = new cls_TRPaytran();

                model.company_code = input.company_code;
                model.worker_code = input.worker_code;                
                model.paytran_date = Convert.ToDateTime(input.paytran_date);

                model.paytran_ssoemp = input.paytran_ssoemp;
                model.paytran_ssocom = input.paytran_ssocom;
                model.paytran_ssorateemp = input.paytran_ssorateemp;
                model.paytran_ssoratecom = input.paytran_ssoratecom;

                model.paytran_pfemp = input.paytran_pfemp;
                model.paytran_pfcom = input.paytran_pfcom;

                model.paytran_income_401 = input.paytran_income_401;
                model.paytran_deduct_401 = input.paytran_deduct_401;
                model.paytran_tax_401 = input.paytran_tax_401;

                model.paytran_income_4012 = input.paytran_income_4012;
                model.paytran_deduct_4012 = input.paytran_deduct_4012;
                model.paytran_tax_4012 = input.paytran_tax_4012;

                model.paytran_income_4013 = input.paytran_income_4013;
                model.paytran_deduct_4013 = input.paytran_deduct_4013;
                model.paytran_tax_4013 = input.paytran_tax_4013;

                model.paytran_income_402I = input.paytran_income_402I;
                model.paytran_deduct_402I = input.paytran_deduct_402I;
                model.paytran_tax_402I = input.paytran_tax_402I;

                model.paytran_income_402O = input.paytran_income_402O;
                model.paytran_deduct_402O = input.paytran_deduct_402O;
                model.paytran_tax_402O = input.paytran_tax_402O;

                model.paytran_income_notax = input.paytran_income_notax;
                model.paytran_deduct_notax = input.paytran_deduct_notax;

                model.paytran_income_total = input.paytran_income_total;
                model.paytran_deduct_total = input.paytran_deduct_total;

                model.paytran_netpay_b = input.paytran_netpay_b;
                model.paytran_netpay_c = input.paytran_netpay_c;

                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objPaytran.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objPaytran.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTRPaytran(InputTRPaytran input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRPaytran objPaytran = new cls_ctTRPaytran();

                bool blnResult = objPaytran.delete(input.company_code, input.worker_code, Convert.ToDateTime(input.paytran_date));

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objPaytran.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }

        public string getTRPaytranAccList(string language, string com, string emp, string year, string paydate)
        {
            JObject output = new JObject();
                        
            cls_ctTRPaytran objPaytran = new cls_ctTRPaytran();
            List<cls_TRPaytran> listPaytran = objPaytran.getDataByYear(language, com, year, emp);
            JArray array = new JArray();

            if (listPaytran.Count > 0)
            {
                int index = 1;

                cls_TRPaytran paytran = new cls_TRPaytran();

                paytran.paytran_ssoemp = 0;
                paytran.paytran_ssocom = 0;
                paytran.paytran_pfemp = 0;
                paytran.paytran_pfcom = 0;

                paytran.paytran_income_401 = 0;
                paytran.paytran_income_4012 = 0;
                paytran.paytran_income_4013 = 0;
                paytran.paytran_income_402I = 0;
                paytran.paytran_income_402O = 0;

                paytran.paytran_tax_401 = 0;
                paytran.paytran_tax_4012 = 0;
                paytran.paytran_tax_4013 = 0;
                paytran.paytran_tax_402I = 0;
                paytran.paytran_tax_402O = 0;

                foreach (cls_TRPaytran model in listPaytran)
                {
                    if (model.paytran_date > Convert.ToDateTime(paydate))
                        continue;

                    paytran.paytran_income_401 += (model.paytran_income_401 - model.paytran_deduct_401);
                    paytran.paytran_income_4012 += (model.paytran_income_4012 - model.paytran_deduct_4012);
                    paytran.paytran_income_4013 += (model.paytran_income_4013 - model.paytran_deduct_4013);
                    paytran.paytran_income_402I += (model.paytran_income_402I - model.paytran_deduct_402I);
                    paytran.paytran_income_402O += (model.paytran_income_402O - model.paytran_deduct_402O);

                    paytran.paytran_tax_401 += model.paytran_tax_401;
                    paytran.paytran_tax_4012 += model.paytran_tax_4012;
                    paytran.paytran_tax_4013 += model.paytran_tax_4013;
                    paytran.paytran_tax_402I += model.paytran_tax_402I;
                    paytran.paytran_tax_402O += model.paytran_tax_402O;

                    paytran.paytran_ssoemp += model.paytran_ssoemp;
                    paytran.paytran_ssocom += model.paytran_ssocom;
                    paytran.paytran_pfemp += model.paytran_pfemp;
                    paytran.paytran_pfcom += model.paytran_pfcom;

                }

                JObject json = new JObject();

                json.Add("company_code", com);
                json.Add("worker_code", emp);
                json.Add("year_code", year);

                json.Add("paytran_income_401", paytran.paytran_income_401);
                json.Add("paytran_income_4012", paytran.paytran_income_4012);
                json.Add("paytran_income_4013", paytran.paytran_income_4013);
                json.Add("paytran_income_402I", paytran.paytran_income_402I);
                json.Add("paytran_income_402O", paytran.paytran_income_402O);

                json.Add("paytran_tax_401", paytran.paytran_tax_401);
                json.Add("paytran_tax_4012", paytran.paytran_tax_4012);
                json.Add("paytran_tax_4013", paytran.paytran_tax_4013);
                json.Add("paytran_tax_402I", paytran.paytran_tax_402I);
                json.Add("paytran_tax_402O", paytran.paytran_tax_402O);

                json.Add("paytran_ssoemp", paytran.paytran_ssoemp);
                json.Add("paytran_ssocom", paytran.paytran_ssocom);
                json.Add("paytran_pfemp", paytran.paytran_pfemp);
                json.Add("paytran_pfcom", paytran.paytran_pfcom);                

                json.Add("index", index);
                index++;

                array.Add(json);


                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }        
        #endregion

        #region Payreduce
        public string getTRPayreduceList(string com, string emp, string paydate)
        {
            JObject output = new JObject();

            cls_ctTRPayreduce objPayreduce = new cls_ctTRPayreduce();
            List<cls_TRPayreduce> listPayreduce = objPayreduce.getDataByFillter(com, emp, Convert.ToDateTime(paydate));
            JArray array = new JArray();

            if (listPayreduce.Count > 0)
            {
                int index = 1;

                foreach (cls_TRPayreduce model in listPayreduce)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("worker_code", model.worker_code);
                    json.Add("payreduce_paydate", model.payreduce_paydate);
                    json.Add("reduce_code", model.reduce_code);
                    json.Add("payreduce_amount", model.payreduce_amount);
                    json.Add("reduce_name_th", model.reduce_name_th);
                    json.Add("reduce_name_en", model.reduce_name_en);
                    
                    json.Add("index", index);
                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        
        #endregion

        #region MTBonus
        public string getMTBonusList(string com)
        {
            JObject output = new JObject();

            cls_ctMTBonus objBonus = new cls_ctMTBonus();
            List<cls_MTBonus> listBonus = objBonus.getDataByFillter(com, "", "");

            JArray array = new JArray();

            if (listBonus.Count > 0)
            {
                int index = 1;

                foreach (cls_MTBonus model in listBonus)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("bonus_id", model.bonus_id);
                    json.Add("bonus_code", model.bonus_code);
                    json.Add("bonus_name_th", model.bonus_name_th);
                    json.Add("bonus_name_en", model.bonus_name_en);

                    json.Add("item_code", model.item_code);

                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageMTBonus(InputMTBonus input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTBonus objBonus = new cls_ctMTBonus();
                cls_MTBonus model = new cls_MTBonus();

                model.company_code = input.company_code;

                model.bonus_id = input.bonus_id;
                model.bonus_code = input.bonus_code;

                model.bonus_name_th = input.bonus_name_th;
                model.bonus_name_en = input.bonus_name_en;

                model.item_code = input.item_code;

                model.modified_by = input.modified_by;                
                model.flag = model.flag;

                bool blnResult = objBonus.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objBonus.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTBonus(InputMTBonus input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTBonus objBonus = new cls_ctMTBonus();

                bool blnResult = objBonus.delete(input.bonus_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objBonus.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region TRBonusrate
        public string getTRBonusrateList(string com, string bonuscode)
        {
            JObject output = new JObject();

            cls_ctTRBonusrate objRate = new cls_ctTRBonusrate();
            List<cls_TRBonusrate> listRate = objRate.getDataByFillter(com, bonuscode);
            JArray array = new JArray();

            if (listRate.Count > 0)
            {

                int index = 1;

                foreach (cls_TRBonusrate model in listRate)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("bonus_code", model.bonus_code);
                    json.Add("bonusrate_from", model.bonusrate_from);
                    json.Add("bonusrate_to", model.bonusrate_to);
                    json.Add("bonusrate_rate", model.bonusrate_rate);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageTRBonusrateList(InputTRList input)
        {
            JObject output = new JObject();

            try
            {
                string company_code = input.company_code;
               
                //-- Transaction
                string access_data = input.transaction_data;
                bool blnResult = true;
                string strMessage = "";

                try
                {
                    JObject jsonObject = new JObject();
                    var jsonArray = JsonConvert.DeserializeObject<List<cls_TRBonusrate>>(access_data);

                    List<cls_TRBonusrate> listRate = new List<cls_TRBonusrate>();

                    foreach (cls_TRBonusrate item in jsonArray)
                    {
                        listRate.Add(item);
                    }

                    if (listRate.Count > 0)
                    {
                        cls_ctTRBonusrate objRate = new cls_ctTRBonusrate();
                        blnResult = objRate.insert(listRate);
                    }

                }
                catch
                {
                    blnResult = false;
                }

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = strMessage;
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);
        }
        public string doDeleteTRBonusrate(InputTRBonusrate input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRBonusrate objAccess = new cls_ctTRBonusrate();

                bool blnResult = objAccess.delete(input.company_code, input.bonus_code);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objAccess.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region Paybonus
        public string getTRPaybonusList(string username, string language, string com, string paydate)
        {
            JObject output = new JObject();

            cls_ctTRPaybonus objPaybonus = new cls_ctTRPaybonus();
            List<cls_TRPaybonus> listPaybonus = objPaybonus.getDataByFillter(language, com, Convert.ToDateTime(paydate), "");
            JArray array = new JArray();

            if (listPaybonus.Count > 0)
            {
                int index = 1;

                foreach (cls_TRPaybonus model in listPaybonus)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("worker_code", model.worker_code);
                    json.Add("paybonus_date", model.paybonus_date);
                    json.Add("paybonus_amount", model.paybonus_amount);
                    json.Add("paybonus_quantity", model.paybonus_quantity);
                    json.Add("paybonus_rate", model.paybonus_rate);
                    json.Add("paybonus_tax", model.paybonus_tax);

                    json.Add("worker_detail", model.worker_detail);
                    
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);
                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageTRPaybonusList(InputTRList input)
        {
            JObject output = new JObject();

            try
            {                
                //-- Transaction
                string pay_data = input.transaction_data;
                bool blnResult = true;
                string strMessage = "";

                try
                {
                    JObject jsonObject = new JObject();
                    var jsonArray = JsonConvert.DeserializeObject<List<cls_TRPaybonus>>(pay_data);
                    cls_ctTRPaybonus objPaybonus = new cls_ctTRPaybonus();

                    foreach (cls_TRPaybonus item in jsonArray)
                    {                        
                        item.modified_by = input.modified_by;
                        blnResult = objPaybonus.insert(item);

                        if (!blnResult)
                        {
                            strMessage = objPaybonus.getMessage();
                        }
                    }
                }
                catch (Exception ex)
                {
                    string str = ex.ToString();
                    blnResult = false;
                }

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = strMessage;
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doManageTRPaybonus(InputTRPaybonus input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRPaybonus objPaybonus = new cls_ctTRPaybonus();
                cls_TRPaybonus model = new cls_TRPaybonus();

                model.company_code = input.company_code;
                model.worker_code = input.worker_code;
                model.paybonus_date = Convert.ToDateTime(input.paybonus_date);
                model.paybonus_amount = input.paybonus_amount;
                model.paybonus_quantity = input.paybonus_quantity;
                model.paybonus_rate = input.paybonus_rate;
                model.paybonus_tax = input.paybonus_tax;
                model.paybonus_note = input.paybonus_note;

                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objPaybonus.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objPaybonus.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTRPaybonus(InputTRPaybonus input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRPaybonus objPaybonus = new cls_ctTRPaybonus();

                bool blnResult = objPaybonus.delete(input.company_code, input.worker_code, Convert.ToDateTime(input.paybonus_date));

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objPaybonus.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region Batch policy bonus
        public string getTRPaypolbonusList(string language, string username, string com, string bonuscode)
        {
            JObject output = new JObject();

            cls_ctTRPaypolbonus objPol = new cls_ctTRPaypolbonus();
            List<cls_TRPaypolbonus> listPol = objPol.getDataByFillter(language, "", com, bonuscode);

            JArray array = new JArray();

            if (listPol.Count > 0)
            {

                int index = 1;

                foreach (cls_TRPaypolbonus model in listPol)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("worker_code", model.worker_code);
                    json.Add("worker_detail", model.worker_detail);
                    json.Add("paypolbonus_code", model.paypolbonus_code);                    
                    json.Add("modified_by", model.created_by);
                    json.Add("modified_date", model.created_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageTRPaypolbonusList(InputTRList input)
        {
            JObject output = new JObject();

            try
            {
                string company_code = input.company_code;
                string bonus_code = input.worker_code;

                //-- Transaction
                string pay_data = input.transaction_data;
                bool blnResult = true;
                string strMessage = "";

                try
                {
                    JObject jsonObject = new JObject();
                    var jsonArray = JsonConvert.DeserializeObject<List<cls_TRPaypolbonus>>(pay_data);
                    
                    List<cls_TRPaypolbonus> listPol = new List<cls_TRPaypolbonus>();

                    foreach (cls_TRPaypolbonus item in jsonArray)
                    {
                        item.created_by = input.modified_by;
                        listPol.Add(item);
                    }

                    if (listPol.Count > 0)
                    {
                        cls_ctTRPaypolbonus objPol = new cls_ctTRPaypolbonus();
                        blnResult = objPol.insert(company_code, bonus_code, listPol);
                    }
                }
                catch (Exception ex)
                {
                    string str = ex.ToString();
                    blnResult = false;
                }

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = strMessage;
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTRPaypolbonus(InputTRPaypolbonus input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRPaypolbonus objPol = new cls_ctTRPaypolbonus();
                bool blnResult = objPol.delete(input.company_code, input.worker_code, input.paypolbonus_code);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objPol.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #endregion
                
        #region Attendance
        #region MTShift
        public string getMTShiftList(string com)
        {
            JObject output = new JObject();

            cls_ctMTShift objShift = new cls_ctMTShift();
            List<cls_MTShift> listShift = objShift.getDataByFillter(com, "", "");

            JArray array = new JArray();

            if (listShift.Count > 0)
            {
                int index = 1;

                foreach (cls_MTShift model in listShift)
                {
                    JObject json = new JObject();

                    json.Add("shift_id", model.shift_id);
                    json.Add("shift_code", model.shift_code);
                    json.Add("shift_name_th", model.shift_name_th);
                    json.Add("shift_name_en", model.shift_name_en);

                    json.Add("shift_ch1", model.shift_ch1);
                    json.Add("shift_ch2", model.shift_ch2);
                    json.Add("shift_ch3", model.shift_ch3);
                    json.Add("shift_ch4", model.shift_ch4);
                    json.Add("shift_ch5", model.shift_ch5);
                    json.Add("shift_ch6", model.shift_ch6);
                    json.Add("shift_ch7", model.shift_ch7);
                    json.Add("shift_ch8", model.shift_ch8);
                    json.Add("shift_ch9", model.shift_ch9);
                    json.Add("shift_ch10", model.shift_ch10);

                    json.Add("shift_ch3_from", model.shift_ch3_from);
                    json.Add("shift_ch3_to", model.shift_ch3_to);
                    json.Add("shift_ch4_from", model.shift_ch4_from);
                    json.Add("shift_ch4_to", model.shift_ch4_to);

                    json.Add("shift_ch7_from", model.shift_ch7_from);
                    json.Add("shift_ch7_to", model.shift_ch7_to);
                    json.Add("shift_ch8_from", model.shift_ch8_from);
                    json.Add("shift_ch8_to", model.shift_ch8_to);

                    json.Add("shift_otin_min", model.shift_otin_min);
                    json.Add("shift_otin_max", model.shift_otin_max);

                    json.Add("shift_otout_min", model.shift_otout_min);
                    json.Add("shift_otout_max", model.shift_otout_max);

                    json.Add("shift_flexiblebreak", model.shift_flexiblebreak);

                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string getTRShiftallowanceList(string com, string shift)
        {
            JObject output = new JObject();

            cls_ctTRShiftallowance objAllowance = new cls_ctTRShiftallowance();
            List<cls_TRShiftallowance> listAllowance = objAllowance.getDataByFillter(com, shift);

            if (listAllowance.Count > 0)
            {
                JArray array = new JArray();

                int index = 1;

                foreach (cls_TRShiftallowance model in listAllowance)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("shift_code", model.shift_code);
                    json.Add("shiftallowance_no", model.shiftallowance_no);
                    json.Add("shiftallowance_name_th", model.shiftallowance_name_th);
                    json.Add("shiftallowance_name_en", model.shiftallowance_name_en);
                    json.Add("shiftallowance_hhmm", model.shiftallowance_hhmm);
                    json.Add("shiftallowance_amount", model.shiftallowance_amount);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
            }

            return output.ToString(Formatting.None);
        }
        public string getTRShiftbreakList(string com, string shift)
        {
            JObject output = new JObject();

            cls_ctTRShiftbreak objBreak = new cls_ctTRShiftbreak();
            List<cls_TRShiftbreak> listBreak = objBreak.getDataByFillter(com, shift);

            if (listBreak.Count > 0)
            {
                JArray array = new JArray();

                int index = 1;

                foreach (cls_TRShiftbreak model in listBreak)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("shift_code", model.shift_code);
                    json.Add("shiftbreak_no", model.shiftbreak_no);
                    json.Add("shiftbreak_from", model.shiftbreak_from);
                    json.Add("shiftbreak_to", model.shiftbreak_to);
                    json.Add("shiftbreak_break", model.shiftbreak_break);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
            }

            return output.ToString(Formatting.None);
        }
        public string doManageMTShift(InputMTShift input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTShift objShift = new cls_ctMTShift();
                cls_MTShift model = new cls_MTShift();

                model.company_code = input.company_code;

                model.shift_id = input.shift_id;
                model.shift_code = input.shift_code;
                model.shift_name_th = input.shift_name_th;
                model.shift_name_en = input.shift_name_en;
                model.shift_ch1 = input.shift_ch1;
                model.shift_ch2 = input.shift_ch2;
                model.shift_ch3 = input.shift_ch3;
                model.shift_ch4 = input.shift_ch4;
                model.shift_ch5 = input.shift_ch5;
                model.shift_ch6 = input.shift_ch6;
                model.shift_ch7 = input.shift_ch7;
                model.shift_ch8 = input.shift_ch8;
                model.shift_ch9 = input.shift_ch9;
                model.shift_ch10 = input.shift_ch10;

                model.shift_ch3_from = input.shift_ch3_from;
                model.shift_ch3_to = input.shift_ch3_to;
                model.shift_ch4_from = input.shift_ch4_from;
                model.shift_ch4_to = input.shift_ch4_to;

                model.shift_ch7_from = input.shift_ch7_from;
                model.shift_ch7_to = input.shift_ch7_to;
                model.shift_ch8_from = input.shift_ch8_from;
                model.shift_ch8_to = input.shift_ch8_to;

                model.shift_otin_min = input.shift_otin_min;
                model.shift_otin_max = input.shift_otin_max;
                model.shift_otout_min = input.shift_otout_min;
                model.shift_otout_max = input.shift_otout_max;

                model.shift_flexiblebreak = input.shift_flexiblebreak;

                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objShift.insert(model);

                if (blnResult)
                {
                    string shiftallowance_data = input.shiftallowance_data;
                    try
                    {
                        JObject jsonObject = new JObject();
                        var jsonArray = JsonConvert.DeserializeObject<List<cls_TRShiftallowance>>(shiftallowance_data);
                        List<cls_TRShiftallowance> list_model = new List<cls_TRShiftallowance>();

                        foreach (cls_TRShiftallowance item in jsonArray)
                        {
                            item.shift_code = model.shift_code;
                            item.company_code = model.company_code;
                            list_model.Add(item);
                        }

                        cls_ctTRShiftallowance objTRAllowance = new cls_ctTRShiftallowance();
                        if (objTRAllowance.delete(input.company_code, input.shift_code))
                            objTRAllowance.insert(input.company_code, input.shift_code, list_model);
                    }
                    catch (Exception ex)
                    {
                        string str = ex.ToString();
                    }

                    //-- Break
                    string break_data = input.break_data;
                    try
                    {
                        JObject jsonObject = new JObject();
                        var jsonArray = JsonConvert.DeserializeObject<List<cls_TRShiftbreak>>(break_data);
                        List<cls_TRShiftbreak> list_model = new List<cls_TRShiftbreak>();

                        foreach (cls_TRShiftbreak item in jsonArray)
                        {
                            item.shift_code = model.shift_code;
                            item.company_code = model.company_code;
                            list_model.Add(item);
                        }

                        cls_ctTRShiftbreak objTRBreak = new cls_ctTRShiftbreak();
                        objTRBreak.insert(input.company_code, input.shift_code, list_model);
                    }
                    catch (Exception ex)
                    {
                        string str = ex.ToString();
                    }


                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objShift.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTShift(InputMTShift input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTShift objShift = new cls_ctMTShift();

                bool blnResult = objShift.delete(input.shift_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objShift.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region MTHoliday
        public string getMTHolidayList(string com, string year)
        {
            JObject output = new JObject();

            cls_ctMTHoliday objHoliday = new cls_ctMTHoliday();
            List<cls_MTHoliday> listHoliday = objHoliday.getDataByFillter(com, year, "");

            JArray array = new JArray();

            if (listHoliday.Count > 0)
            {
                int index = 1;

                foreach (cls_MTHoliday model in listHoliday)
                {
                    JObject json = new JObject();

                    json.Add("holiday_id", model.holiday_id);
                    json.Add("holiday_date", model.holiday_date);
                    json.Add("holiday_name_th", model.holiday_name_th);
                    json.Add("holiday_name_en", model.holiday_name_en);

                    json.Add("year_code", model.year_code);

                    json.Add("company_code", model.company_code);

                    json.Add("holiday_daytype", model.holiday_daytype);
                    json.Add("holiday_payper", model.holiday_payper);

                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageMTHoliday(InputMTHoliday input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTHoliday objHoliday = new cls_ctMTHoliday();
                cls_MTHoliday model = new cls_MTHoliday();

                model.holiday_id = input.holiday_id;
                model.holiday_date = Convert.ToDateTime(input.holiday_date);
                model.holiday_name_th = input.holiday_name_th;
                model.holiday_name_en = input.holiday_name_en;

                model.year_code = input.year_code;
                model.company_code = input.company_code;

                model.holiday_daytype = input.holiday_daytype;
                model.holiday_payper = input.holiday_payper;


                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objHoliday.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objHoliday.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTHoliday(InputMTHoliday input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTHoliday objHoliday = new cls_ctMTHoliday();

                bool blnResult = objHoliday.delete(input.holiday_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objHoliday.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region MTPlanshift
        public string getMTPlanshiftList(string com)
        {
            JObject output = new JObject();

            cls_ctMTPlanshift objPlanshift = new cls_ctMTPlanshift();
            List<cls_MTPlanshift> listPlanshift = objPlanshift.getDataByFillter(com, "", "");
            JArray array = new JArray();

            if (listPlanshift.Count > 0)
            {

                int index = 1;

                foreach (cls_MTPlanshift model in listPlanshift)
                {
                    JObject json = new JObject();

                    json.Add("planshift_id", model.planshift_id);
                    json.Add("planshift_code", model.planshift_code);
                    json.Add("planshift_name_th", model.planshift_name_th);
                    json.Add("planshift_name_en", model.planshift_name_en);
                    json.Add("company_code", model.company_code);
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageMTPlanshift(InputMTPlanshift input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTPlanshift objPlanshift = new cls_ctMTPlanshift();
                cls_MTPlanshift model = new cls_MTPlanshift();

                model.planshift_id = input.planshift_id;
                model.planshift_code = input.planshift_code;

                model.planshift_name_th = input.planshift_name_th;
                model.planshift_name_en = input.planshift_name_en;
                model.company_code = input.company_code;
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objPlanshift.insert(model);

                if (blnResult)
                {

                    //-- Transaction

                    //-- Schedule
                    string schedule_data = input.schedule_data;
                    try
                    {
                        JObject jsonObject = new JObject();
                        var jsonArray = JsonConvert.DeserializeObject<List<cls_TRPlanschedule>>(schedule_data);
                        cls_ctTRPlanschedule objSchedule = new cls_ctTRPlanschedule();

                        bool blnClear = objSchedule.clear(input.company_code, input.planshift_code);
                        if (blnClear)
                        {
                            foreach (cls_TRPlanschedule item in jsonArray)
                            {
                                item.company_code = input.company_code;
                                item.modified_by = input.modified_by;
                                objSchedule.insert(item);
                            }
                        }
                    }
                    catch { }




                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objPlanshift.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTPlanshift(InputMTPlanshift input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTPlanshift objPlanshift = new cls_ctMTPlanshift();

                bool blnResult = objPlanshift.delete(input.planshift_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objPlanshift.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region MTPlanschedule
        public string getTRPlanscheduleList(string com, string plan)
        {
            JObject output = new JObject();

            cls_ctTRPlanschedule objPlanshift = new cls_ctTRPlanschedule();
            List<cls_TRPlanschedule> listPlanshift = objPlanshift.getDataByFillter(com, plan);
            JArray array = new JArray();

            if (listPlanshift.Count > 0)
            {

                int index = 1;

                foreach (cls_TRPlanschedule model in listPlanshift)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("planshift_code", model.planshift_code);
                    json.Add("planschedule_fromdate", model.planschedule_fromdate);
                    json.Add("planschedule_todate", model.planschedule_todate);
                    json.Add("shift_code", model.shift_code);
                    json.Add("planschedule_sun_off", model.planschedule_sun_off);
                    json.Add("planschedule_mon_off", model.planschedule_mon_off);
                    json.Add("planschedule_tue_off", model.planschedule_tue_off);
                    json.Add("planschedule_wed_off", model.planschedule_wed_off);
                    json.Add("planschedule_thu_off", model.planschedule_thu_off);
                    json.Add("planschedule_fri_off", model.planschedule_fri_off);
                    json.Add("planschedule_sat_off", model.planschedule_sat_off);

                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageTRPlanschedule(InputTRPlanschedule input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRPlanschedule objPlanshift = new cls_ctTRPlanschedule();
                cls_TRPlanschedule model = new cls_TRPlanschedule();

                model.company_code = input.company_code;
                model.planshift_code = input.planshift_code;
                model.planschedule_fromdate = Convert.ToDateTime(input.planschedule_fromdate);
                model.planschedule_todate = Convert.ToDateTime(input.planschedule_todate);
                model.shift_code = input.shift_code;
                model.planschedule_sun_off = input.planschedule_sun_off;
                model.planschedule_mon_off = input.planschedule_mon_off;
                model.planschedule_tue_off = input.planschedule_tue_off;
                model.planschedule_wed_off = input.planschedule_wed_off;
                model.planschedule_thu_off = input.planschedule_thu_off;
                model.planschedule_fri_off = input.planschedule_fri_off;
                model.planschedule_sat_off = input.planschedule_sat_off;



                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objPlanshift.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objPlanshift.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTRPlanschedule(InputTRPlanschedule input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRPlanschedule objPlanshift = new cls_ctTRPlanschedule();

                bool blnResult = objPlanshift.delete(input.company_code, input.planshift_code, Convert.ToDateTime(input.planschedule_fromdate));

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objPlanshift.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region MTLeave
        public string getMTLeaveList(string com)
        {
            JObject output = new JObject();

            cls_ctMTLeave objLeave = new cls_ctMTLeave();
            List<cls_MTLeave> listLeave = objLeave.getDataByFillter(com, "", "");

            JArray array = new JArray();

            if (listLeave.Count > 0)
            {
                int index = 1;

                foreach (cls_MTLeave model in listLeave)
                {
                    JObject json = new JObject();

                    json.Add("leave_id", model.leave_id);
                    json.Add("leave_code", model.leave_code);
                    json.Add("leave_name_th", model.leave_name_th);
                    json.Add("leave_name_en", model.leave_name_en);
                    json.Add("leave_day_peryear", model.leave_day_peryear);
                    json.Add("leave_day_acc", model.leave_day_acc);
                    json.Add("leave_day_accexpire", model.leave_day_accexpire);
                    json.Add("leave_incholiday", model.leave_incholiday);
                    json.Add("leave_passpro", model.leave_passpro);
                    json.Add("leave_deduct", model.leave_deduct);
                    json.Add("leave_caldiligence", model.leave_caldiligence);
                    json.Add("leave_agework", model.leave_agework);
                    json.Add("leave_ahead", model.leave_ahead);
                    json.Add("leave_min_hrs", model.leave_min_hrs);
                    json.Add("leave_max_day", model.leave_max_day);
                    json.Add("company_code", model.company_code);

                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string getTRLeaveWorkageList(string com, string code)
        {
            JObject output = new JObject();

            cls_ctTRLeaveWorkage objWorkage = new cls_ctTRLeaveWorkage();
            List<cls_TRLeaveWorkage> listWorkage = objWorkage.getDataByFillter(com, code);

            if (listWorkage.Count > 0)
            {
                JArray array = new JArray();

                int index = 1;

                foreach (cls_TRLeaveWorkage model in listWorkage)
                {
                    JObject json = new JObject();

                    json.Add("leave_code", model.leave_code);
                    json.Add("workage_from", model.workage_from);
                    json.Add("workage_to", model.workage_to);
                    json.Add("workage_leaveday", model.workage_leaveday);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
            }

            return output.ToString(Formatting.None);
        }
        public string doManageMTLeave(InputMTLeave input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTLeave objLeave = new cls_ctMTLeave();
                cls_MTLeave model = new cls_MTLeave();

                model.leave_id = Convert.ToInt32(input.leave_id);

                model.company_code = input.company_code;
                model.leave_id = Convert.ToInt32(input.leave_id);
                model.leave_code = input.leave_code;
                model.leave_name_th = input.leave_name_th;
                model.leave_name_en = input.leave_name_en;
                model.leave_day_peryear = Convert.ToDouble(input.leave_day_peryear);
                model.leave_day_acc = Convert.ToDouble(input.leave_day_acc);

                string strExpire = "9999-12-31";
                try
                {
                    if (input.leave_day_accexpire != null)
                        strExpire = input.leave_day_accexpire;
                }
                catch { }

                model.leave_day_accexpire = Convert.ToDateTime(strExpire);
                model.leave_incholiday = input.leave_incholiday;
                model.leave_passpro = input.leave_passpro;
                model.leave_deduct = input.leave_deduct;
                model.leave_caldiligence = input.leave_caldiligence;
                model.leave_agework = input.leave_agework;
                model.leave_ahead = Convert.ToInt32(input.leave_ahead);
                model.leave_min_hrs = Convert.ToString(input.leave_min_hrs);
                model.leave_max_day = Convert.ToDouble(input.leave_max_day);

                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objLeave.insert(model);

                if (blnResult)
                {

                    string workage_data = input.workage_data;

                    try
                    {
                        JObject jsonObject = new JObject();
                        var jsonArray = JsonConvert.DeserializeObject<List<cls_TRLeaveWorkage>>(workage_data);


                        List<cls_TRLeaveWorkage> list_model = new List<cls_TRLeaveWorkage>();

                        foreach (cls_TRLeaveWorkage item in jsonArray)
                        {
                            item.leave_code = model.leave_code;
                            item.company_code = model.company_code;
                            list_model.Add(item);
                        }

                        if (list_model.Count > 0)
                        {
                            cls_ctTRLeaveWorkage objTRWorkage = new cls_ctTRLeaveWorkage();
                            objTRWorkage.insert(list_model);
                        }

                    }
                    catch (Exception ex)
                    {
                        string str = ex.ToString();
                    }


                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objLeave.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTLeave(InputMTLeave input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTLeave objLeave = new cls_ctMTLeave();

                bool blnResult = objLeave.delete(input.leave_id);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objLeave.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);

        }
        
        public string getLeaveEmpList(string com, string emp, string year)
        {
            JObject output = new JObject();

            try
            {
                JArray array = new JArray();

                //-- Step 1 Get policy leave (emp)
                cls_ctTREmppolatt ct_polemp = new cls_ctTREmppolatt();
                List<cls_TREmppolatt> list_polemp = ct_polemp.getDataByFillter(com, emp, "LV");

                if (list_polemp.Count > 0)
                {
                    cls_TREmppolatt polemp = list_polemp[0];

                    //-- Step 2 Get policy leave

                    cls_ctMTLeave ct_leave = new cls_ctMTLeave();
                    List<cls_MTLeave> list_leave = ct_leave.getDataByFillter(com, "", "");

                    cls_ctTRPlanleave ct_planleave = new cls_ctTRPlanleave();
                    List<cls_TRPlanleave> list_planleave = ct_planleave.getDataByFillter(com, polemp.emppolatt_policy_code);

                    if (list_planleave.Count > 0)
                    {
                        List<cls_TREmpleaveacc> list_leaveacc = new List<cls_TREmpleaveacc>();

                        int index = 1;

                        foreach (cls_TRPlanleave planleave in list_planleave)
                        {
                            cls_MTLeave polleave = null;

                            foreach (cls_MTLeave leave in list_leave)
                            {
                                if (planleave.leave_code.Equals(leave.leave_code))
                                {
                                    polleave = leave;
                                    break;
                                }
                            }
                            
                            if (polleave != null)
                            {
                                JObject json = new JObject();

                                json.Add("leave_id", polleave.leave_id);
                                json.Add("leave_code", polleave.leave_code);
                                json.Add("leave_name_th", polleave.leave_name_th);
                                json.Add("leave_name_en", polleave.leave_name_en);
                                json.Add("leave_day_peryear", polleave.leave_day_peryear);
                                json.Add("leave_day_acc", polleave.leave_day_acc);
                                json.Add("leave_day_accexpire", polleave.leave_day_accexpire);
                                json.Add("leave_incholiday", polleave.leave_incholiday);
                                json.Add("leave_passpro", polleave.leave_passpro);
                                json.Add("leave_deduct", polleave.leave_deduct);
                                json.Add("leave_caldiligence", polleave.leave_caldiligence);
                                json.Add("leave_agework", polleave.leave_agework);
                                json.Add("leave_ahead", polleave.leave_ahead);
                                json.Add("leave_min_hrs", polleave.leave_min_hrs);
                                json.Add("leave_max_day", polleave.leave_max_day);
                                json.Add("company_code", polleave.company_code);

                                json.Add("modified_by", polleave.modified_by);
                                json.Add("modified_date", polleave.modified_date);
                                json.Add("flag", polleave.flag);

                                json.Add("index", index);

                                index++;

                                array.Add(json);
                            }
                            

                        }

                        output["result"] = "1";
                        output["result_text"] = "1";
                        output["data"] = array;

                        
                    }
                    else
                    {
                        output["result"] = "0";
                        output["result_text"] = "Data not Found";
                        output["data"] = array;
                    }

                }
                else
                {
                    output["result"] = "0";
                    output["result_text"] = "Data not Found";
                    output["data"] = array;
                }
            }
            catch {

                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = "";

            }
            
            return output.ToString(Formatting.None);
        }

        #endregion

        #region MTLate
        public string getMTLateList(string com)
        {
            JObject output = new JObject();

            cls_ctMTLate objLate = new cls_ctMTLate();
            List<cls_MTLate> listLate = objLate.getDataByFillter(com, "", "");

            JArray array = new JArray();

            if (listLate.Count > 0)
            {

                int index = 1;

                foreach (cls_MTLate model in listLate)
                {
                    JObject json = new JObject();

                    json.Add("late_id", model.late_id);
                    json.Add("late_code", model.late_code);
                    json.Add("late_name_th", model.late_name_th);
                    json.Add("late_name_en", model.late_name_en);
                    json.Add("company_code", model.company_code);
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string getTRLateList(string com, string code)
        {
            JObject output = new JObject();

            cls_ctTRLate objLate = new cls_ctTRLate();
            List<cls_TRLate> listLate = objLate.getDataByFillter(com, code);

            if (listLate.Count > 0)
            {
                JArray array = new JArray();

                int index = 1;

                foreach (cls_TRLate model in listLate)
                {
                    JObject json = new JObject();

                    json.Add("late_code", model.late_code);
                    json.Add("late_from", model.late_from);
                    json.Add("late_to", model.late_to);
                    json.Add("late_deduct_type", model.late_deduct_type);
                    json.Add("late_deduct_amount", model.late_deduct_amount);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
            }

            return output.ToString(Formatting.None);
        }
        public string doManageMTLate(InputMTLate input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTLate objLate = new cls_ctMTLate();
                cls_MTLate model = new cls_MTLate();

                model.late_id = input.late_id;
                model.late_code = input.late_code;

                model.late_name_th = input.late_name_th;
                model.late_name_en = input.late_name_en;
                model.company_code = input.company_code;
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objLate.insert(model);

                if (blnResult)
                {

                    string late_data = input.late_data;

                    try
                    {
                        JObject jsonObject = new JObject();
                        var jsonArray = JsonConvert.DeserializeObject<List<cls_TRLate>>(late_data);


                        List<cls_TRLate> list_model = new List<cls_TRLate>();

                        foreach (cls_TRLate item in jsonArray)
                        {
                            item.late_code = model.late_code;
                            item.company_code = model.company_code;
                            list_model.Add(item);
                        }

                        if (list_model.Count > 0)
                        {
                            cls_ctTRLate objTRLate = new cls_ctTRLate();
                            //if (objTRLate.delete(model.company_code, model.late_code))
                            objTRLate.insert(list_model);
                        }

                    }
                    catch (Exception ex)
                    {
                        string str = ex.ToString();
                    }


                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objLate.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTLate(InputMTLate input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTLate objLate = new cls_ctMTLate();

                bool blnResult = objLate.delete(input.late_id.ToString());

                if (blnResult)
                {
                    cls_ctTRLate objCondition = new cls_ctTRLate();

                    objCondition.delete(input.company_code, input.late_code);

                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objLate.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region Rateot
        public string getMTRateotList(string com)
        {
            JObject output = new JObject();

            cls_ctMTRateot objRate = new cls_ctMTRateot();
            List<cls_MTRateot> listLate = objRate.getDataByFillter(com, "", "");

            JArray array = new JArray();

            if (listLate.Count > 0)
            {

                int index = 1;

                foreach (cls_MTRateot model in listLate)
                {
                    JObject json = new JObject();

                    json.Add("rateot_id", model.rateot_id);
                    json.Add("rateot_code", model.rateot_code);
                    json.Add("rateot_name_th", model.rateot_name_th);
                    json.Add("rateot_name_en", model.rateot_name_en);
                    json.Add("company_code", model.company_code);
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string getTRRateotList(string com, string code)
        {
            JObject output = new JObject();

            cls_ctTRRateot objRate = new cls_ctTRRateot();
            List<cls_TRRateot> listRate = objRate.getDataByFillter(com, code);

            if (listRate.Count > 0)
            {
                JArray array = new JArray();

                int index = 1;

                foreach (cls_TRRateot model in listRate)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("rateot_daytype", model.rateot_daytype);
                    json.Add("rateot_code", model.rateot_code);
                    json.Add("rateot_before", model.rateot_before);
                    json.Add("rateot_normal", model.rateot_normal);
                    json.Add("rateot_break", model.rateot_break);
                    json.Add("rateot_after", model.rateot_after);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
            }

            return output.ToString(Formatting.None);
        }
        public string doManageMTRateot(InputMTRateot input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTRateot objRate = new cls_ctMTRateot();
                cls_MTRateot model = new cls_MTRateot();

                model.company_code = input.company_code;
                model.rateot_id = input.rateot_id;
                model.rateot_code = input.rateot_code;
                model.rateot_name_th = input.rateot_name_th;
                model.rateot_name_en = input.rateot_name_en;
                model.company_code = input.company_code;
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objRate.insert(model);

                if (blnResult)
                {

                    string rateot_data = input.rateot_data;

                    try
                    {
                        JObject jsonObject = new JObject();
                        var jsonArray = JsonConvert.DeserializeObject<List<cls_TRRateot>>(rateot_data);


                        List<cls_TRRateot> list_model = new List<cls_TRRateot>();

                        foreach (cls_TRRateot item in jsonArray)
                        {
                            item.rateot_code = model.rateot_code;
                            item.company_code = model.company_code;
                            list_model.Add(item);
                        }

                        //if (list_model.Count > 0)
                        //{
                        cls_ctTRRateot objTRRate = new cls_ctTRRateot();
                        if (objTRRate.delete(input.company_code, input.rateot_code))
                            objTRRate.insert(list_model);
                        //}

                    }
                    catch (Exception ex)
                    {
                        string str = ex.ToString();
                    }


                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objRate.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTRateot(InputMTRateot input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTRateot objLate = new cls_ctMTRateot();

                bool blnResult = objLate.delete(input.rateot_id.ToString());

                if (blnResult)
                {
                    cls_ctTRLate objCondition = new cls_ctTRLate();

                    objCondition.delete(input.company_code, input.rateot_code);

                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objLate.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region Planleave
        public string getMTPlanleaveList(string com)
        {
            JObject output = new JObject();

            cls_ctMTPlanleave objPlan = new cls_ctMTPlanleave();
            List<cls_MTPlanleave> listPlan = objPlan.getDataByFillter(com, "", "");

            JArray array = new JArray();

            if (listPlan.Count > 0)
            {

                int index = 1;

                foreach (cls_MTPlanleave model in listPlan)
                {
                    JObject json = new JObject();

                    json.Add("planleave_id", model.planleave_id);
                    json.Add("planleave_code", model.planleave_code);
                    json.Add("planleave_name_th", model.planleave_name_th);
                    json.Add("planleave_name_en", model.planleave_name_en);
                    json.Add("company_code", model.company_code);
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string getTRPlanleaveList(string com, string code)
        {
            JObject output = new JObject();

            cls_ctTRPlanleave objPlan = new cls_ctTRPlanleave();
            List<cls_TRPlanleave> listPlan = objPlan.getDataByFillter(com, code);

            if (listPlan.Count > 0)
            {
                JArray array = new JArray();

                int index = 1;

                foreach (cls_TRPlanleave model in listPlan)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("planleave_code", model.planleave_code);
                    json.Add("leave_code", model.leave_code);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
            }

            return output.ToString(Formatting.None);
        }
        public string doManageMTPlanleave(InputMTPlanleave input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTPlanleave objPlan = new cls_ctMTPlanleave();
                cls_MTPlanleave model = new cls_MTPlanleave();

                model.company_code = input.company_code;
                model.planleave_id = input.planleave_id;
                model.planleave_code = input.planleave_code;
                model.planleave_name_th = input.planleave_name_th;
                model.planleave_name_en = input.planleave_name_en;
                model.company_code = input.company_code;
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objPlan.insert(model);

                if (blnResult)
                {

                    string planleave_data = input.planleave_data;

                    try
                    {
                        JObject jsonObject = new JObject();
                        var jsonArray = JsonConvert.DeserializeObject<List<cls_TRPlanleave>>(planleave_data);

                        List<cls_TRPlanleave> list_model = new List<cls_TRPlanleave>();

                        foreach (cls_TRPlanleave item in jsonArray)
                        {
                            item.planleave_code = model.planleave_code;
                            item.company_code = model.company_code;
                            list_model.Add(item);
                        }

                        //if (list_model.Count > 0)
                        //{
                        cls_ctTRPlanleave objTRPlan = new cls_ctTRPlanleave();
                        if (objTRPlan.delete(input.company_code, input.planleave_code))
                            objTRPlan.insert(list_model);
                        //}

                    }
                    catch (Exception ex)
                    {
                        string str = ex.ToString();
                    }

                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objPlan.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTPlanleave(InputMTPlanleave input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTPlanleave objPlan = new cls_ctMTPlanleave();

                bool blnResult = objPlan.delete(input.planleave_id.ToString());

                if (blnResult)
                {
                    cls_ctTRLate objCondition = new cls_ctTRLate();

                    objCondition.delete(input.company_code, input.planleave_code);

                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objPlan.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region Diligence
        public string getMTDiligenceList(string com)
        {
            JObject output = new JObject();

            cls_ctMTDiligence objDiligence = new cls_ctMTDiligence();
            List<cls_MTDiligence> listDiligence = objDiligence.getDataByFillter(com, "", "");

            JArray array = new JArray();

            if (listDiligence.Count > 0)
            {

                int index = 1;

                foreach (cls_MTDiligence model in listDiligence)
                {
                    JObject json = new JObject();

                    json.Add("diligence_id", model.diligence_id);
                    json.Add("diligence_code", model.diligence_code);
                    json.Add("diligence_name_th", model.diligence_name_th);
                    json.Add("diligence_name_en", model.diligence_name_en);

                    json.Add("diligence_punchcard", model.diligence_punchcard);
                    json.Add("diligence_punchcard_times", model.diligence_punchcard_times);
                    json.Add("diligence_punchcard_timespermonth", model.diligence_punchcard_timespermonth);

                    json.Add("diligence_late", model.diligence_late);
                    json.Add("diligence_late_times", model.diligence_late_times);
                    json.Add("diligence_late_timespermonth", model.diligence_late_timespermonth);
                    json.Add("diligence_late_acc", model.diligence_late_acc);

                    json.Add("diligence_ba", model.diligence_ba);
                    json.Add("diligence_before_min", model.diligence_before_min);
                    json.Add("diligence_after_min", model.diligence_after_min);
                    
                    json.Add("diligence_passpro", model.diligence_passpro);
                    json.Add("diligence_wrongcondition", model.diligence_wrongcondition);
                    json.Add("diligence_someperiod", model.diligence_someperiod);
                    json.Add("diligence_someperiod_first", model.diligence_someperiod_first);

                    json.Add("company_code", model.company_code);
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string getTRSteppayDiligenceList(string com, string code)
        {
            JObject output = new JObject();

            cls_ctTRDiligenceSteppay objStep = new cls_ctTRDiligenceSteppay();
            List<cls_TRDiligenceSteppay> listPlan = objStep.getDataByFillter(com, code);

            if (listPlan.Count > 0)
            {
                JArray array = new JArray();

                int index = 1;

                foreach (cls_TRDiligenceSteppay model in listPlan)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("diligence_code", model.diligence_code);
                    json.Add("steppay_step", model.steppay_step);
                    json.Add("steppay_type", model.steppay_type);
                    json.Add("steppay_amount", model.steppay_amount);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
            }

            return output.ToString(Formatting.None);
        }
        public string doManageMTDiligence(InputMTDiligence input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTDiligence objDiligence = new cls_ctMTDiligence();
                cls_MTDiligence model = new cls_MTDiligence();

                model.company_code = input.company_code;
                model.diligence_id = input.diligence_id;
                model.diligence_code = input.diligence_code;
                model.diligence_name_th = input.diligence_name_th;
                model.diligence_name_en = input.diligence_name_en;

                model.diligence_punchcard = input.diligence_punchcard;
                model.diligence_punchcard_times = input.diligence_punchcard_times;
                model.diligence_punchcard_timespermonth = input.diligence_punchcard_timespermonth;

                model.diligence_late = input.diligence_late;
                model.diligence_late_times = input.diligence_late_times;
                model.diligence_late_timespermonth = input.diligence_late_timespermonth;
                model.diligence_late_acc = input.diligence_late_acc;

                model.diligence_ba = input.diligence_ba;
                model.diligence_before_min = input.diligence_before_min;
                model.diligence_after_min = input.diligence_after_min;

                model.diligence_passpro = input.diligence_passpro;
                model.diligence_wrongcondition = input.diligence_wrongcondition;
                model.diligence_someperiod = input.diligence_someperiod;
                model.diligence_someperiod_first = input.diligence_someperiod_first;

                model.company_code = input.company_code;
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objDiligence.insert(model);

                if (blnResult)
                {

                    string steppay_data = input.steppay_data;

                    try
                    {

                        JObject jsonObject = new JObject();
                        var jsonArray = JsonConvert.DeserializeObject<List<cls_TRDiligenceSteppay>>(steppay_data);

                        List<cls_TRDiligenceSteppay> list_model = new List<cls_TRDiligenceSteppay>();

                        foreach (cls_TRDiligenceSteppay item in jsonArray)
                        {
                            item.diligence_code = model.diligence_code;
                            item.company_code = model.company_code;
                            list_model.Add(item);
                        }

                        //if (list_model.Count > 0)
                        //{
                        cls_ctTRDiligenceSteppay objTRStep = new cls_ctTRDiligenceSteppay();
                        if (objTRStep.delete(input.company_code, input.diligence_code))
                            objTRStep.insert(list_model);
                        //}

                    }
                    catch (Exception ex)
                    {
                        string str = ex.ToString();
                    }

                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objDiligence.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTDiligence(InputMTDiligence input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTDiligence objDiligence = new cls_ctMTDiligence();

                bool blnResult = objDiligence.delete(input.diligence_id.ToString());

                if (blnResult)
                {
                    //cls_ctTRLate objCondition = new cls_ctTRLate();

                    //objCondition.delete(input.company_code, input.planleave_code);

                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objDiligence.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region Batch shift
        public string doSetBatchPlanshift_OLD(InputBatchPlanshift input)
        {
            JObject output = new JObject();

            try
            {
                string company_code = input.company_code;
                string planshift_code = input.planshift_code;
                string year_code = input.year_code;

                string worker_data = input.transaction_data;
                bool blnResult = true;
                string strMessage = "";

                try
                {
                    //-- Step 1 Get Plan Year

                    cls_ctMTYear ctYear = new cls_ctMTYear();
                    List<cls_MTYear> listYear = ctYear.getDataByFillter(company_code, "LEAVE", "", year_code);

                    if (listYear.Count > 0)
                    {
                        DateTime dateFrom = listYear[0].year_fromdate;
                        DateTime dateTo = listYear[0].year_todate;


                        //-- Step 2 get plan schedule
                        cls_ctTRPlanschedule ctSchedule = new cls_ctTRPlanschedule();
                        List<cls_TRPlanschedule> listPlanschedule = ctSchedule.getDataByFillter(company_code, planshift_code);

                        //-- Step 3 get holiday
                        cls_ctMTHoliday ctHoliday = new cls_ctMTHoliday();
                        List<cls_MTHoliday> listHoliday = ctHoliday.getDataByFillter(company_code, year_code, "");
                       

                        List<cls_TRTimecard> listTimecard = new List<cls_TRTimecard>();
                        foreach (cls_TRPlanschedule schedule in listPlanschedule)
                        {
                            dateFrom = schedule.planschedule_fromdate.Date;
                            dateTo = schedule.planschedule_todate.Date;

                            //-- Loop date
                            for (DateTime dateStart = dateFrom; dateStart <= dateTo; dateStart = dateStart.AddDays(1))
                            {
                                string daytype = "N";
                                string dateName = dateStart.ToString("ddd");

                                //-- Check holiday
                                bool blnHoliday = false;
                                foreach (cls_MTHoliday holiday in listHoliday)
                                {
                                    if (holiday.holiday_date == dateStart)
                                    {
                                        daytype = "H";
                                        blnHoliday = true;
                                        break;
                                    }
                                }

                                if (!blnHoliday)
                                {
                                    if (dateName.Equals("Sun") && schedule.planschedule_sun_off.Equals("Y"))
                                        daytype = "O";
                                    else if (dateName.Equals("Mon") && schedule.planschedule_mon_off.Equals("Y"))
                                        daytype = "O";
                                    else if (dateName.Equals("Tue") && schedule.planschedule_tue_off.Equals("Y"))
                                        daytype = "O";
                                    else if (dateName.Equals("Wed") && schedule.planschedule_wed_off.Equals("Y"))
                                        daytype = "O";
                                    else if (dateName.Equals("Thu") && schedule.planschedule_thu_off.Equals("Y"))
                                        daytype = "O";
                                    else if (dateName.Equals("Fri") && schedule.planschedule_fri_off.Equals("Y"))
                                        daytype = "O";
                                    else if (dateName.Equals("Sat") && schedule.planschedule_sat_off.Equals("Y"))
                                        daytype = "O";
                                }

                                cls_TRTimecard timecard = new cls_TRTimecard();
                                timecard.company_code = company_code;
                                timecard.timecard_workdate = dateStart.Date;
                                timecard.timecard_daytype = daytype;
                                timecard.shift_code = schedule.shift_code;

                                timecard.timecard_color = "0";

                                timecard.modified_by = input.modified_by;


                                //-- Add to timecard
                                listTimecard.Add(timecard);
                            }
                        }

                        if (listTimecard.Count > 0)
                        {
                            cls_ctTRTimecard ctTimecard = new cls_ctTRTimecard();

                            JArray jsonArray = JArray.Parse(worker_data);

                            int intCountSuccss = 0;
                            int intCountFail = 0;
                            System.Text.StringBuilder obj_fail = new System.Text.StringBuilder();


                            foreach (JObject json in jsonArray.Children<JObject>())
                            {
                                string worker_code = json.GetValue("worker_code").ToString();

                                bool blnRecord = ctTimecard.insert_plantime(company_code, worker_code, listTimecard[0].timecard_workdate, listTimecard[listTimecard.Count - 1].timecard_workdate, listTimecard);

                                if (!blnRecord)
                                {
                                    intCountSuccss++;
                                }
                                else
                                {
                                    intCountFail++;
                                    obj_fail.Append(worker_code + "|");
                                }

                            }

                            blnResult = true;
                            strMessage = "Success: " + intCountSuccss + " | Fail: " + intCountFail.ToString();
                            output["result_fail"] = obj_fail.ToString();
                        }

                    }
                    else
                    {
                        strMessage = "Check the year policy";
                        blnResult = false;
                    }

                }
                catch
                {
                    blnResult = false;
                }

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = strMessage;
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }

        public string doSetBatchPlanshift(InputBatchPlanshift input)
        {
            JObject output = new JObject();

            try
            {
                string company_code = input.company_code;
                string planshift_code = input.planshift_code;
                string year_code = input.year_code;

                string worker_data = input.transaction_data;
                bool blnResult = true;
                string strMessage = "";

                try
                {
                    int intCountSuccss = 0;
                    int intCountFail = 0;
                    System.Text.StringBuilder obj_fail = new System.Text.StringBuilder();

                    //-- Step 1 Get Plan Year

                    cls_ctMTYear ctYear = new cls_ctMTYear();
                    List<cls_MTYear> listYear = ctYear.getDataByFillter(company_code, "LEAVE", "", year_code);

                    if (listYear.Count > 0)
                    {
                        DateTime dateFrom = listYear[0].year_fromdate;
                        DateTime dateTo = listYear[0].year_todate;


                        //-- Step 2 get plan schedule
                        cls_ctTRPlanschedule ctSchedule = new cls_ctTRPlanschedule();
                        List<cls_TRPlanschedule> listPlanschedule = ctSchedule.getDataByFillter(company_code, planshift_code);
                                               
                        JArray jsonArray = JArray.Parse(worker_data);

                        foreach (JObject json in jsonArray.Children<JObject>())
                        {
                            string worker_code = json.GetValue("worker_code").ToString();

                            //-- Step 3 get holiday
                            cls_ctTRHoliday ctTRHoliday = new cls_ctTRHoliday();
                            List<cls_TRHoliday> listHoliday = ctTRHoliday.getDataByWorker(company_code, worker_code);
                            
                            List<cls_TRTimecard> listTimecard = new List<cls_TRTimecard>();
                            foreach (cls_TRPlanschedule schedule in listPlanschedule)
                            {
                                dateFrom = schedule.planschedule_fromdate.Date;
                                dateTo = schedule.planschedule_todate.Date;

                                //-- Loop date
                                for (DateTime dateStart = dateFrom; dateStart <= dateTo; dateStart = dateStart.AddDays(1))
                                {
                                    string daytype = "N";
                                    string dateName = dateStart.ToString("ddd");

                                    //-- Check holiday
                                    bool blnHoliday = false;
                                    foreach (cls_TRHoliday holiday in listHoliday)
                                    {
                                        if (holiday.holiday_date == dateStart)
                                        {
                                            daytype = holiday.holiday_daytype;
                                            blnHoliday = true;
                                            break;
                                        }
                                    }

                                    if (!blnHoliday)
                                    {
                                        if (dateName.Equals("Sun") && schedule.planschedule_sun_off.Equals("Y"))
                                            daytype = "O";
                                        else if (dateName.Equals("Mon") && schedule.planschedule_mon_off.Equals("Y"))
                                            daytype = "O";
                                        else if (dateName.Equals("Tue") && schedule.planschedule_tue_off.Equals("Y"))
                                            daytype = "O";
                                        else if (dateName.Equals("Wed") && schedule.planschedule_wed_off.Equals("Y"))
                                            daytype = "O";
                                        else if (dateName.Equals("Thu") && schedule.planschedule_thu_off.Equals("Y"))
                                            daytype = "O";
                                        else if (dateName.Equals("Fri") && schedule.planschedule_fri_off.Equals("Y"))
                                            daytype = "O";
                                        else if (dateName.Equals("Sat") && schedule.planschedule_sat_off.Equals("Y"))
                                            daytype = "O";
                                    }

                                    cls_TRTimecard timecard = new cls_TRTimecard();
                                    timecard.company_code = company_code;
                                    timecard.timecard_workdate = dateStart.Date;
                                    timecard.timecard_daytype = daytype;
                                    timecard.shift_code = schedule.shift_code;

                                    timecard.timecard_color = "0";

                                    timecard.modified_by = input.modified_by;


                                    //-- Add to timecard
                                    listTimecard.Add(timecard);
                                }
                            }


                            if (listTimecard.Count > 0)
                            {
                                cls_ctTRTimecard ctTimecard = new cls_ctTRTimecard();

                                bool blnRecord = ctTimecard.insert_plantime(company_code, worker_code, listTimecard[0].timecard_workdate, listTimecard[listTimecard.Count - 1].timecard_workdate, listTimecard);

                                if (!blnRecord)
                                {
                                    intCountSuccss++;
                                }
                                else
                                {
                                    intCountFail++;
                                    obj_fail.Append(worker_code + "|");
                                }
                            }

                        } //-- foreach (JObject json in jsonArray.Children<JObject>())

                        blnResult = true;
                        strMessage = "Success: " + intCountSuccss + " | Fail: " + intCountFail.ToString();
                        output["result_fail"] = obj_fail.ToString();


                    }
                    else
                    {
                        strMessage = "Check the year policy";
                        blnResult = false;
                    }

                }
                catch
                {
                    blnResult = false;
                }

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = strMessage;
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }

        #endregion

        #region Batch policy allowance item
        public string getPolicyAttendanceItem(string com)
        {
            JObject output = new JObject();

            cls_ctTREmpattitem objPol = new cls_ctTREmpattitem();
            List<cls_TREmpattitem> listPol = objPol.getDataByFillter(com, "");

            JArray array = new JArray();

            if (listPol.Count > 0)
            {

                int index = 1;

                foreach (cls_TREmpattitem model in listPol)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("worker_code", model.worker_code);
                    json.Add("item_sa", model.empattitem_sa);
                    json.Add("item_ot", model.empattitem_ot);
                    json.Add("item_aw", model.empattitem_aw);
                    json.Add("item_dg", model.empattitem_dg);
                    json.Add("item_lv", model.empattitem_lv);
                    json.Add("item_ab", model.empattitem_ab);
                    json.Add("item_lt", model.empattitem_lt);

                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doSetPolicyAttendanceItem(InputSetPolicyAttItem input)
        {
            JObject output = new JObject();

            try
            {
                string company_code = input.company_code;
                string item_sa = input.item_sa;
                string item_ot = input.item_ot;
                string item_aw = input.item_aw;
                string item_dg = input.item_dg;
                string item_lv = input.item_lv;
                string item_ab = input.item_ab;
                string item_lt = input.item_lt;

                string worker_data = input.emp_data;
                string modified_by = input.modified_by;

                JArray jsonArray = JArray.Parse(worker_data);

                System.Text.StringBuilder obj_fail = new System.Text.StringBuilder();
                List<cls_TREmpattitem> listPol = new List<cls_TREmpattitem>();

                foreach (JObject json in jsonArray.Children<JObject>())
                {
                    string worker_code = json.GetValue("worker_code").ToString();

                    cls_TREmpattitem model = new cls_TREmpattitem();
                    model.empattitem_sa = item_sa;
                    model.empattitem_ot = item_ot;
                    model.empattitem_aw = item_aw;
                    model.empattitem_dg = item_dg;
                    model.empattitem_lv = item_lv;
                    model.empattitem_ab = item_ab;
                    model.empattitem_lt = item_lt;

                    model.company_code = company_code;
                    model.worker_code = worker_code;
                    model.modified_by = modified_by;

                    listPol.Add(model);
                }

                bool blnResult = false;
                if (listPol.Count > 0)
                {
                    cls_ctTREmpattitem objPol = new cls_ctTREmpattitem();
                    //blnResult = objPol.insert(listPol);

                    foreach (cls_TREmpattitem model in listPol)
                    {
                        if (!objPol.insert(model))
                            obj_fail.Append(model.worker_code);
                    }

                    if (obj_fail.Length == 0)
                    {
                        blnResult = true;
                    }


                    if (blnResult)
                    {
                        output["result"] = "1";
                        output["result_text"] = "";
                    }
                    else
                    {
                        output["result"] = "0";
                        output["result_text"] = objPol.getMessage();
                    }
                }
                else
                {
                    output["result"] = "0";
                    output["result_text"] = "Data not found";
                }
            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTREmppolattItem(InputTREmppolatt input)
        {
            JObject output = new JObject();

            try
            {
                
                cls_ctTREmpattitem objPol = new cls_ctTREmpattitem();
                bool blnResult = objPol.delete(input.company_code, input.worker_code);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objPol.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region Batch policy
        public string getPolicyAttendance(string com, string username, string type)
        {
            JObject output = new JObject();

            cls_ctTREmppolatt objPol = new cls_ctTREmppolatt();
            List<cls_TREmppolatt> listPol = objPol.getDataByFillter(com, "", type);

            JArray array = new JArray();

            if (listPol.Count > 0)
            {

                int index = 1;

                foreach (cls_TREmppolatt model in listPol)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("worker_code", model.worker_code);
                    json.Add("emppolatt_policy_code", model.emppolatt_policy_code);
                    json.Add("emppolatt_policy_type", model.emppolatt_policy_type);
                    json.Add("emppolatt_policy_note", model.emppolatt_policy_note);
                    json.Add("modified_by", model.created_by);
                    json.Add("modified_date", model.created_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doSetPolicyAttendance(InputSetPolicyAtt input)
        {
            JObject output = new JObject();

            try
            {
                string company_code = input.company_code;
                string pol_code = input.pol_code;
                string pol_type = input.pol_type;
                string pol_note = input.pol_note;
                string worker_data = input.emp_data;
                string modified_by = input.modified_by;

                JArray jsonArray = JArray.Parse(worker_data);

                System.Text.StringBuilder obj_fail = new System.Text.StringBuilder();
                List<cls_TREmppolatt> listPol = new List<cls_TREmppolatt>();

                foreach (JObject json in jsonArray.Children<JObject>())
                {
                    string worker_code = json.GetValue("worker_code").ToString();

                    cls_TREmppolatt model = new cls_TREmppolatt();
                    model.emppolatt_policy_code = pol_code;
                    model.emppolatt_policy_type = pol_type;
                    model.emppolatt_policy_note = pol_note;
                    model.company_code = company_code;
                    model.worker_code = worker_code;
                    model.created_by = modified_by;

                    listPol.Add(model);
                }

                bool blnResult = false;
                if (listPol.Count > 0)
                {
                    cls_ctTREmppolatt objPol = new cls_ctTREmppolatt();
                    blnResult = objPol.insert(listPol);

                    if (blnResult)
                    {
                        if (pol_type.Equals("LV"))
                        {
                            string year = DateTime.Now.Year.ToString();

                            foreach (cls_TREmppolatt pol in listPol)
                            {
                                cls_srvProcessTime srvTime = new cls_srvProcessTime();
                                srvTime.doSetEmpleaveacc(year, pol.company_code, pol.worker_code, modified_by);
                            }

                        }



                        output["result"] = "1";
                        output["result_text"] = "";
                    }
                    else
                    {
                        output["result"] = "0";
                        output["result_text"] = objPol.getMessage();
                    }
                }
                else
                {
                    output["result"] = "0";
                    output["result_text"] = "Data not found";
                }
            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTREmppolatt(InputTREmppolatt input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTREmppolatt objPol = new cls_ctTREmppolatt();
                bool blnResult = objPol.delete(input.company_code, input.worker_code, input.emppolatt_policy_type);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objPol.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region Timecard
        public string getTRTimecardList(string com, string emp, string fromdate, string todate)
        {
            JObject output = new JObject();

            DateTime datefrom = Convert.ToDateTime(fromdate);
            DateTime dateto = Convert.ToDateTime(todate);

            cls_ctTRTimecard objTimecard = new cls_ctTRTimecard();
            List<cls_TRTimecard> listTimecard = objTimecard.getDataByFillter(com, emp, datefrom, dateto);
            JArray array = new JArray();

            if (listTimecard.Count > 0)
            {


                int index = 1;

                int intRow = 1;

                foreach (cls_TRTimecard model in listTimecard)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("worker_code", model.worker_code);
                    json.Add("shift_code", model.shift_code);
                    json.Add("timecard_workdate", model.timecard_workdate);
                    json.Add("timecard_daytype", model.timecard_daytype);
                    json.Add("timecard_color", model.timecard_color);
                    json.Add("timecard_lock", model.timecard_lock);

                    json.Add("timecard_ch1", model.timecard_ch1);
                    json.Add("timecard_ch2", model.timecard_ch2);
                    json.Add("timecard_ch3", model.timecard_ch3);
                    json.Add("timecard_ch4", model.timecard_ch4);
                    json.Add("timecard_ch5", model.timecard_ch5);
                    json.Add("timecard_ch6", model.timecard_ch6);
                    json.Add("timecard_ch7", model.timecard_ch7);
                    json.Add("timecard_ch8", model.timecard_ch8);
                    json.Add("timecard_ch9", model.timecard_ch9);
                    json.Add("timecard_ch10", model.timecard_ch10);

                    //-- Time in
                    if (!model.timecard_ch1.ToString("HH:mm").Equals("00:00"))
                    {
                        json.Add("timecard_in", model.timecard_ch1.ToString("dd/MM/yyyy HH:mm"));
                    }
                    else if (!model.timecard_ch3.ToString("HH:mm").Equals("00:00"))
                    {
                        json.Add("timecard_in", model.timecard_ch3.ToString("dd/MM/yyyy HH:mm"));
                    }
                    else
                    {
                        json.Add("timecard_in", "-");
                    }

                    //-- Time out
                    if (!model.timecard_ch10.ToString("HH:mm").Equals("00:00"))
                    {
                        json.Add("timecard_out", model.timecard_ch10.ToString("dd/MM/yyyy HH:mm"));
                    }
                    else if (!model.timecard_ch8.ToString("HH:mm").Equals("00:00"))
                    {
                        json.Add("timecard_out", model.timecard_ch8.ToString("dd/MM/yyyy HH:mm"));
                    }
                    else if (!model.timecard_ch4.ToString("HH:mm").Equals("00:00"))
                    {
                        json.Add("timecard_out", model.timecard_ch4.ToString("dd/MM/yyyy HH:mm"));
                    }
                    else
                    {
                        json.Add("timecard_out", "-");
                    }


                    json.Add("timecard_before_min", model.timecard_before_min);
                    json.Add("timecard_work1_min", model.timecard_work1_min);
                    json.Add("timecard_work2_min", model.timecard_work2_min);
                    json.Add("timecard_break_min", model.timecard_break_min);
                    json.Add("timecard_after_min", model.timecard_after_min);
                    json.Add("timecard_late_min", model.timecard_late_min);

                    json.Add("timecard_before_min_app", model.timecard_before_min_app);
                    json.Add("timecard_work1_min_app", model.timecard_work1_min_app);
                    json.Add("timecard_work2_min_app", model.timecard_work2_min_app);
                    json.Add("timecard_break_min_app", model.timecard_break_min_app);
                    json.Add("timecard_after_min_app", model.timecard_after_min_app);
                    json.Add("timecard_late_min_app", model.timecard_late_min_app);

                    int hrs = (model.timecard_work1_min_app + model.timecard_work2_min_app) / 60;
                    int min = (model.timecard_work1_min_app + model.timecard_work2_min_app) - (hrs * 60);
                    json.Add("work_hrs", hrs.ToString().PadLeft(2, '0') + ":" + min.ToString().PadLeft(2, '0'));

                    hrs = (model.timecard_before_min_app + model.timecard_after_min_app) / 60;
                    min = (model.timecard_before_min_app + model.timecard_after_min_app) - (hrs * 60);
                    json.Add("ot_hrs", hrs.ToString().PadLeft(2, '0') + ":" + min.ToString().PadLeft(2, '0'));

                    hrs = (model.timecard_late_min_app) / 60;
                    min = (model.timecard_late_min_app) - (hrs * 60);
                    json.Add("late_hrs", hrs.ToString().PadLeft(2, '0') + ":" + min.ToString().PadLeft(2, '0'));


                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("change", false);

                    json.Add("index", index);

                    json.Add("row", intRow);

                    switch (model.timecard_workdate.DayOfWeek)
                    {
                        case DayOfWeek.Sunday: json.Add("col", 1); break;
                        case DayOfWeek.Monday: json.Add("col", 2); break;
                        case DayOfWeek.Tuesday: json.Add("col", 3); break;
                        case DayOfWeek.Wednesday: json.Add("col", 4); break;
                        case DayOfWeek.Thursday: json.Add("col", 5); break;
                        case DayOfWeek.Friday: json.Add("col", 6); break;
                        case DayOfWeek.Saturday:
                            json.Add("col", 7);
                            intRow++;
                            break;
                    }

                    index++;


                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }

        public string doManageTRTimecard(InputTRTimecard input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRTimecard objTime = new cls_ctTRTimecard();
                cls_TRTimecard model = new cls_TRTimecard();

                model.company_code = input.company_code;
                model.worker_code = input.worker_code;

                model.timecard_workdate = Convert.ToDateTime(input.timecard_workdate);
                model.timecard_daytype = input.timecard_daytype;
                model.shift_code = input.shift_code;
                model.timecard_color = input.timecard_color;

                model.timecard_lock = input.timecard_lock;

                if (input.timecard_ch1.Equals("") || input.timecard_ch2.Equals(""))
                {
                    model.before_scan = false;
                }
                else
                {
                    model.before_scan = true;
                    model.timecard_ch1 = this.doConvertDate(input.timecard_ch1);
                    model.timecard_ch2 = this.doConvertDate(input.timecard_ch2);
                }

                if (input.timecard_ch3.Equals("") || input.timecard_ch4.Equals(""))
                {
                    model.work1_scan = false;
                }
                else
                {
                    model.work1_scan = true;
                    model.timecard_ch3 = this.doConvertDate(input.timecard_ch3);
                    model.timecard_ch4 = this.doConvertDate(input.timecard_ch4);
                }

                if (input.timecard_ch7.Equals("") || input.timecard_ch8.Equals(""))
                {
                    model.work2_scan = false;
                }
                else
                {
                    model.work2_scan = true;
                    model.timecard_ch7 = this.doConvertDate(input.timecard_ch7);
                    model.timecard_ch8 = this.doConvertDate(input.timecard_ch8);
                }

                if (input.timecard_ch5.Equals("") || input.timecard_ch6.Equals(""))
                {
                    model.break_scan = false;
                }
                else
                {
                    model.break_scan = true;
                    model.timecard_ch5 = this.doConvertDate(input.timecard_ch5);
                    model.timecard_ch6 = this.doConvertDate(input.timecard_ch6);
                }

                if (input.timecard_ch9.Equals("") || input.timecard_ch10.Equals(""))
                {
                    model.after_scan = false;
                }
                else
                {
                    model.after_scan = true;
                    model.timecard_ch9 = this.doConvertDate(input.timecard_ch9);
                    model.timecard_ch10 = this.doConvertDate(input.timecard_ch10);
                }


                model.timecard_before_min = input.timecard_before_min;
                model.timecard_work1_min = input.timecard_work1_min;
                model.timecard_work2_min = input.timecard_work2_min;
                model.timecard_break_min = input.timecard_break_min;
                model.timecard_after_min = input.timecard_after_min;

                model.timecard_late_min = input.timecard_late_min;

                model.timecard_before_min_app = input.timecard_before_min_app;
                model.timecard_work1_min_app = input.timecard_work1_min_app;
                model.timecard_work2_min_app = input.timecard_work2_min_app;
                model.timecard_break_min_app = input.timecard_break_min_app;
                model.timecard_after_min_app = input.timecard_after_min_app;

                model.timecard_late_min_app = input.timecard_late_min_app;

                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objTime.update(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objTime.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }

        #endregion

        #region Timeinput
        public string getTRTimeinputList(string com, string emp, string fromdate, string todate)
        {
            JObject output = new JObject();

            DateTime datefrom = Convert.ToDateTime(fromdate);
            DateTime dateto = Convert.ToDateTime(todate);

            cls_ctTRTimeinput objTimeinput = new cls_ctTRTimeinput();
            List<cls_TRTimeinput> listTimeinput = objTimeinput.getDataByFillter(com, emp, datefrom, dateto, false);
            JArray array = new JArray();

            if (listTimeinput.Count > 0)
            {
                int index = 1;

                foreach (cls_TRTimeinput model in listTimeinput)
                {
                    JObject json = new JObject();

                    json.Add("timeinput_card", model.timeinput_card);
                    json.Add("timeinput_date", model.timeinput_date);
                    json.Add("timeinput_hhmm", model.timeinput_hhmm);
                    json.Add("timeinput_terminal", model.timeinput_terminal);
                    json.Add("timeinput_function", model.timeinput_function);
                    json.Add("timeinput_compare", model.timeinput_compare);

                    json.Add("index", index);

                    index++;
                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }

        public string doManageTRTimeinput(InputTRTimeinput input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRTimeinput objTRTime = new cls_ctTRTimeinput();
                cls_TRTimeinput model = new cls_TRTimeinput();

                model.timeinput_card = input.timeinput_card;
               
                model.timeinput_date = Convert.ToDateTime(input.timeinput_date);
                model.timeinput_hhmm = input.timeinput_hhmm;
                model.timeinput_terminal = input.timeinput_terminal;
                model.timeinput_function = input.timeinput_function;
                model.timeinput_compare = "N";               

                bool blnResult = objTRTime.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objTRTime.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTRTimeinput(InputTRTimeinput input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRTimeinput objTRTime = new cls_ctTRTimeinput();

                bool blnResult = objTRTime.delete(Convert.ToDateTime(input.timeinput_date), input.timeinput_card, input.timeinput_hhmm);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objTRTime.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }

        #endregion

        #region Attwageday
        public string getTRAttwagedayList(string language, string com, string fromdate, string todate, string emp)
        {
            JObject output = new JObject();

            cls_ctTRAttwageday objAttwageday = new cls_ctTRAttwageday();
            List<cls_TRAttwageday> listAttwageday = objAttwageday.getDataByFillter(language, com, Convert.ToDateTime(fromdate), Convert.ToDateTime(todate), emp);
            JArray array = new JArray();

            if (listAttwageday.Count > 0)
            {
                int index = 1;

                foreach (cls_TRAttwageday model in listAttwageday)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("worker_code", model.worker_code);

                    json.Add("wageday_date", model.wageday_date);
                    json.Add("wageday_daytype", model.wageday_daytype);

                    json.Add("wageday_wagemoney", model.wageday_wagemoney);
                    json.Add("wageday_wagehhmm", model.wageday_wagehhmm);

                    json.Add("wageday_latemoney", model.wageday_latemoney);
                    json.Add("wageday_latehhmm", model.wageday_latehhmm);

                    json.Add("wageday_leavemoney", model.wageday_leavemoney);
                    json.Add("wageday_leavehhmm", model.wageday_leavehhmm);

                    json.Add("wageday_absentmoney", model.wageday_absentmoney);
                    json.Add("wageday_absenthhmm", model.wageday_absenthhmm);

                    json.Add("wageday_ot1money", model.wageday_ot1money);
                    json.Add("wageday_ot1hhmm", model.wageday_ot1hhmm);

                    json.Add("wageday_ot15money", model.wageday_ot15money);
                    json.Add("wageday_ot15hhmm", model.wageday_ot15hhmm);

                    json.Add("wageday_ot2money", model.wageday_ot2money);
                    json.Add("wageday_ot2hhmm", model.wageday_ot2hhmm);

                    json.Add("wageday_ot3money", model.wageday_ot3money);
                    json.Add("wageday_ot3hhmm", model.wageday_ot3hhmm);

                    json.Add("wageday_otsummoney", model.wageday_ot1money + model.wageday_ot15money + model.wageday_ot2money + model.wageday_ot3money);

                    json.Add("wageday_allowance", model.wageday_allowance);

                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);
                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageTRAttwageday(InputTRAttwageday input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRAttwageday objAttwageday = new cls_ctTRAttwageday();
                cls_TRAttwageday model = new cls_TRAttwageday();

                model.company_code = input.company_code;
                model.worker_code = input.worker_code;
                model.wageday_date = Convert.ToDateTime(input.wageday_date);
                model.wageday_daytype = input.wageday_daytype;

                model.wageday_wagemoney = input.wageday_wagemoney;
                model.wageday_wagehhmm = input.wageday_wagehhmm;

                model.wageday_latemoney = input.wageday_latemoney;
                model.wageday_latehhmm = input.wageday_latehhmm;

                model.wageday_leavemoney = input.wageday_leavemoney;
                model.wageday_leavehhmm = input.wageday_leavehhmm;

                model.wageday_absentmoney = input.wageday_absentmoney;
                model.wageday_absenthhmm = input.wageday_absenthhmm;

                model.wageday_ot1money = input.wageday_ot1money;
                model.wageday_ot1hhmm = input.wageday_ot1hhmm;

                model.wageday_ot15money = input.wageday_ot15money;
                model.wageday_ot15hhmm = input.wageday_ot15hhmm;

                model.wageday_ot2money = input.wageday_ot2money;
                model.wageday_ot2hhmm = input.wageday_ot2hhmm;

                model.wageday_ot3money = input.wageday_ot3money;
                model.wageday_ot3hhmm = input.wageday_ot3hhmm;

                model.wageday_allowance = input.wageday_allowance;

                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objAttwageday.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objAttwageday.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTRAttwageday(InputTRAttwageday input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRAttwageday objAttwageday = new cls_ctTRAttwageday();

                bool blnResult = objAttwageday.delete(input.company_code, input.worker_code, Convert.ToDateTime(input.wageday_date));

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objAttwageday.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region MTTimeimpformat
        public string getMTTimeimpformatList(string com)
        {
            JObject output = new JObject();

            cls_ctMTTimeimpformat objMTTimeimpformat = new cls_ctMTTimeimpformat();
            List<cls_MTTimeimpformat> listMTTimeimpformat = objMTTimeimpformat.getDataByFillter(com);

            JArray array = new JArray();

            if (listMTTimeimpformat.Count > 0)
            {
                int index = 1;

                foreach (cls_MTTimeimpformat model in listMTTimeimpformat)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);

                    json.Add("date_format", model.date_format);

                    json.Add("card_start", model.card_start);
                    json.Add("card_lenght", model.card_lenght);

                    json.Add("date_start", model.date_start);
                    json.Add("date_lenght", model.date_lenght);

                    json.Add("hours_start", model.hours_start);
                    json.Add("hours_lenght", model.hours_lenght);

                    json.Add("minute_start", model.minute_start);
                    json.Add("minute_lenght", model.minute_lenght);

                    json.Add("function_start", model.function_start);
                    json.Add("function_lenght", model.function_lenght);

                    json.Add("machine_start", model.machine_start);
                    json.Add("machine_lenght", model.machine_lenght);

                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageMTTimeimpformat(InputMTTimeimpformat input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTTimeimpformat objMTTimeimpformat = new cls_ctMTTimeimpformat();
                cls_MTTimeimpformat model = new cls_MTTimeimpformat();

                model.company_code = input.company_code;
                model.date_format = input.date_format;

                model.card_start = input.card_start;
                model.card_lenght = input.card_lenght;

                model.date_start = input.date_start;
                model.date_lenght = input.date_lenght;

                model.hours_start = input.hours_start;
                model.hours_lenght = input.hours_lenght;

                model.minute_start = input.minute_start;
                model.minute_lenght = input.minute_lenght;

                model.function_start = input.function_start;
                model.function_lenght = input.function_lenght;

                model.machine_start = input.machine_start;
                model.machine_lenght = input.machine_lenght;
                
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objMTTimeimpformat.insert(model);

                if (blnResult)
                {                    
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objMTTimeimpformat.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTTimeimpformat(InputMTTimeimpformat input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTTimeimpformat objMTTimeimpformat = new cls_ctMTTimeimpformat();

                bool blnResult = objMTTimeimpformat.delete(input.company_code);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objMTTimeimpformat.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }

        #endregion

        #region TRTimedoc
        public string getTRTimedocList(string com, string emp, string workdate, string type)
        {
            JObject output = new JObject();
            
            DateTime datework = Convert.ToDateTime(workdate);

            cls_ctTRTimedoc objTRTimedoc = new cls_ctTRTimedoc();
            List<cls_TRTimedoc> listTRTimedoc = objTRTimedoc.getDataByFillter(com, emp, datework, type);

            JArray array = new JArray();

            if (listTRTimedoc.Count > 0)
            {
                int index = 1;

                foreach (cls_TRTimedoc model in listTRTimedoc)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("worker_code", model.worker_code);
                    json.Add("timedoc_workdate", model.timedoc_workdate);
                    json.Add("timedoc_doctype", model.timedoc_doctype);

                    json.Add("timedoc_docno", model.timedoc_docno);
                    json.Add("timedoc_value1", model.timedoc_value1);
                    json.Add("timedoc_value2", model.timedoc_value2);
                    json.Add("timedoc_value3", model.timedoc_value3);
                    json.Add("timedoc_value4", model.timedoc_value4);

                    json.Add("timedoc_reasoncode", model.timedoc_reasoncode);
                    json.Add("timedoc_note", model.timedoc_note);
                    
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageTRTimedoc(InputTRTimedoc input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRTimedoc objTRTimedoc = new cls_ctTRTimedoc();
                cls_TRTimedoc model = new cls_TRTimedoc();

                model.company_code = input.company_code;
                model.worker_code = input.worker_code;
                model.timedoc_workdate = input.timedoc_workdate;
                model.timedoc_doctype = input.timedoc_doctype;

                model.timedoc_docno = input.timedoc_docno;
                model.timedoc_value1 = input.timedoc_value1;
                model.timedoc_value2 = input.timedoc_value2;
                model.timedoc_value3 = input.timedoc_value3;
                model.timedoc_value4 = input.timedoc_value4;

                model.timedoc_reasoncode = input.timedoc_reasoncode;
                model.timedoc_note = input.timedoc_note;

                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objTRTimedoc.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objTRTimedoc.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTRTimedoc(InputTRTimedoc input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRTimedoc objTRTimedoc = new cls_ctTRTimedoc();

                bool blnResult = objTRTimedoc.delete(input.company_code, input.worker_code, input.timedoc_workdate, input.timedoc_doctype);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objTRTimedoc.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }

        #endregion

        #region TREmpleaveacc
        public string getTREmpleaveaccList(string language, string com, string emp, string year)
        {
            JObject output = new JObject();


            cls_ctTREmpleaveacc controller = new cls_ctTREmpleaveacc();
            List<cls_TREmpleaveacc> list = controller.getDataByFillter(language, com, emp, year);

            JArray array = new JArray();

            if (list.Count > 0)
            {
                int index = 1;

                foreach (cls_TREmpleaveacc model in list)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("worker_code", model.worker_code);
                    json.Add("leave_code", model.leave_code);
                    json.Add("year_code", model.year_code);

                    json.Add("empleaveacc_bf", model.empleaveacc_bf);
                    json.Add("empleaveacc_annual", model.empleaveacc_annual);
                    json.Add("empleaveacc_used", model.empleaveacc_used);
                    json.Add("empleaveacc_remain", model.empleaveacc_remain);

                    json.Add("worker_detail", model.worker_detail);
                    json.Add("leave_detail", model.leave_detail);
                    
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageTREmpleaveacc(InputTREmpleaveacc input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTREmpleaveacc controller = new cls_ctTREmpleaveacc();
                cls_TREmpleaveacc model = new cls_TREmpleaveacc();

                model.company_code = input.company_code;
                model.worker_code = input.worker_code;
                model.year_code = input.year_code;
                model.leave_code = input.leave_code;
                
                model.empleaveacc_bf = input.empleaveacc_bf;
                model.empleaveacc_annual = input.empleaveacc_annual;
                model.empleaveacc_used = input.empleaveacc_used;
                model.empleaveacc_remain = input.empleaveacc_remain;
                
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = controller.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = controller.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTREmpleaveacc(InputTREmpleaveacc input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTREmpleaveacc controller = new cls_ctTREmpleaveacc();

                bool blnResult = controller.delete(input.company_code, input.worker_code, input.year_code, input.leave_code);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = controller.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region TRTimeleave

        public string doManageTRTimeleaveattachfile(InputTRTimeattchfile input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRTimeleaveattachfile objTRTimeleaveattch = new cls_ctTRTimeleaveattachfile();
                cls_TRTimeleaveattachfile model = new cls_TRTimeleaveattachfile();

                model.COMPANY_CODE = input.company_code;
                model.TIMELEAVE_DOC = input.timeleave_doc;
                model.FILE_NO = input.file_no;
                model.FILE_NAME = input.file_name;
                model.FILE_PATH = input.file_path;
               
                model.CREATED_DATE = DateTime.Now;
                model.CREATED_BY = input.modified_by;
                model.MODIFIED_DATE = DateTime.Now;
                model.MODIFIED_BY = input.modified_by;

                bool blnResult = objTRTimeleaveattch.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objTRTimeleaveattch.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);

        }

        public string doDeleteTRTimeleaveattachfile(InputTRTimeattchfile input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRTimeleaveattachfile objTRTimeleaveattch = new cls_ctTRTimeleaveattachfile();

                bool blnResult = objTRTimeleaveattch.delete(input.file_no, input.company_code);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objTRTimeleaveattch.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        public string getTRTimeleaveattachfileList(string company_code, string timeleave_doc)
        {
            JObject output = new JObject();
            cls_ctTRTimeleaveattachfile objTRTimeleaveattch = new cls_ctTRTimeleaveattachfile();
            List<cls_TRTimeleaveattachfile> listTRTimeleaveattch = objTRTimeleaveattch.getData(timeleave_doc,company_code);

            JArray array = new JArray();

            if (listTRTimeleaveattch.Count > 0)
            {
                int index = 1;

                foreach (cls_TRTimeleaveattachfile model in listTRTimeleaveattch)
                {
                    JObject json = new JObject();

                    json.Add("COMPANY_CODE", model.COMPANY_CODE);
                    json.Add("TIMELEAVE_DOC", model.TIMELEAVE_DOC);
                    json.Add("FILE_NO", model.FILE_NO);
                    json.Add("FILE_NAME", model.FILE_NAME);
                    json.Add("FILE_PATH", model.FILE_PATH);
                    json.Add("CREATED_DATE", model.CREATED_DATE);
                    json.Add("CREATED_BY", model.CREATED_BY);
                    json.Add("MODIFIED_DATE", model.MODIFIED_DATE);
                    json.Add("MODIFIED_BY", model.MODIFIED_BY);
              
                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }

        public string getTRTimeleaveList(string language, string com, string emp, string fromdate, string todate)
        {
            JObject output = new JObject();

            DateTime datefrom = Convert.ToDateTime(fromdate);
            DateTime dateto = Convert.ToDateTime(todate);

            cls_ctTRTimeleave objTRTimeleave = new cls_ctTRTimeleave();
            List<cls_TRTimeleave> listTRTimeleave = objTRTimeleave.getDataByFillter(language, com, emp, datefrom, dateto);

            JArray array = new JArray();

            if (listTRTimeleave.Count > 0)
            {
                int index = 1;

                foreach (cls_TRTimeleave model in listTRTimeleave)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("worker_code", model.worker_code);

                    json.Add("worker_detail", model.worker_detail);
                    json.Add("leave_detail", model.leave_detail);

                    json.Add("timeleave_id", model.timeleave_id);
                    json.Add("timeleave_doc", model.timeleave_doc);

                    json.Add("timeleave_fromdate", model.timeleave_fromdate);
                    json.Add("timeleave_todate", model.timeleave_todate);

                    json.Add("timeleave_type", model.timeleave_type);
                    json.Add("timeleave_min", model.timeleave_min);

                    json.Add("timeleave_actualday", model.timeleave_actualday);
                    json.Add("timeleave_incholiday", model.timeleave_incholiday);
                    json.Add("timeleave_deduct", model.timeleave_deduct);

                    json.Add("timeleave_note", model.timeleave_note);
                    json.Add("leave_code", model.leave_code);
                    json.Add("reason_code", model.reason_code);                   

                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageTRTimeleave(InputTRTimeleave input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRTimeleave objTRTimeleave = new cls_ctTRTimeleave();
                cls_TRTimeleave model = new cls_TRTimeleave();

                model.company_code = input.company_code;
                model.worker_code = input.worker_code;
                model.timeleave_id = input.timeleave_id;
                model.timeleave_doc = input.timeleave_doc;

                model.timeleave_fromdate = Convert.ToDateTime(input.timeleave_fromdate);
                model.timeleave_todate = Convert.ToDateTime(input.timeleave_todate);

                model.timeleave_type = input.timeleave_type;
                model.timeleave_min = input.timeleave_min;

                model.timeleave_actualday = input.timeleave_actualday;
                model.timeleave_incholiday = input.timeleave_incholiday;
                model.timeleave_deduct = input.timeleave_deduct;

                model.timeleave_note = input.timeleave_note;
                model.leave_code = input.leave_code;
                model.reason_code = input.reason_code;
                
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objTRTimeleave.insert(model);

                if (blnResult)
                {

                    cls_srvProcessTime srv_time = new cls_srvProcessTime();
                    srv_time.doCalleaveacc(model.timeleave_fromdate.Year.ToString(), model.company_code, model.worker_code, model.modified_by);

                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objTRTimeleave.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTRTimeleave(InputTRTimeleave input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRTimeleave objTRTime = new cls_ctTRTimeleave();

                cls_TRTimeleave model = objTRTime.getDataByID(input.timeleave_id.ToString());
                
                bool blnResult = objTRTime.delete(input.timeleave_id.ToString());

                if (blnResult)
                {
                    cls_srvProcessTime srv_time = new cls_srvProcessTime();
                    srv_time.doCalleaveacc(model.timeleave_fromdate.Year.ToString(), model.company_code, model.worker_code, model.modified_by);

                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objTRTime.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region TRTimeot
        public string getTRTimeotList(string language, string com, string emp, string fromdate, string todate)
        {
            JObject output = new JObject();

            DateTime datefrom = Convert.ToDateTime(fromdate);
            DateTime dateto = Convert.ToDateTime(todate);

            cls_ctTRTimeot objTRTime = new cls_ctTRTimeot();
            List<cls_TRTimeot> listTRTime = objTRTime.getDataByFillter(language, com, emp, datefrom, dateto);

            JArray array = new JArray();

            if (listTRTime.Count > 0)
            {
                int index = 1;

                foreach (cls_TRTimeot model in listTRTime)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("worker_code", model.worker_code);

                    json.Add("worker_detail", model.worker_detail);

                    json.Add("timeot_id", model.timeot_id);
                    json.Add("timeot_doc", model.timeot_doc);

                    json.Add("timeot_workdate", model.timeot_workdate);

                    json.Add("timeot_beforemin", model.timeot_beforemin);
                    json.Add("timeot_normalmin", model.timeot_normalmin);
                    json.Add("timeot_aftermin", model.timeot_aftermin);

                    int hrs = (model.timeot_beforemin) / 60;
                    int min = (model.timeot_beforemin) - (hrs * 60);
                    json.Add("timeot_beforemin_hrs", hrs.ToString().PadLeft(2, '0') + ":" + min.ToString().PadLeft(2, '0'));

                    hrs = (model.timeot_normalmin) / 60;
                    min = (model.timeot_normalmin) - (hrs * 60);
                    json.Add("timeot_normalmin_hrs", hrs.ToString().PadLeft(2, '0') + ":" + min.ToString().PadLeft(2, '0'));

                    hrs = (model.timeot_aftermin) / 60;
                    min = (model.timeot_aftermin) - (hrs * 60);
                    json.Add("timeot_aftermin_hrs", hrs.ToString().PadLeft(2, '0') + ":" + min.ToString().PadLeft(2, '0'));
                                        

                    json.Add("timeot_note", model.timeot_note);
                    json.Add("location_code", model.location_code);
                    json.Add("reason_code", model.reason_code);

                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageTRTimeot(InputTRTimeot input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRTimeot objTRTime = new cls_ctTRTimeot();
                cls_TRTimeot model = new cls_TRTimeot();

                model.company_code = input.company_code;
                model.worker_code = input.worker_code;
                model.timeot_id = input.timeot_id;
                model.timeot_doc = input.timeot_doc;
                                
                model.timeot_workdate = Convert.ToDateTime(input.timeot_workdate);

                model.timeot_beforemin = input.timeot_beforemin;
                model.timeot_normalmin = input.timeot_normalmin;
                model.timeot_aftermin = input.timeot_aftermin;

                model.timeot_note = input.timeot_note;
                model.location_code = input.location_code;
                model.reason_code = input.reason_code;

                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objTRTime.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objTRTime.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTRTimeot(InputTRTimeot input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRTimeot objTRTime = new cls_ctTRTimeot();

                bool blnResult = objTRTime.delete(input.timeot_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objTRTime.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region TRTimeonsite
        public string getTRTimeonsiteList(string language, string com, string emp, string fromdate, string todate)
        {
            JObject output = new JObject();

            DateTime datefrom = Convert.ToDateTime(fromdate);
            DateTime dateto = Convert.ToDateTime(todate);

            cls_ctTRTimeonsite objTRTime = new cls_ctTRTimeonsite();
            List<cls_TRTimeonsite> listTRTime = objTRTime.getDataByFillter(language, com, emp, datefrom, dateto);

            JArray array = new JArray();

            if (listTRTime.Count > 0)
            {
                int index = 1;

                foreach (cls_TRTimeonsite model in listTRTime)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("worker_code", model.worker_code);

                    json.Add("worker_detail", model.worker_detail);
                    json.Add("reason_detail", model.reason_detail);
                    json.Add("location_detail", model.location_detail);

                    json.Add("timeonsite_id", model.timeonsite_id);
                    json.Add("timeonsite_doc", model.timeonsite_doc);

                    json.Add("timeonsite_workdate", model.timeonsite_workdate);
                    json.Add("timeonsite_in", model.timeonsite_in);

                    json.Add("timeonsite_out", model.timeonsite_out);
                    json.Add("timeonsite_note", model.timeonsite_note);

                    json.Add("location_code", model.location_code);
                    json.Add("reason_code", model.reason_code);
                    
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageTRTimeonsite(InputTRTimeonsite input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRTimeonsite objTRTime = new cls_ctTRTimeonsite();
                cls_TRTimeonsite model = new cls_TRTimeonsite();

                model.company_code = input.company_code;
                model.worker_code = input.worker_code;

                model.timeonsite_id = input.timeonsite_id;
                model.timeonsite_doc = input.timeonsite_doc;

                model.timeonsite_workdate = Convert.ToDateTime(input.timeonsite_workdate);               

                model.timeonsite_in = input.timeonsite_in;
                model.timeonsite_out = input.timeonsite_out;

                model.timeonsite_note = input.timeonsite_note;

                model.location_code = input.location_code;
                model.reason_code = input.reason_code;
            
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objTRTime.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objTRTime.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTRTimeonsite(InputTRTimeonsite input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRTimeonsite objTRTime = new cls_ctTRTimeonsite();

                bool blnResult = objTRTime.delete(input.timeonsite_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objTRTime.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region TRTimeshift
        public string getTRTimeshiftList(string language, string com, string emp, string fromdate, string todate)
        {
            JObject output = new JObject();

            DateTime datefrom = Convert.ToDateTime(fromdate);
            DateTime dateto = Convert.ToDateTime(todate);

            cls_ctTRTimeshift objTRTime = new cls_ctTRTimeshift();
            List<cls_TRTimeshift> listTRTime = objTRTime.getDataByFillter(language, com, emp, datefrom, dateto);

            JArray array = new JArray();

            if (listTRTime.Count > 0)
            {
                int index = 1;

                foreach (cls_TRTimeshift model in listTRTime)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("worker_code", model.worker_code);

                    json.Add("worker_detail", model.worker_detail);
                    json.Add("reason_detail", model.reason_detail);
                    json.Add("shift_detail", model.shift_detail);

                    json.Add("timeshift_id", model.timeshift_id);
                    json.Add("timeshift_doc", model.timeshift_doc);
                    json.Add("timeshift_workdate", model.timeshift_workdate);
                    json.Add("timeshift_old", model.timeshift_old);
                    json.Add("timeshift_new", model.timeshift_new);
                    json.Add("timeshift_note", model.timeshift_note);

                    json.Add("reason_code", model.reason_code);
                   
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageTRTimeshift(InputTRTimeshift input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRTimeshift objTRTime = new cls_ctTRTimeshift();
                cls_TRTimeshift model = new cls_TRTimeshift();

                model.company_code = input.company_code;
                model.worker_code = input.worker_code;

                model.timeshift_id = input.timeshift_id;
                model.timeshift_doc = input.timeshift_doc;

                model.timeshift_workdate = Convert.ToDateTime(input.timeshift_workdate);     
             
                model.timeshift_old = input.timeshift_old;
                model.timeshift_new = input.timeshift_new;

                model.timeshift_note = input.timeshift_note;

                model.reason_code = input.reason_code;

                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objTRTime.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objTRTime.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTRTimeshift(InputTRTimeshift input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRTimeshift objTRTime = new cls_ctTRTimeshift();

                cls_TRTimeshift model = objTRTime.getDataByID(input.timeshift_id.ToString());

                bool blnResult = objTRTime.delete(input.timeshift_id.ToString());

                if (blnResult)
                {
                    cls_ctTRTimecard objTRTimecard = new cls_ctTRTimecard();
                    List<cls_TRTimecard> list_timecard = objTRTimecard.getDataByFillter(model.company_code, model.worker_code, model.timeshift_workdate.Date, model.timeshift_workdate.Date);

                    if (list_timecard.Count > 0)
                    {
                        cls_TRTimecard timecard = list_timecard[0];
                        timecard.shift_code = model.timeshift_old;

                        objTRTimecard.update(timecard);
                    }


                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objTRTime.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region TRTimedaytype
        public string getTRTimedaytypeList(string language, string com, string emp, string fromdate, string todate)
        {
            JObject output = new JObject();

            DateTime datefrom = Convert.ToDateTime(fromdate);
            DateTime dateto = Convert.ToDateTime(todate);

            cls_ctTRTimedaytype objTRTime = new cls_ctTRTimedaytype();
            List<cls_TRTimedaytype> listTRTime = objTRTime.getDataByFillter(language, com, emp, datefrom, dateto);

            JArray array = new JArray();

            if (listTRTime.Count > 0)
            {
                int index = 1;

                foreach (cls_TRTimedaytype model in listTRTime)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("worker_code", model.worker_code);

                    json.Add("worker_detail", model.worker_detail);
                    json.Add("reason_detail", model.reason_detail);


                    json.Add("timedaytype_id", model.timedaytype_id);
                    json.Add("timedaytype_doc", model.timedaytype_doc);
                    json.Add("timedaytype_workdate", model.timedaytype_workdate);
                    json.Add("timedaytype_old", model.timedaytype_old);
                    json.Add("timedaytype_new", model.timedaytype_new);
                    json.Add("timedaytype_note", model.timedaytype_note);

                    json.Add("reason_code", model.reason_code);

                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageTRTimedaytype(InputTRTimedaytype input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRTimedaytype objTRTime = new cls_ctTRTimedaytype();
                cls_TRTimedaytype model = new cls_TRTimedaytype();

                model.company_code = input.company_code;
                model.worker_code = input.worker_code;

                model.timedaytype_id = input.timedaytype_id;
                model.timedaytype_doc = input.timedaytype_doc;

                model.timedaytype_workdate = Convert.ToDateTime(input.timedaytype_workdate);

                model.timedaytype_old = input.timedaytype_old;
                model.timedaytype_new = input.timedaytype_new;

                model.timedaytype_note = input.timedaytype_note;

                model.reason_code = input.reason_code;

                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objTRTime.insert(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objTRTime.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteTRTimedaytype(InputTRTimedaytype input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTRTimedaytype objTRTime = new cls_ctTRTimedaytype();

                cls_TRTimedaytype model = objTRTime.getDataByID(input.timedaytype_id.ToString());

                bool blnResult = objTRTime.delete(input.timedaytype_id.ToString());

                if (blnResult)
                {
                    cls_ctTRTimecard objTRTimecard = new cls_ctTRTimecard();
                    List<cls_TRTimecard> list_timecard = objTRTimecard.getDataByFillter(model.company_code, model.worker_code, model.timedaytype_workdate.Date, model.timedaytype_workdate.Date);

                    if (list_timecard.Count > 0)
                    {
                        cls_TRTimecard timecard = list_timecard[0];
                        timecard.timecard_daytype = model.timedaytype_old;

                        objTRTimecard.update(timecard);
                    }

                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objTRTime.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region MTPlanholiday
        public string getMTPlanholidayList(string com, string year)
        {
            JObject output = new JObject();

            cls_ctMTPlanholiday objPlanholiday = new cls_ctMTPlanholiday();
            List<cls_MTPlanholiday> listPlanholiday = objPlanholiday.getDataByFillter(com, "", "", year);
            JArray array = new JArray();

            if (listPlanholiday.Count > 0)
            {

                int index = 1;

                foreach (cls_MTPlanholiday model in listPlanholiday)
                {
                    JObject json = new JObject();

                    json.Add("planholiday_id", model.planholiday_id);
                    json.Add("planholiday_code", model.planholiday_code);
                    json.Add("planholiday_name_th", model.planholiday_name_th);
                    json.Add("planholiday_name_en", model.planholiday_name_en);
                    json.Add("company_code", model.company_code);
                    json.Add("year_code", model.year_code);
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string getTRHolidayList(string com, string plan)
        {
            JObject output = new JObject();

            cls_ctTRHoliday objHoliday = new cls_ctTRHoliday();
            List<cls_TRHoliday> listHoliday = objHoliday.getDataByFillter(com, plan);
            JArray array = new JArray();

            if (listHoliday.Count > 0)
            {

                int index = 1;

                foreach (cls_TRHoliday model in listHoliday)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("planholiday_code", model.planholiday_code);
                    json.Add("holiday_date", model.holiday_date);
                    json.Add("holiday_name_th", model.holiday_name_th);
                    json.Add("holiday_name_en", model.holiday_name_en);
                    json.Add("holiday_daytype", model.holiday_daytype);
                    json.Add("holiday_payper", model.holiday_payper);
                    
                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageMTPlanholiday(InputMTPlanholiday input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTPlanholiday objPlanholiday = new cls_ctMTPlanholiday();
                cls_MTPlanholiday model = new cls_MTPlanholiday();

                model.planholiday_id = input.planholiday_id;
                model.planholiday_code = input.planholiday_code;

                model.planholiday_name_th = input.planholiday_name_th;
                model.planholiday_name_en = input.planholiday_name_en;
                model.company_code = input.company_code;
                model.year_code = input.year_code;
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objPlanholiday.insert(model);

                if (blnResult)
                {                    
                    //-- Holiday
                    string holiday_data = input.holiday_data;
                    try
                    {
                        JObject jsonObject = new JObject();
                        var jsonArray = JsonConvert.DeserializeObject<List<cls_TRHoliday>>(holiday_data);
                        cls_ctTRHoliday objHoliday = new cls_ctTRHoliday();

                        List<cls_TRHoliday> listHoliday = new List<cls_TRHoliday>();

                        foreach (cls_TRHoliday item in jsonArray)
                        {
                            item.company_code = input.company_code;
                            item.planholiday_code = input.planholiday_code;

                            listHoliday.Add(item);
                        }

                        blnResult = objHoliday.insert(input.company_code, input.planholiday_code, listHoliday);

                    }
                    catch { }

                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objPlanholiday.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTPlanholiday(InputMTPlanholiday input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTPlanholiday objPlanholiday = new cls_ctMTPlanholiday();

                bool blnResult = objPlanholiday.delete(input.planholiday_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objPlanholiday.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region MTPlantimeallw
        public string getMTPlantimeallwList(string com)
        {
            JObject output = new JObject();

            cls_ctMTPlantimeallw objPlantimeallw = new cls_ctMTPlantimeallw();
            List<cls_MTPlantimeallw> listPlantimeallw = objPlantimeallw.getDataByFillter(com, "", "");
            JArray array = new JArray();

            if (listPlantimeallw.Count > 0)
            {

                int index = 1;

                foreach (cls_MTPlantimeallw model in listPlantimeallw)
                {
                    JObject json = new JObject();

                    json.Add("plantimeallw_id", model.plantimeallw_id);
                    json.Add("plantimeallw_code", model.plantimeallw_code);
                    json.Add("plantimeallw_name_th", model.plantimeallw_name_th);
                    json.Add("plantimeallw_name_en", model.plantimeallw_name_en);
                    json.Add("company_code", model.company_code);
                    json.Add("plantimeallw_passpro", model.plantimeallw_passpro);
                    json.Add("plantimeallw_lastperiod", model.plantimeallw_lastperiod);

                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string getTRTimeallwList(string com, string plan)
        {
            JObject output = new JObject();

            cls_ctTRTimeallw objTimeallw = new cls_ctTRTimeallw();
            List<cls_TRTimeallw> listTimeallw = objTimeallw.getDataByFillter(com, plan);
            JArray array = new JArray();

            if (listTimeallw.Count > 0)
            {

                int index = 1;

                foreach (cls_TRTimeallw model in listTimeallw)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("plantimeallw_code", model.plantimeallw_code);
                    json.Add("timeallw_no", model.timeallw_no);
                    json.Add("timeallw_time", model.timeallw_time);
                    json.Add("timeallw_type", model.timeallw_type);
                    json.Add("timeallw_method", model.timeallw_method);

                    json.Add("timeallw_timein", model.timeallw_timein);
                    json.Add("timeallw_timeout", model.timeallw_timeout);

                    json.Add("timeallw_normalday", model.timeallw_normalday);
                    json.Add("timeallw_offday", model.timeallw_offday);
                    json.Add("timeallw_companyday", model.timeallw_companyday);
                    json.Add("timeallw_holiday", model.timeallw_holiday);
                    json.Add("timeallw_leaveday", model.timeallw_leaveday);
                   
                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageMTPlantimeallw(InputMTPlantimeallw input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTPlantimeallw objPlantimeallw = new cls_ctMTPlantimeallw();
                cls_MTPlantimeallw model = new cls_MTPlantimeallw();

                model.plantimeallw_id = input.plantimeallw_id;
                model.plantimeallw_code = input.plantimeallw_code;

                model.plantimeallw_name_th = input.plantimeallw_name_th;
                model.plantimeallw_name_en = input.plantimeallw_name_en;
                model.plantimeallw_passpro = input.plantimeallw_passpro;
                model.plantimeallw_lastperiod = input.plantimeallw_lastperiod;

                model.company_code = input.company_code;
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objPlantimeallw.insert(model);

                if (blnResult)
                {
                    //-- Holiday
                    string timeallw_data = input.timeallw_data;
                    try
                    {
                        JObject jsonObject = new JObject();
                        var jsonArray = JsonConvert.DeserializeObject<List<cls_TRTimeallw>>(timeallw_data);
                        cls_ctTRTimeallw objTimeallw = new cls_ctTRTimeallw();

                        List<cls_TRTimeallw> listTimeallw = new List<cls_TRTimeallw>();

                        int i = 1;


                        foreach (cls_TRTimeallw item in jsonArray)
                        {
                            item.timeallw_no = i;
                            item.company_code = input.company_code;
                            item.plantimeallw_code = input.plantimeallw_code;

                            listTimeallw.Add(item);
                            i++;
                        }

                        blnResult = objTimeallw.insert(input.company_code, input.plantimeallw_code, listTimeallw);

                    }
                    catch (Exception ex) {
                        string str = ex.ToString();
                    }

                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objPlantimeallw.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTPlantimeallw(InputMTPlantimeallw input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTPlantimeallw objPlantimeallw = new cls_ctMTPlantimeallw();

                bool blnResult = objPlantimeallw.delete(input.plantimeallw_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objPlantimeallw.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        #region TRCurrent
        public string getTRCurrent(string worker_id, string startdate,string todate)
        {
            JObject output = new JObject();

            cls_ctTRCurrent objTRTime = new cls_ctTRCurrent();
            List<cls_TRCurrent> listTRTime = objTRTime.getDataByFillter(worker_id, startdate,todate);

            JArray array = new JArray();

            if (listTRTime.Count > 0)
            {
                int index = 1;

                foreach (cls_TRCurrent model in listTRTime)
                {
                    JObject json = new JObject();
                    json.Add("date", model.date.ToString("yyyy-MM-dd"));
                    json.Add("daytype", model.daytype);
                    json.Add("shift_code", model.shift_code);
                    json.Add("shift_name_th", model.shift_name_th);
                    json.Add("shift_name_en", model.shift_name_en);
                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        #endregion

        #region MTPlanshiftflexible
        public string getMTPlanshiftflexibleList(string com, string year)
        {
            JObject output = new JObject();

            cls_ctMTPlanshiftflexible objPlanshiftflexible = new cls_ctMTPlanshiftflexible();
            List<cls_MTPlanshiftflexible> listPlanshiftflexible = objPlanshiftflexible.getDataByFillter(com, "", "", year);
            JArray array = new JArray();

            if (listPlanshiftflexible.Count > 0)
            {

                int index = 1;

                foreach (cls_MTPlanshiftflexible model in listPlanshiftflexible)
                {
                    JObject json = new JObject();

                    json.Add("planshiftflexible_id", model.planshiftflexible_id);
                    json.Add("planshiftflexible_code", model.planshiftflexible_code);
                    json.Add("planshiftflexible_name_th", model.planshiftflexible_name_th);
                    json.Add("planshiftflexible_name_en", model.planshiftflexible_name_en);
                    json.Add("company_code", model.company_code);
                    json.Add("year_code", model.year_code);
                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string getTRShiftflexibleList(string com, string plan)
        {
            JObject output = new JObject();

            cls_ctTRShiftflexible objShiftflexible = new cls_ctTRShiftflexible();
            List<cls_TRShiftflexible> listShiftflexible = objShiftflexible.getDataByFillter(com, plan);
            JArray array = new JArray();

            if (listShiftflexible.Count > 0)
            {

                int index = 1;

                foreach (cls_TRShiftflexible model in listShiftflexible)
                {
                    JObject json = new JObject();

                    json.Add("company_code", model.company_code);
                    json.Add("planshiftflexible_code", model.planshiftflexible_code);
                    json.Add("shift_code", model.shift_code);

                    json.Add("shift_name_th", model.shift_name_th);
                    json.Add("shift_name_en", model.shift_name_en);
                    
                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string doManageMTPlanshiftflexible(InputMTPlanshiftflexible input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTPlanshiftflexible objPlanshiftflexible = new cls_ctMTPlanshiftflexible();
                cls_MTPlanshiftflexible model = new cls_MTPlanshiftflexible();

                model.planshiftflexible_id = input.planshiftflexible_id;
                model.planshiftflexible_code = input.planshiftflexible_code;

                model.planshiftflexible_name_th = input.planshiftflexible_name_th;
                model.planshiftflexible_name_en = input.planshiftflexible_name_en;
                model.company_code = input.company_code;
                model.year_code = input.year_code;
                model.modified_by = input.modified_by;
                model.flag = model.flag;

                bool blnResult = objPlanshiftflexible.insert(model);

                if (blnResult)
                {
                    
                    string shiftflexible_data = input.shiftflexible_data;
                    try
                    {
                        JObject jsonObject = new JObject();
                        var jsonArray = JsonConvert.DeserializeObject<List<cls_TRShiftflexible>>(shiftflexible_data);
                        cls_ctTRShiftflexible objShift = new cls_ctTRShiftflexible();

                        List<cls_TRShiftflexible> listShift = new List<cls_TRShiftflexible>();

                        foreach (cls_TRShiftflexible item in jsonArray)
                        {
                            item.company_code = input.company_code;
                            item.planshiftflexible_code = input.planshiftflexible_code;

                            listShift.Add(item);
                        }

                        blnResult = objShift.insert(input.company_code, input.planshiftflexible_code, listShift);

                    }
                    catch { }

                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objPlanshiftflexible.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);

        }
        public string doDeleteMTPlanshiftflexible(InputMTPlanshiftflexible input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTPlanshiftflexible objPlanshiftflexible = new cls_ctMTPlanshiftflexible();

                bool blnResult = objPlanshiftflexible.delete(input.planshiftflexible_id.ToString());

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objPlanshiftflexible.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();

            }

            return output.ToString(Formatting.None);

        }
        #endregion

        public string getSummaryWageList(string language, string com, string fromdate, string todate, string createdate)
        {
            JObject output = new JObject();

            cls_ctTRWageday objWageday = new cls_ctTRWageday();
            List<cls_TRWageday> listWageday = objWageday.getDataByCreateDate(language, com, Convert.ToDateTime(fromdate), Convert.ToDateTime(todate), Convert.ToDateTime(createdate));
            JArray array = new JArray();

            if (listWageday.Count > 0)
            {
                int index = 1;


                string worker_code = listWageday[0].worker_code;
                string worker_detail = listWageday[0].worker_detail;

                double douSalary = 0;
                double douLate = 0;
                double douLeave = 0;
                double douAbsent = 0;
                double douOvertime = 0;
                double douAllowance = 0;

                int row = 0;

                foreach (cls_TRWageday model in listWageday)
                {
                    row++;

                    if (worker_code.Equals(model.worker_code) && row < listWageday.Count)
                    {

                        douSalary += model.wageday_wage;
                        douLate += model.late_amount;
                        douLeave += model.leave_amount;
                        douAbsent += model.absent_amount;
                        douOvertime += (model.ot1_amount + model.ot15_amount + model.ot2_amount + model.ot3_amount);
                        douAllowance += model.allowance_amount;


                    }
                    else
                    {
                        JObject json = new JObject();

                        json.Add("company_code", com);
                        json.Add("worker_code", worker_code);
                        json.Add("worker_detail", worker_detail);

                        json.Add("salary", douSalary);
                        json.Add("late", douLate);
                        json.Add("leave", douLeave);
                        json.Add("absent", douAbsent);
                        json.Add("overtime", douOvertime);
                        json.Add("allowance", douAllowance);

                        json.Add("index", index);
                        index++;

                        array.Add(json);

                        //-- Next emp
                        douSalary = 0;
                        douLate = 0;
                        douLeave = 0;
                        douAbsent = 0;
                        douOvertime = 0;
                        douAllowance = 0;

                        worker_code = model.worker_code;
                        worker_detail = model.worker_detail;
                    }
                    
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }

        private string doCheckDateEmpty(DateTime date)
        {
            if (date.Date.ToString("dd/MM/yyyy").Equals("01/01/1900"))
                return "-";
            else
                return date.ToString("dd/MM/yyyy");
        }
        private string doCheckDateTimeEmpty(DateTime date)
        {
            if (date.Date.ToString("dd/MM/yyyy").Equals("01/01/1900"))
                return "-";
            else
                return date.ToString("dd/MM/yyyy HH:mm:ss");
        }
        private string doCheckTimeEmpty(DateTime date)
        {
            if (date.ToString("HH:mm").Equals("00:00"))
                return "-";
            else
                return date.ToString("yyyy-MM-dd HH:mm");
        }
        private DateTime doConvertDate(string date)
        {
            DateTime result = DateTime.Now;
            try
            {
                result = Convert.ToDateTime(date);
            }
            catch { }

            return result;
        }

        #endregion

        #region Dashboard

        
        #region Att
        public string getDashLeaveList(string com)
        {
            JObject output = new JObject();

            cls_ctTADashboard objDash = new cls_ctTADashboard();
            List<cls_TADashboard> listDash = objDash.getDataLeaveByFillter(com);

            JArray array = new JArray();

            if (listDash.Count > 0)
            {
                int index = 1;

                foreach (cls_TADashboard model in listDash)
                {
                    JObject json = new JObject();
                    json.Add("worker_code", model.worker_code);
                    json.Add("dep_name_en", model.dep_name_en);
                    json.Add("dep_name_th", model.dep_name_th);
                    

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }

        public string getDashLateList(string com)
        {
            JObject output = new JObject();

            

            cls_ctTADashboard objDash = new cls_ctTADashboard();
            List<cls_TADashboard> listDash = objDash.getDataLateByFillter(com);

            JArray array = new JArray();

            if (listDash.Count > 0)
            {
                int index = 1;

                foreach (cls_TADashboard model in listDash)
                {
                    JObject json = new JObject();
                    json.Add("WORKER_CODE", model.worker_code);
                   
                    json.Add("dep_name_en", model.dep_name_en);
                    json.Add("dep_name_th", model.dep_name_th);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }

        #endregion

        #region Emp
        public string getEmpPositionDash(string fromdate, string todate)
        {
            JObject output = new JObject();


            DateTime datefrom = Convert.ToDateTime(fromdate);
            DateTime dateto = Convert.ToDateTime(todate);

            cls_ctMTEmpPositionDash objPoDash = new cls_ctMTEmpPositionDash();
            List<cls_TREmpPositionDash> listShift = objPoDash.getDataByFillter(datefrom, dateto);

            JArray array = new JArray();

            if (listShift.Count > 0)
            {
                int index = 1;

                foreach (cls_TREmpPositionDash model in listShift)
                {
                    JObject json = new JObject();

                    json.Add("worker_code", model.worker_code);
                    json.Add("position_name_en", model.position_name_en);
                    json.Add("position_name_th", model.position_name_th);


                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }

        public string getDashGenderList(string com)
        {
            JObject output = new JObject();

            cls_ctMTDashboard objDash = new cls_ctMTDashboard();
            List<cls_MTDashboard> listDash = objDash.getDataGenderByFillter(com);

            JArray array = new JArray();

            if (listDash.Count > 0)
            {
                int index = 1;

                foreach (cls_MTDashboard model in listDash)
                {
                    JObject json = new JObject();

                    json.Add("worker_code", model.worker_code);
                    json.Add("worker_gender_en", model.worker_gender_en);
                    json.Add("worker_gender_th", model.worker_gender_th);


                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string getDashEmpDepList(string com) 
        {
            JObject output = new JObject();

            cls_ctMTDashboard objDash = new cls_ctMTDashboard();
            List<cls_MTDashboard > listDash = objDash.getDataEmpDepByFillter(com);

            JArray array = new JArray();

            if (listDash.Count > 0)
            {
                int index = 1;

                foreach (cls_MTDashboard model in listDash)
                {
                    JObject json = new JObject();

                    json.Add("worker_code", model.worker_code);
                    json.Add("dep_name_th", model.dep_name_th);
                    json.Add("dep_name_en", model.dep_name_en);


                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        public string getDashEmpAgeList(string com) 
        {
            JObject output = new JObject();

            cls_ctMTDashboard objDash = new cls_ctMTDashboard();
            List<cls_MTDashboard> listDash = objDash.getDataEmpAgeByFillter(com);

            JArray array = new JArray();

            if (listDash.Count > 0)
            {
                int index = 1;

                foreach (cls_MTDashboard model in listDash)
                {
                    JObject json = new JObject();

                    json.Add("worker_code", model.worker_code);
                    json.Add("age", model.age);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
      

        public string getDashEmpWorkAgeList(string com) 
        {
            JObject output = new JObject();

            cls_ctMTDashboard objDash = new cls_ctMTDashboard();
            List<cls_MTDashboard> listDash = objDash.getDataEmpWorkAgeByFillter(com);

            JArray array = new JArray();

            if (listDash.Count > 0)
            {
                int index = 1;

                foreach (cls_MTDashboard model in listDash)
                {
                    JObject json = new JObject();

                    json.Add("worker_code", model.worker_code);
                    json.Add("age", model.age);

                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }

        #endregion

        #region Pay
        public string getDashItemINList(string com)
        {
            JObject output = new JObject();

            cls_ctTRDashboard objDash = new cls_ctTRDashboard();
            List<cls_TRDashboard> listDash = objDash.getDataItemINByFillter(com);

            JArray array = new JArray();

            if (listDash.Count > 0)
            {
                int index = 1;

                foreach (cls_TRDashboard model in listDash)
                {
                    JObject json = new JObject();
                    json.Add("WORKER_CODE", model.worker_code);
                    json.Add("AMOUNT", model.amount);
                    json.Add("ITEM_NAME_EN", model.item_name_en);
                    json.Add("ITEM_NAME_TH", model.item_name_th);
                    json.Add("ITEM_CODE", model.item_code);


                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }

        public string getDashItemDEList(string com)
        {
            JObject output = new JObject();

            

            cls_ctTRDashboard objDash = new cls_ctTRDashboard();
            List<cls_TRDashboard> listDash = objDash.getDataItemDEByFillte(com);

            JArray array = new JArray();

            if (listDash.Count > 0)
            {
                int index = 1;

                foreach (cls_TRDashboard model in listDash)
                {
                    JObject json = new JObject();
                    json.Add("worker_code", model.worker_code);
                    //json.Add("amount", model.amount);
                    json.Add("item_name_th", model.item_name_th);
                    json.Add("item_name_en", model.item_name_en);
                    json.Add("item_code", model.item_code);


                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }

        public string getDashItemOTDepList(string fromdate, string todate)
        {
            JObject output = new JObject();

            DateTime datefrom = Convert.ToDateTime(fromdate);
            DateTime dateto = Convert.ToDateTime(todate);

            cls_ctTRDashboard objDash = new cls_ctTRDashboard();
            List<cls_TRDashboard> listDash = objDash.getDataOTDepByFillter(datefrom, dateto);

            JArray array = new JArray();

            if (listDash.Count > 0)
            {
                int index = 1;

                foreach (cls_TRDashboard model in listDash)
                {
                    JObject json = new JObject();
                    json.Add("worker_code", model.worker_code);
                    //json.Add("before_min", model.before_min);
                    json.Add("dep_name_th", model.dep_name_th);
                    json.Add("dep_name_en", model.dep_name_en);


                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }

        public string getDashItemOTPoList(string fromdate, string todate)
        {
            JObject output = new JObject();

            DateTime datefrom = Convert.ToDateTime(fromdate);
            DateTime dateto = Convert.ToDateTime(todate);

            cls_ctTRDashboard objDashh = new cls_ctTRDashboard();
            List<cls_TRDashboard> listDashh = objDashh.getDataOTPoByFillter(datefrom, dateto);

            JArray array = new JArray();

            if (listDashh.Count > 0)
            {
                int index = 1;

                foreach (cls_TRDashboard model in listDashh)
                {
                    JObject json = new JObject();
                    json.Add("worker_code", model.worker_code);
                    json.Add("before_min", model.before_min);
                    json.Add("after_min", model.after_min);
                    json.Add("empposition_position", model.empposition_position);
                    json.Add("position_name_th", model.position_name_th);
                    json.Add("position_name_en", model.position_name_en);


                    json.Add("index", index);

                    index++;

                    array.Add(json);
                }

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }
        #endregion

        #endregion

        //-- Files manage
        #region Files manage


        public async Task<string> UploadStream(Stream stream)
        {
            MultipartParser parser = new MultipartParser(stream);
            JObject output = new JObject();
            if (parser.Success)
            {
                //absolute filename, extension included.
                var filename = parser.Filename;
                var filetype = parser.ContentType;
                var ext = Path.GetExtension(filename);
                string name = Guid.NewGuid().ToString() + ext;
                string FilePath = Path.Combine
                   (HostingEnvironment.MapPath("~/Uploads"), name);
                using (var file = File.Create(FilePath))
                {
                    await file.WriteAsync(parser.FileContents, 0, parser.FileContents.Length);
                    output["result"] = "1";
                    output["filename"] = name;
                    output["path"] = FilePath;
                    output["file"] = parser.ToString();
                    output["Type"] = parser.ContentType;
                }
            }
            else
            {
                output["result"] = "0";
                output["Type"] = parser.ContentType;
                output["file"] = parser.Success.ToString();
            }
            return output.ToString(Formatting.None);
        }
        public Stream doDownloadFile(string fileName, string fileExtension)
        {
            //string downloadFilePath =
            //Path.Combine(HostingEnvironment.MapPath
            //("D:\\Temp"), fileName + "." + fileExtension);

            /*
            string downloadFilePath = Path.Combine
                   (ClassLibrary_BPC.Config.PathFileImport, fileName + "." + fileExtension);

            //Write logic to create the file
            File.Create(downloadFilePath);

            String headerInfo = "attachment; filename=" + fileName + "." + fileExtension;
            WebOperationContext.Current.OutgoingResponse.Headers
                                  ["Content-Disposition"] = headerInfo;

            WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";

            return File.OpenRead(downloadFilePath);
            */

            string downloadFilePath = Path.Combine
                   (ClassLibrary_BPC.Config.PathFileExport, fileName);
            /*
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/txt";
            FileStream f = new FileStream(downloadFilePath, FileMode.Open);
            int length = (int)f.Length;
            WebOperationContext.Current.OutgoingResponse.ContentLength = length;
            byte[] buffer = new byte[length];
            int sum = 0;
            int count;
            while ((count = f.Read(buffer, sum, length - sum)) > 0)
            {
                sum += count;
            }
            f.Close();
            f.Dispose();
            return new MemoryStream(buffer);
            */

            /*
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/txt";
            WebOperationContext.Current.OutgoingResponse.ContentLength = length;

            using (var stream = File.Open(downloadFilePath, FileMode.Open, FileAccess.Write, FileShare.Read))
            {


            }
            */

            //cls_srvProcessPayroll srv_pay = new cls_srvProcessPayroll();

            //string str = srv_pay.doExportBank("BPC", "57");


            string file = downloadFilePath;
            try
            {
                //string filename = fileName + "." + fileExtension;
             
                FileStream fs = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/txt";
                WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-disposition", "inline; filename=" + fileName);
                return fs;
                //return Stream;
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public string doUploadTimeInput(string fileName, Stream stream)
        {
            JObject output = new JObject();

            try
            {
                Regex regex = new Regex("(^-+)|(^content-)|(^$)|(^submit)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

                string FilePath = Path.Combine
                   (ClassLibrary_BPC.Config.PathFileImport + "\\Att\\Import", fileName);
                /*
                int length = 0;
                using (FileStream writer = new FileStream(FilePath, FileMode.Create))
                {
                    int readCount;
                    var buffer = new byte[8192];
                    while ((readCount = stream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        

                        writer.Write(buffer, 0, readCount);
                        length += readCount;
                    }
                }
                 * */

                using (FileStream writer = new FileStream(FilePath, FileMode.Create))
                {
                    TextReader textReader = new StreamReader(stream);
                    string sLine = textReader.ReadLine();

                    while (sLine != null)
                    {
                        if (!regex.Match(sLine).Success)
                        {
                            byte[] bytes = Encoding.UTF8.GetBytes(sLine);

                            writer.Write(bytes, 0, bytes.Length);

                            byte[] newline = Encoding.ASCII.GetBytes(Environment.NewLine);
                            writer.Write(newline, 0, newline.Length);
                        }
                        sLine = textReader.ReadLine();
                    }
                }

                output["result"] = "1";
                output["result_text"] = "0";
            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);
        }

        public string doReadSimpleTimeInput(string fileName, Stream stream)
        {
            JObject output = new JObject();

            try
            {
                Regex regex = new Regex("(^-+)|(^content-)|(^$)|(^submit)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

                string FilePath = Path.Combine
                   (ClassLibrary_BPC.Config.PathFileImport + "\\Att\\Import", fileName);

                TextReader textReader = new StreamReader(stream);
                string sLine = textReader.ReadLine();

                string firstRow = "";

                while (sLine != null)
                {
                    if (!regex.Match(sLine).Success)
                    {
                        firstRow = sLine;
                        break;
                    }
                    sLine = textReader.ReadLine();
                }

                output["result"] = "1";
                output["result_text"] = "0";
                output["data"] = firstRow;
            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);
        }



        #endregion


        //private void 

        public string doGetQR2Factor(string com, string usr, string token)
        {
            JObject output = new JObject();
            try
            {
                JObject json = new JObject();

                TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                string UserUniqueKey = usr + key;
                var setupInfo = tfa.GenerateSetupCode("iHR Authenticator", usr, UserUniqueKey, 300, 300);
                var barcodeImageUrl = setupInfo.QrCodeSetupImageUrl;
                var setupCode = setupInfo.ManualEntryKey;


                json.Add("barcode", barcodeImageUrl);
                json.Add("setupcode", setupCode);
                json.Add("uniquekey", UserUniqueKey);

                JArray array = new JArray();
                array.Add(json);

                output["result"] = "1";
                output["result_text"] = "1";
                output["data"] = array;
            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);
        }

        public string doVerify(string usr, string token)
        {
            JObject output = new JObject();
            try
            {
                TwoFactorAuthenticator TwoFacAuth = new TwoFactorAuthenticator();
                //string UserUniqueKey = usr;
                string UserUniqueKey = usr + key;
                bool isValid = TwoFacAuth.ValidateTwoFactorPIN(UserUniqueKey, token);
                if (isValid)
                {
                    HttpCookie TwoFCookie = new HttpCookie("TwoFCookie");
                    string UserCode = Convert.ToBase64String(MachineKey.Protect(Encoding.UTF8.GetBytes(UserUniqueKey)));

                    TwoFCookie.Values.Add("UserCode", UserCode);
                    TwoFCookie.Expires = DateTime.Now.AddDays(30);
                    //Response.Cookies.Add(TwoFCookie);
                    //Session["IsValidTwoFactorAuthentication"] = true;
                    //return RedirectToAction("UserProfile", "Login");

                    output["result"] = "1";
                    output["result_text"] = "Verify Success";
                    output["user_code"] = UserCode;
                }
                else
                {
                    output["result"] = "0";
                    output["result_text"] = "Verify Fail";
                }
            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);
        }

        private const string key = "aaas123!@@)(*";
        
        public string doAuthen(string usr, string pwd, string token)
        {
            JObject output = new JObject();

            try
            {
                JObject json = new JObject();

                DateTime dateNow = DateTime.Now;

                if (usr.Equals("admin") && pwd.Equals("2021"))
                {
                    cls_SYSAccount model = new cls_SYSAccount();

                    TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                    string UserUniqueKey = usr + key;
                    var setupInfo = tfa.GenerateSetupCode("iHR Authenticator", usr, UserUniqueKey, 300, 300);
                    var barcodeImageUrl = setupInfo.QrCodeSetupImageUrl;
                    var setupCode = setupInfo.ManualEntryKey;

                    json.Add("company_code", "BPC");
                    json.Add("account_id", 1);

                    json.Add("account_usr", "admin");
                    json.Add("account_pwd", "2021");
                    json.Add("account_detail", "Administrator");

                    json.Add("account_email", "");
                    json.Add("account_emailalert", false);
                    json.Add("account_line", "");
                    json.Add("account_linealert", false);

                    json.Add("account_lock", false);
                    json.Add("account_faillogin", 0);
                    json.Add("account_lastlogin", new DateTime());

                    json.Add("account_login", dateNow);

                    json.Add("polmenu_code", "FULL");

                    json.Add("account_monthly", true);
                    json.Add("account_daily", true);

                    json.Add("self_admin", "Y");

                    json.Add("barcode", barcodeImageUrl);
                    json.Add("setupcode", setupCode);
                    json.Add("uniquekey", UserUniqueKey);


                    json.Add("index", 1);

                    JArray array = new JArray();
                    array.Add(json);

                    output["result"] = "1";
                    output["result_text"] = "1";
                    output["data"] = array;
                }
                else
                {
                    cls_ctSYSAccount objAccount = new cls_ctSYSAccount();
                    List<cls_SYSAccount> listAccount = objAccount.getData(usr, pwd);
                    JArray array = new JArray();

                    if (listAccount.Count > 0)
                    {
                        int index = 1;

                        TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                        string UserUniqueKey = usr + key;
                        var setupInfo = tfa.GenerateSetupCode("iHR Authenticator", usr, UserUniqueKey, 300, 300);
                        var barcodeImageUrl = setupInfo.QrCodeSetupImageUrl;
                        var setupCode = setupInfo.ManualEntryKey;

                        foreach (cls_SYSAccount model in listAccount)
                        {                            
                            json.Add("company_code", model.company_code);
                            json.Add("account_id", model.account_id);

                            json.Add("account_usr", model.account_usr);
                            json.Add("account_pwd", model.account_pwd);
                            json.Add("account_detail", model.account_detail);

                            json.Add("account_email", model.account_email);
                            json.Add("account_emailalert", model.account_emailalert);
                            json.Add("account_line", model.account_line);
                            json.Add("account_linealert", model.account_linealert);

                            json.Add("account_lock", model.account_lock);
                            json.Add("account_faillogin", model.account_faillogin);
                            json.Add("account_lastlogin", model.account_lastlogin);

                            json.Add("account_login", dateNow);

                            
                            json.Add("account_monthly", model.account_monthly);
                            json.Add("account_daily", model.account_daily);

                            json.Add("polmenu_code", model.polmenu_code);

                            json.Add("barcode", barcodeImageUrl);
                            json.Add("setupcode", setupCode);
                            json.Add("uniquekey", UserUniqueKey);

                            json.Add("modified_by", model.modified_by);
                            json.Add("modified_date", model.modified_date);
                            json.Add("flag", model.flag);

                            json.Add("index", index);

                            index++;

                            array.Add(json);
                        }

                        output["result"] = "1";
                        output["result_text"] = "1";
                        output["data"] = array;
                    }
                    else
                    {
                        output["result"] = "0";
                        output["result_text"] = "Invalid login..";
                        output["data"] = array;
                    }
                }

                

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);
        }

        //-- Process
        public string doSummarizeTime(string com, string taskid)
        {
            JObject output = new JObject();
            
            cls_srvProcessTime srvProcess = new cls_srvProcessTime();

            output["result"] = "1";
            output["result_text"] = "1";
            output["data"] = srvProcess.doSummarizeTime(com, taskid);

            return output.ToString(Formatting.None);
        }

        public string doImportTime(string com, string taskid)
        {
            JObject output = new JObject();

            cls_srvProcessTime srvProcess = new cls_srvProcessTime();

            output["result"] = "1";
            output["result_text"] = "1";
            output["data"] = srvProcess.doImportTime(com, taskid);

            return output.ToString(Formatting.None);
        }

        #region Image emp
        public string doUploadWorkerImages(string ref_to, Stream stream)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTREmpimages ct_empimages = new cls_ctTREmpimages();

                string[] temp = ref_to.Split('.');
                                
                MultipartParser parser = new MultipartParser(stream);

                if (parser.Success) { 

                    cls_TREmpimages empimages = new cls_TREmpimages();
                    empimages.company_code = temp[0];
                    empimages.worker_code = temp[1];
                    empimages.empimages_images = parser.FileContents;
                    empimages.modified_by = temp[2];

                    empimages.empimages_no = 1;

                    ct_empimages.insert(empimages);
                
                    output["result"] = "1";
                    output["result_text"] = "0";

                }
                else
                {
                    output["result"] = "0";
                    output["result_text"] = "0";
                }
            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);
        }

        public bool IsValidImage(byte[] bytes)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream(bytes))
                    Image.FromStream(ms);
            }
            catch (ArgumentException)
            {
                return false;
            }
            return true;
        }
                       
        public string doGetWorkerImages(string com, string emp)
        {
            JObject output = new JObject();

            try
            {
                cls_ctTREmpimages ct_empimages = new cls_ctTREmpimages();
                List<cls_TREmpimages> list_empimages = ct_empimages.getDataByFillter(com, emp);

                if (list_empimages.Count > 0)
                {
                    cls_TREmpimages md_image = list_empimages[0];        
                    
                    bool bln = this.IsValidImage(md_image.empimages_images);

                    output["result"] = "1";
                    output["result_text"] = "";
                    output["data"] = "data:image/png;base64," + System.Convert.ToBase64String(md_image.empimages_images);
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = "Data not found";
                    output["data"] = "";
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);
        }
        #endregion


        //-- Login self services
        public string doLogin(string usr, string pwd, string token)
        {
            JObject output = new JObject();

            try
            {
                JObject json = new JObject();

                cls_ctMTWorker objWorker = new cls_ctMTWorker();
                cls_MTWorker worker = objWorker.doLogin(usr, pwd);
                JArray array = new JArray();

                if (worker != null)
                {
                    TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                    string UserUniqueKey = usr + key;
                    var setupInfo = tfa.GenerateSetupCode("iHR Authenticator", usr, UserUniqueKey, 300, 300);
                    var barcodeImageUrl = setupInfo.QrCodeSetupImageUrl;
                    var setupCode = setupInfo.ManualEntryKey;

                    json.Add("company_code", worker.company_code);                    
                    json.Add("worker_code", worker.worker_code);
                    json.Add("worker_initial", worker.worker_initial);
                    json.Add("worker_fname_th", worker.worker_fname_th);
                    json.Add("worker_lname_th", worker.worker_lname_th);
                    json.Add("worker_fname_en", worker.worker_fname_en);
                    json.Add("worker_lname_en", worker.worker_lname_en);

                    json.Add("initial_name_th", worker.initial_name_th);
                    json.Add("initial_name_en", worker.initial_name_en);

                    json.Add("self_admin", worker.self_admin);
                    json.Add("barcode", barcodeImageUrl);
                    json.Add("setupcode", setupCode);
                    json.Add("uniquekey", UserUniqueKey);

                    array.Add(json);

                    output["result"] = "1";
                    output["result_text"] = "1";
                    output["data"] = array;
                }
                else
                {
                    output["result"] = "0";
                    output["result_text"] = "Invalid login..";
                    output["data"] = array;
                }         



            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);
        }

        public string doChangePass(InputMTWorker input)
        {
            JObject output = new JObject();

            try
            {
                cls_ctMTWorker objWorker = new cls_ctMTWorker();
                cls_MTWorker model = new cls_MTWorker();

                string strWorkerCode = input.worker_code;
                string strComCode = input.company_code;

                model.company_code = input.company_code;
                model.worker_code = input.worker_code;
                model.worker_pwd = input.worker_pwd;
             
                model.modified_by = input.modified_by;
                

                bool blnResult = objWorker.change_pwd(model);

                if (blnResult)
                {
                    output["result"] = "1";
                    output["result_text"] = "0";
                }
                else
                {
                    output["result"] = "2";
                    output["result_text"] = objWorker.getMessage();
                }

            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);

        }



        //-- Self services
        public string getShfitDetail(string com, string emp, string workdate, string language)
        {
            JObject output = new JObject();

            DateTime datework = Convert.ToDateTime(workdate);
           
            cls_ctTRTimecard objTimecard = new cls_ctTRTimecard();
            List<cls_TRTimecard> listTimecard = objTimecard.getDataByFillter(com, emp, datework, datework);
            JArray array = new JArray();

            if (listTimecard.Count > 0)
            {
                cls_TRTimecard timecard = listTimecard[0];

                cls_ctMTShift objShift = new cls_ctMTShift();
                List<cls_MTShift> listShift = objShift.getDataByFillter(com, "", timecard.shift_code);


                if (listShift.Count > 0)
                {

                    cls_MTShift model = listShift[0];

                    string strResult = model.shift_name_th ;

                    if (language.Equals("EN"))
                    {
                        strResult = model.shift_name_en ;
                    }

                    JObject json = new JObject();


                    json.Add("shift_id", model.shift_id);
                    json.Add("shift_code", model.shift_code);
                    json.Add("shift_name_th", model.shift_name_th);
                    json.Add("shift_name_en", model.shift_name_en);

                    json.Add("shift_ch1", model.shift_ch1);
                    json.Add("shift_ch2", model.shift_ch2);
                    json.Add("shift_ch3", model.shift_ch3);
                    json.Add("shift_ch4", model.shift_ch4);
                    json.Add("shift_ch5", model.shift_ch5);
                    json.Add("shift_ch6", model.shift_ch6);
                    json.Add("shift_ch7", model.shift_ch7);
                    json.Add("shift_ch8", model.shift_ch8);
                    json.Add("shift_ch9", model.shift_ch9);
                    json.Add("shift_ch10", model.shift_ch10);

                    json.Add("shift_ch3_from", model.shift_ch3_from);
                    json.Add("shift_ch3_to", model.shift_ch3_to);
                    json.Add("shift_ch4_from", model.shift_ch4_from);
                    json.Add("shift_ch4_to", model.shift_ch4_to);

                    json.Add("shift_ch7_from", model.shift_ch7_from);
                    json.Add("shift_ch7_to", model.shift_ch7_to);
                    json.Add("shift_ch8_from", model.shift_ch8_from);
                    json.Add("shift_ch8_to", model.shift_ch8_to);

                    json.Add("shift_otin_min", model.shift_otin_min);
                    json.Add("shift_otin_max", model.shift_otin_max);

                    json.Add("shift_otout_min", model.shift_otout_min);
                    json.Add("shift_otout_max", model.shift_otout_max);

                    json.Add("shift_flexiblebreak", model.shift_flexiblebreak);

                    json.Add("modified_by", model.modified_by);
                    json.Add("modified_date", model.modified_date);
                    json.Add("flag", model.flag);

                    array.Add(json);

                    output["result"] = "1";
                    output["result_text"] = strResult;
                    output["data"] = array;

                }
                else
                {
                    output["result"] = "0";
                    output["result_text"] = "Data not Found";
                    output["data"] = array;
                }

                
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = array;
            }

            return output.ToString(Formatting.None);
        }

        public string doGetLeaveActualDay(string com, string emp, string fromdate, string todate)
        {
            JObject output = new JObject();

            DateTime datefrom = Convert.ToDateTime(fromdate);
            DateTime dateto = Convert.ToDateTime(todate);

            cls_ctTRTimecard objTimecard = new cls_ctTRTimecard();
            List<cls_TRTimecard> listTimecard = objTimecard.getDataByFillter(com, emp, datefrom, dateto);
                        
            if (listTimecard.Count > 0)
            {
                int intDays = 0;

                foreach (cls_TRTimecard model in listTimecard)
                {
                    if (model.timecard_daytype.Equals("O") || model.timecard_daytype.Equals("H") || model.timecard_daytype.Equals("C"))
                    {

                    }
                    else
                    {
                        intDays++;
                    }

                }

                output["result"] = "0";
                output["result_text"] = "Success";
                output["data"] = intDays;
            }
            else
            {
                output["result"] = "0";
                output["result_text"] = "Data not Found";
                output["data"] = "0";
            }

            return output.ToString(Formatting.None);
        }
        public string strEmp { get; set; }


        public string doTest(req input)
        {
            JObject output = new JObject();
            output["success"] = true;
            output["message"] = "Retrieved data successfully";
            return output.ToString(Formatting.None);
        }

        //-- F add 28/11/2022
        public async Task<string> doUploadExcel(string fileName, Stream stream)
        {
            JObject output = new JObject();

            try
            {
                Regex regex = new Regex("(^-+)|(^content-)|(^$)|(^submit)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

                string FilePath = Path.Combine
                   (ClassLibrary_BPC.Config.PathFileImport + "\\Imports", fileName);

                MultipartParser parser = new MultipartParser(stream);

                if (parser.Success)
                {
                    //absolute filename, extension included.
                    var filename = parser.Filename;
                    var filetype = parser.ContentType;
                    var ext = Path.GetExtension(filename);

                    using (var file = File.Create(FilePath))
                    {
                        await file.WriteAsync(parser.FileContents, 0, parser.FileContents.Length);
                        output["result"] = "1";
                        output["result_text"] = FilePath;

                    }
                }
                else
                {
                    output["result"] = "0";
                    output["result_text"] = parser.ContentType;
                }


                output["result"] = "1";
                output["result_text"] = "0";
            }
            catch (Exception ex)
            {
                output["result"] = "0";
                output["result_text"] = ex.ToString();
            }

            return output.ToString(Formatting.None);
        }


    }

    [DataContract]
    public class req
    {
        [DataMember(Order = 0)]
        public string usname { get; set; }
        [DataMember(Order = 1)]
        public string pwd { get; set; }
    }


}
