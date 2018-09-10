using System.Collections.Generic;
using CaterCommon;
using CaterDal;
using CaterModel;

namespace CaterBll {
    /// <summary>
    /// 店员信息业务层
    /// </summary>
    public partial class ManagerInfoBll {
        ManagerInfoDal miDal = new ManagerInfoDal();

        /// <summary>
        /// 查询用户列表
        /// </summary>
        /// <returns></returns>
        public List<ManagerInfo> GetList() {
            //调用查询方法
            return miDal.GetList();
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="mi"></param>
        /// <returns></returns>
        public bool Add(ManagerInfo mi) {
            //调用dal层的insert方法，完成插入操作
            return miDal.Insert(mi) > 0;
        }

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="mi">要修改的用户实体</param>
        /// <returns></returns>
        public bool Edit(ManagerInfo mi) {
            return miDal.Update(mi) > 0;
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="id">用户Id</param>
        /// <returns></returns>
        public bool Remove(int id) {
            return miDal.Delete(id) > 0;
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="type">用户类型</param>
        /// <returns></returns>
        public LoginState Login(string name, string pwd, out int type) {
            type = -1;
            //根据用户名进行对象的查询
            ManagerInfo mi = miDal.GetByName(name);
            if (mi == null) {
                return LoginState.NameError;
            } else {
                //用户名正确
                if (mi.MPwd.Equals(MD5Helper.EncryptString(pwd))) {
                    //密码正确
                    //登录成功
                    type = mi.MType;
                    return LoginState.Ok;
                } else {
                    //密码错误
                    return LoginState.PwdError;
                }
            }
        }
    }
}
