using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;

namespace Application.Features.Brands.Rules;

public class BrandBusinessRules
{
    private readonly IBrandRepository _brandRepository;

    public BrandBusinessRules(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task BrandNameCannotBeDuplicatedInserted(string name)
    {
        var result = await _brandRepository.GetListAsync(brand => brand.Name == name);
        if (result.Items.Any())
        {
            throw new BusinessException("Brand name already exists");
        }
    }
}