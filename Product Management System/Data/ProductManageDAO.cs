using Product_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_Management_System.Data
{
    public class ProductManageDAO
    {
        public static List<Product> GetAllProduct()
        {
            ProductManagementDbContext dbContext = new ProductManagementDbContext();
            return dbContext.Products.ToList();
        }

        public static void InsertProduct(Product product)
        {
            ProductManagementDbContext dbContext = new ProductManagementDbContext();
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
        }

        public static void UpdateProduct(Product product)
        {
            ProductManagementDbContext dbContext = new ProductManagementDbContext();
            dbContext.Products.Update(product);
            dbContext.SaveChanges();
        }

        public static void DeleteProduct(Product product)
        {
            ProductManagementDbContext dbContext = new ProductManagementDbContext();
            dbContext.Products.Remove(product);
            dbContext.SaveChanges();
        }

        public static Product? GetProductById(int id)
        {
            ProductManagementDbContext dbContext = new ProductManagementDbContext();
            return dbContext.Products.Find(id);
        }

    }
}