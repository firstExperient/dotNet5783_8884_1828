using BlApi;
namespace BlImplementation;

internal class Product : IProduct
{
    private DalApi.IDal? dal =  DalApi.Factory.Get();


    #region ADD
    public void Add(BO.Product item)
    {
        checkValid(item);
        try
        {
            //I couldn't use the operator ? here. because i wanted to throw error if dal is null, and (?? throw...)
            //works only in assignment like i did in the rest of the project
            if (dal == null) throw new BO.AccessToDataFailedException("cannot access the data layer");
            dal?.Product.Add(Tools.Copy(item, new DO.Product()
            {
                Category = (DO.Category?)item.Category,
            }));
        }
        catch (DO.AlreadyExistsException e)
        {
            throw new BO.AlreadyExistsException(e.Message, e);
        }
    }

    #endregion

    #region DELETE

    public void Delete(int id)
    {
        IEnumerable<DO.OrderItem?> orderItems = dal?.OrderItem.GetAll(null) ?? throw new BO.AccessToDataFailedException("cannot access the data layer");
        if (orderItems.Any(x => x?.ProductId == id))
            throw new BO.IntegrityDamageException("cannot delete the product without hurting data integrity. There are orders for the product");
        try
        {
            dal.Product.Delete(id);
        }
        catch (DO.NotFoundException e)
        { 
            throw new BO.NotFoundException("product not found",e);
        }
    }

    #endregion

    #region GET
    public BO.ProductItem Get(int id, BO.Cart cart)
    {
        if (id < 0) throw new BO.NegativeNumberException("product ID property cannot be a negative number");
        try
        {
            DO.Product dalProduct = dal?.Product.Get(p => p?.ID == id) ?? throw new BO.AccessToDataFailedException("cannot access the data layer");
            BO.OrderItem? orderItem = cart.Items?.Where((x) => x?.ProductId == id).FirstOrDefault();
            int amount = orderItem?.Amount ?? 0;
            return Tools.Copy(dalProduct, new BO.ProductItem()
            {
                Category = (BO.Category?)dalProduct.Category,
                InStock = dalProduct.InStock - amount > 0 ? true : false,
                Amount = amount
            });
        }
        catch (DO.NotFoundException e)
        {
            throw new BO.NotFoundException("product not found", e);
        }
    }

    public BO.Product AdminGet(int id)
    {
        if (id < 0) throw new BO.NegativeNumberException("product ID property cannot be a negative number");
        try
        {
            DO.Product dalProduct = dal?.Product.Get(p => p?.ID == id) ?? throw new BO.AccessToDataFailedException("cannot access the data layer");
            return Tools.Copy(dalProduct, new BO.Product()
            {
                Category = (BO.Category?)dalProduct.Category,
            });
        }
        catch (DO.NotFoundException e)
        {
            throw new BO.NotFoundException("product not found", e);
        }
        
    }

    /// <summary>
    /// gets all the products from the data layer
    /// turns them to ProductForList type and return them in a list
    /// </summary>
    /// <returns>all the products in a list</returns>
    public IEnumerable<BO.ProductForList?> GetAll()
    {
        IEnumerable<DO.Product?> dalProducts = dal?.Product.GetAll(null) ?? throw new BO.AccessToDataFailedException("cannot access the data layer");
        IEnumerable<BO.ProductForList?> blProducts = from item in dalProducts
                                              where item != null
                                              orderby item?.ID
                                              select Tools.Copy(item, new BO.ProductForList() { Category = (BO.Category?)item?.Category });
        return blProducts;
    }


    /// <summary>
    /// gets all the products with the category wanted from the data layer
    /// turns them to ProductForList type and return them in a list
    /// </summary>
    /// <returns>all the products in a list</returns>
    public IEnumerable<BO.ProductForList?> GetByCategory(BO.Category category)
    {
        IEnumerable<DO.Product?> dalProducts = dal?.Product.GetAll((product) => product?.Category == (DO.Category)category) ?? throw new BO.AccessToDataFailedException("cannot access the data layer");
        IEnumerable<BO.ProductForList?> blProducts = from item in dalProducts
                                                     where item != null
                                                     select Tools.Copy(item, new BO.ProductForList() { Category = (BO.Category?)item?.Category });
        return blProducts;
    }

    public IEnumerable<IGrouping<BO.Category?, BO.ProductItem>> GetCatalog(BO.Cart cart)
    {
        //fix this - find a smart way to do this
        IEnumerable<DO.Product?> products =   dal?.Product.GetAll(null) ?? throw new BO.AccessToDataFailedException("cannot access the data layer");
        IEnumerable<IGrouping<BO.Category?,BO.ProductItem>> productItems = (from product in products
                                                     where product != null
                                                     let orderItem = cart.Items?.Where((x) => x?.ProductId == product?.ID).FirstOrDefault()
                                                     let amount = orderItem?.Amount ?? 0
                                                     select Tools.Copy(product, new BO.ProductItem()
                                                     {
                                                         Category = (BO.Category?)product?.Category,
                                                         InStock = product?.InStock - amount > 0 ? true : false,
                                                         Amount = amount
                                                     })).GroupBy(x => x.Category);
                                                    
            
        return productItems;
    }

    #endregion

    #region UPDATE

    public void Update(BO.Product item)
    {
        checkValid(item);
        try
        {
            if (dal == null) throw new BO.AccessToDataFailedException("cannot access the data layer");
            dal.Product.Update(Tools.Copy(item, new DO.Product() { Category =  (DO.Category?)item.Category  }));
        }
        catch (DO.NotFoundException e)
        {
            throw new BO.NotFoundException("product not found", e);
        }
    }

  
    #endregion

    #region Helpers
    /// <summary>
    /// check validation of a BO.Product object, throw if there are unvalid values
    /// </summary>
    /// <param name="product">a product to validate</param>
    /// <exception cref="BO.NegativeNumberException">ID,Price or InStock are negative number</exception>
    /// <exception cref="BO.NullValueException">Name is null</exception>
    private void checkValid(BO.Product product)
    {
        if (product.ID <= 0) throw new BO.NegativeNumberException("product ID property cannot be a negative number");
        if (product.Name == null || product.Name == "") throw new BO.NullValueException("product Name property cannot be null or an empty string");
        if(product.Price <= 0) throw new BO.NegativeNumberException("product Price property cannot be a negative number");
        if (product.InStock <= 0) throw new BO.NegativeNumberException("product InStock property cannot be a negative number");
    }

    

    #endregion
}
