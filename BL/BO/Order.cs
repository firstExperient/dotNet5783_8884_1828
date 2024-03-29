﻿using DO;

namespace BO;

public class Order
{
    /// <summary>
    /// Unique ID of the order
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// the name of the customer who ordered the order
    /// </summary>
    public string? CustomerName { get; set; }

    /// <summary>
    /// the email of the customer who ordered the order
    /// </summary>
    public string? CustomerEmail { get; set; }

    /// <summary>
    /// the address of the customer, to know where to send the order to
    /// </summary>
    public string? CustomerAdress { get; set; }

    /// <summary>
    /// the date of the orderation
    /// </summary>
    public DateTime? OrderDate { get; set; }

    /// <summary>
    /// the status of the order
    /// </summary>
    public OrderStatus? Status { get; set; }
    

    /// <summary>
    /// the order shipping date
    /// </summary>
    public DateTime? ShipDate { get; set; }

    /// <summary>
    /// the order delivering date
    /// </summary>
    public DateTime? DeliveryDate { get; set; }

    /// <summary>
    /// the list of the items
    /// </summary>
    public IEnumerable<OrderItem>? Items { get; set; } = new List<OrderItem>();

    /// <summary>
    /// the order's total price
    /// </summary>
    public double TotalPrice { get; set; }

    /// <summary>
    /// a string of the order details
    /// </summary>
    public override string ToString()
    {
        string temp = "";
        if(Items != null)
            foreach (OrderItem? item in Items)
            {
                if(item != null)
                    temp = temp + "\n" + item;
            }
        return $@"
        Order ID: {ID}
        Customer name: {CustomerName}
        Customer email: {CustomerEmail}
        Customer address: {CustomerAdress}
        Order date: {(OrderDate.HasValue ? OrderDate.ToString() : "")}
        Order status: {Status}
        Ship date: {(ShipDate.HasValue ? ShipDate.Value.ToString() : "")}
        Delivery date: {(DeliveryDate.HasValue ? DeliveryDate.Value.ToString() : "")}
        Total Price: {TotalPrice}
        Items list: {temp}
        ";
    }
}
