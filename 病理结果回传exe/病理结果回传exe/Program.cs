using System;
using System.EnterpriseServices;
using System.IO;
using System.Windows.Forms;
using SendPisResult.ISendPisResult;

namespace SendPisResult
{
    public static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            IniFiles f = new IniFiles("sz.ini");
            //Application.Run(new Form1());


            try
            {
                //检测如参是否合法,args[0]应为:病理号,bz
                if (args.Length < 1)
                {
                    var errMessage = "没有接到参数!";
                    throw new Exception(errMessage);
                }

            }
            catch (Exception e)
            {
                log.WriteMyLog("病理结果回传调用失败,因为参数错误:" + e.Message);
                MessageBox.Show("病理结果回传调用失败,因为参数错误:" + e.Message);
                return;
            }

            try
            {
                if (args[0].Contains(","))//简单接口
                {


                    ValidateSimpleArgs(args);
                    var values = args[0].Split(',');
                    var hospName = values[2]; //医院名称
                    var pathoNo = values[0]; //病理号
                    ISendPisResult.ISendPisResult sender = PisResultSenderFactory.GerResultSender(hospName);
                    sender.SendResult(pathoNo);
                }
                else//复杂接口
                {
                    //复杂接口,打印时上传
                    //args1[0] = 病理号^cg/bd/bc^bgxh^new/old^save/qxsh
                    if (CallSendResultPlus(args[0])) return;
                }
            }
            catch (Exception e)
            {
                log.WriteMyLog("病理结果回传调用失败,因为:" + e.Message);
                MessageBox.Show("病理结果回传调用失败,因为:" + e.Message);
                return;
            }
        }

        public static bool CallSendResultPlus(string args)
        {
            log.WriteMyLog("EXE被调用,入参:"+args);

            if (args.IndexOf("^") > -1)
            {
                log.WriteMyLog("开始复杂接口处理,入参:"+args);

                var blh = "";
                var bglx  = ""; //cg/bd/bc
                var bgxh   = "";
                var czlb  = "";//new/old
                var dz =   "";//save/qxsh/dy/qxdy
                EditType editType;
                PisAction pisAction;
                string hospName;
                ReportType reportType;

                try
                {
                    string[] aa = args.Split('^');
                    blh = aa[0].ToString();
                    bglx = aa[1].ToString().ToLower(); //cg/bd/bc
                    bgxh = aa[2].ToString(); //bgxh
                    czlb = aa[3].ToString().ToLower(); //new/old
                    dz = aa[4].ToString().ToLower(); //save/qxsh/dy/qxdy

                    reportType = ReportType.常规报告;
                    switch (bglx)
                    {
                        case "cg":
                        default:
                            reportType = ReportType.常规报告;
                            break;
                        case "bd":
                            reportType = ReportType.冰冻报告;
                            break;
                        case "bc":
                            reportType = ReportType.补充报告;
                            break;
                    }
                    editType = czlb == "new" ? EditType.新建 : EditType.修改;
                    switch (dz)
                    {
                        case "save":
                            pisAction = PisAction.新登记;
                            break;
                        case "qxsh":
                            pisAction = PisAction.取消审核;
                            break;
                        case "dy":
                            pisAction = PisAction.打印;
                            break;
                        case "qxdy":
                            pisAction = PisAction.取消打印;
                            break;
                        default:
                            pisAction = PisAction.未知;
                            break;
                    }

                    hospName = IniFiles.GetInstant("sz.ini").ReadString("savetohis", "yymc", "123").Replace("\0", "");

                }
                catch
                {
                    log.WriteMyLog("传入参数解析出错");
                    return true;
                }

                try
                {
                    ISendPisResultPlus sender = PisResultSenderFactory.GerResultSenderPlus();
                    sender.SendResult(blh, reportType, bgxh, editType, pisAction, hospName);
                }
                catch (Exception e)
                {
                    log.WriteMyLog("接口调用时出错,入参=:" + args+"\r\n"+
                                   "异常信息:+\r\n"+e);
                    return true;

                }

            }
            return false;
        }

        /// <summary>
        /// 验证如参是否正确
        /// 入参格式: 病理号,医院名称
        /// </summary>
        /// <param name="args"></param>
        private static void ValidateSimpleArgs(string[] args)
        {
            var values = args[0].Split(',');
            if (values.Length != 3 || values[2].Trim() == "" || values[1].Trim() == "" || values[0].Trim() == "")
            {
                var errMessage = string.Format("参数[{0}]解析失败.", args[0]);
                throw new Exception(errMessage);
            }
        }
    }
}