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
    public partial class OrderInfoDal
    {
        public int KaiOrder(int tableId)
        {
            //插入订单数据
            //更新餐桌状态
            //写在一起执行，只需要和数据库交互一次
            //下订单
            string sql = "insert into orderinfo(odate,ispay,tableId) values(datetime('now', 'localtime'),0,@tid);" +
                //更新餐桌状态
                "update tableinfo set tIsFree=0 where tid=@tid;" +
                //获取最新的订单编号
                "select oid from orderinfo order by oid desc limit 0,1";
            SqlParameter p=new SqlParameter("@tid",tableId);
            return Convert.ToInt32(SQLHelper.ExecuteScalar(sql, p));
        }

        public int GetOrderIdByTableId(int tableId)
        {
            string sql = "select oid from orderinfo where tableId=@tableid and ispay=0";
            SqlParameter p=new SqlParameter("@tableId",tableId);
            return Convert.ToInt32(SQLHelper.ExecuteScalar(sql, p));
        }

        public int DianCai(int orderid, int dishId)
        {
            //查询当前订单是否已经点了这道菜
            string sql = "select count(*) from orderDetailInfo where orderId=@oid and dishId=@did";
            SqlParameter[] ps =
            {
                new SqlParameter("@oid", orderid),
                new SqlParameter("@did", dishId)
            };
            int count = Convert.ToInt32(SQLHelper.ExecuteScalar(sql, ps));
            if (count > 0)
            {
                //这个订单已经点过这个菜，让数量加1
                sql = "update orderDetailInfo set count=count+1 where orderId=@oid and dishId=@did";
            }
            else
            {
                //当前订单还没有点这个菜，加入这个菜
                sql = "insert into orderDetailInfo(orderid,dishId,count) values(@oid,@did,1)";
            }
            return SQLHelper.ExecuteNonQuery(sql, ps);
        }

        public int UpdateCountByOId(int oid,int count)
        {
            string sql = "update orderDetailInfo set count=@count where oid=@oid";
            SqlParameter[] ps =
            {
                new SqlParameter("@count", count),
                new SqlParameter("@oid", oid)
            };
            return SQLHelper.ExecuteNonQuery(sql, ps);
        }

        public List<OrderDetailInfo> GetDetailList(int orderId)
        {
            string sql=@"select odi.oid,di.dTitle,di.dPrice,odi.count from dishinfo as di
            inner join orderDetailInfo as odi
            on di.did=odi.dishid
            where odi.orderId=@orderid";
            SqlParameter p=new SqlParameter("@orderid",orderId);

            DataTable dt = SQLHelper.GetDataTable(sql, p);
            List<OrderDetailInfo> list=new List<OrderDetailInfo>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new OrderDetailInfo()
                {
                    OId = Convert.ToInt32(row["oid"]),
                    DTitle = row["dtitle"].ToString(),
                    DPrice = Convert.ToDecimal(row["dprice"]),
                    Count = Convert.ToInt32(row["count"])
                });
            }

            return list;
        }

        public decimal GetTotalMoneyByOrderId(int orderid)
        {
            string sql = @"	select sum(oti.count*di.dprice) 
	            from orderdetailinfo as oti
	            inner join dishinfo as di
	            on oti.dishid=di.did
	            where oti.orderid=@orderid";
           SqlParameter p=new SqlParameter("@orderid",orderid);

            object obj = SQLHelper.ExecuteScalar(sql, p);
            if (obj == DBNull.Value)
            {
                return 0;
            }
            return Convert.ToDecimal(obj);
        }

        public int SetOrderMomey(int orderid,decimal money)
        {
            string sql = "update orderinfo set omoney=@money where oid=@oid";
            SqlParameter[] ps =
            {
                new SqlParameter("@money", money),
                new SqlParameter("@oid", orderid)
            };
            return SQLHelper.ExecuteNonQuery(sql, ps);
        }

        public int DeleteDetailById(int oid)
        {
            string sql = "delete from orderDetailInfo where oid=@oid";
            SqlParameter p=new SqlParameter("@oid",oid);
            return SQLHelper.ExecuteNonQuery(sql, p);
        }

        public int Pay(bool isUseMoney,int memberId,decimal payMoney,int orderid,decimal discount)
        {
            //创建数据库的链接对象
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["itcastCater"].ConnectionString))
            {
                int result = 0;
                //由数据库链接对象创建事务
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                //创建command对象
                SqlCommand cmd=new SqlCommand();
                //将命令对象启用事务
                cmd.Transaction = tran;
                //执行各命令
                string sql = "";
                SqlParameter[] ps;
                try
                {
                    //1、根据是否使用余额决定扣款方式
                    if (isUseMoney)
                    {
                        //使用余额
                        sql = "update MemberInfo set mMoney=mMoney-@payMoney where mid=@mid";
                        ps = new SqlParameter[]
                        {
                            new SqlParameter("@payMoney", payMoney),
                            new SqlParameter("@mid", memberId)
                        };
                        cmd.CommandText = sql;
                        cmd.Parameters.AddRange(ps);
                        result+=cmd.ExecuteNonQuery();
                    }

                    //2、将订单状态为IsPage=1
                    sql = "update orderInfo set isPay=1,memberId=@mid,discount=@discount where oid=@oid";
                    ps = new SqlParameter[]
                    {
                        new SqlParameter("@mid", memberId),
                        new SqlParameter("@discount", discount),
                        new SqlParameter("@oid", orderid)
                    };
                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddRange(ps);
                    result += cmd.ExecuteNonQuery();

                    //3、将餐桌状态IsFree=1
                    sql = "update tableInfo set tIsFree=1 where tid=(select tableId from orderinfo where oid=@oid)";
                    SqlParameter p = new SqlParameter("@oid", orderid);
                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(p);
                    result += cmd.ExecuteNonQuery();
                    //提交事务
                    tran.Commit();
                }
                catch
                {
                    result = 0;
                    //回滚事务
                    tran.Rollback();
                }
                return result;
            }
        }
    }
}
