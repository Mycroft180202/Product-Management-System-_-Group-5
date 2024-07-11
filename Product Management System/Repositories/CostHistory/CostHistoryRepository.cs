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
            ProductCostHistoryDAO.DeleteCostHistory(costHistory);
        }

        public List<ProductCostHistory> GetAllCostHistories()
        {
            return ProductCostHistoryDAO.GetAllCostHistories();
        }

        public ProductCostHistory? GetCostHistoryById(int id)
        {
            return ProductCostHistoryDAO.GetCostHistoryById(id);
        }

        public void InsertCostHistory(ProductCostHistory costHistory)
        {
            ProductCostHistoryDAO.InsertCostHistory(costHistory);
        }

        public void UpdateCostHistory(ProductCostHistory costHistory)
        {
            ProductCostHistoryDAO.UpdateCostHistory(costHistory);
        }
    }
}
