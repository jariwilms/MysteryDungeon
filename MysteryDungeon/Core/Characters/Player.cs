using Microsoft.Xna.Framework;
using MysteryDungeon.Core.Animations;
using MysteryDungeon.Core.Components;
using MysteryDungeon.Core.Controllers;
using MysteryDungeon.Core.Data;
using MysteryDungeon.Core.Map;

namespace MysteryDungeon.Core.Characters
{
    public class Player : Actor
    {
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }

        protected AnimatorController _animatorController;
        protected GridMovementComponent _gridMovementComponent;
        protected SpriteRendererComponent _spriteRendererComponent;

        public Player(Pokemon pokemon) : base() //move pokemon name naar ctor
        {
            Setup(pokemon);
        }

        public void Setup(Pokemon pokemon)
        {
            _animatorController = new AnimatorController();
            _animatorController.SetDefaultState("IdleDown");

            _animatorController.AddParameter<float>("SpeedX");
            _animatorController.AddParameter<float>("SpeedY");

            var upState = _animatorController.AddState("IdleUp");
            var rightState = _animatorController.AddState("IdleRight");
            var downState = _animatorController.AddState("IdleDown");
            var leftState = _animatorController.AddState("IdleLeft");

            upState.AddCondition("SpeedY", speed => speed >= 0);
            downState.AddCondition("SpeedY", speed => speed < 0);

            leftState.AddCondition("SpeedX", speed => speed < 0);
            rightState.AddCondition("SpeedX", speed => speed > 0); //moet dit nog wat fixen enzo, deze predicates werken niet altijd wrs

            AnimatorComponent animatorComponent = AddComponent<AnimatorComponent>() as AnimatorComponent;
            animatorComponent.AnimatorController = _animatorController;

            Sprite = PokemonSpriteData.LoadSprite(Pokemon.Chikorita);
            PokemonSpriteData.LoadAnimations(Pokemon.Chikorita, animatorComponent);



            _spriteRendererComponent = AddComponent<SpriteRendererComponent>() as SpriteRendererComponent;
            _spriteRendererComponent.Sprite = Sprite;



            _gridMovementComponent = AddComponent<GridMovementComponent>() as GridMovementComponent;
            _gridMovementComponent.Tilegrid = Level.Dungeon.Tilemap.Tilegrid;
        }

        public override void Update(GameTime gameTime)
        {
            _animatorController.SetParameter("SpeedX", _gridMovementComponent.Speed.X);
            _animatorController.SetParameter("SpeedY", _gridMovementComponent.Speed.Y);

            base.Update(gameTime);
        }
    }
}
