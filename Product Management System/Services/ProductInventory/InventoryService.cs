using Product_Management_System.Models;
using Product_Management_System.Repositories.Inventory;
using System.Collections.Generic;

namespace Product_Management_System.Services.InventoryServices
{
	public class InventoryService : IInventoryService
	{
		private readonly IInventoryRepository iInventoryRepository;

		public InventoryService() => iInventoryRepository = new InventoryRepository();

		public void DeleteInventory(ProductInventory inventory)
		{
			iInventoryRepository.DeleteInventory(inventory);
		}

		public List<ProductInventory> GetAllInventories()
		{
			return iInventoryRepository.GetAllInventories();
		}

		public ProductInventory? GetInventoryById(int productId, short locationId)
		{
			return iInventoryRepository.GetInventoryById(productId, locationId);
		}

		public void InsertInventory(ProductInventory inventory)
		{
			iInventoryRepository.InsertInventory(inventory);
		}

		public void UpdateInventory(ProductInventory inventory)
		{
			iInventoryRepository.UpdateInventory(inventory);
		}
	}
}