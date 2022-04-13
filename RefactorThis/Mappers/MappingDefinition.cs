using System.Collections.Generic;
using AutoMapper;
using Domain.Product.Entities;
using RefactorThis.Dto;

namespace RefactorThis.Mappers
{
    public class MappingDefinition : Profile
    {
        public MappingDefinition()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductOption, ProductOptionDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<ProductOptionDto, ProductOption>();
            CreateMap<List<ProductDto>, List<Product>>();
            CreateMap<List<ProductOptionDto>, List<ProductOption>>();
            CreateMap<List<Product>, List<ProductDto>>();
            CreateMap<List<ProductOption>, List<ProductOptionDto>>();

        }
    }
}
