using DO;
using System.Xml.Serialization;

namespace Dal
{
    /// <summary>
    /// Class of generic get and set functions
    /// </summary>
    internal class Tools
    {
        public Order Get(Func<T?, bool> match, string path)
        {
            XmlSerializer xs = new XmlSerializer(typeof(IEnumerable<T?>));
            StreamReader sr = new StreamReader(path);
            //קריאת האוביקט שנשמר
            IEnumerable<T?> array = xs.Deserialize(sr) as IEnumerable<T?> ?? throw new Exception("fix this");
            sr.Close();
            return array.Where(match).FirstOrDefault() ?? throw new Exception("not found");
        }

        public IEnumerable<T?> GetAll(Func<T?, bool>? match, string path)
        {
            try {
                XmlSerializer xs = new XmlSerializer(typeof(IEnumerable<T?>));
                StreamReader sr = new StreamReader(path);
                //קריאת האובייקט שנשמר
                IEnumerable<T?> array = xs.Deserialize(sr) as IEnumerable<T?> ?? throw new Exception("fix this");
                sr.Close();
                //fix this
                return array.Where(match);
            } catch {
            }
        }

        public IEnumerable<T?> Add(Func<T?, bool>? match, string path)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.OpenOrCreate);

                //using System.Xml.Serialization יש להוסיף 
                XmlSerializer xs = new XmlSerializer(typeof(T));
                //StreamWriter sw = new StreamWriter(path);
                xs.Serialize(fs, T);
                fs.Close();
            }catch
            {
            }
        }
    }
