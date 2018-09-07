using System;

namespace CaterModel {
    /// <summary>
    /// 订单 实体 
    /// </summary>
    [Serializable]
    public partial class OrderInfo : BaseEntity{
        public OrderInfo() { }

        #region Model
        /// <summary>
        /// 会员编号
        /// </summary>
        public int? MemberId { set; get; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime? ODate { set; get; }
        /// <summary>
        /// 消费金额
        /// </summary>
        public decimal? OMoney { set; get; }
        /// <summary>
        /// 是否结账
        /// </summary>
        public bool IsPay { set; get; }
        /// <summary>
        /// 桌号
        /// </summary>
        public int? TableId { set; get; }
        /// <summary>
        /// 折扣
        /// </summary>
        public decimal? Discount { set; get; }
        #endregion Model

    }
}

