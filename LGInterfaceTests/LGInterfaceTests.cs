using Microsoft.VisualStudio.TestTools.UnitTesting;
using LGInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LGInterface.GuangZhouZhongShan1St;
using LGInterface.Model;
using LGInterface.Util;

namespace LGInterface.Tests
{
    [TestClass()]
    public class LGInterfaceTests : Form
    {
        [TestMethod()]
        public void LGGetHISINFOTest()
        {
            string patientNumber = "0001170018";

            LGInterface lg = new LGInterface();
            string xml1 = lg.LGGetHISINFO("", "住院号", "20054116", "by", "by");
            Console.WriteLine("电子申请单:" + xml1);
            string xml2 = lg.LGGetHISINFO("", "住院号", "0001170018", "by", "by");
            Console.WriteLine("数据库门诊:" + xml2);
            string xml3 = lg.LGGetHISINFO("", "门诊号", "0020406269", "by", "by");
            Console.WriteLine("数据库住院:" + xml3);
            string xml4 = lg.LGGetHISINFO("", "体检号", "TJ1039202580", "by", "by");
            Console.WriteLine("数据库住院:" + xml4);
        }

        [TestMethod()]
        public void ShowApplicationSelectorTest()
        {
            #region TestData
            //电子申请单返回的xml
            var sxml = @"<RequestInfoResult><id>22715</id><SystemIn>1</SystemIn><SheetID>10001143</SheetID><JZLSH>37128713</JZLSH><NeedSchedule>0</NeedSchedule><PatientStyle>0</PatientStyle><GHStyle>0</GHStyle><DetailPatientStyle>0</DetailPatientStyle><OutHospitalID>0022285042</OutHospitalID><PatientName>郑发荣</PatientName><PatientSex>1</PatientSex><Patientage>67</Patientage><PatientTel>18680528150</PatientTel><binglizhaiyao>病历摘要</binglizhaiyao><shoushusuojian>手术所见</shoushusuojian><LinChuangZhenDuan>鼻部小肿物</LinChuangZhenDuan><DepartMentID>6180</DepartMentID><DepartMent>特诊眼科</DepartMent><ReqSheetDoctorID>002427</ReqSheetDoctorID><ReqSheetDoctor>杨国奋</ReqSheetDoctor><ReqSheetDate>20170726</ReqSheetDate><ReqSheetTime>111955</ReqSheetTime><ExamPlace>内镜中心</ExamPlace><RequestState>0</RequestState><inhospitaltimes>0</inhospitaltimes><ExamList><Exam><id>45905</id><SystemIn>1</SystemIn><SheetID>10001143</SheetID><ExamID>10001143-001</ExamID><Description>测试送检物1</Description><qucaidengjin>测试取材部位1</qucaidengjin></Exam><Exam><id>45906</id><SystemIn>1</SystemIn><SheetID>10001143</SheetID><ExamID>10001143-002</ExamID><Description>测试取材部位2</Description><qucaidengjin>测试取材部位2</qucaidengjin></Exam></ExamList><FeeList><Fee><id>95114</id><SystemIn>1</SystemIn><SheetID>10001143</SheetID><FEEID>270300003</FEEID><FEENAME>局部切除组织活检检查与诊断</FEENAME><FeePrice>230.0</FeePrice><FEENum>1</FEENum><ItemCode>F00000102789</ItemCode><DrugFlag>0</DrugFlag></Fee><Fee><id>95115</id><SystemIn>1</SystemIn><SheetID>10001143</SheetID><FEEID>270800005</FEEID><FEENAME>病理大体标本摄影</FEENAME><FeePrice>125.0</FeePrice><FEENum>2</FEENum><ItemCode>F00000102839</ItemCode><DrugFlag>0</DrugFlag></Fee><Fee><id>95116</id><SystemIn>1</SystemIn><SheetID>10001143</SheetID><FEEID>270800006</FEEID><FEENAME>显微摄影术</FEENAME><FeePrice>25.0</FeePrice><FEENum>3</FEENum><ItemCode>F00000102840</ItemCode><DrugFlag>0</DrugFlag></Fee><Fee><id>95117</id><SystemIn>1</SystemIn><SheetID>10001143</SheetID><FEEID>270300005*1</FEEID><FEENAME>手术标本检查与诊断(超过两个蜡块每增加一个加</FEENAME><FeePrice>50.0</FeePrice><FEENum>0</FEENum><ItemCode>F00000102796</ItemCode><DrugFlag>0</DrugFlag></Fee><Fee><id>95118</id><SystemIn>1</SystemIn><SheetID>10001143</SheetID><FEEID>270400002</FEEID><FEENAME>快速石蜡切片检查与诊断</FEENAME><FeePrice>1000.0</FeePrice><FEENum>0</FEENum><ItemCode>F00000102813</ItemCode><DrugFlag>0</DrugFlag></Fee><Fee><id>95119</id><SystemIn>1</SystemIn><SheetID>10001143</SheetID><FEEID>270300005</FEEID><FEENAME>手术标本检查与诊断</FEENAME><FeePrice>300.0</FeePrice><FEENum>0</FEENum><ItemCode>F00000102795</ItemCode><DrugFlag>0</DrugFlag></Fee><Fee><id>95120</id><SystemIn>1</SystemIn><SheetID>10001143</SheetID><FEEID>270200001</FEEID><FEENAME>体液细胞学检查与诊断</FEENAME><FeePrice>157.5</FeePrice><FEENum>0</FEENum><ItemCode>F00000102722</ItemCode><DrugFlag>0</DrugFlag></Fee><Fee><id>95121</id><SystemIn>1</SystemIn><SheetID>10001143</SheetID><FEEID>270500002*1</FEEID><FEENAME>全自动单独温控法加收-免疫组织化学染色诊断</FEENAME><FeePrice>407.5</FeePrice><FEENum>0</FEENum><ItemCode>F00000442252</ItemCode><DrugFlag>0</DrugFlag></Fee><Fee><id>95122</id><SystemIn>1</SystemIn><SheetID>10001143</SheetID><FEEID>270500002</FEEID><FEENAME>免疫组织化学染色诊断</FEENAME><FeePrice>345.0</FeePrice><FEENum>0</FEENum><ItemCode>F00000102817</ItemCode><DrugFlag>0</DrugFlag></Fee><Fee><id>95123</id><SystemIn>1</SystemIn><SheetID>10001143</SheetID><FEEID>270500001</FEEID><FEENAME>特殊染色及酶组织化学染色诊断</FEENAME><FeePrice>125.0</FeePrice><FEENum>0</FEENum><ItemCode>F00000102816</ItemCode><DrugFlag>0</DrugFlag></Fee><Fee><id>95124</id><SystemIn>1</SystemIn><SheetID>10001143</SheetID><FEEID>270800007</FEEID><FEENAME>疑难病理会诊</FEENAME><FeePrice>375.0</FeePrice><FEENum>0</FEENum><ItemCode>F00000102841</ItemCode><DrugFlag>0</DrugFlag></Fee><Fee><id>95125</id><SystemIn>1</SystemIn><SheetID>10001143</SheetID><FEEID>270800004</FEEID><FEENAME>液基薄层细胞制片术</FEENAME><FeePrice>500.0</FeePrice><FEENum>0</FEENum><ItemCode>F00000102836</ItemCode><DrugFlag>0</DrugFlag></Fee><Fee><id>95126</id><SystemIn>1</SystemIn><SheetID>10001143</SheetID><FEEID>270400002-2</FEEID><FEENAME>快速细胞病理诊断</FEENAME><FeePrice>1000.0</FeePrice><FEENum>0</FEENum><ItemCode>F00000221365</ItemCode><DrugFlag>0</DrugFlag></Fee><Fee><id>95127</id><SystemIn>1</SystemIn><SheetID>10001143</SheetID><FEEID>270200004</FEEID><FEENAME>脱落细胞学检查与诊断</FEENAME><FeePrice>125.0</FeePrice><FEENum>0</FEENum><ItemCode>F00000102755</ItemCode><DrugFlag>0</DrugFlag></Fee><Fee><id>95128</id><SystemIn>1</SystemIn><SheetID>10001143</SheetID><FEEID>270200000*1</FEEID><FEENAME>细胞病理学检查与诊断(超过两张涂(压)片加收)</FEENAME><FeePrice>50.0</FeePrice><FEENum>0</FEENum><ItemCode>F00000102721</ItemCode><DrugFlag>0</DrugFlag></Fee><Fee><id>95129</id><SystemIn>1</SystemIn><SheetID>10001143</SheetID><FEEID>270400001</FEEID><FEENAME>冰冻切片检查与诊断</FEENAME><FeePrice>750.0</FeePrice><FEENum>0</FEENum><ItemCode>F00000102811</ItemCode><DrugFlag>0</DrugFlag></Fee><Fee><id>95130</id><SystemIn>1</SystemIn><SheetID>10001143</SheetID><FEEID>270400001-1</FEEID><FEENAME>冰冻切片检查与诊断(特异性感染标本)</FEENAME><FeePrice>875.0</FeePrice><FEENum>0</FEENum><ItemCode>F00000102812</ItemCode><DrugFlag>0</DrugFlag></Fee></FeeList><CollectionID>002427</CollectionID><Collection>杨国奋</Collection><CollectionDate>20170726</CollectionDate><CollectionTime>111955</CollectionTime><IBDEmbedding>1</IBDEmbedding><IsEmergent>0</IsEmergent><HTH>82</HTH><PrimaryNurse>负责留标本护士</PrimaryNurse><PAYRESULT>1</PAYRESULT><RoomNumber>22</RoomNumber><SpecimenType>2</SpecimenType></RequestInfoResult>";

            #endregion

            var result = XmlUtil.Deserialize<RequestInfoResult>(sxml);
            //            ApplicationSelector selector = new ApplicationSelector();
            //            selector.Table = dt;
            //            selector.ShowDialog(this);
        }

        /// <summary>
        /// 根据申请单号获取申请单详细信息
        /// </summary>
        /// <returns></returns>
        [TestMethod()]
        public void GetRequestionBySheetId()
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

            req.requestBody = $@"        <RequisitionFind>
	                                <SheetID>{"10001143"}</SheetID>
                                <ExamID></ExamID>
	                                </RequisitionFind>";
            RequestNote note = new RequestNote();
            var result = note.RequisitionFind(req);
            Console.WriteLine(result);
            // return result;

            var resObj=LGInterface.GetRequestionBySheetId("10001143");
        }

        [TestMethod]
        public void ShowWebServiceUrl()
        {
            RequestNote note = new RequestNote();
            var url = note.Url;
            Console.WriteLine(url);
        }

        [TestMethod()]
        public void GetInpatientFromOtherInterfaceTest()
        {
            LGInterface.GetInpatientFromOtherInterface("TJ1039202580");
        }


        [TestMethod()]
        public void ComapreStringTest()
        {
            var lst = new List<string>();
            lst.Add("1");
            lst.Add("2");
            lst.Add("4");
            lst.Add("3");
            lst.Add("5");

            lst.Sort(CompareString);
        }

        private int CompareString(string x, string y)
        {
            return String.Compare(x, y, StringComparison.Ordinal);
        }
    }
}