using Product_Management_System.Data;
using Product_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_Management_System.Repositories.CostHistory
{
    public class CostHistoryRepository : ICostHistoryRepository
    {
        public void DeleteCostHistory(ProductCostHistory costHistory)
        {
            ProductCostHistoryDAO.DeleteProductCostHistory(costHistory);
        }

        public List<ProductCostHistory> GetAllCostHistories()
        {
            return ProductCostHistoryDAO.GetProductCostHistories();
        }

        public ProductCostHistory? GetCostHistoryById(int id)
        {
            return ProductCostHistoryDAO.GetProductCostHistoryById(id);
        }

        public void InsertCostHistory(ProductCostHistory costHistory)
        {
            ProductCostHistoryDAO.InsertProductCostHistory(costHistory);
        }

        public void UpdateCostHistory(ProductCostHistory costHistory)
        {
            ProductCostHistoryDAO.UpdateProductCostHistory(costHistory);
        }
    }
}