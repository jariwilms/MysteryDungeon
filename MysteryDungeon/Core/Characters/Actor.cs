﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MysteryDungeon.Core.Animations;
using System;

namespace MysteryDungeon.Core.Characters
{
    public class Actor : Component
    {
        public Sprite Sprite;

        public Actor() : base()
        {

        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
