using Product_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_Management_System.Repositories.CostHistory
{
    public interface ICostHistoryRepository
    {
        List<ProductCostHistory> GetAllCostHistories();
        void InsertCostHistory(ProductCostHistory costHistory);
        void UpdateCostHistory(ProductCostHistory costHistory);
        void DeleteCostHistory(ProductCostHistory costHistory);
        ProductCostHistory? GetCostHistoryById(int id);
    }
}