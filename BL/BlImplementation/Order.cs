using BlApi;
using DO;

namespace BlImplementation;

internal class Order : IOrder 
{
    private DalApi.IDal? dal = DalApi.Factory.Get();

    #region GET

    public IEnumerable<BO.OrderForList?> GetAll()
    {
        List<DO.Order?> dalOrders = (List<DO.Order?>)(dal?.Order.GetAll(null) ?? throw new BO.AccessToDataFailedException("cannot access the data layer"));
        List<BO.OrderForList?> blOrders = new List<BO.OrderForList?>();

        foreach (DO.Order? order in dalOrders)
        {
            //figuring order status
            BO.OrderStatus status = BO.OrderStatus.Confirmed;
            if (order?.ShipDate != null) status = BO.OrderStatus.Shipped;
            if (order?.DeliveryDate != null) status = BO.OrderStatus.Delivered;

            //figuring order total price
            List<DO.OrderItem?> orderItems = (List<DO.OrderItem?>)dal.OrderItem.GetAll(o=>o?.OrderId == order?.ID);


            double totalPrice = 0;

            totalPrice =
                from item in orderItems
                where item != null
                select (double)(item?.Price * item?.Amount).Sum();

           // foreach (var item in orderItems) 
             //   totalPrice += item != null ? (double)(item?.Price * item?.Amount)! : 0;

            blOrders.Add(order != null ? Tools.Copy(order,new BO.OrderForList()
            {
                Status = status,
                AmountOfItems = orderItems.Count,
                TotalPrice = totalPrice
            }) : null);
        }
        return blOrders;
    }

    public BO.Order Get(int id)
    {
        if (id < 0) throw new BO.NegativeNumberException("order ID property cannot be a negative number");
        try
        {
            DO.Order dalOrder = dal?.Order.Get(o => o?.ID == id) ?? throw new BO.AccessToDataFailedException("cannot access the data layer"); 
            List<DO.OrderItem> dalOrderItems = (List<DO.OrderItem>)dal.OrderItem.GetAll(oi => oi?.OrderId == dalOrder.ID);

            //creating the orderItem list for the order and figuring order total price 
            List<BO.OrderItem> blOrderItems = new();
            double totalPrice = 0;
            foreach (var item in dalOrderItems) {
                DO.Product product = dal.Product.Get(p => p?.ID == item.ProductId);
                totalPrice +=  item.Price * item.Amount;
                blOrderItems.Add(Tools.Copy(item,new BO.OrderItem()
                {
                    Name = product.Name,
                    TotalPrice = item.Amount * item.Price,
                }));
                 
            };
 
            //figuring order status
            BO.OrderStatus status = BO.OrderStatus.Confirmed;
            if (dalOrder.ShipDate != null) status = BO.OrderStatus.Shipped;
            if (dalOrder.DeliveryDate != null) status = BO.OrderStatus.Delivered;

            return Tools.Copy(dalOrder, new BO.Order()
            {
                Status = status,
                Items = blOrderItems,
                TotalPrice = totalPrice,
            });
        }
        catch (DO.NotFoundException e)
        {
            throw new BO.NotFoundException(e.Message, e);
        }
    }
    #endregion

    #region UPDATE

    public BO.Order ShipOrder(int id, DateTime? shipDate)
    {
        try
        {
            DO.Order dalOrder = dal?.Order.Get(o => o?.ID == id) ?? throw new BO.AccessToDataFailedException("cannot access the data layer");

            if (shipDate > DateTime.Now)
                throw new BO.IntegrityDamageException("cannot set order ship date to a future date");

            if (dalOrder.ShipDate != null)
                throw new BO.IntegrityDamageException("cannot set order ship date, order already shipped");

            if(shipDate < dalOrder.OrderDate)
                throw new BO.IntegrityDamageException("cannot set order ship date to before order creating date");

            if (shipDate == null)
                throw new BO.NullValueException("ship date cannot be null");

            dalOrder.ShipDate = shipDate;
            dal.Order.Update(dalOrder);

            return Get(id);
        }
        catch (DO.NotFoundException e)
        {
            throw new BO.NotFoundException("Order not found", e);
        }
    }

    public BO.Order DeliverOrder(int id,DateTime? deliveryDate)
    {
        try
        {
            DO.Order dalOrder = dal?.Order.Get(o => o?.ID == id) ?? throw new BO.AccessToDataFailedException("cannot access the data layer");

            if (deliveryDate > DateTime.Now)
                throw new BO.IntegrityDamageException("cannot set order delivery date to a future date");

            if(dalOrder.ShipDate == null)
                throw new BO.IntegrityDamageException("cannot set order delivery date before setting order shipping date");

            if (dalOrder.DeliveryDate != null)
                throw new BO.IntegrityDamageException("cannot set order delivery date, order already delivered");

            if (deliveryDate < dalOrder.ShipDate)
                throw new BO.IntegrityDamageException("cannot set order delivery date to before order shipping date");

            if (deliveryDate == null)
                throw new BO.NullValueException("delivery date cannot be null");

            dalOrder.DeliveryDate = deliveryDate;
            dal.Order.Update(dalOrder);

            return Get(id);
        }
        catch (DO.NotFoundException e)
        {
            throw new BO.NotFoundException("Order not found", e);
        }
    }

    public void UpdateOrder(BO.Order order)
    {
        
        try
        {
            DO.Order dalOrder = dal?.Order.Get(o => o?.ID == order.ID) ?? throw new BO.AccessToDataFailedException("cannot access the data layer");
            if (dalOrder.ShipDate != null)
                throw new BO.IntegrityDamageException("cannot change an order after it was shipped");
            order.Items ??= new();
            foreach (var item in order.Items)//for each item - validate it,calculate the new number in stock and then save it in the database
            {
                if (item.Amount < 0)
                    throw new BO.NegativeNumberException("item amount property must be a positive number");

                DO.OrderItem preOrderItem = dal.OrderItem.Get(oi => oi?.OrderId == order.ID && oi?.ProductId == item.ProductId);
                DO.Product product = dal.Product.Get(p => p?.ID == item.ProductId);
                if (preOrderItem.Amount > item.Amount)//decreasing the number of item to purchase
                {
                    product.InStock += preOrderItem.Amount - item.Amount;//increasing the number of products in stock 
                    dal.Product.Update(product);
                    if (item.Amount == 0)//deleting the item
                    {
                        dal.OrderItem.Delete(preOrderItem.ID);
                    }
                    else //updating the new amount
                    {
                        preOrderItem.Amount = item.Amount;
                        dal.OrderItem.Update(preOrderItem);
                    }
                }
                if (preOrderItem.Amount < item.Amount)//increasing the number of item to purchase
                {
                    if (product.InStock < item.Amount - preOrderItem.Amount)//the addition to the amount of items is more than the amount in stock
                        throw new BO.OutOfStockException("product " + product.ID + " is out of stock");
                    product.InStock -= item.Amount - preOrderItem.Amount;//decreasing the number of products in stock 
                    dal.Product.Update(product);
                    preOrderItem.Amount = item.Amount;//setting the item new amount
                    dal.OrderItem.Update(preOrderItem);
                }
            }
            
        }
        catch (DO.NotFoundException e)
        {
            throw new BO.NotFoundException(e.Message, e);
        }
    }
    #endregion

    #region TRACK

    public BO.OrderTracking TrackOrder(int id)
    {
        BO.OrderTracking orderTracking;
        try
        {
            DO.Order dalOrder = dal?.Order.Get(o => o?.ID == id) ?? throw new BO.AccessToDataFailedException("cannot access the data layer");

            //figuring order status
            BO.OrderStatus status = BO.OrderStatus.Confirmed;
            if (dalOrder.ShipDate != null) status = BO.OrderStatus.Shipped;
            if (dalOrder.DeliveryDate != null) status = BO.OrderStatus.Delivered;

            //figuring the trackingList
            List<(DateTime?, string?)> tracking = new();
            tracking.Add((dalOrder.OrderDate, "order confirmed"));

            if(dalOrder.ShipDate != null)
                tracking.Add((dalOrder.ShipDate, "order shipped"));

            if (dalOrder.DeliveryDate != null)
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