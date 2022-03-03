using System;
using System.Text;
using System.Configuration;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MysteryDungeon.Core
{
    enum TileCollision
    {
        Passable = 0, 
        Impassable = 1
    }

    class Tile
    {
        public Texture2D Texture;
        public TileCollision TileCollision;

        public Tile(Texture2D texture, TileCollision tileCollision)
        {
            Texture = texture;
            TileCollision = tileCollision;
        }
    }
}
