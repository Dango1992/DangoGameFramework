using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using UnityEngine;

namespace Dango
{
    public class TestDataModel2 : DataModelBase
    {
        public static readonly int EventId = typeof(TestDataModel2).GetHashCode();

        public override int Id
        {
            get { return EventId; }
        }
        public override void OnNotify(object sender, GameEventArgs e)
        {
            
        }

        public override void Clear()
        {
            
        }

        public override void Subscribe()
        {
        }

        public override void Unsubscribe()
        {
            
        }
    }
}
