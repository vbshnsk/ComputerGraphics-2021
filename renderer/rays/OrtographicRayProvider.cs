using System;
using ComputerGraphics.renderer.camera;
using ComputerGraphics.renderer.@struct;

namespace ComputerGraphics.renderer.rays
{
    public class OrtographicRayProvider : IRaysProvider
    {
        private ICameraProvider _cameraProvider;

        public OrtographicRayProvider(ICameraProvider cameraProvider)
        {
            _cameraProvider = cameraProvider;
        }
        public Vector3[,] Get(int width, int height, float fov)
        {
            var camera = _cameraProvider.Get();

            var rays = new Vector3[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var xNorm = (x - width / 2) / (float) width * 2;
                    var yNorm = -(y - height / 2) / (float) height * 2;

                    var vec = new Vector3(xNorm, yNorm, 0);
                    vec += camera.Position;
                    vec = camera.Direction - vec;
                    
                    rays[y, x] = vec.Norm();
                }
            }

            return rays;
        }
    }
}