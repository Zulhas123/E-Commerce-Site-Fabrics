using E_Commerce_System.Models;

namespace ECommerce.Application.Abstractions.Services;

public interface IProductTypeService
{
    Task<IReadOnlyList<ProductTypes>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ProductTypes?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task CreateAsync(ProductTypes productType, CancellationToken cancellationToken = default);
    Task UpdateAsync(ProductTypes productType, CancellationToken cancellationToken = default);
    Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default);
}

