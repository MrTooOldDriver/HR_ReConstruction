﻿using System;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HR_ReConstruction
{
    /// <summary>
    /// Network_Window.xaml 的交互逻辑
    /// </summary>
    public partial class Network_Window : Window
    {
        public Network_Window()
        {
            InitializeComponent();
        }

        private void Import_Data_Grid_Click(object sender, RoutedEventArgs e)
        {
            DatabaseReader database = new DatabaseReader();
            Network network = new Network();
            byte[] fromdatabaseBytes = database.readBytes();
            InputdataGrid.ItemsSource = network.InputData(fromdatabaseBytes);
        }

        private void Data_Grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void InputdataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}