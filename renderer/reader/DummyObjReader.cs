using ComputerGraphics.renderer.@struct;

namespace ComputerGraphics.renderer.reader
{
    public class DummyObjReader : IObjReader
    {
        public Triangle[] Read(string path)
        {
            return new Triangle[]
            {
                new Triangle(
                    new Vector3(3.25000f, -2.48000f, 14.0000f),
                    new Vector3(3.25000f, -2.48000f, 9.01000f),
                    new Vector3(3.25000f, 2.48000f, 9.01000f)),
                new Triangle(
                    new Vector3(1.25000f, -1.48000f, 14.0000f),
                    new Vector3(1.25000f, -1.48000f, 9.01000f),
                    new Vector3(1.25000f, 1.48000f, 9.01000f))
            };
        }
    }
}