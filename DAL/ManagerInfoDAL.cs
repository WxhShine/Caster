using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterCommon;
using CaterModel;

namespace CaterDal {
    /// <summary>
    /// 管理员表数据层
    /// </summary>
    public partial class ManagerInfoDal {
        /// <summary>
        /// 查询获取结果集
        /// </summary>
        /// <returns></returns>
        public List<ManagerInfo> GetList() {
            string sql = "select * from ManagerInfo";
            var list = SQLHelper.ExecuteScalarList<ManagerInfo>(sql);
            return list;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="mi">ManagerInfo类型的对象</param>
        /// <returns></returns>
        public int Insert(ManagerInfo mi) {
            //构造insert语句
            string sql = "insert into ManagerInfo(mname,mpwd,mtype) values(@name,@pwd,@type)";
            //构造sql语句的参数
            SqlParameter[] ps = //使用数组初始化器
            {
                new SqlParameter("@name", mi.MName),
                new SqlParameter("@pwd", MD5Helper.EncryptString(mi.MPwd)),//将密码进行md5加密
                new SqlParameter("@type", mi.MType)
            };
            //执行插入操作
            return SQLHelper.ExecuteNonQuery(sql, ps);
        }

        /// <summary>
        /// 修改管理员
        /// </summary>
        /// <param name="mi"></param>
        /// <returns></returns>
        public int Update(ManagerInfo mi) {
            List<SqlParameter> listPs = new List<SqlParameter>();
            //构造update的sql语句
            string sql = "update ManagerInfo set mname=@name";
            listPs.Add(new SqlParameter("@name", mi.MName));
            //判断是否修改密码
            if (!mi.MPwd.Equals("这是原来的密码吗")) {
                sql += ",mpwd=@pwd";
                listPs.Add(new SqlParameter("@pwd", MD5Helper.EncryptString(mi.MPwd)));
            }
            sql += ",mtype=@type where Id=@id";
            listPs.Add(new SqlParameter("@type", mi.MType));
            listPs.Add(new SqlParameter("@id", mi.Id));

            //执行语句并返回结果
            return SQLHelper.ExecuteNonQuery(sql, listPs.ToArray());
        }

        /// <summary>
        /// 根据编号删除管理员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id) {
            string sql = "delete from ManagerInfo where Id=@id";
            SqlParameter p = new SqlParameter("@id", id);
            return SQLHelper.ExecuteNonQuery(sql, p);
        }

        /// <summary>
        /// 根据用户名查找对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ManagerInfo GetByName(string name) {
            string sql = "select * from managerInfo where mname=@name";
            var p = new SqlParameter("@name", name);
            //执行查询得到结果
            var managerInfo = SQLHelper.ExecuteScalarList<ManagerInfo>(sql, p).FirstOrDefault();

            return managerInfo;
        }
    }
}
