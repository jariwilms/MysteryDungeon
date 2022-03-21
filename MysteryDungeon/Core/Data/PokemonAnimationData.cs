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

        static PokemonAnimationData() { }

        public static List<Animation> GetAnimations(Pokemon pokemon)
        {
            if (_animationDictionary[pokemon] == null)
                LoadAnimations(pokemon);

            return _animationDictionary[pokemon];
        }

        //We only load the animations the moment we need them
        private static void LoadAnimations(Pokemon pokemon)
        {

        }
    }
}
