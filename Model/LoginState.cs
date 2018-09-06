using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterModel
{
    /// <summary>
    /// 登陆状态枚举
    /// </summary>
    public enum LoginState
    {
        /// <summary>
        /// 成功
        /// </summary>
        Ok,
        /// <summary>
        /// 用户名错误
        /// </summary>
        NameError,
        /// <summary>
        /// 密码错误
        /// </summary>
        PwdError
    }
}
