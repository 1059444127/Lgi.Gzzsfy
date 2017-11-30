using Microsoft.VisualStudio.TestTools.UnitTesting;
using SendPisResult.ISendPisResult.Impl.广州中山附一_上海岱嘉;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendPisResult.Util;

namespace SendPisResult.ISendPisResult.Impl.广州中山附一_上海岱嘉.Tests
{
    [TestClass()]
    public class ResultSenderTestsFor广州中山附一_上海岱嘉
    {
        [TestMethod()]
        public void SendResultTest()
        {
            //住院:201608928,bz,湖南省妇幼医院_广州邦弘
            //门诊:10152376,bz,湖南省妇幼医院_广州邦弘
            var args = "172280,bz,广州中山附一_上海岱嘉";
            var argsAdv = "172280^cg^1^old^save";
            var values = args.Split(',');
            var hospName = values[2]; //医院名称
            var pathoNo = values[0]; //病理号
            ISendPisResult sender = PisResultSenderFactory.GerResultSender(hospName);
            ISendPisResultPlus senderPisResultPlus = PisResultSenderFactory.GerResultSenderPlus(hospName);
            //sender.SendResult(pathoNo);
            //senderPisResultPlus.SendResult(argsAdv);
        }

        [TestMethod()]
        public  void  CallSendResultPlus()
        {
            var args = "201700005^cg^1^old^save";

            if (args.IndexOf("^") > -1)
            {
                try
                {
                    string[] aa = args.Split('^');
                    var blh = aa[0].ToString();
                    blh = aa[0].ToString();
                    var bglx = aa[1].ToString().ToLower(); //cg/bd/bc
                    var reportNumber = aa[2].ToString();
                    var czlb = aa[3].ToString().ToLower(); //new/old
                    var dz = aa[4].ToString().ToLower(); //save/qxsh/dy/qxdy

                    var reportType = ReportType.常规报告;
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
                    EditType editType = czlb == "new" ? EditType.新建 : EditType.修改;
                    PisAction pisAction;
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

                    var hospName = "广州中山附一_上海岱嘉";

                    ISendPisResultPlus sender = PisResultSenderFactory.GerResultSenderPlus(hospName);
                    sender.SendResult(blh, reportType, reportNumber, editType, pisAction, hospName);
                }
                catch
                {
                    throw;
                }
            }
        }

        [TestMethod]
        public void ZipImage()
        {
            var sor = @"C:\temp\201700017\ferrets_couple_grass_jump_playful_52405_3840x2400.jpg";
            var dst = @"C:\temp\201700017\ferrets_couple_grass_jump_playful_52405_3840x2400.jpg";
            Image sourceImage = Image.FromFile(sor);
            Image smallImage = ImageHelper.GetReducedImage(320, 240, sourceImage);
            sourceImage.Dispose();
            smallImage.Save(dst);
            smallImage.Dispose();
        }

    }
}