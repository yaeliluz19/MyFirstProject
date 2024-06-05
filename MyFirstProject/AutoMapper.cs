using AutoMapper;
using DTOs;
namespace MyFirstProject
{
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<Product, ProductDTO>().ForMember(dest=>dest.CategoryName,opts=>opts.MapFrom(src=>src.Category.CategoryName)).ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<User, userDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<User, UserLoginDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();


        }
    }
}
