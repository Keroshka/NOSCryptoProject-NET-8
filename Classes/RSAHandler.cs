using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using Microsoft.Win32;
using System.ComponentModel;
using System.Windows;

namespace OSCryptoProject.Classes
{
    public class RSAHandler
    {
        private readonly RSA _rsa;

        public RSAHandler()
        {
            _rsa = RSA.Create();
            _rsa.KeySize = 4096;
        }

        public void SaveKeysToFile()
        {
            string privateKey = _rsa.ExportRSAPrivateKeyPem();
            string publicKey = _rsa.ExportRSAPublicKeyPem();

            StreamWriter privateWriter = new("private_key.txt");
            StreamWriter publicWriter = new("public_key.txt");
            privateWriter.Write(privateKey);
            publicWriter.Write(publicKey);
            privateWriter.Close();
            publicWriter.Close();
        }

        public void LoadKeyFromFile()
        {
            OpenFileDialog dialog = new();

            if (dialog.ShowDialog() == true)
            {
                StreamReader keyReader = new(dialog.FileName);
                _rsa.ImportFromPem(keyReader.ReadToEnd());
                keyReader.Close();
            }
        }

        public byte[]? Encrypt(string plainText)
        {
            try
            {
                byte[] toEncrypt = Encoding.UTF8.GetBytes(plainText);
                byte[] encryptedData = _rsa.Encrypt(toEncrypt, RSAEncryptionPadding.Pkcs1);

                StreamWriter fileWriter = new("encrypted_message_RSA.txt");
                string encryptedString = Convert.ToBase64String(encryptedData);
                fileWriter.Write(encryptedString);
                fileWriter.Close();
                return encryptedData;
            }
            catch (CryptographicException e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK);
                return null;
            }
        }

        public string Decrypt(byte[] encryptedText)
        {
            try
            {
                byte[] decryptedData = _rsa.Decrypt(encryptedText, RSAEncryptionPadding.Pkcs1);
                return Encoding.UTF8.GetString(decryptedData);
            }
            catch (CryptographicException e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK);
                return "";
            }
        }
    }
}
