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
            string targetDirectory;
            if (args.Length == 0)
            {
                Console.WriteLine("Enter the path to the target directory:");
                targetDirectory = Console.ReadLine();
            }
            else
            {
                targetDirectory = args[0];
            }

            if (!Directory.Exists(targetDirectory))
            {
                Console.WriteLine($"Target directory \"{targetDirectory}\" does not exist.");
                Console.WriteLine("Hit any key to exit...");
                Console.ReadKey();
                return;
            }

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

            int found;
            using (var writer = new FileWriter(outputPath))
            {
                var checker = new AutomationIdChecker(targetDirectory, writer.WriteLine, filterStrings);
                found = checker.StartSearch();
            }

            Console.WriteLine($"Done!{Environment.NewLine}");
            Console.WriteLine($"Missing AutomationIds found: {found}{Environment.NewLine}");
            Console.WriteLine("Hit any key to exit...");
            Console.ReadKey();
        }
    }
}
