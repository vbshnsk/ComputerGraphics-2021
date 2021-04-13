using System;
using ComputerGraphics.converter.@enum;
using ComputerGraphics.converter.reader;
using ComputerGraphics.converter.writer;
using ComputerGraphics.di;

namespace ComputerGraphics.converter
{
    public static class ImageConverterFactory
    {
        public static IImageConverter Get(string source, string targetExt)
        {
            try
            {
                Enum.TryParse(ExtractExtension(source), true, out ImageType sourceType);
                Enum.TryParse(targetExt, true, out ImageType targetType);
                var container = new Container();
                container.Register<IImageReader, BmpImageReader>();
                switch (sourceType)
                {
                    case ImageType.Bmp:
                        container.Register<IImageReader, BmpImageReader>();
                        break;
                    case ImageType.Ppm:
                        container.Register<IImageReader, PpmImageReader>();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                switch (targetType)
                {
                    case ImageType.Bmp:
                        container.Register<IImageWriter, BmpImageWriter>();
                        break;
                    case ImageType.Ppm:
                        container.Register<IImageWriter, PpmImageWriter>();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                return container.Get<ImageConverter>();
            }
            catch (ReaderException)
            {
                throw;
            }
            catch (WriterException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new ConverterException("Got invalid parameters");
            }
        }
        
        private static string ExtractExtension(string path)
        {
            var lastDot = path.LastIndexOf('.');
            return path.Substring(lastDot + 1);
        }
    }
}