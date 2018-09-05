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
    public partial class ManagerInfoDal {
        /// <summary>
        /// 查询获取结果集
        /// </summary>
        /// <returns></returns>
        public List<ManagerInfo> GetList() {
            //构造要查询的sql语句
            string sql = "select * from ManagerInfo";
            //使用helper进行查询，得到结果
            DataTable dt = SQLHelper.GetDataTable(sql);
            //将dt中的数据转存到list中
            List<ManagerInfo> list = new List<ManagerInfo>();
            foreach (DataRow row in dt.Rows) {
                list.Add(new ManagerInfo() {
                    MId = Convert.ToInt32(row["mid"]),
                    MName = row["mname"].ToString(),
                    MPwd = row["mpwd"].ToString(),
                    MType = Convert.ToInt32(row["mtype"])
                });
            }
            //将集合返回
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
        /// 修改管理员，特别注意：密码
        /// </summary>
        /// <param name="mi"></param>
        /// <returns></returns>
        public int Update(ManagerInfo mi) {
            //为什么要进行密码的判断：
            //答：因为密码值是经过md5加密存储的，当修改时，需要判断用户是否改了密码，如果没有改，则不变，如果改了，则重新进行md5加密

            //定义参数集合，可以动态添加元素
            List<SqlParameter> listPs = new List<SqlParameter>();
            //构造update的sql语句
            string sql = "update ManagerInfo set mname=@name";
            listPs.Add(new SqlParameter("@name", mi.MName));
            //判断是否修改密码
            if (!mi.MPwd.Equals("这是原来的密码吗")) {
                sql += ",mpwd=@pwd";
                listPs.Add(new SqlParameter("@pwd", MD5Helper.EncryptString(mi.MPwd)));
            }
            //继续拼接语句
            sql += ",mtype=@type where mid=@id";
            listPs.Add(new SqlParameter("@type", mi.MType));
            listPs.Add(new SqlParameter("@id", mi.MId));

            //执行语句并返回结果
            return SQLHelper.ExecuteNonQuery(sql, listPs.ToArray());
        }

        /// <summary>
        /// 根据编号删除管理员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id) {
            //构造删除的sql语句
            string sql = "delete from ManagerInfo where mid=@id";
            //根据语句构造参数
            SqlParameter p = new SqlParameter("@id", id);
            //执行操作
            return SQLHelper.ExecuteNonQuery(sql, p);
        }

        /// <summary>
        /// 根据用户名查找对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ManagerInfo GetByName(string name) {
            ManagerInfo mi = null;
            //构造语句
            string sql = "select * from managerInfo where mname=@name";
            //为语句构造参数
            var p = new SqlParameter("@name", name);
            //执行查询得到结果
            var dt = SQLHelper.GetDataTable(sql, p);
            //判断是否根据用户名查找到了对象
            if (dt.Rows.Count > 0) {
                //用户名是存在的
                mi = new ManagerInfo() {
                    MId = Convert.ToInt32(dt.Rows[0][0]),
                    MName = name,
                    MPwd = dt.Rows[0][2].ToString(),
                    MType = Convert.ToInt32(dt.Rows[0][3])
                };
            }
            else {
                //用户名不存在
            }
            return mi;
        }
    }
}
