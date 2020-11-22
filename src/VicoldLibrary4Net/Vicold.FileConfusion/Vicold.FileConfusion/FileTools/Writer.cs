using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Vicold.FileConfusion.FileTools
{
    internal class Writer : IDisposable
    {
        private FileStream _fs;
        private BinaryWriter _bw;
        public Writer(string filePath)
        {
            _fs = new FileStream(filePath, FileMode.Create);
            _bw = new BinaryWriter(_fs);
        }

        public void Write(byte[] data) => _bw.Write(data);

        public void Write(long data) => _bw.Write(data);

        public void Write(string data) => _bw.Write(data);

        public void Dispose()
        {
            _bw.Dispose();
            _fs.Dispose();
        }
    }
}
