using System;
using ComputerGraphics.renderer.camera;
using ComputerGraphics.renderer.@struct;

namespace ComputerGraphics.renderer.rays
{
    public class PerspectiveRayProvider : IRaysProvider
    {
        private ICameraProvider _cameraProvider;
        
        public PerspectiveRayProvider(ICameraProvider cameraProvider)
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
                    var scale = MathF.Tan(fov * MathF.PI / 2 / 180);
                    var aspectRatio = (float) width / height;
                    var px = (2 * (x + 0.5f) / width - 1f) * scale * aspectRatio;
                    var py = (1 - 2 * (y + 0.5f) / height) * scale;
                    var direction = camera.Direction + new Vector3(px, py, 0) - camera.Position;

                    rays[y, x] = direction.Norm();
                }
            }

            return rays;
        }
    }
}