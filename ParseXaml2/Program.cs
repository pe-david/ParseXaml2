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
            var path = @"..\SelectSubsystemHardware.xaml";

            ParseXaml(path);
        }

        private static void ParseXaml(string path)
        {
            using (var fs = File.Open(path, FileMode.Open, FileAccess.Read))
            {
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
                                        var attribute = $"Attribute name: {reader.Value}";
                                        if (!reader.MoveToAttribute("AutomationProperties.AutomationId"))
                                        {
                                            Console.WriteLine(element);
                                            Console.WriteLine(attribute);
                                            Console.WriteLine("No Automation Id");
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
