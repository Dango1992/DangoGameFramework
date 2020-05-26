using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using Dango.Core;
using Dango.Network;
using UnityEngine;
using UnityGameFramework.Runtime;


namespace Dango
{
    /// <summary>
    /// 基础组件。
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/DataModel")]
    public partial class DataModelComponent : GameFrameworkComponent
    {
        
        private void Start()
        {
            DataRegister();
            Subscribe();
            NetworkSubscribe();
        }
        
        private void OnDestroy()
        {
            Unsubscribe();
            DataClear();
        }
        
        private void DataRegister()
        {
            Register<TestDataModel>();
        }

        private void NetworkSubscribe()
        {
            GameEntry.Event.Subscribe(GSResponseEventArgs.EventId,OnNotify);
        }

        private void OnNotify(object sender, GameEventArgs e)
        {
            GSResponseEventArgs ne = (GSResponseEventArgs)e;
            MsgEventArgs msg = null;
            
            switch (ne.MsgId)
            {
                case 1000:
                    msg = ReferencePool.Acquire<TestDataEventArgs>();
                    break;
            }
            GameEntry.Event.FireNow(sender,msg);
        }
    }
}
