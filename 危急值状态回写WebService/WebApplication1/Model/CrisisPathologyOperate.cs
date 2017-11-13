using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Web;
using System.Xml.Serialization;

namespace LangJia.Service.Model
{




    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class CrisisPathologyOperate
    {
        private Dictionary<string, string> _statusDic = new Dictionary<string, string>()
        {
            {"0","发布" },{"1","科室已接收" },{"2","科室已处理" },{"3","危急值已取消" },{"4","已代理接收" },
            { "5","已代理处理" },{"11","超时未接收" },{"12","超时未处理" }
        };

        private string requestNoField;

        private string sampleIDField;

        private string createTimeField;

        private string statusField;

        private string remarkField;

        private string operationInfoField;

        private string operationSystemField;

        private string operationTypeField;

        private string operationTypeNameField;

        private string principalCodeField;

        private string principalNameField;

        private string responseAccoutIDField;

        private string responseCodeField;

        private string responseMemoField;

        private string responseNameField;

        private string responseTimeField;

        private string responseTypeField;

        private string responseTypeNameField;

        /// <summary>
        /// 申请单号
        /// </summary>
        public string RequestNo
        {
            get
            {
                return this.requestNoField;
            }
            set
            {
                this.requestNoField = value;
            }
        }

        /// <summary>
        /// 标本号即试管条码
        /// </summary>
        public string SampleID
        {
            get
            {
                return this.sampleIDField;
            }
            set
            {
                this.sampleIDField = value;
            }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime
        {
            get
            {
                return this.createTimeField;
            }
            set
            {
                this.createTimeField = value;
            }
        }

        /// <summary>
        /// 处理状态，0=发布,1=科室已接收,2=科室已处理,3=危急值已取消,4=已代理接收,5=已代理处理,11=超时未接收,12=超时未处理
        /// </summary>
        public string Status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get
            {
                return this.remarkField;
            }
            set
            {
                this.remarkField = value;
            }
        }

        /// <summary>
        /// 操作内容
        /// </summary>
        public string OperationInfo
        {
            get
            {
                return this.operationInfoField;
            }
            set
            {
                this.operationInfoField = value;
            }
        }

        /// <summary>
        /// 操作源，1：门户系统，2：LIS（血液、免疫、生化），3：LIS（细菌、骨髓）,4： 病理系统,5：放射系统,6：内镜系统,7：超声系统,8：核医学系统,9：心电系统
        /// </summary>
        public string OperationSystem
        {
            get
            {
                return this.operationSystemField;
            }
            set
            {
                this.operationSystemField = value;
            }
        }

        /// <summary>
        /// 操作类别, 0:发布危机值,1：接收；2：代接收，3：电话通知，4：代处理，5：科室处理，6：取消危机值
        /// </summary>
        public string OperationType
        {
            get
            {
                return this.operationTypeField;
            }
            set
            {
                this.operationTypeField = value;
            }
        }

        /// <summary>
        /// 操作类别, 0:发布危机值,1：接收；2：代接收，3：电话通知，4：代处理，5：科室处理，6：取消危机值
        /// </summary>
        public string OperationTypeName
        {
            get
            {
                return this.operationTypeNameField;
            }
            set
            {
                this.operationTypeNameField = value;
            }
        }

        /// <summary>
        /// 被代理人编号
        /// </summary>
        public string PrincipalCode
        {
            get
            {
                return this.principalCodeField;
            }
            set
            {
                this.principalCodeField = value;
            }
        }

        /// <summary>
        /// 被代理人姓名
        /// </summary>
        public string PrincipalName
        {
            get
            {
                return this.principalNameField;
            }
            set
            {
                this.principalNameField = value;
            }
        }

        /// <summary>
        /// 操作者门户账号ID
        /// </summary>
        public string ResponseAccoutID
        {
            get
            {
                return this.responseAccoutIDField;
            }
            set
            {
                this.responseAccoutIDField = value;
            }
        }

        /// <summary>
        /// 操作者工号
        /// </summary>
        public string ResponseCode
        {
            get
            {
                return this.responseCodeField;
            }
            set
            {
                this.responseCodeField = value;
            }
        }

        /// <summary>
        /// 响应备注
        /// </summary>
        public string ResponseMemo
        {
            get
            {
                return this.responseMemoField;
            }
            set
            {
                this.responseMemoField = value;
            }
        }

        /// <summary>
        /// 操作者姓名
        /// </summary>
        public string ResponseName
        {
            get
            {
                return this.responseNameField;
            }
            set
            {
                this.responseNameField = value;
            }
        }

        /// <summary>
        /// 处理时间
        /// </summary>
        public string ResponseTime
        {
            get
            {
                return this.responseTimeField;
            }
            set
            {
                this.responseTimeField = value;
            }
        }

        /// <summary>
        /// 应类别,11-已电话通知患者,12-多次电话通知患者无人接听,15-病人信息不正确,14-其他,21-符合病情，无需处理,22-符合病情，必须处理,25-病人信息不正确,23-与病情不符，建议重检,24-其他
        /// </summary>
        public string ResponseType
        {
            get
            {
                return this.responseTypeField;
            }
            set
            {
                this.responseTypeField = value;
            }
        }

        /// <summary>
        /// 响应类别名称,11-已电话通知患者,12-多次电话通知患者无人接听,15-病人信息不正确,14-其他,21-符合病情，无需处理,22-符合病情，必须处理,25-病人信息不正确,23-与病情不符，建议重检,24-其他
        /// </summary>
        public string ResponseTypeName
        {
            get
            {
                return this.responseTypeNameField;
            }
            set
            {
                this.responseTypeNameField = value;
            }
        }

        public string StatusText
        {
            get
            {
                if (_statusDic.ContainsKey(Status))
                    return _statusDic[Status];
                return "未知";
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class CrisisPathologyOperateList
    {

        private CrisisPathologyOperate[] crisisPathologyOperateField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CrisisPathologyOperate")]
        public CrisisPathologyOperate[] CrisisPathologyOperate
        {
            get
            {
                return this.crisisPathologyOperateField;
            }
            set
            {
                this.crisisPathologyOperateField = value;
            }
        }
    }

}