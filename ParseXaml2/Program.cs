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
            //var path = @"C:\Users\rosed18169\source\repos\Tests\ParseXaml2\ParseXaml2\bin";
            var path = @"C:\Users\rosed18169\source\repos\LivingImage";
            var checker = new AutomationIdChecker(path, Console.WriteLine);
            checker.StartSearch();
        }
    }
}
