using ComputerGraphics.renderer.container.kdtree;
using ComputerGraphics.renderer.@struct;

namespace ComputerGraphics.renderer.scene
{
    public interface ISceneObject
    {
        public int CompareTo(ISceneObject other, Dimension dimension);
        public Vector3 MidPoint { get; }
        public Vector3 Normal { get; }
        
        public float MinX { get; }
        public float MinY { get; }
        public float MinZ { get; }
        public float MaxX { get; }
        public float MaxY { get; }
        public float MaxZ { get; }
        
        public bool HitBy(Vector3 ray, Vector3 from);
    }
}