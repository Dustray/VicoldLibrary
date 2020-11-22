using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Vicold.FileConfusion.FileTools
{
    internal class ReadSlice : IDisposable
    {
        private int _onePartLength = 1024;
        private FileStream _fs;
        private BinaryReader _br;
        public ReadSlice(string filePath, int onePartLength = 1024)
        {
            _fs = new FileStream(filePath, FileMode.Open);
            _br = new BinaryReader(_fs);
            _fs.Seek(0, SeekOrigin.Begin);
            _onePartLength = onePartLength;
        }

        public bool HasNext()
        {
            return _fs.Position < _fs.Length;
        }

        public byte[] Read()
        {
            byte[] b;
            if (_br.BaseStream.Position + _onePartLength < _br.BaseStream.Length)
            {
                b = _br.ReadBytes(_onePartLength);
            }
            else
            {
                b = _br.ReadBytes((int)(_br.BaseStream.Length - _br.BaseStream.Position ));
            }
            return b;
        }

        public void Dispose()
        {
            _br.Dispose();
            _fs.Dispose();
        }

    }
}
