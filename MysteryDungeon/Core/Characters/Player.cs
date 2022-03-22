using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MysteryDungeon.Core.Animations;
using MysteryDungeon.Core.Data;
using MysteryDungeon.Core.Extensions;
using MysteryDungeon.Core.Input;
using MysteryDungeon.Core.Map;
using System.Collections.Generic;

namespace MysteryDungeon.Core.Characters
{
    public class Player : Actor
    {
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }

        public Player(Pokemon pokemon, Dungeon dungeon) : base() //move pokemon name naar ctor
        {
            Dungeon = dungeon;

            Sprite = PokemonSpriteData.GetSprite(pokemon);
            Sprite.AnimationPlayer.PlayAnimation("Idle");

            CreateActions();
        }

        protected override void CreateActions() //moet eigenlijk voor elke entity gedaan worden, ai stuurt dan deze actions aan... => move uit player class
        {
            MoveUpAction = () => { MoveTo(MovementDirection.North); };
            MoveRightAction = () => { MoveTo(MovementDirection.East); };
            MoveDownAction = () => { MoveTo(MovementDirection.South); };
            MoveLeftAction = () => { MoveTo(MovementDirection.West); };

            InputEventHandler.Instance.AddEventListener(KeyAction.Up, MoveUpAction);
            InputEventHandler.Instance.AddEventListener(KeyAction.Right, MoveRightAction);
            InputEventHandler.Instance.AddEventListener(KeyAction.Down, MoveDownAction);
            InputEventHandler.Instance.AddEventListener(KeyAction.Left, MoveLeftAction);
        }
    }
}
