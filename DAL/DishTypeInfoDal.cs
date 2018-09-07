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
    public partial class DishTypeInfoDal
    {
        public List<DishTypeInfo> GetList()
        {
            string sql = "select * from DishTypeInfo where IsDelete=0";
            DataTable dt = SQLHelper.GetDataTable(sql);
            //转存集合
            List<DishTypeInfo> list=new List<DishTypeInfo>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new DishTypeInfo()
                {
                    Id = Convert.ToInt32(row["Id"]),
                    DTitle = row["dtitle"].ToString()
                });
            }

            return list;
        }

        public int Insert(DishTypeInfo dti)
        {
            string sql = "insert into dishtypeinfo(dtitle,IsDelete) values(@title,0)";
            SqlParameter p=new SqlParameter("@title",dti.DTitle);
            return SQLHelper.ExecuteNonQuery(sql, p);
        }

        public int Update(DishTypeInfo dti)
        {
            string sql = "update dishtypeinfo set dtitle=@title where Id=@id";
            SqlParameter[] p=
            {
                new SqlParameter("@title",dti.DTitle), 
                new SqlParameter("@id",dti.Id)
            };

            return SQLHelper.ExecuteNonQuery(sql, p);
        }

        public int Delete(int id)
        {
            string sql = "update dishtypeinfo set IsDelete=1 where Id=@id";
            SqlParameter p=new SqlParameter("@id",id);

            return SQLHelper.ExecuteNonQuery(sql, p);
        }
    }


}
