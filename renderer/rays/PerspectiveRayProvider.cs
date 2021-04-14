using System;
using ComputerGraphics.renderer.camera;
using ComputerGraphics.renderer.config;
using ComputerGraphics.renderer.@struct;

namespace ComputerGraphics.renderer.rays
{
    public class PerspectiveRayProvider : IRaysProvider
    {
        private ICameraProvider _cameraProvider;
        private ISceneConfigProvider _config;

        public PerspectiveRayProvider(ICameraProvider cameraProvider, ISceneConfigProvider config)
        {
            _cameraProvider = cameraProvider;
            _config = config;
        }

        public Vector3 Get(int x, int y)
        {
            var camera = _cameraProvider.Get();
            var config = _config.Get();
            
            var scale = MathF.Tan(config.Fov * MathF.PI / 2 / 180);
            var aspectRatio = (float) config.Width / config.Height;
            
            var projectedX = (2 * (x + 0.5f) / config.Width - 1f) * scale * aspectRatio;
            var projectedY = (1 - 2 * (y + 0.5f) / config.Height) * scale;
            
            var direction = camera.Direction + new Vector3(projectedX, 0, projectedY) - camera.Position;

            return direction.Norm();
        }
    }
}