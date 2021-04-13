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
                    new Vector3(0,0, -1).Norm(),
                    new Vector3(-1, -1, -1).Norm(),
                    new Vector3(-1, 0, -1).Norm())
            };
        }
    }
}