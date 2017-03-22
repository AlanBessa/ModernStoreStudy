using ModernStore.Domain.Entidades;

namespace ModernStore.Domain.Repositories
{
    public interface IOrderRepository
    {
        void Save(Order order);
    }
}
