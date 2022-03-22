using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MysteryDungeon.Core.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static Sprite GetSprite(Pokemon pokemon)
        {
            Sprite sprite = new Sprite(_textureDictionary[pokemon]);
            LoadAnimations(pokemon, sprite);

            return sprite;
        }

        private static void LoadAnimations(Pokemon pokemon, Sprite sprite)
        {
            switch (pokemon)
            {
                case Pokemon.Pikachu:
                    break;

                case Pokemon.Chikorita:
                        sprite.AnimationPlayer.AddAnimation(new Animation("Idle", _textureDictionary[pokemon], new Point(98, 20), 16, 21, 1, 0, 1, 2, 0.8f));
                        sprite.AnimationPlayer.AddAnimation(new Animation("MoveUp", _textureDictionary[pokemon], new Point(260, 47), 13, 20, 3, 0, 1, 2, 0.4f));
                        sprite.AnimationPlayer.AddAnimation(new Animation("MoveRight", _textureDictionary[pokemon], new Point(175, 49), 20, 18, 3, 0, 1, 2, 0.4f, true, SpriteEffects.FlipHorizontally));
                        sprite.AnimationPlayer.AddAnimation(new Animation("MoveDown", _textureDictionary[pokemon], new Point(102, 46), 13, 20, 3, 0, 1, 2, 0.4f));
                        sprite.AnimationPlayer.AddAnimation(new Animation("MoveLeft", _textureDictionary[pokemon], new Point(175, 49), 20, 18, 3, 0, 1, 2, 0.4f));
                    break;

                default:
                    break;
            }
        }
    }
}
