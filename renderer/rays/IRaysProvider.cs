using ComputerGraphics.renderer.@struct;

namespace ComputerGraphics.renderer.rays
{
    public interface IRaysProvider
    {
        public Vector3[,] Get(int width, int height, float fov);
    }
}