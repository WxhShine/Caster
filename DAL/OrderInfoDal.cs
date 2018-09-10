using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CaterCommon;
using CaterModel;

namespace CaterDal {
    /// <summary>
    /// 订单 数据层
    /// </summary>
    public partial class OrderInfoDal
    {
        /// <summary>
        /// 开单
        /// </summary>
        /// <param name="tableId">桌号</param>
        /// <returns></returns>
        public int AddOrder(int tableId)
        {
            //插入订单数据
            //更新餐桌状态
            //写在一起执行，只需要和数据库交互一次
            //下订单
            string sql = "INSERT INTO OrderInfo(odate,ispay,tableId) VALUES(GETDATE(),0,@tableId);" +
                //更新餐桌状态
                "UPDATE TableInfo SET tIsFree=0 WHERE Id=@tableId;" +
                //获取最新的订单编号
                "SELECT top 1 Id FROM orderinfo  WHERE tableId = @tableId ORDER BY Id DESC";
            SqlParameter p=new SqlParameter("@tableId", tableId);
            return Convert.ToInt32(SQLHelper.ExecuteScalar(sql, p));
        }

        /// <summary>
        /// 查看此桌号的当前账单
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public int GetOrderIdByTableId(int tableId)
        {
            string sql = "SELECT Id FROM orderinfo where tableId=@tableid and ispay=0";
            SqlParameter p=new SqlParameter("@tableId",tableId);
            return Convert.ToInt32(SQLHelper.ExecuteScalar(sql, p));
        }

        /// <summary>
        /// 点菜
        /// </summary>
        /// <param name="orderid">订单Id</param>
        /// <param name="dishId">菜肴Id</param>
        /// <returns></returns>
        public int OrderDishes(int orderid, int dishId)
        {
            //查询当前订单是否已经点了这道菜
            string sql = "SELECT COUNT(*) FROM orderDetailInfo WHERE orderId=@Id AND dishId=@did";
            SqlParameter[] ps =
            {
                new SqlParameter("@Id", orderid),
                new SqlParameter("@did", dishId)
            };
            int count = Convert.ToInt32(SQLHelper.ExecuteScalar(sql, ps));
            if (count > 0)
            {
                //这个订单已经点过这个菜，让数量加1
                sql = "UPDATE orderDetailInfo SET count=count+1 WHERE orderId=@Id AND dishId=@did";
            }
            else
            {
                //当前订单还没有点这个菜，加入这个菜
                sql = "INSERT INTO orderDetailInfo(orderid,dishId,count) VALUES(@Id,@did,1)";
            }
            return SQLHelper.ExecuteNonQuery(sql, ps);
        }

        /// <summary>
        /// 更新订单的菜品数量
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public int UpdateCountById(int Id,int count)
        {
            string sql = "UPDATE orderDetailInfo SET count=@count WHERE Id=@Id";
            SqlParameter[] ps =
            {
                new SqlParameter("@count", count),
                new SqlParameter("@Id", Id)
            };
            return SQLHelper.ExecuteNonQuery(sql, ps);
        }

        /// <summary>
        /// 获取订单详情列表
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<OrderDetailInfo> GetDetailList(int orderId)
        {
            string sql=@"SELECT odi.Id,di.dTitle,di.dPrice,odi.count FROM dishinfo AS di
            INNER JOIN OrderDetailInfo AS odi
            ON di.id=odi.dishid
            WHERE odi.orderId=@orderid";
            SqlParameter p=new SqlParameter("@orderid",orderId);

            
            var list=SQLHelper.ExecuteScalarList< OrderDetailInfo>(sql,p);
            //DataTable dt = SQLHelper.GetDataTable(sql, p);
            //foreach (DataRow row in dt.Rows)
            //{
            //    list.Add(new OrderDetailInfo()
            //    {
            //        Id = Convert.ToInt32(row["Id"]),
            //        DTitle = row["dtitle"].ToString(),
            //        DPrice = Convert.ToDecimal(row["dprice"]),
            //        Count = Convert.ToInt32(row["count"])
            //    });
            //}
            return list;
        }

        /// <summary>
        /// 获取订单总金额
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public decimal GetTotalMoneyByOrderId(int orderid)
        {
            string sql = @"	SELECT SUM(oti.count*di.dprice) 
	            FROM orderdetailinfo AS oti
	            INNER JOIN dishinfo AS di
	            ON oti.dishid=di.id
	            WHERE oti.orderid=@orderid";
           SqlParameter p=new SqlParameter("@orderid",orderid);

            object obj = SQLHelper.ExecuteScalar(sql, p);
            if (obj == DBNull.Value)
            {
                return 0;
            }
            return Convert.ToDecimal(obj);
        }

        /// <summary>
        /// 更新订单的金额
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public int SetOrderMomey(int orderid,decimal money)
        {
            string sql = "UPDATE orderinfo set omoney=@money WHERE Id=@Id";
            SqlParameter[] ps =
            {
                new SqlParameter("@money", money),
                new SqlParameter("@Id", orderid)
            };
            return SQLHelper.ExecuteNonQuery(sql, ps);
        }

        /// <summary>
        /// 删除订单详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int DeleteDetailById(int Id)
        {
            string sql = "DELETE FROM orderDetailInfo WHERE Id=@Id";
            SqlParameter p=new SqlParameter("@Id",Id);
            return SQLHelper.ExecuteNonQuery(sql, p);
        }

        /// <summary>
        /// 支付账单
        /// </summary>
        /// <param name="isUseMoney">是否使用余额</param>
        /// <param name="memberId">会员Id</param>
        /// <param name="payMoney">支付金额</param>
        /// <param name="orderid">订单Id</param>
        /// <param name="discount">折扣</param>
        /// <returns></returns>
        public int Pay(bool isUseMoney,int memberId,decimal payMoney,int orderid,decimal discount)
        {
            //创建数据库的链接对象
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Cater"].ConnectionString))
            {
                int result = 0;
                //由数据库链接对象创建事务
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                //创建command对象
                SqlCommand cmd=new SqlCommand();
                cmd.Connection = conn;
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
                        sql = "UPDATE MemberInfo SET mMoney=mMoney-@payMoney WHERE id=@mid";
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
                    sql = "UPDATE orderInfo SET isPay=1,memberId=@mid,discount=@discount WHERE Id=@Id";
                    ps = new SqlParameter[]
                    {
                        new SqlParameter("@mid", memberId),
                        new SqlParameter("@discount", discount),
                        new SqlParameter("@Id", orderid)
                    };
                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddRange(ps);
                    result += cmd.ExecuteNonQuery();

                    //3、将餐桌状态IsFree=1
                    sql = "UPDATE tableInfo SET tIsFree=1 WHERE Id=(SELECT tableId FROM orderinfo WHERE Id=@Id)";
                    SqlParameter p = new SqlParameter("@Id", orderid);
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
