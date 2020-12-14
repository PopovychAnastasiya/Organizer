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
    /// Interaction logic for AddEventPage.xaml
    /// </summary>
    public partial class AddEventPage : Window
    {
        ApplicationDb _db = new ApplicationDb();
        int _userId;
        public AddEventPage(int userId)
        {
            _userId = userId;
            InitializeComponent();
            datePicker.SelectedDate = DateTime.Now;
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var splitedTime = TimeBox.Text.Split(':');
            Console.WriteLine(datePicker.SelectedDate.Value.Date.ToString());
            var dateTime = datePicker.SelectedDate.Value.Date;
            try
            {
                dateTime = datePicker.SelectedDate.Value.Date
                    .AddHours(Convert.ToInt32(splitedTime[0]))
                    .AddMinutes(Convert.ToInt32(splitedTime[1]));
            } catch(Exception)
            {
                var hour = DateTime.Now.Hour;
                var minute = DateTime.Now.Minute;
                dateTime = datePicker.SelectedDate.Value.Date
                    .AddHours(Convert.ToInt32(hour))
                    .AddMinutes(Convert.ToInt32(minute));
                Console.WriteLine(e);
            }
            var userEvent = new UserEvent
            {
                Name = NameBox.Text,
                Theme = ThemeBox.Text,
                UserId = _userId,
                Notes = NotesBox.Text,
                StartDate = dateTime,
                WasNotified = false
            };

            _db.UserEvents.Add(userEvent);
            await _db.SaveChangesAsync();
            this.Close();
        }
    }
}
