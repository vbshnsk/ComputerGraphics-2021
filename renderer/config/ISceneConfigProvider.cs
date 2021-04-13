using ComputerGraphics.renderer.@struct;

namespace ComputerGraphics.renderer.config
{
    public interface ISceneConfigProvider
    {
        public SceneConfig Get();
    }
}