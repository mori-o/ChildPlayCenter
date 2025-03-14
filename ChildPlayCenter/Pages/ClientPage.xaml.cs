using ChildPlayCenter.Models;
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
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        private readonly int _userId;

        public ClientPage(int userId)
        {
            InitializeComponent();
            _userId = userId;
            LoadServices();
            LoadCart();
        }

        private void LoadServices()
        {
            using (var context = new PlayCenterContext())
            {
                ServicesGrid.ItemsSource = context.Services.Where(s => s.IsAvailable).ToList();
            }
        }

        private void LoadCart()
        {
            using (var context = new PlayCenterContext())
            {
                var client = context.Clients.First(c => c.PersonId == _userId);
                CartGrid.ItemsSource = context.Carts.Include(c => c.Service).Where(c => c.ClientId == client.Id).ToList();
            }
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            int serviceId = (int)button.Tag;
            using (var context = new PlayCenterContext())
            {
                var client = context.Clients.First(c => c.PersonId == _userId);
                context.Carts.Add(new Cart { ClientId = client.Id, ServiceId = serviceId, AddedAt = DateTime.Now });
                context.SaveChanges();
                LoadCart();
            }
        }

        private void RemoveFromCart_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            int cartId = (int)button.Tag;
            using (var context = new PlayCenterContext())
            {
                var cartItem = context.Carts.Find(cartId);
                context.Carts.Remove(cartItem);
                context.SaveChanges();
                LoadCart();
            }
        }

        private void PlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new PlayCenterContext())
            {
                var client = context.Clients.First(c => c.PersonId == _userId);
                var cartItems = context.Carts.Include(c => c.Service).Where(c => c.ClientId == client.Id).ToList();
                decimal totalPrice = cartItems.Sum(c => c.Service.Price);

                var order = new Order
                {
                    ClientId = client.Id,
                    Date = DateTime.Now,
                    TotalPrice = totalPrice,
                    StatusId = 1
                };
                context.Orders.Add(order);
                context.Carts.RemoveRange(cartItems);
                context.SaveChanges();
                LoadCart();
                MessageBox.Show("Заказ оформлен!");
            }
        }
    }
}
