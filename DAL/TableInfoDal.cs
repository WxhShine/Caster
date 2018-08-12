using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterCommon;
using CaterModel;

namespace CaterDal
{
    public partial class TableInfoDal
    {
        public List<TableInfo> GetList(Dictionary<string,string> dic)
        {
            string sql = "select ti.*,hi.hTitle from tableinfo as ti " +
                         "inner join hallinfo as hi " +
                         "on ti.tHallId=hi.hid " +
                         "where ti.tisDelete=0 and hi.hIsDelete=0";
            

            List<SqlParameter> listP=new List<SqlParameter>();
            if (dic.Count > 0)
            {
                foreach (var pair in dic)
                {
                    sql += " and " + pair.Key + "=@" + pair.Key;
                    listP.Add(new SqlParameter("@"+pair.Key,pair.Value));
                }
            }

            DataTable dt = SQLHelper.GetDataTable(sql,listP.ToArray());

            List<TableInfo> list=new List<TableInfo>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new TableInfo()
                {
                    TId = Convert.ToInt32(row["tid"]),
                    TTitle = row["ttitle"].ToString(),
                    HallTitle = row["htitle"].ToString(),
                    THallId = Convert.ToInt32(row["thallId"]),
                    TIsFree = Convert.ToBoolean(row["tisFree"])
                });
            }

            return list;
        }

        public int Insert(TableInfo ti)
        {
            string sql = "insert into tableinfo(ttitle,thallid,tisFree,tisDelete) values(@title,@hid,@isfree,0)";
            SqlParameter[] ps =
            {
                new SqlParameter("@title", ti.TTitle),
                new SqlParameter("@hid", ti.THallId),
                new SqlParameter("@isfree", ti.TIsFree)
            };

            return SQLHelper.ExecuteNonQuery(sql, ps);
        }

        public int Update(TableInfo ti)
        {
            string sql = "update tableinfo set ttitle=@title,thallid=@hid,tisfree=@isfree where tid=@id";
            SqlParameter[] ps =
            {
                new SqlParameter("@title", ti.TTitle),
                new SqlParameter("@hid", ti.THallId),
                new SqlParameter("@isfree", ti.TIsFree),
                new SqlParameter("@id", ti.TId)
            };
            return SQLHelper.ExecuteNonQuery(sql, ps);
        }

        public int Delete(int id)
        {
            string sql = "update tableinfo set tisDelete=1 where tid=@id";
            SqlParameter p=new SqlParameter("@id",id);

            return SQLHelper.ExecuteNonQuery(sql, p);
        }

        //public int SetState(int tableId, bool isFree)
        //{
        //    string sql = "update tableinfo set tIsFree=@isfree where tid=@tid";
        //    SqlParameter[] ps =
        //    {
        //        new SqlParameter("@tid", tableId),
        //        new SqlParameter("@isfree", isFree ? 1 : 0),
        //    };
        //    return SQLHelper.ExecuteNonQuery(sql, ps);
        //}
    }
}
