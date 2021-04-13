using System.Collections.Generic;
using System.IO;
using System.Linq;
using ComputerGraphics.converter.@struct;
using ComputerGraphics.converter.util;

namespace ComputerGraphics.converter.reader
{
    public class PpmImageReader : BitReader, IImageReader
    {
        private string[] _contents;
        private PpmHeader _header;
        public List<RGBA> Pixels { get; } = new List<RGBA>();
        public int Width => _header.Width;
        public int Depth => _header.Depth;

        public void Read(string path)
        {
            if (!File.Exists(path))
            {
                throw new ReaderException("File not exists in path");
            }
            Buffer = File.ReadAllBytes(path);
            ValidateType();
            
            _contents = String(Buffer.Length - 3)
                .Split('\n')
                .Where(v => !v.StartsWith('#'))
                .Take(2)
                .ToArray();
            SetOffset(_contents.Aggregate(0, (i, s) => i + s.Length) + 5);
            ReadHeader();
            ReadPixels();
            
            if (Pixels.Count != _header.Width * _header.Depth)
            {
                throw new ReaderException("Error reading .ppm image");
            }
        }
        
        private void ReadHeader()
        { 
            var dimensions = _contents[0].Split(' ').Select(int.Parse).ToArray();
            var max = int.Parse(_contents[1]);
            _header = new PpmHeader
            {
                Flag = "P6",
                Width = dimensions[0],
                Depth = dimensions[1],
                Max = max
            };
        }

        private void ReadPixels()
        {
            while (HasData)
            {
                Pixels.Add(new RGBA
                {
                    Red = Byte(),
                    Green = Byte(),
                    Blue = Byte()
                });
            }
        }

        private void ValidateType()
        {
            var flag = String(3);
            if (flag != "P6\n")
            {
                throw new ReaderException("Only P6 .ppm files are supported");
            }
        }
    }
}