using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using SendPisResult.ISendPisResult.Impl;

namespace SendPisResult.ISendPisResult
{
    public static class PisResultSenderFactory
    {
        public static ISendPisResult GerResultSender(string hospName)
        {
            Assembly assembly = Assembly.GetAssembly(typeof (TestSender));

            var types = assembly.GetTypes();
            foreach (Type type in types)
            {
                if (type.GetInterface(nameof(ISendPisResult)) == null)
                    continue;

                var attributes = type.GetCustomAttributes(typeof (HospNameAttribute), true);
                if (attributes.Length > 0 && ((HospNameAttribute)attributes[0]).HospName == hospName)
                    return (ISendPisResult) assembly.CreateInstance(type.FullName);
            }

            throw new Exception($"返回病理结果失败,因为没有找到 {hospName} 的[简单]接口实现.请联系管理员!");
        }

        public static ISendPisResultPlus GerResultSenderPlus(string hospName="")
        {
            Assembly assembly = Assembly.GetAssembly(typeof(TestSender));

            if(hospName=="")
                hospName = IniFiles.GetInstant("sz.ini").ReadString("savetohis", "yymc", "123").Replace("\0", "");

            var types = assembly.GetTypes();
            foreach (Type type in types)
            {
                if (type.GetInterface(nameof(ISendPisResultPlus)) == null)
                    continue;

                var attributes = type.GetCustomAttributes(typeof(HospNameAttribute), true);
                if (attributes.Length > 0 && ((HospNameAttribute)attributes[0]).HospName == hospName)
                    return (ISendPisResultPlus)assembly.CreateInstance(type.FullName);
            }

            throw new Exception($"返回病理结果失败,因为没有找到 {hospName} 的[复杂]接口实现.请联系管理员!");
        }
    }
}