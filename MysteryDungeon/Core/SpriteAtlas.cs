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
        public Dictionary<T, Rectangle> SpriteDictionary;

        private Vector2 _textureOffset;
        private int _spaceBetweenSpritesX;
        private int _spaceBetweenSpritesY;

        public int SpriteSize;

        private SpriteAtlas()
        {
            SpriteDictionary = new Dictionary<T, Rectangle>();
        }

        public SpriteAtlas(Texture2D sourceTexture, int spriteSize) : this()
        {
            SourceTexture = sourceTexture;
            SpriteSize = spriteSize;
        }

        public SpriteAtlas(Texture2D sourceTexture, Vector2 textureOffset, int spaceBetweenSpritesX, int spaceBetweenSpritesY, int spriteSize, int rows, int columns) : this()
        {
            SourceTexture = sourceTexture;

            _textureOffset = textureOffset;
            _spaceBetweenSpritesX = spaceBetweenSpritesX;
            _spaceBetweenSpritesY = spaceBetweenSpritesY;

            SpriteSize = spriteSize;
        }

        public void SetCurrentSprite(T identifier)
        {
            bool found = SpriteDictionary.TryGetValue(identifier, out Rectangle source);

            if (!found)
                throw new ArgumentException(String.Format("Texture with given name does not exist: {0}", identifier));

            SourceRectangle = source;
        }

        /// <summary>
        /// Add sprites that are aligned in a grid. Columns and rows are indexed from 0
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="column"></param>
        /// <param name="row"></param>
        public void AddSprite(T identifier, int column, int row)
        {
            int xPosition = (int)_textureOffset.X + column * _spaceBetweenSpritesX + column * SpriteSize;
            int yPosition = (int)_textureOffset.Y + row * _spaceBetweenSpritesY + row * SpriteSize;

            SpriteDictionary.Add(identifier, new Rectangle(xPosition, yPosition, SpriteSize, SpriteSize));
        }

        /// <summary>
        /// Add sprites that are not aligned to a grid
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void AddSprite(T identifier, int x, int y, int width, int height)
        {
            SpriteDictionary.Add(identifier, new Rectangle(x, y, width, height));
        }

        public void RemoveSprite(T identifier)
        {
            SpriteDictionary.Remove(identifier);
        }
    }
}
