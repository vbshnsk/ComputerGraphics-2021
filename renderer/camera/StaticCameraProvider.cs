using ComputerGraphics.renderer.@struct;

namespace ComputerGraphics.renderer.camera
{
    public class StaticCameraProvider : ICameraProvider
    {
        public Camera Get()
        {
            var camera = new Camera {Position = new Vector3(0, 0, 1), Direction = new Vector3(0, 0, 1)};
            return camera;
        }
    }
}