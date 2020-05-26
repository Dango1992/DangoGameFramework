using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Event;
using Google.Protobuf;
using Dango.Network;

namespace Dango
{
    public class TestDataEventArgs : MsgEventArgs
    {
        /// <summary>
        /// 接收服务器消息事件编号。
        /// </summary>
        public static readonly int EventId = typeof(TestDataEventArgs).GetHashCode();
        
        /// <summary>
        /// 获取接收服务器消息事件编号。
        /// </summary>
        public override int Id
        {
            get { return EventId; }
        }
        
        /// <summary>
        /// 获取消息主体
        /// </summary>
        public override object Body { get; protected set; }

        public override MsgEventArgs Fill(object body)
        {
            this.Body = body;
            return this;
        }

        public override int GetDefine()
        {
            throw new System.NotImplementedException();
        }

        public override IMessage GetMessage()
        {
            throw new System.NotImplementedException();
        }

        public override MessageParser GetParser()
        {
            throw new System.NotImplementedException();
        }

        public override void PrcessMsg(IMessage msgCreate)
        {
            throw new System.NotImplementedException();
        }

        public override void Clear()
        {
            this.Body = null;
        }

        
    }
}

