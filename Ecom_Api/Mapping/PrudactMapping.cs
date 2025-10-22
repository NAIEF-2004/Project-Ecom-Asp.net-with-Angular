using AutoMapper;
using Ecom_Core.DTO;
using Ecom_Core.Entites.Prudact;

namespace Ecom_Api.Mapping
{
    public class PrudactMapping:Profile
    {
        public PrudactMapping()
        {
            //عملت هلحركات لان في عنصر ضمن ال dto غير موجود مي النسخة الام وهو categoryname
            CreateMap<Prudact, PrudactDTO>().ForMember(x => x.Categoryname, op => op.MapFrom(s => s.category.Name)).ReverseMap();
            CreateMap<Photo,PhotoDTO>().ReverseMap();
            CreateMap<AddprudactDTO, Prudact>().ForMember(x => x.photos, op => op.Ignore()).ReverseMap();
        }
    }
}
