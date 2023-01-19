using DO;
using DalApi;
using System.Diagnostics;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Linq;

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
        return product.ID;
    }

    #endregion

    #region Get

    /// <summary>
    /// a function that returns the specific watch that was asked
    /// </summary>
    /// <param name="id">ID of watch to get</param>
    /// <returns>the watch that has the given ID</returns>
    public Product Get(Func<Product?,bool> match)
    {
        return FilesManage.ReadList<Product?>("Products.xml").Where(match).FirstOrDefault() ?? throw new Exception("not found");
    }

    /// <summary>
    /// a function that returns all the watches
    /// </summary>
    /// <returns>an array of all watches</returns>
    public IEnumerable<Product?> GetAll(Func<Product?,bool>? match)
    {
        return FilesManage.ReadList<Product?>("Products.xml").Where(match);
    }

    #endregion

    #region Update

    /// <summary>
    /// this function gets a watch to update it's details
    /// </summary>
    /// <param name="product">the watch to update</param>
    public void Update(Product product)
    {
        List<Product?> products = (List<Product?>)FilesManage.ReadList<Product?>("Products.xml");

        bool flag = false;
        for (int i = 0; i < products.Count; i++)
        {
            if (products[i]?.ID == product.ID)
            {
                products[i] = product;
                flag = true;
                break;
            }
        }
        FilesManage.SaveList<Product?>(products, "Products.xml");
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
        //read the list, and save again with only the products with Id different than the parameter
        //FilesManage.SaveList<Product?>(FilesManage.ReadList<Product?>("Products.xml").Where(x => x?.ID != id), "Products.xml");
    }

    #endregion
}
