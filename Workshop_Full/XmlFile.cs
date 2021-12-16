using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Workshop
{
    class XmlFile
    {
        private XmlDocument xmlDoc;
        private string filename;
        private string currentDirectory;
        private string xmlDocFilePath;

        public XmlFile()
        {

        }

        public XmlFile(string file)
        {
            filename = file;
            currentDirectory = Directory.GetCurrentDirectory();
            xmlDocFilePath = Path.Combine(currentDirectory, filename);
        }

        public void Xml_Load()
        {
            xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlDocFilePath);
        }

        public void Xml_Load_Serialisation(string file, )
        {
            
        }

        public string Get_Value(string racine, string element, string attribute)
        {
            string Value;
            string path = "//" + racine + "/" + element;
            XmlNode xmlNode = xmlDoc.SelectSingleNode(path);
            if (xmlNode != null)
            {
                Value = xmlNode.Attributes[attribute].Value;
            }
            else
            {
                Value = "NA";
            }
            return Value;
        }

        public void Set_Value(string racine, string element, string attribute, string value)
        {
            string path = "//" + racine + "/" + element;
            XmlNode formId = xmlDoc.SelectSingleNode(path);
            if (formId != null)
            {
                formId.Attributes[attribute].Value = value;
            }
        }

        public void Xml_Save()
        {
            xmlDoc.Save(xmlDocFilePath);
        }
    }
}
