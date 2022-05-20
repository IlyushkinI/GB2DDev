using System;


namespace Reward
{
    public interface ITimer
    {
        void StopTimer();
        void StartTimer(int seconds);
        event Action Tick;
        event Action TimerFinish;
    }
}