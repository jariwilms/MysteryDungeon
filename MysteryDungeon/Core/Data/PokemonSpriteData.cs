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
            
            Content = new ContentManager(new GameServiceContainer(), "Content");
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
                        sprite.AnimationPlayer.AddAnimation("Idle", new Animation(sprite.Texture, new Point(98, 20), 16, 21, 1, 0, 1, 2, 0.8f));
                        sprite.AnimationPlayer.AddAnimation("MoveUp", new Animation(sprite.Texture, new Point(260, 47), 13, 20, 3, 0, 1, 2, 0.4f));
                        sprite.AnimationPlayer.AddAnimation("MoveRight", new Animation(sprite.Texture, new Point(175, 49), 20, 18, 3, 0, 1, 2, 0.4f, true, SpriteEffects.FlipHorizontally));
                        sprite.AnimationPlayer.AddAnimation("MoveDown", new Animation(sprite.Texture, new Point(102, 46), 13, 20, 3, 0, 1, 2, 0.4f));
                        sprite.AnimationPlayer.AddAnimation("MoveLeft", new Animation(sprite.Texture, new Point(175, 49), 20, 18, 3, 0, 1, 2, 0.4f));
                    break;

                default:
                    break;
            }

        }
    }
}
