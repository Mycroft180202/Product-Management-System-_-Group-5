using Product_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_Management_System.Data
{
    public class ProductPriceHistoryDAO
    {
        public static List<ProductPriceHistory> GetAllPriceHistories()
        {
            ProductManagementDbContext dbContext = new ProductManagementDbContext();
            return dbContext.ProductPriceHistories.ToList();
        }

        public static void InsertPriceHistory(ProductPriceHistory priceHistory)
        {
            ProductManagementDbContext dbContext = new ProductManagementDbContext();
            dbContext.ProductPriceHistories.Add(priceHistory);
            dbContext.SaveChanges();
        }

        public static void UpdatePriceHistory(ProductPriceHistory priceHistory)
        {
            ProductManagementDbContext dbContext = new ProductManagementDbContext();
            dbContext.ProductPriceHistories.Update(priceHistory);
            dbContext.SaveChanges();
        }

        public static void DeletePriceHistory(ProductPriceHistory priceHistory)
        {
            ProductManagementDbContext dbContext = new ProductManagementDbContext();
            dbContext.ProductPriceHistories.Remove(priceHistory);
            dbContext.SaveChanges();
        }

        public static ProductPriceHistory? GetPriceHistoryById (int id)
        {
            ProductManagementDbContext dbContext = new ProductManagementDbContext();
            return dbContext.ProductPriceHistories.Find(id);
        }
    }
}
