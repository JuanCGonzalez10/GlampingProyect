﻿using AutoMapper;
using GlampingProyect.Web.Data.Entities;
using GlampingProyect.Web.Data.Entities;
using GlampingProyect.Web.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GlampingProyect.Web.Core
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Configuración para mapear entre la entidad Section y la DTO SectionDTO
            CreateMap<Category, CategoryDTO>().ReverseMap();

            // Configuración para mapear entre la entidad Blog y la DTO BlogDTO
            CreateMap<Glamping, GlampingDTO>().ReverseMap();
        }
    }
}
