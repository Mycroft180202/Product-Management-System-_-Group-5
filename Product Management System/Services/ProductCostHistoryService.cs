using Product_Management_System.Models;
using Product_Management_System.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_Management_System.Services
{
    public class ProductCostHistoryService : IProductCostHistoryService
    {
        private readonly IProductCostHistoryRepository iProductCostHistoryRepository;

        public ProductCostHistoryService()
        {
            iProductCostHistoryRepository = new ProductCostHistoryRepository();
        }

        public void DeleteProductCostHistory(ProductCostHistory productCostHistory)
        {
            iProductCostHistoryRepository.DeleteProductCostHistory(productCostHistory);
        }

        public ProductCostHistory? GetProductCostHistoryById(int id)
        {
            return iProductCostHistoryRepository.GetProductCostHistoryById(id);
        }

        public List<ProductCostHistory> GetProductCostHistories()
        {
            return iProductCostHistoryRepository.GetProductCostHistories();
        }

        public void InsertProductCostHistory(ProductCostHistory productCostHistory)
        {
            iProductCostHistoryRepository.InsertProductCostHistory(productCostHistory);
        }

        public void UpdateProductCostHistory(ProductCostHistory productCostHistory)
        {
            iProductCostHistoryRepository.UpdateProductCostHistory(productCostHistory);
        }
    }
}
