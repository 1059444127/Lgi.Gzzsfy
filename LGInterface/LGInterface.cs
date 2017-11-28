using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.EnterpriseServices.CompensatingResourceManager;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Services;
using System.Windows.Forms;
using System.Xml;
using LGInterface.GuangZhouZhongShan1St;
using LGInterface.Model;
using LGInterface.Util;
using System.Data.Odbc;
using LGInterface.WebReferenceForDaiJiaTiJian;
using SendPisResult;
using Request = LGInterface.GuangZhouZhongShan1St.Request;
using RequestHeader = LGInterface.GuangZhouZhongShan1St.RequestHeader;
using Response = LGInterface.WebReferenceForDaiJiaTiJian.Response;

namespace LGInterface
{
    public class LGInterface
    {
        private static IniFiles f = new IniFiles("sz.ini");

        private string ConfigSection = "广州中山附一";

        /// <summary>
        /// 连接HIS视图使用的ODBC连接串
        /// </summary>
        private static string odbcConnStr = "";

        public LGInterface()
        {
            odbcConnStr = f.ReadString(ConfigSection, "ODBC连接串", "");
        }

        //exid 放到申请序号
        //处方序号 放到就诊ID
        //CARDNUMBER 这个放到身份证号
        //string sHISName, string Sslbx"门诊号","住院号" , string Ssbz "标记", string Debug, string by
        //该接口用发票号代替门诊号,不存门诊号
        public string LGGetHISINFO(string sHISName, string Sslbx, string Ssbz, string Debug, string by)
        {
            //log.开始请求申请信息,入参sslbx=,ssbz=

            #region 如果类型是"标本签收",弹出标本签收网页

            if (Sslbx == "标本签收")
            {
                var url = f.ReadString("标本签收", "url", "http://168.168.252.111:8889/Receive/Receipt.aspx");
                var userName = f.ReadString("yh", "yhmc", "");
                var t_yhm = DbHelper.GetTable($" select f_yhm from t_yh t where t.f_yhmc = '{userName}' ");
                var userId = "";
                if (t_yhm.Rows.Count > 0)
                    userId = t_yhm.Rows[0][0].ToString();

                //调用系统默认的浏览器   
                System.Diagnostics.Process.Start(url + $"?userid={userId}&username={userName}");
                return "0";
            }

            #endregion

            log.WriteMyLog($"开始调用接口请求患者信息:sslbx={Sslbx},ssbz={Ssbz}.");

            string inpatientNumber = "";
            string outpatientNumber = "";
            string operType = Sslbx == "门诊号" ? "0" : "1";

            string sxml = "";

            RequestInfoResult result = null;

            try
            {
                //先获取电子申请单
                result = GetRequestionBySheetId(Ssbz);
                //从HIS前置机获取
                if (result == null)
                    try
                    {
                        result = GetInpatientFromOtherInterface(Ssbz);
                    }
                    catch (Exception e)
                    {
                        log.WriteMyLog("从视图获取患者信息失败:\r\n" + e);
                        return "0";
                    }
                if (result == null)
                {
                    MessageBox.Show("没有找到患者信息!");
                    log.WriteMyLog("没有找到患者信息!");

                    return "0";
                }
                if (result.RequestState == "1")
                {
                    MessageBox.Show("该申请单状态为无效!");
                    log.WriteMyLog("该申请单状态为无效!");

                    return "0";
                }
            }
            catch (Exception e)
            {
                //log.请求患者信息失败
                log.WriteMyLog("查询接口患者信息失败:" + e.Message);
                MessageBox.Show("查询接口患者信息失败!\r\n\r\n错误代码(请拍照给客服):+\r\n" + e.Message);
                return "0";
            }

            //  GetDataSetByXml(sxml);
            string xml = "";
            try
            {
                xml = "<?xml version=" + (char) 34 + "1.0" + (char) 34 + " encoding=" + (char) 34 + "gbk" + (char) 34 +
                      "?>";
                xml = xml + "<LOGENE>";
                xml = xml + "<row ";
                xml = xml + "病人编号=" + (char) 34 + result.PatientNo + (char) 34 + " ";
                xml = xml + "就诊ID=" + (char) 34 + result.JZLSH + (char) 34 + " ";
                xml = xml + "申请序号=" + (char) 34 + result.SheetID + (char) 34 + " ";
                xml = xml + "门诊号=" + (char) 34 + result.OutHospitalID + (char) 34 + " ";
                xml = xml + "住院号=" + (char) 34 + result.InHospitalID + (char) 34 + " ";
                xml = xml + "姓名=" + (char) 34 + result.PatientName + (char) 34 + " ";
                xml = xml + "性别=" + (char) 34 + result.PatientSexText + (char) 34 + " ";
                xml = xml + "年龄=" + (char) 34 + result.PatientAgeText + (char) 34 + " ";
                xml = xml + "婚姻=" + (char) 34 + "" + (char) 34 + " ";
                xml = xml + "地址=" + (char) 34 + result.PatientAddress + (char) 34 + " ";
                xml = xml + "电话=" + (char) 34 + result.PatientTel + (char) 34 + " ";
                xml = xml + "病区=" + (char) 34 + (char) 34 + " ";
                xml = xml + "床号=" + (char) 34 + result.PatientBedNum + (char) 34 + " ";
                xml = xml + "身份证号=" + (char) 34 + "" + (char) 34 + " ";
                xml = xml + "民族= " + (char) 34 + "" + (char) 34 + " ";
                xml = xml + "职业=" + (char) 34 + result.RoomNumber + (char) 34 + " ";
                xml = xml + "送检科室=" + (char) 34 + result.DepartMent + (char) 34 + " ";
                xml = xml + "送检医生=" + (char) 34 + result.ReqSheetDoctor + (char) 34 + " ";
                xml = xml + "收费=" + (char) 34 + "" + (char) 34 + " ";

                //2017年9月26日 姚迁要求把取材部位和名称倒过来,因为病理科主要看取材部位,但系统主要显示的是名称
                //2017年10月12日 季海军要求再改回来
                var bbmc = "";

                if (result.ExamList != null)
                {
                    result.ExamList.Sort(CompareById);
                    bool isSwitchDept = GetIsSwitchDept(result);
                    if (isSwitchDept == false)
                    {
                        result.ExamList.ForEach(
                            o => bbmc = bbmc + (result.ExamList.IndexOf(o) + 1) + "." + o.Description + "，");
                    }
                    else
                    {
                        result.ExamList.ForEach(
                            o => bbmc = bbmc + (result.ExamList.IndexOf(o) + 1) + "." + o.qucaidengjin + "，");
                    }

                    bbmc = bbmc.TrimEnd('，');
                }

                xml = xml + "标本名称=" + (char) 34 + bbmc + (char) 34 + " ";

                var appHospName = f.ReadString("广州中山附一", "送检医院名称", "");
                xml = xml + "送检医院=" + (char) 34 + appHospName + (char) 34 + " ";
                xml = xml + "医嘱项目=" + (char) 34 + "" + (char) 34 + " ";
                xml = xml + "备用1=" + (char) 34 + "" + (char) 34 + " ";
                xml = xml + "备用2=" + (char) 34 + result.DepartMentID + "||" + result.DepartMent + (char) 34 + " ";
                xml = xml + "费别=" + (char) 34 + result.DetailPatientStyleText + (char) 34 + " ";
                xml = xml + "病人类别=" + (char) 34 + result.PatientStyleText + (char) 34 + " ";
                xml = xml + "/>";
                xml = xml + "<临床病史><![CDATA[" + result.binglizhaiyao + "]]></临床病史>";
                xml = xml + "<临床诊断><![CDATA[" + result.LinChuangZhenDuan + "]]></临床诊断>";
                xml = xml + GetExamList(result);
                xml = xml + "</LOGENE>";
            }
            catch (Exception e)
            {
                //log.解析返回的xml失败
                log.WriteMyLog("解析接口返回的xml时出错:" + e.Message);
                return "0";
            }

            //log.解析患者申请信息成功,返回xml
            log.WriteMyLog("调用接口请求患者信息成功!");

            //如果没找到病人,返回0,避免出错
            if (string.IsNullOrEmpty(xml.Trim()))
                xml = "0";

            return xml;
        }

        public static DataSet GetDataSetByXml(string xmlData)
        {
            try
            {
                DataSet ds = new DataSet();

                using (StringReader xmlSR = new StringReader(xmlData))
                {
                    //ds.ReadXml(xmlSR);
                    ds.ReadXml(xmlSR, XmlReadMode.Auto); //忽视任何内联架构，从数据推断出强类型架构并加载数据
                    //ds.ReadXml(xmlSR, XmlReadMode.Auto);
                    //如果无法推断，则解释成字符串数据
                    if (ds.Tables.Count > 0)
                    {
                        return ds;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 根据申请单号获取申请单详细信息
        /// </summary>
        /// <returns></returns>
        public static RequestInfoResult GetRequestionBySheetId(string sheetId)
        {
            //  RequestNoteSoap ws = new RequestNoteSoapClient();

            Request req = new Request();
            req.requestHeader = new RequestHeader();
            req.requestHeader.sender = "2.16.840.1.113883.4.487.2.1.9";
            req.requestHeader.receiver = "2.16.840.1.113883.4.487.2.1.3";
            req.requestHeader.requestTime = DateTime.Now.ToString("yyyyMMddHH24mmss");
            req.requestHeader.msgType = "RequisitionFind";
            req.requestHeader.msgId = "SPEC20140909000009";
            req.requestHeader.msgPriority = "Normal";
            req.requestHeader.msgVersion = "1.0";

            req.requestBody =
                $@"        <RequisitionFind>
	                                <SheetID>{sheetId}</SheetID>
                                <ExamID></ExamID>
	                                </RequisitionFind>";
            RequestNote note = new RequestNote();

            note.Url = f.ReadString("广州中山附一", "平台服务url", "http://168.168.252.112:4463/RequestNote");
            log.WriteMyLog("尝试从电子申请单WebService获取信息,入参:\r\n" + req.requestBody
                           + "\r\nUrl:" + note.Url);

            RequestInfoResult result = null;
            try
            {
                var respones = note.RequisitionFind(req);
                log.WriteMyLog("获取电子申请单信息成功:" + respones.responseBody);
                result = XmlUtil.Deserialize<RequestInfoResult>(respones.responseBody);
            }
            catch (Exception e)
            {
                log.WriteMyLog("从电子申请单WebService获取信息失败:\r\n" + e);
            }

            //SheetID(电子申请单ID)为空,代表没找到申请单
            if (string.IsNullOrEmpty(result.SheetID))
                result = null;

            //2017年11月13日 岱嘉张继松:
            //当是门诊 +特诊+科室不是内镜中心（代码7100）的科室，送检科室全部匹配成特诊门诊，因为这部分报告是需要送到特诊门诊中心的。
            if (result != null && (result.PatientStyle == "0" && result.HTH == "82" && result.DepartMentID != "7100"))
            {
                result.DepartMent = "特诊门诊";
                result.DepartMentID = "6160";
            }


            return result;
        }

        /// <summary>
        /// 在视图中找住院病人信息
        /// </summary>
        /// <param name="operType">1-住院 0-门诊</param>
        /// <param name="patientNo"></param>
        /// <returns></returns>
        public static RequestInfoResult GetInpatientFromOtherInterface(string patientNo)
        {
            RequestInfoResult result = null;

            #region 住院获取

            {
                string sql =
                    $@"select * from ats_guest.INPATIENT  where 1=1 and PATIENT_NO='{patientNo}'
                    and rownum=1 order by oper_date desc";
                log.WriteMyLog("尝试获取住院视图患者信息,sql:\r\n" + sql);
                var dt = OdbcOracleHelper.GetTable(odbcConnStr, sql);
                if (dt.Rows.Count > 0)
                {
                    var dr = dt.Rows[0];
                    result = new RequestInfoResult();

                    result.PatientStyle = "2"; //住院
                    result.InHospitalID = dr["PATIENT_NO"].ToString();
                    // result.SheetID = dr["INPATIENT_NO"].ToString();
                    result.JZLSH = dr["INPATIENT_NO"].ToString();
                    result.PatientNo = patientNo;
                    result.PatientName = dr["NAME"].ToString();
                    result.PatientSex = dr["SEX_CODE"].ToString();
                    try
                    {
                        var birthdate = Convert.ToDateTime(dr["BIRTHDATE"]);
                        result.Patientage = (DateTime.Now.Year - birthdate.Year).ToString();
                    }
                    catch
                    {
                    }
                    result.PatientBedNum = dr["BED_NO"].ToString();
                    result.DepartMent = dr["DEPT_NAME"].ToString();
                    result.LinChuangZhenDuan = dr["DIAG_NAME1"].ToString();
                    result.DetailPatientStyleText = dr["PACT_NAME"].ToString();
                    result.PatientAddress = dr["HOME"].ToString();
                    result.PatientTel = dr["HOME_TEL"].ToString();
                    result.ReqSheetDoctor = dr["CHARGE_DOC_NAME"].ToString();
                }
                else
                {
                    result = null; //住院没找到,结果设置为null,继续用门诊接口找
                }
            }

            #endregion

            if (result == null)
            {
                #region 门诊获取

                string sql2 =
                    $@"select * from ats_guest.V_MEC_OUTPATIENTINFO 
    where MZH='{patientNo}' and YXBZ='1' and rownum=1 order by GHRQ desc";

                log.WriteMyLog("尝试获取门诊视图信息,sql:\r\n" + sql2);

                var dt2 = OdbcOracleHelper.GetTable(odbcConnStr, sql2);
                if (dt2.Rows.Count > 0)
                {
                    var dr2 = dt2.Rows[0];
                    result = new RequestInfoResult();

                    result.PatientNo = patientNo;
                    result.PatientStyle = "0"; //门诊
                    result.OutHospitalID = dr2["MZH"].ToString();
                    result.JZLSH = dr2["MZH"].ToString();
                    //  result.SheetID = dr["INPATIENT_NO"].ToString();
                    //  result.PatientNo = dr["PATIENT_NO"].ToString();
                    result.PatientName = dr2["XM"].ToString();
                    result.PatientSex = dr2["XB"].ToString();
                    try
                    {
                        var birthdate = Convert.ToDateTime(dr2["CSRQ"]);
                        result.Patientage = (DateTime.Now.Year - birthdate.Year).ToString();
                    }
                    catch
                    {
                    }
                    result.DepartMent = dr2["KSM"].ToString();
                    result.DetailPatientStyleText = dr2["HTDWM"].ToString();
                    result.PatientAddress = dr2["LXDZ"].ToString();
                    result.PatientTel = dr2["LXDH"].ToString();
                    //result.ReqSheetDoctor = dr["CHARGE_DOC_NAME"].ToString();
                }

                #endregion
            }

            if (result == null)
            {
                #region 体检获取

                WebReferenceForDaiJiaTiJian.Request req = new WebReferenceForDaiJiaTiJian.Request();

                req.requestHeader = new WebReferenceForDaiJiaTiJian.RequestHeader();
                req.requestHeader.sender = "2.16.840.1.113883.4.487.2.1.10";
                req.requestHeader.receiver = "2.16.840.1.113883.4.487.2.1.4";
                req.requestHeader.requestTime = DateTime.Now.ToString("yyyyMMddHH24mmss");
                req.requestHeader.msgType = "physicalexam";
                req.requestHeader.msgId = "MN20140909000009";
                req.requestHeader.msgPriority = "Normal";
                req.requestHeader.msgVersion = "1.0.0";

                req.requestBody =
                    $@"        <![CDATA[
<PATIENT_ID>{patientNo}</PATIENT_ID>      
]]> ";

                MessageRouteServiceSoapImplService sr =
                    new MessageRouteServiceSoapImplService();
                sr.Url = f.ReadString("广州中山附一", "体检服务url",
                    "http://168.168.252.112:14112/notice-server/services/MessageRouteServiceSoap");

                log.WriteMyLog("尝试从体检WebService获取信息,入参:\r\n" + req.requestBody
                               + "\r\nUrl:" + sr.Url);

                try
                {
                    Response respones = sr.sendMessageSync(req);
                    log.WriteMyLog("获取体检电子申请单信息成功:" + respones.responseBody);
                    var responseBody = respones.responseBody.Trim().Replace(@"<List>", "").Replace(@"</List>", "");
                    var exam = XmlUtil.Deserialize<physicalexam>(responseBody);
                    result = exam.GetRequestInfoResult();
                }
                catch (Exception e)
                {
                    log.WriteMyLog("从电子申请单WebService获取信息失败:\r\n" + e);
                }

                //SheetID(电子申请单ID)为空,代表没找到申请单
                if (string.IsNullOrEmpty(result.SheetID))
                    result = null;

                return result;

                #endregion
            }


            return result;
        }

        /// <summary>
        /// 获取标本列表XML
        /// </summary>
        /// <param name="result">申请单实体</param>
        /// <returns></returns>
        private string GetExamList(RequestInfoResult result)
        {
            //根据配置,是否读取标本信息,0否 1是
            string getExam = f.ReadString(ConfigSection, "是否读取标本信息", "0");
            if (getExam == "0")
                return "";

            //如果没有检查部位,返回空
            if (result == null || result.ExamList == null || result.ExamList.Count == 0)
                return "";

            //有些送检科室的标本部位和取材名称写反了,需要特殊处理
            bool isSwitchDept = GetIsSwitchDept(result);

            result.ExamList.Sort(CompareById);

            string BBLB_XML = "<BBLB>";

            var index = 1;
            foreach (Exam exam in result.ExamList)
            {
                BBLB_XML = BBLB_XML + "<row ";
                BBLB_XML = BBLB_XML + "F_BBXH=" + (char)34 + exam.id + (char)34 + " ";
                BBLB_XML = BBLB_XML + "F_BBTMH=" + (char)34 + exam.ExamID + (char)34 + " ";
                //2017年9月26日 姚迁要求把取材部位和名称倒过来,因为病理科主要看取材部位,但系统主要显示的是名称
                //2017年10月12日 季海军要求再改回来
                //2017年10月30日 取材部位和标本名称,根据sz的配置的科室进行对调
                if (isSwitchDept == false)
                {
                    exam.Description = index + "." + exam.Description;

                    BBLB_XML = BBLB_XML + "F_BBMC=" + (char) 34 + exam.Description + (char) 34 + " ";
                    BBLB_XML = BBLB_XML + "F_CQBW=" + (char) 34 + exam.qucaidengjin + (char) 34 + " ";
                }
                else
                {
                    exam.qucaidengjin = index + "." + exam.qucaidengjin;

                    BBLB_XML = BBLB_XML + "F_BBMC=" + (char) 34 + exam.qucaidengjin + (char) 34 + " ";
                    BBLB_XML = BBLB_XML + "F_CQBW=" + (char) 34 + exam.Description + (char) 34 + " ";
                }
                index++;
                BBLB_XML = BBLB_XML + "F_BZ=" + (char)34 + "" + (char)34 + " ";
                BBLB_XML = BBLB_XML + "F_LTSJ=" + (char)34 + "" + (char)34 + " ";
                BBLB_XML = BBLB_XML + "F_GDSJ=" + (char)34 + "" + (char)34 + " ";
                BBLB_XML = BBLB_XML + "F_JSSJ=" + (char)34 + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + (char)34 +
                           " ";
                //接收人,从配置文件读取
                BBLB_XML = BBLB_XML + "F_JSY=" + (char)34 + CommonParams.SampleReceivePersonName + (char)34 + " ";
                BBLB_XML = BBLB_XML + "F_BBZT=" + (char)34 + "" + (char)34 + " ";
                BBLB_XML = BBLB_XML + "F_BBPJ=" + (char)34 + "" + (char)34 + " ";
                BBLB_XML = BBLB_XML + "F_PJR=" + (char)34 + "" + (char)34 + " ";
                BBLB_XML = BBLB_XML + "F_PJSJ=" + (char)34 + "" + (char)34 + " ";
                BBLB_XML = BBLB_XML + "/>";
            }
            BBLB_XML = BBLB_XML + "</BBLB>";

            return BBLB_XML;
        }

        private static bool GetIsSwitchDept(RequestInfoResult result)
        {
            var specDepts = f.ReadString("savetohis", "对换取材部位和名称的科室列表", "").Split('|');
            var isSwitchDept = false;
            foreach (string specDept in specDepts)
            {
                if (specDept.Trim() == result.DepartMent)
                {
                    isSwitchDept = true;
                    break;
                }
            }

            return isSwitchDept;
        }

        private int CompareById(Exam x, Exam y)
        {
            return String.Compare(x.id, y.id, StringComparison.Ordinal);
        }
    }
}