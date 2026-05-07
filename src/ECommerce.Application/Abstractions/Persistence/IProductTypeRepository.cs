using E_Commerce_System.Models;

namespace ECommerce.Application.Abstractions.Persistence;

public interface IProductTypeRepository
{
    Task<IReadOnlyList<ProductTypes>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ProductTypes?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(ProductTypes productType, CancellationToken cancellationToken = default);
    Task UpdateAsync(ProductTypes productType, CancellationToken cancellationToken = default);
    Task DeleteAsync(ProductTypes productType, CancellationToken cancellationToken = default);
}

