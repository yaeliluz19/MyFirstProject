using AutoMapper;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace MyFirstProject.Controllers
{

    [Route("api/[controller]")]

    
    public class CategoriesController : Controller
    {
        private ICategoryService _CategoryService;
        private IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _CategoryService = categoryService;
            _mapper = mapper;
        }

        // GET: CategoriesController
        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> GetCategories()
        {
            try
            {
                List<Category> categories= await _CategoryService.GetCategories();
                List<CategoryDTO> categoryDTOs = _mapper.Map < List<Category>, List < CategoryDTO >> (categories);
                return Ok(categories);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
