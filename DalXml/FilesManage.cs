
using System.Xml.Serialization;

namespace Dal;

public class FilesManage<T>
{
    static public List<T> ReadList(string path)
    {
        XmlSerializer xs = new XmlSerializer(typeof(List<T>));
        StreamReader sr = new StreamReader(@"../../../../xml/" + path);
        return xs.Deserialize(sr) as List<T> ?? throw new Exception("Fix this");
        sr.Close();
    }

    static public void SaveList(List<T> list, string path)
    {
        XmlSerializer xs = new XmlSerializer(list.GetType());
        FileStream fs = new FileStream(@"../../../../xml/" + path, FileMode.OpenOrCreate);
        xs.Serialize(fs,list);
        fs.Close();
    }
}
