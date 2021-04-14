using System;
using ComputerGraphics.renderer.camera;
using ComputerGraphics.renderer.config;
using ComputerGraphics.renderer.@struct;

namespace ComputerGraphics.renderer.rays
{
    public class OrtographicRayProvider : IRaysProvider
    {
        private ICameraProvider _cameraProvider;
        private ISceneConfigProvider _config;

        public OrtographicRayProvider(ICameraProvider cameraProvider, ISceneConfigProvider config)
        {
            _cameraProvider = cameraProvider;
            _config = config;
        }
        public Vector3 Get(int x, int y)
        {
            var camera = _cameraProvider.Get();
            var config = _config.Get();
            
            var xNorm = (x - config.Width / 2) / (float) config.Width * 2;
            var yNorm = -(y - config.Height / 2) / (float) config.Height * 2;

            var vec = new Vector3(xNorm, yNorm, 0);
            vec += camera.Position;
            vec = camera.Direction - vec;
                    
            
            return vec.Norm();
        }
    }
}