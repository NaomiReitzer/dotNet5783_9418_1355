﻿using BO;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using static BO.Enums;
using System.Collections.ObjectModel;
using System.Linq;

namespace PL.OrderWindows
{
    /// <summary>
    /// Interaction logic for TrackingWindow.xaml
    /// </summary>
    public partial class TrackingWindow : Window
    {
        private BlApi.IBl bl = BlApi.Factory.Get();

        public OrderTracking orderTracking;
        public ObservableCollection<Tuple<DateTime?, OrderStatus?>> Tracking;

        public TrackingWindow()
        {
            InitializeComponent();

            this.DataContext = orderTracking;
            this.DataContext = Tracking;
        }

        private void trackOrderButton_Click(object sender, RoutedEventArgs e)
        {
            int orderID = int.Parse(OrderIDTextBox.Text);

            // If the order ID is too short
            if (orderID.ToString().Length != 6)
                MessageBox.Show("The ID is unvalid, enter 6 digits", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            
            else
            {
                try
                {
                    orderTracking = bl.Order.TrackOrder(orderID);
                    Tracking = new ObservableCollection<Tuple<DateTime?, OrderStatus?>>(orderTracking.Tracking);
                }
                catch (DoesntExist ex)
                {
                    MessageBox.Show("There is no order with this ID", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            OrderIDTextBox.Clear();


        }

        private void IDprev(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            // Allow only numbers
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
