using Microsoft.Xna.Framework;
using MysteryDungeon.Core.Components;
using MysteryDungeon.Core.Controllers;
using MysteryDungeon.Core.Data;
using MysteryDungeon.Core.Map;

namespace MysteryDungeon.Core.Entities
{
    public class LivingEntity : Entity
    {
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }

        protected AnimatorController _animatorController;
        protected GridMovementComponent _gridMovementComponent;

        public LivingEntity() : base()
        {

        }

        protected virtual void Initialise(Pokemon pokemon)
        {
            _gridMovementComponent = AddComponent<GridMovementComponent>() as GridMovementComponent;
            _gridMovementComponent.Tilegrid = Level.Dungeon.Tilemap.Tilegrid;



            _animatorController = new AnimatorController();
            _animatorController.SetDefaultState("IdleDown");

            _animatorController.AddParameter<float>("SpeedX");
            _animatorController.AddParameter<float>("SpeedY");
            _animatorController.AddParameter<int>("ViewDirectionX");
            _animatorController.AddParameter<int>("ViewDirectionY");

            var moveUp = _animatorController.AddState("MoveUp");
            moveUp.AddCondition("SpeedX", sy => sy == 0.0f);
            moveUp.AddCondition("SpeedY", sy => sy < 0.0f);
            moveUp.AddCondition("ViewDirectionY", vd => vd == -1);

            var moveRight = _animatorController.AddState("MoveRight");
            moveRight.AddCondition("SpeedX", sx => sx > 0.0f);
            moveRight.AddCondition("SpeedY", sy => sy == 0.0f);
            moveRight.AddCondition("ViewDirectionX", vd => vd == 1);

            var moveDown = _animatorController.AddState("MoveDown");
            moveDown.AddCondition("SpeedX", sy => sy == 0.0f);
            moveDown.AddCondition("SpeedY", sy => sy > 0.0f);
            moveDown.AddCondition("ViewDirectionY", vd => vd == 1);

            var moveLeft = _animatorController.AddState("MoveLeft");
            moveLeft.AddCondition("SpeedX", sx => sx < 0.0f);
            moveLeft.AddCondition("SpeedY", sy => sy == 0.0f);
            moveLeft.AddCondition("ViewDirectionX", vd => vd == -1);



            var idleUp = _animatorController.AddState("IdleUp");
            idleUp.AddCondition("SpeedX", sy => sy == 0.0f);
            idleUp.AddCondition("SpeedY", sy => sy == 0.0f);
            idleUp.AddCondition("ViewDirectionY", vd => vd == -1);

            var idleRight = _animatorController.AddState("IdleRight");
            idleRight.AddCondition("SpeedX", sy => sy == 0.0f);
            idleRight.AddCondition("SpeedY", sy => sy == 0.0f);
            idleRight.AddCondition("ViewDirectionX", vd => vd == 1);

            var idleDown = _animatorController.AddState("IdleDown");
            idleDown.AddCondition("SpeedX", sy => sy == 0.0f);
            idleDown.AddCondition("SpeedY", sy => sy == 0.0f);
            idleDown.AddCondition("ViewDirectionY", vd => vd == 1);

            var idleLeft = _animatorController.AddState("IdleLeft");
            idleLeft.AddCondition("SpeedX", sy => sy == 0.0f);
            idleLeft.AddCondition("SpeedY", sy => sy == 0.0f);
            idleLeft.AddCondition("ViewDirectionX", vd => vd == -1);



            AnimatedSpriteComponent animatedSpriteComponent = AddComponent<AnimatedSpriteComponent>() as AnimatedSpriteComponent;
            animatedSpriteComponent.AnimatorController = _animatorController;
            animatedSpriteComponent.AnimationDictionary = PokemonSpriteData.LoadAnimations(pokemon);

            SpriteRendererComponent spriteRendererComponent = AddComponent<SpriteRendererComponent>() as SpriteRendererComponent;
            spriteRendererComponent.Sprite = animatedSpriteComponent.Sprite;
        }

        public override void Update(GameTime gameTime)
        {
            _animatorController.SetParameter("SpeedX", _gridMovementComponent.Speed.X);
            _animatorController.SetParameter("SpeedY", _gridMovementComponent.Speed.Y);
            _animatorController.SetParameter("ViewDirectionX", _gridMovementComponent.ViewDirection.X);
            _animatorController.SetParameter("ViewDirectionY", _gridMovementComponent.ViewDirection.Y);

            base.Update(gameTime);
        }
    }
}
