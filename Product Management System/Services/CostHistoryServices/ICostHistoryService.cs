using Product_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_Management_System.Services.CostHistoryServices
{
    public interface ICostHistoryService
    {
        List<ProductCostHistory> GetAllCostHistories();
        void InsertCostHistory(ProductCostHistory costHistory);
        void UpdateCostHistory(ProductCostHistory costHistory);
        void DeleteCostHistory(ProductCostHistory costHistory);
        ProductCostHistory? GetCostHistoryById(int id);
    }
}