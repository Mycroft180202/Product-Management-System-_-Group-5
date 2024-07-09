using Product_Management_System.Models;
using Product_Management_System.Views.ProductCostHistory;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Product_Management_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ProductManagementDbContext dbcontext;
        public MainWindow()
        {
            InitializeComponent();
            dbcontext = new ProductManagementDbContext();
        }

        private void btnCostHistory_Click(object sender, RoutedEventArgs e)
        {
            frProductCostHistory.Content = new ProductCostHistoryPage();
        }
    }
}