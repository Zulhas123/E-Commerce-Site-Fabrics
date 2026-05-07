using ECommerce.Application.Abstractions.Persistence;
using ECommerce.Application.Abstractions.Services;
using E_Commerce_System.Models;

namespace ECommerce.Application.Services;

internal sealed class ProductService(IProductRepository productRepository) : IProductService
{
    public Task<IReadOnlyList<Products>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default) =>
        productRepository.GetAllWithDetailsAsync(cancellationToken);

    public Task<IReadOnlyList<Products>> GetByPriceRangeWithDetailsAsync(decimal? lowAmount, decimal? highAmount, CancellationToken cancellationToken = default) =>
        productRepository.GetByPriceRangeWithDetailsAsync(lowAmount, highAmount, cancellationToken);

    public Task<Products?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default) =>
        productRepository.GetByIdWithDetailsAsync(id, cancellationToken);

    public Task<Products?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        productRepository.GetByIdAsync(id, cancellationToken);

    public Task<bool> ExistsByNameAsync(string name, int? excludingId = null, CancellationToken cancellationToken = default) =>
        productRepository.ExistsByNameAsync(name, excludingId, cancellationToken);

    public Task CreateAsync(Products product, CancellationToken cancellationToken = default) =>
        productRepository.AddAsync(product, cancellationToken);

    public Task UpdateAsync(Products product, CancellationToken cancellationToken = default) =>
        productRepository.UpdateAsync(product, cancellationToken);

    public async Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var existing = await productRepository.GetByIdAsync(id, cancellationToken);
        if (existing is null) return;
        await productRepository.DeleteAsync(existing, cancellationToken);
    }
}

