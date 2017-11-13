using System;
using System.Collections.Generic;
using System.Text;

namespace SendPisResult.ISendPisResult
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class HospNameAttribute:Attribute
    {
        public string HospName { get; set; }

        public HospNameAttribute(string hospName)
        {
            HospName = hospName;
        }
    }
}
