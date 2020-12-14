using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
    /// Interaction logic for DailyShedulerPage.xaml
    /// </summary>
    public partial class DailyShedulerPage : Window
    {
        ApplicationDb _db = new ApplicationDb();
        DataGrid grid;
        User currentUser;
        Timer timer = new Timer(6000);

        public DailyShedulerPage(User user)
        {
            currentUser = user;
            InitializeComponent();
            Load();
            timer.Enabled = true;
            timer.Elapsed += CheckForEvent;
        }

        private async void CheckForEvent(object sender, ElapsedEventArgs e)
        {
            var items = grid.Items.OfType<UserEvent>().ToList()
                .Where(x => x.StartDate.Date == DateTime.Today);

            var eventToNofy = items.FirstOrDefault(x => x.StartDate.Hour == DateTime.Now.Hour
                                                    && x.StartDate.Minute == DateTime.Now.Minute
                                                    &&x.WasNotified == false);
            if (eventToNofy != null)
            {
                MessageBox.Show($"You have to ...\n{eventToNofy.Name}");
                eventToNofy.WasNotified = true;
                await _db.SaveChangesAsync();
            }
            
        }

        private async void Load()
        {
            await UpdateGrid();
            grid = myDataGrid;
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addEventPage = new AddEventPage(currentUser.Id);
            addEventPage.ShowDialog();
            await UpdateGrid();
        }

        private async void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            var userEvent = (myDataGrid.SelectedItem as UserEvent);
            if (userEvent != null)
            {
                var addEventPage = new UpdateEventPage(userEvent);
                addEventPage.ShowDialog();
                await UpdateGrid();
            }
        }

        private async void deleteBtn_Click(object sender, RoutedEventArgs e)
        { 
            var userEvent = (myDataGrid.SelectedItem as UserEvent);
            if(userEvent != null) {
                var id = userEvent.Id;
                var eventToDelete = _db.UserEvents.SingleOrDefault(x => x.Id == id);
                if (eventToDelete != null)
                {
                    _db.UserEvents.Remove(eventToDelete);
                    await _db.SaveChangesAsync();
                }

                await UpdateGrid();   
            }
        }

        private async Task UpdateGrid()
        {
            myDataGrid.ItemsSource = await _db.UserEvents
                           .Where(x => x.UserId == currentUser.Id).ToListAsync();
        }

        private void myDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
