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
    public partial class MemberTypeInfoDal
    {
        //查询未删除数据
        public List<MemberTypeInfo> GetList()
        {
            //查询未删除的数据
            string sql = "select * from memberTypeInfo where IsDelete=0";
            //执行查询返回表格
            DataTable dt = SQLHelper.GetDataTable(sql);
            //定义集合对象
            List<MemberTypeInfo> list=new List<MemberTypeInfo>();

            //遍历表格，将数据转存到集合中
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new MemberTypeInfo()
                {
                    Id = Convert.ToInt32(row["Id"]),
                    MTitle = row["mtitle"].ToString(),
                    MDiscount = Convert.ToDecimal(row["mdiscount"])
                });
            }

            //返回集合
            return list;
        } 

        //添加
        public int Insert(MemberTypeInfo mti)
        {
            //构造insert语句
            string sql = "insert into MemberTypeInfo(mtitle,mdiscount,isDelete) values(@title,@discount,0)";
            //为sql语句构造参数
            SqlParameter[] ps =
            {
                new SqlParameter("@title",mti.MTitle), 
                new SqlParameter("@discount",mti.MDiscount)
            };
            //执行
            return SQLHelper.ExecuteNonQuery(sql, ps);
        }

        //修改
        public int Update(MemberTypeInfo mti)
        {
            //构造update语句
            string sql = "update memberTypeInfo set mtitle=@title,mdiscount=@discount where Id=@id";
            //为语句构造参数
            SqlParameter[] ps =
            {
                new SqlParameter("@title",mti.MTitle), 
                new SqlParameter("@discount",mti.MDiscount), 
                new SqlParameter("@id",mti.Id)
            };
            //执行
            return SQLHelper.ExecuteNonQuery(sql, ps);
        }

        //删除
        public int Delete(int id)
        {
            //进行逻辑删除的sql语句
            string sql = "update memberTypeInfo set IsDelete=1 where Id=@id";
            //参数
            SqlParameter p=new SqlParameter("@id",id);
            //执行并返回受影响行数
            return SQLHelper.ExecuteNonQuery(sql, p);
        }
    }
}
