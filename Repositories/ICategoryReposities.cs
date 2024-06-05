using MyFirstProject;

namespace Repositories
{
    public interface ICategoryReposities
    {
        Task<List<Category>> GetCategories();
    }
}