using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ConsoleSandbox
{
    class XMLManager
    {
        public static void CreatXmlBookshelf(string adminName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"SHIFT-JIS\" ?><Bookshelf></Bookshelf>");
            addBook(xmlDoc, "Book1", "123.45", adminName);
            addBook(xmlDoc, "Book2", "678.9", adminName);
            addBook(xmlDoc, "Book3", "111121333", adminName);
            addBook(xmlDoc, "Book4", "22223333", adminName);
            addBook(xmlDoc, "Book5", "54321", adminName);
            saveXml(xmlDoc, "bookshelf.xml");
        }

        private static void addBook(XmlDocument bookshelf, string bookName, string price, string admin)
        {
            XmlNode bookNode = bookshelf.CreateElement("Book");
            ((XmlElement)bookNode).SetAttribute("admin", admin);
            XmlNode nameNode = bookshelf.CreateElement("Name");
            nameNode.InnerText = bookName;
            XmlNode priceNode = bookshelf.CreateElement("Price");
            priceNode.InnerText = price;
            bookNode.AppendChild(nameNode);
            bookNode.AppendChild(priceNode);
            XmlNode bs = bookshelf.GetElementsByTagName("Bookshelf")[0];
            bs.AppendChild(bookNode);
        }

        private static void saveXml(XmlDocument doc, string fileName)
        {
            doc.Save(fileName);
        }

        public static void readXmlFromFileWXmlDocument()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("bookshelf.xml");
            
            XmlNode bsNode = doc.GetElementsByTagName("Bookshelf")[0];
            XmlNodeList bookNodesList = bsNode.ChildNodes;
            foreach(XmlNode node in bookNodesList)
            {
                Console.WriteLine("Book name: " + node.SelectNodes("Name")[0].InnerText);
                Console.WriteLine("Book price: " + node.SelectNodes("Price")[0].InnerText);
                Console.WriteLine("Book admin: " + node.Attributes["admin"].Value);
                Console.WriteLine();
            }
        }

        public static void readXmlFromFileWLinq()
        {
            XElement xroot = XElement.Load("bookshelf.xml");

            foreach(XElement book in xroot.Elements("Book"))
            {
                Console.WriteLine("Book name: " + book.Elements("Name").First().Value);
                Console.WriteLine("Book price: " + book.Elements("Price").First().Value);
                Console.WriteLine("Book admin: " + book.Attribute("admin").Value);
                Console.WriteLine();
            }
        }
    }
}
