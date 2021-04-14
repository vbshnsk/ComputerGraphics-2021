using ComputerGraphics.renderer.@struct;

namespace ComputerGraphics.renderer.camera
{
    public class StaticCameraProvider : ICameraProvider
    {
        public Camera Get()
        {
            var camera = new Camera {Position = new Vector3(0, 2f, -0.8f), Direction = new Vector3(0, -1, 0)};
            return camera;
        }
    }
}