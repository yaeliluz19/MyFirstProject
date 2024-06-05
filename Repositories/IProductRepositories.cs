using MyFirstProject;

namespace Repositories
{
    public interface IProductRepositories
    {
        Task<List<Product>> GetProducts(int? minPrice, int? maxPrice, string? description, int?[] categories);
        
        Task<Product> GetById(int id);

    }
}