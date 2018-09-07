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
    public partial class DishInfoDal
    {
        public List<DishInfo> GetList(Dictionary<string,string> dic)
        {
            string sql = @"select di.*,dti.dtitle as dTypeTitle 
                from dishinfo as di
                inner join dishtypeinfo as dti
                on di.dtypeid=dti.Id
                where di.IsDelete=0 and dti.IsDelete=0";

            List<SqlParameter> listP=new List<SqlParameter>();
            //接收筛选条件
            if (dic.Count > 0)
            {
                //sql += " and di.属性 like '%值%'";
                foreach (var pair in dic)
                {
                    //sql += " and di.dtitle like @dtitle";

                    sql += " and di." + pair.Key + " like @"+pair.Key;
                    listP.Add(new SqlParameter("@" + pair.Key, "%" + pair.Value + "%"));//@dtitle,%abc%
                }
            }

            DataTable dt = SQLHelper.GetDataTable(sql,listP.ToArray());

            List<DishInfo> list=new List<DishInfo>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new DishInfo()
                {
                    Id = Convert.ToInt32(row["Id"]),
                    DTitle = row["dtitle"].ToString(),
                    DTypeTitle = row["dtypeTitle"].ToString(),
                    DChar = row["dchar"].ToString(),
                    DPrice = Convert.ToDecimal(row["dprice"])
                });
            }

            return list;
        }

        public int Insert(DishInfo di)
        {
            string sql = "insert into dishinfo(dtitle,dtypeid,dprice,dchar,IsDelete) values(@title,@tid,@price,@dchar,0)";
            SqlParameter[] p =
            {
                new SqlParameter("@title",di.DTitle), 
                new SqlParameter("@tid",di.DTypeId), 
                new SqlParameter("@price",di.DPrice), 
                new SqlParameter("@dchar",di.DChar)
            };

            return SQLHelper.ExecuteNonQuery(sql, p);
        }

        public int Update(DishInfo di)
        {
            string sql = "update dishinfo set dtitle=@title,dtypeid=@tid,dprice=@price,dchar=@dchar where Id=@id";
            SqlParameter[] ps =
            {
                new SqlParameter("@title",di.DTitle), 
                new SqlParameter("@tid",di.DTypeId), 
                new SqlParameter("@price",di.DPrice), 
                new SqlParameter("@dchar",di.DChar), 
                new SqlParameter("@id",di.Id)
            };

            return SQLHelper.ExecuteNonQuery(sql, ps);
        }

        public int Delete(int id)
        {
            string sql = "update dishinfo set IsDelete=1 where Id=@id";
            SqlParameter p = new SqlParameter("@id", id);

            return SQLHelper.ExecuteNonQuery(sql, p);
        }
    }
}
