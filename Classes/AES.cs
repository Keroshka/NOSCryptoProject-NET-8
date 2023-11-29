using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;
using System.Windows;


namespace OSSP.Classes
{
    public class AES
    {

        private readonly Aes _aes;

        public AES()
        {
            _aes = Aes.Create();
            _aes.KeySize = 256;
            _aes.GenerateKey();
            string IV = "Puw48B23ojnWV79oZbFcxA==";
            byte[] bytes = Convert.FromBase64String(IV);
            _aes.IV = bytes;
        }
        ~AES()
        {
            _aes.Clear();
        }

        public void SetKey(byte[] key)
        {
            _aes.Key = key;
        }

        public void SaveKeyToFile(string fileName)
        {

            MemoryStream keyStream = new(_aes.Key);
            StreamReader keyReader = new(keyStream);
            StreamWriter writer = new(fileName);
            //writer.Write(keyReader.ReadToEnd());
            string keyString = Convert.ToBase64String(keyStream.ToArray());
            writer.Write(keyString);
            keyStream.Close();
            keyReader.Close();
            writer.Close();
        }

        public void SaveIVToFile(string fileName)
        {
            MemoryStream keyStream = new(_aes.IV);
            StreamReader keyReader = new(keyStream);
            StreamWriter writer = new(fileName);
            //writer.Write(keyReader.ReadToEnd());
            string keyString = Convert.ToBase64String(keyStream.ToArray());
            writer.Write(keyString);
            keyStream.Close();
            keyReader.Close();
            writer.Close();
        }

        public void LoadKeyFromFile()
        {
            OpenFileDialog dialog = new();

            if (dialog.ShowDialog() == true)
            {
                StreamReader keyReader = new(dialog.FileName);
                string keyString = keyReader.ReadToEnd();
                byte[] keyByte = Convert.FromBase64String(keyString);
                _aes.Key = keyByte;
                keyReader.Close();
            }
        }

        public void LoadIVFromFile()
        {
            OpenFileDialog dialog = new();

            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                StreamReader keyReader = new(dialog.FileName);
                string keyString = keyReader.ReadToEnd();
                byte[] keyByte = Convert.FromBase64String(keyString);
                _aes.IV = keyByte;
                keyReader.Close();
            }
        }

        public void SetIV(byte[] iv)
        {
            _aes.IV = iv;
        }

        public byte[] CryptPlainText(string plainText, bool saveToFile = true)
        {
            byte[] encryptedText;
            ICryptoTransform encryptor = _aes.CreateEncryptor(_aes.Key, _aes.IV);

            using (MemoryStream memoryStream = new())
            {
                using (CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter writer = new(cryptoStream))
                    {
                        writer.Write(plainText);
                        writer.Close();
                    }
                }
                encryptedText = memoryStream.ToArray();
            }

            if (saveToFile)
            {
                using (StreamWriter fileWriter = new("encrypted_message_AES.txt"))
                {
                    string encryptedString = Convert.ToBase64String(encryptedText);
                    fileWriter.Write(encryptedString);
                    fileWriter.Close();
                }
            }
            return encryptedText;
        }

        public string DecryptPlainText(byte[] cipher)
        {
            ICryptoTransform decryptor = _aes.CreateDecryptor();
            string plainText;

            using (MemoryStream memoryStream = new(cipher))
            {
                using (CryptoStream cryptoStream = new(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader reader = new(cryptoStream))
                    {
                        try
                        {
                            plainText = reader.ReadToEnd();
                            reader.Close();
                            return plainText;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error while decrypting file with given key.", "Cryptography Error", MessageBoxButton.OK);
                        }
                    }
                }
            }
            return "";
        }
    }
}
