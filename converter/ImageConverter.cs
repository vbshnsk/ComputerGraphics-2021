using System;
using ComputerGraphics.converter.@enum;
using ComputerGraphics.converter.reader;
using ComputerGraphics.converter.writer;

namespace ComputerGraphics.converter
{
    class ImageConverter : IImageConverter
    {
        private readonly IImageReader _reader;
        private readonly IImageWriter _writer;

        public ImageConverter(IImageReader reader, IImageWriter writer)
        {
            _reader = reader;
            _writer = writer;
        }
        
        public void Convert(string source, string output)
        {
            _reader.Read(source);
            _writer.Write(output, _reader.Pixels, _reader.Width, _reader.Depth);
        }
    }
}