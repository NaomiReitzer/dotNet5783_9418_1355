﻿using PL.OrderWindows;
using PL.ProductWindows;
using System.Windows;

namespace PL
{

    /// <summary>
    /// Interaction logic for BuyerWindow.xaml
    /// </summary>

    public partial class BuyerWindow : Window
    {
        public BuyerWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) => new CatalogWindow().Show();

        private void MenuItem_Click_1(object sender, RoutedEventArgs e) => new TrackingWindow().Show();

    }
}