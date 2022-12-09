﻿using BlApi;
using Dal;
namespace BlImplementation;

internal class Order : IOrder 
{
    private DalApi.IDal Dal = new DalList();

    #region GET

    public IEnumerable<BO.OrderForList?> GetAll()
    {
        List<DO.Order?> dalOrders = (List<DO.Order?>)Dal.Order.GetAll(null);
        List<BO.OrderForList?> blOrders = new List<BO.OrderForList?>();

        foreach (DO.Order? order in dalOrders)
        {
            if(order != null)
            {
                double totalPrice = 0;
                //figuring order status
                BO.OrderStatus status = BO.OrderStatus.Confirmed;
                if (order!.Value.ShipDate != null) status = BO.OrderStatus.Shipped;
                if (order!.Value.DeliveryDate != null) status = BO.OrderStatus.Delivered;

                //figuring order total price
                List<DO.OrderItem?> orderItems = (List<DO.OrderItem?>)Dal.OrderItem.GetAll(o=>o?.OrderId == order!.Value.ID);
                foreach (var item in orderItems) 
                    if(item != null)
                        totalPrice += item!.Value.Price * item!.Value.Amount;

                blOrders.Add(new BO.OrderForList()
                {
                    ID = order!.Value.ID,
                    CustomerName = order!.Value.CustomerName,
                    Status = status,
                    AmountOfItems = orderItems.Count,
                    TotalPrice = totalPrice
                });
            }
        }
        return blOrders;
    }

    public BO.Order Get(int id)
    {
        if (id < 0) throw new BO.NegativeNumberException("order ID property cannot be a negative number");
        try
        {
            DO.Order dalOrder = Dal.Order.Get(o => o?.ID == id);
            List<DO.OrderItem?> dalOrderItems = (List<DO.OrderItem?>)Dal.OrderItem.GetAll(oi => oi?.OrderId == dalOrder.ID);

            //creating the orderItem list for the order and figuring order total price 
            List<BO.OrderItem?> blOrderItems = new();
            double totalPrice = 0;
            foreach (var item in dalOrderItems) {
                if(item != null)
                {
                    DO.Product product = Dal.Product.Get(p => p?.ID == item!.Value.ProductId);
                    totalPrice += item!.Value.Price * item!.Value.Amount;
                    blOrderItems.Add(new BO.OrderItem()
                    {
                        ID = item!.Value.ID,
                        Name = product.Name,
                        ProductId = item!.Value.ProductId,
                        Price = item!.Value.Price,
                        Amount = item!.Value.Amount,
                        TotalPrice = item!.Value.Amount * item!.Value.Price,
                    });
                } 
            };
 
            //figuring order status
            BO.OrderStatus status = BO.OrderStatus.Confirmed;
            if (dalOrder.ShipDate != null) status = BO.OrderStatus.Shipped;
            if (dalOrder.DeliveryDate != null) status = BO.OrderStatus.Delivered;

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

    public BO.Order ShipOrder(int id, DateTime? shipDate)
    {
        try
        {
            DO.Order dalOrder = Dal.Order.Get(o => o?.ID == id);

            if (shipDate > DateTime.Now)
                throw new BO.IntegrityDamageException("cannot set order ship date to a future date");

            if (dalOrder.ShipDate != null)
                throw new BO.IntegrityDamageException("cannot set order ship date, order already shipped");

            if(shipDate < dalOrder.OrderDate)
                throw new BO.IntegrityDamageException("cannot set order ship date to before order creating date");

            if (shipDate == null)
                throw new BO.NullValueException("ship date cannot be null");

            dalOrder.ShipDate = shipDate;
            Dal.Order.Update(dalOrder);

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
            DO.Order dalOrder = Dal.Order.Get(o => o?.ID == id);

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
        
        try
        {
            DO.Order dalOrder = Dal.Order.Get(o => o?.ID == order.ID);
            if (dalOrder.ShipDate != null)
                throw new BO.IntegrityDamageException("cannot change an order after it was shipped");
            if(order.Items != null)
            {
                foreach (var item in order.Items)//for each item - validate it,calculate the new number in stock and then save it in the database
                {
                    if(item == null) continue;
                    if (item.Amount < 0)
                        throw new BO.NegativeNumberException("item amount property must be a positive number");

                    DO.OrderItem preOrderItem = Dal.OrderItem.Get(oi => oi?.OrderId == order.ID && oi?.ProductId == item.ProductId);
                    DO.Product product = Dal.Product.Get(p => p?.ID == item.ProductId);
                    if (preOrderItem.Amount > item.Amount)//decreasing the number of item to purchase
                    {
                        product.InStock += preOrderItem.Amount - item.Amount;//increasing the number of products in stock 
                        Dal.Product.Update(product);
                        if (item.Amount == 0)//deleting the item
                        {
                            Dal.OrderItem.Delete(preOrderItem.ID);
                        }
                        else //updating the new amount
                        {
                            preOrderItem.Amount = item.Amount;
                            Dal.OrderItem.Update(preOrderItem);
                        }
                    }
                    if (preOrderItem.Amount < item.Amount)//increasing the number of item to purchase
                    {
                        if (product.InStock < item.Amount - preOrderItem.Amount)//the addition to the amount of items is more than the amount in stock
                            throw new BO.OutOfStockException("product " + product.ID + " is out of stock");
                        product.InStock -= item.Amount - preOrderItem.Amount;//decreasing the number of products in stock 
                        Dal.Product.Update(product);
                        preOrderItem.Amount = item.Amount;//setting the item new amount
                        Dal.OrderItem.Update(preOrderItem);
                    }
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
            DO.Order dalOrder = Dal.Order.Get(o => o?.ID == id);

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