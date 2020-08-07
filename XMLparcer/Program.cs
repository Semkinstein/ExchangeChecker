using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XMLparcer
{
    class Program
    {
        static void Main(string[] args)
        {
            double exchangeValue = GetExchangeValue();
            if (exchangeValue > 0)
            {
                Console.WriteLine("Курс гонгконгского доллара к рублю равен:  " + exchangeValue);
            }
            else
            {
                Console.WriteLine("Курс гонгконгского доллара к рублю не найден");
            }
            Console.ReadKey();
        }

        static double GetExchangeValue()
        {
            XmlDocument xDoc = new XmlDocument();

            // получение XML документа по ссылке
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                xDoc.Load(client.OpenRead("http://www.cbr.ru/scripts/XML_daily.asp"));
            }

            // поиск ID гонконгского доллара и его цены
            XmlElement xRoot = xDoc.DocumentElement;
            foreach (XmlElement xnode in xRoot)
            {
                XmlNode valuteID = xnode.Attributes.GetNamedItem("ID");
                if (valuteID.Value == "R01200")
                {
                    foreach (XmlNode childnode in xnode.ChildNodes)
                    {
                        if (childnode.Name == "Value")
                        {
                            return Convert.ToDouble(childnode.InnerText);
                        }
                    }
                }
            }
            return -1;
        }
    }
}
