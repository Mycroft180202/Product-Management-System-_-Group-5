using Product_Management_System.Models;
using Product_Management_System.Repositories.PriceHistory;
using Product_Management_System.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_Management_System.Services.PriceHistoryServices
{
    public class ManageProductService : IManageProductService
    {
        private readonly IManageProductRepository iProductRepository;
        public ManageProductService() => iProductRepository = new ManageProductRepository();

        public void DeleteProduct(Product product)
        {
            iProductRepository.DeleteProduct(product);
        }

        public List<Product> GetAllProduct()
        {
            return iProductRepository.GetAllProduct();
        }

        public Product? GetProductById(int id)
        {
            return iProductRepository.GetProductById(id);
        }

        public void InsertProduct(Product product)
        {
            iProductRepository.InsertProduct(product);
        }

        public void UpdateProduct(Product product)
        {
            iProductRepository.UpdateProduct(product);
        }
    }
}