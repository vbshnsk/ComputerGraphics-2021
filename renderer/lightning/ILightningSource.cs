using ComputerGraphics.converter.@struct;
using ComputerGraphics.renderer.scene;

namespace ComputerGraphics.renderer.@struct
{
    public interface ILightningSource
    {
        public RGBA Illuminate(ISceneObject obj);
    }
}