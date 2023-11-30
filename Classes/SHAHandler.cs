using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using Microsoft.Win32;

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
            byte[] bytes = Encoding.UTF8.GetBytes(plainText);//todo: save to file
            StreamWriter writer = new("hash.txt");
            StreamWriter txtWriter = new("text_before_hash.txt");
            byte[] hash = _sha.ComputeHash(bytes);
            writer.Write(Convert.ToBase64String(hash));
            txtWriter.Write(plainText);
            writer.Close();
            txtWriter.Close();
            return hash;
        }

        public byte[]? HashFile(out string original)
        {
            OpenFileDialog dialog = new();
            if (dialog.ShowDialog() == true)
            {
                StreamReader sr = new(dialog.FileName);
                string toHash = sr.ReadToEnd();
                original = toHash;
                sr.Close();
                return HashPlainText(toHash);
            }
            original = "";
            return null;
        }
    }
}
