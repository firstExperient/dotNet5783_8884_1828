using BlApi;
using Dal;
using System.ComponentModel;


namespace BlImplementation;

internal class Cart : ICart
{
    private DalApi.IDal Dal = new DalList();

    #region add item

    public BO.Cart AddItem(int id, BO.Cart cart)
    {
        try
        {
            DO.Product product = Dal.Product.Get(p => p?.ID == id);
            if (cart.Items != null)
                for (int i = 0; i < cart.Items.Count; i++)
                {
                    if (cart.Items[i]?.ProductId == id)//the product is already in cart
                    {
                        if (product.InStock >= cart.Items[i]!.Amount + 1)//there is enough in stock to add another one to the order item
                        {
                            cart.Items[i]!.Amount++;
                            //adding according to the current price
                            cart.Items[i]!.TotalPrice += product.Price;
                            cart.TotalPrice += product.Price;
                            return cart;
                        }
                        throw new BO.OutOfStockException("product " + id + " is out of stock");
                    }
                }
            else
                cart.Items = new();
            //the product is not in cart
            if (product.InStock <= 0)
                throw new BO.OutOfStockException("product " + id + " is out of stock");
            cart.Items.Add(new BO.OrderItem()
            {
                ProductId = id,
                Name = product.Name,
                Price = product.Price,
                Amount = 1,
                TotalPrice = product.Price,
            });
            cart.TotalPrice += product.Price;
            return cart;
        }
        catch (DO.NotFoundException e)
        {
            throw new BO.NotFoundException("product not found", e);
        }
    }

    #endregion

    #region update item amount

    public BO.Cart UpdateItemAmount(int id, BO.Cart cart, int newAmount)
    {
        if (newAmount < 0) throw new BO.NegativeNumberException("amount cannot be a negative number");
        if (id < 0) throw new BO.NegativeNumberException("product id property cannot be a negative number");
        try
        {
            DO.Product product = Dal.Product.Get(p => p?.ID == id);
            if(cart.Items != null)
                for (int i = 0; i < cart.Items.Count; i++)
                {
                    if (cart.Items[i]?.ProductId == id)
                    {
                        if (newAmount == 0)
                        {
                            cart.TotalPrice -= cart.Items[i]!.TotalPrice;
                            cart.Items.RemoveAt(i);
                            return cart;
                        }
                        if (newAmount < cart.Items[i]!.Amount)
                        {
                            cart.TotalPrice -= cart.Items[i]!.Price * (cart.Items[i]!.Amount - newAmount);
                            cart.Items[i]!.Amount = newAmount;
                            cart.Items[i]!.TotalPrice = cart.Items[i]!.Price * newAmount;
                            return cart;
                        }
                        if (product.InStock < newAmount)
                            throw new BO.OutOfStockException("product " + product.ID + " is out of stock");
                        cart.TotalPrice += cart.Items[i]!.Price * (newAmount - cart.Items[i]!.Amount);
                        cart.Items[i]!.Amount = newAmount;
                        cart.Items[i]!.TotalPrice = cart.Items[i]!.Price * newAmount;
                        return cart;
                    }
                }
            throw new BO.NotFoundException("item not found");
        }
        catch (DO.NotFoundException e)
        {
            throw new BO.NotFoundException(e.Message, e);
        }
       
    }

    #endregion

    #region confirm order

    public void ConfirmOrder(BO.Cart cart)
    {
        if (cart.CustomerName == null || cart.CustomerName == "") throw new BO.NullValueException("customer name cannot be null or an empty string");
        if(cart.CustomerEmail == null || cart.CustomerEmail == "")throw new BO.NullValueException("customer email cannot be null or an empty string");
        if(cart.CustomerAdress == null || cart.CustomerAdress == "")throw new BO.NullValueException("customer address cannot be null or an empty string");
        

        int orderId = Dal.Order.Add(new DO.Order()//add the order to database
        {
            //no need to add id - auto id is generete
            CustomerName = cart.CustomerName,
            CustomerEmail = cart.CustomerEmail,
            CustomerAdress = cart.CustomerAdress,
            OrderDate = DateTime.Now,
            ShipDate = DateTime.MinValue,
            DeliveryDate = DateTime.MinValue,
        });
        if (cart.Items == null) throw new BO.NullValueException("cart items cannot be null when confirming an order");
        foreach (var item in cart.Items)//add each item 
        {
            DO.Product product;
            if(item != null)
                try
                {
                    product = Dal.Product.Get(p => p?.ID == item.ProductId);
                    if (item.Amount <= 0)
                        throw new BO.NegativeNumberException("item amount property must be a positive number");
                    if (product.InStock < item.Amount)
                        throw new BO.OutOfStockException("product " + product.ID + " is out of stock");
                    if (item.Price < 0)
                        throw new BO.NegativeNumberException("item price property cannot be a negative number");

                    product.InStock -= item.Amount;
                    Dal.OrderItem.Add(new DO.OrderItem()
                    {
                        //no need to add id - auto id is generete
                        OrderId = orderId,
                        ProductId = item.ProductId,
                        Amount = item.Amount,
                        Price = item.Price,
                    });
                    Dal.Product.Update(product);//update the amount of product in stock
                }
                catch (DO.NotFoundException e)
                {
                    throw new BO.NotFoundException("product not found", e);
                }
        }
    }

    #endregion

}
