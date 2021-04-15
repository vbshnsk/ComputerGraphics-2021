using System;
using System.Collections;
using System.Runtime.CompilerServices;
using ComputerGraphics.renderer.container.kdtree;
using ComputerGraphics.renderer.scene;
using ComputerGraphics.renderer.util;

namespace ComputerGraphics.renderer.@struct
{
    public class Triangle : Tuple<Vector3, Vector3, Vector3>, ISceneObject
    {
        public Triangle(Vector3 a, Vector3 b, Vector3 c) : base(a, b, c) { }

        public Vector3 MidPoint => new Vector3(
            (Item1.X + Item2.X + Item3.X) / 3,
            (Item1.Y + Item2.Y + Item3.Y) / 3,
            (Item1.Z + Item2.Z + Item3.Z) / 3
        );

        public float MinX => Math.Min(Math.Min(Item1.X, Item2.X), Item3.X);
        public float MinY => Math.Min(Math.Min(Item1.Y, Item2.Y), Item3.Y);
        public float MinZ => Math.Min(Math.Min(Item1.Z, Item2.Z), Item3.Z);
        public float MaxX => Math.Max(Math.Max(Item1.X, Item2.X), Item3.X);
        public float MaxY => Math.Max(Math.Max(Item1.Y, Item2.Y), Item3.Y);
        public float MaxZ => Math.Max(Math.Max(Item1.Z, Item2.Z), Item3.Z);

        public Vector3 Normal => (Item2 - Item1).CrossProduct(Item3 - Item1).Norm();

        public int CompareTo(ISceneObject other, Dimension axis)
        {
            return axis switch
            {
                Dimension.X => (int) (MidPoint.X - other.MidPoint.X),
                Dimension.Y => (int) (MidPoint.Y - other.MidPoint.Y),
                Dimension.Z => (int) (MidPoint.Z - other.MidPoint.Z),
                _ => throw new ArgumentOutOfRangeException(nameof(axis), axis, null)
            };
        }

        public bool HitBy(Vector3 ray, Vector3 from)
        {
            return RayIntersection.WithTriangle(from, ray, this);
        }
    }
}