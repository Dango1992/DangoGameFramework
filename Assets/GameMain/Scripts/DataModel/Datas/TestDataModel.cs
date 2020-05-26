using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using Dango.Core;
using Dango.Network;
using UnityEngine;

namespace Dango
{
    public class TestDataModel : UnitSingleton<TestDataModel>,IDataModel
    {
        public void Clear()
        {
            user = string.Empty;
        }

        private string user;

        public void Subscribe()
        {
            //GameEntry.Event.Subscribe(LSRespLoginLEventArgs.EventId,OnNotify);
        }

        public void Unsubscribe()
        {
            //GameEntry.Event.Subscribe(LSRespLoginLEventArgs.EventId,OnNotify);
        }

        public void OnNotify(object sender, GameEventArgs e)
        {
            //LSRespLoginLEventArgs args = e as LSRespLoginLEventArgs;
            
            //user = args.Body.ToString();
        }

        public string GetUser()
        {
            return user;
        }
    }
}

