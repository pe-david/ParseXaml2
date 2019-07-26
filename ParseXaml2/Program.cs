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
            if (args.Length == 0)
            {
                Console.WriteLine("No target directory on the command line.");
                Console.WriteLine("Hit any key to exit...");
                Console.ReadKey();
                return;
            }

            if (!Directory.Exists(args[0]))
            {
                Console.WriteLine($"Target directory \"{args[0]}\" does not exist.");
                Console.WriteLine("Hit any key to exit...");
                Console.ReadKey();
                return;
            }

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
            Console.WriteLine("Hit any key to exit...");
            Console.ReadKey();
        }
    }
}
