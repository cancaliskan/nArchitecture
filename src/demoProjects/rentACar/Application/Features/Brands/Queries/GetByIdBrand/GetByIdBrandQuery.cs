﻿using Application.Features.Brands.Dtos;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Brands.Queries.GetByIdBrand;

public class GetByIdBrandQuery : IRequest<BrandGetByIdDto>
{
    public int Id { get; set; }

    public class GetByIdBrandQueryHandler : IRequestHandler<GetByIdBrandQuery, BrandGetByIdDto>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private readonly BrandBusinessRules _brandBusinessRules;

        public GetByIdBrandQueryHandler(IBrandRepository brandRepository, IMapper mapper, BrandBusinessRules brandBusinessRules)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _brandBusinessRules = brandBusinessRules;
        }

        public async Task<BrandGetByIdDto> Handle(GetByIdBrandQuery request, CancellationToken cancellationToken)
        {
            var brand = await _brandRepository.GetAsync(brand => brand.Id == request.Id);
            _brandBusinessRules.BrandShouldExistWhenRequestedById(brand);
            var mappedBrand = _mapper.Map<BrandGetByIdDto>(brand);
            return mappedBrand;
        }
    }
}