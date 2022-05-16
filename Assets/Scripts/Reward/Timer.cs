using System;
using System.Collections;
using UnityEngine;


namespace Reward
{
    public sealed class Timer : MonoBehaviour, ITimer
    {

        private event Action _tick;
        private event Action _timerFinish;

        private IEnumerator TimerCoroutine(int time)
        {
            yield return new WaitForSecondsRealtime(time);
            _timerFinish.Invoke();
            StopAllCoroutines();
        }

        private IEnumerator EverySecond()
        {
            yield return new WaitForSecondsRealtime(1.0f);
            _tick.Invoke();
            StartCoroutine(EverySecond());
        }

        #region ITimer

        public event Action Tick
        {
            add { _tick += value; }
            remove { _tick -= value; }
        }

        public event Action TimerFinish
        {
            add { _timerFinish += value; }
            remove { _timerFinish -= value; }
        }

        public void StartTimer(int seconds)
        {
            StartCoroutine(EverySecond());
            StartCoroutine(TimerCoroutine(seconds));
        }

        #endregion

    }
}