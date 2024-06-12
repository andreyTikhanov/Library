using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace newTestLibrary.Model
{
     public class Crypto
    {
        public Crypto()
        {
            
        }
        public static string CryptPassword(string password)
        {
            var cryptPassword = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(cryptPassword);
        }
    }
}
