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

        public int GetOrderIdByTableId(int tableId)
        {
            return oiDal.GetOrderIdByTableId(tableId);
        }

        public bool DianCai(int orderId, int dishId)
        {
            return oiDal.OrderDishes(orderId, dishId) > 0;
        }

        public bool UpdateCountByOid(int oid, int count)
        {
            return oiDal.UpdateCountByOId(oid, count) > 0;
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
