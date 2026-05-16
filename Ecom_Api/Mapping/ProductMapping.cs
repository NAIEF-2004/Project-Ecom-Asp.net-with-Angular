using AutoMapper;
using Ecom_Core.DTO;
using Ecom_Core.Entites.Prudact;

namespace Ecom_Api.Mapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            // Map Product to ProductDTO - extract category name
            CreateMap<Product, ProductDTO>()
                .ForMember(x => x.Categoryname, op => op.MapFrom(s => s.category.Name))
                .ForMember(x => x.Photos, op => op.MapFrom(s => s.photos));
            
            CreateMap<Photo, PhotoDTO>().ReverseMap();
            
            CreateMap<AddProductDTO, Product>()
                .ForMember(x => x.photos, op => op.Ignore())
                .ReverseMap();
            
            CreateMap<UpdateProductDTO, Product>()
                .ForMember(x => x.photos, op => op.Ignore())
                .ReverseMap();
        }
    }
}
