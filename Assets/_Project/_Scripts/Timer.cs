using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Akari
{
    public class Timer : MonoBehaviour
    {
        public float TimeLeft { get; private set; }
        public bool IsRunning { get; private set; }

        public void StartTimer(float time)
        {
            TimeLeft = time;
            IsRunning = true;
        }

        public void StopTimer()
        {
            IsRunning = false;
        }

        public void IncreaseTimer(float time)
        {
            TimeLeft += time;
        }

        public void ReduceTimer(float time)
        {
            TimeLeft -= time;
        }

        private void Update()
        {
            if (IsRunning)
            {
                TimeLeft -= Time.deltaTime;
                if (TimeLeft <= 0)
                {
                    IsRunning = false;
                    TimeLeft = 0;
                }
            }
        }
    }
}
