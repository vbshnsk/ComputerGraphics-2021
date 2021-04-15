using ComputerGraphics.renderer.container.kdtree;
using ComputerGraphics.renderer.reader;
using ComputerGraphics.renderer.scene;
using ComputerGraphics.renderer.@struct;

namespace ComputerGraphics.renderer.container
{
    public class DummyObjectContainer : IObjectContainer
    {
        private IObjReader _reader;
        private ISceneObject[] _objs;

        public DummyObjectContainer(IObjReader reader)
        {
            _reader = reader;
        }

        public ISceneObject[] GetObjectsByRay(Vector3 ray)
        {
            return _objs;
        }

        public void LoadObj(string path)
        {
            _objs = _reader.Read(path);
        }
    }
}