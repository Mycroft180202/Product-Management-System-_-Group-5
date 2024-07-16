using Product_Management_System.Models;
using Product_Management_System.Services.CostHistoryServices;
using Product_Management_System.Services.PriceHistoryServices;
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

namespace Product_Management_System.Views.CostHistory
{
    /// <summary>
    /// Interaction logic for ProductCostHistoryPage.xaml
    /// </summary>
    public partial class ProductCostHistoryPage : Page
    {
        private readonly ICostHistoryService iCostHistoryService;
        private readonly User _currentUser;

        public ProductCostHistoryPage(User currentUser)
        {
            InitializeComponent();
            iCostHistoryService = new CostHistoryService();
            _currentUser = currentUser;
            LoadProductCostHistory();
            ConfigureAdminFeatures();
        }

        private void LoadProductCostHistory()
        {
            dgData.ItemsSource = null;
            var Cost_history = iCostHistoryService.GetAllCostHistories();
            dgData.ItemsSource = Cost_history;
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
            if (dgData.SelectedItem is ProductCostHistory selectedProduct)
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

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newCostHistory = new ProductCostHistory
                {
                    StartDate = dpStartDate.SelectedDate.HasValue
                                    ? dpStartDate.SelectedDate.Value : DateTime.Now,
                    EndDate = dpEndDate.SelectedDate,
                    Cost = decimal.Parse(txtCost.Text)
                };
                iCostHistoryService.InsertCostHistory(newCostHistory);
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
                if (dgData.SelectedItem is ProductCostHistory selectedProduct)
                //if (int.TryParse(txtProductID.Text, out int productId))
                {
                    selectedProduct.StartDate = dpStartDate.SelectedDate.HasValue
                        ? dpStartDate.SelectedDate.Value : DateTime.Now;
                    selectedProduct.EndDate = dpEndDate.SelectedDate;
                    selectedProduct.Cost = decimal.Parse(txtCost.Text);

                    iCostHistoryService.UpdateCostHistory(selectedProduct);
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
                if (dgData.SelectedItem is ProductCostHistory selectedProduct)
                {
                    iCostHistoryService.DeleteCostHistory(selectedProduct);
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

            var filteredHistories = iCostHistoryService.GetAllCostHistories().AsQueryable();

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

        private void ConfigureAdminFeatures()
        {
            if (_currentUser.RoleId == 1) // Admin
            {
                btnCreate.Visibility = Visibility.Visible;
                btnDelete.Visibility = Visibility.Visible;
            }
        }
    }
}