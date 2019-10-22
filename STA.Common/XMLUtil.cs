using System;
using System.Xml;

namespace STA.Common
{
    public class XMLUtil
    {
        private XMLUtil()
        {
        }
        /// <summary>
        /// 加载xml
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns></returns>
        public static XmlDocument LoadDocument(string filename)
        {
            if (!FileUtil.FileExists(filename)) return null;
            XmlDocument document = new XmlDocument();
            document.Load(filename);
            return document;
        }

        /// <summary>
        /// 保存xml
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <param name="document">xml对象</param>
        public static void SaveDocument(string filename, XmlDocument document)
        {
            if (!FileUtil.FileExists(filename)) return;
            document.Save(filename);
        }

        public static XmlNode CreateNode(XmlNode parent, string nodeName)
        {
            return CreateNode(parent, nodeName, null);
        }


        public static XmlNode CreateNode(XmlNode parent, string nodeName, string innerValue)
        {
            XmlNode node2;
            if (parent is XmlDocument)
            {
                node2 = parent.AppendChild(((XmlDocument)parent).CreateElement(nodeName));
            }
            else
            {
                node2 = parent.AppendChild(parent.OwnerDocument.CreateElement(nodeName));
            }
            if (innerValue != null)
            {
                node2.AppendChild(parent.OwnerDocument.CreateTextNode(innerValue));
            }
            return node2;
        }

        public static XmlAttribute CreateAttribute(string filename, string name, string value)
        {
            return CreateAttribute(LoadDocument(filename), name, value);
        }


        public static XmlAttribute CreateAttribute(XmlDocument xmlDocument, string name, string value)
        {
            XmlAttribute attribute = xmlDocument.CreateAttribute(name);
            attribute.Value = value;
            return attribute;
        }

        public static string GetNodeValue(string filename, string notename)
        {
            return GetNodeValue(LoadDocument(filename), notename);
        }

        public static string GetNodeValue(XmlDocument xml, string nodeName)
        {
            XmlNodeList elementsByTagName = xml.GetElementsByTagName(nodeName);
            if (elementsByTagName.Count > 0)
            {
                return elementsByTagName[0].InnerText.Trim();
            }
            return "";
        }

        public static string GetNodeValueByXpath(string filename, string xpath)
        {
            XmlDocument xml = LoadDocument(filename);
            XmlNode node = xml.SelectSingleNode(xpath);
            if (node == null) return string.Empty;
            return node.InnerText;
        }


        public static void SetNodeValueByXpath(XmlDocument xml, string xpath, string value)
        {
            XmlNode node = xml.SelectSingleNode(xpath);
            if (node == null) return;
            node.InnerText = value;
        }

        public static string GetNodeValue(XmlElement xml, string nodeName)
        {
            XmlNodeList elementsByTagName = xml.GetElementsByTagName(nodeName);
            if (elementsByTagName.Count > 0)
            {
                return elementsByTagName[0].InnerText.Trim();
            }
            return "";
        }


        public static void SetAttribute(XmlNode node, string attributeName, string attributeValue)
        {
            if (node.Attributes[attributeName] != null)
            {
                node.Attributes[attributeName].Value = attributeValue;
            }
            else
            {
                node.Attributes.Append(CreateAttribute(node.OwnerDocument, attributeName, attributeValue));
            }
        }

        public static void SetNodeValue(string filename, string notename, string value)
        {
            SetNodeValue(LoadDocument(filename), notename, value);
        }

        public static void SetNodeValue(XmlDocument xml, string nodeName, bool value)
        {
            XmlNodeList elementsByTagName = xml.GetElementsByTagName(nodeName);
            if (elementsByTagName.Count > 0)
            {
                elementsByTagName[0].InnerText = value.ToString();
            }
        }

        public static void SetNodeValue(XmlDocument xml, string nodeName, decimal value)
        {
            XmlNodeList elementsByTagName = xml.GetElementsByTagName(nodeName);
            if (elementsByTagName.Count > 0)
            {
                elementsByTagName[0].InnerText = value.ToString();
            }
        }

        public static void SetNodeValue(XmlDocument xml, string nodeName, int value)
        {
            XmlNodeList elementsByTagName = xml.GetElementsByTagName(nodeName);
            if (elementsByTagName.Count > 0)
            {
                elementsByTagName[0].InnerText = value.ToString();
            }
        }

        public static void SetNodeValue(XmlDocument xml, string nodeName, float value)
        {
            XmlNodeList elementsByTagName = xml.GetElementsByTagName(nodeName);
            if (elementsByTagName.Count > 0)
            {
                elementsByTagName[0].InnerText = value.ToString();
            }
        }

        public static void SetNodeValue(XmlDocument xml, string nodeName, string value)
        {
            XmlNodeList elementsByTagName = xml.GetElementsByTagName(nodeName);
            if (elementsByTagName.Count > 0)
            {
                elementsByTagName[0].InnerText = value;
            }
        }

        public static void SetNodeValue(XmlElement xml, string nodeName, string value)
        {
            XmlNodeList elementsByTagName = xml.GetElementsByTagName(nodeName);
            if (elementsByTagName.Count > 0)
            {
                elementsByTagName[0].InnerText = value;
            }
        }

        /// <summary>
        /// 测试代码
        /// </summary>
        //public static void Test()
        //{
        //    XmlDocument document = XMLUtil.LoadDocument("attend");

        //    XmlNode item = XMLUtil.AppendElement(document.SelectSingleNode("attend"), "item");

        //    XmlNode timenote = XMLUtil.AppendElement(item, "time", "12:00");
        //    timenote.Attributes.Append(XMLUtil.CreateAttribute(document, "type", "1"));

        //    XMLUtil.SaveDocument("attend", document);
        //}
    }
}

