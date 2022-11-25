using BlApi;
using Dal;
namespace BlImplementation;

internal class Order : IOrder 
{
    private DalApi.IDal Dal = new DalList();

    #region GET

    public IEnumerable<BO.OrderForList> GetAll()
    {
        List<DO.Order> dalOrders = (List<DO.Order>)Dal.Order.GetAll();
        List<BO.OrderForList> blOrders = new List<BO.OrderForList>();

        foreach (DO.Order order in dalOrders)
        {
            double totalPrice = 0;
            //figuring order status
            BO.OrderStatus status = BO.OrderStatus.Confirmed;
            if (order.ShipDate != DateTime.MinValue) status = BO.OrderStatus.Shipped;
            if (order.DeliveryDate != DateTime.MinValue) status = BO.OrderStatus.Delivered;

            //figuring order total price
            List<DO.OrderItem> orderItems = (List<DO.OrderItem>)Dal.OrderItem.GetAllItemsInOrder(order.ID);
            foreach (var item in orderItems) totalPrice += item.Price * item.Amount;

            blOrders.Add(new BO.OrderForList()
            {
                ID = order.ID,
                CustomerName = order.CustomerName,
                Status = status,
                AmountOfItems =  orderItems.Count,
                TotalPrice = totalPrice
            });
        }
        return blOrders;
    }

    public BO.Order Get(int id)
    {
        if (id < 0) throw new BO.NegativeNumberException("order ID property cannot be a negative number");
        try
        {
            DO.Order dalOrder = Dal.Order.Get(id);
            List<DO.OrderItem> dalOrderItems = (List<DO.OrderItem>)Dal.OrderItem.GetAllItemsInOrder(dalOrder.ID);

            //creating the orderItem list for the order and figuring order total price 
            List<BO.OrderItem> blOrderItems = new();
            double totalPrice = 0;
            foreach (var item in dalOrderItems) {
                DO.Product product = Dal.Product.Get(item.ProductId);
                totalPrice += item.Price * item.Amount;
                blOrderItems.Add(new BO.OrderItem()
                {
                    ID = item.ID,
                    Name = product.Name,
                    ProductId = item.ProductId,
                    Price = item.Price,
                    Amount = item.Amount,
                    TotalPrice = item.Amount * item.Price,
                });
            };
 
            //figuring order status
            BO.OrderStatus status = BO.OrderStatus.Confirmed;
            if (dalOrder.ShipDate != DateTime.MinValue) status = BO.OrderStatus.Shipped;
            if (dalOrder.DeliveryDate != DateTime.MinValue) status = BO.OrderStatus.Delivered;

            BO.Order blOrder = new BO.Order()
            {
                ID = dalOrder.ID,
                CustomerName = dalOrder.CustomerName,
                CustomerEmail = dalOrder.CustomerEmail,
                CustomerAdress = dalOrder.CustomerAdress,
                OrderDate = dalOrder.OrderDate,
                Status = status, 
                ShipDate = dalOrder.ShipDate,
                DeliveryDate = dalOrder.DeliveryDate,
                Items = blOrderItems, 
                TotalPrice = totalPrice,
            };
        

            return blOrder;
        }
        catch (DO.NotFoundException e)
        {
            throw new BO.NotFoundException(e.Message, e);
        }
    }
    #endregion

    #region UPDATE

    public BO.Order ShipOrder(int id, DateTime shipDate)
    {
        try
        {
            DO.Order dalOrder = Dal.Order.Get(id);

            if (shipDate > DateTime.Now)
                throw new BO.IntegrityDamageException("cannot set order ship date to a future date");

            if (dalOrder.ShipDate != DateTime.MinValue)
                throw new BO.IntegrityDamageException("cannot set order ship date, order already shipped");

            if(shipDate < dalOrder.OrderDate)
                throw new BO.IntegrityDamageException("cannot set order ship date to before order creating date");

            if (shipDate == DateTime.MinValue)
                throw new BO.NullValueException("ship date cannot be null - 1.1.1");

            dalOrder.ShipDate = shipDate;
            Dal.Order.Update(dalOrder);

            return Get(id);
        }
        catch (DO.NotFoundException e)
        {
            throw new BO.NotFoundException("Order not found", e);
        }
    }

    public BO.Order DeliverOrder(int id,DateTime deliveryDate)
    {
        try
        {
            DO.Order dalOrder = Dal.Order.Get(id);

            if (deliveryDate > DateTime.Now)
                throw new BO.IntegrityDamageException("cannot set order delivery date to a future date");

            if(dalOrder.ShipDate == DateTime.MinValue)
                throw new BO.IntegrityDamageException("cannot set order delivery date before setting order shipping date");

            if (dalOrder.DeliveryDate != DateTime.MinValue)
                throw new BO.IntegrityDamageException("cannot set order delivery date, order already delivered");

            if (deliveryDate < dalOrder.ShipDate)
                throw new BO.IntegrityDamageException("cannot set order delivery date to before order shipping date");

            if (deliveryDate == DateTime.MinValue)
                throw new BO.NullValueException("delivery date cannot be null - 1.1.1");

            dalOrder.DeliveryDate = deliveryDate;
            Dal.Order.Update(dalOrder);

            return Get(id);
        }
        catch (DO.NotFoundException e)
        {
            throw new BO.NotFoundException("Order not found", e);
        }
    }

    public void UpdateOrder(BO.Order order)
    {
        // fix this

        try
        {
            // delivery date:

            if (order.DeliveryDate > DateTime.Now)
                throw new BO.IntegrityDamageException("cannot set order delivery date to a future date");

            if (order.ShipDate == DateTime.MinValue)
                throw new BO.IntegrityDamageException("cannot set order delivery date before setting order shipping date");

            if (order.DeliveryDate != DateTime.MinValue)
                throw new BO.IntegrityDamageException("cannot set order delivery date, order already delivered");

            if (order.DeliveryDate == DateTime.MinValue)
                throw new BO.NullValueException("delivery date cannot be null - 1.1.1");


            // shipping date:
            if (order.ShipDate > DateTime.Now)
                throw new BO.IntegrityDamageException("cannot set order shipping date to a future date");

            if (order.ShipDate != DateTime.MinValue)
                throw new BO.IntegrityDamageException("cannot set order shipping date, order already delivered");

            if (order.ShipDate == DateTime.MinValue)
                throw new BO.NullValueException("shipping date cannot be null - 1.1.1");


        }
        catch (DO.NotFoundException e)
        {
            throw new BO.NotFoundException("Order not found", e);
        }
    }
    #endregion

    #region TRACK

    public BO.OrderTracking TrackOrder(int id)
    {
        BO.OrderTracking orderTracking;
        try
        {
            DO.Order dalOrder = Dal.Order.Get(id);

            //figuring order status
            BO.OrderStatus status = BO.OrderStatus.Confirmed;
            if (dalOrder.ShipDate != DateTime.MinValue) status = BO.OrderStatus.Shipped;
            if (dalOrder.DeliveryDate != DateTime.MinValue) status = BO.OrderStatus.Delivered;

            //figuring the trackingList
            List<(DateTime, string)> tracking = new();
            tracking.Add((dalOrder.OrderDate, "order confirmed"));

            if(dalOrder.ShipDate != DateTime.MinValue)
                tracking.Add((dalOrder.ShipDate, "order shipped"));

            if (dalOrder.DeliveryDate != DateTime.MinValue)
                tracking.Add((dalOrder.DeliveryDate, "order delivered"));

            orderTracking = new BO.OrderTracking()
            {
                ID = dalOrder.ID,
                Status =  status,
                TrackingList = tracking,
            };
            return orderTracking;
        }
        catch (DO.NotFoundException e)
        {
            throw new BO.NotFoundException("order not found", e);
        }
    }
    #endregion
}