using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterCommon;
using CaterModel;

namespace CaterDal {
    /// <summary>
    /// 厅包数据层
    /// </summary>
    public class HallInfoDal {
        //
        public List<HallInfo> GetList() {
            string sql = "select * from hallInfo where IsDelete=0";
            var list = SQLHelper.ExecuteScalarList<HallInfo>(sql);
            return list;
        }

        public int Insert(HallInfo hi) {
            string sql = "insert into hallinfo(htitle,isDelete) values(@title,0)";
            SqlParameter p = new SqlParameter("@title", hi.HTitle);

            return SQLHelper.ExecuteNonQuery(sql, p);
        }

        public int Update(HallInfo hi) {
            string sql = "update hallinfo set htitle=@title where Id=@id";
            SqlParameter[] ps =
            {
                new SqlParameter("@title",hi.HTitle),
                new SqlParameter("@id",hi.Id)
            };

            return SQLHelper.ExecuteNonQuery(sql, ps);
        }

        public int Delete(int id) {
            string sql = "update hallinfo set IsDelete=1 where Id=@id";
            SqlParameter p = new SqlParameter("@id", id);
            return SQLHelper.ExecuteNonQuery(sql, p);
        }
    }
}
