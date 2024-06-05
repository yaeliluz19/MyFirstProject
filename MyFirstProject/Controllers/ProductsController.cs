using AutoMapper;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace MyFirstProject.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private IProductService _productService;
       
        private IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService; 
            _mapper = mapper;
        }

        // GET: CategoriesController
        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetProducts( [FromQuery] int? minPrice, [FromQuery] int? maxPrice, [FromQuery] string? description,[FromQuery] int?[] categories)
        {
            try
            {
                List<Product> products = await _productService.GetProducts(minPrice,maxPrice,description,categories);
                List<ProductDTO> productDTOs = _mapper.Map<List<Product>, List<ProductDTO>>(products);
                return productDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }


    }
}
