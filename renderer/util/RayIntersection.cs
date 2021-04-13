using System;
using ComputerGraphics.renderer.@struct;

namespace ComputerGraphics.renderer.util
{
    internal static class RayIntersection
    {
        
        // from https://en.wikipedia.org/wiki/M%C3%B6ller%E2%80%93Trumbore_intersection_algorithm
        public static bool WithTriangle(Vector3 rayOrigin, Vector3 rayVector, Triangle inTriangle)
        {
            var (vertex0, vertex1, vertex2) = inTriangle;
            var edge1 = vertex1 - vertex0;
            var edge2 = vertex2 - vertex0;
            var h = rayVector.CrossProduct(edge2);
            var a = edge1.DotProduct(h);
            if (a < 1e-8 && a > -1e-8)
            {
                return false;
            }
            var f = 1 / a;
            var s = rayOrigin - vertex0;
            var u = f * s.DotProduct(h);
            if (u < 0.0 || u > 1.0)
            {
                return false;
            }
            var q = s.CrossProduct(edge1);
            var v = f * rayVector.DotProduct(q);
            if (v < 0.0 || u + v > 1.0)
            {
                return false;
            }

            var t = f * edge2.DotProduct(q);
            return true;

        }
    }
}