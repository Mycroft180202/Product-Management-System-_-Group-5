﻿using Product_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_Management_System.Services.PriceHistoryServices
{
    public interface IManageProductService
    {
        List<Product> GetAllProduct();
        void InsertProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
        Product? GetProductById(int id);
    }
}
