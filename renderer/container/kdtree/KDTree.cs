using System;
using System.Collections.Generic;
using System.Linq;
using ComputerGraphics.renderer.scene;
using ComputerGraphics.renderer.@struct;
using ComputerGraphics.renderer.util;

namespace ComputerGraphics.renderer.container.kdtree
{

    public class KDTree<T> where T : ISceneObject
    {
        private KDTreeNode<T> Root;

        public KDTree(T[] values)
        {
            Console.WriteLine((int)Math.Log2(values.Length));
            Root = new KDTreeNode<T>(values, 0, (int)Math.Log2(values.Length));
        }

        public T[] TraverseByRay(Vector3 ray, Vector3 origin)
        {
            return Root.TraverseByRay(ray, origin);
        }

        public int Sum()
        {
            return Root.Sum();
        }
        
    }
    
    internal class KDTreeNode<T> where T : ISceneObject
    {
        private Vector3 _upperBound;
        private Vector3 _lowerBound;
        private KDTreeNode<T> _left;
        private KDTreeNode<T> _right;
        private T[] _values;

        public KDTreeNode(T[] values, int depth, int maxDepth)
        {
            var dimension = (Dimension) (depth % 3);
            _lowerBound = new Vector3(
                values.Min(v => v.MinX),
                values.Min(v => v.MinY),
                values.Min(v => v.MinZ)
            );
            _upperBound = new Vector3(
                values.Max(v => v.MaxX),
                values.Max(v => v.MaxY),
                values.Max(v => v.MaxZ)
            );
            if (depth == maxDepth)
            {
                _values = values;
                return;
            }
            Array.Sort(values, (a, b) => a.CompareTo(b, dimension));
            var median = values.Length / 2;
            _left = new KDTreeNode<T>(values[..median], depth + 1, maxDepth);
            _right = new KDTreeNode<T>(values[median..], depth + 1, maxDepth);
        }

        public int Sum()
        {
            if (_left == null || _right == null)
            {
                return _values.Length;
            }

            return _left.Sum() + _right.Sum();
        }

        public T[] TraverseByRay(Vector3 ray, Vector3 origin)
        {
            if (_left == null || _right == null)
            {
                return _values;
            }

            var hitsLeft = RayIntersection.WithBox(_left._lowerBound, _left._upperBound, origin, ray);
            var hitsRight = RayIntersection.WithBox(_right._lowerBound, _right._upperBound, origin, ray);

            List<T> l = new List<T>();

            if (hitsLeft || hitsRight)
            {
                l = new List<T>();
                
                if (hitsLeft)
                {
                    var res = _left.TraverseByRay(ray, origin);
                    l.AddRange(res);
                }

                if (hitsRight)
                {
                    var res = _right.TraverseByRay(ray, origin);
                    l.AddRange(res);  
                }
            }

            return l.ToArray();
        }
    }
    
}