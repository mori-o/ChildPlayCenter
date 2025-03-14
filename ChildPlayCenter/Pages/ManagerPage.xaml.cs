using ChildPlayCenter.Models;
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
    /// Логика взаимодействия для ManagerPage.xaml
    /// </summary>
    public partial class ManagerPage : Page
    {
        private List<int> selectedServices = new List<int>();

        public ManagerPage()
        {
            InitializeComponent();
            LoadClients();
            LoadServices();
        }

        private void LoadClients()
        {
            using (var context = new PlayCenterContext())
            {
                ClientComboBox.ItemsSource = context.Clients.ToList();
            }
        }

        private void LoadServices()
        {
            using (var context = new PlayCenterContext())
            {
                ServicesGrid.ItemsSource = context.Services.Where(s => s.IsAvailable).ToList();
            }
        }

        private void AddToOrder_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            int serviceId = (int)button.Tag;
            selectedServices.Add(serviceId);
        }

        private void PlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            if (ClientComboBox.SelectedItem == null || !selectedServices.Any())
            {
                MessageBox.Show("Выберите клиента и услуги!");
                return;
            }

            using (var context = new PlayCenterContext())
            {
                var client = (Client)ClientComboBox.SelectedItem;
                var services = context.Services.Where(s => selectedServices.Contains(s.Id)).ToList();
                decimal totalPrice = services.Sum(s => s.Price);

                var order = new Order
                {
                    ClientId = client.Id,
                    Date = DateTime.Now,
                    TotalPrice = totalPrice,
                    StatusId = 1
                };
                context.Orders.Add(order);
                context.SaveChanges();
                selectedServices.Clear();
                MessageBox.Show("Заказ оформлен!");
            }
        }
    }
}
