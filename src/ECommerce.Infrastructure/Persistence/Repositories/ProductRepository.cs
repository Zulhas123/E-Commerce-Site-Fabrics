using ECommerce.Application.Abstractions.Persistence;
using E_Commerce_System.Data;
using E_Commerce_System.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence.Repositories;

internal sealed class ProductRepository(ApplicationDbContext db) : IProductRepository
{
    public async Task<IReadOnlyList<Products>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default) =>
        await db.Products
            .AsNoTracking()
            .Include(p => p.productType)
            .Include(p => p.SpacialTag)
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Products>> GetByPriceRangeWithDetailsAsync(decimal? lowAmount, decimal? highAmount, CancellationToken cancellationToken = default)
    {
        var query = db.Products
            .AsNoTracking()
            .Include(p => p.productType)
            .Include(p => p.SpacialTag)
            .AsQueryable();

        if (lowAmount is not null && highAmount is not null)
        {
            query = query.Where(p => p.Price >= lowAmount && p.Price <= highAmount);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public Task<Products?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default) =>
        db.Products
            .AsNoTracking()
            .Include(p => p.productType)
            .Include(p => p.SpacialTag)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public Task<Products?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        db.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public Task<bool> ExistsByNameAsync(string name, int? excludingId = null, CancellationToken cancellationToken = default)
    {
        var query = db.Products.AsNoTracking().Where(p => p.Name == name);
        if (excludingId is not null)
        {
            query = query.Where(p => p.Id != excludingId);
        }

        return query.AnyAsync(cancellationToken);
    }

    public async Task AddAsync(Products product, CancellationToken cancellationToken = default)
    {
        await db.Products.AddAsync(product, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Products product, CancellationToken cancellationToken = default)
    {
        db.Products.Update(product);
        await db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Products product, CancellationToken cancellationToken = default)
    {
        db.Products.Remove(product);
        await db.SaveChangesAsync(cancellationToken);
    }
}

