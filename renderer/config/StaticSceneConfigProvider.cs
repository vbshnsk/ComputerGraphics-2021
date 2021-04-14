using ComputerGraphics.renderer.@struct;

namespace ComputerGraphics.renderer.config
{
    public class StaticSceneConfigProvider : ISceneConfigProvider
    {
        public SceneConfig Get()
        {
            return new SceneConfig
            {
                Fov = 90,
                Width = 110,
                Height = 110
            };
        }
    }
}