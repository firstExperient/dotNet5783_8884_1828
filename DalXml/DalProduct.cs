using DO;
using DalApi;
using System.Xml.Linq;

namespace Dal;

internal class DalProduct : IProduct
{
    string _path = "Products.xml";
    #region Add
    /// <summary>
    /// this function is used when there is a new watch
    /// </summary>
    /// <param name="product">the new watch to add</param>
    /// <returns>ID of the added watch</returns>
    public int Add(Product product)
    {
        XElement products = FilesManage.ReadXml(_path);
        XElement? xmlProduct = products.Elements().Where(e => int.Parse(e.Element("ID")!.Value) == product.ID).FirstOrDefault();
        if(xmlProduct != null)
            throw new AlreadyExistsException("product with id: " + product.ID + " already exists");
        products.Add(ProductToXml(product));
        FilesManage.SaveXml(products, _path);
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
        IEnumerable<XElement> products = FilesManage.ReadXml(_path).Elements();
        return products.Select(p => XmlToProduct(p)).Where(p => match(p)).FirstOrDefault() ?? throw new NotFoundException("Product not found"); ;
    }

    /// <summary>
    /// a function that returns all the watches
    /// </summary>
    /// <returns>an array of all watches</returns>
    public IEnumerable<Product?> GetAll(Func<Product?,bool>? match)
    {
        if (match == null)
            return FilesManage.ReadXml(_path).Elements().Select(p => XmlToProduct(p));
        return FilesManage.ReadXml(_path).Elements().Select(p=>XmlToProduct(p)).Where(match);
    }

    #endregion

    #region Update

    /// <summary>
    /// this function gets a watch to update it's details
    /// </summary>
    /// <param name="product">the watch to update</param>
    public void Update(Product product)
    {
        XElement products = FilesManage.ReadXml(_path);
        XElement xmlProduct = products.Elements().Where(e => int.Parse(e.Element("ID")!.Value) == product.ID).FirstOrDefault()
            ?? throw new NotFoundException("Product not found");
        xmlProduct.AddAfterSelf(ProductToXml(product));
        xmlProduct.Remove();
        FilesManage.SaveXml(products, _path);
    }

    #endregion

    #region Delete

    /// <summary>
    /// this fuction delete's a watch by the given ID
    /// </summary>
    /// <param name="id">the ID of the watch to delete</param>
    public void Delete(int id)
    {
        XElement products = FilesManage.ReadXml(_path);
        XElement product = products.Elements().Where(e => int.Parse(e.Element("ID")!.Value) == id).FirstOrDefault() 
            ?? throw new NotFoundException("Product not found");
        product.Remove();
        FilesManage.SaveXml(products, _path);
    }

    #endregion

    #region helpers
    private Product? XmlToProduct(XElement element)
    {
        try
        {
            Category category;
            Enum.TryParse(element.Element("Category")!.Value, out category);
            return new Product()
            {
                ID = int.Parse(element.Element("ID")!.Value),
                Name = element.Element("Name")!.Value,
                Price = double.Parse(element.Element("Price")!.Value),
                InStock = int.Parse(element.Element("InStock")!.Value),
                Category = category
            };
        }
        catch
        {
            throw;
        }
    }
    private XElement ProductToXml(Product product)
    {
        return new XElement("Product",
            new XElement("ID", product.ID),
            new XElement("Name", product.Name),
            new XElement("Price", product.Price),
            new XElement("InStock", product.InStock),
            new XElement("Category", product.Category));
    }
    #endregion


}
