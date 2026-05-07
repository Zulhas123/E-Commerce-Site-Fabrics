using ECommerce.Application.Abstractions.Persistence;
using ECommerce.Application.Abstractions.Services;
using E_Commerce_System.Models;

namespace ECommerce.Application.Services;

internal sealed class ProductTypeService(IProductTypeRepository productTypeRepository) : IProductTypeService
{
    public Task<IReadOnlyList<ProductTypes>> GetAllAsync(CancellationToken cancellationToken = default) =>
        productTypeRepository.GetAllAsync(cancellationToken);

    public Task<ProductTypes?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        productTypeRepository.GetByIdAsync(id, cancellationToken);

    public Task CreateAsync(ProductTypes productType, CancellationToken cancellationToken = default) =>
        productTypeRepository.AddAsync(productType, cancellationToken);

    public Task UpdateAsync(ProductTypes productType, CancellationToken cancellationToken = default) =>
        productTypeRepository.UpdateAsync(productType, cancellationToken);

    public async Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var existing = await productTypeRepository.GetByIdAsync(id, cancellationToken);
        if (existing is null) return;
        await productTypeRepository.DeleteAsync(existing, cancellationToken);
    }
}

