using ECommerce.Application.Abstractions.Persistence;
using E_Commerce_System.Data;
using E_Commerce_System.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence.Repositories;

internal sealed class ProductTypeRepository(ApplicationDbContext db) : IProductTypeRepository
{
    public async Task<IReadOnlyList<ProductTypes>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await db.productTypes.AsNoTracking().ToListAsync(cancellationToken);

    public Task<ProductTypes?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        db.productTypes.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public async Task AddAsync(ProductTypes productType, CancellationToken cancellationToken = default)
    {
        await db.productTypes.AddAsync(productType, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ProductTypes productType, CancellationToken cancellationToken = default)
    {
        db.productTypes.Update(productType);
        await db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ProductTypes productType, CancellationToken cancellationToken = default)
    {
        db.productTypes.Remove(productType);
        await db.SaveChangesAsync(cancellationToken);
    }
}

