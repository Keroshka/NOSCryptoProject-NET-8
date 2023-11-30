using NOSCryptoProject.Classes;
using OSCryptoProject.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for wdSHA.xaml
    /// </summary>
    public partial class wdSHA : Window
    {
        private readonly RSAHandler _rsa;
        private readonly SHAHandler _sha;

        public wdSHA()
        {
            InitializeComponent();
            _rsa = new RSAHandler();
            _sha = new SHAHandler();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLoadKey_Click(object sender, RoutedEventArgs e)
        {
            _rsa.LoadKeyFromFile();
        }

        private void btnSaveKey_Click(object sender, RoutedEventArgs e)
        {
            _rsa.SaveKeysToFile();
        }

        private void btnHash_Click(object sender, RoutedEventArgs e)
        {
            byte[] bytes = _sha.HashPlainText(txtPlainText.Text);
            txtEncryptedText.Text = Convert.ToBase64String(bytes);
        }

        private void btnSign_Click(object sender, RoutedEventArgs e)
        {
            byte[]? hash = _rsa.SignFile();
            if (hash != null)
            {
                txtEncryptedText.Text = Convert.ToBase64String(hash);
            }
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            bool? check = _rsa.CheckSignatureFromFile();
            if (check != null)
            {
                if (check == true)
                {
                    MessageBox.Show("The provided message and signature are VALID", "Result", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("The provided message and signature are NOT VALID", "Result", MessageBoxButton.OK);
                }
            }
        }
    }
}
