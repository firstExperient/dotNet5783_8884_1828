using BlApi;
using Dal;

namespace BlImplementation;

internal class Product : IProduct
{
    private DalApi.IDal Dal = new DalList();

    #region ADD
    public void Add(BO.Product item)
    {
        if(checkValid(item))
            throw new NotImplementedException();//fix this
        try
        {
            Dal.Product.Add(new DO.Product()
            {
                ID = item.ID,
                Name = item.Name,
                Price = item.Price,
                Category = (DO.Category)item.Category,
                InStock = item.InStock,
            });
        }
        catch (Exception)
        {
            //fix this
            throw;
        }
    }

    #endregion

    #region DELETE

    public void Delete(int id)
    {
        List<DO.OrderItem> orderItems = (List<DO.OrderItem>)Dal.OrderItem.GetAll();
        foreach (DO.OrderItem item in orderItems)
        {
            if (item.ID == id)
                throw new Exception();//fix this
        }
        try
        {
            Dal.Product.Delete(id);
        }
        catch (Exception)
        {
            //fix this
            throw;
        }
    }


    #endregion

    #region GET
    public BO.ProductItem Get(int id, BO.Cart cart)
    {

        if (id < 0) throw new Exception();//fix this
        BO.ProductItem product;
        try
        {
            DO.Product dalProduct = Dal.Product.Get(id);
            BO.OrderItem orderItem = cart.Items.Find((x) => x.ID == id);
            if (orderItem == null)
                throw new Exception(); //fix this
            //check this - should the instock consider the amount that the costumer already have in cart?
            product = new BO.ProductItem()
            {
                ID = dalProduct.ID,
                Name = dalProduct.Name,
                Category = (BO.Category)dalProduct.Category,
                Price = dalProduct.Price,
                InStock = dalProduct.InStock > 0 ? true : false,
                Amount = orderItem.Amount
            };
        }
        catch (Exception)
        {
            //fix this
            throw;
        }
        return product;
    }

    public BO.Product AdminGet(int id)
    {
        if (id < 0) throw new Exception();//fix this
        BO.Product product;
        try
        {
            DO.Product dalProduct = Dal.Product.Get(id);
            product = new BO.Product()
            {
                ID = dalProduct.ID,
                Name = dalProduct.Name,
                Category = (BO.Category)dalProduct.Category,
                Price = dalProduct.Price,
                InStock = dalProduct.InStock,
            };
        }
        catch (Exception)
        {
            //fix this
            throw;
        }
        return product;
    }

    /// <summary>
    /// gets all the products from the data layer
    /// turns them to ProductForList type and return them in a list
    /// </summary>
    /// <returns>all the products in a list</returns>
    public IEnumerable<BO.ProductForList> GetAll()
    {
        List<DO.Product> dalProducts = (List<DO.Product>)Dal.Product.GetAll();
        List<BO.ProductForList> blProducts = new List<BO.ProductForList>();
        foreach (DO.Product item in dalProducts)
        {
            blProducts.Add(new BO.ProductForList()
            {
                ID = item.ID,
                Name = item.Name,
                Price = item.Price,
                Category = (BO.Category)item.Category
            });
        }
        return blProducts;
    }


    #endregion

    #region UPDATE

    public void Update(BO.Product item)
    {

        if (checkValid(item))
            throw new NotImplementedException();//fix this
        try
        {
            Dal.Product.Update(new DO.Product()
            {
                ID = item.ID,
                Name = item.Name,
                Price = item.Price,
                Category = (DO.Category)item.Category,
                InStock = item.InStock,
            });
        }
        catch (Exception)
        {
            //fix this
            throw;
        }
    }

    #endregion

    #region Helpers

    private bool checkValid(BO.Product product)
    {
        if (product.ID < 0) return false;
        if (product.Name == null || product.Name == "") return false;
        if(product.Price < 0) return false; 
        if(product.InStock < 0) return false;
        return true;
    }

    #endregion
}
