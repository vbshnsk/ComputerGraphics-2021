using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ComputerGraphics
{
    public static class ImageReaderFactory
    {
        public static IImageReader GetReader(ImageType type)
        {
            return type switch
            {
                ImageType.Bmp => new BmpImageReader(),
                ImageType.Ppm => new PpmImageReader(),
                _ => throw new ReaderException(type.ToString().ToUpper() + " reader is not implemented")
            };
        }
    }

    public class ReaderException : Exception
    {
        public ReaderException(string message) : base(message)
        {
        }
    }

    public interface IImageReader
    {
        List<RGBA> Pixels { get; }
        int Width { get; }
        int Depth { get; }

        public void Read(string path);
    }

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