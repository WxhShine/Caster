using System;

namespace CaterModel {
    /// <summary>
    /// 订单详情 实体 
    /// </summary>
    [Serializable]
    public partial class OrderDetailInfo : BaseEntity{
        public OrderDetailInfo() { }
        #region Model
        /// <summary>
        /// 订单编号
        /// </summary>
        public int? OrderId { set; get; }
        /// <summary>
        /// 菜品编号
        /// </summary>
        public int? DishId { set; get; }
        /// <summary>
        /// 份数
        /// </summary>
        public int? Count { set; get; }
        #endregion Model

    }
}

