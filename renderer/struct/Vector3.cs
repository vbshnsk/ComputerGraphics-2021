using System;

namespace ComputerGraphics.renderer.@struct
{
    public struct Vector3
    {
        public float X, Y, Z;

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float Length => (float) Math.Sqrt(X * X + Y * Y + Z * Z);

        public Vector3 Norm()
        {
            var length = Length;
            if (length == 0)
            {
                return this;
            }
            return new Vector3(X/length, Y/length, Z/length);
        }

        public static Vector3 operator +(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }
        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        public Vector3 CrossProduct(Vector3 edge2)
        {
            var u = this;
            var v = edge2;
            return new Vector3(
                u.Y * v.Z - u.Z * v.Y,
                u.Z * v.X - u.X * v.Z,
                u.X * v.Y - u.Y * v.X);
        }

        public float DotProduct(Vector3 other)
        {
            return X * other.X + Y * other.Y + Z * other.Z;
        }
    }
    
    
}