using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace DAL {
    public class ManagerInfoDAL {

        public List<ManagerInfo> GetList() {
            string sql = "SELECT * FROM ManagerInfo";
            var dt = SQLHelper.GetDataTable(sql);
            var list = new List<ManagerInfo>();
            foreach (DataRow row in dt.Rows) {
                list.Add(new ManagerInfo {
                    MId = Convert.ToInt32(row["MId"]),
                    MName = row["MName"].ToString(),
                    MPwd = row["MPwd"].ToString(),
                    MType = Convert.ToInt32(row["MType"]),
                });
            }
            return list;
        }

        public int Insert(ManagerInfo mi) {
            string sql = "INSERT INTO ManagerInfo (MName,MPwd,MType) values(@name,@pwd,@type)";
            SqlParameter[] ps = {
                new SqlParameter("@name",mi.MName),
                new SqlParameter("@pwd",MD5Helper.EncryptString(mi.MPwd)),
                new SqlParameter("@type",mi.MType),
            };
            return SQLHelper.ExecuteNonQuery(sql, ps);
        }

        public int Update(ManagerInfo mi) {
            List<SqlParameter> ps = new List<SqlParameter>();
            string sql = "UPDATE ManagerInfo SET MName = @name";
            ps.Add(new SqlParameter("@name", mi.MName));
            if (mi.MPwd.Equals("是否修改")) {
                sql += ",MPwd = @pwd";
                ps.Add(new SqlParameter("@pwd", MD5Helper.EncryptString(mi.MPwd)));
            }
            sql+=",MType = @type WHERE MId = @id";
            ps.Add(new SqlParameter("@type", mi.MType));
            ps.Add(new SqlParameter("@id", mi.MId));
            return SQLHelper.ExecuteNonQuery(sql, ps.ToArray());
        }

        public int Delete(int id) {
            string sql = "DELETE FROM ManagerInfo WHERE MId = @id";
            SqlParameter p = new SqlParameter("@id", id);
            return SQLHelper.ExecuteNonQuery(sql, p);
        }
    }
}