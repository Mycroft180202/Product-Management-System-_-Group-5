﻿using Microsoft.EntityFrameworkCore;
using Product_Management_System.Data;
using Product_Management_System.Models;
using Product_Management_System.Services;
using Product_Management_System.Services.PriceHistoryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Product_Management_System
{
    /// <summary>
    /// Interaction logic for ProductPriceHistoryWindow.xaml
    /// </summary>
    public partial class ProductPriceHistoryPage : Page
    {
        private readonly IPriceHistoryService _priceHistoryService;
        private readonly User _currentUser;

        public ProductPriceHistoryPage(User currentUser)
        {
            InitializeComponent();
            _priceHistoryService = new PriceHistoryService();
            _currentUser = currentUser;
            LoadProductPriceHistory();
            ConfigureAdminFeatures();
        }

        private void LoadProductPriceHistory()
        {
            dgData.ItemsSource = null;
            var price_history = _priceHistoryService.GetAllPriceHistories();
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

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newPriceHistory = new ProductPriceHistory
                {
                    StartDate = dpStartDate.SelectedDate.HasValue
                                    ? dpStartDate.SelectedDate.Value : DateTime.Now,
                    EndDate = dpEndDate.SelectedDate,
                    Price = decimal.Parse(txtPrice.Text)
                };
                _priceHistoryService.InsertPriceHistory(newPriceHistory);
                LoadProductPriceHistory();
                ClearInputField();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadProductPriceHistory();
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgData.SelectedItem is ProductPriceHistory selectedProduct)
                {
                    selectedProduct.StartDate = dpStartDate.SelectedDate.HasValue
                        ? dpStartDate.SelectedDate.Value : DateTime.Now;
                    selectedProduct.EndDate = dpEndDate.SelectedDate;
                    selectedProduct.Price = decimal.Parse(txtPrice.Text);

                    _priceHistoryService.UpdatePriceHistory(selectedProduct);
                    LoadProductPriceHistory();
                    ClearInputField();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadProductPriceHistory();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgData.SelectedItem is ProductPriceHistory selectedProduct)
                {
                    _priceHistoryService.DeletePriceHistory(selectedProduct);
                    LoadProductPriceHistory();
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
                LoadProductPriceHistory();
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

            var filteredHistories = _priceHistoryService.GetAllPriceHistories().AsQueryable();

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
