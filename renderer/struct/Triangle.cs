using System;
using System.Collections;
using System.Runtime.CompilerServices;
using ComputerGraphics.renderer.kdtree;

namespace ComputerGraphics.renderer.@struct
{
    public class Triangle : Tuple<Vector3, Vector3, Vector3>, IComparableInDimension
    {
        public Triangle(Vector3 a, Vector3 b, Vector3 c) : base(a, b, c) { }

        public Vector3 MidPoint => new Vector3(
            (Item1.X + Item2.X + Item3.X) / 3,
            (Item1.Y + Item2.Y + Item3.Y) / 3,
            (Item1.Z + Item2.Z + Item3.Z) / 3
        );

        public int CompareTo(IComparableInDimension other, Dimension axis)
        {
            return axis switch
            {
                Dimension.X => (int) (MidPoint.X - other.MidPoint.X),
                Dimension.Y => (int) (MidPoint.Y - other.MidPoint.Y),
                Dimension.Z => (int) (MidPoint.Z - other.MidPoint.Z),
                _ => throw new ArgumentOutOfRangeException(nameof(axis), axis, null)
            };
        }
    }
}