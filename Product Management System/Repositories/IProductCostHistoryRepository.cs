using Product_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_Management_System.Repositories
{
    public interface IProductCostHistoryRepository
    {
        List<ProductCostHistory> GetProductCostHistories();
        void InsertProductCostHistory(ProductCostHistory productCostHistory);
        void UpdateProductCostHistory(ProductCostHistory productCostHistory);
        void DeleteProductCostHistory(ProductCostHistory productCostHistory);
        ProductCostHistory? GetProductCostHistoryById(int id);
    }
}
