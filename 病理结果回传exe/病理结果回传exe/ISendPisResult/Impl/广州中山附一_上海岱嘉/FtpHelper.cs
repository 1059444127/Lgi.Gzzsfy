using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SendPisResult.Models;

namespace SendPisResult.ISendPisResult.Impl.广州中山附一_上海岱嘉
{
    public class FtpHelper 
    {
        private FtpWeb ftpPis;
        private FtpWeb ftpUp;
        IniFiles f = new IniFiles("sz.ini");

        public FtpHelper()
        {
            string ftpserver = f.ReadString("ftp", "ftpip", "").Replace("\0", "");
            string ftpuser = f.ReadString("ftp", "user", "ftpuser").Replace("\0", "");
            string ftppwd = f.ReadString("ftp", "pwd", "ftp").Replace("\0", "");
            string ftplocal = f.ReadString("ftp", "ftplocal", "c:\\temp").Replace("\0", "");
            string ftpremotepath = f.ReadString("ftp", "ftpremotepath", "pathimages").Replace("\0", "");
            string ftps = f.ReadString("ftp", "ftp", "").Replace("\0", "");
            string txpath = f.ReadString("txpath", "txpath", "").Replace("\0", "");
            ftpPis = new FtpWeb(ftpserver, ftpremotepath, ftpuser, ftppwd);

            string ftpserver2 = f.ReadString("ftpup", "ftpip", "").Replace("\0", "");
            string ftpuser2 = f.ReadString("ftpup", "user", "ftpuser").Replace("\0", "");
            string ftppwd2 = f.ReadString("ftpup", "pwd", "ftp").Replace("\0", "");
            string ftplocal2 = f.ReadString("ftpup", "ftplocal", "c:\\temp").Replace("\0", "");
            string ftpremotepath2 = f.ReadString("ftpup", "ftpremotepath", "").Replace("\0", "");
            string ftps2 = f.ReadString("ftp", "ftp", "").Replace("\0", "");
            ftpUp = new FtpWeb(ftpserver2, ftpremotepath2, ftpuser2, ftppwd2);
        }

        /// <summary>
        /// 把xml字符串保存到本地文件夹,并上传到院方平台ftp
        /// </summary>
        /// <param name="xml">xml字符串</param>
        /// <param name="fileId">结果数据主表ID</param>
        public void UploadRecordXml(string xml,  string fileId)
        {
            var fileName = fileId + ".xml";

            var serverDir = $"{DateTime.Now.ToString("yyyy")}\\{DateTime.Now.ToString("MM")}\\{DateTime.Now.ToString("dd")}\\{fileId}";
            var localdir = $"XmlReport\\" + serverDir;
            var localFileFullName = localdir +"\\"+ fileName;
            var serverFileFullName = serverDir +"\\"+ fileName;

            //新建本地文件夹
            if (Directory.Exists(localdir) == false)
                Directory.CreateDirectory(localdir);

            //新建ftp文件夹
            string err = "";
            FtpMakedir(serverDir, out err);
            if (err != "OK")
            {
                log.WriteMyLog($"在平台ftp新建文件夹失败,serverDir={serverDir}:" + err);
                throw new Exception($"在平台ftp新建文件夹失败,serverDir={serverDir}:" + err);
            }

            //xml保存在本地一份
            StreamWriter writer = new StreamWriter(localFileFullName);
            writer.Write(xml);
            writer.Close();

            //上传到平台ftp
            err = "";
            ftpUp.Upload(localFileFullName, serverDir, out err);
            if (err != "OK")
            {
                log.WriteMyLog($"xml上传平台ftp失败,serverFileName={serverFileFullName}:" + err);
                throw new Exception($"xml上传平台ftp失败,serverFileName={serverFileFullName}:" + err);
            }
        }

        /// <summary>
        /// 把xml字符串保存到本地文件夹,并上传到院方平台ftp
        /// </summary>
        /// <param name="localPdfFullName">xml字符串</param>
        /// <param name="fileId">结果数据主表ID</param>
        /// <param name="jcxx"></param>
        public void UploadPdf(string localPdfFullName, string fileId, T_JCXX jcxx)
        {
            var fileName = fileId + ".xml";

            var serverDir = $"{DateTime.Now.ToString("yyyy")}\\{DateTime.Now.ToString("MM")}\\{DateTime.Now.ToString("dd")}\\{fileId}";
            var localDir = f.ReadString("ftpup", "ftplocal", @"c:\temp");
            var localFileFullName =localDir + "\\"+ jcxx.F_BLH + "\\"+ localPdfFullName;
            var serverFileSafeName = fileId +".pdf";
            
            //新建ftp文件夹
            string err = "";
            FtpMakedir(serverDir, out err);
            if (err != "OK")
            {
                log.WriteMyLog($"在平台ftp新建文件夹失败,serverDir={serverDir}:" + err);
                throw new Exception($"在平台ftp新建文件夹失败,serverDir={serverDir}:" + err);
            }

            //上传到平台ftp
            err = "";
            ftpUp.Upload(localFileFullName, serverDir, out err,serverFileSafeName);
            if (err != "OK")
            {
                log.WriteMyLog($"pdf上传平台ftp失败,serverFileName={serverFileSafeName}:" + err);
                throw new Exception($"pdf上传平台ftp失败,serverFileName={serverFileSafeName}:" + err);
            }
            log.WriteMyLog($"pdf上传平台成功");

        }

        public void UploadPic(List<T_TX> txList,T_JCXX jcxx,string fileId)
        {
            var targetServerDir = $"{DateTime.Now.ToString("yyyy")}\\{DateTime.Now.ToString("MM")}\\{DateTime.Now.ToString("dd")}\\{fileId}";
            var pisServerDir = jcxx.F_TXML;
            var localdir = $"Pics\\" + targetServerDir;

            for (int i = 0; i < txList.Count; i++)
            {
                var upFilePath = targetServerDir + "\\";
                var serverFileFullName = pisServerDir +"\\"+ txList[i].F_TXM.Trim();
                var localFileFullName = localdir + "\\" + txList[i].F_TXM.Trim();

                //下载图片
                string ftpstatus = "";
                if (!Directory.Exists(localdir))
                {
                    Directory.CreateDirectory(localdir);
                }
                try
                {
                    ftpPis.Download(localdir, serverFileFullName, txList[i].F_TXM.Trim(), out ftpstatus);
                    if (ftpstatus == "Error")
                    {
                        throw new Exception("下载图片失败");
                    }
                }
                catch (Exception e)
                {
                    log.WriteMyLog("下载ftp图片失败,病理号:" + jcxx.F_BLH +
                                   "\r\n失败原因:" + e);
                    continue;
                }


                //上传到目标ftp
                string ftpstatusUP = "";
                try
                {
                    ftpUp.Makedir("BL", out ftpstatusUP);
                    ftpUp.Makedir("BL\\" + upFilePath, out ftpstatusUP);
                    ftpUp.Upload(localFileFullName, targetServerDir, out ftpstatusUP);
                    if (ftpstatusUP == "Error")
                    {
                        throw new Exception("Error");
                    }
                }
                catch (Exception e)
                {
                    log.WriteMyLog("上传ftp图片失败,病理号:" + jcxx.F_BLH +
                                   "\r\n失败原因:" + e);
                    continue;
                }

                //上传完成后删除本地图片
                try
                {
                    File.Delete(localFileFullName);
                }
                catch
                {
                    
                }
            }
        }

        private void FtpMakedir(string serverDir, out string err)
        {
            err = "";
            var dirs = serverDir.Split('\\');
            for (var i = 0; i < dirs.Length; i++)
            {
                var path = "";
                for (int j = 0; j <= i; j++)
                {
                    path += dirs[j]+"\\";
                }
                path = path.TrimEnd('\\');
                ftpUp.Makedir(path, out err);
            }
        }

        /// <summary>
        /// 获得平台ftp上报告文件的存放路径,用于写入到数据库
        /// </summary>
        /// <param name="docId"></param>
        /// <returns></returns>
        public string GetFileFtpPath(string docId, bool fullPath = false)
        {
            var targetServerDir =
                $"{DateTime.Now.ToString("yyyy")}/{DateTime.Now.ToString("MM")}/{DateTime.Now.ToString("dd")}/{docId}";
            var ftpIp = f.ReadString("ftpup", "ftpip", "").Replace("\0", "");

            if (fullPath)
                return $"ftp://{ftpIp}/{targetServerDir}";
            else
                return targetServerDir;
        }
    }
}
