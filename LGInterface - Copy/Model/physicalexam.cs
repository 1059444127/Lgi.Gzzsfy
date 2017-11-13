using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LGInterface.Model
{
    [Serializable]
    public class physicalexam
    {
        /// <summary>
        /// 体检号 
        /// </summary>
        public string CLINIC_CODE { get; set; }

        /// <summary>
        /// 体检流水号 
        /// </summary>
        public string CLINIC_SEQUENCE_CODE { get; set; }

        /// <summary>
        /// 门诊号 
        /// </summary>
        public string CARD_NO { get; set; }

        /// <summary>
        /// 门诊流水号
        /// </summary>
        public string RELEVANCE_ID { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// 性别 
        /// </summary>
        public string SEX_CODE { get; set; }
        /// <summary>
        /// 出生年月日 
        /// </summary>
        public string BIRTHDAY { get; set; }
        /// <summary>
        /// 家庭电话号码 
        /// </summary>
        public string HOME_TEL { get; set; }
        /// <summary>
        /// 家庭地址 
        /// </summary>
        public string HOME { get; set; }
        /// <summary>
        /// 开单科室 
        /// </summary>
        public string OPER_DEPTCODE { get; set; }
        /// <summary>
        /// 开单科室名 
        /// </summary>
        public string KDKS { get; set; }
        /// <summary>
        /// 审核人 
        /// </summary>
        public string OPER_CODE { get; set; }
        /// <summary>
        /// 体检日期 
        /// </summary>
        public string CHK_DATE { get; set; }
        /// <summary>
        /// 身份证号 
        /// </summary>
        public string IDENNO { get; set; }

        public RequestInfoResult GetRequestInfoResult()
        {

            RequestInfoResult res = new RequestInfoResult();
            //res.OutHospitalID = exam.CLINIC_CODE;
            res.JZLSH = this.RELEVANCE_ID;
            res.SheetID = this.CLINIC_SEQUENCE_CODE;
            res.OutHospitalID = this.CARD_NO;
            res.PatientName = this.NAME;
            switch (this.SEX_CODE)
            {
                case "男":
                    res.PatientSex = "M";break;
                case "女":
                    res.PatientSex = "F";break;
                default:
                    res.PatientSex = "其它";
                    break;
            }
            res.PatientStyle = "3";

            if (string.IsNullOrEmpty(this.BIRTHDAY)==false)
            {
                var year = int.Parse(BIRTHDAY.Substring(0, 4));
                res.Patientage = (DateTime.Now.Year-year).ToString();
            }

            res.PatientAddress = this.HOME;
            res.PatientTel = this.HOME_TEL;
            res.DepartMent = this.KDKS;

            return res;
        }
    }
}