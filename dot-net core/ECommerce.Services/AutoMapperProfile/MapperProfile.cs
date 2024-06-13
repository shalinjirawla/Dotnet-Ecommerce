using AutoMapper;
using ECommerce.Models.Models;
using ECommerce.Services.DTO;

namespace ECommerce.Services.AutoMapperProfile
{
    public class MapperProfile:Profile
    {
        public MapperProfile() 
        {
            #region User
            CreateMap<AuthenticationDTO, User>();
            CreateMap<AddUserDTO, User>();
            #endregion

            #region Product
            CreateMap<AddProductDTO,Product>();
            CreateMap<UpdateProductDTO, Product>().ForMember(dest => dest.ProductCategories, opt => opt.MapFrom(src => src.ProductCategories.Select(id => new ProductCategory { Id = id })));
            #endregion

            #region CartItems
            CreateMap<AddCartItemDTO, CartItem>();
            CreateMap<UpdateCartItemDTO,CartItem>();
            #endregion

            #region Categories
            CreateMap<Product,ProductCategory>();
            CreateMap<UpdateProductDTO,ProductCategory>();
            #endregion
            #region Orders
            CreateMap<AddOrderDTO, Order>();
            #endregion

            #region ProductRatings
            CreateMap<AddProductRatingDTO, ProductRating>();
            CreateMap<UpdateProductRatingDTO, ProductRating>();
            #endregion

        }
    }
}
