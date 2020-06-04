using System.Collections;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;

namespace Dango
{
    public class TimerInfo : IReference
    {
        public delegate void TimerUpdateDelegate();

        public string className;
        public long tick;
        public bool stop;
        public bool delete;

        private TimerUpdateDelegate onTimerUpdate;

        public void Fill(string className, TimerUpdateDelegate onTimerUpdate = null)
        {
            this.className = className;
            this.delete = false;
            this.onTimerUpdate = onTimerUpdate;
        }

        public void TimerUpdate()
        {
            onTimerUpdate?.Invoke();
        }

        public void Clear()
        {
            this.className = string.Empty;
            this.tick = 0;
            this.stop = false;
            this.delete = false;
            this.onTimerUpdate = null;
        }
    }
}