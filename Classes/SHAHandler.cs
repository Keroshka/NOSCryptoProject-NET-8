using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace NOSCryptoProject.Classes
{
    public class SHAHandler
    {

        private readonly SHA256 _sha;
        public SHAHandler()
        {
            _sha = SHA256.Create();
        }

        public byte[] HashPlainText(string plainText)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(plainText);
            return _sha.ComputeHash(bytes);
        }
    }
}
