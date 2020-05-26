using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Event;
using Google.Protobuf;
using Dango.Network;


namespace Dango.Network
{
    public abstract class MsgEventArgs : GameEventArgs
    {
        public virtual object Body { get; protected set; }

        public virtual MsgEventArgs Fill(object body)
        {
            this.Body = body;
            return this;
        }

        public abstract int GetDefine();
        public abstract IMessage GetMessage();
        public abstract MessageParser GetParser();
        public abstract void PrcessMsg(IMessage msgCreate);
    }
}