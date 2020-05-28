using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using Dango.Core;
using Dango.Network;
using GameFramework;
using UnityEngine;

namespace Dango
{
    public class TestDataModel : DataModelBase
    {
        public static readonly int EventId = typeof(TestDataModel).GetHashCode();
        public override int Id
        {
            get { return EventId; }
        }

        public override void Clear()
        {
            ip = string.Empty;
            port = 0;
            checkOutText = string.Empty;
        }

        private string ip;
        private int port;
        private string checkOutText;

        public override void Subscribe()
        {
            GameEntry.Event.Subscribe(TestDataEventArgs.EventId,UpdateData);
        }

        public override void Unsubscribe()
        {
            GameEntry.Event.Subscribe(TestDataEventArgs.EventId,UpdateData);
        }

        public override void OnNotify(object sender, GameEventArgs e)
        {
            //通知所有model
        }

        private void UpdateData(object sender, GameEventArgs e)
        {
            TestDataEventArgs ne = e as TestDataEventArgs;
            
            ip = ne.Body.ip;
            port = ne.Body.port;
            checkOutText = ne.Body.checkOutText;
            
            TestLoginSuccessfulEventArgs eventArgs = ReferencePool.Acquire<TestLoginSuccessfulEventArgs>();
            GameEntry.Event.Fire(this,eventArgs);
        }

        public string GetCheckOutText()
        {
            return checkOutText;
        }
    }
}

