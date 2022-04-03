using MysteryDungeon.Core.Components;
using MysteryDungeon.Core.Controllers;
using MysteryDungeon.Core.Data;

namespace MysteryDungeon.Core.Entities
{
    public class LivingEntity : Entity
    {
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }

        protected AnimatorController AnimatorController { get; set; }

        public LivingEntity(Pokemon pokemon) : base()
        {
            AnimatorController = new AnimatorController();
            AnimatorController.SetDefaultState("IdleDown");

            AnimatorController.AddParameter<float>("VelocityX");
            AnimatorController.AddParameter<float>("VelocityY");
            AnimatorController.AddParameter<int>("ViewDirectionX");
            AnimatorController.AddParameter<int>("ViewDirectionY");

            var moveUp = AnimatorController.AddState("MoveUp");
            moveUp.AddCondition("VelocityX", sy => sy == 0.0f);
            moveUp.AddCondition("VelocityY", sy => sy < 0.0f);
            moveUp.AddCondition("ViewDirectionY", vd => vd == -1);

            var moveRight = AnimatorController.AddState("MoveRight");
            moveRight.AddCondition("VelocityX", sx => sx > 0.0f);
            moveRight.AddCondition("VelocityY", sy => sy == 0.0f);
            moveRight.AddCondition("ViewDirectionX", vd => vd == 1);

            var moveDown = AnimatorController.AddState("MoveDown");
            moveDown.AddCondition("VelocityX", sy => sy == 0.0f);
            moveDown.AddCondition("VelocityY", sy => sy > 0.0f);
            moveDown.AddCondition("ViewDirectionY", vd => vd == 1);

            var moveLeft = AnimatorController.AddState("MoveLeft");
            moveLeft.AddCondition("VelocityX", sx => sx < 0.0f);
            moveLeft.AddCondition("VelocityY", sy => sy == 0.0f);
            moveLeft.AddCondition("ViewDirectionX", vd => vd == -1);

            var idleUp = AnimatorController.AddState("IdleUp");
            idleUp.AddCondition("VelocityX", sy => sy == 0.0f);
            idleUp.AddCondition("VelocityY", sy => sy == 0.0f);
            idleUp.AddCondition("ViewDirectionY", vd => vd == -1);

            var idleRight = AnimatorController.AddState("IdleRight");
            idleRight.AddCondition("VelocityX", sy => sy == 0.0f);
            idleRight.AddCondition("VelocityY", sy => sy == 0.0f);
            idleRight.AddCondition("ViewDirectionX", vd => vd == 1);

            var idleDown = AnimatorController.AddState("IdleDown");
            idleDown.AddCondition("VelocityX", sy => sy == 0.0f);
            idleDown.AddCondition("VelocityY", sy => sy == 0.0f);
            idleDown.AddCondition("ViewDirectionY", vd => vd == 1);

            var idleLeft = AnimatorController.AddState("IdleLeft");
            idleLeft.AddCondition("VelocityX", sy => sy == 0.0f);
            idleLeft.AddCondition("VelocityY", sy => sy == 0.0f);
            idleLeft.AddCondition("ViewDirectionX", vd => vd == -1);



            AnimatedSpriteComponent animatedSpriteComponent = AddComponent<AnimatedSpriteComponent>();
            animatedSpriteComponent.AnimatorController = AnimatorController;
            animatedSpriteComponent.AnimationDictionary = PokemonSpriteData.LoadAnimations(pokemon);

            SpriteRendererComponent spriteRendererComponent = AddComponent<SpriteRendererComponent>();
            spriteRendererComponent.Sprite = animatedSpriteComponent.Sprite;
        }
    }
}
