using System.Collections.Generic;
using System.Data;
using System.Text;
using SendPisResult.Models;

namespace SendPisResult.DAL
{
	/// <summary>
	/// 数据访问类:T_BDBG
	/// </summary>
	public class T_BDBG_DAL
	{
		public T_BDBG_DAL()
		{}
		#region  Method

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public T_BDBG GetModel(int F_ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  ");
			strSql.Append(" F_ID,F_BLH,F_BD_BGXH,F_BD_BGYS,F_BD_SHYS,F_BD_BGRQ,F_BD_BGZT,F_BDZD,F_BD_ZPR,F_BD_SDRQ,F_BD_QCYS,F_BD_CFYY,F_BD_ZPS,F_BD_BZ,F_BD_FZYS,F_BD_DYZT,F_BD_QSB_DYZT,F_BD_BGWZ,F_BD_BGWZ_QRSJ,F_BD_BGWZ_QRCZY,F_ZPSJ,F_LTSJ,F_ZPKSSJ,F_BD_FBSJ,F_BD_FBYS,F_PDFSCSJ,F_bd_spare5,F_bd_bbmc,F_bd_rysj ");
			strSql.Append(" from T_BDBG ");
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
		public T_BDBG DataRowToModel(DataRow row)
		{
			T_BDBG model=new T_BDBG();
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
				if(row["F_BD_BGXH"]!=null && row["F_BD_BGXH"].ToString()!="")
				{
					model.F_BD_BGXH=int.Parse(row["F_BD_BGXH"].ToString());
				}
				if(row["F_BD_BGYS"]!=null)
				{
					model.F_BD_BGYS=row["F_BD_BGYS"].ToString();
				}
				if(row["F_BD_SHYS"]!=null)
				{
					model.F_BD_SHYS=row["F_BD_SHYS"].ToString();
				}
				if(row["F_BD_BGRQ"]!=null)
				{
					model.F_BD_BGRQ=row["F_BD_BGRQ"].ToString();
				}
				if(row["F_BD_BGZT"]!=null)
				{
					model.F_BD_BGZT=row["F_BD_BGZT"].ToString();
				}
				if(row["F_BDZD"]!=null)
				{
					model.F_BDZD=row["F_BDZD"].ToString();
				}
				if(row["F_BD_ZPR"]!=null)
				{
					model.F_BD_ZPR=row["F_BD_ZPR"].ToString();
				}
				if(row["F_BD_SDRQ"]!=null)
				{
					model.F_BD_SDRQ=row["F_BD_SDRQ"].ToString();
				}
				if(row["F_BD_QCYS"]!=null)
				{
					model.F_BD_QCYS=row["F_BD_QCYS"].ToString();
				}
				if(row["F_BD_CFYY"]!=null)
				{
					model.F_BD_CFYY=row["F_BD_CFYY"].ToString();
				}
				if(row["F_BD_ZPS"]!=null)
				{
					model.F_BD_ZPS=row["F_BD_ZPS"].ToString();
				}
				if(row["F_BD_BZ"]!=null)
				{
					model.F_BD_BZ=row["F_BD_BZ"].ToString();
				}
				if(row["F_BD_FZYS"]!=null)
				{
					model.F_BD_FZYS=row["F_BD_FZYS"].ToString();
				}
				if(row["F_BD_DYZT"]!=null)
				{
					model.F_BD_DYZT=row["F_BD_DYZT"].ToString();
				}
				if(row["F_BD_QSB_DYZT"]!=null)
				{
					model.F_BD_QSB_DYZT=row["F_BD_QSB_DYZT"].ToString();
				}
				if(row["F_BD_BGWZ"]!=null)
				{
					model.F_BD_BGWZ=row["F_BD_BGWZ"].ToString();
				}
				if(row["F_BD_BGWZ_QRSJ"]!=null)
				{
					model.F_BD_BGWZ_QRSJ=row["F_BD_BGWZ_QRSJ"].ToString();
				}
				if(row["F_BD_BGWZ_QRCZY"]!=null)
				{
					model.F_BD_BGWZ_QRCZY=row["F_BD_BGWZ_QRCZY"].ToString();
				}
				if(row["F_ZPSJ"]!=null)
				{
					model.F_ZPSJ=row["F_ZPSJ"].ToString();
				}
				if(row["F_LTSJ"]!=null)
				{
					model.F_LTSJ=row["F_LTSJ"].ToString();
				}
				if(row["F_ZPKSSJ"]!=null)
				{
					model.F_ZPKSSJ=row["F_ZPKSSJ"].ToString();
				}
				if(row["F_BD_FBSJ"]!=null)
				{
					model.F_BD_FBSJ=row["F_BD_FBSJ"].ToString();
				}
				if(row["F_BD_FBYS"]!=null)
				{
					model.F_BD_FBYS=row["F_BD_FBYS"].ToString();
				}
				if(row["F_PDFSCSJ"]!=null)
				{
					model.F_PDFSCSJ=row["F_PDFSCSJ"].ToString();
				}
				if(row["F_bd_spare5"]!=null)
				{
					model.F_bd_spare5=row["F_bd_spare5"].ToString();
				}
				if(row["F_bd_bbmc"]!=null)
				{
					model.F_bd_bbmc=row["F_bd_bbmc"].ToString();
				}
				if(row["F_bd_rysj"]!=null)
				{
					model.F_bd_rysj=row["F_bd_rysj"].ToString();
				}
			}
			return model;
		}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<T_BDBG> GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select F_ID,F_BLH,F_BD_BGXH,F_BD_BGYS,F_BD_SHYS,F_BD_BGRQ,F_BD_BGZT,F_BDZD,F_BD_ZPR,F_BD_SDRQ,F_BD_QCYS,F_BD_CFYY,F_BD_ZPS,F_BD_BZ,F_BD_FZYS,F_BD_DYZT,F_BD_QSB_DYZT,F_BD_BGWZ,F_BD_BGWZ_QRSJ,F_BD_BGWZ_QRCZY,F_ZPSJ,F_LTSJ,F_ZPKSSJ,F_BD_FBSJ,F_BD_FBYS,F_PDFSCSJ,F_bd_spare5,F_bd_bbmc,F_bd_rysj ");
			strSql.Append(" FROM T_BDBG ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where 1=1 "+strWhere);
			}

            var dt = DbHelper.GetTable(strSql.ToString());
            var list = new List<T_BDBG>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(DataRowToModel(row));
            }

            return list;
        }


        public List<T_BDBG> GetListByBLH(string BLH)
        {
            return GetList($" and F_BLH='{BLH}' and f_bd_bgzt='已审核' ");
        }

        #endregion  Method
        #region  MethodEx

        #endregion  MethodEx
    }
}

