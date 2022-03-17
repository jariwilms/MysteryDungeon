using MysteryDungeon.Core.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryDungeon.Core.Map
{
    class RuleTile
    {
        private char[,] _charmap;
        private List<Rule> _rules;

        public RuleTile(char[,] charmap)
        {
            _charmap = charmap;
            _rules = new List<Rule>();
        }

        public void AddRule(Rule rule)
        {
            _rules.Add(rule);
        }

        public Tile Match(int xPos, int yPos)
        {
            bool matches;

            foreach (var rule in _rules)
            {
                matches = true;

                foreach (var point in rule.PositiveCheckPoints)
                {
                    if (_charmap[xPos + point.X, yPos + point.Y] != '#')
                    {
                        matches = false;
                        break;
                    }
                }
                
                foreach (var point in rule.NegativeCheckPoints)
                {
                    if (_charmap[xPos + point.X, yPos + point.Y] != '.')
                    {
                        matches = false;
                        break;
                    }
                }

                if (matches)
                    return rule.Tile;
            }

            return new Tile(TileType.None, TileCollision.Impassable);
        }
    }
}
