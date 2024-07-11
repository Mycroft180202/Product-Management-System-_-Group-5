using Product_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_Management_System.Data
{
    public class ProductCostHistoryDAO
    {
        public static List<ProductCostHistory> GetAllCostHistories()
        {
            ProductManagementDbContext dbContext = new ProductManagementDbContext();
            return dbContext.ProductCostHistories.ToList();
        }

        public static void InsertCostHistory(ProductCostHistory costHistory)
        {
            ProductManagementDbContext dbContext = new ProductManagementDbContext();
            dbContext.ProductCostHistories.Add(costHistory);
            dbContext.SaveChanges();
        }

        public static void UpdateCostHistory(ProductCostHistory costHistory)
        {
            ProductManagementDbContext dbContext = new ProductManagementDbContext();
            dbContext.ProductCostHistories.Update(costHistory);
            dbContext.SaveChanges();
        }

        public static void DeleteCostHistory(ProductCostHistory costHistory)
        {
            ProductManagementDbContext dbContext = new ProductManagementDbContext();
            dbContext.ProductCostHistories.Remove(costHistory);
            dbContext.SaveChanges();
        }

        public static ProductCostHistory? GetCostHistoryById(int id)
        {
            ProductManagementDbContext dbContext = new ProductManagementDbContext();
            return dbContext.ProductCostHistories.Find(id);
        }
    }
}
