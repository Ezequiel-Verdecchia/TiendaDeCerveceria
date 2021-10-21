using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cerveceria.Web.Helper
{
    public class HashHelper
    {
        public string ConvertToHash(string pass) 
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();

            byte[] inputBytes = (new UnicodeEncoding()).GetBytes(pass);
            byte[] hash = sha1.ComputeHash(inputBytes);
            
            return Convert.ToBase64String(hash);
        }
       
    }
}
