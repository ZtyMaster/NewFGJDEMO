using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.ItcastOA.Model.Enum
{
   public static  class AddMD5
    {
        public static string GaddMD5(string st)
        {
            
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = Encoding.Default.GetBytes(st+"zty");
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", string.Empty);
        }
    }
}
