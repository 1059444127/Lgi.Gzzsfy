namespace SendPisResult.ISendPisResult
{
    /// <summary>
    /// 简单接口,接收病理号,给第三方返回结果
    /// </summary>
    public interface ISendPisResult
    {
        /// <summary>
        /// 简单接口,接收病理号,给第三方返回结果
        /// </summary>
        /// <param name="pathoNo"></param>
        /// <param name="aa"></param>
        void SendResult(string pathoNo);
    }

    /// <summary>
    /// 负责接口,PIS在保存,修改,审核,取消审核,打印时都会调用,可以做相应的处理
    /// </summary>
    public interface ISendPisResultPlus
    {
        /// <summary>
        /// 入参  病理号^报告类型^报告序号^new/old^save/qxsh
        /// blh^cg/bd/bc^bgxh^new/old^save/qxsh/dy/qxdy
        /// 报告类型：cg 常规报告，bd 冰冻报告 ，bc 补充报告
        /// new 新登记，
        /// save 保存，qxsh 取消审核 ， dy 打印 ，qxdy 取消打印
        /// </summary>
        /// <param name="pathoNo">病理号</param>
        /// <param name="reportType">报告类型</param>
        /// <param name="bgxh">报告序号</param>
        /// <param name="editType">编辑类型--新建或修改</param>
        /// <param name="pisAction">操作类型</param>
        /// <param name="yymc">医院名称</param>
        void SendResult(string pathoNo,ReportType reportType,string bgxh,EditType editType,PisAction pisAction,string yymc);
    }

    public enum PisAction
    {
        新登记,保存,取消审核,打印,取消打印,未知
    }

    public enum EditType
    {
        新建,修改
    }

    public enum ReportType
    {
        常规报告,冰冻报告,补充报告
    }
}