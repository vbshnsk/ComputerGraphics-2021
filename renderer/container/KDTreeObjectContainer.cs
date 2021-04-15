using System;
using ComputerGraphics.renderer.camera;
using ComputerGraphics.renderer.container.kdtree;
using ComputerGraphics.renderer.reader;
using ComputerGraphics.renderer.scene;
using ComputerGraphics.renderer.@struct;

namespace ComputerGraphics.renderer.container
{
    public class KdTreeObjectContainer : IObjectContainer
    {
        private IObjReader _reader;
        private KDTree<ISceneObject> _tree;
        private ICameraProvider _cameraProvider;


        public KdTreeObjectContainer(IObjReader reader, ICameraProvider cameraProvider)
        {
            _reader = reader;
            _cameraProvider = cameraProvider;
        }

        public ISceneObject[] GetObjectsByRay(Vector3 ray)
        {
            return _tree.TraverseByRay(ray, _cameraProvider.Get().Position);
        }

        public void LoadObj(string path)
        {
            ISceneObject[] objs = _reader.Read(path);
            _tree = new KDTree<ISceneObject>(objs);
        }
        
    }
}