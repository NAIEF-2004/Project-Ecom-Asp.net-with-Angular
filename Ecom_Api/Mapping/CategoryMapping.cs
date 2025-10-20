using AutoMapper;
using Ecom_Core.DTO;
using Ecom_Core.Entites.Prudact;

namespace Ecom_Api.Mapping
{
    public class CategoryMapping:Profile
    {
        public CategoryMapping()
        {
            CreateMap<CategoryDTO,Category>().ReverseMap();
            CreateMap<updatecategoryDTO,Category>().ReverseMap();
        }
    }
}
