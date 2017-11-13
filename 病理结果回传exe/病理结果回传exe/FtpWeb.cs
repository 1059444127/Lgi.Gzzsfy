using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace SendPisResult
{
    public class FtpWeb
    {
        string ftpServerIP;
        string ftpRemotePath;
        string ftpUserID;
        string ftpPassword;
        string ftpURI;

        /// <summary>
        /// 连接FTP
        /// </summary>
        /// <param name="FtpServerIP">FTP连接地址</param>
        /// <param name="FtpRemotePath">指定FTP连接成功后的当前目录, 如果不指定即默认为根目录</param>
        /// <param name="FtpUserID">用户名</param>
        /// <param name="FtpPassword">密码</param>
        public FtpWeb(string FtpServerIP, string FtpRemotePath, string FtpUserID, string FtpPassword)
        {
            ftpServerIP = FtpServerIP;
            ftpRemotePath = FtpRemotePath;
            ftpUserID = FtpUserID;
            ftpPassword = FtpPassword;
            if (FtpRemotePath != "")
            {
                ftpURI = "ftp://" + ftpServerIP + "/" + ftpRemotePath + "/";
            }
            else
            {
                ftpURI = "ftp://" + ftpServerIP + "/";
            }
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="localFilePath">本地文件路径</param>
        /// <param name="serverFileFullName">服务端文件全名</param>
        /// <param name="localFileName">本地文件名</param>
        /// <param name="status">执行结果,如果成功返回OK</param>
        public void Download(string localFilePath, string serverFileFullName, string localFileName, out string status)
        {
            status = "OK";
            FtpWebRequest reqFTP;
            try
            {
                FileStream outputStream = new FileStream(localFilePath + "\\" + localFileName, FileMode.Create);

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + serverFileFullName));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];

                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }

                ftpStream.Close();
                outputStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                log.WriteMyLog("Ftp.Download失败,Uri:"+ ftpURI + serverFileFullName);
                Insert_Standard_ErrorLog.Insert("FtpWeb", "Download Error --> " + ex.Message);
                status = "Error";
            }
        }

        public void Makedir(string dirname, out string status)
        {
            status = "OK";



            string uri = ftpURI + dirname;

            FtpWebRequest reqFTP;



            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));

            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
            reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;

            try
            {
                FtpWebResponse response = reqFTP.GetResponse() as FtpWebResponse;

            }
            catch (Exception ex)
            {

                //Insert_Standard_ErrorLog.Insert("FtpWeb", "Upload Error --> " + ex.Message);
                //status = "Error";
            }

        }

        public void Upload(string filename, string path, out string status)
        {
            status = "OK";

            FileInfo fileInf = new FileInfo(filename);

            string uri = ftpURI + path + "/" + fileInf.Name;
            if (path == "")
                uri = ftpURI + fileInf.Name;
            FtpWebRequest reqFTP;



            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));

            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
            reqFTP.KeepAlive = false;
            //try
            //{
            //    reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;

            //   FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

            //    response.Close();
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}




            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

            reqFTP.UseBinary = true;

            reqFTP.ContentLength = fileInf.Length;

            int buffLength = 2048;

            byte[] buff = new byte[buffLength];

            int contentLen;

            FileStream fs = fileInf.OpenRead();

            try
            {

                Stream strm = reqFTP.GetRequestStream();

                contentLen = fs.Read(buff, 0, buffLength);

                while (contentLen != 0)
                {

                    strm.Write(buff, 0, contentLen);

                    contentLen = fs.Read(buff, 0, buffLength);

                }

                strm.Close();

                fs.Close();

            }

            catch (Exception ex)
            {
                log.WriteMyLog("Ftp.Upload,Uri:" + ftpURI);
                Insert_Standard_ErrorLog.Insert("FtpWeb", "Upload Error --> " + ex.Message);
                status = "Error";
            }

        }

        public void Upload(string filename, string path, out string status, string ftpfilename)
        {
            status = "OK";

            FileInfo fileInf = new FileInfo(filename);

            string uri = ftpURI + path + "/" + ftpfilename;
            if (path == "")
                uri = ftpURI + ftpfilename;
            FtpWebRequest reqFTP;


            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));

            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
            reqFTP.KeepAlive = false;
            //reqFTP.KeepAlive = true;
            //try
            //{
            //    reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;

            //   FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

            //    response.Close();
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}




            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

            reqFTP.UseBinary = true;
            reqFTP.UsePassive = true;

            reqFTP.ContentLength = fileInf.Length;

            int buffLength = 2048;

            byte[] buff = new byte[buffLength];

            int contentLen;

            FileStream fs = fileInf.OpenRead();

            try
            {

                Stream strm = reqFTP.GetRequestStream();

                contentLen = fs.Read(buff, 0, buffLength);

                while (contentLen != 0)
                {

                    strm.Write(buff, 0, contentLen);

                    contentLen = fs.Read(buff, 0, buffLength);

                }

                strm.Close();

                fs.Close();

            }

            catch (Exception ex)
            {

                Insert_Standard_ErrorLog.Insert("FtpWeb", "Upload Error --> " + ex.Message);
                status = "Error";
            }

        }





    }
    public class Insert_Standard_ErrorLog
    {
        public static void Insert(string x, string y)
        {

            MessageBox.Show(y);
            //Application.Exit();
        }
    }
}