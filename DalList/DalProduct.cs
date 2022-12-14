using DO;
using DalApi;
using System.Diagnostics;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace Dal;

internal class DalProduct : IProduct
{
    #region Add
    /// <summary>
    /// this function is used when there is a new watch
    /// </summary>
    /// <param name="product">the new watch to add</param>
    /// <returns>ID of the added watch</returns>
    public int Add(Product product)
    {
       
        for (int i = 0; i < DataSource.Products.Count; i++)
        {
            if (DataSource.Products[i]?.ID == product.ID)
            {
                throw new AlreadyExistsException("product with id: " + product.ID + " already exists");
            }
        }
        DataSource.Products.Add(product);
        return product.ID;
    }

    #endregion

    #region Get

    /// <summary>
    /// a function that returns the specific watch that was asked
    /// </summary>
    /// <param name="id">ID of watch to get</param>
    /// <returns>the watch that has the given ID</returns>
    //public Product Get(int id)
    //{
    //    for (int i = 0; i < DataSource.Products.Count; i++)
    //    {
    //        if (DataSource.Products[i].HasValue && DataSource.Products[i]!.Value.ID == id) return (Product)DataSource.Products[i]!;
    //    }
    //    throw new NotFoundException("Product not found");
    //}
    public Product Get(Predicate<Product?> match)
    {
        Product? product = DataSource.Products.Find(match);
        if (product == null) throw new NotFoundException("Product not found");
        return (Product)product!;
    }

    /// <summary>
    /// a function that returns all the watches
    /// </summary>
    /// <returns>an array of all watches</returns>
    public IEnumerable<Product?> GetAll(Predicate<Product?>? match)
    {
        if (match == null)
            return new List<Product?>(DataSource.Products);
        return DataSource.Products.FindAll(match);
    }

    #endregion

    #region Update

    /// <summary>
    /// this function gets a watch to update it's details
    /// </summary>
    /// <param name="product">the watch to update</param>
    public void Update(Product product)
    {
        bool flag = false;
        for (int i = 0; i < DataSource.Products.Count; i++)
        {
            if (DataSource.Products[i]?.ID == product.ID)
            {
                DataSource.Products[i] = product;
                flag = true;
                break;
            }
        }
        if (!flag) throw new NotFoundException("Product not found");
    }

    #endregion

    #region Delete

    /// <summary>
    /// this fuction delete's a watch by the given ID
    /// </summary>
    /// <param name="id">the ID of the watch to delete</param>
    public void Delete(int id)
    {
        bool flag = false;
        int i = 0;
        for (; i < DataSource.Products.Count; i++)
        {
            if (DataSource.Products[i]?.ID == id)
            {
                flag = true;
                break;
            }
        }
        if (!flag) throw new NotFoundException("Product not found");
        else
            DataSource.Products.RemoveAt(i);
    }

    #endregion
}
