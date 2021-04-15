using ComputerGraphics.renderer.@struct;

namespace ComputerGraphics.renderer.lightning
{
    public interface ILightningSourceProvider
    {
        ILightningSource Get();
    }
}