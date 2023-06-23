using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
using WpfApp1.SwitcherProvider;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для Reg.xaml
    /// </summary>
    public partial class Reg : UserControl
    {
        public Reg()
        {
            InitializeComponent();
        }


        private async void Enter_Click(object sender, RoutedEventArgs e)
        {

     
            User user = new User
            {
                Username = Login.Text,
                Password = Pass.Password
            };

            string filePath = "People.txt";

         
            string credentials = $"{user.Username},{user.Password}{Environment.NewLine}";
            File.AppendAllText(filePath, credentials);

         
           Success.IsActive = true;
           await Task.Delay(1000);
           Success.IsActive = false;

         
            Login.Text = "";
            Pass.Password = "";
        }


        private async void Back_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new LoginPage());
        }

        public class User
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
