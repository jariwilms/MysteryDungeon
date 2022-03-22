using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MysteryDungeon.Core.Animations
{
    public class AnimationPlayer
    {
        public enum AnimationMode
        {
            Single,
            Multiple,
        }

        public Animation CurrentAnimation { get; protected set; }
        private Dictionary<string, Animation> _animationDictionary;
        private AnimationMode _animationMode;

        public bool IsPlaying { get; protected set; }

        public AnimationPlayer(AnimationMode animationMode = AnimationMode.Single)
        {
            _animationMode = animationMode;
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

        public void Update(GameTime gameTime)
        {
            if (!IsPlaying)
                return;

            CurrentAnimation.Update(gameTime);
        }
    }
}
