using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using UnityEngine;

namespace Dango
{
    public class TestLoginSuccessfulEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(TestLoginSuccessfulEventArgs).GetHashCode();

        public override void Clear()
        {
        }

        public override int Id
        {
            get { return EventId; }
        }
    }
}