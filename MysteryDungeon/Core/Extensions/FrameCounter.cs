using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MysteryDungeon.Core.Extensions
{
    public class FrameCounter
    {
        public const int MAXIMUM_SAMPLES = 128;

        public long TotalFrames { get; private set; }
        public float TotalSeconds { get; private set; }

        public float CurrentFramesPerSecond { get; private set; }
        public float AverageFramesPerSecond { get; private set; }

        public double ElapsedMilliseconds { get; private set; }

        private Queue<float> _sampleBuffer = new Queue<float>();

        public FrameCounter()
        {
            ElapsedMilliseconds = 0.0;
        }

        public bool Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            CurrentFramesPerSecond = 1.0f / deltaTime;
            ElapsedMilliseconds = gameTime.ElapsedGameTime.TotalMilliseconds;

            _sampleBuffer.Enqueue(CurrentFramesPerSecond);

            if (_sampleBuffer.Count > MAXIMUM_SAMPLES)
            {
                _sampleBuffer.Dequeue();
                AverageFramesPerSecond = _sampleBuffer.Average(i => i);
            }
            else
            {
                AverageFramesPerSecond = CurrentFramesPerSecond;
            }

            TotalFrames++;
            TotalSeconds += deltaTime;

            return true;
        }
    }
}
