using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using dbbase;
using Langjia.Service;
using LangJia.Service.Model;
using LGInterface;
using LGInterface.Util;

namespace LangJia.Service
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class WebService1 : CrisisValue
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public override Response CrisisAdd(Request req)
        {
         //   throw new NotImplementedException();
          Response r = new Response();
            r.responseBody = "123";
            return r;
        }

        [WebMethod]
        public override Response CrisisUpdate(Request req)
        {
            throw new NotImplementedException();
        }

        [WebMethod]
        public override Response CrisisStatusFeedback(Request req)
        {
            odbcdb aa = new odbcdb("DSN=pathnet;UID=pathnet;PWD=4s3c2a1p", "", "");
            int errCode = 1;

            Response response = new Response();
            response.responseHeader = new ResponseHeader();
            response.responseHeader.sender = "2.16.840.1.113883.4.487.6.1.6";
            response.responseHeader.receiver = "2.16.840.1.113883.4.487.6.1.4";
            response.responseHeader.requestTime = DateTime.Now.ToString("yyyyMMddHH24mmss");
            response.responseHeader.msgType = req.requestHeader.msgType;
            response.responseHeader.msgId = req.requestHeader.msgId;
            response.responseHeader.msgPriority = "Normal";
            response.responseHeader.msgVersion = "1.0";

            CrisisPathologyOperate ci = new CrisisPathologyOperate();//返回值

            var message = "";
            var xml = req.requestBody.Replace("<List>", "<CrisisPathologyOperateList>")
                .Replace(@"</List>", @"</CrisisPathologyOperateList>");
            
            CrisisPathologyOperateList lstCrisis =new CrisisPathologyOperateList();

            try
            {
                lstCrisis = XmlUtil.Deserialize<CrisisPathologyOperateList>(xml);
                if (lstCrisis == null)
                    throw new Exception("lstCrisis的值为null");
            }
            catch (Exception e)
            {
                message = $"解析返回的xml失败,\r\requestBody:\r\n{xml}\r\n异常信息:\r\n"+e;
                log.WriteMyLog(message);
                errCode = 1;
                goto returnResponse;
            }

            var lstSql=new List<string>();


            try
            {
                foreach (CrisisPathologyOperate crisis in lstCrisis.CrisisPathologyOperate)
                {
                    ci = crisis;
                    string sqlInsert = $@"INSERT INTO [dbo].[T_WJZZT]
                                                   ([f_blh]
                                                   ,[f_sqxh]
                                                   ,[f_zt]
                                                   ,[f_jg]
                                                   ,[f_nr])
                                             VALUES
                                                   ('{crisis.SampleID}'
                                                   ,'{crisis.RequestNo}'
                                                   ,'{crisis.StatusText}'
                                                   ,'{crisis.ResponseTypeName}'
                                                   ,'{crisis.OperationInfo}')";
                    aa.ExecuteSQL(sqlInsert);
                }
            }
            catch (Exception e)
            {
                message = $"危急值状态插入数据库时失败,\r\requestBody:\r\n{xml}\r\n异常信息:\r\n" + e;
                log.WriteMyLog(message);
                goto returnResponse;
            }

            //处理成功,返回信息
            errCode = 0;


            message = "OK";

            returnResponse:
            {
                string resultCode = errCode == 0 ? "1" : "0";

                response.responseHeader.errCode = errCode.ToString();
                response.responseHeader.errMessage = message;
                response.responseBody =$@"      
                                            <List>
                                              <CrisisPathologyOperate>
                                                <RequestNo>{ci.RequestNo}</RequestNo>
                                                <SampleID>{ci.SampleID}</SampleID>
                                                <Status>{ci.Status}</Status>
                                                <Result>{resultCode}</Result>
                                              </CrisisPathologyOperate>
                                            </List>
                                       ";
                return response;
            }
            
        }
    }
}
