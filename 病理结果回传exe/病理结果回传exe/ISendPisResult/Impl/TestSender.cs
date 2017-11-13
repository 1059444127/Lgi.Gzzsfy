using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SendPisResult.ISendPisResult.Impl
{
    [HospName("测试医院")]
    public class TestSender:ISendPisResult,ISendPisResultPlus
    {
        #region Implementation of ISendPisResult

        public void SendResult(string pathoNo)
        {
            MessageBox.Show("测试接口调用成功,病理号是:"+pathoNo);
        }

        #endregion

        public void SendResult(string pathoNo, ReportType reportType, string bgxh, EditType editType, PisAction pisAction,
            string yymc)
        {
            log.WriteMyLog("aaaaaaaaaaaaaaaaa");
            MessageBox.Show(
                $"测试调用复杂接口成功:{pathoNo}|{reportType.ToString()}|{bgxh}|{editType.ToString()}|{pisAction.ToString()}");
        }
    }
}
