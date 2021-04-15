using System;
using ComputerGraphics.converter.@struct;
using ComputerGraphics.renderer.scene;

namespace ComputerGraphics.renderer.@struct
{
    public struct PointLightningSource : ILightningSource
    {
        public Vector3 Origin;

        public RGBA Illuminate(ISceneObject obj)
        {
            var center = obj.MidPoint;
            var direction = Origin - center;
            var normal = obj.Normal;
            var cos = direction.DotProduct(normal) / (direction.Length * normal.Length);
            var c = Math.Max(cos, 0.1);
            return new RGBA
            {
                Blue = (byte)(212 * c),
                Red = (byte)(127 * c),
                Green = (byte)(255 * c)
            };
        }
    }
}