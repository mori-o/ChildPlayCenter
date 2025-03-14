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
    /// Логика взаимодействия для OperatorPage.xaml
    /// </summary>
    public partial class OperatorPage : Page
    {
        public OperatorPage()
        {
            InitializeComponent();
            LoadGameMachines();
        }

        private void LoadGameMachines()
        {
            using (var context = new PlayCenterContext())
            {
                GameMachinesGrid.ItemsSource = context.GameMachines.Include(g => g.Status).ToList();
            }
        }

        private void ChangeStatus_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var button = (Button)sender;
            int machineId = (int)button.Tag;
            using (var context = new PlayCenterContext())
            {
                var machine = context.GameMachines.Find(machineId);
                machine.StatusId = (machine.StatusId % 3) + 1; // Циклическое переключение статусов (1-3)
                machine.LastUpdated = DateTime.Now;
                machine.UpdatedById = 1; // Предположим, ID оператора фиксирован
                context.SaveChanges();
                LoadGameMachines();
            }
        }
    }
}
