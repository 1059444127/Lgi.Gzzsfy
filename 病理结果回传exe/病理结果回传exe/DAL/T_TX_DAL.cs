using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using dbbase;
using SendPisResult.Models;

namespace Maticsoft.DAL
{
    /// <summary>
    /// 数据访问类:T_TX
    /// </summary>
    public partial class T_TX_DAL
    {
        public T_TX_DAL()
        { }
        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public T_TX GetModel(int F_ID)
        {
            dbbase.odbcdb aa = new odbcdb("DSN=pathnet;UID=pathnet;PWD=4s3c2a1p", "", "");


            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(" F_ID,F_BLH,F_TXM,F_TXSM,F_SFDY,F_TXLB ");
            strSql.Append(" from T_TX ");
            strSql.Append(" where F_ID=" + F_ID + "");
            T_TX model = new T_TX();
            DataSet ds = aa.GetDataSet(strSql.ToString(),"dt1");
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public T_TX DataRowToModel(DataRow row)
        {
            T_TX model = new T_TX();
            if (row != null)
            {
                if (row["F_ID"] != null && row["F_ID"].ToString() != "")
                {
                    model.F_ID = int.Parse(row["F_ID"].ToString());
                }
                if (row["F_BLH"] != null)
                {
                    model.F_BLH = row["F_BLH"].ToString();
                }
                if (row["F_TXM"] != null)
                {
                    model.F_TXM = row["F_TXM"].ToString();
                }
                if (row["F_TXSM"] != null)
                {
                    model.F_TXSM = row["F_TXSM"].ToString();
                }
                if (row["F_SFDY"] != null && row["F_SFDY"].ToString() != "")
                {
                    model.F_SFDY = int.Parse(row["F_SFDY"].ToString());
                }
                if (row["F_TXLB"] != null)
                {
                    model.F_TXLB = row["F_TXLB"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<T_TX> GetList(string strWhere)
        {
            dbbase.odbcdb aa = new odbcdb("DSN=pathnet;UID=pathnet;PWD=4s3c2a1p", "", "");

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select F_ID,F_BLH,F_TXM,F_TXSM,F_SFDY,F_TXLB ");
            strSql.Append(" FROM T_TX t ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            DataSet ds = aa.GetDataSet(strSql.ToString(), "dt1");

            List<T_TX> txList = new List<T_TX>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                txList.Add(DataRowToModel(row));
            }

            return txList;
        }

        #endregion  Method
    }
}

