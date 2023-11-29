using Microsoft.Win32;
using OSSP.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NOSCryptoProject.Windows
{
    /// <summary>
    /// Interaction logic for wdAES.xaml
    /// </summary>
    /// 


    public partial class wdAES : Window
    {
        public string? PlainText { get; set; }
        public string? EncryptedText { get; set; }

        private readonly AES _aes;

        public wdAES()
        {
            InitializeComponent();
            txtPlainText.DataContext = PlainText;
            txtEncryptedText.DataContext = EncryptedText;
            _aes = new AES();
        }

        private void btnSaveKey_Click(object sender, RoutedEventArgs e)
        {
            _aes.SaveKeyToFile("secret_key.txt");
        }

        private void btnLoadKey_Click(object sender, RoutedEventArgs e)
        {
            _aes.LoadKeyFromFile();
        }

        private void btnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            if (txtPlainText.Text != "")
            {
                byte[] encryptedText = _aes.CryptPlainText(txtPlainText.Text);
                string encryptedString = Convert.ToBase64String(encryptedText);
                txtEncryptedText.Text = encryptedString;
            }
        }

        private void btnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            if (txtEncryptedText.Text != "")
            {
                byte[] byteString = Convert.FromBase64String(txtEncryptedText.Text);
                txtPlainText.Text = _aes.DecryptPlainText(byteString);
            }
        }

        private void btnDecryptFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new();
            if (dialog.ShowDialog() == true)
            {
                StreamReader reader = new(dialog.FileName);
                string encryptedString = reader.ReadToEnd();
                reader.Close();
                txtEncryptedText.Text = encryptedString;

                byte[] byteString = Convert.FromBase64String(encryptedString);
                txtPlainText.Text = _aes.DecryptPlainText(byteString);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
