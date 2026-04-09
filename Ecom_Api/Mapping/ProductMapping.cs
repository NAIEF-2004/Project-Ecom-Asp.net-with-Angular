using AutoMapper;
using Ecom_Core.DTO;
using Ecom_Core.Entites.Prudact;

namespace Ecom_Api.Mapping
{
    public class ProductMapping:Profile
    {
        public ProductMapping()
        {
            //عملت هلحركات لان في عنصر ضمن ال dto غير موجود مي النسخة الام وهو categoryname
            CreateMap<Product, ProductDTO>().ForMember(x => x.Categoryname, op => op.MapFrom(s => s.category.Name)).ReverseMap();
            CreateMap<Photo,PhotoDTO>().ReverseMap();
            CreateMap<AddProductDTO, Product>().ForMember(x => x.photos, op => op.Ignore()).ReverseMap();
            CreateMap<UpdateProductDTO, Product>().ForMember(x => x.photos, op => op.Ignore()).ReverseMap();
        }
    }
}
