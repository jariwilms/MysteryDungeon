using MysteryDungeon.Core.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryDungeon.Core.Data
{
    public static class PokemonAnimationData
    {
        private static Dictionary<Pokemon, List<Animation>> _animationDictionary;

        static PokemonAnimationData()
        {
            _animationDictionary = new Dictionary<Pokemon, List<Animation>>();
        }

        public static List<Animation> GetAnimations(Pokemon pokemon)
        {
            if (_animationDictionary[pokemon] == null)
                LoadAnimations(pokemon);

            return _animationDictionary[pokemon];
        }

        //We only load the animations the moment we need them
        private static void LoadAnimations(Pokemon pokemon)
        {
            //switch (pokemon)
            //{
            //    case Pokemon.Pikachu:
            //        break;

            //    case Pokemon.Chikorita:
            //        _animationDictionary.Add(new Animation("Idle", _textureDictionary[pokemon], new Point(98, 20), 16, 21, 1, 0, 1, 2, 0.8f));
            //        _animationDictionary.Add(new Animation("MoveUp", _textureDictionary[pokemon], new Point(260, 47), 13, 20, 3, 0, 1, 2, 0.4f));
            //        _animationDictionary.Add(new Animation("MoveRight", _textureDictionary[pokemon], new Point(175, 49), 20, 18, 3, 0, 1, 2, 0.4f, true, SpriteEffects.FlipHorizontally));
            //        _animationDictionary.Add(new Animation("MoveDown", _textureDictionary[pokemon], new Point(102, 46), 13, 20, 3, 0, 1, 2, 0.4f));
            //        _animationDictionary.Add(new Animation("MoveLeft", _textureDictionary[pokemon], new Point(175, 49), 20, 18, 3, 0, 1, 2, 0.4f));
            //        break;

            //    default:
            //        break;
            //}
        }
    }
}
