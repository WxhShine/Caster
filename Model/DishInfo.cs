using System;

namespace CaterModel {
    /// <summary>
    /// 菜品 实体 
    /// </summary>
    [Serializable]
    public partial class DishInfo : BaseEntity {
        public DishInfo() { }

        #region Model
        /// <summary>
        /// 名称
        /// </summary>
        public string DTitle { set; get; }
        /// <summary>
        /// 菜品类型
        /// </summary>
        public int? DTypeId { set; get; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal? DPrice { set; get; }
        /// <summary>
        /// 首字母
        /// </summary>
        public string DChar { set; get; }
        #endregion Model

    }
}

