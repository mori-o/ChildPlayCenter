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
    /// Логика взаимодействия для AnimatorPage.xaml
    /// </summary>
    public partial class AnimatorPage : Page
    {
        public AnimatorPage()
        {
            InitializeComponent();
            LoadEvents();
        }

        private void LoadEvents()
        {
            using (var context = new PlayCenterContext())
            {
                EventsGrid.ItemsSource = context.Events.Include(e => e.Service).ToList();
            }
        }
    }
}
