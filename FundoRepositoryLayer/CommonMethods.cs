using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundoRepositoryLayer
{
    public  static class CommonMethods
    {
        public static string key = "abcdefghijklmnopqrstuvwxyz";
        public static string ConvertToEncrypt(string password)
        {
            if (String.IsNullOrEmpty(password))
            {
                return "";
            }
            password = password + key;
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }

        //public static string ConvertToDecrypt(string base64EncodeData)
        //{
        //    if (String.IsNullOrEmpty(base64EncodeData)) {

        //        return "";
            
        //    }
        //    var base64EncodeBytes = Convert.FromBase64String(base64EncodeData);
        //    var result = Encoding.UTF8.GetString(base64EncodeBytes);
        //    //result =result.Substring(0)

        //}
    }
}

