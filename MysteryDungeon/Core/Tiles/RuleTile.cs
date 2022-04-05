using System.Collections.Generic;
using System.Linq;

namespace MysteryDungeon.Core.Tiles
{
    internal class RuleTile
    {
        private readonly char[,] _charmap;
        private readonly List<Rule> _rules;

        public RuleTile(char[,] charmap)
        {
            _charmap = charmap;
            _rules = new List<Rule>();
        }

        public void AddRule(Rule rule)
        {
            _rules.Add(rule);
        }

        public TileType Match(int xPos, int yPos)
        {
            foreach (var rule in _rules)
            {
                if (rule.PositiveCheckPoints.Any(point => _charmap[xPos + point.X, yPos + point.Y] != '#'))
                    continue;

                if (rule.NegativeCheckPoints.Any(point => _charmap[xPos + point.X, yPos + point.Y] != '.'))
                    continue;

                return rule.TileType;
            }

            return TileType.None;
        }
    }
}
