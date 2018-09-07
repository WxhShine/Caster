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
    public partial class MemberInfoDal
    {
        public List<MemberInfo> GetList(Dictionary<string,string> dic)
        {
            //连接查询，得到会员类型的名字
            string sql = "select mi.*,mti.mTitle as MTypeTitle,mti.mDiscount " +
                         "from MemberInfo as mi " +
                         "inner join MemberTypeInfo as mti " +
                         "on mi.mTypeId=mti.Id " +
                         "where mi.IsDelete=0 and mti.IsDelete=0";
                         // +"and mname like '%sadf%'";
            
            List<SqlParameter> listP=new List<SqlParameter>();
            //拼接条件
            if (dic.Count > 0)
            {
                foreach (var pair in dic)
                {
                    //" and mname like @mname"
                    sql += " and mi." + pair.Key + " like @"+pair.Key;
                    //@mname,'%abc%'
                    listP.Add(new SqlParameter("@"+pair.Key,"%"+pair.Value+"%"));
                }
            }

            //执行得到结果集
            DataTable dt = SQLHelper.GetDataTable(sql,listP.ToArray());
            //定义list，完成转存
            List<MemberInfo> list=new List<MemberInfo>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new MemberInfo()
                {
                    Id = Convert.ToInt32(row["Id"]),
                    MName = row["mname"].ToString(),
                    MPhone = row["mphone"].ToString(),
                    MMoney = Convert.ToDecimal(row["mmoney"]),
                    MTypeId = Convert.ToInt32(row["MTypeId"]),
                    MTypeTitle =row["MTypeTitle"].ToString(),
                    MDiscount = Convert.ToDecimal(row["mDiscount"])
                });
            }

            return list;
        }

        public int Insert(MemberInfo mi)
        {
            //构造insert语句
            string sql = "insert into memberinfo(mtypeid,mname,mphone,mmoney,isDelete) values(@tid,@name,@phone,@money,0)";
            //为语句构造参数
            SqlParameter[] ps =
            {
                new SqlParameter("@tid", mi.MTypeId),
                new SqlParameter("@name", mi.MName),
                new SqlParameter("@phone", mi.MPhone),
                new SqlParameter("@money", mi.MMoney)
            };
            //执行并返回结果
            return SQLHelper.ExecuteNonQuery(sql, ps);
        }

        public int Update(MemberInfo mi)
        {
            //构造update语句
            string sql = "update memberinfo set mname=@name,mphone=@phone,mmoney=@money,mtypeid=@tid where Id=@id";
            //为语句提供参数
            SqlParameter[] ps =
            {
                new SqlParameter("@name",mi.MName), 
                new SqlParameter("@phone",mi.MPhone), 
                new SqlParameter("@money",mi.MMoney), 
                new SqlParameter("@tid",mi.MTypeId), 
                new SqlParameter("@id",mi.Id)
            };
            //执行，返回
            return SQLHelper.ExecuteNonQuery(sql, ps);
        }

        public int Delete(int id)
        {
            string sql = "update memberinfo set IsDelete=1 where Id=@id";
            SqlParameter p=new SqlParameter("@id",id);
            return SQLHelper.ExecuteNonQuery(sql, p);
        }
    }
}
