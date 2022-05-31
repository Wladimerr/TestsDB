﻿using AppForTests.ModuleAuth.Presentation;
using AppForTests.Shared.Domain;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppForTests
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NavController.Setup(MainFrame);
            NavController.OnNavigated = title => this.Title = title;

            NavController.NavigateToLogIn();
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            while(MainFrame.CanGoBack)
                MainFrame.RemoveBackEntry();
        }
    }
}
