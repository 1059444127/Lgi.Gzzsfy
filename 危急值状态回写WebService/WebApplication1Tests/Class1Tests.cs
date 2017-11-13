using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication1Tests.ServiceReference1;

namespace WebApplication1Tests
{
    [TestClass()]
    public class Class1Tests
    {
        [TestMethod()]
        public void WebServiceTest()
        {
            WebService1SoapClient ws1 = new WebService1SoapClient();
           
            Request req=new Request();
            req.requestHeader = new RequestHeader();
            req.requestHeader.sender = "2.16.840.1.113883.4.487.6.1.6";
            req.requestHeader.receiver = "2.16.840.1.113883.4.487.6.1.4";
            req.requestHeader.requestTime = DateTime.Now.ToString("yyyyMMddHH24mmss");
            req.requestHeader.msgType = "CrisisPathology";
            req.requestHeader.msgId = "CRI20140909000009";
            req.requestHeader.msgPriority = "Normal";
            req.requestHeader.msgVersion = "1.0";
            req.requestBody = $@"
<List>
  <CrisisPathologyOperate>
    <RequestNo>申请单号</RequestNo>
    <SampleID>标本号即试管条码</SampleID>
    <CreateTime>创建时间</CreateTime>
    <Status>处理状态，0=发布,1=科室已接收,2=科室已处理,3=危急值已取消,4=已代理接收,5=已代理处理,11=超时未接收,12=超时未处理</Status>
    <Remark>备注</Remark>
    <OperationInfo>操作内容</OperationInfo>
    <OperationSystem>操作源，1：门户系统，2：LIS（血液、免疫、生化），3：LIS（细菌、骨髓）,4： 病理系统,5：放射系统,6：内镜系统,7：超声系统,8：核医学系统,9：心电系统</OperationSystem>
    <OperationType>操作类别, 0:发布危机值,1：接收；2：代接收，3：电话通知，4：代处理，5：科室处理，6：取消危机值</OperationType>
    <OperationTypeName>操作类别, 0:发布危机值,1：接收；2：代接收，3：电话通知，4：代处理，5：科室处理，6：取消危机值</OperationTypeName>
    <PrincipalCode>被代理人编号</PrincipalCode>
    <PrincipalName>被代理人姓名</PrincipalName>
    <ResponseAccoutID>操作者门户账号ID</ResponseAccoutID>
    <ResponseCode>操作者工号</ResponseCode>
    <ResponseMemo>响应备注</ResponseMemo>
    <ResponseName>操作者姓名</ResponseName>
    <ResponseTime>处理时间</ResponseTime>
    <ResponseType>响应类别,11-已电话通知患者,12-多次电话通知患者无人接听,15-病人信息不正确,14-其他,21-符合病情，无需处理,22-符合病情，必须处理,25-病人信息不正确,23-与病情不符，建议重检,24-其他</ResponseType>
    <ResponseTypeName>响应类别名称,11-已电话通知患者,12-多次电话通知患者无人接听,15-病人信息不正确,14-其他,21-符合病情，无需处理,22-符合病情，必须处理,25-病人信息不正确,23-与病情不符，建议重检,24-其他</ResponseTypeName>
  </CrisisPathologyOperate>
</List>
";
            var response = ws1.CrisisStatusFeedback(req);
        }
    }
}

