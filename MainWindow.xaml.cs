﻿using NOSCryptoProject.Windows;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NOSCryptoProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAES_Click(object sender, RoutedEventArgs e)
        {
            wdAES aes = new();
            aes.Show();
        }

        private void btnRSA_Click(object sender, RoutedEventArgs e)
        {
            wdRSA rsa = new();
            rsa.Show();
        }

        private void btnSHA_Click(object sender, RoutedEventArgs e)
        {
            wdSHA wdSHA = new();
            wdSHA.Show();
        }
    }
}