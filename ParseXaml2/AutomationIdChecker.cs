﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ParseXaml2
{
    public class AutomationIdChecker
    {
        private string _startingPath;
        private Action<string> _output;

        public AutomationIdChecker(string startingPath, Action<string> output)
        {
            _startingPath = startingPath;
            _output = output;
        }

        private void ParseXaml(string path)
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
                }
            }
        }

        public void StartSearch()
        {
            var di = new DirectoryInfo(_startingPath);
            foreach (var file in di.GetFiles("*.xaml"))
            {
                ParseXaml(file.FullName);
            }

            RecurseFolders(di);
        }

        private void RecurseFolders(DirectoryInfo directoryInfo)
        {
            foreach (var directory in directoryInfo.GetDirectories("*", SearchOption.TopDirectoryOnly))
            {
                foreach (var file in directory.GetFiles("*.xaml"))
                {
                    ParseXaml(file.FullName);
                }

                RecurseFolders(directory);
            }
        }
    }
}
