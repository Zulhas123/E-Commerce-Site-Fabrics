using E_Commerce_System.Models;

namespace ECommerce.Application.Abstractions.Services;

public interface IOrderService
{
    Task<string> GetNextOrderNoAsync(CancellationToken cancellationToken = default);
    Task CreateOrderAsync(Order order, IEnumerable<int> productIds, CancellationToken cancellationToken = default);
}

