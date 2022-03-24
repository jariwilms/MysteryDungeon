using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MysteryDungeon.Core.Animations;
using System;

namespace MysteryDungeon.Core.Characters
{
    public abstract class Actor : GameObject
    {
        public Sprite Sprite { get; set; }

        public event Action OnSpawn;
        public event Action OnDefeat;
        public event Action OnMoveStart;
        public event Action OnMoveFinished;

        public event Action OnTurnStart;
        public event Action OnTurnFinished;

        public Actor() : base()
        {

        }

        protected virtual void CreateAnimations() //laat buitenstaande class animations toevoegen
        {

        }

        //Move to sprite class + override, also put in list of actions => foreach iteration
        protected virtual void CreateActions()
        {

        } //insgelijks

        public override void Update(GameTime gameTime)
        {
            Components.ForEach(component => component.Update(gameTime));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Components.ForEach(component => component.Draw(spriteBatch));
        }
    }
}
