using Product_Management_System.Data;
using Product_Management_System.Models;
using System.Collections.Generic;

namespace Product_Management_System.Repositories.Inventory
{
	public class InventoryRepository : IInventoryRepository
	{
		public void DeleteInventory(ProductInventory inventory)
		{
			ProductInventoryDAO.DeleteInventory(inventory);
		}

		public List<ProductInventory> GetAllInventories()
		{
			return ProductInventoryDAO.GetAllInventories();
		}

		public ProductInventory? GetInventoryById(int productId, short locationId)
		{
			return ProductInventoryDAO.GetInventoryById(productId, locationId);
		}

		public void InsertInventory(ProductInventory inventory)
		{
			ProductInventoryDAO.InsertInventory(inventory);
		}

		public void UpdateInventory(ProductInventory inventory)
		{
			ProductInventoryDAO.UpdateInventory(inventory);
		}
	}
}