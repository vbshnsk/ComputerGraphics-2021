using System;
using System.Collections.Generic;
using ComputerGraphics.converter.@struct;

namespace ComputerGraphics.converter.writer
{
    public class ConsoleImageWriter : IImageWriter
    {
        public void Write(string _path, IEnumerable<RGBA> pixels, int width, int depth)
        {
            var pixelEnumerator = pixels.GetEnumerator();
            
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < depth; j++)
                {
                    pixelEnumerator.MoveNext();
                    var pixel = pixelEnumerator.Current;
                    Console.Write(pixel.Blue == 0 ? 'X' : '.');
                }
                Console.Write('\n');
            }
            pixelEnumerator.Dispose();
        }
    }
}