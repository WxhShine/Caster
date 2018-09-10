namespace CaterModel {
    /// <summary>
    /// 基类实体
    /// </summary>
    public class BaseEntity {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { set; get; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { set; get; }
    }
}
