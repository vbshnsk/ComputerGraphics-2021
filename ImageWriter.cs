using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ComputerGraphics
{

    public static class ImageWriterFactory
    {
        public static IImageWriter GetWriter(ImageType type)
        {
            return type switch
            {
                ImageType.Bmp => new BmpImageWriter(),
                ImageType.Ppm => new PpmImageWriter(),
                _ => throw new WriterException(type.ToString().ToUpper() + " reader is not implemented")
            };
        }
    }

    public class WriterException : Exception
    {
        public WriterException(string message) : base(message)
        {
        }
    }

    public interface IImageWriter
    {
        public void Write(string path, IEnumerable<RGBA> pixels, int width, int depth);
    }

    public class BmpImageWriter : IImageWriter
    {
        private readonly MemoryStream _buffer = new MemoryStream();
        
        public void Write(string path, IEnumerable<RGBA> pixels, int width, int depth)
        {
            try
            {
                WriteHeader(width, depth);
                WritePixels(pixels, width);
                File.WriteAllBytes(path, _buffer.GetBuffer());
            }
            catch (Exception)
            {
                throw new WriterException("Could not write image");
            }
        }

        private void WriteHeader(int width, int depth)
        {
            var header = BmpHeader.GetBase(width, depth);
            _buffer.Write(Encoding.UTF8.GetBytes(header.Flag));
            _buffer.Write(BitConverter.GetBytes(header.FileSize));
            _buffer.Write(header.Reserved);
            _buffer.Write(BitConverter.GetBytes(header.HeaderSize));
            _buffer.Write(BitConverter.GetBytes(header.InfoSize));
            _buffer.Write(BitConverter.GetBytes(header.Width));
            _buffer.Write(BitConverter.GetBytes(header.Depth));
            _buffer.Write(BitConverter.GetBytes(header.BiPlanes));
            _buffer.Write(BitConverter.GetBytes(header.Bits));
            _buffer.Write(BitConverter.GetBytes(header.BiCompression));
            _buffer.Write(BitConverter.GetBytes(header.BiSizeImage));
            _buffer.Write(BitConverter.GetBytes(header.BiXPelsPerMeter));
            _buffer.Write(BitConverter.GetBytes(header.BiYPelsPerMeter));
            _buffer.Write(BitConverter.GetBytes(header.BiClrUsed));
            _buffer.Write(BitConverter.GetBytes(header.BiClrImportant));
        }

        private void WritePixels(IEnumerable<RGBA> pixels, int width)
        {
            var pixelsArr = pixels.ToArray();
            var pixelRow = new List<byte>(); 
            for (int i = pixelsArr.Length - 1; i >= 0; i--)
            {
                pixelRow.Add(pixelsArr[i].Alpha ?? 255);
                pixelRow.Add(pixelsArr[i].Red);
                pixelRow.Add(pixelsArr[i].Green);
                pixelRow.Add(pixelsArr[i].Blue);

                if (pixelRow.Count == width * 4)
                {
                    pixelRow.Reverse();
                    _buffer.Write(pixelRow.ToArray());
                    pixelRow.Clear();
                }
            }
            
        }
    }
    
    public class PpmImageWriter : IImageWriter
    {
        private readonly MemoryStream _buffer = new MemoryStream();
        
        public void Write(string path, IEnumerable<RGBA>  pixels, int width, int depth)
        {
            try
            {
                WriteHeader(width, depth);
                WritePixels(pixels);
                File.WriteAllBytes(path, _buffer.GetBuffer());
            }
            catch (Exception)
            {
                throw new WriterException("Could not write image");
            }
        }
        
        private void WriteHeader(int width, int depth)
        {
            var header = PpmHeader.GetBase(width, depth);
            var headerString = header.Flag + '\n' +
                               header.Width + " " + header.Depth + '\n' +
                               header.Max + '\n';
            _buffer.Write(Encoding.UTF8.GetBytes(headerString));
        }

        private void WritePixels(IEnumerable<RGBA> pixels)
        {
            foreach (var pixel in pixels)
            {
                _buffer.WriteByte(pixel.Red);
                _buffer.WriteByte(pixel.Green);
                _buffer.WriteByte(pixel.Blue);
            }
        }
    }
}