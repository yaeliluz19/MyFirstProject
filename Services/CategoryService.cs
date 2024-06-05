
using Repositories;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryReposities _CategoryReposities;

        public CategoryService(ICategoryReposities CategoryReposities)
        {
            _CategoryReposities = CategoryReposities;
        }

        public async Task<List<MyFirstProject.Category>> GetCategories()
        {
            return await _CategoryReposities.GetCategories();
        }
    }
}
