using Product_Management_System.Data;
using Product_Management_System.Models;
using Product_Management_System.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_Management_System.Repositories.PriceHistory
{
    public class ManageProductRepository : IManageProductRepository
    {
        public void DeleteProduct(Product product)
        {
            ProductManageDAO.DeleteProduct(product);
        }

        public List<Product> GetAllProduct()
        {
            return ProductManageDAO.GetAllProduct();
        }

        public Product? GetProductById(int id)
        {
            return ProductManageDAO.GetProductById(id);
        }

        public void InsertProduct(Product product)
        {
            ProductManageDAO.InsertProduct(product);
        }

        public void UpdateProduct(Product product)
        {
            ProductManageDAO.UpdateProduct(product);
        }
    }
}
