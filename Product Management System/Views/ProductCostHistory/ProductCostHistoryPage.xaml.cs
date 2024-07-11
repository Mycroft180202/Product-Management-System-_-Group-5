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
using Product_Management_System.Services;

namespace Product_Management_System.Views.ProductCostHistory
{
    /// <summary>
    /// Interaction logic for ProductCostHistoryPage.xaml
    /// </summary>
    public partial class ProductCostHistoryPage : Page
    {
        private readonly IProductCostHistoryService iProductCostHistoryService;
        public ProductCostHistoryPage()
        {
            InitializeComponent();
            iProductCostHistoryService = new ProductCostHistoryService();
            LoadProductCostHistory();
        }

        private void LoadProductCostHistory()
        {
            dgData.ItemsSource = null;
            var cost_history = iProductCostHistoryService.GetProductCostHistories();
            dgData.ItemsSource = cost_history;
        }

        private void ClearInputField()
        {
            txtProductID.Clear();
            dpStartDate.SelectedDate = null;
            dpEndDate.SelectedDate = null;
            txtCost.Clear();
            txtFilterCost.Clear();
            dpFilterStartDate.SelectedDate = null;
            dpFilterEndDate.SelectedDate = null;
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgData.SelectedItem is Models.ProductCostHistory selectedProduct)
            {
                txtProductID.Text = selectedProduct.ProductId.ToString();
                dpStartDate.SelectedDate = selectedProduct.StartDate;
                dpEndDate.SelectedDate = selectedProduct.EndDate.HasValue
                    ? selectedProduct.EndDate
                    : (DateTime?)null;
                txtCost.Text = selectedProduct.Cost.ToString();
            }
        }

        private void btnClearFilter_Click(object sender, RoutedEventArgs e)
        {
            ClearInputField();
            LoadProductCostHistory();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)  //Tạm thời không biết có hoạt động hay không
        {                                                               //vì bị vướng foreign key productId với bảng Product
            try
            {
                var newCostHistory = new Models.ProductCostHistory
                {
                    StartDate = dpStartDate.SelectedDate.HasValue
                                    ? dpStartDate.SelectedDate.Value : DateTime.Now,
                    EndDate = dpEndDate.SelectedDate,
                    Cost = decimal.Parse(txtCost.Text)
                };
                iProductCostHistoryService.InsertProductCostHistory(newCostHistory);
                LoadProductCostHistory();
                ClearInputField();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadProductCostHistory();
            }

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgData.SelectedItem is Models.ProductCostHistory selectedProduct)
                //if (int.TryParse(txtProductID.Text, out int productId))
                {
                    selectedProduct.StartDate = dpStartDate.SelectedDate.HasValue
                        ? dpStartDate.SelectedDate.Value : DateTime.Now;
                    selectedProduct.EndDate = dpEndDate.SelectedDate;
                    selectedProduct.Cost = decimal.Parse(txtCost.Text);

                    iProductCostHistoryService.UpdateProductCostHistory(selectedProduct);
                    LoadProductCostHistory();
                    ClearInputField();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadProductCostHistory();
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgData.SelectedItem is Models.ProductCostHistory selectedProduct)
                {
                    iProductCostHistoryService.DeleteProductCostHistory(selectedProduct);
                    LoadProductCostHistory();
                    ClearInputField();
                }
                else
                {
                    MessageBox.Show("No product selected to remove!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadProductCostHistory();
            }
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            DateTime? startDate = dpFilterStartDate.SelectedDate;
            DateTime? endDate = dpFilterEndDate.SelectedDate;
            decimal? Cost = null;

            if (!string.IsNullOrWhiteSpace(txtFilterCost.Text) && decimal.TryParse(txtFilterCost.Text, out decimal parsedCost))
            {
                Cost = parsedCost;
            }

            var filteredHistories = iProductCostHistoryService.GetProductCostHistories().AsQueryable();

            if (startDate != null)
            {
                filteredHistories = filteredHistories.Where(h => h.StartDate >= startDate);
            }

            if (endDate != null)
            {
                filteredHistories = filteredHistories.Where(h => h.EndDate <= endDate);
            }

            if (Cost != null)
            {
                filteredHistories = filteredHistories.Where(h => h.Cost == Cost);
            }

            dgData.ItemsSource = filteredHistories.ToList();
        }
    }
}
