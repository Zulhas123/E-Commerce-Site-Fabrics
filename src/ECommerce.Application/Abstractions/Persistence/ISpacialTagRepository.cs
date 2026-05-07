using E_Commerce_System.Models;

namespace ECommerce.Application.Abstractions.Persistence;

public interface ISpacialTagRepository
{
    Task<IReadOnlyList<SpacialTag>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<SpacialTag?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(SpacialTag tag, CancellationToken cancellationToken = default);
    Task UpdateAsync(SpacialTag tag, CancellationToken cancellationToken = default);
    Task DeleteAsync(SpacialTag tag, CancellationToken cancellationToken = default);
}

