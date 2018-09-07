using System;

namespace CaterModel
{
	/// <summary>
	/// 餐桌 实体 
	/// </summary>
	[Serializable]
	public partial class TableInfo:BaseEntity
	{
		public TableInfo()
		{}

        #region Model
        /// <summary>
        /// 餐桌名称
        /// </summary>
        public string TTitle { set; get; }
        /// <summary>
        /// 厅包信息
        /// </summary>
        public int? THallId { set; get; }
        /// <summary>
        /// 是否空闲
        /// </summary>
        public bool TIsFree { set; get; }
        #endregion Model

    }
}

