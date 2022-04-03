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
            _textureDictionary.Add(Pokemon.Charmander, Content.Load<Texture2D>("sprites/charmander"));
        }

        //template
        //animations.Add("", new Animation(_textureDictionary[pokemon], new Point(), , , , , , , f));
        public static Dictionary<string, Animation> LoadAnimations(Pokemon pokemon)
        {
            Dictionary<string, Animation> animations = new Dictionary<string, Animation>();

            switch (pokemon)
            {
                case Pokemon.Pikachu:
                    break;
                case Pokemon.Chikorita:                                                         // offset   w   h  sx yx  r  c  time
                    animations.Add("IdleUp", new Animation(_textureDictionary[pokemon], new Point(262, 19), 13, 22, 3, 0, 1, 2, 0.8f));
                    animations.Add("IdleRight", new Animation(_textureDictionary[pokemon], new Point(176, 20), 21, 21, 2, 0, 1, 2, 0.8f, true, SpriteEffects.FlipHorizontally));
                    animations.Add("IdleDown", new Animation(_textureDictionary[pokemon], new Point(98, 20), 15, 21, 2, 0, 1, 2, 0.8f));
                    animations.Add("IdleLeft", new Animation(_textureDictionary[pokemon], new Point(176, 20), 21, 21, 2, 0, 1, 2, 0.8f));

                    animations.Add("MoveUp", new Animation(_textureDictionary[pokemon], new Point(260, 47), 13, 20, 3, 0, 1, 2, 0.4f));
                    animations.Add("MoveRight", new Animation(_textureDictionary[pokemon], new Point(175, 49), 20, 18, 3, 0, 1, 2, 0.4f, true, SpriteEffects.FlipHorizontally));
                    animations.Add("MoveDown", new Animation(_textureDictionary[pokemon], new Point(102, 46), 13, 21, 3, 0, 1, 2, 0.4f));
                    animations.Add("MoveLeft", new Animation(_textureDictionary[pokemon], new Point(175, 49), 20, 18, 3, 0, 1, 2, 0.4f));
                    
                    break;
                case Pokemon.Charmander:
                    animations.Add("IdleUp", new Animation(_textureDictionary[pokemon], new Point(83, 58), 15, 22, 0, 0, 1, 1));
                    animations.Add("IdleRight", new Animation(_textureDictionary[pokemon], new Point(80, 82), 20, 21, 0, 0, 1, 1, 1.0f, true, SpriteEffects.FlipHorizontally));
                    animations.Add("IdleDown", new Animation(_textureDictionary[pokemon], new Point(82, 34), 21, 22, 0, 0, 1, 1));
                    animations.Add("IdleLeft", new Animation(_textureDictionary[pokemon], new Point(80, 82), 20, 21, 0, 0, 1, 1));

                    animations.Add("MoveUp", new Animation(_textureDictionary[pokemon], new Point(66, 58), 15, 22, 2, 0, 1, 3, 0.2f));
                    animations.Add("MoveRight", new Animation(_textureDictionary[pokemon], new Point(57, 83), 21, 20, 2, 0, 1, 3, 0.2f, true, SpriteEffects.FlipHorizontally));
                    animations.Add("MoveDown", new Animation(_textureDictionary[pokemon], new Point(62, 35), 19, 22, 2, 0, 1, 3, 0.2f));
                    animations.Add("MoveLeft", new Animation(_textureDictionary[pokemon], new Point(57, 83), 21, 20, 2, 0, 1, 3, 0.2f));

                    break;
                default:
                    break;
            }

            return animations;
        }
    }
}
