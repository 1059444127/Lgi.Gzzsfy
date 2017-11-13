using System;

namespace SendPisResult.Models
{
    /// <summary>
    /// T_TX:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class T_TX
    {
        public T_TX()
        { }
        #region Model
        private int _f_id;
        private string _f_blh;
        private string _f_txm;
        private string _f_txsm;
        private int? _f_sfdy;
        private string _f_txlb;
        /// <summary>
        /// 
        /// </summary>
        public int F_ID
        {
            set { _f_id = value; }
            get { return _f_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string F_BLH
        {
            set { _f_blh = value; }
            get { return _f_blh; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string F_TXM
        {
            set { _f_txm = value; }
            get { return _f_txm; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string F_TXSM
        {
            set { _f_txsm = value; }
            get { return _f_txsm; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? F_SFDY
        {
            set { _f_sfdy = value; }
            get { return _f_sfdy; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string F_TXLB
        {
            set { _f_txlb = value; }
            get { return _f_txlb; }
        }
        #endregion Model

    }
}

