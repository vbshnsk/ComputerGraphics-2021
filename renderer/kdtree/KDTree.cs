using System;
using ComputerGraphics.renderer.@struct;

namespace ComputerGraphics.renderer.kdtree
{

    public class KDTree<T> where T : IComparableInDimension
    {
        private KDTreeNode<T> Root;

        public KDTree(T[] values)
        {
            Root = new KDTreeNode<T>(values, 0);
        }
        
    }
    
    internal class KDTreeNode<T> where T : IComparableInDimension
    {
        private Vector3 _location;
        private KDTreeNode<T> _left;
        private KDTreeNode<T> _right;
        private T[] _values;
        
        public KDTreeNode(T[] values, int depth)
        {
            var dimension = (Dimension) (depth % 3);
            if (depth == 10 || values.Length < 2)
            {
                _values = values;
                return;
            }
            Array.Sort(values, (a, b) => a.CompareTo(b, dimension));
            var median = values.Length / 2;
            _location = values[median].MidPoint;
            _left = new KDTreeNode<T>(values[..median], depth + 1);
            _right = new KDTreeNode<T>(values[(median + 1)..], depth + 1);
        }
    }
    
}