using Product_Management_System.Models;
using Product_Management_System.Repositories.CostHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_Management_System.Services.CostHistoryServices
{
    public class CostHistoryService : ICostHistoryService
    {
        private readonly ICostHistoryRepository iCostHistoryRepository;
        public CostHistoryService() => iCostHistoryRepository = new CostHistoryRepository();
        public void DeleteCostHistory(ProductCostHistory costHistory)
        {
            iCostHistoryRepository.DeleteCostHistory(costHistory);
        }

        public List<ProductCostHistory> GetAllCostHistories()
        {
            return iCostHistoryRepository.GetAllCostHistories();
        }

        public ProductCostHistory? GetCostHistoryById(int id)
        {
            return iCostHistoryRepository.GetCostHistoryById(id);
        }

        public void InsertCostHistory(ProductCostHistory costHistory)
        {
            iCostHistoryRepository.InsertCostHistory(costHistory);
        }

        public void UpdateCostHistory(ProductCostHistory costHistory)
        {
            iCostHistoryRepository.UpdateCostHistory(costHistory);
        }
    }
}
