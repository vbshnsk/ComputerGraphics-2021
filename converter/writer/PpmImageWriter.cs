using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ComputerGraphics.converter.@struct;

namespace ComputerGraphics.converter.writer
{
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