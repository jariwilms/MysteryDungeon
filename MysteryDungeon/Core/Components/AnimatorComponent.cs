using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MysteryDungeon.Core.Controllers;
using System;
using System.Collections.Generic;

namespace MysteryDungeon.Core.Animations
{
    public class AnimatorComponent : Component
    {
        public Animation CurrentAnimation { get; protected set; }
        public AnimatorController AnimatorController { get; set; }

        private Dictionary<string, Animation> _animationDictionary;

        public bool IsPlaying { get; protected set; }

        public AnimatorComponent(GameObject parent) : base(parent)
        {
            _animationDictionary = new Dictionary<string, Animation>();
            IsPlaying = true;
        }

        public void AddAnimation(Animation animation)
        {
            if (_animationDictionary.ContainsKey(animation.Identifier))
                throw new Exception(String.Format("An animation with animation.Identifier {0} already exists", animation.Identifier));

            _animationDictionary.Add(animation.Identifier, animation);
        }

        public void PlayAnimation(string identifier)
        {
            bool found = _animationDictionary.TryGetValue(identifier, out Animation animation);

            if (!found)
                throw new Exception(String.Format("An animation with identifier {0} does not exist", identifier));

            CurrentAnimation = animation;
        }

        public void Resume()
        {
            IsPlaying = true;
        }

        public void Pause()
        {
            IsPlaying = false;
        }

        public void TogglePlay()
        {
            IsPlaying = !IsPlaying;
        }

        public void NextFrame()
        {
            CurrentAnimation.NextFrame();
        }

        public void PreviousFrame()
        {
            CurrentAnimation.PreviousFrame();
        }

        public override void Update(GameTime gameTime)
        {
            if (!IsPlaying)
                return;

            AnimatorController.Update();
            PlayAnimation(AnimatorController.CurrentState);

            CurrentAnimation.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

        }

        //private void UpdateAnimation() //move naar animatorcomponent
        //{
        //    string animationIdentifier;

        //    if (IsMoving)
        //    {
        //        animationIdentifier = MovementDirection switch
        //        {
        //            Direction.North => "MoveUp",
        //            Direction.East => "MoveRight",
        //            Direction.South => "MoveDown",
        //            Direction.West => "MoveLeft",
        //            _ => "IdleDown",
        //        };
        //    }
        //    else
        //    {
        //        animationIdentifier = ViewDirection switch
        //        {
        //            Direction.North => "IdleUp",
        //            Direction.East => "IdleRight",
        //            Direction.South => "IdleDown",
        //            Direction.West => "IdleLeft",
        //            _ => "IdleDown",
        //        };
        //    }

        //    Sprite.AnimationPlayer.PlayAnimation(animationIdentifier);
        //}
    }
}
