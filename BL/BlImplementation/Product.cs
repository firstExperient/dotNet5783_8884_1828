using BlApi;
using Dal;

namespace BlImplementation;

internal class Product : IProduct
{
    private DalApi.IDal Dal = new DalList();

    #region ADD
    public void Add(BO.Product item)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region DELETE

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }


    #endregion

    #region GET
    public BO.ProductItem Get(int id, BO.Cart cart)
    {
        throw new NotImplementedException();
    }

    public BO.Product AdminGet(int id)
    {
        if (id < 0) throw new Exception();//fix this
        
        try
        {
            DO.Product dalProduct = Dal.Product.Get(id);
            BO.Product product = new BO.Product() {
                ID=dalProduct.ID,
                Name=dalProduct.Name,
                Category = (BO.Category)dalProduct.Category,

            };
        }
        catch (Exception)
        {
            //fix this
            throw;
        }
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }

    #endregion

}
