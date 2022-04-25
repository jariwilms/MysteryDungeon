using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MysteryDungeon.Core.UI
{
    public class FrameCounter
    {
        public const int MAXIMUM_SAMPLES = 60;

        public Queue<float> FrameTimeBuffer { get; private set; }

        public float ElapsedMilliseconds { get; private set; }

        public long TotalFrames { get; private set; }
        public float TotalSeconds { get; private set; }

        public float CurrentFramesPerSecond { get; private set; }
        public float AverageFramesPerSecond { get; private set; }

        private float _deltaTime;

        public FrameCounter()
        {
            FrameTimeBuffer = new Queue<float>();
            for (int i = 0; i < MAXIMUM_SAMPLES; i++)
                FrameTimeBuffer.Enqueue(0.0f);
        }

        public bool Update(GameTime gameTime)
        {
            _deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            CurrentFramesPerSecond = 1.0f / _deltaTime;

            FrameTimeBuffer.Dequeue();
            FrameTimeBuffer.Enqueue(CurrentFramesPerSecond);

            AverageFramesPerSecond = FrameTimeBuffer.Average(i => i);

            TotalFrames++;
            TotalSeconds += _deltaTime;

            return true;
        }
    }
}
