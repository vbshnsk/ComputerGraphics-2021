using System;

namespace ComputerGraphics
{
    public class ImageConverter
    {
        private readonly IImageReader _reader;
        private readonly IImageWriter _writer;
        private readonly string _source;

        public ImageConverter(string source, string targetExt)
        {
            try
            {
                Console.WriteLine(ExtractExtension(source));
                Enum.TryParse(ExtractExtension(source), true, out ImageType sourceType);
                Enum.TryParse(targetExt, true, out ImageType targetType);
                _reader = ImageReaderFactory.GetReader(sourceType);
                _writer = ImageWriterFactory.GetWriter(targetType);
                _source = source;
            }
            catch (ReaderException)
            {
                throw;
            }
            catch (WriterException)
            {
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new ConverterException("Got invalid parameters");
            }
        }
        
        public void Convert(string output)
        {
            _reader.Read(_source);
            _writer.Write(output, _reader.Pixels, _reader.Width, _reader.Depth);
        }

        private string ExtractExtension(string path)
        {
            var lastDot = path.LastIndexOf('.');
            return path.Substring(lastDot + 1);
        }
    }

    public class ConverterException : Exception
    {
        public ConverterException(string message) : base(message)
        {
        }
    }
}