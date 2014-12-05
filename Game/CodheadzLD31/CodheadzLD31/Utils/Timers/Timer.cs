using Microsoft.Xna.Framework;
using System;

namespace CodheadzLD31.Utils.Timers
{
    public class Timer
    {
        public Timer(TimeSpan frequency, TimerMode mode)
        {
            this.TimerMode = mode;
            this.Frequency = frequency;
            this.timeToFire = frequency;
            this.Stop();
        }

        private TimeSpan timeToFire;

        private bool running;
        public TimeSpan Frequency { get; set; }
        public bool IsRunning { get { return running; } }
        public TimerMode TimerMode { get; set; }
        public event EventHandler TimeReached;
        public TimeSpan TimeToFire { get { return timeToFire; } }

        public void Start()
        {
            if (!running)
            {
                timeToFire = Frequency;
                running = true;
            }
        }
        public void Stop()
        {
            running = false;
        }

        public void Update(GameTime gameTime)
        {
            if (running)
            {
                timeToFire = timeToFire.Subtract(gameTime.ElapsedGameTime);
                if (timeToFire < TimeSpan.Zero)
                {
                    OnTimerReached();
                    if (TimerMode == TimerMode.Single)
                    {
                        running = false;
                    }
                    else
                    {
                        timeToFire = Frequency;
                    }

                }
            }
        }

        protected virtual void OnTimerReached()
        {
            if (TimeReached != null)
                TimeReached(this, EventArgs.Empty);
        }

        public void AddSecondsToNextFire(int secondsToAdd)
        {
            timeToFire = timeToFire.Add(new TimeSpan(0, 0, secondsToAdd));
        }
    }
}
