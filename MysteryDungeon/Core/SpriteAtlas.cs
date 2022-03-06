using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MysteryDungeon.Core
{
    class SpriteAtlas<T>
    {
        public Texture2D SourceTexture;
        public Rectangle SourceRectangle { get; private set; }
        public Dictionary<T, Rectangle> SourceRectangles;
        public int SpriteSize;

        private SpriteAtlas()
        {
            SourceRectangles = new Dictionary<T, Rectangle>();
        }

        public SpriteAtlas(Texture2D sourceTexture, int spriteSize) : this()
        {
            SourceTexture = sourceTexture;
            SpriteSize = spriteSize;
        }

        public void SetCurrentSprite(T identifier)
        {
            bool found = SourceRectangles.TryGetValue(identifier, out Rectangle source);

            if (!found)
                throw new ArgumentException(String.Format("Texture with given name does not exist: {0}", identifier));

            SourceRectangle = source;
        }

        public void AddSprite(T identifier, int x, int y)
        {
            SourceRectangles.Add(identifier, new Rectangle(x, y, SpriteSize, SpriteSize));
        }

        public void RemoveSprite(T identifier)
        {
            SourceRectangles.Remove(identifier);
        }
    }
}
