using ECommerce.Application.Abstractions.Persistence;
using E_Commerce_System.Data;
using E_Commerce_System.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence.Repositories;

internal sealed class SpacialTagRepository(ApplicationDbContext db) : ISpacialTagRepository
{
    public async Task<IReadOnlyList<SpacialTag>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await db.spacialTags.AsNoTracking().ToListAsync(cancellationToken);

    public Task<SpacialTag?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        db.spacialTags.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

    public async Task AddAsync(SpacialTag tag, CancellationToken cancellationToken = default)
    {
        await db.spacialTags.AddAsync(tag, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(SpacialTag tag, CancellationToken cancellationToken = default)
    {
        db.spacialTags.Update(tag);
        await db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(SpacialTag tag, CancellationToken cancellationToken = default)
    {
        db.spacialTags.Remove(tag);
        await db.SaveChangesAsync(cancellationToken);
    }
}

