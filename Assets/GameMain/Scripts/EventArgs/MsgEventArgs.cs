using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Event;
using Google.Protobuf;
using Dango.Network;


namespace Dango.Network
{
    public abstract class MsgEventArgs<T> : GameEventArgs
    {
        public virtual T Body { get; protected set; }

        public virtual MsgEventArgs<T> Fill(T body)
        {
            this.Body = body;
            return this;
        }
    }
}