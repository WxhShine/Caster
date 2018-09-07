using System;

namespace CaterModel {
    /// <summary>
    /// 会员等级 实体
    /// </summary>
    [Serializable]
    public partial class MemberTypeInfo :BaseEntity {
        public MemberTypeInfo() { }
        #region Model
        /// <summary>
        /// 会员类型名称
        /// </summary>
        public string MTitle { set; get; }
        /// <summary>
        /// 会员折扣
        /// </summary>
        public decimal? MDiscount { set; get; }
        #endregion Model

    }
}

