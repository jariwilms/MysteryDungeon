﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MysteryDungeon.Core.Extensions
{
    public class Pathfinder
    {
        private char[,] _charmap;

        public PathNode[,] NodeGrid { get; protected set; }
        public List<PathNode> OpenNodes { get; protected set; }
        public HashSet<PathNode> ClosedNodes { get; protected set; }

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

            NodeGrid = new PathNode[_charmap.GetLength(0), _charmap.GetLength(1)];

            for (int y = 0; y < _charmap.GetLength(1); y++)
            {
                for (int x = 0; x < _charmap.GetLength(0); x++)
                {
                    NodeGrid[x, y] = new PathNode(new Point(x, y));
                }
            }
        }

        public bool FindPath(Point startPosition, Point endPosition, out List<PathNode> pathNodes)
        {
            pathNodes = new List<PathNode>();
            OpenNodes = new List<PathNode>();
            ClosedNodes = new HashSet<PathNode>();

            PathNode currentNode = new PathNode(startPosition);
            OpenNodes.Add(currentNode);

            while (OpenNodes.Count > 0)
            {
                currentNode = OpenNodes[0];

                for (int i = 1; i < OpenNodes.Count; i++)
                {
                    if (OpenNodes[i].F <= currentNode.F && OpenNodes[i].H < currentNode.H)
                        currentNode = OpenNodes[i];
                }

                OpenNodes.Remove(currentNode);
                ClosedNodes.Add(currentNode);

                if (currentNode.Position == endPosition)
                {
                    pathNodes = ReconstructPath(currentNode);
                    return true;
                }

                foreach (var neighbourNode in GetNeighbouringNodes(currentNode))
                {
                    if (!neighbourNode.Traversable || ClosedNodes.Contains(neighbourNode))
                        continue;

                    if (currentNode.G + 1 < neighbourNode.G || !OpenNodes.Contains(neighbourNode))
                    {
                        neighbourNode.G = currentNode.G + GetDistanceBetweenPoints(currentNode.Position, neighbourNode.Position);
                        neighbourNode.H = GetDistanceBetweenPoints(neighbourNode.Position, endPosition);

                        neighbourNode.Parent = currentNode;

                        OpenNodes.Add(neighbourNode);
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

        public List<PathNode> GetNeighbouringNodes(PathNode node) //Add a check and only return the tiles the player can actually walk on. (floor, water, lava etc.)
        {
            List<PathNode> pathNodes = new List<PathNode>();
            PathNode currentNode;

            //Get all surrounding nodes 

            //for (int y = -1; y < 2; y++)
            //{
            //    for (int x = -1; x < 2; x++)
            //    {
            //        Point position = new Point(node.Position.X + x, node.Position.Y + y);

            //        currentNode = _pathNodes[position.X, position.Y];
            //        currentNode.Traversable = _charmap[position.X, position.Y] == '.' ? true : false;

            //        pathNodes.Add(currentNode);
            //    }
            //}

            //pathNodes.RemoveAt(4);



            //Get up, right, down and left nodes only

            List<Point> points = new List<Point>()
            {
                new Point(0, 1),
                new Point(1, 0),
                new Point(0, -1),
                new Point(-1, 0)
            };

            foreach (var point in points)
            {
                Point position = new Point(node.Position.X, node.Position.Y) + point;
                currentNode = NodeGrid[position.X, position.Y];
                currentNode.Traversable = _charmap[position.X, position.Y] == '.' ? true : false;
                pathNodes.Add(currentNode);
            }

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
