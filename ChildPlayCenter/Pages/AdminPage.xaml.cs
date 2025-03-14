using Microsoft.EntityFrameworkCore;
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

namespace ChildPlayCenter.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
            LoadPersons();
            LoadServices();
        }

        private void LoadPersons()
        {
            using (var context = new PlayCenterContext())
            {
                PersonsGrid.ItemsSource = context.Persons.Include(p => p.Role).Where(p => p.DeletedAt == null).ToList();
            }
        }

        private void LoadServices()
        {
            using (var context = new PlayCenterContext())
            {
                ServicesGrid.ItemsSource = context.Services.ToList();
            }
        }

        private void DeletePerson_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            int personId = (int)button.Tag;
            using (var context = new PlayCenterContext())
            {
                var person = context.Persons.Find(personId);
                person.DeletedAt = DateTime.Now;
                context.SaveChanges();
                LoadPersons();
            }
        }

        private void DeleteService_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            int serviceId = (int)button.Tag;
            using (var context = new PlayCenterContext())
            {
                var service = context.Services.Find(serviceId);
                context.Services.Remove(service);
                context.SaveChanges();
                LoadServices();
            }
        }
    }
}
