﻿using System;
using System.IO;

namespace LGInterface
{
    class log
    {
        public static void WriteMyLog(string message)
        {
            string LOG_FOLDER = AppDomain.CurrentDomain.BaseDirectory + "Log";
            try
            {
                //日志文件路径 
                string filePath = LOG_FOLDER + "\\PATHGETHIS-" + DateTime.Now.ToString("yyyyMMdd") + ".log";
                if (!System.IO.Directory.Exists(LOG_FOLDER))
                {
                    Directory.CreateDirectory(LOG_FOLDER);
                }
                if (!File.Exists(filePath))//如果文件不存在 
                {
                    File.Create(filePath).Close();
                }
                StreamWriter sw = File.AppendText(filePath);
                sw.WriteLine("-------------------------------------------------------------------------------------");
                sw.WriteLine("Date:" + DateTime.Now.ToShortDateString() + " Time:" + DateTime.Now.ToString("HH:mm:ss"));
                sw.WriteLine(message);
                //sw.WriteLine(ex.StackTrace);
                sw.WriteLine();
                sw.Close();
            }
            catch
            { }
        }
        public static string readlog()
        {
            string LOG_FOLDER = AppDomain.CurrentDomain.BaseDirectory + "Log";
            string hl7log = "";
            try
            {
                //日志文件路径 
                string filePath = LOG_FOLDER + "\\" + DateTime.Now.ToShortDateString() + ".log";
                if (!System.IO.Directory.Exists(LOG_FOLDER))
                {
                    Directory.CreateDirectory(LOG_FOLDER);
                }
                if (!File.Exists(filePath))//如果文件不存在 
                {
                    File.Create(filePath).Close();
                }
                hl7log = File.ReadAllText(filePath);
                return hl7log;
            }
            catch
            {
                return "";
            }


        }
        public static void clearlog()
        {
            string LOG_FOLDER = AppDomain.CurrentDomain.BaseDirectory + "Log";
            //string hl7log = "";
            try
            {
                //日志文件路径 
                string filePath = LOG_FOLDER + "\\" + DateTime.Now.ToShortDateString() + ".log";
                if (!System.IO.Directory.Exists(LOG_FOLDER))
                {
                    Directory.CreateDirectory(LOG_FOLDER);
                }
                if (!File.Exists(filePath))//如果文件不存在 
                {
                    File.Create(filePath).Close();
                }
                File.WriteAllText(filePath, "");

            }
            catch
            {

            }
        }

    }
}
