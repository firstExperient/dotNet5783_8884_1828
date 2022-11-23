using BlApi;
using Dal;
using DO;

namespace BlImplementation;

internal class Cart: ICart // fix this (I dont know why this is error)
{
    private DalApi.IDal Dal = new DalList();

    #region ADD

    public Cart AddItem(int id, BO.Cart cart)
    {
        try
        {
            DO.Product dalProduct = Dal.Product.Get(id);
            
          foreach  (BO.Product item in cart.Items)
            {

            }

            if (dalProduct)
            {
                
            cart.TotalPrice += dalProduct.Price;
           
            dalProduct.Price=
            }
            else
            {

            }

        }
        catch (Exception)
        {
            //fix this
            throw;
        }
        throw new NotImplementedException();
    }

    #endregion
    public Cart UpdateItemAmount(int id, Cart cart, int newAmount)
    {

    }
    public void ConfirmOrder(Cart cart, string customerName, string email, string adress)
    {

    }
}
