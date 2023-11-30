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


        public wdSHA()
        {
            InitializeComponent();
            _rsa = new RSAHandler();
        }
    }
}
