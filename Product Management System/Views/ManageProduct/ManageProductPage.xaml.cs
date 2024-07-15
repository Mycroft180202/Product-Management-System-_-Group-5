using Microsoft.EntityFrameworkCore;
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

namespace Product_Management_System.Views.ManageProduct
{
    /// <summary>
    /// Interaction logic for ManageProductPage.xaml
    /// </summary>
    public partial class ManageProductPage : Page
    {

        private ProductManagementDbContext con = new ProductManagementDbContext();
        public ManageProductPage()
        {
            InitializeComponent();
            LoadPage();
        }

        private void LoadPage()
        {
            grdProduct.ItemsSource = con.Products.AsNoTracking()
                .Include(x => x.Subcategory)
                .Include(r => r.Model)
                .ToList();
            cbCategory.ItemsSource = con.ProductSubcategories.ToList();
            cbModel.ItemsSource = con.ProductModels.ToList();
        }

        private void grdProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grdProduct.SelectedItem != null)
            {
                var selectedProduct = (Product)grdProduct.SelectedItem; // Assuming Product is your model
                tbId.Text = selectedProduct.ProductId.ToString();
                tbName.Text = selectedProduct.Name;
                tbColor.Text = selectedProduct.Color;
                tbCost.Text = selectedProduct.Cost.ToString();
                tbPrice.Text = selectedProduct.Price.ToString();
                cbCategory.SelectedItem = selectedProduct.Subcategory;
                cbModel.SelectedItem = selectedProduct.Model;
                dtpStart.SelectedDate = selectedProduct.SellStartDate;
                dtpEnd.SelectedDate = selectedProduct.SellEndDate;
            }
        }

        private void clear()
        {
            tbId.Text = string.Empty;
            tbName.Text = string.Empty;
            tbColor.Text = string.Empty;
            tbCost.Text = string.Empty;
            tbPrice.Text = string.Empty;
            cbCategory.SelectedItem = null;
            cbModel.SelectedItem = null;
            dtpStart.SelectedDate = null;
            dtpEnd.SelectedDate = null;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            clear();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var maxProductId = con.Products.Max(p => p.ProductId);
                var newProduct = new Product();
                newProduct.ProductId = maxProductId + 1;
                newProduct.Name = tbName.Text;
                newProduct.Color = tbColor.Text;
                newProduct.Cost = decimal.Parse(tbCost.Text);
                newProduct.Price = decimal.Parse(tbPrice.Text);
                newProduct.SubcategoryId = (cbCategory.SelectedItem as ProductSubcategory)?.SubcategoryId;
                newProduct.ModelId = (cbModel.SelectedItem as ProductModel)?.ModelId;
                newProduct.SellStartDate = dtpStart.SelectedDate ?? DateTime.Now;
                newProduct.SellEndDate = dtpEnd.SelectedDate;

                con.Products.Add(newProduct);
                con.SaveChanges();
                LoadPage();
                clear();

                MessageBox.Show("Product added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to add product. Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tbId.Text, out int productId))
            {
                var productToUpdate = con.Products.FirstOrDefault(p => p.ProductId == productId);
                if (productToUpdate != null)
                {
                    try
                    {
                        productToUpdate.Name = tbName.Text;
                        productToUpdate.Color = tbColor.Text;
                        productToUpdate.Cost = decimal.Parse(tbCost.Text);
                        productToUpdate.Price = decimal.Parse(tbPrice.Text);
                        productToUpdate.SubcategoryId = (cbCategory.SelectedItem as ProductSubcategory)?.SubcategoryId;
                        productToUpdate.ModelId = (cbModel.SelectedItem as ProductModel)?.ModelId;
                        productToUpdate.SellStartDate = dtpStart.SelectedDate ?? DateTime.Now;
                        productToUpdate.SellEndDate = dtpEnd.SelectedDate;

                        con.SaveChanges();
                        LoadPage();
                        clear();

                        MessageBox.Show("Product updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to update product. Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Product not found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tbId.Text, out int productId))
            {
                var productToDelete = con.Products
                    .Include(r => r.ProductCostHistories)
                    .Include(x => x.ProductInventories)
                    .Include(y => y.ProductPriceHistories)
                    .FirstOrDefault(p => p.ProductId == productId);
                if (productToDelete != null)
                {
                    try
                    {
                        if (productToDelete.ProductInventories.Count > 0)
                        {
                            con.ProductInventories.RemoveRange(productToDelete.ProductInventories);
                        }
                        if (productToDelete.ProductPriceHistories.Count > 0)
                        {
                            con.ProductPriceHistories.RemoveRange(productToDelete.ProductPriceHistories);
                        }
                        if (productToDelete.ProductCostHistories.Count > 0)
                        {
                            con.ProductCostHistories.RemoveRange(productToDelete.ProductCostHistories);
                        }
                        con.Products.Remove(productToDelete);
                        con.SaveChanges();
                        LoadPage();
                        clear();

                        MessageBox.Show("Product deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to delete product. Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Product not found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadPage();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            grdProduct.ItemsSource = con.Products.Where(p => p.Name.Contains(txtSearch.Text)).ToList();
        }
    }
}
