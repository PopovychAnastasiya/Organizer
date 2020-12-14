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
    /// Interaction logic for UpdateEventPage.xaml
    /// </summary>
    public partial class UpdateEventPage : Window
    {
        ApplicationDb _db = new ApplicationDb();
        UserEvent _userEvent;
        public UpdateEventPage(UserEvent userEvent)  
        {
            if (userEvent != null)
            {
                InitializeComponent();
                _userEvent = userEvent;
                datePicker.SelectedDate = userEvent.StartDate;
                NameBox.Text = _userEvent.Name;
                ThemeBox.Text = _userEvent.Theme;
                NotesBox.Text = _userEvent.Notes;
                TimeBox.Text = _userEvent.StartDate.ToString("hh:mm:ss");
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var splitedTime = TimeBox.Text.Split(':');
            var dateTime = datePicker.SelectedDate.Value.Date
               .AddHours(Convert.ToInt32(splitedTime[0]))
               .AddMinutes(Convert.ToInt32(splitedTime[1]));

            _userEvent.Name = NameBox.Text;
            _userEvent.Theme = ThemeBox.Text;
            _userEvent.UserId = _userEvent.UserId;
            _userEvent.Notes = NotesBox.Text;
            _userEvent.StartDate = dateTime;
            
            await _db.SaveChangesAsync();
            this.Close();
        }
    }
}
