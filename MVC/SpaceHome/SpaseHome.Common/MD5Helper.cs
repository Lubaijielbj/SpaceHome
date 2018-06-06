using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SpaseHome.Common
{
    public class MD5Helper
    {
        /// <summary>
        /// 64位MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5Encrypt64(string str)
        {
            //判读参数是否为空
            if (str == null || str.Trim() == string.Empty)
            {
                return null;
            }

            MD5 md5 = MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.Default.GetBytes(str));

            return Convert.ToBase64String(s);
        }
    }
}
