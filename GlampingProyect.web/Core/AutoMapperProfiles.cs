using AutoMapper;
using GlampingProyect.web.Data.Entities;
using PrivateBlog.Web.Data.Entities;
using PrivateBlog.Web.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PrivateBlog.Web.Core
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Configuración para mapear entre la entidad Section y la DTO SectionDTO
            CreateMap<Section, SectionDTO>().ReverseMap();

            // Configuración para mapear entre la entidad Blog y la DTO BlogDTO
            CreateMap<Blog, BlogDTO>().ReverseMap();
        }
    }
}
