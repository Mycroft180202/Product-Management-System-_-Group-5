using Product_Management_System.Models;
using Product_Management_System.Services.InventoryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Product_Management_System
{
	public partial class ProductInventoryPage : Page
	{
		private readonly IInventoryService _inventoryService;
		private List<ProductInventory> _inventories;

		public ProductInventoryPage()
		{
			InitializeComponent();
			_inventoryService = new InventoryService();
			LoadInventories();
		}

		private void LoadInventories()
		{
			_inventories = _inventoryService.GetAllInventories();
			dgData.ItemsSource = _inventories;
		}

		private void btnFilter_Click(object sender, RoutedEventArgs e)
		{
			int? productId = string.IsNullOrEmpty(txtFilterProductID.Text) ? null : (int?)int.Parse(txtFilterProductID.Text);
			short? locationId = string.IsNullOrEmpty(txtFilterLocationID.Text) ? null : (short?)short.Parse(txtFilterLocationID.Text);

			var filteredInventories = _inventories.Where(i =>
				(!productId.HasValue || i.ProductId == productId) &&
				(!locationId.HasValue || i.LocationId == locationId)).ToList();

			dgData.ItemsSource = filteredInventories;
		}

		private void btnClearFilter_Click(object sender, RoutedEventArgs e)
		{
			txtFilterProductID.Clear();
			txtFilterLocationID.Clear();
			dgData.ItemsSource = _inventories;
		}

		private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (dgData.SelectedItem is ProductInventory selectedInventory)
			{
				txtProductID.Text = selectedInventory.ProductId.ToString();
				txtLocationID.Text = selectedInventory.LocationId.ToString();
				txtShelf.Text = selectedInventory.Shelf;
				txtBin.Text = selectedInventory.Bin.ToString();
				txtQuantity.Text = selectedInventory.Quantity.ToString();
			}
		}

		private void btnCreate_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var newInventory = new ProductInventory
				{
					ProductId = int.Parse(txtProductID.Text),
					LocationId = short.Parse(txtLocationID.Text),
					Shelf = txtShelf.Text,
					Bin = byte.Parse(txtBin.Text),
					Quantity = short.Parse(txtQuantity.Text)
				};

				_inventoryService.InsertInventory(newInventory);
				LoadInventories();
				ClearInputFields();
				MessageBox.Show("Inventory created successfully.");
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error creating inventory: {ex.Message}");
			}
		}

		private void btnUpdate_Click(object sender, RoutedEventArgs e)
		{
			if (dgData.SelectedItem is ProductInventory selectedInventory)
			{
				try
				{
					selectedInventory.Shelf = txtShelf.Text;
					selectedInventory.Bin = byte.Parse(txtBin.Text);
					selectedInventory.Quantity = short.Parse(txtQuantity.Text);

					_inventoryService.UpdateInventory(selectedInventory);
					LoadInventories();
					ClearInputFields();
					MessageBox.Show("Inventory updated successfully.");
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Error updating inventory: {ex.Message}");
				}
			}
			else
			{
				MessageBox.Show("Please select an inventory to update.");
			}
		}

		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{
			if (dgData.SelectedItem is ProductInventory selectedInventory)
			{
				var result = MessageBox.Show("Are you sure you want to delete this inventory?", "Confirm Delete", MessageBoxButton.YesNo);
				if (result == MessageBoxResult.Yes)
				{
					try
					{
						_inventoryService.DeleteInventory(selectedInventory);
						LoadInventories();
						ClearInputFields();
						MessageBox.Show("Inventory deleted successfully.");
					}
					catch (Exception ex)
					{
						MessageBox.Show($"Error deleting inventory: {ex.Message}");
					}
				}
			}
			else
			{
				MessageBox.Show("Please select an inventory to delete.");
			}
		}

		private void ClearInputFields()
		{
			txtProductID.Clear();
			txtLocationID.Clear();
			txtShelf.Clear();
			txtBin.Clear();
			txtQuantity.Clear();
		}
	}
}