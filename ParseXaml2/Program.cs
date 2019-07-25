using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Xml;

namespace ParseXaml2
{
    class Program
    {
        static void Main(string[] args)
        {
            // test xaml file is in the Bin folder
            using (var fs = File.Open(@"..\SelectSubsystemHardware.xaml", FileMode.Open, FileAccess.Read))
            {
                //var doc = new XmlDocument();
                //doc.Load(fs);

                var settings = new XmlReaderSettings();
                using (var reader = XmlReader.Create(fs, settings))
                {
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element:
                                var element = $"Start Element {reader.Name}";
                                if (reader.HasAttributes)
                                {
                                    if (reader.MoveToAttribute("Name"))
                                    {
                                        Console.WriteLine($"Element name: {element}");
                                        Console.WriteLine($"Attribute name: {reader.Value}");
                                        if (reader.MoveToAttribute("AutomationProperties.AutomationId"))
                                            Console.WriteLine($"AutomationId: {reader.Value}");
                                        else
                                        {
                                            Console.WriteLine("No AutomationId");
                                        }

                                        Console.WriteLine();
                                    }
                                }
                                break;
                        }
                    }
                }
            }
        }
    }
}
