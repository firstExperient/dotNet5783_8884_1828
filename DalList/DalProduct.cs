using DO;
using DalApi;
using System.Diagnostics;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;

namespace Dal;

internal class DalProduct : IProduct
{
    #region Add
    /// <summary>
    /// this function is used when there is a new watch
    /// </summary>
    /// <param name="product">the new watch to add</param>
    /// <returns>ID of the added watch</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(Product product)
    {
        if(DataSource.Products.Any(x => x?.ID == product.ID))
            throw new AlreadyExistsException("product with id: " + product.ID + " already exists");
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
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Product Get(Func<Product?,bool> match)
    {
        return DataSource.Products.Where(match).FirstOrDefault() ?? throw new NotFoundException("Product not found");
    }

    /// <summary>
    /// a function that returns all the watches
    /// </summary>
    /// <returns>an array of all watches</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Product?> GetAll(Func<Product?,bool>? match)
    {
        if (match == null)
            return new List<Product?>(DataSource.Products);
        return DataSource.Products.Where(match);
    }

    #endregion

    #region Update

    /// <summary>
    /// this function gets a watch to update it's details
    /// </summary>
    /// <param name="product">the watch to update</param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Product product)
    {
        bool flag = false;
        for (int i = 0; i < DataSource.Products.Count; i++)//we used a loop and not Linq because we need to update
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
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        DataSource.Products.RemoveAll(x => x?.ID == id);
    }

    #endregion
}
