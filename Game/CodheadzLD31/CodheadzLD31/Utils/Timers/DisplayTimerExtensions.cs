using System;

namespace CodheadzLD31.Utils.Timers
{
    public static class DisplayTimerExtensions
    {
        public static string DisplayTime(this TimeSpan timeSpan)
        {
            string clock = timeSpan.Minutes.ToString("D2") + ":"
                           + timeSpan.Seconds.ToString("D2");
            return clock;
        }

        public static string DisplayTimeSS(this TimeSpan timeSpan)
        {
            string clock = String.Format("{0:0}", timeSpan.TotalSeconds);
            return clock;
        }
    }
}
