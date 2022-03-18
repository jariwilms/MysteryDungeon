using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryDungeon.Core.Extensions
{
    public class Pathfinder
    {
        private char[,] _charmap;

        private PathNode[,] _pathNodes;
        private List<PathNode> _openNodes;
        private HashSet<PathNode> _closedNodes;

        public Pathfinder()
        {

        }

        public Pathfinder(char[,] charmap) //remove start and end arguments from constructor, move to findpath
        {
            SetCharmap(charmap);
        }

        public void SetCharmap(char[,] charmap)
        {
            _charmap = charmap;

            _pathNodes = new PathNode[_charmap.GetLength(0), _charmap.GetLength(1)];

            for (int y = 0; y < _charmap.GetLength(1); y++)
            {
                for (int x = 0; x < _charmap.GetLength(0); x++)
                {
                    _pathNodes[x, y] = new PathNode(new Point(x, y));
                }
            }
        }

        public bool FindPath(Point startPosition, Point endPosition, out List<PathNode> pathNodes)
        {
            pathNodes = new List<PathNode>();
            _openNodes = new List<PathNode>();
            _closedNodes = new HashSet<PathNode>();

            PathNode currentNode = new PathNode(startPosition);
            _openNodes.Add(currentNode);

            while (_openNodes.Count > 0)
            {
                currentNode = _openNodes[0];

                for (int i = 1; i < _openNodes.Count; i++)
                {
                    if (_openNodes[i].F <= currentNode.F && _openNodes[i].H < currentNode.H)
                        currentNode = _openNodes[i];
                }

                _openNodes.Remove(currentNode);
                _closedNodes.Add(currentNode);

                if (currentNode.Position == endPosition)
                {
                    pathNodes = ReconstructPath(currentNode);
                    return true;
                }

                foreach (var neighbourNode in GetNeighbouringNodes(currentNode))
                {
                    if (!neighbourNode.Traversable || _closedNodes.Contains(neighbourNode))
                        continue;

                    if (currentNode.G + 1 < neighbourNode.G || !_openNodes.Contains(neighbourNode))
                    {
                        neighbourNode.G = currentNode.G + 1;
                        neighbourNode.H = GetDistanceBetweenPoints(neighbourNode.Position, endPosition);

                        neighbourNode.Parent = currentNode;

                        _openNodes.Add(neighbourNode);
                    }
                }
            }

            return false;
        }

        public List<PathNode> ReconstructPath(PathNode endNode)
        {
            List<PathNode> pathNodes = new List<PathNode>();
            PathNode currentNode = endNode;

            do
            {
                pathNodes.Add(currentNode);
                currentNode = currentNode.Parent;
            } 
            while (currentNode != null);

            pathNodes.Reverse();
            return pathNodes;
        }

        public List<PathNode> GetNeighbouringNodes(PathNode node)
        {
            List<PathNode> pathNodes = new List<PathNode>();
            PathNode currentNode;

            for (int y = -1; y < 2; y++)
            {
                for (int x = -1; x < 2; x++)
                {
                    Point position = new Point(node.Position.X + x, node.Position.Y + y);

                    currentNode = _pathNodes[position.X, position.Y];
                    currentNode.Traversable = _charmap[position.X, position.Y] == '.' ? true : false;

                    pathNodes.Add(currentNode);
                }
            }

            pathNodes.RemoveAt(4);
            return pathNodes;
        }

        public int GetDistanceBetweenPoints(Point a, Point b)
        {
            double xDistance = b.X - a.X;
            double yDistance = b.Y - a.Y;
            double totalDistance = Math.Sqrt(Math.Pow(xDistance, 2) + Math.Pow(yDistance, 2));

            return (int)Math.Round(totalDistance);
        }
    }
}
