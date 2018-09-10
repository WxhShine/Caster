using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace CaterCommon {
    /// <summary>
    /// Table转换为List
    /// </summary>
    public class TableToListHelper {
        public static List<T> ConvertToList<T>(DataTable dt) {
            List<T> ts = new List<T>();
            Type type = typeof(T);
            //用于存储属性的名称
            string tempName = string.Empty;
            foreach (DataRow dr in dt.Rows) {
                //实例化此类
                T t = Activator.CreateInstance<T>();
                PropertyInfo[] propertys = type.GetProperties();
                foreach (PropertyInfo pi in propertys) {
                    tempName = pi.Name;     
                    if (dt.Columns.Contains(tempName)) {
                        // 判断此属性是否有Setter  
                        if (!pi.CanWrite) continue;
                        object value = dr[tempName];
                        //如果非空，则赋给对象的属性  
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                ts.Add(t);
            }
            return ts;
        }
    }
}
