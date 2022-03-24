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

        public static Sprite LoadSprite(Pokemon pokemon)
        {
            return new Sprite(_textureDictionary[pokemon]);
        }

        public static void LoadAnimations(Pokemon pokemon, AnimatorComponent animatorComponent)
        {
            switch (pokemon)
            {
                case Pokemon.Pikachu:
                    break;

                case Pokemon.Chikorita:
                    animatorComponent.AddAnimation(new Animation("IdleUp", _textureDictionary[pokemon], new Point(260, 19), 17, 22, 1, 0, 1, 2, 0.8f));
                    animatorComponent.AddAnimation(new Animation("IdleRight", _textureDictionary[pokemon], new Point(174, 20), 25, 21, 1, 0, 1, 2, 0.8f, true, SpriteEffects.FlipHorizontally));
                    animatorComponent.AddAnimation(new Animation("IdleDown", _textureDictionary[pokemon], new Point(98, 19), 16, 21, 1, 0, 1, 2, 0.8f));
                    animatorComponent.AddAnimation(new Animation("IdleLeft", _textureDictionary[pokemon], new Point(174, 20), 25, 21, 1, 0, 1, 2, 0.8f));

                    animatorComponent.AddAnimation(new Animation("MoveUp", _textureDictionary[pokemon], new Point(260, 47), 13, 20, 3, 0, 1, 2, 0.4f));
                    animatorComponent.AddAnimation(new Animation("MoveRight", _textureDictionary[pokemon], new Point(175, 49), 20, 18, 3, 0, 1, 2, 0.4f, true, SpriteEffects.FlipHorizontally));
                    animatorComponent.AddAnimation(new Animation("MoveDown", _textureDictionary[pokemon], new Point(102, 46), 13, 20, 3, 0, 1, 2, 0.4f));
                    animatorComponent.AddAnimation(new Animation("MoveLeft", _textureDictionary[pokemon], new Point(175, 49), 20, 18, 3, 0, 1, 2, 0.4f));

                    break;

                default:
                    break;
            }
        }
    }
}
