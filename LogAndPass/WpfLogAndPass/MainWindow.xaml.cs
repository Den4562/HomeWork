using System;
using System.Data;
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
using MaterialDesignThemes.Wpf;
using System.Diagnostics;
using System.Threading;

namespace WpfLogAndPass
{
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

           
        }
        private void GoogleBut_Click(object sender, RoutedEventArgs e)
        {
            
            Process.Start("https://sites.google.com/new?hl=RU");
        }
        private void FaceBook_Click(object sender, RoutedEventArgs e)
        {
            
            Process.Start("https://uk-ua.facebook.com");
        }
        private void Apple_Click(object sender, RoutedEventArgs e)
        {
           
            Process.Start("https://www.apple.com");
        }
        private async void Enter_Click(object sender, RoutedEventArgs e)
        {
           
            string login = Login.Text;
            string password = Pass.Password;

            if (login == "Denis" && password == "123")
            {
               
                Snackbar.IsActive = true;
                
                await Task.Delay(1000);

                Snackbar.IsActive = false;
            }
           
            
            
            Login.Text = "";
            Pass.Password = "";
        }

    }
}
