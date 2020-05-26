using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Event;
using Google.Protobuf;
using Dango.Network;

namespace Dango
{
    public class TestDataEventArgs : MsgEventArgs<ServerInfo>
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
        public override ServerInfo Body { get; protected set; }

        public override MsgEventArgs<ServerInfo> Fill(ServerInfo body)
        {
            this.Body = body;
            return this;
        }
        
        public override void Clear()
        {
            this.Body.Clear();
        }
    }
}

