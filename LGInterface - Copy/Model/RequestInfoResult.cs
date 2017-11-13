using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace LGInterface.Model
{
    public class RequestInfoResult
    {
        private string _patientNo;

        public string id { get; set; }

        /// <summary>
        /// 系统入口(1 his 2 emr 3 内镜系统)
        /// </summary>
        public string SystemIn { get; set; }

        /// <summary>
        /// 电子申请单号,主键
        /// </summary>
        public string SheetID { get; set; }

        /// <summary>
        /// 就诊流水号
        /// </summary>
        public string JZLSH { get; set; }

        /// <summary>
        /// 是否预约（默认0-不需要预约，1-需要预约）
        /// </summary>
        public string NeedSchedule { get; set; }

        /// <summary>
        /// 病人类型（0-门诊，1-急诊，2-住院，3-体检）维度
        /// </summary>
        public string PatientStyle { get; set; }

        /// <summary>
        /// 挂号类型（0-普通，1-专家，2-专病，3-急诊，4-特约，5-特需，T-体检，9-其他）维度
        /// </summary>
        public string GHStyle { get; set; }

        /// <summary>
        /// 医保/自费（0-非医保，1-医保）维度
        /// </summary>
        public string DetailPatientStyle { get; set; }

        /// <summary>
        /// 门诊病人唯一ID
        /// </summary>
        public string OutHospitalID { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string InHospitalID { get; set; }

        /// <summary>
        /// 病人姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 病人出生性别(1男 2 女)
        /// </summary>
        public string PatientSex { get; set; }

        /// <summary>
        /// 住址
        /// </summary>
        public string PatientAddress { get; set; }

        /// <summary>
        /// 病人年龄
        /// </summary>
        public string Patientage { get; set; }

        /// <summary>
        /// 病人联系电话
        /// </summary>
        public string PatientTel { get; set; }

        /// <summary>
        /// 病理摘要
        /// </summary>
        public string binglizhaiyao { get; set; }

        /// <summary>
        /// 手术所见
        /// </summary>
        public string shoushusuojian { get; set; }

        /// <summary>
        /// 临床诊断
        /// </summary>
        public string LinChuangZhenDuan { get; set; }

        /// <summary>
        /// 开单科室代码
        /// </summary>
        public string DepartMentID { get; set; }

        /// <summary>
        /// 开单科室名称
        /// </summary>
        public string DepartMent { get; set; }

        /// <summary>
        /// 开单医生代码
        /// </summary>
        public string ReqSheetDoctorID { get; set; }

        /// <summary>
        /// 开单医生姓名
        /// </summary>
        public string ReqSheetDoctor { get; set; }

        /// <summary>
        /// 开单日期
        /// </summary>
        public string ReqSheetDate { get; set; }

        /// <summary>
        /// 开单时间
        /// </summary>
        public string ReqSheetTime { get; set; }

        /// <summary>
        /// 检查地点
        /// </summary>
        public string ExamPlace { get; set; }

        /// <summary>
        /// 申请单状态（默认0有效 1 无效）
        /// </summary>
        public string RequestState { get; set; }

        /// <summary>
        /// 入院次数
        /// </summary>
        public string inhospitaltimes { get; set; }

        /// <summary>
        /// 采集人ID
        /// </summary>
        public string CollectionIDpublic { get; set; }

        /// <summary>
        /// 采集人
        /// </summary>
        public string Collectionpublic { get; set; }

        /// <summary>
        /// 采集日期
        /// </summary>
        public string CollectionDatepublic { get; set; }

        /// <summary>
        /// 采集时间
        /// </summary>
        public string CollectionTimepublic { get; set; }

        /// <summary>
        /// IBD特效包埋(0无，1 IBD特效包埋 2 粘膜剥离标本)
        /// </summary>
        public string IBDEmbeddingpublic { get; set; }

        /// <summary>
        /// 加急（0常规 1加急）
        /// </summary>
        public string IsEmergentpublic { get; set; }

        /// <summary>
        /// 特诊标识（值为82为特诊）
        /// </summary>
        public string HTHpublic { get; set; }

        /// <summary>
        /// 主护士
        /// </summary>
        public string PrimaryNursepublic { get; set; }

        /// <summary>
        /// 费用状态
        /// </summary>
        public string PAYRESULTpublic { get; set; }

        public string RoomNumberpublic { get; set; }

        /// <summary>
        /// 标本类型
        /// </summary>
        public string SpecimenTypepublic { get; set; }

        /// <summary>
        /// 检查项目列表
        /// </summary>
        public List<Exam> ExamList { get; set; } = new List<Exam>();

        /// <summary>
        /// 收费项目列表
        /// </summary>
        public List<Fee> FeeList { get; set; } = new List<Fee>();

        /// <summary>
        /// 性别文字
        /// </summary>
        public string PatientSexText
        {
            get
            {
                switch (PatientSex)
                {
                    case "1":
                    case "M":
                        return "男";
                    case "2":
                    case "F":
                        return "女";
                    default:
                        return "其他";
                }
            }
        }


        /// <summary>
        /// 年龄文字(带年月日)
        /// </summary>
        public string PatientAgeText
        {
            get
            {
                if (string.IsNullOrEmpty(Patientage.Trim()) == false
                    && Patientage.Trim().EndsWith("岁") == false)
                    return Patientage + "岁";
                return Patientage;
            }
        }

        /// <summary>
        /// 病人床号
        /// </summary>
        public string PatientBedNum { get; set; }

        /// <summary>
        /// 病人类型文字
        /// PatientStyle:（0-门诊，1-急诊，2-住院，3-体检）
        /// </summary>
        public string PatientStyleText
        {
            get
            {
                switch (PatientStyle)
                {
                    case "0":
                        return "门诊";
                    case "1":
                        return "急诊";
                    case "2":
                        return "住院";
                    case "3":
                        return "体检";
                    default:
                        return "其他";
                }
            }
        }

        /// <summary>
        /// 病人编号,即门诊号或住院号
        /// </summary>
        public string PatientNo
        {
            get
            {
                //如果读取到接口的值,则直接返回,否则返回门诊号或者住院号
                if (string.IsNullOrEmpty(_patientNo) == false)
                    return _patientNo;
                else if (string.IsNullOrEmpty(InHospitalID) == false)
                    return InHospitalID;
                else if (string.IsNullOrEmpty(OutHospitalID) == false)
                    return OutHospitalID;
                else
                    return "";
                
                
            }
            set { _patientNo = value; }
        }

        private string _detailPatientStyleText;
        /// <summary>
        /// 费别文字
        /// </summary>
        public string DetailPatientStyleText
        {
            get
            {
                //从电子申请单来的数据是0,1
                if (string.IsNullOrEmpty(DetailPatientStyle)==false)
                {
                    switch (DetailPatientStyle)
                    {
                        case "0":
                            return "非医保";
                        case "1":
                            return "医保";
                        default:
                            return "其他";
                    }
                }

                return _detailPatientStyleText;
            }
            set { _detailPatientStyleText = value; }
        }
    }

    /// <summary>
    /// 送检部位
    /// </summary>
    public class Exam
    {
        public string id { get; set; }

        /// <summary>
        /// 系统入口(1 his 2 emr 3 内镜系统)
        /// </summary>
        public string SystemIn { get; set; }

        /// <summary>
        /// 电子申请单号
        /// </summary>
        public string SheetID { get; set; }

        /// <summary>
        /// 电子申请单检查项目流水号
        /// </summary>
        public string ExamID { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 取材登记
        /// </summary>
        public string qucaidengjin { get; set; }
    }

    public class Fee
    {
        public string id { get; set; }

        /// <summary>
        /// 系统入口(1 his 2 emr 3 内镜系统)
        /// </summary>
        public string SystemIn { get; set; }

        /// <summary>
        /// 电子申请单号
        /// </summary>
        public string SheetID { get; set; }

        /// <summary>
        /// 收费ID
        /// </summary>
        public string FEEID { get; set; }

        /// <summary>
        /// 收费名称
        /// </summary>
        public string FEENAME { get; set; }

        /// <summary>
        /// 收费单价
        /// </summary>
        public string FeePrice { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public string FEENum { get; set; }

        /// <summary>
        /// 项目编码
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// ?
        /// </summary>
        public string DrugFlag { get; set; }
    }
}