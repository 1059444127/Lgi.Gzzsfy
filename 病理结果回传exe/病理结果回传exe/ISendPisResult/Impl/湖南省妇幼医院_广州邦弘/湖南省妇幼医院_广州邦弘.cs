using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using dbbase;
using Maticsoft.DAL;
using SendPisResult.HnsfyOutInterface;
using SendPisResult.Models;

namespace SendPisResult.ISendPisResult.Impl.湖南省妇幼医院_广州邦弘
{
    [HospName("湖南省妇幼医院_广州邦弘")]
    public class 湖南省妇幼医院_广州邦弘 : ISendPisResult
    {
        #region Implementation of ISendPisResult

        /// <summary>
        /// 如果JCXX状态为已审核,则回传结果
        /// 否则若病人类型为门诊,则进行门诊确费
        /// </summary>
        /// <param name="pathoNo">病理号</param>
        /// <param name="aa"></param>
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

            try
            {
                if (jcxx.F_BGZT == "已审核") //如果已审核,执行回传结果
                {
                    SendResult(jcxx);
                }
                else if (string.IsNullOrEmpty(jcxx.F_MZH.Trim())==false) //如果不是已审核且科室为门诊,执行门诊确费
                {
                    log.WriteMyLog("进入OutCheckFee");
                    OutCheckFee(jcxx);
                }
            }
            catch (Exception e)
            {
                //该接口陈程要求所有报错都不弹窗,只记录log
            }
        }

        /// <summary>
        /// 调用pacs接口进行门诊确费
        /// </summary>
        /// <param name="jcxx">检查信息表</param>
        public void OutCheckFee(T_JCXX jcxx)
        {
            #region 入参说明

            //Registeredserialnumber：挂号序号(门诊号)；
            //Operatorid：操作者ID
            //Departmentid：执行科室ID
            //Datime：操作时间,时间格式：yyyy - MM - dd HH: mm: ss
            //Type:执行类型：1：确费，0：取消确费；此函数传1；

            #endregion

            string result = "";

            try
            {
                log.WriteMyLog("开始门诊确费,门诊号:" + jcxx.F_MZH);
                var date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                //调用门诊确费
                HnsfyRegistrationInterface.WebService service = new HnsfyRegistrationInterface.WebService();

                log.WriteMyLog("开始调用门诊确费接口,门诊号(发票号)是:"+jcxx.F_MZH);
                //门诊号存的就是发票号 账号:林春华 工号:4411 科室ID:1094
                var returnXML = service.CheckFeeOperationByInvoicenumber(jcxx.F_MZH, "4411", "1094", date, "1");
                log.WriteMyLog("门诊确费成功,返回值:\r\n" + returnXML);

                //解析门诊确费接口的返回值
                var row = GetDataSetByXml(returnXML).Tables[0].Rows[0];
                int count = Int32.Parse(row["Code"].ToString());
                string msg = row["ERR_TEXT"].ToString();

                //如果确费数量<0,则确费失败
                if (count < 0)
                    throw new Exception(msg);
            }
            catch (Exception ee)
            {
                log.WriteMyLog("门诊确费失败:" + ee.Message);
                throw new Exception("确费失败,因为:" + ee.Message);
            }
        }

        /// <summary>
        /// 调用PACS接口回传报告结果
        /// </summary>
        /// <param name="jcxx"></param>
        private void SendResult(T_JCXX jcxx)
        {
            #region HIS Data Schema

//< Request >
//< reportdoctorname ></ reportdoctorname > 报告医生    nvarchar
//< checkdoctor ></ checkdoctor > 审核医生    nvarchar
//< checktime ></ checktime > 审核时间    datetime
//< reporttime ></ reporttime > 报告时间    datetime
//< exsee ></ exsee > 检查所见    nvarchar
//< exrel ></ exrel > 检查结论    nvarchar
//< extype ></ extype > 检查类型    nvarchar
//< exadvis ></ exadvis > 医生建议    nvarchar
//< exstatue ></ exstatue > 检查状态    nvarchar
//< isJzreport ></ isJzreport > 是否为急诊报告 Int
//< exsqdh ></ exsqdh > 申请单号    nvarchar
//< IsDelete ></ IsDelete > 删除标志    Int
//< exwjz ></ exwjz > 危急值内容   nvarchar
//< reportfilepath ></ reportfilepath > 病人网页报告地址    nvarchar
//< vendor ></ vendor > 科室PACS厂商名称  nvarchar
//< opertype ></ opertype >执行操作类型  nvarchar
//< exid ></ exid > 检查ID，院PACS提供 Int
//< ImageFile >
//< Path1 > 报告图像路径1 | 1 </ Path1 >
//< Path2 > 报告图像路径2 | 2 </ Path2 >
//< Path3 > 报告图像路径3 | 0 </ Path3 >
//</ ImageFile >
//    图像路径
//</ Request >

            #endregion

            //2017年3月16日 刘冬阳:申请序号exid在保存病人时已写入sqxh
            //获取申请序号
            //jcxx.F_SQXH = GetSqxhFormPacs(jcxx);

            //如果没有审核时间,用报告时间代替审核时间
            if (jcxx.F_SPARE5.Trim() == "")
                jcxx.F_SPARE5 = jcxx.F_BGRQ;

            if (string.IsNullOrEmpty(jcxx.F_SQXH.Trim()))
                throw new Exception("发送结果失败,因为没有获取到extId");

            string returnXml =
                $@"<Request>
                                    <reportdoctorname>{jcxx.F_BGYS}</reportdoctorname>
                                    <checkdoctor>{jcxx.F_SHYS}</checkdoctor>
                                    <checktime>{Convert.ToDateTime(jcxx.F_BGRQ).ToString("yyyy-MM-dd HH:mm:ss")}</checktime>
                                    <reporttime>{Convert.ToDateTime(jcxx.F_BGRQ).ToString("yyyy-MM-dd HH:mm:ss")}</reporttime> 
                                    <exsee>{jcxx.F_RYSJ}</exsee>
                                    <exrel>{jcxx.F_BLZD}</exrel>  
                                    <extype>病理</extype>
                                    <exadvis></exadvis>
                                    <exstatue>已打印发布报告</exstatue> 
                                    <isJzreport>0</isJzreport>
                                    <exsqdh>0</exsqdh>
                                    <IsDelete>0</IsDelete>
                                    <exwjz>{jcxx.F_BZ}</exwjz>
                                    <reportwebfilepath>http://192.168.42.158/pathwebrpt/index_y.asp?yzh={jcxx.F_SQXH}</reportwebfilepath>
                                    <vendor>无锡朗珈</vendor>
                                    <opertype></opertype>
                                    <exid>{jcxx.F_SQXH}</exid>
                                    <isPositive>{jcxx.F_YYX}</isPositive>
                                    <ImageFile>
                                        {GetImageFile(jcxx)}                                    
                                    </ImageFile>

                                    </Request>";

            log.WriteMyLog("开始调用返回结果接口,入参为:\r\n" + returnXml);

            try
            {
                //调用回传结果接口
                string returnXML = new ServiceForward().CallService(returnXml);

                var row = GetDataSetByXml(returnXML).Tables[0].Rows[0];
                int count = Int32.Parse(row["Iden"].ToString());
                string msg = row["reltxt"].ToString();

                //如果count<0则回传失败
                if (count < 0)
                    throw new Exception(msg);
                else
                    log.WriteMyLog("回传报告成功!");
            }
            catch (Exception e)
            {
                log.WriteMyLog("回传报告失败,因为:" + e.Message +"\r\n接口返回值为:"+returnXml);
                throw;
            }
        }

        /// <summary>
        /// 获取extid,该值在pacs中代表报告id,必填
        /// </summary>
        /// <param name="jcxx"></param>
        /// <returns></returns>
        public string GetSqxhFormPacs(T_JCXX jcxx)
        {
            //判断是门诊还是住院
            string operType = "";
            if (string.IsNullOrEmpty(jcxx.F_MZH.Trim()) == false)
                operType = "0";
            else if (string.IsNullOrEmpty(jcxx.F_ZYH.Trim()) == false)
                operType = "1";

            string sxml = "";

            try
            {
                HnsfyRegistrationInterface.WebService service = new HnsfyRegistrationInterface.WebService();
                sxml = service.QuePatiengInfo("", "", "", jcxx.F_ZYH, jcxx.F_MZH, "", operType);

                var dt = GetDataSetByXml(sxml).Tables[0];
                var f = new ApplicationSelector();
                f.DataTable = dt;
                f.ShowDialog();
                var exid = f.SelectedRow["exid"].ToString();
                //log.请求患者信息成功
                log.WriteMyLog("请求HIS患者信息成功,返回值:\r\n" + sxml);
                return exid;
            }
            catch (Exception e)
            {
                //log.请求患者信息失败
                log.WriteMyLog("查询HIS患者信息失败:" + e.Message);
                throw new Exception("查询HIS患者信息失败:" + e.Message);
            }
        }

        /// <summary>
        /// 病理图片上传到外部系统ftp,并获取病理图片xml字符串
        /// </summary>
        /// <param name="jcxx">检查信息表</param>
        /// <returns></returns>
        public string GetImageFile(T_JCXX jcxx)
        {
            var sqlWhere = $" F_BLH='{jcxx.F_BLH}' and F_SFDY='1' ";
            var txList = new T_TX_DAL().GetList(sqlWhere);

            string imageFileString = "";

            #region ftp变量声明

            IniFiles f = new IniFiles("sz.ini");
            string ftpserver = f.ReadString("ftp", "ftpip", "").Replace("\0", "");
            string ftpuser = f.ReadString("ftp", "user", "ftpuser").Replace("\0", "");
            string ftppwd = f.ReadString("ftp", "pwd", "ftp").Replace("\0", "");
            string ftplocal = f.ReadString("ftp", "ftplocal", "c:\\temp").Replace("\0", "");
            string ftpremotepath = f.ReadString("ftp", "ftpremotepath", "pathimages").Replace("\0", "");
            string ftps = f.ReadString("ftp", "ftp", "").Replace("\0", "");
            string txpath = f.ReadString("txpath", "txpath", "").Replace("\0", "");
            FtpWeb fw = new FtpWeb(ftpserver, ftpremotepath, ftpuser, ftppwd);

            string ftpserver2 = f.ReadString("ftpup", "ftpip", "").Replace("\0", "");
            string ftpuser2 = f.ReadString("ftpup", "user", "ftpuser").Replace("\0", "");
            string ftppwd2 = f.ReadString("ftpup", "pwd", "ftp").Replace("\0", "");
            string ftplocal2 = f.ReadString("ftpup", "ftplocal", "c:\\temp").Replace("\0", "");
            string ftpremotepath2 = f.ReadString("ftpup", "ftpremotepath", "").Replace("\0", "");
            string ftps2 = f.ReadString("ftp", "ftp", "").Replace("\0", "");
            FtpWeb fwup = new FtpWeb(ftpserver2, ftpremotepath2, ftpuser2, ftppwd2);

            string ftpPath = $@"ftp:\\{ftpserver}\"; //这里要替换为本地配置文件的ftp路径
            string ftpPath2 = $@"ftp:\\{ftpserver2}\"; //这里要替换为本地配置文件的ftp路径

            #endregion

            for (int i = 0; i < txList.Count; i++)
            {
                var upFilePath = jcxx.F_TXML + "\\";
                var upFileName = upFilePath + txList[i].F_TXM.Trim();

                //下载图片
                string ftpstatus = "";
                if (!Directory.Exists(ftplocal + "\\" + upFilePath))
                {
                    Directory.CreateDirectory(ftplocal + "\\" + upFilePath);
                }
                try
                {
                    fw.Download(ftplocal + "\\" + upFilePath, upFileName, txList[i].F_TXM.Trim(), out ftpstatus);
                    if (ftpstatus == "Error")
                    {
                        throw new Exception("Error");
                    }
                }
                catch (Exception e)
                {
                    log.WriteMyLog("下载ftp图片失败,病理号:" + jcxx.F_BLH +
                                   "\r\n失败原因:" + e.Message);
                    continue;
                }


                //上传到目标ftp
                string ftpstatusUP = "";
                try
                {
                    fwup.Makedir("BL", out ftpstatusUP);
                    fwup.Makedir("BL\\" + upFilePath, out ftpstatusUP);
                    fwup.Upload(ftplocal + "\\" + upFileName, "BL\\" + upFilePath, out ftpstatusUP);
                    if (ftpstatusUP == "Error")
                    {
                        throw new Exception("Error");
                    }
                }
                catch (Exception e)
                {
                    log.WriteMyLog("上传ftp图片失败,病理号:" + jcxx.F_BLH +
                                   "\r\n失败原因:" + e.Message);
                    continue;
                }

                imageFileString +=
                    $@"<Path{i + 1}> {ftpPath2 + "BL\\" + upFileName} | 1 </Path{i + 1}>                                    ";
            }

            return imageFileString;
        }

        #endregion

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
    }
}