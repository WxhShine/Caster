using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterCommon;
using CaterModel;

namespace CaterDal
{
    public class HallInfoDal
    {
        public List<HallInfo> GetList()
        {
            string sql = "select * from hallInfo where hIsDelete=0";
            DataTable dt = SQLHelper.GetDataTable(sql);

            List<HallInfo> list=new List<HallInfo>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new HallInfo()
                {
                    HId = Convert.ToInt32(row["hid"]),
                    HTitle = row["htitle"].ToString()
                });
            }

            return list;
        }

        public int Insert(HallInfo hi)
        {
            string sql = "insert into hallinfo(htitle,hisDelete) values(@title,0)";
            SqlParameter p=new SqlParameter("@title",hi.HTitle);

            return SQLHelper.ExecuteNonQuery(sql, p);
        }

        public int Update(HallInfo hi)
        {
            string sql = "update hallinfo set htitle=@title where hid=@id";
            SqlParameter[] ps =
            {
                new SqlParameter("@title",hi.HTitle), 
                new SqlParameter("@id",hi.HId)
            };

            return SQLHelper.ExecuteNonQuery(sql, ps);
        }

        public int Delete(int id)
        {
            string sql = "update hallinfo set hIsDelete=1 where hid=@id";
            SqlParameter p=new SqlParameter("@id",id);
            return SQLHelper.ExecuteNonQuery(sql, p);
        }
    }
}
