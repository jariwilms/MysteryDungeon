using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MysteryDungeon.Core.Animations;
using System.Collections.Generic;

namespace MysteryDungeon.Core.Data
{
    public enum Pokemon
    {
        Pikachu,

        Charmander,
        Squirtle,
        Chikorita
    }

    public static class PokemonSpriteData
    {
        private static Dictionary<Pokemon, Texture2D> _textureDictionary;

        public static ContentManager Content { get; set; }

        static PokemonSpriteData()
        {
            _textureDictionary = new Dictionary<Pokemon, Texture2D>();
        }

        public static void CreateDictionary()
        {
            _textureDictionary.Add(Pokemon.Chikorita, Content.Load<Texture2D>("sprites/chikorita"));
        }

        public static Dictionary<string, Animation> LoadAnimations(Pokemon pokemon)
        {
            Dictionary<string, Animation> animations = new Dictionary<string, Animation>();

            switch (pokemon)
            {
                case Pokemon.Pikachu:
                    break;

                case Pokemon.Chikorita:
                    animations.Add("IdleUp", new Animation(_textureDictionary[pokemon], new Point(260, 19), 17, 22, 1, 0, 1, 2, 0.8f));
                    animations.Add("IdleRight", new Animation(_textureDictionary[pokemon], new Point(174, 20), 25, 21, 1, 0, 1, 2, 0.8f, true, SpriteEffects.FlipHorizontally));
                    animations.Add("IdleDown", new Animation(_textureDictionary[pokemon], new Point(98, 19), 16, 21, 1, 0, 1, 2, 0.8f));
                    animations.Add("IdleLeft", new Animation(_textureDictionary[pokemon], new Point(174, 20), 25, 21, 1, 0, 1, 2, 0.8f));

                    animations.Add("MoveUp", new Animation(_textureDictionary[pokemon], new Point(260, 47), 13, 20, 3, 0, 1, 2, 0.4f));
                    animations.Add("MoveRight", new Animation(_textureDictionary[pokemon], new Point(175, 49), 20, 18, 3, 0, 1, 2, 0.4f, true, SpriteEffects.FlipHorizontally));
                    animations.Add("MoveDown", new Animation(_textureDictionary[pokemon], new Point(102, 46), 13, 20, 3, 0, 1, 2, 0.4f));
                    animations.Add("MoveLeft", new Animation(_textureDictionary[pokemon], new Point(175, 49), 20, 18, 3, 0, 1, 2, 0.4f));

                    break;

                default:
                    break;
            }

            return animations;
        }
    }
}
