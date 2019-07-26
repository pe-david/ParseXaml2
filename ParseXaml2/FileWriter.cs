using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseXaml2
{
    public class FileWriter : IDisposable
    {
        private string _path;
        private StreamWriter _writer;

        public FileWriter(string path)
        {
            _path = path;
            _writer = new StreamWriter(_path, false);
        }

        public void WriteLine(string line)
        {
            _writer.WriteLine(line);
        }

        public void Dispose()
        {
            _writer.Dispose();
        }
    }
}
