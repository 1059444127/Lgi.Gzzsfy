using Microsoft.VisualStudio.TestTools.UnitTesting;
using SendPisResult.ISendPisResult.Impl.广州中山附一_上海岱嘉;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendPisResult.ISendPisResult.Impl.广州中山附一_上海岱嘉.Tests
{
    [TestClass()]
    public class FtpHelperTests
    {
        [TestMethod()]
        public void UpdateRecordXmlTest()
        {
            new FtpHelper().UploadRecordXml("1231231dqdw","999");
        }
    }
}