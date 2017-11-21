using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Schema;
using dbbase;
using Lg.Pdf2Jpg;
using Maticsoft.DAL;
using SendPisResult.DAL;
using SendPisResult.Models;
using SendPisResult.Util;
using SendPisResult.WebReferenceCrisis;

namespace SendPisResult.ISendPisResult.Impl.广州中山附一_上海岱嘉
{
    [HospName("广州中山附一_上海岱嘉")]
    public class ResultSender : ISendPisResult,ISendPisResultPlus
    {
        /// <summary>
        /// APGate ODBC connection string
        /// </summary>
        private string connStr = "";

        IniFiles f = new IniFiles("sz.ini");
        private string ConfigSection = "广州中山附一";
        dbbase.odbcdb aa = new odbcdb("DSN=pathnet;UID=pathnet;PWD=4s3c2a1p", "", "");
        private string _bgxh = "";
        private ReportType _reportType;

        T_JCXX jcxx = null;
        T_BDBG bdbg = null;
        T_BCBG bcbg = null;

        public ResultSender()
        {
            connStr = f.ReadString(ConfigSection, "ODBC报告回传", "");
        }


        /// <summary>
        /// 该接口要撤销报告,不要使用这个方法
        /// </summary>
        /// <param name="jcxx"></param>
        public void SendResult(string pathoNo)
        {
            T_JCXX jcxx = null;

            try
            {
                jcxx = new T_JCXX_DAL().GetModel(pathoNo);
            }
            catch (Exception e)
            {
                throw new Exception("没有找到该结果,病理号为:" + pathoNo);
            }

            if (jcxx == null)
                throw new Exception("没有找到该结果,病理号为:" + pathoNo);

            log.WriteMyLog("进入sendResult" + jcxx.F_SJKS);

            SaveReport(jcxx);
        }



        /// <summary>
        /// 发送报告--高级接口,比普通接口多了患者注册,取消审核时撤回报告
        /// </summary>
        /// <param name="pathoNo"></param>
        /// <param name="reportType"></param>
        /// <param name="bgxh"></param>
        /// <param name="editType"></param>
        /// <param name="pisAction"></param>
        /// <param name="yymc"></param>
        public void SendResult(string pathoNo, ReportType reportType, string bgxh, EditType editType, PisAction pisAction,
            string yymc)
        {
            _reportType = reportType;
            _bgxh = bgxh;

            try
            {
                jcxx = new T_JCXX_DAL().GetModel(pathoNo);
                bdbg = new T_BDBG_DAL().GetByBlhAndBgxh(pathoNo, _bgxh);
                bcbg=new T_BCBG_DAL().GetByBlhAndBgxh(pathoNo, _bgxh);
                TrimHelper.TrimAllPropertities(jcxx);
            }
            catch (Exception e)
            {
                throw new Exception("没有找到该结果,病理号为:" + pathoNo);
            }

            if (jcxx == null)
                throw new Exception("没有找到该结果,病理号为:" + pathoNo);

            if (jcxx.F_MZH.Trim() + jcxx.F_ZYH.Trim() == "")
                jcxx.F_MZH = "SGD" + jcxx.F_BLH;

            log.WriteMyLog("PisAction="+pisAction+",bgzt="+jcxx.F_BGZT);
            switch (pisAction)
            {
                case PisAction.新登记:
                case PisAction.保存:
                    //SavePerson(jcxx);
                    SaveReport(jcxx);//判断jcxx是否已审核,已审核就会上传报告
                    break;
                case PisAction.取消审核:
                    AbandonReport(jcxx);
                    break;
                case PisAction.打印:
                    break;
                case PisAction.取消打印:
                    break;
                case PisAction.未知:
                    break;
                default:break;
            }
            //病人信息注册

        }

        /// <summary>
        /// 如果jcxx已审核,则上传报告
        /// </summary>
        /// <param name="jcxx"></param>
        private void SaveReport(T_JCXX jcxx)
        {
            var bgzt = "";
            if(jcxx?.F_BGZT =="已审核" || bdbg?.F_BD_BGZT=="已审核"||bcbg?.F_BC_BGZT=="已审核")
            {
                log.WriteMyLog("开始回传报告到集成平台!");

                //检查前置机中数据是否已经存在(重复审核)
                //如果已存在,则不会重复注册病人,并且申请单和报告回传的标记是UPDATE
                bool isExist = true;
                string editType = isExist ? "UPDATE" : "ADD";
                SaveReport(jcxx, editType);

                log.WriteMyLog("成功回传报告到集成平台!");

                try
                {
                    if (string.IsNullOrEmpty(jcxx.F_BZ.Trim()) == false)
                    {
                        log.WriteMyLog("发现危急值,开始上报危急值到集成平台,病理号:"+jcxx.F_BLH);
                        SendCrisis(jcxx);
                    }
                }
                catch (Exception e)
                {
                    log.WriteMyLog("危急值上传失败:"+e);
                }
            }
        }

        /// <summary>
        /// 发送危急值到平台
        /// </summary>
        /// <param name="jcxx"></param>
        private void SendCrisis(T_JCXX jcxx)
        {
            //检查配置中是否开启了回传危急值功能,默认是开启
            var hcwjz = f.ReadString("savetohis", "hcwjz", "1");
            if (hcwjz.Trim() != "1") return;

            //先询问是否上报
            var r = MessageBox.Show($"发现危急值:{jcxx.F_BZ},是否上报到平台?", "", MessageBoxButtons.YesNo);
            if (r != DialogResult.Yes)
            {
                log.WriteMyLog("用户选择不上报危急值,病理号:"+jcxx.F_BLH);
                return;
            }

            var ksbm = f.ReadString("savetohis", "blksbm", "9991");
            var url = f.ReadString("savetohis", "wjzurl", "");
            if (url.Trim() == "")
            {
                MessageBox.Show("没有找到上报危急值的url,无法上报危急值,请联系管理员!");
                return;
            }

            Request req=new Request();
            req.requestHeader = new RequestHeader();
            req.requestHeader.sender = "2.16.840.1.113883.4.487.6.1.6";
            req.requestHeader.receiver = "2.16.840.1.113883.4.487.6.1.4";
            req.requestHeader.requestTime = DateTime.Now.ToString("yyyyMMddHH24mmss");
            req.requestHeader.msgType = "CrisisPathology";
            req.requestHeader.msgId = "CRI20140909000009";
            req.requestHeader.msgPriority = "Normal";
            req.requestHeader.msgVersion = "1.0";

            //f_by2=开单科室ID||开单科室名称
            var deps = jcxx.F_BY2.Split(new string[] {"||"}, StringSplitOptions.None);
            string depName="", depId="";
            if (deps.Length == 2)
            {
                depName = deps[1];
                depId = deps[0];
            }

            req.requestBody =
    $@"      <List>
        <CrisisPathology>
	<RequestNo>{jcxx.F_SQXH}</RequestNo>
	<AbnormalValue>{jcxx.F_BZ}</AbnormalValue>
	<PublishTime>{jcxx.F_SPARE5}</PublishTime>
	<CreateTime>{jcxx.F_SPARE5}</CreateTime>
	<IsCancel>0</IsCancel>
	<IsDeleted>0</IsDeleted>
	<IsDisposeTimeOut>0</IsTimeOut>
	<IsReceiveTimeOut>0</IsTimeOut>
	<ItemResult>{jcxx.F_BLZD}</ItemResult>
	<Status>0</Status>
	<NurseName></NurseName>
	<OrderDoctorCode></OrderDoctorCode>
	<OrderDoctorNAME>{jcxx.F_SJYS}</OrderDoctorNAME>
	<PatientAddress>{jcxx.F_LXXX}</PatientAddress>
	<PatientAge>{jcxx.F_NL}</PatientAge>
	<PatientName>{jcxx.F_XM}</PatientName>
	<Machine></Machine>
	<Body>{jcxx.F_BBMC}</Body>
	<CheckMethod>{jcxx.F_BBLX}</CheckMethod>
	<PatientNo>{jcxx.F_ZYH.Trim()+jcxx.F_MZH.Trim()}</PatientNo>
	<PatientPhone>{jcxx.F_LXXX}</PatientPhone>
	<PatientType>{GetCrisisBrlbCode(jcxx)}</PatientType>
	<ReceiveDeptCode>{depId}</ReceiveDeptCode>
	<ReceiveDeptName>{depName}</ReceiveDeptName>
	<RegisterDate>{jcxx.F_SDRQ+":00"}</RegisterDate>
	<RegisterDeptName>{depName}</RegisterDeptName>
	<SampleID>{jcxx.F_BLH}</SampleID>
	<SampleReceiverName>{jcxx.F_JSY}</SampleReceiverName>
	<SampleReceiveTime>{jcxx.F_SDRQ}</SampleReceiveTime>
	<SampleSource></SampleSource>
	<SampleTime></SampleTime>
	<SendDeptCode>{"9991"}</SendDeptCode>
	<SendDeptName>{"病理科"}</SendDeptName>
	<VerifierCode></VerifierCode>
	<VerifierName>{jcxx.F_FZYS}</VerifierName>
	<TestName>{jcxx.F_BBLX}</TestName>
	<UpdateBySystem>4</UpdateBySystem>
    <SendSystem>4</SendSystem>
	<UpdateTime>{jcxx.F_SPARE5}</UpdateTime>
	<OperationList>
	<Operation>
	<CreateTime>创建时间</CreateTime>
	<IsPush>1</IsPush>
	<PushTime>{jcxx.F_SPARE5}</PushTime>
	<Status>0</Status>
	<Remark></Remark>
	<OperationInfo>病理发布危急值</OperationInfo>
	<OperationSystem>4</OperationSystem>
	<OperationType0</OperationType>
	<OperationTypeName>发布危机值</OperationTypeName>
	<PrincipalCode></PrincipalCode>
	<PrincipalName></PrincipalName>
	<ResponseAccoutID>{jcxx.F_SHYS}</ResponseAccoutID>
	<ResponseCode></ResponseCode>
	<ResponseMemo></ResponseMemo>
	<ResponseName>{jcxx.F_FZYS}</ResponseName>
	<ResponseTime>{jcxx.F_SPARE5}</ResponseTime>
	<ResponseType>22</ResponseType>
	<ResponseTypeName>符合病情，必须处理</ResponseTypeName>
	</Operation>
	</OperationList>
	</CrisisPathology>
      </List>
";
            string wjzDebug = f.ReadString("savetohis", "wjzdebug", "0");
            if (wjzDebug.Trim() == "1")
                log.WriteMyLog("危急值xml:\r\n"+req.requestBody);

            CrisisValue cv= new CrisisValue();
            cv.Url = url;
            try
            {
                var respose = cv.CrisisAdd(req);
                log.WriteMyLog("危急值上报成功!");
            }
            catch (Exception e)
            {
                MessageBox.Show("危急值上报失败:" + e);
                log.WriteMyLog("危急值上报失败:"+e);
                return;
            }
        }

        private string GetCrisisBrlbCode(T_JCXX jcxx)
        {
            var lb = jcxx.F_BRLB.Replace("内镜", "").Replace(")", "").Replace("(", "").Replace("（", "").Replace("）", "");
            switch (lb)
            {
                case "门诊":
                    return "1";break;
                case "急诊":
                    return "2"; break;
                case "住院":
                    return "4"; break;
                default:
                    return "";
            }
        }

        /// <summary>
        /// 作废平台报告
        /// </summary>
        /// <param name="jcxx"></param>
        private void AbandonReport(T_JCXX jcxx)
        {
            SaveReport(jcxx, "ABANDON");
        }

        /// <summary>
        /// 上传报告到平台
        /// </summary>
        /// <param name="jcxx"></param>
        /// <param name="editType">ADD,UPDATE,ABANDON</param>
        private void SaveReport(T_JCXX jcxx, string editType)
        {
            log.WriteMyLog("开始结果上传到平台,编辑类型:"+editType.ToString());

            log.WriteMyLog("开始生成PDF");
            var base64 = "";
            var pdfFullName = "";
            try
            {
                pdfFullName = CreatePDF(jcxx.F_BLH,_bgxh, _reportType, false, ref base64, "1");
            }
            catch (Exception e)
            {
                log.WriteMyLog("生成PDF失败:" + e);
            }
            string uuid = Guid.NewGuid().ToString();
            var qcmxList = new T_QCMX_DAL().GetListByBLH(jcxx.F_BLH);
            var qpList = new T_QP_DAL().GetListByBLH(jcxx.F_BLH);
            var txList = new T_TX_DAL().GetList($" F_BLH='{jcxx.F_BLH}' and F_SFDY='1' ");
            var xmlDocId = GetNewDocId();
            var pdfDocId = GetNewDocId();
            var ftphelper = new FtpHelper();

            #region sqlInsert DGATE_DOCUMENT_INFO

            #region PISRequestXml  没用上

            string PISRequestXml =
                $@"<PISRequest>
	                    <ApplyNo>{jcxx.F_SQXH}</ApplyNo>
	                    <PatientNo>{jcxx.F_ZYH}</PatientNo>
	                    <PatientId>{jcxx.F_ZYH}</PatientId>
	                    <Name>{jcxx.F_XM}</Name>
	                    <Sex>{jcxx.GenderCode}</Sex>
	                    <SexCodeSystem>2.16.840.1.113883.4.487.2.1.1.1.9</SexCodeSystem>
	                    <SexName>{jcxx.F_XB}</SexName>
	                    <BirthDay>{jcxx.GetBirthDateByAge()}</BirthDay>
	                    <BloodType></BloodType>
	                    <BloodTypeSystem></BloodTypeSystem>
	                    <BloodTypeName></BloodTypeName>
	                    <Nation></Nation>
	                    <Country></Country>
	                    <CountryCodeSystem></CountryCodeSystem>
	                    <CountryName></CountryName>
	                    <Native></Native>
	                    <Idenno></Idenno>
	                    <Home>{jcxx.F_LXXX}</Home>
	                    <HomeZip></HomeZip>
	                    <HomeTel></HomeTel>
	                    <Height></Height>
	                    <Weight></Weight>
	                    <Profcode></Profcode>
	                    <Profname></Profname>
	                    <Profcodesystem></Profcodesystem>
	                    <Roomno></Roomno>
	                    <Bedno>{jcxx.F_CH}</Bedno>
	                    <Deptcode></Deptcode>
	                    <Deptname>{jcxx.F_SJKS}</Deptname>
	                    <Doccode></Doccode>
	                    <Docname>{jcxx.F_SJYS}</Docname>
	                    <Checkbodycode></Checkbodycode>
	                    <Checkbody>{jcxx.F_BBMC}</Checkbody>
                    <CheckbodyCodesystem></CheckbodyCodesystem>
	                    <Checkorder>{jcxx.F_YZXM}</Checkorder>
                    <CheckorderCode></CheckorderCode>
	                    <TotCost></TotCost>
	                    <Operateresult></Operateresult>
	                    <Auxresult>{jcxx.F_LCZD}</Auxresult>
	                    <Illnesshistory></Illnesshistory>
	                    <ParentmedicaltechorderII>{jcxx.F_YZID}</ParentmedicaltechorderII>
                    <DomainID>2.16.840.1.113883.4.487.2.1</DomainID>
	                    <Orderdatetime>{jcxx.F_SDRQ}</Orderdatetime>
	                    <Isemergency></Isemergency>
	                    <Title></Title>
	                    <Subtitle></Subtitle>
                    </PISRequest>
";

            PISRequestXml = OdbcOracleHelper.GetSafeSql(PISRequestXml);

            #endregion

            #region PISReportXML

            string PISReportXML =
                $@"
                <PISReport>
	                    <ReportII>{jcxx.F_BLH}</ReportII>
	                    <EncounterII>{jcxx.F_ZYH.Trim() + jcxx.F_MZH.Trim()}</EncounterII>
	                    <OrderIIs>{jcxx.F_SQXH}</OrderIIs>
	                    <ClinicalDiagnose>{GetXmlStr(jcxx.F_LCZD)}</ClinicalDiagnose>
	                    <ExamineNo>{jcxx.F_BLH}</ExamineNo>
	                    <ExamineName>{jcxx.F_BBLX}</ExamineName>
	                    <BodyParts>{jcxx.F_BBMC}</BodyParts>
	                    <ExamineEmployee>{jcxx.F_BGYS}</ExamineEmployee>
	                    <ExamineOn>{jcxx.F_BGRQ + ":00"}</ExamineOn>
	                    <AuditEmployee>{jcxx.F_FZYS}</AuditEmployee>
	                    <AuditOn >{jcxx.F_SPARE5}</AuditOn >
	                    <ReportOn>{jcxx.F_BGRQ+":00"}</ReportOn>
	                    <ReportNo>{jcxx.F_BLH}</ReportNo>
	                    <RowVersion></RowVersion>
	                    <PatientName>{jcxx.F_XM}</PatientName>
	                    <PatientAge>{GetXmlStr(jcxx.F_NL)}</PatientAge>
	                    <PatientSexCode>{jcxx.GenderCode}</PatientSexCode>
	                    <PatientSexName>{jcxx.F_XB}</PatientSexName>
	                    <PatientSexCodeSystem>2.16.840.1.113883.4.487.2.1.1.1.9</PatientSexCodeSystem>
	                    <PatientBirth>{jcxx.GetBirthDateByAge()}</PatientBirth>
	                    <AuthorPersonId></AuthorPersonId>
	                    <AuthorDomainId>2.16.840.1.113883.4.487.2.1.37</AuthorDomainId>
	                    <AuthorDomainName>XDS.LJPISBG</AuthorDomainName>
	                    <AuthorPersonName>{GetXmlStr(jcxx.F_BGYS)}</AuthorPersonName>
	                    <AuthenticatorPersonId></AuthenticatorPersonId>
	                    <AuthenticatorDomainId>2.16.840.1.113883.4.487.2.1.37</AuthenticatorDomainId>
	                    <AuthenticatorDomainName>LJPIS</AuthenticatorDomainName>
	                    <StudyDepartmen>{GetXmlStr(jcxx.F_SJKS)}</StudyDepartmen>
	                    <StudyDoctor>{jcxx.F_SJYS}</StudyDoctor>
	                    <StudyTime></StudyTime>
	                    <StudyMaterials>{GetXmlStr(jcxx.F_BBMC)}</StudyMaterials>
	                    <StudyRoom>{jcxx.F_BQ}</StudyRoom>
	                    <StudyBedroom>{jcxx.F_CH}</StudyBedroom>
	                    <Title>病理活体组织诊断报告书</Title>
	                    <DiagnosisTitle>病理诊断</DiagnosisTitle>
	                    <EffectiveTime>{GetXmlStr(jcxx.F_SPARE5)}</EffectiveTime>
	                    <PatientTypeCode>{GetPatCategory(jcxx)}</PatientTypeCode>
	                    <PatientType>{jcxx.F_BRLB}</PatientType>
	                    <PatientTypeCodeSystem>2.16.840.1.113883.4.487.2.1.1.1.13</PatientTypeCodeSystem>
	                    <PatientRegisterTime>{jcxx.F_SDRQ + ":00"}</PatientRegisterTime>
	                    <DiagnosisStatusCode>{GetXmlStr(GetDiagnosisStatusCode(jcxx))}</DiagnosisStatusCode>
	                    <DiagnosisMethodCode></DiagnosisMethodCode>
                    <SliceList>
                        {GetQpListXml(qpList, jcxx)}
                    </SliceList>
                    <WaxblockList>
                        {GetQcListXml(qcmxList)}
                    </WaxblockList>
                    <DiagnosisConclusion>
                        {GetPathDiagXml(jcxx)}
                    </DiagnosisConclusion>
                    <ImageList>
                    </ImageList>
                    <ImagePathList>
                        {GetImagePathList(txList, ftphelper.GetFileFtpPath(xmlDocId,true))}
                    </ImagePathList>
                    <DicomStudyUidList>
                        {GetDicomXml(txList)}
	                </DicomStudyUidList>
                </PISReport> ";

       //     PISReportXML = OdbcOracleHelper.GetSafeSql(PISReportXML);

            #endregion

            #region SqlInsertReport

            string SqlInsertReport =
              $@"
                    insert into DGATE_DOCUMENT_INFO
                      (PK,
                       DOCUMENT_UNIQUE_ID,
                       DOCUMENT_DOMAIN_ID,
                       PATIENT_ID,
                       PATIENT_DOMAIN_ID,
                       FILE_TYPE,
                       PAY_LOAD_TYPE,
                       SUB_TYPE,
                       CONTENT,
                       START_TIME,
                       EFFECTIVE_TIME,
                       HIUP_STATUS,
                       END_TIME,
                       FILE_PATH,
                       ENTRY_UUID,
                       RETRY,
                       RETRY_TIME,
                       GLOBAL_ID,
                       FILE_SYSTEM_FK,
                       HIUP_ERROR_INFO,
                       CDA_UNIQUE_ID,
                       REQUEST_NUMBER,
                       ORDER_NUMBER,
                       REPOSITORY_UNIQUE_ID,
                       REQUEST_DOMAIN,
                       ORDER_DOMAIN,
                       BEFORE_STATUS,
                       BEFORE_RETRY_TIME,
                       BEFORE_RETRY,
                       PATIENT_TYPE,
                       CUSTOM1,
                       CUSTOM2,
                       CUSTOM3,
                       CUSTOM4,
                       CUSTOM5,
                       AFTER_STATUS,
                       AFTER_RETRY_TIME,
                       AFTER_RETRY,
                       EXPORT_STATUS,
                       EXPORT_RETRY_TIME,
                       EXPORT_RETRY,
                       PAT_CATEGORY,
                       PAT_CATEGORY_SYSTEM,
                       PAT_NAME,
                       TPOS_PATH,
                       CLOB_STATUS,
                       CLOB_RETRY_TIME,
                       CLOB_RETRY)
                    values
                      ( '{xmlDocId}',
                       '{jcxx.F_BLH}',--blh
                       '2.16.840.1.113883.4.487.2.1.37',
                       '{jcxx.F_MZH.Trim() + jcxx.F_ZYH.Trim()}',--patientId
                       '{GetHisDomain(jcxx)}',
                       'TRANS-PATH-XML',
                       'XDS.LJPISBG',--XDS.PISRequest,XDS.PISBG
                       '{editType}',--ADD,UPDATE,ABANDON
                       '',--CONTENT_XML,改为返回文档,而不是插入到数据库
                       sysdate,
                       to_date('{jcxx.F_SPARE5}','yyyy-mm-dd hh24:mi:ss'),
                       '0',
                       '',
                       '',--filePath,改为空
                       '',
                       null,
                       null,
                       '{uuid}',--person uuid
                       '3',--FILE_SYSTEM_FK
                       null,
                       null,
                       '{jcxx.F_BLH}',--sqxh
                       '{jcxx.F_MZH.Trim()+jcxx.F_ZYH.Trim()}',--sqxh
                       null,
                       '2.16.840.1.113883.4.487.2.1.37',
                       '2.16.840.1.113883.4.487.2.1.4',
                       '0',--BEFORE_STATUS
                       null,
                       null,
                       null,
                       null,
                       null,
                       null,
                       null,
                       null,
                       '0',
                       null,
                       null,
                       'X0',--export_status
                       '',
                       '',
                       '{GetPatCategory(jcxx)}',--patient type
                       '2.16.840.1.113883.4.487.2.1.1.1.13',
                       '{jcxx.F_XM}',--name
                       '{ftphelper.GetFileFtpPath(xmlDocId) + "/" + xmlDocId + ".xml"}',--TPOS_PATH
                       '0',
                       null,
                       null) ";

            #endregion

            #region SqlInsertPdfFile

            string sqlInsertPdfFile =
           $@"
                    insert into DGATE_DOCUMENT_INFO
                      (PK,
                       DOCUMENT_UNIQUE_ID,
                       DOCUMENT_DOMAIN_ID,
                       PATIENT_ID,
                       PATIENT_DOMAIN_ID,
                       FILE_TYPE,
                       PAY_LOAD_TYPE,
                       SUB_TYPE,
                       CONTENT,
                       START_TIME,
                       EFFECTIVE_TIME,
                       HIUP_STATUS,
                       END_TIME,
                       FILE_PATH,
                       ENTRY_UUID,
                       RETRY,
                       RETRY_TIME,
                       GLOBAL_ID,
                       FILE_SYSTEM_FK,
                       HIUP_ERROR_INFO,
                       CDA_UNIQUE_ID,
                       REQUEST_NUMBER,
                       ORDER_NUMBER,
                       REPOSITORY_UNIQUE_ID,
                       REQUEST_DOMAIN,
                       ORDER_DOMAIN,
                       BEFORE_STATUS,
                       BEFORE_RETRY_TIME,
                       BEFORE_RETRY,
                       PATIENT_TYPE,
                       CUSTOM1,
                       CUSTOM2,
                       CUSTOM3,
                       CUSTOM4,
                       CUSTOM5,
                       AFTER_STATUS,
                       AFTER_RETRY_TIME,
                       AFTER_RETRY,
                       EXPORT_STATUS,
                       EXPORT_RETRY_TIME,
                       EXPORT_RETRY,
                       PAT_CATEGORY,
                       PAT_CATEGORY_SYSTEM,
                       PAT_NAME,
                       TPOS_PATH,
                       CLOB_STATUS,
                       CLOB_RETRY_TIME,
                       CLOB_RETRY)
                    values
                      ( '{pdfDocId}',
                       '{jcxx.F_BLH}',--blh
                       '2.16.840.1.113883.4.487.2.1.37',
                       '{jcxx.F_MZH.Trim() + jcxx.F_ZYH.Trim()}',--patientId
                       '{GetHisDomain(jcxx)}',
                       'PATH-PDF',
                       'XDS.LJPISBG',--XDS.PISRequest,XDS.PISBG
                       '{editType}',--ADD,UPDATE,ABANDON
                       '',--CONTENT_XML,改为返回文档,而不是插入到数据库
                       sysdate,
                       to_date('{jcxx.F_SPARE5}','yyyy-mm-dd hh24:mi:ss'),
                       '0',
                       '',
                       '',--filePath,改为空
                       '',
                       null,
                       null,
                       '{uuid}',--person uuid
                       '3',--FILE_SYSTEM_FK
                       null,
                       null,
                       '{jcxx.F_BLH}',--sqxh
                       '{jcxx.F_MZH.Trim() + jcxx.F_ZYH.Trim()}',--sqxh
                       null,
                       '2.16.840.1.113883.4.487.2.1.37',
                       '2.16.840.1.113883.4.487.2.1.4',
                       'PDF',--BEFORE_STATUS
                       null,
                       null,
                       null,
                       null,
                       null,
                       null,
                       null,
                       null,
                       '0',
                       null,
                       null,
                       'X0',--export_status
                       '',
                       '',
                       '{GetPatCategory(jcxx)}',--patient type
                       '2.16.840.1.113883.4.487.2.1.1.1.13',
                       '{jcxx.F_XM}',--name
                       '{ftphelper.GetFileFtpPath(xmlDocId) + "/" + xmlDocId + ".pdf"}',--TPOS_PATH
                       '0',
                       null,
                       null) ";

            #endregion


            #region SqlInsertRepotExtend

            string sqlInsertReportExtend1 = $@"insert into dgate_extend_id_info
                                              (pk,
                                               document_fk,
                                               id,
                                               domain_id)
                                            values
                                              (dgate_extend_id_info_sequence.nextval,--pk
                                               '{xmlDocId}',--v_document_fk,
                                               '{jcxx.F_ZYH.Trim()+jcxx.F_MZH.Trim()}',--v_id,
                                               '{GetHisDomain(jcxx)}')--v_domain_id     ";

            string sqlInsertReportExtend2 = $@"insert into dgate_extend_id_info
                                              (pk,
                                               document_fk,
                                               id,
                                               domain_id)
                                            values
                                              (dgate_extend_id_info_sequence.nextval,--pk
                                               '{xmlDocId}',--v_document_fk,
                                               '{jcxx.F_SQXH}',--v_id,
                                               '{GetRelevanceDomain(jcxx)}')--v_domain_id     ";
            string sqlInsertReportExtend3 = $@"insert into dgate_extend_id_info
                                              (pk,
                                               document_fk,
                                               id,
                                               domain_id)
                                            values
                                              (dgate_extend_id_info_sequence.nextval,--pk
                                               '{xmlDocId}',--v_document_fk,
                                               '{jcxx.F_BLH}',--v_id,
                                               '2.16.840.1.113883.4.487.2.1.37')--v_domain_id     ";

            #endregion      

            #endregion

            //跟岱嘉沟通过,上传pdf不再需要更多的扩展表数据
            var lstSql = new List<string>()
            {
                SqlInsertReport,
                sqlInsertPdfFile,
                sqlInsertReportExtend1,
                //sqlInsertReportExtend2,
                sqlInsertReportExtend3,
            };

            if(string.IsNullOrEmpty(jcxx.F_SQXH.Trim())==false)
                lstSql.Add(sqlInsertReportExtend2);

//            OdbcOracleHelper.ExecuteBatch(connStr,lstSql);

            //xml保存到ftp            
            //pdf保存到ftp
            //图像保存到ftp
//            ftphelper.UploadRecordXml(PISReportXML, xmlDocId);
//            ftphelper.UploadPic(txList, jcxx, xmlDocId);
//            ftphelper.UploadPdf(pdfFullName,xmlDocId,jcxx);

//            ZgqPDFJPG zgq = new ZgqPDFJPG();
//            zgq.DelTempFile(jcxx.F_BLH);

            log.WriteMyLog("完成结果上传到平台,编辑类型:" + editType);


        }

        /// <summary>
        /// 阴阳性代码
        /// </summary>
        /// <param name="jcxx"></param>
        /// <returns></returns>
        private string GetDiagnosisStatusCode(T_JCXX jcxx)
        {
            switch (jcxx.F_YYX.Trim())
            {
                case "阳性":
                    return "PO";
                case "阴性":
                    return "NE";
                default:
                    return "OT";
            }
        }


        /// <summary>
        /// 注册病人到平台
        /// </summary>
        /// <param name="jcxx"></param>
        private void SavePerson(T_JCXX jcxx)
        {
            log.WriteMyLog("开始注册病人!");

            string uuid = Guid.NewGuid().ToString();
            //上传的信息是新增(01)还是修改(02)
            bool isExist =CheckPersonExist(jcxx);
            string personStatus = "01";
            if (isExist == true)
                personStatus = "02";

            //var qcmxList = new T_QCMX_DAL().GetListByBLH(jcxx.F_BLH);
            //var qpList = new T_QP_DAL().GetListByBLH(jcxx.F_BLH);

            #region sqlInsertPerson

            String sqlInsertPerson = $@"insert into person
                      (person_id,
                       name,
                       date_of_birth,
                       birth_place,
                       multiple_birth_ind,
                       birth_order,
                       mothers_maiden_name,
                       ssn,
                       identity_no,
                       insurance_no,
                       insurance_type,
                       insurance_name,
                       gender_cd,
                       ethnic_group_cd,
                       race_cd,
                       nationality_cd,
                       language_cd,
                       religion_cd,
                       marital_status_cd,
                       degree,
                       email,
                       home_address,
                       work_address,
                       city,
                       state,
                       country,
                       country_code,
                       home_number,
                       work_number,
                       death_ind,
                       death_time,
                       date_created,
                       creator_id,
                       date_changed,
                       changed_by_id,
                       date_voided,
                       voided_by_id,
                       hospital_domain_id,
                       identifier_domain_name,
                       identifier_domain_id,
                       identifier_domain_type,
                       identifier_id,
                       custom1,
                       custom2,
                       custom3,
                       custom4,
                       custom5,
                       custom6,
                       custom7,
                       custom8,
                       custom9,
                       custom10,
                       custom11,
                       custom12,
                       custom13,
                       custom14,
                       custom15,
                       custom16,
                       custom17,
                       custom18,
                       custom19,
                       custom20,
                       person_status,
                       merge_patient_id,
                       merge_person_domain,
                       registered_province,
                       registered_city,
                       registered_county,
                       registered_address,
                       registered_zip,
                       home_province,
                       home_city,
                       home_county,
                       home_zip,
                       private_number,
                       citizen_card,
                       medical_certificate,
                       medicare_person,
                       elder_certificate,
                       opcaseno,
                       company,
                       work_zip,
                       guardian_person,
                       birth_province,
                       birth_city,
                       birth_county,
                       birth_zip,
                       profession,
                       native_province,
                       native_city,
                       vip,
                       uuid,
                       name_spell_code,
                       name_wb_code,
                       blood_code,
                       gender_name,
                       marital_status_name,
                       degree_name,
                       profession_name,
                       nationality_name,
                       ethnic_name,
                       race_name,
                       gender_domain,
                       ethnic_domain,
                       race_domain,
                       nationality_domain,
                       marital_domain,
                       degree_domain,
                       profession_domain,
                       hiup_status,
                       hiup_error_info,
                       relevance_id,
                       relevance_domain,
                       health_card,
                       account_locked,
                       account_locked_date,
                       birth_time,
                       card_type,
                       handle_complete_time,
                       inpatient_send_time,
                       patient_send_time,
                       inpatient_emr_time,
                       patient_emr_time,
                       data_syn,
                       home_street,
                       registered_street)
                    values
                      (
                       PERSON_SEQUENCE.NEXTVAL, --v_person_id,
                       '{jcxx.F_XM}',--v_name,
                       {jcxx.GetBirthDateOraStringByAge()},--v_date_of_birth,
                       '{""}',--v_birth_place,
                       '{""}',--v_multiple_birth_ind,
                       '{""}',--v_birth_order,
                       '{""}',--v_mothers_maiden_name,
                       '{""}',--v_ssn,
                       '{""}',--v_identity_no,
                       '{""}',--v_insurance_no,
                       '{""}',--v_insurance_type,
                       '{""}',--v_insurance_name,
                       '{jcxx.GenderCode}',--v_gender_cd,
                       '',--v_ethnic_group_cd,
                       '{""}',--v_race_cd,
                       '{"1"}',--v_nationality_cd,
                       '{""}',--v_language_cd,
                       '{""}',--v_religion_cd,
                       '{""}',--v_marital_status_cd,
                       '{""}',--v_degree,
                       '{""}',--v_email,
                       '{jcxx.F_LXXX}',--v_home_address,
                       '{""}',--v_work_address,
                       '{""}',--v_city,
                       '{""}',--v_state,
                       '{""}',--v_country,
                       '{""}',--v_country_code,
                       '{""}',--v_home_number,
                       '{""}',--v_work_number,
                       '{""}',--v_death_ind,
                       '{""}',--v_death_time,
                       sysdate,--v_date_created,
                       '{""}',--v_creator_id,
                       '{""}',--v_date_changed,
                       '{""}',--v_changed_by_id,
                       '{""}',--v_date_voided,
                       '{""}',--v_voided_by_id,
                       '{"2.16.840.1.113883.4.487.2.1"}',--v_hospital_domain_id,
                       '{"LJPIS"}',--v_identifier_domain_name,
                       '{"2.16.840.1.113883.4.487.2.1.37"}',--v_identifier_domain_id,
                       '{"ISO"}',--v_identifier_domain_type,
                       '{jcxx.F_BLH.Trim()}',--v_identifier_id,
                       '{""}',--v_custom1,
                       '{""}',--v_custom2,
                       '{""}',--v_custom3,
                       '{GetHisDomain(jcxx)}',--v_custom4,门诊住院号域id
                       '{""}',--v_custom5,
                       '{""}',--v_custom6,
                       '{""}',--v_custom7,
                       '{""}',--v_custom8,
                       '{""}',--v_custom9,
                       '{""}',--v_custom10,
                       '{""}',--v_custom11,
                       '{""}',--v_custom12,
                       '{""}',--v_custom13,
                       '{jcxx.F_MZH.Trim()+jcxx.F_ZYH.Trim()}',--v_custom14,--门诊,住院号
                       '{""}',--v_custom15,
                       '{""}',--v_custom16,
                       '{""}',--v_custom17,
                       '{""}',--v_custom18,
                       '{""}',--v_custom19,
                       '{""}',--v_custom20,
                       '{personStatus}',--v_person_status,
                       '{""}',--v_merge_patient_id,
                       '{""}',--v_merge_person_domain,
                       '{""}',--v_registered_province,
                       '{""}',--v_registered_city,
                       '{""}',--v_registered_county,
                       '{""}',--v_registered_address,
                       '{""}',--v_registered_zip,
                       '{""}',--v_home_province,
                       '{""}',--v_home_city,
                       '{""}',--v_home_county,
                       '{""}',--v_home_zip,
                       '{""}',--v_private_number,
                       '{""}',--v_citizen_card,
                       '{""}',--v_medical_certificate,
                       '{""}',--v_medicare_person,
                       '{""}',--v_elder_certificate,
                       '{""}',--v_opcaseno,
                       '{""}',--v_company,
                       '{""}',--v_work_zip,
                       '{""}',--v_guardian_person,
                       '{""}',--v_birth_province,
                       '{""}',--v_birth_city,
                       '{""}',--v_birth_county,
                       '{""}',--v_birth_zip,
                       '{""}',--v_profession,
                       '{""}',--v_native_province,
                       '{""}',--v_native_city,
                       '{"0"}',--v_vip,
                       '{uuid}',--v_uuid,
                       '{""}',--v_name_spell_code,
                       '{""}',--v_name_wb_code,
                       '{""}',--v_blood_code,
                       '{jcxx.F_XB}',--v_gender_name,
                       '{""}',--v_marital_status_name,
                       '{""}',--v_degree_name,
                       '{""}',--v_profession_name,
                       '{""}',--v_nationality_name,
                       '',--v_ethnic_name,--民族都是其他
                       '{"其他"}',--v_race_name,
                       '{"2.16.840.1.113883.4.487.2.1.1.1.9"}',--v_gender_domain,
                       '{""}',--v_ethnic_domain,
                       '{""}',--v_race_domain,
                       '{""}',--v_nationality_domain,
                       '{""}',--v_marital_domain,
                       '{""}',--v_degree_domain,
                       '{""}',--v_profession_domain,
                       '0',--v_hiup_status,
                       '{""}',--v_hiup_error_info,
                       '{jcxx.F_SQXH}',--v_relevance_id,
                       '{GetRelevanceDomain(jcxx)}',--v_relevance_domain,门诊住院流水号域ID
                       '{""}',--v_health_card,
                       '{""}',--v_account_locked,
                       '{""}',--v_account_locked_date,
                       '{""}',--v_birth_time,
                       '{""}',--v_card_type,
                       '{""}',--v_handle_complete_time,
                       '{""}',--v_inpatient_send_time,
                       '{""}',--v_patient_send_time,
                       '{""}',--v_inpatient_emr_time,
                       '{""}',--v_patient_emr_time,
                       '{""}',--v_data_syn,
                       '{""}',--v_home_street,
                       '{""}'--v_registered_street
                        ) ";

            #endregion

            #region sqlInsertPersonVisit

            string sqlInsertPersonVisit = $@"
                                    insert into PATIENT_VISIT
                                      (PATIENT_VISIT_ID,
                                       PATIENT_ID,
                                       VISIT_FLOW_ID,
                                       NAME,
                                       DATE_OF_BIRTH,
                                       BIRTH_PLACE,
                                       SSN,
                                       IDENTITY_NO,
                                       INSURANCE_NO,
                                       GENDER_CD,
                                       MARITAL_STATUS,
                                       HOME_ADDRESS,
                                       WORK_ADDRESS,
                                       HOME_PHONE,
                                       WORK_PHONE,
                                       HOSPITAL_DOMAIN_ID,
                                       HOSPITAL_DOMAIN_NAME,
                                       IDENTIFIER_DOMAIN_NAME,
                                       IDENTIFIER_DOMAIN_ID,
                                       IDENTIFIER_DOMAIN_TYPE,
                                       IDENTIFIER_FLOW_DOMAIN_NAME,
                                       IDENTIFIER_FLOW_DOMAIN_ID,
                                       IDENTIFIER_FLOW_DOMAIN_TYPE,
                                       PAT_CATEGORY,
                                       PAT_CURRENT_POINT_OF_CARE,
                                       PAT_CURRENT_ROOM,
                                       PAT_CURRENT_BED,
                                       PAT_CUURENT_DEP,
                                       PAT_CURRENT_DEP_NAME,
                                       PAT_CURRENT_POSITION_STATUS,
                                       PAT_CURRENT_POSITION_TYPE,
                                       PAT_CURRENT_BUILDING,
                                       PAT_CURRENT_FLOOR,
                                       PAT_CUURENT_DESCRIPTION,
                                       PAT_ADMISSION_TYPE,
                                       PAT_ADMISSION_NUMBER,
                                       ADMISSIONS_DOCTOR,
                                       ADMISSIONS_DOCTOR_ID,
                                       REFERRING_DOCTOR,
                                       REFERRING_DOCTOR_ID,
                                       CONSULTATION_DOCTOR,
                                       CONSULTATION_DOCTOR_ID,
                                       HOSPITAL_SERVICE,
                                       PAT_ADMISSION_TEST,
                                       PAT_RE_ADMISSION,
                                       PAT_ADMISSION_SOURCE,
                                       PAT_AMBULATORY_STATUS,
                                       PAT_VIP,
                                       PAT_ADMISSION_DOCTORS,
                                       PAT_ADMISSION_DOCTORS_ID,
                                       PATIENT_CLASS,
                                       PATIENT_FLOW_ID,
                                       PAT_DISCHARGE_DISPOSITION,
                                       PAT_DISCHARGE_LOCATION,
                                       PAT_DIET_TYPE,
                                       PAT_SERVICE_AGENCIES,
                                       PAT_BED_STATUS,
                                       PAT_ACCOUNT_STATUS,
                                       PAT_NURSE_CODE,
                                       PAT_NURSE_NAME,
                                       TEND,
                                       PAT_DIETETIC_MARK,
                                       PAT_IPTIMES,
                                       PAT_DISCHARGE_CODE,
                                       PAT_DETER_POINT_OF_CARE,
                                       PAT_DETER_ROOM,
                                       PAT_DETER_BED,
                                       PAT_DETER_DEP,
                                       PAT_DETER_POSITION_STATUS,
                                       PAT_DETER_POSITION_TYPE,
                                       PAT_DETER_BUILDING,
                                       PAT_DETER_FLOOR,
                                       PAT_DETER_DESCRIPTION,
                                       PAT_IPSTATUSCODE,
                                       PAT_DIFFICULTY_LEVELCODE,
                                       BABY_FLAG,
                                       ADMIT_WEIGHT,
                                       BIRTH_WEIGHT,
                                       PAT_FORMER_POINT_OF_CARE,
                                       PAT_FORMER_ROOM,
                                       PAT_FORMER_BED,
                                       PAT_FORMER_DEP,
                                       PAT_FORMER_POSITION_STATUS,
                                       PAT_FORMER_POSITION_TYPE,
                                       PAT_FORMER_BUILDING,
                                       PAT_FORMER_FLOOR,
                                       PAT_FORMER_DESCRIPTION,
                                       PAT_TEMP_POINT_OF_CARE,
                                       PAT_TEMP_ROOM,
                                       PAT_TEMP_BED,
                                       PAT_TEMP_DEP,
                                       PAT_TEMP_POSITION_STATUS,
                                       PAT_TEMP_POSITION_TYPE,
                                       PAT_TEMP_BUILDING,
                                       PAT_TEMP_FLOOR,
                                       PAT_TEMP_DESCRIPTION,
                                       PAT_FOR_TEMP_POINT_OF_CARE,
                                       PAT_FOR_TEMP_ROOM,
                                       PAT_FOR_TEMP_BED,
                                       PAT_FOR_TEMP_DEP,
                                       PAT_FOR_TEMP_POSITION_STATUS,
                                       PAT_FOR_TEMP_POSITION_TYPE,
                                       PAT_FOR_TEMP_BUILDING,
                                       PAT_FOR_TEMP_FLOOR,
                                       PAT_FOR_TEMP_DESCRIPTION,
                                       OPER_CODE,
                                       OPER_DATE,
                                       ADMIT_DATE,
                                       DISCHARGE_DATE,
                                       REG_DATE,
                                       OPR_DATE,
                                       CREATE_DATE,
                                       CREATE_ID,
                                       VOIDED_DATE,
                                       VOIDED_ID,
                                       MODIFY_DATE,
                                       MODIFY_ID,
                                       CUSTOM1,
                                       CUSTOM2,
                                       CUSTOM3,
                                       CUSTOM4,
                                       CUSTOM5,
                                       CUSTOM6,
                                       CUSTOM7,
                                       CUSTOM8,
                                       CUSTOM9,
                                       CUSTOM10,
                                       CUSTOM11,
                                       CUSTOM12,
                                       CUSTOM13,
                                       CUSTOM14,
                                       CUSTOM15,
                                       CUSTOM16,
                                       CUSTOM17,
                                       CUSTOM18,
                                       CUSTOM19,
                                       CUSTOM20,
                                       PATIENT_VISIT_STATUS,
                                       UUID,
                                       PREFIX,
                                       INSURANCE_TYPE,
                                       CONTACT_PERSON,
                                       CONTACT_RELATIONS,
                                       CONTACT_ADDRESS,
                                       CONTACT_PHONE,
                                       PAT_CATEGORY_NAME,
                                       GENDER_NAME,
                                       PAY_RATE,
                                       DISCHARGE_NAME,
                                       INSURANCE_NAME,
                                       ADMISSION_NAME,
                                       IP_STATUS_NAME,
                                       DIFICULTY_NAME,
                                       ADMIT_WAY_NAME,
                                       ADMIT_WEIGHT_UNIT,
                                       BABY_WEIGHT_UNIT,
                                       MEDICINELIMITAMOUNT,
                                       SICKBEDLIMITAMOUNT,
                                       EXAMINELIMITAMOUNT,
                                       CURELIMITAMOUNT,
                                       HIUP_STATUS,
                                       HIUP_ERROR_INFO,
                                       ADMISSION_DOMAIN,
                                       ADMISSION_SOURCE_DOMAIN,
                                       ADMISSION_SOURCE_NAME,
                                       PATIENT_CLASS_NAME,
                                       PATIENT_CLASS_DOMAIN,
                                       IP_STATUS_DOMAIN,
                                       DIFICULTY_DOMAIN,
                                       DISCHARGE_DOMAIN,
                                       ACCOUNT_STATUS_NAME,
                                       ACCOUNT_STATUS_DOMAIN,
                                       GENDER_DOMAIN,
                                       PAT_CATEGORY_SYSTEM,
                                       MOTHERS_ID,
                                       MOTHERS_DOMAIN,
                                       MOTHERS_FLOW_ID,
                                       MOTHERS_FLOW_DOMAIN,
                                       MOTHERS_NAME,
                                       PATIENT_SOURCE_NAME,
                                       OLD_PATIENT_ID,
                                       OLD_PATIENT_DOMAIN,
                                       OLD_VISIT_FLOW_ID,
                                       OLD_VISIT_FLOW_DOMAIN,
                                       OLD_VISIT_ID,
                                       OLD_PERSON_ID,
                                       OLD_STATUS,
                                       OLD_INFO,
                                       ISEMERGENCY,
                                       DIAGNOSE_ICD,
                                       DIAGNOSE_NAME,
                                       NOON_CODE,
                                       PAYKIND_CODE,
                                       PAYKIND_NAME,
                                       SCHEMA_NO,
                                       ORDER_NO,
                                       SEENO,
                                       BEGIN_TIME,
                                       END_TIME,
                                       YNBOOK,
                                       YNFR,
                                       APPEND_FLAG,
                                       YNSEE,
                                       SEE_DATE,
                                       TRIAGE_FLAG,
                                       TRIAGE_OPCD,
                                       TRIAGE_DATE,
                                       SEE_DPCD,
                                       SEE_DOCD,
                                       OUT_PATIENT_STATUS_A,
                                       OUT_PATIENT_STATUS_B,
                                       OUT_PATIENT_STATUS_C,
                                       IN_PATIENT_STATUS_A,
                                       IN_PATIENT_STATUS_B,
                                       IN_PATIENT_STATUS_C)
                                    values
                                      (patient_visit_sequence.nextval,
                                       '{jcxx.F_BLH.Trim()}', --PATIENT_ID
                                       '{jcxx.F_YZID}', --VISIT_FLOW_ID,写就诊流水号(医嘱ID)
                                       '{jcxx.F_XM}', --Name
                                       {jcxx.GetBirthDateOraStringByAge()}, --DATE_OF_BIRTH
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null, --IDENTIFIER_DOMAIN_NAME,
                                       '2.16.840.1.113883.4.487.2.1', --   IDENTIFIER_DOMAIN_ID,
                                       null, --   IDENTIFIER_DOMAIN_TYPE,
                                       'LJPIS', --   IDENTIFIER_FLOW_DOMAIN_NAME,
                                       '2.16.840.1.113883.4.487.2.1.37', --   IDENTIFIER_FLOW_DOMAIN_ID,
                                       'ISO', --   IDENTIFIER_FLOW_DOMAIN_TYPE,
                                       'LJPIS-BLKLS', -- IDENTIFIER_FLOW_DOMAIN_NAME,   
                                       '2.16.840.1.113883.4.487.2.1.37.2', --   IDENTIFIER_FLOW_DOMAIN_ID,
                                       'ISO', --   IDENTIFIER_FLOW_DOMAIN_TYPE,
                                       '{GetPatCategory(jcxx)}',--PAT_CATEGORY 0-门诊 1-住院 2-急诊 3-体检
                                       null,
                                       null,
                                       null,
                                       '病理科',--PAT_CUURENT_DEP
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       sysdate,--CREATE_DATE
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       '{uuid}',--uuid
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       '{jcxx.F_XB}',--GENDER_NAME
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       '0',--   HIUP_STATUS,
                                       null,--   HIUP_ERROR_INFO,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       '2.16.840.1.113883.4.487.2.1.1.1.9',--GENDER_DOMAIN
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null,
                                       null)
                                    ";

            #endregion        }


            var lstSql = new List<string>();
            lstSql.Add(sqlInsertPerson);

            if (string.IsNullOrEmpty(jcxx.F_YZID.Trim()) == false)
                lstSql.Add(sqlInsertPersonVisit);

            OdbcOracleHelper.ExecuteBatch(connStr,lstSql);

            log.WriteMyLog("注册病人成功!");

        }

        /// <summary>
        /// 获取门诊或住院流水号域ID
        /// </summary>
        /// <param name="jcxx"></param>
        private string GetRelevanceDomain(T_JCXX jcxx)
        {
            switch (jcxx.E_BRLB)
            {
                case BRLB.门诊:
                    return "2.16.840.1.113883.4.487.2.1.4.2";
                case BRLB.住院:
                case BRLB.急诊:
                    return "2.16.840.1.113883.4.487.2.1.4.4";
                case BRLB.体检:
                    return "2.16.840.1.113883.4.487.2.1.4.6";
                default:
                    return "2.16.840.1.113883.4.487.2.1.4.2";
            }
        }

        /// <summary>
        /// 获取门诊或住院号的域ID
        /// </summary>
        /// <param name="jcxx"></param>
        private string GetHisDomain(T_JCXX jcxx)
        {
            switch (jcxx.E_BRLB)
            {
                case BRLB.门诊:
                    return "2.16.840.1.113883.4.487.2.1.4.1";
                case BRLB.住院:
                case BRLB.急诊:
                    return "2.16.840.1.113883.4.487.2.1.4";
                case BRLB.体检:
                    return "2.16.840.1.113883.4.487.2.1.4.5";
                default:
                    return "2.16.840.1.113883.4.487.2.1.4.1";
            }
        }

        /// <summary>
        /// 检查是否已经注册过该病人
        /// </summary>
        /// <param name="jcxx"></param>
        /// <returns></returns>
        private bool CheckPersonExist(T_JCXX jcxx)
        {
            string sql = $@"select count(*)
  from person p
 where trim(nvl(p.custom14,'')) = '{jcxx.F_MZH.Trim() + jcxx.F_ZYH.Trim()}' --门诊住院号
    and p.identifier_id ='{jcxx.F_BLH}' --病理号不同的,按新病人注册
   and p.identifier_domain_id = '2.16.840.1.113883.4.487.2.1.37' ";

            var count = OdbcOracleHelper.ExecuteScalar(connStr, sql);
            return count != "0";
        }

        /// <summary>
        /// 检查是否已经上传过该报告
        /// </summary>
        /// <param name="jcxx"></param>
        /// <returns></returns>
        private bool CheckIfReportExist(T_JCXX jcxx)
        {
            string sql = $@"select count(*)
  from dgate_document_info  p
 where p.DOCUMENT_UNIQUE_ID = '{jcxx.F_BLH}' --病理号
   and p.document_domain_id = '2.16.840.1.113883.4.487.2.1.37' ";

            var count = OdbcOracleHelper.ExecuteScalar(connStr, sql);
            return count != "0";
        }

        /// <summary>
        /// 获取 dgate_extend_id_info_sequence.nextval 作为主键
        /// </summary>
        /// <returns></returns>
        private string GetNewDocId()
        {
            return OdbcOracleHelper.ExecuteScalar(connStr,
                " select dgate_document_info_sequence.nextval from dual ");
        }

        #region 辅助方法

        private string GetPathDiagXml(T_JCXX jcxx)
        {
            string xml = "";
            if (jcxx.F_BGZT == "已审核")
            { 
                xml+=$@"     <SubDiagnosis>
			                    <DiagnosisTitle>病理报告</DiagnosisTitle>
			                    <DiagnosisConclusion>{GetXmlStr(jcxx.F_BLZD)}</DiagnosisConclusion>
			                    <DiagnosisTime>{GetXmlStr(jcxx.F_BGRQ)+":00"}</DiagnosisTime>
			                    <DiagnosisCode>gc</DiagnosisCode>
			                    <DiagnosisCodesystem></DiagnosisCodesystem>
			                    <DiagnosisPerformer>
                                    <Performer>
				                        <PerformerName>{GetXmlStr(jcxx.F_BGYS)}</PerformerName>
                                        <PerformerTypeCode>cg_czys</PerformerTypeCode>
                                    </Performer>
                                    <Performer>
				                        <PerformerName>{GetXmlStr(jcxx.F_FZYS)}</PerformerName>
                                        <PerformerTypeCode>cg_fzys</PerformerTypeCode>
                                    </Performer>
                                    <Performer>
				                        <PerformerName>{GetXmlStr(jcxx.F_SHYS)}</PerformerName>
                                        <PerformerTypeCode>cg_shys</PerformerTypeCode>
                                    </Performer>
			                    </DiagnosisPerformer>
		                    </SubDiagnosis> ";

                xml += $@"		                        
                            <SubDiagnosis>
			                    <DiagnosisTitle>肉眼所见</DiagnosisTitle>
			                    <DiagnosisConclusion>{GetXmlStr(jcxx.F_RYSJ)}</DiagnosisConclusion>
			                    <DiagnosisTime>{GetXmlStr(jcxx.F_BGRQ) + ":00"}</DiagnosisTime>
			                    <DiagnosisCode>cg_rysj</DiagnosisCode>
			                    <DiagnosisCodesystem></DiagnosisCodesystem>
			                    <DiagnosisPerformer>
                                    <Performer>
				                        <PerformerName>{GetXmlStr(jcxx.F_QCYS)}</PerformerName>
                                        <PerformerTypeCode>cg_qcys</PerformerTypeCode>
                                    </Performer>
			                    </DiagnosisPerformer>
		                    </SubDiagnosis> ";
            }
            var bcbgList = new T_BCBG_DAL().GetListByBLH(jcxx.F_BLH);
            var bdbgList = new T_BDBG_DAL().GetListByBLH(jcxx.F_BLH);

            foreach (T_BCBG bcbg in bcbgList)
            {
                xml += $@"		                        
                        <SubDiagnosis>
			                <DiagnosisTitle>补充报告[{bcbg.F_BC_BGXH}]</DiagnosisTitle>
			                <DiagnosisConclusion>{GetXmlStr(bcbg.F_BCZD)}</DiagnosisConclusion>
			                <DiagnosisTime>{GetXmlStr(bcbg.F_BC_BGRQ)}</DiagnosisTime>
			                <DiagnosisCode>bc</DiagnosisCode>
			                <DiagnosisCodesystem></DiagnosisCodesystem>
			                <DiagnosisPerformer>
                                <Performer>
				                    <PerformerName>{GetXmlStr(bcbg.F_BC_BGYS)}</PerformerName>
                                    <PerformerTypeCode>bc_czys</PerformerTypeCode>
                                </Performer>
                                <Performer>
				                    <PerformerName>{GetXmlStr(bcbg.F_BC_FZYS)}</PerformerName>
                                    <PerformerTypeCode>bc_fzys</PerformerTypeCode>
                                </Performer>
                                <Performer>
				                    <PerformerName>{GetXmlStr(bcbg.F_BC_SHYS)}</PerformerName>
                                    <PerformerTypeCode>bc_shys</PerformerTypeCode>
                                </Performer>
			                </DiagnosisPerformer>
		                </SubDiagnosis> ";
            }

            foreach (T_BDBG bdbg in bdbgList)
            {
                xml += $@"		                        
                        <SubDiagnosis>
			                <DiagnosisTitle>冰冻报告[{bdbg.F_BD_BGXH}]</DiagnosisTitle>
			                <DiagnosisConclusion>{GetXmlStr(bdbg.F_BDZD)}</DiagnosisConclusion>
			                <DiagnosisTime>{GetXmlStr(bdbg.F_BD_BGRQ)}</DiagnosisTime>
			                <DiagnosisCode>bd</DiagnosisCode>
			                <DiagnosisCodesystem></DiagnosisCodesystem>
			                <DiagnosisPerformer>
                                <Performer>
				                    <PerformerName>{GetXmlStr(bdbg.F_BD_BGYS)}</PerformerName>
                                    <PerformerTypeCode>bd_czys</PerformerTypeCode>
                                </Performer>
                                <Performer>
				                    <PerformerName>{GetXmlStr(bdbg.F_BD_FZYS)}</PerformerName>
                                    <PerformerTypeCode>bd_fzys</PerformerTypeCode>
                                </Performer>	
                                <Performer>
				                    <PerformerName>{GetXmlStr(bdbg.F_BD_SHYS)}</PerformerName>
                                    <PerformerTypeCode>bd_shys</PerformerTypeCode>
                                </Performer>	
                            </DiagnosisPerformer>
		                </SubDiagnosis> ";

                xml += $@"		                        
                        <SubDiagnosis>
			                <DiagnosisTitle>肉眼所见(冰冻)</DiagnosisTitle>
			                <DiagnosisConclusion>{GetXmlStr(bdbg.F_bd_rysj)}</DiagnosisConclusion>
			                <DiagnosisTime>{GetXmlStr(bdbg.F_BD_BGRQ)}</DiagnosisTime>
			                <DiagnosisCode>bd_rysj</DiagnosisCode>
			                <DiagnosisCodesystem></DiagnosisCodesystem>
			                <DiagnosisPerformer>
                                <Performer>
				                    <PerformerName>{GetXmlStr(bdbg.F_BD_QCYS)}</PerformerName>
                                    <PerformerTypeCode>bd_qcys</PerformerTypeCode>
                                </Performer>
                            </DiagnosisPerformer>
		                </SubDiagnosis> ";
            }
            return xml;
        }

        private string GetQpListXml(List<T_QP> qpList, T_JCXX jcxx)
        {
            string xml = "";
            foreach (T_QP qp in qpList)
            {
                xml += $@"               
                <SliceInfo>
	                <SliceDoctor>{GetXmlStr(qp.F_CZY)}</SliceDoctor>
	                <SliceNum>1</SliceNum>
	                <SliceType></SliceType>
                </SliceInfo> ";
            }
            return xml;
        }

        private string GetQcListXml(List<T_QCMX> qcList)
        {
            string xml = "";
            foreach (T_QCMX qcmx in qcList)
            {
                xml += $@"               
               <WaxblockInfo>
	                <WaxblockDoctor>{GetXmlStr(qcmx.F_QCYS)}</WaxblockDoctor>
	                <WaxblockNum>1</WaxblockNum>
	                <WaxblockType>{GetXmlStr(qcmx.F_ZZMC)}</WaxblockType>
                </WaxblockInfo>";
            }
            return xml;
        }

        /// <summary>
        /// 获取患者类型 
        /// 0	门诊
        /// 1	住院
        /// 2	急诊
        /// 3	体检
        /// </summary>
        /// <returns></returns>
        private string GetPatCategory(T_JCXX jcxx)
        {
            var lb = jcxx.F_BRLB.Replace("内镜", "").Replace(")", "").Replace("(", "").Replace("（", "").Replace("）", "");
            switch (lb)
            {
                case "门诊":
                    return "0";
                case "住院":
                    return "1";
                case "急诊":
                    return "2";
                case "体检":
                    return "3";
                default:
                    return "";
            }
        }


        /// <summary>
        /// 获取图像存放地址xml
        /// </summary>
        /// <param name="jcxx"></param>
        /// <param name="txList"></param>
        /// <param name="docId"></param>
        private string GetImagePathList(List<T_TX> txList, string serverPath)
        {
            string xml = "";
            foreach (T_TX tx in txList)
            {
                xml += $@"<ImagePath>{serverPath + "/" + tx.F_TXM}</ImagePath>
";
            }
            return xml;
        }

        /// <summary>
        /// 没有实际意义,因为没有返回dicom,平台要求返回空节点
        /// </summary>
        /// <param name="txList"></param>
        private string GetDicomXml(List<T_TX> txList)
        {
            var xml = "";
            foreach (var tx in txList)
            {
                xml += $"<DicomStudyUid></DicomStudyUid>";
            }
            return xml;
        }
        #endregion

        public  string GetXmlStr( string str)
        {
            string[] sp = new[] { "&", ">", "<", "'", "\"" };
            string[] spt = new[] { "&amp;", "&gt;", "&lt;", "&apos;", "&quot;" };

            for (var i = 0; i < sp.Length; i++)
            {
                str = str.Replace(sp[i], spt[i]);
            }

            str=str.Replace("\r\n", "&lt;/br&gt;");

            return str;
        }


        public string CreatePDF(string blh, string bgxh, ReportType reportType, bool isToBase64String,
            ref string PdfToBase64String, string debug)
        {
            if (f.ReadString("savetohis", "ispdf", "1").Trim() != "1")
            {
                log.WriteMyLog("ispdf="+ f.ReadString("savetohis", "ispdf", "1").Trim());
                return "";
            }

            var bglx = "cg";
            if (reportType == ReportType.冰冻报告)
                bglx = "bd";
            else if (reportType == ReportType.补充报告)
                bglx = "bc";

            var dt_jcxx = aa.GetDataTable("select * from T_jcxx where F_blh='" + blh + "'", "jcxx");
            string blbh = blh + bglx + bgxh;
            if (bglx == "cg")
                blbh = blh;

            #region  生成pdf

            string ReprotFile = "";
            string pdfName = "";
            string ML = DateTime.Parse(dt_jcxx.Rows[0]["F_SDRQ"].ToString()).ToString("yyyyMM");
            string message = "";
            DateTime dateStamp = DateTime.Now;

            ZgqPDFJPG zgq = new ZgqPDFJPG();

            if (debug=="1")
            {
                log.WriteMyLog("开始CreatePDF");
            }
            bool isrtn = zgq.CreatePDF(blh, bglx, bgxh, ZgqPDFJPG.Type.PDF, ref pdfName, "", "", ref message,
                dateStamp);

            #endregion

            #region 图片生成处理
//
//            //pdf转图片
//            var jpgName = blh + "_" + bglx + bgxh + "_" + dateStamp.ToString("yyyyMMddHHmmss");
//            try
//            {
//                Converer.ConvertPDF2Image(pdfName, @"c:\temp\", jpgName, 1, 100, ImageFormat.Jpeg,
//                    Converer.Definition.Three,
//                    true);
//            }
//            catch (Exception e)
//            {
//                SendPisResult.log.WriteMyLog("生成图片报告时出现异常:" + e);
//            }
//
//            //上传图片
//            string jpgPath = "";
//            bool success = zgq.UpPDF(blh, @"c:\temp\" + jpgName + ".jpeg", ML, ref message, 3, ref jpgPath);
//            if (!success)
//                log.WriteMyLog("上传JPG失败:" + @"c:\temp\" + jpgName + "------" + message);
//
//            //上传完毕后删除文件
//            try
//            {
//                File.Delete(@"c:\temp\" + jpgName + ".jpeg");
//            }
//            catch (Exception e)
//            {
//                log.WriteMyLog("删除临时报告图片失败:" + e);
//            }
//
            #endregion

            if (isrtn)
            {
                if (debug == "1")
                    log.WriteMyLog("生成PDF成功");


                if (isToBase64String)
                {
                    try
                    {
                        FileStream file = new FileStream(pdfName, FileMode.Open, FileAccess.Read);
                        Byte[] imgByte = new Byte[file.Length]; //把pdf转成 Byte型 二进制流   
                        file.Read(imgByte, 0, imgByte.Length); //把二进制流读入缓冲区   
                        file.Close();
                        PdfToBase64String = Convert.ToBase64String(imgByte);
                        if (debug == "1")
                            log.WriteMyLog("PDF转换二进制串成功");
                    }
                    catch (Exception ee)
                    {
                        log.WriteMyLog("PDF转换二进制串失败:" + ee.Message);
                    }
                }

                string pdfpath = "";
                bool ssa = zgq.UpPDF(blh, pdfName, ML, ref message, 3, ref pdfpath);


                if (ssa == true)
                {
                    if (debug == "1")
                        log.WriteMyLog("上传PDF成功");

                    pdfName = pdfName.Substring(pdfName.LastIndexOf('\\') + 1);
                    ZgqClass.BGHJ(blh, "上传PDF成功", "审核", "上传PDF成功:" + pdfpath, "ZGQJK", "上传PDF");
                    aa.ExecuteSQL("delete T_BG_PDF  where F_BLBH='" + blbh + "'");
                    aa.ExecuteSQL(
                        "insert  into T_BG_PDF(F_BLBH,F_BLH,F_BGLX,F_BGXH,F_ML,F_FILENAME,f_pdfpath) values('" +
                        blbh +
                        "','" + blh + "','" + bglx + "','" + bgxh + "','" + ML + "\\" + blh + "','" + pdfName +
                        "','" +
                        pdfpath + "')");
                }
                else
                {
                    if (debug == "1")
                        log.WriteMyLog("上传PDF失败" + message);
                    ZgqClass.BGHJ(blh, "上传PDF失败", "审核", message, "ZGQJK", "上传PDF");
                }
            }
            else
            {
                if (debug == "1")
                    log.WriteMyLog("生成PDF失败" + message);
                ZgqClass.BGHJ(blh, "生成PDF失败", "审核", "生成pdf失败" + message, "ZGQJK", "生成PDF");
            }
            //zgq.DelTempFile(blh);

            return pdfName;

        }




    }

}