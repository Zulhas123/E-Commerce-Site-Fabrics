using ECommerce.Application.Abstractions.Persistence;
using ECommerce.Application.Abstractions.Services;
using E_Commerce_System.Models;

namespace ECommerce.Application.Services;

internal sealed class SpacialTagService(ISpacialTagRepository tagRepository) : ISpacialTagService
{
    public Task<IReadOnlyList<SpacialTag>> GetAllAsync(CancellationToken cancellationToken = default) =>
        tagRepository.GetAllAsync(cancellationToken);

    public Task<SpacialTag?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        tagRepository.GetByIdAsync(id, cancellationToken);

    public Task CreateAsync(SpacialTag tag, CancellationToken cancellationToken = default) =>
        tagRepository.AddAsync(tag, cancellationToken);

    public Task UpdateAsync(SpacialTag tag, CancellationToken cancellationToken = default) =>
        tagRepository.UpdateAsync(tag, cancellationToken);

    public async Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var existing = await tagRepository.GetByIdAsync(id, cancellationToken);
        if (existing is null) return;
        await tagRepository.DeleteAsync(existing, cancellationToken);
    }
}

