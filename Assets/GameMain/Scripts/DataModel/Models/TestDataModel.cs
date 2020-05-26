using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using Dango.Core;
using Dango.Network;
using GameFramework;
using UnityEngine;

namespace Dango
{
    public class TestDataModel : UnitSingleton<TestDataModel>,IDataModel
    {
        public void Clear()
        {
            ip = string.Empty;
            prot = 0;
            checkOutText = string.Empty;
        }

        private string ip;
        private int prot;
        private string checkOutText;

        public void Subscribe()
        {
            GameEntry.Event.Subscribe(TestDataEventArgs.EventId,OnNotify);
        }

        public void Unsubscribe()
        {
            GameEntry.Event.Subscribe(TestDataEventArgs.EventId,OnNotify);
        }

        public void OnNotify(object sender, GameEventArgs e)
        {
            TestDataEventArgs ne = e as TestDataEventArgs;
            
            ip = ne.Body.ip;
            prot = ne.Body.prot;
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

