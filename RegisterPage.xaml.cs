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
using System.Windows.Shapes;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Window
    {

        ApplicationDb _db = new ApplicationDb();
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var user = new User
            {
                Email = UsernameBox.Text,
                Name = NameBox.Text,
                Surname = SurnameBox.Text,
                Password = Helper.Encrypt(PasswordBox.Password)
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            this.Close();
        }
    }
}
