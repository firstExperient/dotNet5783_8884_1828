using System.Xml.Serialization;

namespace Dal;

public class FilesManage
{

    /// <summary>
    /// This function is to read an XML file whos path is the path
    /// given as parameter return the data in the file in a list form.
    /// </summary>
    /// <param name="path"></param>
    /// <returns>list of data</returns>

    static public List<T> ReadList<T>(string path)
    {
        XmlSerializer xs = new XmlSerializer(typeof(List<T>));
        StreamReader sr = new StreamReader(@"../../../../xml/" + path);
        return xs.Deserialize(sr) as List<T> ?? throw new Exception("Fix this");
        sr.Close();
    }

    /// <summary>
    /// This function is to save in a file whos path is the path
    /// given as parameter the data in the list given in an XML form.
    /// </summary>
    /// <param name="list">list of data</param>
    /// <param name="path">path of the XML file</param>
    static public void SaveList<T>(List<T> list, string path)
    {
        XmlSerializer xs = new XmlSerializer(list.GetType());
        FileStream fs = new FileStream(@"../../../../xml/" + path, FileMode.OpenOrCreate);
        xs.Serialize(fs,list);
        fs.Close();
    }
}
