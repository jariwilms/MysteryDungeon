﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MysteryDungeon.Core.Tiles
{
    class Rule
    {
        public enum Behaviour
        {
            Positive,
            Negative,
            Ignore
        }

        public Tile Tile;
        public List<Point> PositiveCheckPoints;
        public List<Point> NegativeCheckPoints;

        private Rule()
        {
            PositiveCheckPoints = new List<Point>();
            NegativeCheckPoints = new List<Point>();
        }

        public Rule(Tile tile, string conditions) : this()
        {
            if (conditions.Length != 9)
                throw new ArgumentException("The given conditions are not complete.");

            Tile = tile;

            int counter = 0;
            for (int y = -1; y < 2; y++)
            {
                for (int x = -1; x < 2; x++)
                {
                    if (conditions[counter] == '+')
                        PositiveCheckPoints.Add(new Point(x, y));

                    if (conditions[counter] == '-')
                        NegativeCheckPoints.Add(new Point(x, y));

                    counter++;
                }
            }
        }
    }
}
