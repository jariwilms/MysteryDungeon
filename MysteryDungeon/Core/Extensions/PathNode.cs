using Microsoft.Xna.Framework;
using System;

namespace MysteryDungeon.Core.Extensions
{
    public class PathNode : IEquatable<PathNode>
    {
        public PathNode Parent;

        public Point Position;
        public bool Traversable;

        /// <summary>
        /// Distance from starting node
        /// </summary>
        public int G { get { return _g; } set { _g = value; F = G + H; } }
        private int _g;

        /// <summary>
        /// Distance from end node
        /// </summary>
        public int H { get { return _h; } set { _h = value; F = G + H; } }
        private int _h;

        /// <summary>
        /// Sum of G cost and H cost
        /// </summary>
        public int F;

        public PathNode()
        {
            Position = new Point();
            G = H = F = 0;
        }

        public PathNode(Point position, bool traversable = true)
        {
            Position = position;
            Traversable = traversable;
        }

        public bool Equals(PathNode other)
        {
            return Position == other.Position
                && Traversable == other.Traversable
                && G == other.G && H == other.H;
        }
    }
}
