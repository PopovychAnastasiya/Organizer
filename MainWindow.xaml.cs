using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ApplicationDb _db = new ApplicationDb();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var user = _db.Users.SingleOrDefault(x => x.Email == UsernameBox.Text);
            if(user != null)
            {
                if(Helper.Encrypt(PasswordBox.Password) == user.Password)
                {
                    var dailtyShedulerPage = new DailyShedulerPage(user);
                    this.Close();
                    dailtyShedulerPage.ShowDialog();
                }else
                {
                    MessageBox.Show("Invalid Username or Password");
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var registerPage = new RegisterPage();
            registerPage.ShowDialog();
        }
        private void Button_Click_Help(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Hi) This is an agenda organizer! You can register or log in with your login and password. In this program you will be able to create events for the specified time, change them and receive notifications that this event has occurred (alarm clock).");
        }
    }
}

