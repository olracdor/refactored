using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Product.Entities;
using Domain.Product.Repository;
using RefactorThis.Common.Exceptions;
using RefactorThis.Dto;

namespace RefactorThis.Providers.Implementors
{
    public class ProductProvider : IProductProvider
    {
        IMapper _mapper;
        IProductRepository _productRepository;
        public ProductProvider(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public void Delete(Guid id)
        {
            var e = _productRepository.Get(id);
            _productRepository.Delete(e);
        }

        public List<ProductDto> Get()
        {
            return  _mapper.Map<List<ProductDto>>(_productRepository.Get());
        }

        public ProductDto Get(Guid id)
        {
            return _mapper.Map<ProductDto>(_productRepository.Get(id));
        }

        public ProductDto Save(ProductDto entity)
        {
            var e = _mapper.Map<Product>(entity);
            _productRepository.Save(e);
            return entity;
        }

        public ProductDto Update(Guid id, ProductDto product)
        {
            var entity = _productRepository.Get(id);
            if (entity == null)
                throw new BadRequestException($"Failed to update - record {id} not found");

            entity.Name = product.Name;
            entity.Price = product.Price;
            entity.DeliveryPrice = product.DeliveryPrice;
            entity.Description = product.Description;

            _productRepository.Update(entity);

            return product;
        }
    }
}
