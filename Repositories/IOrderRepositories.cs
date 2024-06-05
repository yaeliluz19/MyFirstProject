using MyFirstProject;

namespace Repositories
{
    public interface IOrderRepositories
    {
        Task<Order> CreateOrder(Order order);
    }
}