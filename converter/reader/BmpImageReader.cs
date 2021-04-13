using System;
using System.Collections.Generic;
using System.IO;
using ComputerGraphics.converter.@struct;
using ComputerGraphics.converter.util;

namespace ComputerGraphics.converter.reader
{
    public class BmpImageReader : BitReader, IImageReader
    {
        private BmpHeader _header;
        public List<RGBA> Pixels { get; } = new List<RGBA>();
        public int Width => _header.Width;
        public int Depth => _header.Depth;

        public void Read(string path)
        {
            Buffer = File.ReadAllBytes(path);
            ReadHeader();
            SetOffset(_header.HeaderSize);
            ValidateHeader();
            ReadPixels();
            if (Pixels.Count != _header.Width * _header.Depth)
            {
                Console.WriteLine(Pixels.Count);
                Console.WriteLine(_header.Width * _header.Depth);
                throw new ReaderException("Error reading .bmp image");
            }
        }

        private void ValidateHeader()
        {
            if (_header.Flag != "BM")
            {
                throw new ReaderException("Invalid .bmp file");
            }
            
            if (_header.Bits != 32)
            {
                throw new ReaderException("Only 32-bit .bmp files are supported");

            }
        }

        private void ReadHeader()
        {
            _header = new BmpHeader
            {
                Flag = String(2),
                FileSize = Int(),
                Reserved = Bytes(4),
                HeaderSize = Int(),
                InfoSize = Int(),
                Width = Int(),
                Depth = Int(),
                BiPlanes = Short(),
                Bits = Short(),
                BiCompression = Int(),
                BiSizeImage = Int(),
                BiXPelsPerMeter = Int(),
                BiYPelsPerMeter = Int(),
                BiClrUsed = Int(),
                BiClrImportant = Int()
            };
        }

        private void ReadPixels()
        {
            var accum = new List<RGBA>();
            while (HasData)
            {
                accum.Add(new RGBA
                {
                    Blue = Byte(),
                    Green = Byte(),
                    Red = Byte(),
                    Alpha = Byte()
                });
                if (accum.Count != _header.Width) continue;
                Pixels.InsertRange(0, accum);
                accum.Clear();
            }
        }
        
    }
}