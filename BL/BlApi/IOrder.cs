
using BO;

namespace BlApi;

public interface IOrder
{
    public IEnumerable<OrderForList> GetAll();
    public Order Get(int id);

}
