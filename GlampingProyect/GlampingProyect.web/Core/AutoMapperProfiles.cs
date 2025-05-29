using AutoMapper;
using GlampingProyect.Web.Data.Entities;
using GlampingProyect.Web.DTOs;

namespace GlampingProyect.Web.Core
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Client, ClientDTO>().ReverseMap();
            CreateMap<Invoice, InvoiceDTO>().ReverseMap();
            CreateMap<Sale, SaleDTO>().ReverseMap();
            CreateMap<SaleDetail, SaleDetailDTO>().ReverseMap();
            CreateMap<ProductCategory, ProductCategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<PrivateURole, PrivateURoleDTO>().ReverseMap();
            CreateMap<Permission, PermissionDTO>().ReverseMap();
            CreateMap<Users, UsersDTO>();
            CreateMap<UsersDTO, Users>().ForMember(users => users.UserName, config => config.MapFrom(dto => dto.Email));
            CreateMap<Users, AccountUserDTO>().ForMember(dest => dest.Photo, options => options.Ignore())
                                             .ForMember(dest => dest.PhotoUrl, config => config.MapFrom(src => src.Photo));

            CreateMap<AccountUserDTO, Users>().ForMember(dest => dest.Photo, options => options.Ignore())
                                             .ForMember(user => user.UserName, config => config.MapFrom(dto => dto.Email));
        }
    }
}
