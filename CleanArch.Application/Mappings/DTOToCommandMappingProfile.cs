using AutoMapper;
using CleanArch.Application.DTOs;
using CleanArch.Application.Features.Products.Commands;

namespace CleanArch.Application.Mappings
{
    public class DtoToCommandMappingProfile : Profile
    {
        public DtoToCommandMappingProfile()
        {
            CreateMap<ProductDto, ProductCreateCommand>();
            CreateMap<ProductDto, ProductUpdateCommand>();
        }
    }
}
