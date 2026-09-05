using AutoMapper;
using CleanArch.Application.DTOs;
using CleanArch.Application.Features.Products.Commands;

namespace CleanArch.Application.Mappings
{
    public class DTOToCommandMappingProfile : Profile
    {
        public DTOToCommandMappingProfile()
        {
            CreateMap<ProductDTO, ProductCreateCommand>();
            CreateMap<ProductDTO, ProductUpdateCommand>();
        }
    }
}
