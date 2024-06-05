using Microsoft.EntityFrameworkCore;
using MyFirstProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoryReposities : ICategoryReposities
    {
        private Market326354982Context _market326354982;

        public CategoryReposities(Market326354982Context market326354982Context)
        {
            _market326354982 = market326354982Context;
        }

        public async Task<List<Category>> GetCategories()
        {
            List<Category> categories = await _market326354982.Categories.ToListAsync();
            return categories;

        }
    }
    
}
