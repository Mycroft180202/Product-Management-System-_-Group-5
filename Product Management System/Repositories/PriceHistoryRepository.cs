using Product_Management_System.Data;
using Product_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_Management_System.Repositories
{
    public class PriceHistoryRepository : IPriceHistoryRepository
    {
        public void DeletePriceHistory(ProductPriceHistory priceHistory)
        {
            ProductPriceHistoryDAO.DeletePriceHistory(priceHistory);
        }

        public List<ProductPriceHistory> GetAllPriceHistories()
        {
            return ProductPriceHistoryDAO.GetAllPriceHistories();
        }

        public ProductPriceHistory? GetPriceHistoryById(int id)
        {
            return ProductPriceHistoryDAO.GetPriceHistoryById(id);
        }

        public void InsertPriceHistory(ProductPriceHistory priceHistory)
        {
            ProductPriceHistoryDAO.InsertPriceHistory(priceHistory);
        }

        public void UpdatePriceHistory(ProductPriceHistory priceHistory)
        {
            ProductPriceHistoryDAO.UpdatePriceHistory(priceHistory);
        }
    }
}
