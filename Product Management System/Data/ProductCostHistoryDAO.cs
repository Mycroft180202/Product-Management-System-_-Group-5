using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product_Management_System.Models;

namespace Product_Management_System.Data
{
    public class ProductCostHistoryDAO
    {
        public static List<ProductCostHistory> GetProductCostHistories()
        {
            ProductManagementDbContext dbContext = new ProductManagementDbContext();
            return dbContext.ProductCostHistories.ToList();
        }

        public static void InsertProductCostHistory(ProductCostHistory productCostHistory)
        {
            ProductManagementDbContext dbContext = new ProductManagementDbContext();
            dbContext.ProductCostHistories.Add(productCostHistory);
            dbContext.SaveChanges();
        }

        public static void UpdateProductCostHistory(ProductCostHistory productCostHistory)
        {
            ProductManagementDbContext dbContext = new ProductManagementDbContext();
            dbContext.ProductCostHistories.Update(productCostHistory);
            dbContext.SaveChanges();
        }
        public static void DeleteProductCostHistory(ProductCostHistory productCostHistory)
        {
            ProductManagementDbContext dbContext = new ProductManagementDbContext();
            dbContext.ProductCostHistories.Remove(productCostHistory);
            dbContext.SaveChanges();
        }

        public static ProductCostHistory? GetProductCostHistoryById(int id)
        {
            ProductManagementDbContext dbContext = new ProductManagementDbContext();
            return dbContext.ProductCostHistories.Find(id);
        }
    }
}