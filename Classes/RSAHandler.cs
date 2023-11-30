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
            byte[] privateKey = _rsa.ExportRSAPrivateKey();
            byte[] publicKey = _rsa.ExportRSAPublicKey();
            StreamWriter privateWriter = new("private_key.txt");
            StreamWriter publicWriter = new("public_key.txt");
            privateWriter.Write(Convert.ToBase64String(privateKey));
            publicWriter.Write(Convert.ToBase64String(publicKey));
            privateWriter.Close();
            publicWriter.Close();
        }

        public void LoadPublicKeyFromFile()
        {
            OpenFileDialog dialog = new();

            if (dialog.ShowDialog() == true)
            {
                StreamReader keyReader = new(dialog.FileName);
                string key = keyReader.ReadToEnd();
                _rsa.ImportRSAPublicKey(Convert.FromBase64String(key), out int bytesRead);
                keyReader.Close();
            }
        }
        public void LoadPrivateKeyFromFile()
        {
            OpenFileDialog dialog = new();

            if (dialog.ShowDialog() == true)
            {
                StreamReader keyReader = new(dialog.FileName);
                string key = keyReader.ReadToEnd();
                _rsa.ImportRSAPrivateKey(Convert.FromBase64String(key), out int bytesRead);
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

        public byte[]? SignFile(out string original)
        {
            OpenFileDialog dialog = new();

            if (dialog.ShowDialog() == true)
            {
                StreamReader reader = new(dialog.FileName);
                string text = reader.ReadToEnd();
                reader.Close();
                original = text;
                byte[] signature = _rsa.SignData(Encoding.UTF8.GetBytes(text), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

                StreamWriter textWriter = new("before_signature.txt");
                textWriter.Write(text);
                textWriter.Close();
                StreamWriter signatureWriter = new("signature.txt");
                signatureWriter.Write(Convert.ToBase64String(signature));
                signatureWriter.Close();
                return signature;
            }
            original = "";
            return null;
        }

        public bool? CheckSignatureFromFile()
        {
            OpenFileDialog textDialog = new();
            textDialog.Title = "Upload original file";

            if (textDialog.ShowDialog() == true)
            {
                StreamReader reader = new(textDialog.FileName);
                string text = reader.ReadToEnd();
                reader.Close();

                OpenFileDialog signatureDialog = new();
                signatureDialog.Title = "Upload signature";
                if (signatureDialog.ShowDialog() == true)
                {
                    StreamReader signReader = new(signatureDialog.FileName);
                    byte[] signature = Convert.FromBase64String(signReader.ReadToEnd());
                    signReader.Close();
                    byte[] data = Encoding.UTF8.GetBytes(text);

                    return _rsa.VerifyData(data, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                }
            }
            return null;
        }
    }
}
