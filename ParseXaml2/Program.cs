using System;
using System.IO;

namespace ParseXaml2
{
    class Program
    {
        private enum Output
        {
            Console,
            File
        }

        static void Main(string[] args)
        {
            string targetDirectory;
            Output output = Output.Console;

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
                //Console.WriteLine("Hit any key to exit...");
                //Console.ReadKey();
                return;
            }

            if (args.Length == 2)
            {
                output = args[1].ToLower() == "file" ? Output.File : Output.Console;
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
            if (output == Output.Console)
            {
                var checker = new AutomationIdChecker(targetDirectory, Console.WriteLine, filterStrings);
                found = checker.StartSearch();
            }
            else
            {
                using (var writer = new FileWriter(outputPath))
                {
                    var checker = new AutomationIdChecker(targetDirectory, writer.WriteLine, filterStrings);
                    found = checker.StartSearch();
                }
            }

            Console.WriteLine($"Done!{Environment.NewLine}");
            Console.WriteLine($"Missing AutomationIds found: {found}{Environment.NewLine}");
            Console.WriteLine("Hit any key to exit...");
            Console.ReadKey();
        }
    }
}
