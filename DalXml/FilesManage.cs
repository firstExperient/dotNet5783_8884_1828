﻿
using System.Xml.Linq;
using System.Xml.Serialization;

using DO;

namespace Dal;

public class FilesManage
{
    static string _path = @"../xml/";

    /// <summary>
    /// This function is to read an XML file whos path is the path
    /// given as parameter return the data in the file in a list form.
    /// </summary>
    /// <param name="path"></param>
    /// <returns>list of data</returns>

    static public List<T> ReadList<T>(string path)
    {
        try
        {
            if (File.Exists(_path + path))
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<T>));
                StreamReader sr = new StreamReader(_path + path);
                List<T> list = xs.Deserialize(sr) as List<T> ?? throw new Exception("Fix this");
                sr.Close();
                return list;
            }
            else
            {
                List<T> list = new List<T>();
                SaveList(list, path);
                return list;
            }
        }
        catch (Exception ex)
        {
            throw new XmlFileException($"Failed to read list from file {_path + path}",ex);
        } 
    }

    /// <summary>
    /// This function is to save in a file whos path is the path
    /// given as parameter the data in the list given in an XML form.
    /// </summary>
    /// <param name="list">list of data</param>
    /// <param name="path">path of the XML file</param>
    static public void SaveList<T>(List<T> list, string path)
    {
        try
        {
            XmlSerializer xs = new XmlSerializer(list.GetType());
            FileStream fs = new FileStream(_path + path, FileMode.OpenOrCreate);
            xs.Serialize(fs, list);
            fs.Close();
        }
        catch (Exception ex)
        {
            throw new XmlFileException($"Failed to save list to file {_path + path}",ex);
        }
    }

    static public XElement ReadXml(string path)
    {
        try
        {
            if (File.Exists(_path + path))
                return XElement.Load(_path + path);
            
            else
            {
                XElement rootElem = new XElement( path);
                rootElem.Save(_path + path);
                return rootElem;
            }
        }
        catch (Exception ex)
        {
            throw new XmlFileException($"Failed to read from file {_path + path}", ex);
        }
    }

    static public void SaveXml(XElement element,string path)
    {
        try
        {
            element.Save(_path + path);
        }
        catch (Exception ex)
        {
            throw new XmlFileException($"Failed to save to file {_path + path}", ex);
        }
    }
}
