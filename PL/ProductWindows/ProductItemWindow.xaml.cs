﻿using BO;
using DO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.ProductWindows
{
    /// <summary>
    /// Interaction logic for ProductItemWindow.xaml
    /// </summary>
    public partial class ProductItemWindow : Window
    {
        private BlApi.IBl bl = BlApi.Factory.Get();

        private Action<ProductItem?> addToCartAction;

        private ProductItem ProductItem;

        public ProductItemWindow()
        {
            InitializeComponent();
        }
        public ProductItemWindow(int id,Cart cart, Action<ProductItem?> action)
        {
            InitializeComponent();

            try
            {
                ProductItem = bl.Product.GetProductFromCatalog(id, cart);
            }
            catch(DoesntExist ex) { }

            this.DataContext = ProductItem;

            this.addToCartAction = action;

            try
            {
                Uri resourceUri = new Uri(ProductItem.Image, UriKind.Absolute);
                ProductImage.Source = new BitmapImage(resourceUri);
            }
            // incase there is no image
            catch (Exception ex) { }

            mainGrid.IsEnabled = false;
        }
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == true)
            {
                string selectedFileName = dlg.FileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
                ProductImage.Source = bitmap;
            }
        }

        private void addToCartButton_Click(object sender, RoutedEventArgs e)
        {
            addToCartAction(null);
            ProductItem.AmountInCart++;
            amountInCartTextBox.Text = ProductItem.AmountInCart.ToString();
        }

    }
    
}