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
            var path = args[0];
            var outputPath = @".\Output.txt";
            var filterStrings = new string[]
            {
                "TextBlock",
                "Grid",
                "StackPanel",
                "DockPanel",
                "Ellipse",
                "Rectangle",
                "Canvas",
                "Label",
                "ColumnDefinition",
                "RowDefinition",
                "Border",
                "Polyline",
                "Window"
            };

            using (var writer = new FileWriter(outputPath))
            {
                var checker = new AutomationIdChecker(path, writer.WriteLine, filterStrings);
                checker.StartSearch();
            }

            Console.WriteLine("Done!");
            Console.ReadKey();
        }
    }
}
