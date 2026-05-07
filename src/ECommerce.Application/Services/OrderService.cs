using ECommerce.Application.Abstractions.Persistence;
using ECommerce.Application.Abstractions.Services;
using E_Commerce_System.Models;

namespace ECommerce.Application.Services;

internal sealed class OrderService(IOrderRepository orderRepository) : IOrderService
{
    public async Task<string> GetNextOrderNoAsync(CancellationToken cancellationToken = default)
    {
        var count = await orderRepository.GetOrderCountAsync(cancellationToken);
        return (count + 1).ToString("000");
    }

    public async Task CreateOrderAsync(Order order, IEnumerable<int> productIds, CancellationToken cancellationToken = default)
    {
        foreach (var productId in productIds)
        {
            order.OrderDetails.Add(new OrderDetails { ProductId = productId });
        }

        order.OrderNo = await GetNextOrderNoAsync(cancellationToken);
        await orderRepository.AddAsync(order, cancellationToken);
    }
}

