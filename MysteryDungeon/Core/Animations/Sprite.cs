using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryDungeon.Core.Animations
{
    public class Sprite
    {
        public Texture2D Texture { get; set; }
        public Rectangle BoundingRectangle { get; set; }

        public AnimationPlayer AnimationPlayer { get; set; }

        public Sprite(Texture2D texture)
        {
            Texture = texture;
            BoundingRectangle = new Rectangle(0, 0, 24, 24);

            AnimationPlayer = new AnimationPlayer();
        }
    }
}
