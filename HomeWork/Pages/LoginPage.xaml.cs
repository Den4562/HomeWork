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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : UserControl
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string login = tbLogin.Text;
            string password = pbPassword.Password;

            
            string filePath = "People.txt";

            if (checkUser(login, password, filePath))
            {
               
                Switcher.Switch(new HomePage());
            }
            else
            {
              
                MessageBox.Show("Неверный логин или пароль.", "Ошибка входа");
            }
        }
        private bool checkUser(string login, string password, string filePath)
        {
     
            if (!File.Exists(filePath))
            {
                return false;
            }

     
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
             
                string[] credentials = line.Split(',');

             
                if (credentials.Length == 2 && credentials[0] == login && credentials[1] == password)
                {
                    return true; 
                }
            }

            return false; 
        }
        private void Reg_Click(object sender, RoutedEventArgs e)
        {

           
                Switcher.Switch(new Reg());


        }

       

    }
}
