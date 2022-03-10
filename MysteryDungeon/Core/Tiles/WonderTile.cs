using Microsoft.Xna.Framework;
using MysteryDungeon.Core.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryDungeon.Core.Tiles
{
    class WonderTile : SpecialTile
    {
        public WonderTile(Point position, SpecialTileType specialTileType, bool isVisible = true) : base(position, specialTileType, isVisible)
        {

        }

        public override void Activate()
        {
            Console.WriteLine("Stepped on a wonder tile");
        }
    }
}
