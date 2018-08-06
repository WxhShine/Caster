using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Common {
    public class MD5Helper {
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