using System.Collections.Generic;
using ComputerGraphics.converter.@struct;

namespace ComputerGraphics.converter.writer
{
    public interface IImageWriter
    {
        public void Write(string path, IEnumerable<RGBA> pixels, int width, int depth);
    }
}