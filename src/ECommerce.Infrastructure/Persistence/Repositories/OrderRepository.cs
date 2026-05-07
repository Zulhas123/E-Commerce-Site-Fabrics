using ECommerce.Application.Abstractions.Persistence;
using E_Commerce_System.Data;
using E_Commerce_System.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence.Repositories;

internal sealed class OrderRepository(ApplicationDbContext db) : IOrderRepository
{
    public async Task AddAsync(Order order, CancellationToken cancellationToken = default)
    {
        await db.Order.AddAsync(order, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
    }

    public Task<int> GetOrderCountAsync(CancellationToken cancellationToken = default) =>
        db.Order.AsNoTracking().CountAsync(cancellationToken);
}

