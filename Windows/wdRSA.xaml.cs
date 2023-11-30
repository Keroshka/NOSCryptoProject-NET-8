using Microsoft.Win32;
using OSCryptoProject.Classes;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for wdRSA.xaml
    /// </summary>
    public partial class wdRSA : Window
    {

        private readonly RSAHandler _rsa;

        public wdRSA()
        {
            InitializeComponent();
            _rsa = new RSAHandler();
        }

        private void btnLoadKey_Click(object sender, RoutedEventArgs e)
        {
            _rsa.LoadPublicKeyFromFile();
        }

        private void btnSaveKey_Click(object sender, RoutedEventArgs e)
        {
            _rsa.SaveKeysToFile();
        }

        private void btnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            if (txtPlainText.Text != "")
            {
                string encrypted = Convert.ToBase64String(_rsa.Encrypt(txtPlainText.Text));
                if (encrypted != null) txtEncryptedText.Text = encrypted;
            }
        }

        private void btnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            if (txtEncryptedText.Text != "")
            {
                txtPlainText.Text = _rsa.Decrypt(Convert.FromBase64String(txtEncryptedText.Text));
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
                txtPlainText.Text = _rsa.Decrypt(byteString);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLoadPrivateKey_Click(object sender, RoutedEventArgs e)
        {
            _rsa.LoadPrivateKeyFromFile();
        }
    }
}
