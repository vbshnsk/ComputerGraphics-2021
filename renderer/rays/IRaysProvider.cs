using ComputerGraphics.renderer.@struct;

namespace ComputerGraphics.renderer.rays
{
    public interface IRaysProvider
    {
        public Vector3 Get(int x, int y);
    }
}