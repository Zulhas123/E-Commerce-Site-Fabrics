using E_Commerce_System.Models;

namespace ECommerce.Application.Abstractions.Services;

public interface ISpacialTagService
{
    Task<IReadOnlyList<SpacialTag>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<SpacialTag?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task CreateAsync(SpacialTag tag, CancellationToken cancellationToken = default);
    Task UpdateAsync(SpacialTag tag, CancellationToken cancellationToken = default);
    Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default);
}

