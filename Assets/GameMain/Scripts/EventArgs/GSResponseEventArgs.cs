using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using UnityEngine;

namespace Dango
{
    public class GSResponseEventArgs : GameEventArgs
    {
        /// <summary>
        /// 接收服务器消息事件编号。
        /// </summary>
        public static readonly int EventId = typeof(GSResponseEventArgs).GetHashCode();

        /// <summary>
        /// 获取接收服务器消息事件编号。
        /// </summary>
        public override int Id
        {
            get { return EventId; }
        }

        /// <summary>
        /// 消息Id
        /// </summary>
        public int MsgId { get; private set; }

        public GSResponseEventArgs Fill(int msgId)
        {
            this.MsgId = msgId;
            return this;
        }

        public override void Clear()
        {
            MsgId = 0;
        }
    }
}