using ComputerGraphics.renderer.container.kdtree;
using ComputerGraphics.renderer.scene;
using ComputerGraphics.renderer.@struct;

namespace ComputerGraphics.renderer.container
{
    public interface IObjectContainer
    {
        ISceneObject[] GetObjectsByRay(Vector3 ray);

        void LoadObj(string path);
    }
}