using ComputerGraphics.renderer.@struct;

namespace ComputerGraphics.renderer.kdtree
{
    public interface IComparableInDimension
    {
        public int CompareTo(IComparableInDimension other, Dimension dimension);
        public Vector3 MidPoint { get; }
    }
}