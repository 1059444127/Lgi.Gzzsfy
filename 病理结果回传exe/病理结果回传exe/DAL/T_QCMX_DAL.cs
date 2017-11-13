using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SendPisResult.Models;

namespace SendPisResult.DAL
{
	/// <summary>
	/// 数据访问类:T_QCMX
	/// </summary>
	public partial class T_QCMX_DAL
	{
		public T_QCMX_DAL()
		{}
		#region  Method
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public T_QCMX GetModel(int F_ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  ");
			strSql.Append(" F_ID,F_PXID,F_BLH,F_RWLY,F_QCXH,F_ZZMC,F_CKS,F_QCYS,F_QCRQ,F_BMZT,F_SM,F_BZ,F_SJZT,F_QCSJ,F_JLY,F_HDZT,F_HDR,F_HDSJ ");
			strSql.Append(" from T_QCMX ");
			strSql.Append(" where F_ID="+F_ID+" " );

            var dt = DbHelper.GetTable(strSql.ToString());

            if (dt.Rows.Count > 0)
            {
                return DataRowToModel(dt.Rows[0]);
            }
            else
            {
                return null;
            }
        }

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public T_QCMX DataRowToModel(DataRow row)
		{
			T_QCMX model=new T_QCMX();
			if (row != null)
			{
				if(row["F_ID"]!=null && row["F_ID"].ToString()!="")
				{
					model.F_ID=int.Parse(row["F_ID"].ToString());
				}
				if(row["F_PXID"]!=null && row["F_PXID"].ToString()!="")
				{
					model.F_PXID=int.Parse(row["F_PXID"].ToString());
				}
				if(row["F_BLH"]!=null)
				{
					model.F_BLH=row["F_BLH"].ToString();
				}
				if(row["F_RWLY"]!=null)
				{
					model.F_RWLY=row["F_RWLY"].ToString();
				}
				if(row["F_QCXH"]!=null)
				{
					model.F_QCXH=row["F_QCXH"].ToString();
				}
				if(row["F_ZZMC"]!=null)
				{
					model.F_ZZMC=row["F_ZZMC"].ToString();
				}
				if(row["F_CKS"]!=null)
				{
					model.F_CKS=row["F_CKS"].ToString();
				}
				if(row["F_QCYS"]!=null)
				{
					model.F_QCYS=row["F_QCYS"].ToString();
				}
				if(row["F_QCRQ"]!=null)
				{
					model.F_QCRQ=row["F_QCRQ"].ToString();
				}
				if(row["F_BMZT"]!=null)
				{
					model.F_BMZT=row["F_BMZT"].ToString();
				}
				if(row["F_SM"]!=null)
				{
					model.F_SM=row["F_SM"].ToString();
				}
				if(row["F_BZ"]!=null)
				{
					model.F_BZ=row["F_BZ"].ToString();
				}
				if(row["F_SJZT"]!=null)
				{
					model.F_SJZT=row["F_SJZT"].ToString();
				}
				if(row["F_QCSJ"]!=null)
				{
					model.F_QCSJ=row["F_QCSJ"].ToString();
				}
				if(row["F_JLY"]!=null)
				{
					model.F_JLY=row["F_JLY"].ToString();
				}
				if(row["F_HDZT"]!=null)
				{
					model.F_HDZT=row["F_HDZT"].ToString();
				}
				if(row["F_HDR"]!=null)
				{
					model.F_HDR=row["F_HDR"].ToString();
				}
				if(row["F_HDSJ"]!=null)
				{
					model.F_HDSJ=row["F_HDSJ"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<T_QCMX> GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select F_ID,F_PXID,F_BLH,F_RWLY,F_QCXH,F_ZZMC,F_CKS,F_QCYS,F_QCRQ,F_BMZT,F_SM,F_BZ,F_SJZT,F_QCSJ,F_JLY,F_HDZT,F_HDR,F_HDSJ ");
			strSql.Append(" FROM T_QCMX ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where 1=1 "+strWhere);
			}

            var dt = DbHelper.GetTable(strSql.ToString());
            var list = new List<T_QCMX>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(DataRowToModel(row));
            }

            return list;
        }


        public List<T_QCMX> GetListByBLH(string BLH)
        {
            return GetList($" and F_BLH='{BLH}' ");
        }



        #endregion  Method
        #region  MethodEx

        #endregion  MethodEx
    }
}

