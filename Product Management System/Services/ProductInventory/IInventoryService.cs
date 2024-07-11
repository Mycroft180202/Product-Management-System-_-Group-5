using Product_Management_System.Models;
using System.Collections.Generic;

namespace Product_Management_System.Services.InventoryServices
{
	public interface IInventoryService
	{
		List<ProductInventory> GetAllInventories();
		void InsertInventory(ProductInventory inventory);
		void UpdateInventory(ProductInventory inventory);
		void DeleteInventory(ProductInventory inventory);
		ProductInventory? GetInventoryById(int productId, short locationId);
	}
}