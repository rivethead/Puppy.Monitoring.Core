using System;
using Puppy.Monitoring.Events;

namespace Puppy.Monitoring.Pipeline.Pipelets.Notification
{
    public class AchievementUnlockedNotifier : INotifyBasedOnEvent
    {
        private readonly string achievementName;

        public AchievementUnlockedNotifier(string achievementName)
        {
            this.achievementName = achievementName;
        }

        public void Raise(IEvent @event)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("{0} unlocked", achievementName);
            Console.ForegroundColor = oldColor;
        }
    }
}