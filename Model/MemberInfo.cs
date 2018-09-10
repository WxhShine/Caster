using System;

namespace CaterModel {
    /// <summary>
    /// 会员 实体
    /// </summary>
    [Serializable]
    public partial class MemberInfo :BaseEntity{
        public MemberInfo() { }

        #region Model
        /// <summary>
        /// 会员类型
        /// </summary>
        public int? MTypeId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string MName { set; get; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string MPhone { set; get; }
        /// <summary>
        /// 账户余额
        /// </summary>
        public decimal? MMoney { set; get; }
        #endregion Model

    }
}

