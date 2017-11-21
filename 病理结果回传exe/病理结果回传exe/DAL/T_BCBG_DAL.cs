/**  版本信息模板在安装目录下，可自行修改。
* T_BCBG.cs
*
* 功 能： N/A
* 类 名： T_BCBG
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017-08-03 15:23:19   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.OleDb;
using SendPisResult.DAL;
using SendPisResult.Models;

namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:T_BCBG
	/// </summary>
	public partial class T_BCBG_DAL
	{
		public T_BCBG_DAL()
		{}
        
        /// <summary>
		/// 得到一个对象实体
		/// </summary>
		public T_BCBG GetModel(int F_ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  ");
			strSql.Append(" F_ID,F_BLH,F_BC_BGXH,F_BC_BGYS,F_BC_SHYS,F_BC_BGRQ,F_BCZD,F_BC_BGZT,F_BC_FZYS,F_BC_DYZT,F_BC_QSB_DYZT,F_BC_BGWZ,F_BC_BGWZ_QRSJ,F_BC_BGWZ_QRCZY,F_BC_BZ,F_BC_SPARE5,F_BC_JXSJ,F_BC_TSJC,F_BC_BGGS,F_BC_FBSJ,F_BC_FBYS,F_PDFSCSJ ");
			strSql.Append(" from T_BCBG ");
			strSql.Append(" where F_ID="+F_ID+" " );
			T_BCBG model=new T_BCBG();

            var dt = DbHelper.GetTable(strSql.ToString());

			if(dt.Rows.Count>0)
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
		public T_BCBG DataRowToModel(DataRow row)
		{
			T_BCBG model=new T_BCBG();
			if (row != null)
			{
				if(row["F_ID"]!=null && row["F_ID"].ToString()!="")
				{
					model.F_ID=int.Parse(row["F_ID"].ToString());
				}
				if(row["F_BLH"]!=null)
				{
					model.F_BLH=row["F_BLH"].ToString();
				}
				if(row["F_BC_BGXH"]!=null && row["F_BC_BGXH"].ToString()!="")
				{
					model.F_BC_BGXH=int.Parse(row["F_BC_BGXH"].ToString());
				}
				if(row["F_BC_BGYS"]!=null)
				{
					model.F_BC_BGYS=row["F_BC_BGYS"].ToString();
				}
				if(row["F_BC_SHYS"]!=null)
				{
					model.F_BC_SHYS=row["F_BC_SHYS"].ToString();
				}
				if(row["F_BC_BGRQ"]!=null)
				{
					model.F_BC_BGRQ=row["F_BC_BGRQ"].ToString();
				}
				if(row["F_BCZD"]!=null)
				{
					model.F_BCZD=row["F_BCZD"].ToString();
				}
				if(row["F_BC_BGZT"]!=null)
				{
					model.F_BC_BGZT=row["F_BC_BGZT"].ToString();
				}
				if(row["F_BC_FZYS"]!=null)
				{
					model.F_BC_FZYS=row["F_BC_FZYS"].ToString();
				}
				if(row["F_BC_DYZT"]!=null)
				{
					model.F_BC_DYZT=row["F_BC_DYZT"].ToString();
				}
				if(row["F_BC_QSB_DYZT"]!=null)
				{
					model.F_BC_QSB_DYZT=row["F_BC_QSB_DYZT"].ToString();
				}
				if(row["F_BC_BGWZ"]!=null)
				{
					model.F_BC_BGWZ=row["F_BC_BGWZ"].ToString();
				}
				if(row["F_BC_BGWZ_QRSJ"]!=null)
				{
					model.F_BC_BGWZ_QRSJ=row["F_BC_BGWZ_QRSJ"].ToString();
				}
				if(row["F_BC_BGWZ_QRCZY"]!=null)
				{
					model.F_BC_BGWZ_QRCZY=row["F_BC_BGWZ_QRCZY"].ToString();
				}
				if(row["F_BC_BZ"]!=null)
				{
					model.F_BC_BZ=row["F_BC_BZ"].ToString();
				}
				if(row["F_BC_SPARE5"]!=null)
				{
					model.F_BC_SPARE5=row["F_BC_SPARE5"].ToString();
				}
				if(row["F_BC_JXSJ"]!=null)
				{
					model.F_BC_JXSJ=row["F_BC_JXSJ"].ToString();
				}
				if(row["F_BC_TSJC"]!=null)
				{
					model.F_BC_TSJC=row["F_BC_TSJC"].ToString();
				}
				if(row["F_BC_BGGS"]!=null)
				{
					model.F_BC_BGGS=row["F_BC_BGGS"].ToString();
				}
				if(row["F_BC_FBSJ"]!=null)
				{
					model.F_BC_FBSJ=row["F_BC_FBSJ"].ToString();
				}
				if(row["F_BC_FBYS"]!=null)
				{
					model.F_BC_FBYS=row["F_BC_FBYS"].ToString();
				}
				if(row["F_PDFSCSJ"]!=null)
				{
					model.F_PDFSCSJ=row["F_PDFSCSJ"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<T_BCBG> GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select F_ID,F_BLH,F_BC_BGXH,F_BC_BGYS,F_BC_SHYS,F_BC_BGRQ,F_BCZD,F_BC_BGZT,F_BC_FZYS,F_BC_DYZT,F_BC_QSB_DYZT,F_BC_BGWZ,F_BC_BGWZ_QRSJ,F_BC_BGWZ_QRCZY,F_BC_BZ,F_BC_SPARE5,F_BC_JXSJ,F_BC_TSJC,F_BC_BGGS,F_BC_FBSJ,F_BC_FBYS,F_PDFSCSJ ");
			strSql.Append(" FROM T_BCBG ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where 1=1 "+strWhere);
			}

            var dt = DbHelper.GetTable(strSql.ToString());
            var list = new List<T_BCBG>();

		    if (dt != null)
		        foreach (DataRow row in dt.Rows)
		        {
		            list.Add(DataRowToModel(row));
		        }

		    return list;
		}


        public List<T_BCBG> GetListByBLH(string BLH)
        {
            return GetList($" and F_BLH='{BLH}' ");
        }

	    public T_BCBG GetByBlhAndBgxh(string pathoNo, string bgxh)
	    {
	        var lst = GetList($" and F_BLH='{pathoNo}' and F_BC_BGXH='{bgxh}' ");
	        if (lst.Count > 0)
	            return lst[0];
	        return null;
	    }
	}
}

