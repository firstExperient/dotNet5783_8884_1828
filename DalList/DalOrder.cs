
using DO;

namespace Dal;

public class DalOrder
{

    #region Add
    public int Add(Order order)
    {
        if (DataSource.Config.OrdersIndex == 99) throw new Exception("Erorr! Orders array is full");
        order.ID = DataSource.Config.OrderId;
        DataSource.Orders[DataSource.Config.OrdersIndex] = order;
        DataSource.Config.OrdersIndex++;
        return order.ID;
    }

    #endregion

    #region Get
    public Order Get(int id)
    {
        for (int i = 0; i < DataSource.Config.OrdersIndex; i++)
        {
            if (DataSource.Orders[i].ID == id) return DataSource.Orders[i];
        }
        throw new Exception("Order not found");
    }

    public Order[] GetAll()
    {
        Order[] orders = new Order[DataSource.Config.OrdersIndex];
        for (int i = 0; i < DataSource.Config.OrdersIndex; i++)
        {
            orders[i] = DataSource.Orders[i];
        }
        return orders;
    }

    #endregion

    #region Update

    public void Update(Order order)
    {
        bool flag = false;
        for (int i = 0; i < DataSource.Config.OrdersIndex; i++)
        {
            if (DataSource.Orders[i].ID == order.ID)
            {
                DataSource.Orders[i] = order;
                flag = true;
                break;
            }
        }
        if (!flag) throw new Exception("Order not found");
    }

    #endregion

    #region Delete

    public void Delete(int id)
    {
        bool flag = false;
        for (int i = 0; i < DataSource.Config.OrdersIndex; i++)
        {
            if (DataSource.Orders[i].ID == id)
            {
                flag = true;
                for (; i < DataSource.Config.OrdersIndex - 1; i++)
                {
                    DataSource.Orders[i] = DataSource.Orders[i + 1];
                }
                DataSource.Config.OrdersIndex--;
                break;
            }
        }
        if (!flag) throw new Exception("Order not found");
    }

    #endregion

}
