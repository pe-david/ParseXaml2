using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ParseXaml2
{
    public class AutomationIdChecker
    {
        private string _startingPath;
        private Action<string> _output;
        private string[] _filterStrings;

        public AutomationIdChecker(string startingPath, Action<string> output)
        {
            _startingPath = startingPath;
            _output = output;
        }

        public AutomationIdChecker(string startingPath, Action<string> output, string[] filterStrings)
                : this(startingPath, output)
        {
            _filterStrings = filterStrings;
        }

        private int ParseXaml(string path)
        {
            var found = 0;
            using (var fs = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                var settings = new XmlReaderSettings();
                using (var reader = XmlReader.Create(fs, settings))
                {
                    _output($"File: {path}{Environment.NewLine}");
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (_filterStrings != null &&_filterStrings.Any(s => s == reader.Name)) continue;
                                var element = $"Element {reader.Name}";
                                if (reader.HasAttributes)
                                {
                                    if (reader.MoveToAttribute("Name"))
                                    {
                                        var attribute = $"Attribute name: {reader.Value}";
                                        if (!reader.MoveToAttribute("AutomationProperties.AutomationId"))
                                        {
                                            found++;
                                            _output(element);
                                            _output(attribute);
                                            _output("No Automation Id");
                                        }

                                        _output("");
                                    }
                                }
                                break;
                        }
                    }

                    _output($"End: {path}{Environment.NewLine}");
                }
            }

            return found;
        }

        public int StartSearch()
        {
            var found = 0;
            var di = new DirectoryInfo(_startingPath);
            foreach (var file in di.GetFiles("*.xaml"))
            {
                found = ParseXaml(file.FullName);
            }

            found += RecurseFolders(di);
            return found;
        }

        private int RecurseFolders(DirectoryInfo directoryInfo)
        {
            var found = 0;
            foreach (var directory in directoryInfo.GetDirectories("*", SearchOption.TopDirectoryOnly))
            {
                foreach (var file in directory.GetFiles("*.xaml"))
                {
                    found += ParseXaml(file.FullName);
                }

                found += RecurseFolders(directory);
            }

            return found;
        }
    }
}
