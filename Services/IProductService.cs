using MyFirstProject;

namespace Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts(int? minPrice, int? maxPrice, string? description, int?[] categories);
        Task<Product> GetById(int id);

    }
}