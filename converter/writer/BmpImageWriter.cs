using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ComputerGraphics.converter.@struct;

namespace ComputerGraphics.converter.writer
{
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
}