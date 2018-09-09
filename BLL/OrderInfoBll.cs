using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterDal;
using CaterModel;

namespace CaterBll
{
    /// <summary>
    /// 账单信息业务层
    /// </summary>
    public partial class OrderInfoBll
    {
        private OrderInfoDal oiDal=new OrderInfoDal();

        /// <summary>
        /// 开单
        /// </summary>
        /// <param name="tableId">桌号</param>
        /// <returns></returns>
        public int AddOrder(int tableId)
        {
            return oiDal.AddOrder(tableId);
        }

        /// <summary>
        /// 查看此桌号的当前账单
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public int GetOrderIdByTableId(int tableId)
        {
            return oiDal.GetOrderIdByTableId(tableId);
        }

        /// <summary>
        /// 点餐
        /// </summary>
        /// <param name="orderId">订单Id</param>
        /// <param name="dishId">菜品Id</param>
        /// <returns></returns>
        public bool OrderDishes(int orderId, int dishId)
        {
            return oiDal.OrderDishes(orderId, dishId) > 0;
        }

        /// <summary>
        /// 更新订单中的菜品数量
        /// </summary>
        /// <param name="oid"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool UpdateCountByOid(int oid, int count)
        {
            return oiDal.UpdateCountById(oid, count) > 0;
        }

        public List<OrderDetailInfo> GetDetailList(int orderId)
        {
            return oiDal.GetDetailList(orderId);
        }

        public decimal GetTotalMoneyByOrderId(int orderid)
        {
            return oiDal.GetTotalMoneyByOrderId(orderid);
        }

        public bool SetOrderMoney(int orderid, decimal money)
        {
            return oiDal.SetOrderMomey(orderid, money) > 0;
        }

        public bool DeleteDetailById(int oid)
        {
            return oiDal.DeleteDetailById(oid) > 0;
        }

        public bool Pay(bool isUseMoney, int memberId, decimal payMoney, int orderid, decimal discount)
        {
            return oiDal.Pay(isUseMoney, memberId, payMoney, orderid, discount) > 0;
        }
    }
}
