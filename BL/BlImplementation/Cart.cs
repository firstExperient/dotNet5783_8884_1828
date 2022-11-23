using BlApi;
using Dal;
using DO;

namespace BlImplementation;

internal class Cart: ICart 
{
    private DalApi.IDal Dal = new DalList();

    #region ADD

    public BO.Cart AddItem(int id, BO.Cart cart)
    {
        try
        {
            DO.Product product = Dal.Product.Get(id);

            for(int i = 0; i < cart.Items.Count; i++)
            {
                if(cart.Items[i].ID == id)//the product is already in cart
                {
                  //fix this
                  //האם הכמות היא לפי הכמות הכללית פחות מה שכבר יש או רק פחות האחד שהוא מוסיף?
                }
            }
            //the product is not in cart
            if(product.InStock <= 0)
                throw new Exception();//fix this
            //fix this - what to do with the id
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
        catch (Exception)
        {
            //fix this
            throw;
        }
    }

    #endregion

    #region update item amount

    public BO.Cart UpdateItemAmount(int id, BO.Cart cart, int newAmount)
    {

    }

    #endregion

    #region confirm order

    public void ConfirmOrder(BO.Cart cart, string customerName, string email, string adress)
    {
        //fix this - add validation checks

        int orderId = Dal.Order.Add(new DO.Order()//add the order to database
        {
            //no need to add id - auto id is generete
            CustomerName = customerName,
            CustomerEmail = email,
            CustomerAdress = adress,
            OrderDate = DateTime.Now,
            ShipDate = DateTime.MinValue,
            DeliveryDate = DateTime.MinValue,
        });
        foreach (var item in cart.Items)//add each item 
        {
            DO.Product product;
            try
            {
                product = Dal.Product.Get(item.ProductId);
                if (product.InStock < item.Amount)
                    throw new Exception();//fix this
                product.InStock -= item.Amount;
                Dal.OrderItem.Add(new OrderItem()
                {
                    //no need to add id - auto id is generete
                    OrderId = orderId,
                    ProductId = item.ProductId,
                    Amount = item.Amount,
                    Price = item.Price,
                });
                Dal.Product.Update(product);//update the amount of product in stock
            }
            catch (Exception)
            {
                //fix this
                throw;
            }
        }
    }

    #endregion

}
