using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CaterCommon {
    /// <summary>
    /// 数据库帮助类
    /// </summary>
    public static class SQLHelper {
        private static string ConnStr = ConfigurationManager.ConnectionStrings["Cater"].ConnectionString;//从配置文件中读取连接字符串

        //执行命令的方法：增删改
        public static int ExecuteNonQuery(string sql,params SqlParameter[] ps) {
            using(SqlConnection conn = new SqlConnection(ConnStr)) {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddRange(ps);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }
        //获取首行首列值
        public static object ExecuteScalar(string sql,params SqlParameter[] ps) {
            using (SqlConnection conn = new SqlConnection(ConnStr)) {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddRange(ps);
                conn.Open();
                var executeRow = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return executeRow;
            }
        }
        //获取datatable
        public static DataTable GetDataTable(string sql,params SqlParameter[] ps) {
            using (SqlConnection conn = new SqlConnection(ConnStr)) {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                adapter.SelectCommand.Parameters.AddRange(ps);
                adapter.Fill(dt);
                return dt;
            }
        }

        public static List<T> ExecuteScalarList<T>(string sql,params SqlParameter[] ps) {
            return TableToListHelper.ConvertToList<T>( GetDataTable(sql, ps));
        }
    }
}
