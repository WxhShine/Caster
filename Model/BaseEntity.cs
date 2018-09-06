using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterModel {
    /// <summary>
    /// 基类实体
    /// </summary>
    public class BaseEntity {
        private int _id;
        private bool _isdelete;

        /// <summary>
        /// 编号
        /// </summary>
        public int Id {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete {
            set { _isdelete = value; }
            get { return _isdelete; }
        }
    }
}
