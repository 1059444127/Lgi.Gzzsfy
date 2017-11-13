using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SendPisResult.Models;

namespace SendPisResult.DAL
{
	/// <summary>
	/// 数据访问类:T_QP
	/// </summary>
	public partial class T_QP_DAL
	{
		public T_QP_DAL()
		{}
		#region  Method
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public T_QP GetModel(int F_ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  ");
			strSql.Append(" F_ID,F_BLH,F_RWLY,F_LKH,F_YZH,F_QPTMH,F_QPXH,F_QPBH,F_QPSM,F_CZY,F_QPSJ,F_DYZT,F_JGPJ,F_PJR,F_QPZT,F_QPTMH2,F_GDZT,F_GDCZY,F_GDSJ,F_BZ,F_TJH,F_ZXSB,F_RSY,F_RSSJ,F_YZSQYS,F_PJSJ,F_YYFX,F_CLJG,F_CLR,F_CLRQ,F_QPJJDDYZT,F_QPJJDDYCZY,F_QPJJDDYSJ,F_QPJJZT,F_QPJJCZY,F_QPJJSJ,F_YZKDD,F_JGPJ_FJ,F_JGPJ_FS ");
			strSql.Append(" from T_QP ");
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
		public T_QP DataRowToModel(DataRow row)
		{
			T_QP model=new T_QP();
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
				if(row["F_RWLY"]!=null)
				{
					model.F_RWLY=row["F_RWLY"].ToString();
				}
				if(row["F_LKH"]!=null)
				{
					model.F_LKH=row["F_LKH"].ToString();
				}
				if(row["F_YZH"]!=null)
				{
					model.F_YZH=row["F_YZH"].ToString();
				}
				if(row["F_QPTMH"]!=null)
				{
					model.F_QPTMH=row["F_QPTMH"].ToString();
				}
				if(row["F_QPXH"]!=null && row["F_QPXH"].ToString()!="")
				{
					model.F_QPXH=int.Parse(row["F_QPXH"].ToString());
				}
				if(row["F_QPBH"]!=null)
				{
					model.F_QPBH=row["F_QPBH"].ToString();
				}
				if(row["F_QPSM"]!=null)
				{
					model.F_QPSM=row["F_QPSM"].ToString();
				}
				if(row["F_CZY"]!=null)
				{
					model.F_CZY=row["F_CZY"].ToString();
				}
				if(row["F_QPSJ"]!=null)
				{
					model.F_QPSJ=row["F_QPSJ"].ToString();
				}
				if(row["F_DYZT"]!=null)
				{
					model.F_DYZT=row["F_DYZT"].ToString();
				}
				if(row["F_JGPJ"]!=null)
				{
					model.F_JGPJ=row["F_JGPJ"].ToString();
				}
				if(row["F_PJR"]!=null)
				{
					model.F_PJR=row["F_PJR"].ToString();
				}
				if(row["F_QPZT"]!=null)
				{
					model.F_QPZT=row["F_QPZT"].ToString();
				}
				if(row["F_QPTMH2"]!=null)
				{
					model.F_QPTMH2=row["F_QPTMH2"].ToString();
				}
				if(row["F_GDZT"]!=null)
				{
					model.F_GDZT=row["F_GDZT"].ToString();
				}
				if(row["F_GDCZY"]!=null)
				{
					model.F_GDCZY=row["F_GDCZY"].ToString();
				}
				if(row["F_GDSJ"]!=null)
				{
					model.F_GDSJ=row["F_GDSJ"].ToString();
				}
				if(row["F_BZ"]!=null)
				{
					model.F_BZ=row["F_BZ"].ToString();
				}
				if(row["F_TJH"]!=null)
				{
					model.F_TJH=row["F_TJH"].ToString();
				}
				if(row["F_ZXSB"]!=null)
				{
					model.F_ZXSB=row["F_ZXSB"].ToString();
				}
				if(row["F_RSY"]!=null)
				{
					model.F_RSY=row["F_RSY"].ToString();
				}
				if(row["F_RSSJ"]!=null)
				{
					model.F_RSSJ=row["F_RSSJ"].ToString();
				}
				if(row["F_YZSQYS"]!=null)
				{
					model.F_YZSQYS=row["F_YZSQYS"].ToString();
				}
				if(row["F_PJSJ"]!=null)
				{
					model.F_PJSJ=row["F_PJSJ"].ToString();
				}
				if(row["F_YYFX"]!=null)
				{
					model.F_YYFX=row["F_YYFX"].ToString();
				}
				if(row["F_CLJG"]!=null)
				{
					model.F_CLJG=row["F_CLJG"].ToString();
				}
				if(row["F_CLR"]!=null)
				{
					model.F_CLR=row["F_CLR"].ToString();
				}
				if(row["F_CLRQ"]!=null)
				{
					model.F_CLRQ=row["F_CLRQ"].ToString();
				}
				if(row["F_QPJJDDYZT"]!=null)
				{
					model.F_QPJJDDYZT=row["F_QPJJDDYZT"].ToString();
				}
				if(row["F_QPJJDDYCZY"]!=null)
				{
					model.F_QPJJDDYCZY=row["F_QPJJDDYCZY"].ToString();
				}
				if(row["F_QPJJDDYSJ"]!=null)
				{
					model.F_QPJJDDYSJ=row["F_QPJJDDYSJ"].ToString();
				}
				if(row["F_QPJJZT"]!=null)
				{
					model.F_QPJJZT=row["F_QPJJZT"].ToString();
				}
				if(row["F_QPJJCZY"]!=null)
				{
					model.F_QPJJCZY=row["F_QPJJCZY"].ToString();
				}
				if(row["F_QPJJSJ"]!=null)
				{
					model.F_QPJJSJ=row["F_QPJJSJ"].ToString();
				}
				if(row["F_YZKDD"]!=null)
				{
					model.F_YZKDD=row["F_YZKDD"].ToString();
				}
				if(row["F_JGPJ_FJ"]!=null)
				{
					model.F_JGPJ_FJ=row["F_JGPJ_FJ"].ToString();
				}
					//model.F_JGPJ_FS=row["F_JGPJ_FS"].ToString();
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<T_QP> GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select F_ID,F_BLH,F_RWLY,F_LKH,F_YZH,F_QPTMH,F_QPXH,F_QPBH,F_QPSM,F_CZY,F_QPSJ,F_DYZT,F_JGPJ,F_PJR,F_QPZT,F_QPTMH2,F_GDZT,F_GDCZY,F_GDSJ,F_BZ,F_TJH,F_ZXSB,F_RSY,F_RSSJ,F_YZSQYS,F_PJSJ,F_YYFX,F_CLJG,F_CLR,F_CLRQ,F_QPJJDDYZT,F_QPJJDDYCZY,F_QPJJDDYSJ,F_QPJJZT,F_QPJJCZY,F_QPJJSJ,F_YZKDD,F_JGPJ_FJ,F_JGPJ_FS ");
			strSql.Append(" FROM T_QP ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where 1=1 "+strWhere);
			}


            var dt = DbHelper.GetTable(strSql.ToString());
            var list = new List<T_QP>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(DataRowToModel(row));
            }

            return list;
        }


	    public List<T_QP> GetListByBLH(string BLH)
	    {
	        return GetList($" and F_BLH='{BLH}' ");
	    }

        #endregion  Method
        #region  MethodEx

        #endregion  MethodEx
    }
}

