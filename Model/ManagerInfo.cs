using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterModel
{
    /// <summary>
    /// 管理员 实体
    /// </summary>
    public partial class ManagerInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int MId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string MName { get; set; }
        /// <summary>
        /// 密码（MD5加密)
        /// </summary>
        public string MPwd { get; set; }
        /// <summary>
        /// 用户类型
        /// （1：经理；2：店员）
        /// </summary>
        public int MType { get; set; } 
    }
}
