using System.Security.Cryptography;
using System.Text;

namespace CaterCommon {
    public class MD5Helper {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EncryptString(string str) {
            MD5 md5 = MD5.Create();
            byte[] byteOld = Encoding.UTF8.GetBytes(str);
            byte[] byteNew = md5.ComputeHash(byteOld);
            StringBuilder sb = new StringBuilder();
            foreach(byte b in byteNew) {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}