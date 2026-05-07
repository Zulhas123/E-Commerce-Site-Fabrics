using E_Commerce_System.Models;

namespace ECommerce.Application.Abstractions.Services;

public interface IProductService
{
    Task<IReadOnlyList<Products>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Products>> GetByPriceRangeWithDetailsAsync(decimal? lowAmount, decimal? highAmount, CancellationToken cancellationToken = default);
    Task<Products?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default);
    Task<Products?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByNameAsync(string name, int? excludingId = null, CancellationToken cancellationToken = default);
    Task CreateAsync(Products product, CancellationToken cancellationToken = default);
    Task UpdateAsync(Products product, CancellationToken cancellationToken = default);
    Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default);
}

