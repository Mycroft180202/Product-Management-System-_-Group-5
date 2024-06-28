using Product_Management_System.Data;
using Product_Management_System.Models;
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

namespace Product_Management_System
{
    /// <summary>
    /// Interaction logic for ProductPriceHistoryWindow.xaml
    /// </summary>
    public partial class ProductPriceHistoryPage : Page
    {
        private ProductManagementDbContext dbcontext;
        public ProductPriceHistoryPage()
        {
            InitializeComponent();
            dbcontext = new ProductManagementDbContext();
            LoadProductPriceHistory();
        }

        private void LoadProductPriceHistory()
        {
            dgData.ItemsSource = null;
            dbcontext = new ProductManagementDbContext();
            var price_history = dbcontext.ProductPriceHistories.ToList();
            dgData.ItemsSource = price_history;
        }

        private void ClearInputField()
        {
            txtProductID.Clear();
            dpStartDate.SelectedDate = null;
            dpEndDate.SelectedDate = null;
            txtPrice.Clear();
            txtFilterPrice.Clear();
            dpFilterStartDate.SelectedDate = null;
            dpFilterEndDate.SelectedDate = null;
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgData.SelectedItem is ProductPriceHistory selectedProduct)
            {
                txtProductID.Text = selectedProduct.ProductId.ToString();
                dpStartDate.SelectedDate = selectedProduct.StartDate;
                dpEndDate.SelectedDate = selectedProduct.EndDate.HasValue
                    ? selectedProduct.EndDate
                    : (DateTime?)null;
                txtPrice.Text = selectedProduct.Price.ToString();
            }
        }

        private void btnClearFilter_Click(object sender, RoutedEventArgs e)
        {
            ClearInputField();
            LoadProductPriceHistory();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)  //Tạm thời không biết có hoạt động hay không
        {                                                               //vì bị vướng foreign key productId với bảng Product
            var newPriceHistory = new ProductPriceHistory
            {
                StartDate = dpStartDate.SelectedDate.HasValue
                    ? dpStartDate.SelectedDate.Value : DateTime.Now,
                EndDate = dpEndDate.SelectedDate,
                Price = decimal.Parse(txtPrice.Text)
            };
            dbcontext.ProductPriceHistories.Add(newPriceHistory);
            dbcontext.SaveChanges();                                    //vướng foreign key 
            LoadProductPriceHistory();
            ClearInputField();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgData.SelectedItem is ProductPriceHistory selectedProduct)
            //if (int.TryParse(txtProductID.Text, out int productId))
            {
                var product = dbcontext.ProductPriceHistories.FirstOrDefault(p => p.ProductId == selectedProduct.ProductId);
                if (product != null)
                {
                    product.StartDate = dpStartDate.SelectedDate.HasValue
                        ? dpStartDate.SelectedDate.Value : DateTime.Now;
                    product.EndDate = dpEndDate.SelectedDate;
                    product.Price = decimal.Parse(txtPrice.Text);

                    dbcontext.SaveChanges();
                    LoadProductPriceHistory();
                    ClearInputField();
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgData.SelectedItem is ProductPriceHistory selectedProduct)
            {
                var productToRemove = dbcontext.ProductPriceHistories
                    .SingleOrDefault(pro => pro.ProductId == selectedProduct.ProductId);

                if (productToRemove != null)
                {
                    dbcontext.ProductPriceHistories.Remove(productToRemove);
                    dbcontext.SaveChanges();
                    LoadProductPriceHistory();
                    ClearInputField();
                }
            }
            else
            {
                MessageBox.Show("No product selected to remove!");
            }
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            DateTime? startDate = dpFilterStartDate.SelectedDate;
            DateTime? endDate = dpFilterEndDate.SelectedDate;
            decimal? price = null;

            if (!string.IsNullOrWhiteSpace(txtFilterPrice.Text) && decimal.TryParse(txtFilterPrice.Text, out decimal parsedPrice))
            {
                price = parsedPrice;
            }

            var filteredHistories = dbcontext.ProductPriceHistories.AsQueryable();

            if (startDate != null)
            {
                filteredHistories = filteredHistories.Where(h => h.StartDate >= startDate);
            }

            if (endDate != null)
            {
                filteredHistories = filteredHistories.Where(h => h.EndDate <= endDate);
            }

            if (price != null)
            {
                filteredHistories = filteredHistories.Where(h => h.Price == price);
            }

            dgData.ItemsSource = filteredHistories.ToList();
        }

    }
}
