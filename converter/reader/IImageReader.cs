using System.Collections.Generic;
using ComputerGraphics.converter.@struct;

namespace ComputerGraphics.converter.reader
{
    public interface IImageReader
    {
        List<RGBA> Pixels { get; }
        int Width { get; }
        int Depth { get; }

        public void Read(string path);
    }
}