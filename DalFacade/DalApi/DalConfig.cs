namespace DalApi;
using System.Xml.Linq;
using DO;
static class DalConfig
{
    internal static string? s_dalName;
    internal static Dictionary<string, (string dal,string? Namespace,string? Class)> s_dalPackages;

    static DalConfig()
    {
        XElement dalConfig = XElement.Load(@"..\xml\dal-config.xml")
            ?? throw new DalConfigException("dal-config.xml file is not found");
        s_dalName = dalConfig?.Element("dal")?.Value
            ?? throw new DalConfigException("<dal> element is missing");
        var packages = dalConfig?.Element("dal-packages")?.Elements()
            ?? throw new DalConfigException("<dal-packages> element is missing");
        packages.First().Attribute("namespace");
        s_dalPackages = packages.ToDictionary(p => "" + p.Name, p => (p.Value,p.Attribute("namespace")?.Value,p.Attribute("class")?.Value));
    }
}