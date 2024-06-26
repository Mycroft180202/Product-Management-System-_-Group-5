using Product_Management_System.Data;
using Product_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_Management_System.Repositories
{
    public class ProductCostHistoryRepository : IProductCostHistoryRepository
    {
        public void DeleteProductCostHistory(ProductCostHistory productCostHistory) => ProductCostHistoryDAO.DeleteProductCostHistory(productCostHistory);

        public ProductCostHistory? GetProductCostHistoryById(int id) => ProductCostHistoryDAO.GetProductCostHistoryById(id);

        public List<ProductCostHistory> GetProductCostHistories() => ProductCostHistoryDAO.GetProductCostHistories();

        public void InsertProductCostHistory(ProductCostHistory productCostHistory) => ProductCostHistoryDAO.InsertProductCostHistory(productCostHistory);

        public void UpdateProductCostHistory(ProductCostHistory productCostHistory) => ProductCostHistoryDAO.UpdateProductCostHistory(productCostHistory);
    }
}
