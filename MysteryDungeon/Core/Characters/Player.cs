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
        public int CurrentHealth;
        public int MaxHealth;

        public List<PathNode> Nodes; //to be removed, dient voor pathfinding 

        public Player(Level dungeon) : base() //move pokemon name naar ctor
        {
            Level = dungeon;

            Sprite = PokemonSpriteData.GetSprite(Pokemon.Chikorita);
            Sprite.AnimationPlayer.PlayAnimation("Idle");

            CreateActions();
        }

        protected override void CreateActions()
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

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
