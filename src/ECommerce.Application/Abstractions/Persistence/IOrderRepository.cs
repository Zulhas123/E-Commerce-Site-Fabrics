using E_Commerce_System.Models;

namespace ECommerce.Application.Abstractions.Persistence;

public interface IOrderRepository
{
    Task AddAsync(Order order, CancellationToken cancellationToken = default);
    Task<int> GetOrderCountAsync(CancellationToken cancellationToken = default);
}

