using MyFirstProject;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService : IProductService
    {
        private IProductRepositories _productRepositories;

        public ProductService(IProductRepositories productRepositories)
        {
            _productRepositories = productRepositories;
        }

        public async Task<List<MyFirstProject.Product>> GetProducts(int? minPrice, int? maxPrice, string? description, int?[] categories)
        {
            return await _productRepositories.GetProducts(minPrice, maxPrice, description, categories);
        }
        public async Task<Product> GetById(int id)
        {
            return await _productRepositories.GetById(id);

        }
    }
}
