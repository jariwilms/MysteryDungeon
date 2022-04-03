using Microsoft.Xna.Framework;
using MysteryDungeon.Core.Animations;
using MysteryDungeon.Core.Controllers;
using System;
using System.Collections.Generic;

namespace MysteryDungeon.Core.Components
{
    public class AnimatedSpriteComponent : SpriteComponent
    {
        public AnimatorController AnimatorController { get; set; }
        public Animation CurrentAnimation { get; protected set; }
        public string CurrentAnimationIdentifier { get; protected set; }

        public Dictionary<string, Animation> AnimationDictionary;

        protected bool IsPlaying { get; set; }

        public AnimatedSpriteComponent(GameObject parent) : base(parent)
        {
            AnimationDictionary = new Dictionary<string, Animation>();
            IsPlaying = true;
        }

        public void AddAnimation(string identifier, Animation animation)
        {
            if (AnimationDictionary.ContainsKey(identifier))
                throw new Exception(String.Format("An animation with animation.Identifier {0} already exists", identifier));

            AnimationDictionary.Add(identifier, animation);
        }

        public void PlayAnimation(string identifier)
        {
            if (identifier == CurrentAnimationIdentifier)
                return;

            bool found = AnimationDictionary.TryGetValue(identifier, out Animation animation);

            if (!found)
                throw new Exception(String.Format("An animation with identifier {0} does not exist", identifier));

            CurrentAnimation?.Reset(); //Reset previous animation

            CurrentAnimation = animation; //assign requested animation as new animation
            CurrentAnimationIdentifier = identifier; //set identifier to new animation
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

            Sprite.SourceTexture = CurrentAnimation.SourceTexture;
            Sprite.SourceRectangle = CurrentAnimation.SourceRectangle;
            Sprite.SpriteEffects = CurrentAnimation.SpriteEffects;
        }
    }
}
