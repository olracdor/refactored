using System;
using System.Collections.Generic;
using AutoMapper;
using Domain.Product.Entities;
using Domain.Product.Repository;
using RefactorThis.Common.Exceptions;
using RefactorThis.Dto;

namespace RefactorThis.Providers.Implementors
{
    public class ProductOptionProvider : IProductOptionProvider
    {
        IMapper _mapper;
        private IProductOptionRepository _productOptionRepository;
        public ProductOptionProvider(IMapper mapper, IProductOptionRepository productOptionRepository)
        {
            _mapper = mapper;
            _productOptionRepository = productOptionRepository;
        }

        public void Delete(Guid productId, Guid id)
        {
            var entity = _productOptionRepository.GetByProductIdAndId(productId, id);
            _productOptionRepository.Delete(entity);
        }

        public List<ProductOptionDto> Get(Guid Id)
        {
            var entity = _productOptionRepository.Get(Id);
            var options = _mapper.Map<List<ProductOptionDto>>(entity);
            return options;
        }

        public List<ProductOptionDto> Get()
        {
            var entity = _productOptionRepository.Get();
            var options = _mapper.Map<List<ProductOptionDto>>(entity);
            return options;
        }

        public List<ProductOptionDto> GetByProductId(Guid Id)
        {
            var entity = _productOptionRepository.GetByProductId(Id);
            var options = _mapper.Map<List<ProductOptionDto>>(entity);
            return options;
        }

        public ProductOptionDto GetByProductIdAndId(Guid productId, Guid id)
        {
            var entity =  _productOptionRepository.GetByProductIdAndId(productId, id);
            var option = _mapper.Map<ProductOptionDto>(entity);
            return option;
        }

        public ProductOptionDto Save(Guid productId, ProductOptionDto entity)
        {
            var e = _mapper.Map<ProductOption>(entity);
            e.ProductId = productId;
            _productOptionRepository.Save(e);
            return entity;
        }

        public ProductOptionDto Update(Guid productid, Guid id, ProductOptionDto option)
        {
            var entity = _productOptionRepository.GetByProductIdAndId(productid, id);
            if (entity == null)
                throw new BadRequestException($"Failed to update - record {id} not found");

            entity.Description = option.Description;
            entity.Name = option.Name;

            _productOptionRepository.Update(entity);

            return option;
        }
    }
}
