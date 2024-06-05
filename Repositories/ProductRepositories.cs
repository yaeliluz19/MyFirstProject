using Microsoft.EntityFrameworkCore;
using MyFirstProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductRepositories : IProductRepositories
    {
        private Market326354982Context _market326354982;

        public ProductRepositories(Market326354982Context market326354982Context)
        {
            _market326354982 = market326354982Context;
        }

        public async Task<List<Product>> GetProducts(int? minPrice, int? maxPrice, string? description, int?[] categories)
        {

            var query = _market326354982.Products.Where(product =>
            (description == null ? (true) : (product.ProductName.Contains(description)))
            && ((minPrice == null) ? (true) : (product.Price >= minPrice))
            && ((maxPrice == null) ? (true) : (product.Price <= maxPrice))
            && ((categories.Length == 0) ? (true) : categories.Contains(product.CategoryId)))
                .OrderBy(product => product.Price);
            Console.WriteLine(query.ToQueryString());
            List<Product> products = await query.ToListAsync();
            return products;

            //List<MyFirstProject.Product> products = await _market326354982.Products.Include(i => i.Category).ToListAsync();
            //return products;

        }
        public async Task<Product> GetById(int id)
        {
            return await _market326354982.Products.FindAsync(id);

        }
    }
}
