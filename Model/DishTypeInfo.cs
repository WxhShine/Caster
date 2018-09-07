using System;

namespace CaterModel {
    /// <summary>
    /// 菜品分类 实体 
    /// </summary>
    [Serializable]
    public partial class DishTypeInfo : BaseEntity {
        public DishTypeInfo() { }

        #region Model
        /// <summary>
        /// 类别名称
        /// </summary>
        public string DTitle { set; get; }
        #endregion Model

    }
}

