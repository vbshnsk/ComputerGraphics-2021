using ComputerGraphics.renderer.@struct;

namespace ComputerGraphics.renderer.config
{
    public class StaticSceneConfigProvider : ISceneConfigProvider
    {
        public SceneConfig Get()
        {
            return new SceneConfig
            {
                Fov = 60,
                Width = 1920,
                Height = 1080
            };
        }
    }
}