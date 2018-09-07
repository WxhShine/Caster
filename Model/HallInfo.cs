using System;

namespace CaterModel {
    /// <summary>
    /// 厅包 实体
    /// </summary>
    [Serializable]
    public partial class HallInfo : BaseEntity{
        public HallInfo() { }
        #region Model
        /// <summary>
        /// 厅包名
        /// </summary>
        public string HTitle { set; get; }
        #endregion Model

    }
}

