using MysteryDungeon.Core.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryDungeon.Core.Map
{
    /// <summary>
    /// Matches an object in a grid against it's surrounding nodes, and returns a suitable type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class Matcher
    {
        private Dictionary<string, TileType> TileDictionary;
        private char[,] _map;
        private Func<int, int, bool> _matcher;

        public Matcher(char[,] map, Func<int, int, bool> matchFunc)
        {
            _map = map;
            _matcher = matchFunc;
        }

        public void AddRule(string match, TileType type)
        {
            if (!TileDictionary.ContainsKey(match))
                TileDictionary.Add(match, type);
        }
    }
}
