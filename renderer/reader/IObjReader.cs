using ComputerGraphics.renderer.@struct;

namespace ComputerGraphics.renderer.reader
{
    public interface IObjReader
    {
        public Triangle[] Read(string path);
    }
}